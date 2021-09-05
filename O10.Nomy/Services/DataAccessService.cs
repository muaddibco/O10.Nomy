using Microsoft.EntityFrameworkCore;
using O10.Core.Architecture;
using O10.Nomy.Data;
using O10.Nomy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace O10.Nomy.Services
{
    [RegisterDefaultImplementation(typeof(IDataAccessService), Lifetime = LifetimeManagement.Scoped)]
    public partial class DataAccessService : IDataAccessService
    {
        private readonly ApplicationDbContext _dbContext;

        public DataAccessService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region Expertise Areas

        public async Task<ExpertiseArea> AddExpertiseArea(string name, string description)
        {
            var entry = await _dbContext.ExpertiseAreas.AddAsync(new ExpertiseArea
            {
                Name = name,
                Description = description
            });

            await _dbContext.SaveChangesAsync();

            return entry.Entity;
        }

        public async Task<ExpertiseSubArea> AddExpertiseSubArea(long expertiseAreaId, string name, string description)
        {
            var expertiseArea = await _dbContext.ExpertiseAreas.FirstOrDefaultAsync(s => s.ExpertiseAreaId == expertiseAreaId);

            if (expertiseArea == null)
            {
                throw new ArgumentOutOfRangeException(nameof(expertiseAreaId));
            }
            var entry = await _dbContext.ExpertiseSubAreas.AddAsync(new ExpertiseSubArea
            {
                Name = name,
                Description = description,
                ExpertiseArea = expertiseArea
            });

            await _dbContext.SaveChangesAsync();

            return entry.Entity;
        }

        public async Task<List<ExpertiseArea>> GetExpertiseAreas()
        {


            return await _dbContext.ExpertiseAreas.Include(s => s.ExpertiseSubAreas).ToListAsync();
        }

        public async Task<List<ExpertiseSubArea>> GetExpertiseSubAreas(long expertiseAreaId)
        {
            var expertiseArea = await _dbContext.ExpertiseAreas.Include(s => s.ExpertiseSubAreas).FirstOrDefaultAsync(s => s.ExpertiseAreaId == expertiseAreaId);

            if (expertiseArea == null)
            {
                throw new ArgumentOutOfRangeException(nameof(expertiseAreaId));
            }

            return expertiseArea.ExpertiseSubAreas.ToList();
        }

        public async Task<List<ExpertProfile>> GetExpertProfiles(params string[] expertiseSubAreaNames)
        {


            return
                await _dbContext
                .Experts
                .Include(s => s.NomyUser)
                .Include(s => s.ExpertiseSubAreas)
                .Where(s => s.ExpertiseSubAreas.Any(a => expertiseSubAreaNames.Contains(a.Name)))
                .ToListAsync();
        }

        public async Task<ExpertProfile> GetExpertProfile(long expertProfileId)
        {


            return
                await _dbContext
                .Experts
                .Include(s => s.NomyUser)
                .Include(s => s.ExpertiseSubAreas)
                .FirstOrDefaultAsync(s => s.ExpertProfileId == expertProfileId);
        }

        #endregion Expertise Areas

        #region Experts

        public async Task<ExpertProfile> AddExpertProfile(long userId, string description, ulong fee, params string[] expertiseSubAreas)
        {
            var user = await _dbContext.Users.Include(s => s.Account).FirstOrDefaultAsync(u => u.Account.NomyAccountId == userId);

            if (user == null)
            {
                throw new ArgumentOutOfRangeException(nameof(userId));
            }
            var entry = await _dbContext.Experts.AddAsync(new ExpertProfile
            {
                NomyUser = user,
                Description = description,
                Fee = fee,
                ExpertiseSubAreas = await _dbContext.ExpertiseSubAreas.Where(s => expertiseSubAreas.Contains(s.Name)).ToListAsync()
            });

            await _dbContext.SaveChangesAsync();

            return entry.Entity;
        }

        #endregion Experts

        #region Users

        public async Task<NomyUser?> UpdateO10Id(long userId, long o10Id, CancellationToken ct)
        {
            var user = await _dbContext.Users.Include(s => s.Account).FirstOrDefaultAsync(c => c.Account.NomyAccountId == userId, ct);

            if (user != null)
            {
                user.Account.O10Id = o10Id;

                await _dbContext.SaveChangesAsync(ct);
            }

            return user;
        }

        public async Task<NomyUser> CreateUser(long o10Id, string email, string firstName, string lastName, string walletId, string? beneficiaryId, string? senderId, CancellationToken ct)
        {
            var account = await GetAccountAsync(o10Id, ct); var res = await _dbContext.Users.AddAsync(new NomyUser
            {
                Account = account,
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                WalletId = walletId,
                BeneficiaryId = beneficiaryId,
                SenderId = senderId
            }, ct);

            await _dbContext.SaveChangesAsync(ct);

            return res.Entity;
        }

        private async Task<NomyAccount> GetAccountAsync(long o10Id, CancellationToken ct)
        {
            var account = await _dbContext.Accounts.FirstOrDefaultAsync(a => a.O10Id == o10Id, ct);

            if (account == null)
            {
                account = new NomyAccount
                {
                    O10Id = o10Id
                };

                _dbContext.Accounts.Add(account);
            }

            return account;
        }

        public async Task<NomyUser?> FindUser(string email, CancellationToken ct)
        {
            var res = await _dbContext.Users.Include(s => s.Account).FirstOrDefaultAsync(c => c.Email == email, ct);

            return res;
        }

        public async Task<NomyUser?> GetUser(long userId, CancellationToken ct)
        {
            var res = await _dbContext.Users.Include(s => s.Account).FirstOrDefaultAsync(c => c.Account.NomyAccountId == userId, ct);

            return res;
        }

        #endregion Users

        #region Nomy Service Provider

        public async Task<NomyServiceProvider> CreateServiceProvider(long o10Id, string name, CancellationToken ct)
        {
            var account = await GetAccountAsync(o10Id, ct); var res = await _dbContext.ServiceProviders.AddAsync(new NomyServiceProvider
            {
                Account = account,
                Name = name
            }, ct);

            await _dbContext.SaveChangesAsync(ct);

            return res.Entity;
        }

        public async Task<NomyServiceProvider> FindServiceProvider(string name, CancellationToken ct)
        {
            var res = await _dbContext.ServiceProviders.Include(s => s.Account).FirstOrDefaultAsync(c => c.Name == name, ct);

            return res;
        }

        public async Task<NomyServiceProvider?> GetServiceProvider(long accountId, CancellationToken ct)
        {
            var res = await _dbContext.ServiceProviders.Include(s => s.Account).FirstOrDefaultAsync(c => c.Account.NomyAccountId == accountId, ct);

            return res;
        }

        #endregion Nomy Service Provider

        #region Invoice Records

        public async Task<InvoiceRecord> AddInvoiceRecord(long userId, string commitment, string rangeProof, CancellationToken ct)
        {
            var user = await _dbContext.Users.Include(s => s.Account).FirstOrDefaultAsync(u => u.Account.NomyAccountId == userId, ct);

            if (user == null)
            {
                throw new ArgumentOutOfRangeException(nameof(userId));
            }
            var entry = _dbContext.InvoiceRecords.Add(
   new InvoiceRecord
   {
       User = user,
       Commitment = commitment,
       RangeProof = rangeProof
   });

            await _dbContext.SaveChangesAsync(ct);

            return entry.Entity;

        }

        public async Task<IEnumerable<InvoiceRecord>> GetInvoiceRecords(long userId, bool notProcessed, CancellationToken ct)
        {
            var user = await _dbContext.Users.Include(s => s.Account).FirstOrDefaultAsync(u => u.Account.NomyAccountId == userId, ct);

            if (user == null)
            {
                throw new ArgumentOutOfRangeException(nameof(userId));
            }

            return _dbContext.InvoiceRecords.Include(r => r.User).Where(r => r.User.NomyUserId == userId && (!notProcessed || r.DateOfProcessing == null));
        }

        public async Task<IEnumerable<InvoiceRecord>> GetInvoiceRecords(bool notProcessed, CancellationToken ct)
        {


            return _dbContext.InvoiceRecords.Include(r => r.User).Where(r => !notProcessed || r.DateOfProcessing == null);
        }

        public async Task<IEnumerable<InvoiceRecord>> MarkInvoiceRecordsProcessed(long userId, IEnumerable<long> invoiceRecordIds, CancellationToken ct)
        {
            var user = await _dbContext.Users.Include(s => s.Account).FirstOrDefaultAsync(u => u.Account.NomyAccountId == userId, ct);

            if (user == null)
            {
                throw new ArgumentOutOfRangeException(nameof(userId));
            }
            var records = _dbContext.InvoiceRecords.Include(r => r.User)
   .Where(r => r.User.NomyUserId == userId && invoiceRecordIds.Contains(r.InvoiceRecordId));

            await records.ForEachAsync(r => r.DateOfProcessing = DateTime.Now, ct);

            await _dbContext.SaveChangesAsync(ct);

            return records;
        }

        #endregion Invoice Records

        #region Payment Records

        public async Task<PaymentRecord> AddPaymentRecord(long userId, string commitment, string rangeProof, string signature, string invoiceCommitment, CancellationToken ct)
        {
            var user = await _dbContext.Users.Include(s => s.Account).FirstOrDefaultAsync(u => u.Account.NomyAccountId == userId, ct);

            if (user == null)
            {
                throw new ArgumentOutOfRangeException(nameof(userId));
            }
            var invoiceRecord = await _dbContext.InvoiceRecords.FirstOrDefaultAsync(r => r.Commitment == commitment, ct);
            if (invoiceRecord == null)
            {
                throw new ArgumentOutOfRangeException(nameof(invoiceCommitment));
            }
            var entry = _dbContext.PaymentRecords.Add(
   new PaymentRecord
   {
       User = user,
       Commitment = commitment,
       RangeProof = rangeProof,
       Signature = signature,
       Invoice = invoiceRecord
   });

            await _dbContext.SaveChangesAsync(ct);

            return entry.Entity;
        }

        public async Task<IEnumerable<PaymentRecord>> GetPaymentRecords(long userId, bool notProcessed, CancellationToken ct)
        {
            var user = await _dbContext.Users.Include(s => s.Account).FirstOrDefaultAsync(u => u.Account.NomyAccountId == userId, ct);

            if (user == null)
            {
                throw new ArgumentOutOfRangeException(nameof(userId));
            }

            return _dbContext.PaymentRecords.Include(r => r.User).Where(r => r.User.NomyUserId == userId && (!notProcessed || r.DateOfProcessing == null));
        }

        public async Task<IEnumerable<PaymentRecord>> GetPaymentRecords(bool notProcessed, CancellationToken ct)
        {


            return _dbContext.PaymentRecords.Include(r => r.User).Where(r => !notProcessed || r.DateOfProcessing == null);
        }

        public async Task<IEnumerable<PaymentRecord>> MarkPaymentRecordsProcessed(long userId, IEnumerable<long> paymentRecordIds, CancellationToken ct)
        {
            var user = await _dbContext.Users.Include(s => s.Account).FirstOrDefaultAsync(u => u.Account.NomyAccountId == userId, ct);

            if (user == null)
            {
                throw new ArgumentOutOfRangeException(nameof(userId));
            }
            var records = _dbContext.PaymentRecords.Include(r => r.User)
   .Where(r => r.User.NomyUserId == userId && paymentRecordIds.Contains(r.PaymentRecordId));

            await records.ForEachAsync(r => r.DateOfProcessing = DateTime.Now, ct);

            await _dbContext.SaveChangesAsync(ct);

            return records;
        }

        #endregion Payment Records

        #region Secret Payment Records

        public async Task<SecretPaymentRecord> AddSecretPaymentRecord(long userId, long paymentRecordId, string blindingFactor, ulong amount, CancellationToken ct)
        {
            var user = await _dbContext.Users.Include(s => s.Account).FirstOrDefaultAsync(u => u.Account.NomyAccountId == userId, ct);

            if (user == null)
            {
                throw new ArgumentOutOfRangeException(nameof(userId));
            }
            var paymentRecord = await _dbContext.PaymentRecords.FirstOrDefaultAsync(r => r.PaymentRecordId == paymentRecordId);

            if (paymentRecord == null)
            {
                throw new ArgumentOutOfRangeException(nameof(paymentRecordId));
            }
            var entry = _dbContext.SecretPaymentRecords.Add(
   new SecretPaymentRecord
   {
       User = user,
       PaymentRecord = paymentRecord,
       BlindingFactor = blindingFactor,
       Amount = amount
   });

            await _dbContext.SaveChangesAsync(ct);

            return entry.Entity;
        }

        public IEnumerable<SecretPaymentRecord> GetSecretPayments(IEnumerable<long> paymentRecordIds)
        {


            return _dbContext.SecretPaymentRecords.Include(r => r.PaymentRecord)
                                                 .Include(r => r.User)
                                                 .Where(r => paymentRecordIds.Contains(r.PaymentRecord.PaymentRecordId));
        }

        #endregion Secret Payment Records

        #region Secret Invoice Records

        public async Task<SecretInvoiceRecord> AddSecretInvoiceRecord(long userId, long invoiceRecordId, string blindingFactor, ulong amount, CancellationToken ct)
        {
            var user = await _dbContext.Users.Include(s => s.Account).FirstOrDefaultAsync(u => u.Account.NomyAccountId == userId, ct);

            if (user == null)
            {
                throw new ArgumentOutOfRangeException(nameof(userId));
            }
            var invoiceRecord = await _dbContext.InvoiceRecords.FirstOrDefaultAsync(r => r.InvoiceRecordId == invoiceRecordId);

            if (invoiceRecord == null)
            {
                throw new ArgumentOutOfRangeException(nameof(invoiceRecordId));
            }
            var entry = _dbContext.SecretInvoiceRecords.Add(
   new SecretInvoiceRecord
   {
       User = user,
       InvoiceRecord = invoiceRecord,
       BlindingFactor = blindingFactor,
       Amount = amount
   });

            await _dbContext.SaveChangesAsync(ct);

            return entry.Entity;
        }

        public IEnumerable<SecretInvoiceRecord> GetSecretInvoices(IEnumerable<long> invoiceRecordIds)
        {


            return _dbContext.SecretInvoiceRecords.Include(r => r.InvoiceRecord)
                                                 .Include(r => r.User)
                                                 .Where(r => invoiceRecordIds.Contains(r.InvoiceRecord.InvoiceRecordId));
        }

        #endregion Secret Invoice Records

        #region Payouts

        public IEnumerable<PayoutRecord> GetPayouts()
        {


            return _dbContext.PayoutRecords;
        }

        #endregion Payouts

        #region System Parameters

        public async Task<SystemParameter> SetSystemParameter(string name, string value, CancellationToken ct)
        {
            var param = await _dbContext.SystemParameters.FirstOrDefaultAsync(s => s.Name == name, ct);
            if (param == null)
            {
                var entry = await _dbContext.SystemParameters.AddAsync(
                    new SystemParameter
                    {
                        Name = name,
                        Value = value
                    }, ct);

                await _dbContext.SaveChangesAsync(ct);

                return entry.Entity;
            }
            else
            {
                param.Value = value;
                await _dbContext.SaveChangesAsync(ct);
                return param;
            }
        }

        public async Task<SystemParameter> GetSystemParameter(string name, CancellationToken ct)
        {


            return await _dbContext.SystemParameters.FirstOrDefaultAsync(s => s.Name == name, ct);
        }

        #endregion System Parameters

        #region Joint Service

        public async Task<JointGroup?> AddJointGroup(long o10RegistrationId, long o10GroupId, string name, string description, CancellationToken ct)
        {
            var res = _dbContext.JointGroups.Add(
                new JointGroup
                {
                    O10RegistrationId = o10RegistrationId,
                    O10GroupId = o10GroupId,
                    Name = name,
                    Description = description
                });

            await _dbContext.SaveChangesAsync(ct);

            return res.Entity;
        }

        public async Task<IEnumerable<JointGroup>> GetJointGroups(long o10RegistrationId, CancellationToken ct)
        {


            return await _dbContext.JointGroups.Where(s => s.O10RegistrationId == o10RegistrationId).ToListAsync(ct);
        }

        public async Task<JointGroup> GetJointGroup(long groupId, CancellationToken ct)
        {
            return await _dbContext.JointGroups.Include(s => s.Registration).FirstOrDefaultAsync(s => s.JointGroupId == groupId, ct);
        }

        public async Task<JointGroupMember?> AddJointGroupMember(long groupId, string email, string description, CancellationToken ct)
        {
            var group = await _dbContext.JointGroups.FirstOrDefaultAsync(s => s.JointGroupId == groupId, ct);

            if (group == null)
            {
                throw new ArgumentOutOfRangeException(nameof(groupId));
            }
            var res = _dbContext.JointGroupMembers.Add(
   new JointGroupMember
   {
       Group = group,
       Email = email,
       Description = description,
       IsRegistered = false
   });

            await _dbContext.SaveChangesAsync(ct);

            return res.Entity;
        }

        public async Task<IEnumerable<JointGroupMember>> GetJointGroupMembers(long groupId, CancellationToken ct)
        {
            var group = await _dbContext.JointGroups.FirstOrDefaultAsync(s => s.JointGroupId == groupId, ct);

            if (group == null)
            {
                throw new ArgumentOutOfRangeException(nameof(groupId));
            }

            return await _dbContext.JointGroupMembers.Include(s => s.Group).Where(s => s.Group.JointGroupId == groupId).ToListAsync(ct);
        }

        public async Task<JointServiceRegistration> GetOrAddJointServiceRegistration(long o10RegistrationId, string registrationCommitment, CancellationToken ct)
        {
            var registration = await _dbContext.JointServiceRegistrations.FirstOrDefaultAsync(s => s.O10RegistrationId == o10RegistrationId, ct);

            if(registration == null)
            {
                registration = new JointServiceRegistration
                {
                    O10RegistrationId = o10RegistrationId,
                    RegistrationCommitment = registrationCommitment
                };

                var entry = _dbContext.JointServiceRegistrations.Add(registration);

                await _dbContext.SaveChangesAsync(ct);

                registration = entry.Entity;
            }

            return registration;
        }

        #endregion Joint Service
    }
}
