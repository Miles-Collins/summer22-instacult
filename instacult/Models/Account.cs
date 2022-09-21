namespace instacult.Models
{
  public class Profile : RepoItem<string>
  {
    public string Name { get; set; }

    public string Picture { get; set; }
  }
  // NOTE since account has MORE info that the profile the ACCOUNT EXTENDS PROFILE
  public class Account : Profile
  {
    public string Email { get; set; }
  }
  // NOTE Cultist extends profile to include the id of the membership to the cult
  public class Cultist : Profile
  {
    public int cultMemberId { get; set; }
  }

}