using System.ComponentModel.DataAnnotations;

namespace Vidly.Models.Dtos
{
    public class MembershipTypeDto
    {
        public byte Id { get; set; }

        [StringLength(255)]
        public string Name { get; set; }
    }
}
