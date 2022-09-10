using Microsoft.AspNet.Identity.EntityFramework;

namespace ESourcingUI.Core.Entites
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsSeller { get; set; }
        public bool IsBuyer { get; set; }

    }
}
