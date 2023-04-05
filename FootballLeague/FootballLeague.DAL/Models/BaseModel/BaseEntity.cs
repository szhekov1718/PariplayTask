using System;
using System.ComponentModel.DataAnnotations;

namespace FootballLeague.DAL.Models.BaseModel
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateUpdated { get; set; }

        public DateTime? DateDeleted { get; set; }

        public bool IsDeleted { get; set; }
    }
}
