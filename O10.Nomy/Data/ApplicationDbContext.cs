using Microsoft.EntityFrameworkCore;
using O10.Nomy.Models;

namespace O10.Nomy.Data
{
    public class ApplicationDbContext : DbContext //: ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        /*public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }*/

        public DbSet<NomyAccount> Accounts { get; set; }
        public DbSet<ExpertiseArea> ExpertiseAreas { get; set; }
        public DbSet<ExpertiseSubArea> ExpertiseSubAreas { get; set; }
        public DbSet<ExpertProfile> Experts { get; set; }
        public DbSet<NomyUser> Users { get; set; }
        public DbSet<InvoiceRecord> InvoiceRecords { get; set; }
        public DbSet<PaymentRecord> PaymentRecords { get; set; }
        public DbSet<SecretInvoiceRecord> SecretInvoiceRecords { get; set; }
        public DbSet<SecretPaymentRecord> SecretPaymentRecords { get; set; }
        public DbSet<PayoutRecord> PayoutRecords { get; set; }
        public DbSet<SystemParameter> SystemParameters { get; set; }
        public DbSet<NomyServiceProvider> ServiceProviders { get; set; }

        public DbSet<JointGroup> JointGroups { get; set; }
        public DbSet<JointGroupMember> JointGroupMembers { get; set; }

        public DbSet<JointServiceRegistration> JointServiceRegistrations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<NomyAccount>().HasIndex(s => s.O10Id);
            modelBuilder.Entity<JointGroup>().HasIndex(s => s.O10RegistrationId);
            modelBuilder.Entity<JointServiceRegistration>().HasIndex(s => s.O10RegistrationId);
        }
    }
}
