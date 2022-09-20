using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using instacult.Models;

namespace instacult.Repositories
{
  public class CultsRepository
  {
    private readonly IDbConnection _db;

    public CultsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal List<Cult> GetAll()
    {
      string sql = @"
        SELECT
            c.*,
            a.*
        FROM cults c
        JOIN accounts a ON a.id = c.leaderId;
        ";
      return _db.Query<Cult, Profile, Cult>(sql, (cult, profile) =>
      {
        cult.Leader = profile;
        return cult;
      }).ToList();
    }

    internal Cult GetOne(int id)
    {
      string sql = @"
       SELECT
            c.*,
            a.*
        FROM cults c
        JOIN accounts a ON a.id = c.leaderId
        WHERE c.id = @id;
        ";
      return _db.Query<Cult, Profile, Cult>(sql, (cult, profile) =>
        {
          cult.Leader = profile;
          return cult;
        }, new { id }).FirstOrDefault();
    }

    internal void Delete(int id)
    {
      string sql = @"
        DELETE FROM cults WHERE id = @id;
        ";
      _db.Execute(sql, new { id });
    }

    internal Cult Create(Cult newCult)
    {
      string sql = @"
        INSERT INTO cults
        (name, description, fee, coverImg, leaderId)
        VALUES
        (@name, @description, @fee, @coverImg, @leaderId);
        SELECT LAST_INSERT_ID();
        ";
      int id = _db.ExecuteScalar<int>(sql, newCult);
      newCult.Id = id;
      return newCult;
    }

    internal Cult Update(Cult cultData)
    {
      string sql = @"
      UPDATE cults SET
      name = @name,
      description = @description,
      coverImg = @coverImg,
      fee = @fee
      WHERE id = @id;
      ";

      _db.Execute(sql, cultData);
      return cultData;
    }
  }
}