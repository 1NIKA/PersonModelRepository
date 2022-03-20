using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class RepositoryContext : DbContext
{
    public RepositoryContext(DbContextOptions options) : base(options) { }

    public DbSet<Person>? Persons { get; set; }
    public DbSet<PhoneNumber>? PhoneNumbers { get; set; }
    public DbSet<RelationPerson>? RelationPersons { get; set; }
    public DbSet<Image>? Images { get; set; }
}