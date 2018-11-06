using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Academy.Core.AcademyConst;
using Academy.Core.Base;
using Academy.Core.ComplexTypes;
using Academy.Core.Courses;
using Academy.Core.DropLists;
using Academy.Core.DynamicFilters;
using Academy.Core.Enrollments;
using Academy.Core.Instructors;
using Academy.Core.Students;
using Academy.Core.Users;
using EntityFramework.DynamicFilters;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WebGrease.Css.Extensions;

namespace Academy.Web.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser<int, UserLogin, UserRole, UserClaim>
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser, int> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, Role, int,
        UserLogin, UserRole, UserClaim>
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseLocation> CourseLocations { get; set; }
        public DbSet<CourseLabs> CourseLabs { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Nationality> Nationalities { get; set; }
        public DbSet<Collage> Collages { get; set; }
        public DbSet<CourseNames> CourseNames { get; set; }
        public DbSet<Qualifiation> Qualifiations { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Area> Areas { get; set; }
        public ApplicationDbContext()
            : base("AcademyConnection")
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public void SetFilter(string name, object value)
        {
            this.SetFilterScopedParameterValue(name, value);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Filter(AcademyConst.IsDeleted,(ISoftDelete d)=>d.IsDeleted,false);
            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            AddTimestamps();
            return base.SaveChangesAsync(cancellationToken);
        }
        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            entities.ForEach(entity =>
            {
                if (entity.State == EntityState.Added)
                {
                    ((BaseEntity)entity.Entity).CreationTime = DateTime.Now;
                    ((BaseEntity)entity.Entity).CreatorUserId = HttpContext.Current.User.Identity.GetUserId<int>();
                }

                ((BaseEntity)entity.Entity).ModificationTime = DateTime.Now;

                ((BaseEntity)entity.Entity).ModifiedUserId = HttpContext.Current.User.Identity.GetUserId<int>();

            });

        }
    }
}