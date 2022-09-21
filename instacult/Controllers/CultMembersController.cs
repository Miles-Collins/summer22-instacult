using System;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using instacult.Models;
using instacult.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace instacult.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class CultMembersController : ControllerBase
  {
    private readonly CultMembersService _cultMembersService;

    public CultMembersController(CultMembersService cultMembersService)
    {
      _cultMembersService = cultMembersService;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Cultist>> Create([FromBody] CultMember cultMember)
    {
      try
      {
        Cultist user = await HttpContext.GetUserInfoAsync<Cultist>();
        cultMember.AccountId = user.Id;
        CultMember newMember = _cultMembersService.Create(cultMember);
        user.cultMemberId = newMember.Id;
        return user;
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<string>> Excommunicate(int id)
    {
      try
      {
        Account user = await HttpContext.GetUserInfoAsync<Account>();
        string message = _cultMembersService.Excommunicate(id, user);
        return Ok(message);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

  }
}