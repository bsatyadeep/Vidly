using System;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class Customer
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Date of Birth")]
        [Min18YearsIfAMember]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "Subscribed to Newsletter?")]
        public bool IsSubscribedToNewsletter { get; set; }

        public MembershipType MembershipType { get; set; }

        [Display(Name = "Membership Type")]
        public byte MembershipTypeId { get; set; }
    }
}
