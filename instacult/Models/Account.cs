namespace instacult.Models
{
  public class Profile : RepoItem<string>
  {
    public string Name { get; set; }

    public string Picture { get; set; }
  }
  //   NOTE since account has MORE info that the profile the ACCOUNT EXTENDS PROFILE
  public class Account : Profile
  {
    public string Email { get; set; }
  }

}