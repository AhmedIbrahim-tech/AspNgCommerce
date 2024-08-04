namespace ECommerce.Infrastrucure.Data.Configurations
{
    public class CategoryConfigration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(50);
            builder.Property(x => x.Description).HasMaxLength(50);

            //Has Data 
            builder.HasData(
             new Category { Id = 1, Name = "category 1", Description = "description 1" },
             new Category { Id = 2, Name = "category 2", Description = "description 2" },
             new Category { Id = 3, Name = "category 3", Description = "description 3" }
            );
        }
    }
}
