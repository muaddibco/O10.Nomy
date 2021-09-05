using Microsoft.EntityFrameworkCore;
using O10.Nomy.DemoFeatures.Models;

namespace O10.Nomy.Data
{
    public partial class ApplicationDbContext
    {
        public DbSet<DemoValidation> DemoValidations { get; set; }
    }
}
