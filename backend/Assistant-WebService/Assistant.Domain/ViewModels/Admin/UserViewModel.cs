namespace Assistant.Domain.ViewModels.Admin
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }

        public string Password { get; set; }
        public string Salt { get; set; }
    }

    public class CreateUserViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class UpdateUserViewModel : CreateUserViewModel
    {
        public string Id { get; set; }
    }

    public class UserContextViewModel
    {
        // TODO
    }
}
