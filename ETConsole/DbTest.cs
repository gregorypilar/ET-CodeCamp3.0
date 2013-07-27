using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace ETConsole
{
    public class DbTest : DbContext
    {
        public DbTest()
        {

        }
        public DbTest(string databaseName)
            : base(databaseName)
        {

        }
        public DbSet<Person> Person { get; set; }
        public DbSet<Test> Test { get; set; }

        public DbSet<Destination> Destinations { get; set; }
        public DbSet<Lodging> Lodgings { get; set; }
        public DbSet<Trip> Trips { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new DestinationConfiguration());
            modelBuilder.Configurations.Add(new LodgingConfiguration());
            modelBuilder.Configurations.Add(new PersonConfiguration());
            modelBuilder.Configurations.Add(new AddressConfiguration());


            modelBuilder.ComplexType<Address>();

            modelBuilder.ComplexType<Address>().Property(p => p.StreetAddress).HasMaxLength(150);


            modelBuilder.Entity<Destination>().ToTable("a_table_name");

            modelBuilder.Entity<Destination>().Property(d => d.Name).IsRequired();

            modelBuilder.Entity<Destination>().Property(d => d.Description).HasMaxLength(500);

            modelBuilder.Entity<Destination>().Property(d => d.Photo).HasColumnType("image");

            modelBuilder.Entity<Lodging>().Property(l => l.Name).IsRequired().HasMaxLength(200);



            modelBuilder.Entity<Trip>()
                        .HasKey(t => t.Identifier)
                        .Property(t => t.Identifier)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Person>()
                        .HasKey(p => p.SocialSecurityNumber);

            modelBuilder.Entity<Person>()
                        .Property(p => p.SocialSecurityNumber)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                       ;

            modelBuilder.Entity<Person>().Property(p => p.RowVersion).IsRowVersion();





        }
    }



    public  class Test
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El Nombre es obligatorio.")]
        [StringLength(50,ErrorMessage = "El nombre no tiene la longitud",MinimumLength = 5)]
        public string Nombre { get; set; }

        [Required]
        public string Apellido { get; set; }

        //[RegularExpression("")]
        [DataType(DataType.PhoneNumber)]
        public string Telefono { get; set; }
    }

    [Table("Locations")]
    public class Destination
    {
        [Column("LocationID")]
        public int DestinationId { get; set; }
        [Required, Column("LocationName")]
        public string Name { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }

        [Column(TypeName = "image")]
        public byte[] Photo { get; set; }

        public List<Lodging> Lodgings { get; set; }
    }


    public class Lodging
    {
        public int LodgingId { get; set; }
        public string Name { get; set; }
        public string Owner { get; set; }
        public bool IsResort { get; set; }
        public Destination Destination { get; set; }
    }

    public class Trip
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Identifier { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal CostUsd { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }

    [Table("People")]
    public class Person
    {
        [Key]
        [ConcurrencyCheck]
        public int SocialSecurityNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Address Address { get; set; }
        public PersonalInfo Info { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }


    [Table("People")]
    public class PersonPhoto
    {
        [Key, ForeignKey("PhotoOf")]
        public int PersonId { get; set; }
        [Column(TypeName = "image")]
        public byte[] Photo { get; set; }
        public string Caption { get; set; }
        public Person PhotoOf { get; set; }
    }

    [ComplexType]
    public class Address
    {
        public int AddressId { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
    }


    [ComplexType]
    public class PersonalInfo
    {
        public Measurement Weight { get; set; }
        public Measurement Height { get; set; }
        public string DietryRestrictions { get; set; }
    }
    public class Measurement
    {
        public decimal Reading { get; set; }
        public string Units { get; set; }
    }

    //public class DestinationConfiguration : EntityTypeConfiguration<Destination>
    //{
    //    public DestinationConfiguration()
    //    {
    //        Property(d => d.Name).IsRequired();
    //        Property(d => d.Description).HasMaxLength(500);
    //        Property(d => d.Photo).HasColumnType("image");
    //    }
    //}
    public class LodgingConfiguration : EntityTypeConfiguration<Lodging>
    {
        public LodgingConfiguration()
        {
            Property(l => l.Name).IsRequired().HasMaxLength(200);
        }
    }

    public class PersonConfiguration : EntityTypeConfiguration<Person>
    {
        public PersonConfiguration()
        {
            Property(p => p.SocialSecurityNumber).IsConcurrencyToken();
        }
    }

    public class AddressConfiguration : ComplexTypeConfiguration<Address>
    {
        public AddressConfiguration()
        {
            Property(a => a.StreetAddress).HasMaxLength(150);
        }
    }

    public class DestinationConfiguration : EntityTypeConfiguration<Destination>
    {
        public DestinationConfiguration()
        {
            Property(d => d.Name).IsRequired().HasColumnName("LocationName");
            Property(d => d.DestinationId).HasColumnName("LocationID");
            Property(d => d.Description).HasMaxLength(500);
            Property(d => d.Photo).HasColumnType("image");
            // ToTable("Locations", "baga");
            Map(m =>
            {
                m.Properties(d => new { d.Name, d.Country, d.Description });
                m.ToTable("Locations");
            });
            Map(m =>
            {
                m.Properties(d => new { d.Photo });
                m.ToTable("LocationPhotos");
            });
        }
    }
}
