using CollegeOfSystem.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CollegeOfSystem.ConfigurationEntites
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Students").HasKey(t => t.Id);

            builder.Property(x => x.Name).HasColumnType("varchar").HasMaxLength(250).IsRequired();
            builder.Property(x => x.Email).HasColumnType("varchar").HasMaxLength(250).IsRequired();
            builder.Property(x => x.Phone).HasColumnType("varchar").HasMaxLength(250).IsRequired();
            builder.Property(x => x.Form).HasColumnType("varbinary(max)").IsRequired();
            builder.Property(x => x.BirthDay).HasColumnType("date").HasMaxLength(250).IsRequired();

            builder.HasOne(x=>x.department)
                   .WithMany(x=>x.Students)
                   .HasForeignKey(x=>x.departmentId).IsRequired();
        }



    }
}
