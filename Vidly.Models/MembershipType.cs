using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class MembershipType
    {
        public byte Id { get; set; }

        [StringLength(255)]
        [Display(Name = "Membership Type")]
        public string Name { get; set; }

        [Display(Name = "Signup Fee")]
        public short SignUpFee { get; set; }

        [Display(Name = "Duration in Months")]
        public byte DurationInMonths { get; set; }

        [Display(Name = "Discount Rate")]
        public byte DiscountRate { get; set; }

        public static readonly byte Unknown = 0;
        public static readonly byte payAsYouGo = 1;

    }
}
