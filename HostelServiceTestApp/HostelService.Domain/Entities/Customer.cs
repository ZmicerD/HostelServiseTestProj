namespace HostelService.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Customer")]
    public partial class Customer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customer()
        {
            Booking = new HashSet<Booking>();
        }

        public int CustomerID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Address1 { get; set; }

        [StringLength(255)]
        public string Address2 { get; set; }

        [StringLength(10)]
        public string ZipCode { get; set; }

        [StringLength(50)]
        public string City { get; set; }

        public int? CountryID { get; set; }

        [StringLength(255)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Phone { get; set; }

        [StringLength(50)]
        public string Mobile { get; set; }

        public int? BirthYear { get; set; }

        public short? Gender { get; set; }

        [StringLength(50)]
        public string SiteID { get; set; }

        public int? LocalCustomerID { get; set; }

        public bool? PermissionStatus { get; set; }

        [StringLength(50)]
        public string Country { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreationDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? CreatorTypeID { get; set; }

        public int? UpdaterTypeID { get; set; }

        public bool? IsMerged { get; set; }

        public int? MergedCustomerID { get; set; }

        [StringLength(50)]
        public string Category { get; set; }

        [StringLength(2048)]
        public string Interests { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Booking> Booking { get; set; }
    }
}
