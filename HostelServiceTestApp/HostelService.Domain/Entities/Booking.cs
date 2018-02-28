namespace HostelService.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Booking")]
    public partial class Booking
    {
        public int BookingID { get; set; }

        public int CustomerID { get; set; }

        public int? CardID { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime BookingDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime FirstNightDate { get; set; }

        public int NumberOfDays { get; set; }

        public int NumberOfPersons { get; set; }

        public int LocalCustomerID { get; set; }

        [Required]
        [StringLength(50)]
        public string SiteID { get; set; }

        public int? CBBookingID { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        [Column(TypeName = "money")]
        public decimal? Price { get; set; }

        [StringLength(20)]
        public string TypeOfRoom { get; set; }

        public bool? SendToCsv { get; set; }

        [StringLength(50)]
        public string Campaign { get; set; }

        [StringLength(50)]
        public string Category { get; set; }

        public virtual Card Card { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
