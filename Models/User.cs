namespace HotelPairs.Services
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User
    {
        public int UserID { get; set; }

        [Required]
        [StringLength(150)]
        public string Login { get; set; }

        [Required]
        [StringLength(250)]
        public string PasswordHash { get; set; }

        [Required]
        [StringLength(150)]
        public string LastName { get; set; }

        [Required]
        [StringLength(150)]
        public string FirstName { get; set; }

        public int RoleID { get; set; }

        public int? FailedLoginAttempts { get; set; }

        public bool? IsBlocked { get; set; }

        public DateTime? LastLoginAttempt { get; set; }

        public virtual Role Role { get; set; }

    }
}
