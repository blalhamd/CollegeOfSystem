using CollegeOfSystem.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CollegeOfSystem.ConfigurationEntites
{
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.ToTable("Doctors").HasKey(t => t.Id);

            builder.Property(x => x.Name).HasColumnType("varchar").HasMaxLength(250).IsRequired();
            builder.Property(x => x.Email).HasColumnType("varchar").HasMaxLength(250).IsRequired();
            builder.Property(x => x.Phone).HasColumnType("varchar").HasMaxLength(250).IsRequired();
            builder.Property(x => x.DateOfBirth).HasColumnType("date").HasMaxLength(250).IsRequired();

            builder.HasOne(x => x.department)
                   .WithMany(x => x.doctors)
                   .HasForeignKey(x => x.departmentId).IsRequired();
        }









    }
}
