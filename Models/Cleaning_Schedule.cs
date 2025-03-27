namespace HotelPairs.Services
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Cleaning_Schedule
    {
        public int Cleaning_ScheduleID { get; set; }

        public int StaffID { get; set; }

        public DateTime CleaningDate { get; set; }

        [StringLength(20)]
        public string StatusClean { get; set; }

        public int RoomID { get; set; }

        public string GuestPreferences { get; set; }

        [Required]
        [StringLength(20)]
        public string CleaningStatus { get; set; }

        public virtual Room Room { get; set; }

        public virtual Staff Staff { get; set; }
    }
}
