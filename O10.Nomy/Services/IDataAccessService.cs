using O10.Core.Architecture;
using O10.Nomy.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace O10.Nomy.Services
{
    [ServiceContract]
    public interface IDataAccessService
    {
        #region Users

        Task<NomyUser> CreateUser(long o10Id, string email, string firstName, string lastName, string walletId, CancellationToken ct);
        Task<NomyUser?> FindUser(string email, CancellationToken ct);
        Task<NomyUser?> GetUser(long userId, CancellationToken ct);

        #endregion Users

        #region Expertise Areas

        Task<ExpertiseArea> AddExpertiseArea(string name, string description);
        Task<List<ExpertiseArea>> GetExpertiseAreas();

        Task<ExpertiseSubArea> AddExpertiseSubArea(long expertiseArea, string name, string description);
        
        Task<List<ExpertiseSubArea>> GetExpertiseSubAreas(long expertiseArea);

        Task<List<ExpertProfile>> GetExpertProfiles(params string[] expertiseSubAreaNames);

        Task<ExpertProfile> GetExpertProfile(long expertProfileId);

        #endregion Expertise Areas

        #region Experts

        Task<ExpertProfile> AddExpertProfile(long userId, string description, ulong fee, params string[] expertiseSubAreas);

        #endregion Experts

        #region Invoice Records

        Task<InvoiceRecord> AddInvoiceRecord(long userId, string commitment, string rangeProof, CancellationToken ct);

        Task<IEnumerable<InvoiceRecord>> GetInvoiceRecords(long userId, bool notProcessed, CancellationToken ct);
        Task<IEnumerable<InvoiceRecord>> GetInvoiceRecords(bool notProcessed, CancellationToken ct);

        Task<IEnumerable<InvoiceRecord>> MarkInvoiceRecordsProcessed(long userId, IEnumerable<long> invoiceRecordIds, CancellationToken ct);

        #endregion Invoice Records

        #region Payment Records

        Task<PaymentRecord> AddPaymentRecord(long userId, string commitment, string rangeProof, string signature, string invoiceCommitment, CancellationToken ct );

        Task<IEnumerable<PaymentRecord>> GetPaymentRecords(long userId, bool notProcessed, CancellationToken ct);
        Task<IEnumerable<PaymentRecord>> GetPaymentRecords(bool notProcessed, CancellationToken ct);

        Task<IEnumerable<PaymentRecord>> MarkPaymentRecordsProcessed(long userId, IEnumerable<long> paymentRecordIds, CancellationToken ct);

        #endregion Payment Records

        #region Secret Payment Records

        Task<SecretPaymentRecord> AddSecretPaymentRecord(long userId, long paymentRecordId, string blindingFactor, ulong amount, CancellationToken ct);

        IEnumerable<SecretPaymentRecord> GetSecretPayments(IEnumerable<long> paymentRecordIds);

        #endregion Secret Payment Records

        #region Secret Invoice Records

        Task<SecretInvoiceRecord> AddSecretInvoiceRecord(long userId, long invoiceRecordId, string blindingFactor, ulong amount, CancellationToken ct);
        
        IEnumerable<SecretInvoiceRecord> GetSecretInvoices(IEnumerable<long> invoiceRecordIds);

        #endregion Secret Invoice Records

        #region Payouts

        IEnumerable<PayoutRecord> GetPayouts();

        #endregion Payouts
    }
}
