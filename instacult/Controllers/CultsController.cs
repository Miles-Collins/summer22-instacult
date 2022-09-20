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

    public CultsController(CultsService cultsService)
    {
      _cultsService = cultsService;
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