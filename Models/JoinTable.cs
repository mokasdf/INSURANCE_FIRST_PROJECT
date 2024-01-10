namespace INSURANCE_FIRST_PROJECT.Models
{
    public class JoinTable
    {
        public Useraccount? useraccount { get; set; }
        public Subcrebtion? subcrebtion { get; set; }
        public Subcrebtiontype? subcrebtiontype { get; set; }

        public Beneficiary? beneficiary { get; set; }

        public Testimonial? testimonial { get; set; }
    }
}
