namespace HostelService.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Card")]
    public partial class Card
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Card()
        {
            Booking = new HashSet<Booking>();
        }

        public int CardID { get; set; }

        public long CardNumber { get; set; }

        [StringLength(26)]
        public string CardNumberBase { get; set; }

        public int CustomerID { get; set; }

        public int? BirthYear { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime IssueDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime ExpirationDate { get; set; }

        [StringLength(50)]
        public string SiteID { get; set; }

        public int? LocalCustomerID { get; set; }

        [StringLength(20)]
        public string CardType { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Address { get; set; }

        [StringLength(50)]
        public string ZipCode { get; set; }

        [StringLength(50)]
        public string City { get; set; }

        [StringLength(50)]
        public string Country { get; set; }

        [StringLength(255)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Phone { get; set; }

        [StringLength(50)]
        public string Mobile { get; set; }

        public short? Gender { get; set; }

        public bool? PermissionStatus { get; set; }

        public bool? Active { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Booking> Booking { get; set; }
    }
}
