namespace HotelPairs.Services
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Room
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Room()
        {
            Bookings = new HashSet<Booking>();
            CleaningSchedules = new HashSet<CleaningSchedule>();
            RoomCards = new HashSet<RoomCard>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RoomID { get; set; }

        public int Number { get; set; }

        public int TypeID { get; set; }

        [Required]
        [StringLength(20)]
        public string RoomStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Booking> Bookings { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CleaningSchedule> CleaningSchedules { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RoomCard> RoomCards { get; set; }

        public virtual Type Type { get; set; }
    }
}
