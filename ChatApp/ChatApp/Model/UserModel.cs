namespace ChatApp.Model
{
  class UserModel
  {
    private static UserModel _instance;

    public static UserModel Instance => _instance ?? (_instance = new UserModel());

    public string Email { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public bool IsActive { get; set; } = true;

  }
}
