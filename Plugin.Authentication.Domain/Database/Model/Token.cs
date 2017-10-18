using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plugin.Authentication.Domain.Database.Model
{
    public class Token
    {
        [Key, ForeignKey(nameof(Identity))]
        public int Id { get; set; }

        [Required]
        public string Value { get; set; }

        [Required]
        public DateTime GenerationTime { get; set; }

        [Required]
        public DateTime ExpiredTime { get; set; }

        [Required]
        public Identity Identity { get; set; }
    }
}