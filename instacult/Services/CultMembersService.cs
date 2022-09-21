using System;
using System.Collections.Generic;
using instacult.Models;
using instacult.Repositories;

namespace instacult.Services
{
  public class CultMembersService
  {
    private readonly CultMembersRepository _cultMemberRepo;
    private readonly CultsService _cultsService;

    public CultMembersService(CultMembersRepository cultMemberRepo, CultsService cultsService)
    {
      _cultMemberRepo = cultMemberRepo;
      _cultsService = cultsService;
    }

    internal List<Cultist> GetCultists(int cultId)
    {
      // TODO check if cult actually exists (which get one does)
      _cultsService.GetOne(cultId);
      return _cultMemberRepo.GetCultists(cultId);
    }

    internal CultMember Create(CultMember cultMember)
    {
      _cultsService.GetOne(cultMember.CultId);
      return _cultMemberRepo.Create(cultMember);

    }

    internal string Excommunicate(int id, Account user)
    {
      CultMember cultMember = _cultMemberRepo.GetOne(id);
      if (cultMember == null)
      {
        throw new Exception("no cultmember by that id");
      }
      Cult cult = _cultsService.GetOne(cultMember.CultId);
      if (cult.LeaderId != user.Id)
      {
        throw new Exception($"I can't let you do that {user.Name}");
      }
      _cultMemberRepo.Excommunicate(id);
      return $"Goodbye {user.Name}, we will be in touch.";
    }
  }
}