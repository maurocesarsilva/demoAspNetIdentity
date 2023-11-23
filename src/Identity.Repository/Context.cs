using Identity.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity.Repository
{
	public class Context : IdentityDbContext<User>
	{
		public Context(DbContextOptions<Context> options) : base(options)
		{
		}


		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			builder.ApplyConfigurationsFromAssembly(typeof(Context).Assembly);


			//alteração das tabelas do aspNet
			//builder.Entity<User>().ToTable("User").Property(x => x.UserName).HasColumnName("Name").HasMaxLength(100);


			//builder.Ignore<IdentityRole<string>>();
			//builder.Ignore<IdentityUserClaim<string>>();
			//builder.Ignore<IdentityUserRole<string>>();
			//builder.Ignore<IdentityRoleClaim<string>>();
			//builder.Ignore<IdentityUserLogin<string>>();
			//builder.Ignore<IdentityUserToken<string>>();
		}

		public DbSet<User> User { get; set; }
	}
}
