using DanMu.Pan.Data.Entities;
using DanMu.Pan.Data.Info;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DanMu.Pan.Domain.Context;

/// <summary>
/// 第一个参数 User: 指定自定义的用户实体类，继承自 IdentityUser<Guid>
/// 第二个参数 IdentityRole<Guid>: 使用默认的角色类，主键类型为 Guid
/// 第三个参数 Guid: 指定用户和角色的主键类型为 Guid
/// </summary>
public class DocumentContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public DocumentContext(DbContextOptions options)
        : base(options) { }

    public DbSet<Document> Documents { get; set; }
    public DbSet<DocumentDeleted> DocumentDeleteds { get; set; } // 关系表
    public DbSet<DocumentStarred> DocumentStarreds { get; set; } // 关系表
    public DbSet<DocumentUserPermission> DocumentUserPermissions { get; set; } // 关系表
    public DbSet<DocumentVersion> DocumentVersions { get; set; }

    public DbSet<LoginAudit> LoginAudits { get; set; }

    public DbSet<PhysicalFolder> PhysicalFolders { get; set; }
    public DbSet<PhysicalFolderUser> PhysicalFolderUsers { get; set; } // 关系表

    public DbSet<SharedDocumentUser> SharedDocumentUsers { get; set; } // 关系表

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        #region Document
        modelBuilder.Entity<Document>(entity =>
        {
            entity.ToTable("T_Document");
            entity.HasKey(e => e.Id);
            entity
                .HasOne(e => e.PhysicalFolder)
                .WithMany()
                .HasForeignKey(e => e.PhysicalFolderId)
                .OnDelete(DeleteBehavior.NoAction); // 禁用级联删除 数据完整性保护
            entity
                .HasOne(e => e.CreatedByUser)
                .WithMany()
                .HasForeignKey(e => e.CreatedBy)
                .OnDelete(DeleteBehavior.NoAction);
            entity
                .HasOne(e => e.DeletedByUser)
                .WithMany()
                .HasForeignKey(e => e.DeletedBy)
                .OnDelete(DeleteBehavior.NoAction);
            entity
                .HasOne(e => e.ModifiedByUser)
                .WithMany()
                .HasForeignKey(e => e.ModifiedBy)
                .OnDelete(DeleteBehavior.NoAction);
            entity.HasIndex(c => new
            {
                c.Name,
                c.IsDeleted,
                c.PhysicalFolderId,
            }); // 复合索引会优化以下场景的数据库查询效率

            entity.Ignore(e => e.OriginalPath);
            entity.Ignore(e => e.OriginalThumbnailPath);
        });
        #endregion

        #region DocumentDeleted
        modelBuilder.Entity<DocumentDeleted>(entity =>
        {
            entity.ToTable("R_DocumentDeleted");
            entity.HasKey(e => new { e.DocumentId, e.UserId }); //定义复合主键
            entity
                .HasOne(e => e.CreatedByUser)
                .WithMany()
                .HasForeignKey(e => e.CreatedBy)
                .OnDelete(DeleteBehavior.NoAction);
            entity
                .HasOne(e => e.ModifiedByUser)
                .WithMany()
                .HasForeignKey(e => e.ModifiedBy)
                .OnDelete(DeleteBehavior.NoAction);
            entity
                .HasOne(e => e.DeletedByUser)
                .WithMany()
                .HasForeignKey(e => e.DeletedBy)
                .OnDelete(DeleteBehavior.NoAction);
        });
        #endregion

        #region DocumentStarred
        modelBuilder.Entity<DocumentStarred>(entity =>
        {
            entity.ToTable("R_DocumentStarred");
            entity.HasKey(e => new { e.DocumentId, e.UserId });
            entity
                .HasOne(e => e.Document)
                .WithMany(e => e.DocumentStarreds)
                .HasForeignKey(e => e.DocumentId)
                .HasPrincipalKey(d => d.Id)
                .OnDelete(DeleteBehavior.NoAction);
            entity
                .HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .HasPrincipalKey(u => u.Id)
                .OnDelete(DeleteBehavior.NoAction);
        });
        #endregion

        #region DocumentUserPermission
        modelBuilder.Entity<DocumentUserPermission>(entity =>
        {
            entity.ToTable("R_DocumentUserPermission");
            entity.HasKey(e => new { e.DocumentId, e.UserId });
            entity.HasIndex(c => new { c.DocumentId, c.UserId });
            entity
                .HasOne(e => e.Document)
                .WithMany()
                .HasForeignKey(e => e.DocumentId)
                .OnDelete(DeleteBehavior.NoAction);
            entity
                .HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        });
        #endregion

        #region DocumentVersion
        modelBuilder.Entity<DocumentVersion>(entity =>
        {
            entity.ToTable("T_DocumentVersion");
            entity.HasKey(e => e.Id);
            entity
                .HasOne(e => e.Document)
                .WithMany()
                .HasForeignKey(e => e.DocumentId)
                .OnDelete(DeleteBehavior.NoAction);
            entity
                .HasOne(e => e.CreatedByUser)
                .WithMany()
                .HasForeignKey(e => e.CreatedBy)
                .OnDelete(DeleteBehavior.NoAction);
            entity
                .HasOne(e => e.ModifiedByUser)
                .WithMany()
                .HasForeignKey(e => e.ModifiedBy)
                .OnDelete(DeleteBehavior.NoAction);
            entity
                .HasOne(e => e.DeletedByUser)
                .WithMany()
                .HasForeignKey(e => e.DeletedBy)
                .OnDelete(DeleteBehavior.NoAction);
        });
        #endregion

        #region LoginAudit
        modelBuilder.Entity<LoginAudit>(entity =>
        {
            entity.ToTable("T_LoginAudit");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Provider).IsRequired(false);
            entity.Property(e => e.LoginTime).HasColumnType(PostgresqlStr.ColumnTimeName);
            entity.Property(e => e.RemoteIP).HasMaxLength(50);
            entity.Property(e => e.Latitude).HasMaxLength(50);
            entity.Property(e => e.Longitude).HasMaxLength(50);
        });
        #endregion

        #region PhysicalFolder
        modelBuilder.Entity<PhysicalFolder>(entity =>
        {
            entity.ToTable("T_PhysicalFolder");
            entity.HasKey(e => e.Id);
            entity
                .Property(e => e.SystemFolderName)
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore); // 添加时自动生成, 用于本地数据库自增标识，便于排序、索引优化等场景。

            entity
                .HasOne(e => e.Parent)
                .WithMany()
                .HasForeignKey(e => e.ParentId)
                .IsRequired(false) // 这意味着文件夹可以没有父文件夹（即根文件夹）
                .OnDelete(DeleteBehavior.Restrict); // 如果文件夹有子文件夹，则不允许删除该文件夹
            entity
                .HasMany(x => x.PhysicalFolderUsers)
                .WithOne(x => x.PhysicalFolder)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade); // 删除一个PhysicalFolder时，会自动删除所有关联的PhysicalFolderUser记录

            entity.HasAlternateKey(x => x.SystemFolderName).HasName("AK_PhysicalFolder"); //创建一个备用键（Alternate Key）

            entity.HasIndex(c => new
            {
                c.Name,
                c.IsDeleted,
                c.ParentId,
            });
        });
        #endregion

        #region PhysicalFolderUser
        modelBuilder.Entity<PhysicalFolderUser>(entity =>
        {
            entity.ToTable("R_PhysicalFolderUser");
            entity.HasKey(e => new { e.FolderId, e.UserId });
            entity
                .HasOne(e => e.PhysicalFolder)
                .WithMany(physicalFolder => physicalFolder.PhysicalFolderUsers)
                .HasForeignKey(e => e.FolderId)
                .HasPrincipalKey(physicalFolder => physicalFolder.Id)
                .OnDelete(DeleteBehavior.NoAction);
            entity
                .HasOne(e => e.User)
                .WithMany(user => user.Folders)
                .HasForeignKey(e => e.UserId)
                .HasPrincipalKey(user => user.Id)
                .OnDelete(DeleteBehavior.NoAction);
        });
        #endregion

        #region SharedDocumentUsers
        modelBuilder.Entity<SharedDocumentUser>(entity =>
        {
            entity.ToTable("R_SharedDocumentUser");
            entity.HasKey(e => new { e.DocumentId, e.UserId });
            entity
                .HasOne(e => e.Document)
                .WithMany(document => document.SharedDocumentUsers)
                .HasForeignKey(e => e.DocumentId)
                .HasPrincipalKey(s => s.Id)
                .OnDelete(DeleteBehavior.NoAction);
            entity
                .HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .HasPrincipalKey(u => u.Id)
                .OnDelete(DeleteBehavior.NoAction);
        });
        #endregion

        #region User

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("T_User");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.CreatedDate).HasColumnType(PostgresqlStr.ColumnTimeName);
            entity.Property(e => e.ModifiedDate).HasColumnType(PostgresqlStr.ColumnTimeName);
            entity.Property(e => e.DeletedDate).HasColumnType(PostgresqlStr.ColumnTimeName);
        });
        #endregion
    }
}
