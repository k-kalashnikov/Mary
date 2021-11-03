using System.ComponentModel.DataAnnotations;

namespace Mary.Data.Models
{
    public class User : Common.Models.User
    {
        [Key]
        public Guid Id  { get; set; }
    }
}
