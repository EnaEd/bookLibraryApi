using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BookLibraryApi.BusinesLayer.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        [BindingBehavior(BindingBehavior.Optional)]
        public string Name { get; set; }

        [BindingBehavior(BindingBehavior.Required)]
        public string Login { get; set; }

        [BindingBehavior(BindingBehavior.Required)]
        public string Password { get; set; }

        [BindingBehavior(BindingBehavior.Optional)]
        public string ConfirmPassword { get; set; }

        [BindingBehavior(BindingBehavior.Optional)]
        public bool RememberMe { get; set; }

        [BindingBehavior(BindingBehavior.Optional)]
        public bool EmailConfimed { get; set; }

        [BindingBehavior(BindingBehavior.Optional)]
        public string Role { get; set; }
    }
}
