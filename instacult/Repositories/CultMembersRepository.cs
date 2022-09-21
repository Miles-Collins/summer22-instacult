using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using instacult.Models;

namespace instacult.Repositories
{
  public class CultMembersRepository
  {
    private readonly IDbConnection _db;

    public CultMembersRepository(IDbConnection db)
    {
      _db = db;
    }

    internal List<Cultist> GetCultists(int cultId)
    {
      string sql = @"
      SELECT 
        a.*,
        cm.*
    FROM cultMembers cm
    JOIN accounts a ON a.id = cm.accountId
    WHERE cm.cultId = @cultId;
      ";
      List<Cultist> cultists = _db.Query<Cultist, CultMember, Cultist>(sql, (cultist, cm) =>
      {
        cultist.cultMemberId = cm.Id;
        return cultist;
      }, new { cultId }).ToList();
      return cultists;
    }

    internal CultMember GetOne(int id)
    {
      string sql = @"
      SELECT * FROM cultMembers WHERE id = @id";
      return _db.Query<CultMember>(sql, new { id }).FirstOrDefault();
    }

    internal CultMember Create(CultMember cultMember)
    {
      string sql = @"
      INSERT INTO cultMembers
      (cultId, accountId)
      VALUES
      (@cultId, @accountId);
      SELECT LAST_INSERT_ID();
      ";
      int id = _db.ExecuteScalar<int>(sql, cultMember);
      cultMember.Id = id;
      return cultMember;
    }

    internal void Excommunicate(int id)
    {
      string sql = @"
        DELETE FROM cultMembers WHERE id = @id;
      ";
      _db.Execute(sql, new { id });
    }
  }
}