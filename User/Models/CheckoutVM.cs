using Admin.Models;

namespace User.Models
{
    public class CheckoutVM
    {
        public List<Addresses> Addresses { get; set; }
        public Order Order { get; set; }
    }
}
