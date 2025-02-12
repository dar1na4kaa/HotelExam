namespace HotelPairs.Services
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RoomCard")]
    public partial class RoomCard
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RoomCardID { get; set; }

        public int RoomID { get; set; }

        [Required]
        [StringLength(20)]
        public string Access { get; set; }

        public virtual Room Room { get; set; }
    }
}
