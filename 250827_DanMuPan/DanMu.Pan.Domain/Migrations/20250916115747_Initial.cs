using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DanMu.Pan.Domain.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_LoginAudit",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: false),
                    LoginTime = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    RemoteIP = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    Provider = table.Column<string>(type: "text", nullable: true),
                    Latitude = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Longitude = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_LoginAudit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_PhysicalFolder",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    SystemFolderName = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ParentId = table.Column<Guid>(type: "uuid", nullable: true),
                    Size = table.Column<string>(type: "text", nullable: false),
                    PhysicalFolderId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamptz", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_PhysicalFolder", x => x.Id);
                    table.UniqueConstraint("AK_PhysicalFolder", x => x.SystemFolderName);
                    table.ForeignKey(
                        name: "FK_T_PhysicalFolder_T_PhysicalFolder_ParentId",
                        column: x => x.ParentId,
                        principalTable: "T_PhysicalFolder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_T_PhysicalFolder_T_PhysicalFolder_PhysicalFolderId",
                        column: x => x.PhysicalFolderId,
                        principalTable: "T_PhysicalFolder",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "T_User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    ProfilePhoto = table.Column<string>(type: "text", nullable: false),
                    Provider = table.Column<string>(type: "text", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamptz", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    IsAdmin = table.Column<bool>(type: "boolean", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_T_User_UserId",
                        column: x => x.UserId,
                        principalTable: "T_User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_T_User_UserId",
                        column: x => x.UserId,
                        principalTable: "T_User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_T_User_UserId",
                        column: x => x.UserId,
                        principalTable: "T_User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_T_User_UserId",
                        column: x => x.UserId,
                        principalTable: "T_User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "R_PhysicalFolderUser",
                columns: table => new
                {
                    FolderId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_R_PhysicalFolderUser", x => new { x.FolderId, x.UserId });
                    table.ForeignKey(
                        name: "FK_R_PhysicalFolderUser_T_PhysicalFolder_FolderId",
                        column: x => x.FolderId,
                        principalTable: "T_PhysicalFolder",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_R_PhysicalFolderUser_T_User_UserId",
                        column: x => x.UserId,
                        principalTable: "T_User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "T_Document",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Md5 = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    PhysicalFolderId = table.Column<Guid>(type: "uuid", nullable: false),
                    Extension = table.Column<string>(type: "text", nullable: false),
                    Path = table.Column<string>(type: "text", nullable: false),
                    Size = table.Column<long>(type: "bigint", nullable: false),
                    ThumbnailPath = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamptz", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Document", x => x.Id);
                    table.ForeignKey(
                        name: "FK_T_Document_T_PhysicalFolder_PhysicalFolderId",
                        column: x => x.PhysicalFolderId,
                        principalTable: "T_PhysicalFolder",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_T_Document_T_User_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "T_User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_T_Document_T_User_DeletedBy",
                        column: x => x.DeletedBy,
                        principalTable: "T_User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_T_Document_T_User_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalTable: "T_User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "R_DocumentDeleted",
                columns: table => new
                {
                    DocumentId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamptz", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_R_DocumentDeleted", x => new { x.DocumentId, x.UserId });
                    table.ForeignKey(
                        name: "FK_R_DocumentDeleted_T_Document_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "T_Document",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_R_DocumentDeleted_T_User_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "T_User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_R_DocumentDeleted_T_User_DeletedBy",
                        column: x => x.DeletedBy,
                        principalTable: "T_User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_R_DocumentDeleted_T_User_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalTable: "T_User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_R_DocumentDeleted_T_User_UserId",
                        column: x => x.UserId,
                        principalTable: "T_User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "R_DocumentStarred",
                columns: table => new
                {
                    DocumentId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_R_DocumentStarred", x => new { x.DocumentId, x.UserId });
                    table.ForeignKey(
                        name: "FK_R_DocumentStarred_T_Document_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "T_Document",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_R_DocumentStarred_T_User_UserId",
                        column: x => x.UserId,
                        principalTable: "T_User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "R_DocumentUserPermission",
                columns: table => new
                {
                    DocumentId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamptz", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_R_DocumentUserPermission", x => new { x.DocumentId, x.UserId });
                    table.ForeignKey(
                        name: "FK_R_DocumentUserPermission_T_Document_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "T_Document",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_R_DocumentUserPermission_T_User_UserId",
                        column: x => x.UserId,
                        principalTable: "T_User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "R_SharedDocumentUser",
                columns: table => new
                {
                    DocumentId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_R_SharedDocumentUser", x => new { x.DocumentId, x.UserId });
                    table.ForeignKey(
                        name: "FK_R_SharedDocumentUser_T_Document_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "T_Document",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_R_SharedDocumentUser_T_User_UserId",
                        column: x => x.UserId,
                        principalTable: "T_User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "T_DocumentVersion",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentId = table.Column<Guid>(type: "uuid", nullable: false),
                    Path = table.Column<string>(type: "text", nullable: false),
                    Message = table.Column<string>(type: "text", nullable: false),
                    Size = table.Column<long>(type: "bigint", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamptz", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_DocumentVersion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_T_DocumentVersion_T_Document_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "T_Document",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_T_DocumentVersion_T_User_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "T_User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_T_DocumentVersion_T_User_DeletedBy",
                        column: x => x.DeletedBy,
                        principalTable: "T_User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_T_DocumentVersion_T_User_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalTable: "T_User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_R_DocumentDeleted_CreatedBy",
                table: "R_DocumentDeleted",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_R_DocumentDeleted_DeletedBy",
                table: "R_DocumentDeleted",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_R_DocumentDeleted_ModifiedBy",
                table: "R_DocumentDeleted",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_R_DocumentDeleted_UserId",
                table: "R_DocumentDeleted",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_R_DocumentStarred_UserId",
                table: "R_DocumentStarred",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_R_DocumentUserPermission_DocumentId_UserId",
                table: "R_DocumentUserPermission",
                columns: new[] { "DocumentId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_R_DocumentUserPermission_UserId",
                table: "R_DocumentUserPermission",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_R_PhysicalFolderUser_UserId",
                table: "R_PhysicalFolderUser",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_R_SharedDocumentUser_UserId",
                table: "R_SharedDocumentUser",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_T_Document_CreatedBy",
                table: "T_Document",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_T_Document_DeletedBy",
                table: "T_Document",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_T_Document_ModifiedBy",
                table: "T_Document",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_T_Document_Name_IsDeleted_PhysicalFolderId",
                table: "T_Document",
                columns: new[] { "Name", "IsDeleted", "PhysicalFolderId" });

            migrationBuilder.CreateIndex(
                name: "IX_T_Document_PhysicalFolderId",
                table: "T_Document",
                column: "PhysicalFolderId");

            migrationBuilder.CreateIndex(
                name: "IX_T_DocumentVersion_CreatedBy",
                table: "T_DocumentVersion",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_T_DocumentVersion_DeletedBy",
                table: "T_DocumentVersion",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_T_DocumentVersion_DocumentId",
                table: "T_DocumentVersion",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_T_DocumentVersion_ModifiedBy",
                table: "T_DocumentVersion",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_T_PhysicalFolder_Name_IsDeleted_ParentId",
                table: "T_PhysicalFolder",
                columns: new[] { "Name", "IsDeleted", "ParentId" });

            migrationBuilder.CreateIndex(
                name: "IX_T_PhysicalFolder_ParentId",
                table: "T_PhysicalFolder",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_T_PhysicalFolder_PhysicalFolderId",
                table: "T_PhysicalFolder",
                column: "PhysicalFolderId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "T_User",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "T_User",
                column: "NormalizedUserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "R_DocumentDeleted");

            migrationBuilder.DropTable(
                name: "R_DocumentStarred");

            migrationBuilder.DropTable(
                name: "R_DocumentUserPermission");

            migrationBuilder.DropTable(
                name: "R_PhysicalFolderUser");

            migrationBuilder.DropTable(
                name: "R_SharedDocumentUser");

            migrationBuilder.DropTable(
                name: "T_DocumentVersion");

            migrationBuilder.DropTable(
                name: "T_LoginAudit");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "T_Document");

            migrationBuilder.DropTable(
                name: "T_PhysicalFolder");

            migrationBuilder.DropTable(
                name: "T_User");
        }
    }
}
