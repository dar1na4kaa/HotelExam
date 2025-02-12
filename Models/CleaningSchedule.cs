namespace HotelPairs.Services
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CleaningSchedule")]
    public partial class CleaningSchedule
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CleaningScheduleID { get; set; }

        public int StaffID { get; set; }

        public DateTime CleaningDate { get; set; }

        [Required]
        [StringLength(20)]
        public string CleanStatus { get; set; }

        public int RoomID { get; set; }

        public string GuestsPreferences { get; set; }

        public virtual Room Room { get; set; }

        public virtual Staff Staff { get; set; }
    }
}
