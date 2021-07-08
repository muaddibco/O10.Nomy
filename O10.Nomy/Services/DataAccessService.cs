using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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
    [RegisterDefaultImplementation(typeof(IDataAccessService), Lifetime = LifetimeManagement.Singleton)]
    public class DataAccessService : IDataAccessService
    {
        private readonly IServiceProvider _serviceProvider;

        public DataAccessService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        #region Expertise Areas

        public async Task<ExpertiseArea> AddExpertiseArea(string name, string description)
        {
            using var dbContext = _serviceProvider.CreateScope().ServiceProvider.GetService<ApplicationDbContext>();
            var entry = await dbContext.ExpertiseAreas.AddAsync(new ExpertiseArea
            {
                Name = name,
                Description = description
            });

            await dbContext.SaveChangesAsync();

            return entry.Entity;
        }

        public async Task<ExpertiseSubArea> AddExpertiseSubArea(long expertiseAreaId, string name, string description)
        {
            using var dbContext = _serviceProvider.CreateScope().ServiceProvider.GetService<ApplicationDbContext>();

            var expertiseArea = await dbContext.ExpertiseAreas.FirstOrDefaultAsync(s => s.ExpertiseAreaId == expertiseAreaId);

            if(expertiseArea == null)
            {
                throw new ArgumentOutOfRangeException(nameof(expertiseAreaId));
            }

            var entry = await dbContext.ExpertiseSubAreas.AddAsync(new ExpertiseSubArea
            {
                Name = name,
                Description = description,
                ExpertiseArea = expertiseArea
            });

            await dbContext.SaveChangesAsync();

            return entry.Entity;
        }

        public async Task<List<ExpertiseArea>> GetExpertiseAreas()
        {
            using var dbContext = _serviceProvider.CreateScope().ServiceProvider.GetService<ApplicationDbContext>();

            return await dbContext.ExpertiseAreas.Include(s => s.ExpertiseSubAreas).ToListAsync();
        }

        public async Task<List<ExpertiseSubArea>> GetExpertiseSubAreas(long expertiseAreaId)
        {
            using var dbContext = _serviceProvider.CreateScope().ServiceProvider.GetService<ApplicationDbContext>();

            var expertiseArea = await dbContext.ExpertiseAreas.Include(s => s.ExpertiseSubAreas).FirstOrDefaultAsync(s => s.ExpertiseAreaId == expertiseAreaId);

            if (expertiseArea == null)
            {
                throw new ArgumentOutOfRangeException(nameof(expertiseAreaId));
            }

            return expertiseArea.ExpertiseSubAreas.ToList();
        }

        public async Task<List<ExpertProfile>> GetExpertProfiles(params string[] expertiseSubAreaNames)
        {
            using var dbContext = _serviceProvider.CreateScope().ServiceProvider.GetService<ApplicationDbContext>();

            return 
                await dbContext
                .Experts
                .Include(s => s.NomyUser)
                .Include(s => s.ExpertiseSubAreas)
                .Where(s => s.ExpertiseSubAreas.Any(a => expertiseSubAreaNames.Contains(a.Name)))
                .ToListAsync();
        }

        public async Task<ExpertProfile> GetExpertProfile(long expertProfileId)
        {
            using var dbContext = _serviceProvider.CreateScope().ServiceProvider.GetService<ApplicationDbContext>();

            return
                await dbContext
                .Experts
                .Include(s => s.NomyUser)
                .Include(s => s.ExpertiseSubAreas)
                .FirstOrDefaultAsync(s => s.ExpertProfileId == expertProfileId);
        }

        #endregion Expertise Areas

        #region Experts

        public async Task<ExpertProfile> AddExpertProfile(long userId, string description, ulong fee, params string[] expertiseSubAreas)
        {
            using var dbContext = _serviceProvider.CreateScope().ServiceProvider.GetService<ApplicationDbContext>();

            var user = await dbContext.ConsumerUsers.FirstOrDefaultAsync(u => u.NomyUserId == userId);

            if(user == null)
            {
                throw new ArgumentOutOfRangeException(nameof(userId));
            }

            var entry = await dbContext.Experts.AddAsync(new ExpertProfile
            {
                NomyUser = user,
                Description = description,
                Fee = fee,
                ExpertiseSubAreas = await dbContext.ExpertiseSubAreas.Where(s => expertiseSubAreas.Contains(s.Name)).ToListAsync()
            });

            await dbContext.SaveChangesAsync();

            return entry.Entity;
        }

        #endregion Experts

        #region Users

        public async Task<NomyUser> CreateUser(long o10Id, string email, string firstName, string lastName, string walletId, CancellationToken ct)
        {
            using var dbContext = _serviceProvider.CreateScope().ServiceProvider.GetService<ApplicationDbContext>();

            var res = await dbContext.ConsumerUsers.AddAsync(new NomyUser
            {
                O10Id = o10Id,
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                WalletId = walletId,
            }, ct);

            await dbContext.SaveChangesAsync(ct);

            return res.Entity;
        }

        public async Task<NomyUser?> FindUser(string email, CancellationToken ct)
        {
            using var dbContext = _serviceProvider.CreateScope().ServiceProvider.GetService<ApplicationDbContext>();

            var res = await dbContext.ConsumerUsers.FirstOrDefaultAsync(c => c.Email == email, ct);

            return res;
        }

        public async Task<NomyUser?> GetUser(long userId, CancellationToken ct)
        {
            using var dbContext = _serviceProvider.CreateScope().ServiceProvider.GetService<ApplicationDbContext>();

            var res = await dbContext.ConsumerUsers.FirstOrDefaultAsync(c => c.NomyUserId == userId, ct);

            return res;
        }

        #endregion Users

    }
}
