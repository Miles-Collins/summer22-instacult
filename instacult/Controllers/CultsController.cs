using System;
using System.Collections.Generic;
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
  public class CultsController : ControllerBase
  {
    private readonly CultsService _cultsService;
    private readonly CultMembersService _cultMembersService;

    public CultsController(CultsService cultsService, CultMembersService cultMembersService)
    {
      _cultsService = cultsService;
      _cultMembersService = cultMembersService;
    }

    [HttpGet]
    public ActionResult<List<Cult>> GetAll()
    {
      try
      {
        List<Cult> cults = _cultsService.GetAll();
        return Ok(cults);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}")]
    public ActionResult<Cult> GetOne(int id)
    {
      try
      {
        Cult cult = _cultsService.GetOne(id);
        return Ok(cult);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}/members")]
    public ActionResult<List<Cultist>> GetCultists(int id)
    {
      try
      {
        List<Cultist> cultists = _cultMembersService.GetCultists(id);
        return Ok(cultists);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Cult>> Create([FromBody] Cult newCult)
    {
      try
      {
        Account user = await HttpContext.GetUserInfoAsync<Account>();
        newCult.LeaderId = user.Id;
        Cult cult = _cultsService.Create(newCult);
        cult.Leader = user;
        return Ok(cult);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Cult>> Update(int id, [FromBody] Cult update)
    {
      try
      {
        Account user = await HttpContext.GetUserInfoAsync<Account>();
        update.Id = id;
        Cult cult = _cultsService.Update(update, user.Id);
        return Ok(cult);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<string>> Delete(int id)
    {
      try
      {
        Account user = await HttpContext.GetUserInfoAsync<Account>();
        string message = _cultsService.Delete(id, user.Id);
        return Ok(message);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

  }
}