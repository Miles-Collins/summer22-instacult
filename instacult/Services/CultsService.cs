using System;
using System.Collections.Generic;
using instacult.Models;
using instacult.Repositories;

namespace instacult.Services
{
  public class CultsService
  {
    private readonly CultsRepository _cultsRepo;

    public CultsService(CultsRepository cultsRepo)
    {
      _cultsRepo = cultsRepo;
    }

    internal List<Cult> GetAll()
    {
      return _cultsRepo.GetAll();
    }

    internal Cult Create(Cult newCult)
    {
      return _cultsRepo.Create(newCult);
    }

    internal Cult GetOne(int id)
    {
      Cult cult = _cultsRepo.GetOne(id);
      if (cult == null)
      {
        throw new Exception("no cult by that Id");
      }
      return cult;
    }
    internal Cult Update(Cult update, string userId)
    {
      Cult original = GetOne(update.Id);
      if (original.LeaderId != userId)
      {
        throw new Exception("You are not the leader, can't update, you should be banished for you actions.");
      }
      original.Name = update.Name ?? original.Name;
      original.Fee = update.Fee ?? original.Fee;
      original.CoverImg = update.CoverImg ?? original.CoverImg;
      original.Description = update.Description ?? original.Description;
      return _cultsRepo.Update(original);
    }

    internal string Delete(int id, string userId)
    {
      Cult original = GetOne(id);
      if (original.LeaderId != userId)
      {
        throw new Exception("You are not the leader, can't destroy, you should be banished for you actions.");
      }
      _cultsRepo.Delete(id);
      return $"The Cult of {original.Name} has been destroyed.";
    }

  }
}