using DanMu.Pan.Data.Dto.LoginAuditDto;
using DanMu.Pan.Data.Resources;
using DanMu.Pan.Repository.GenericRepository;

namespace DanMu.Pan.Repository.LoginAudit;

public interface ILoginAuditRepository : IGenericRepository<Data.Entities.LoginAudit>
{
    Task<LoginAuditList> GetDocumentAuditTrails(LoginAuditResource loginAuditResrouce);
    Task LoginAudit(LoginAuditDto loginAudit);
}
