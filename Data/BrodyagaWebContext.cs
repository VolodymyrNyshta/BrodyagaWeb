/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
*/
using Microsoft.EntityFrameworkCore;
using BrodyagaWeb.Models;

namespace BrodyagaWeb.Data
{
    public class BrodyagaWebContext : DbContext
    {
        public BrodyagaWebContext(DbContextOptions<BrodyagaWebContext> options)
            : base(options)
        {
        }
        public BrodyagaWebContext() { }

        /*------------------------------------------*/
        public DbSet<DictGoodType> DictGoodType { get; set; } = default!;
        public DbSet<DictMeasure> DictMeasures { get; set; } = default!;
        public DbSet<DictFighterState> DictFighterStates { get; set; } = default!;
        public DbSet<Unit> Units { get; set; } = default!;
        public DbSet<User> Users { get; set; } = default!;
        
        public IQueryable<Unit> GetUnits()
        {
            var vPlatoonSet = Units.FromSql($"SELECT * FROM dbo.VW_PlatoonTree ORDER BY ParentId, Number");
            return vPlatoonSet;
        }

        /*------------------------------------------*/
        public IQueryable<User> GetUsers()
        {
            var vFigterSet = Users.FromSql($@"SELECT Fighters.Id, 
    Fighters.IdPlatoon,
    Fighters.FirstName,
    Fighters.MidleName,
    Fighters.LastName,
    Platoon.Number PlatoonNumber
FROM dbo.Fighters
JOIN dbo.Platoon ON Platoon.Id = Fighters.IdPlatoon");
            return vFigterSet;
        }

        public IQueryable<User> GetFigter(Guid AId)
        {
            var vFigterSet = Users.FromSql($@"SELECT Fighters.Id, 
    Fighters.IdPlatoon,
    Fighters.FirstName,
    Fighters.MidleName,
    Fighters.LastName,
    Platoon.Number PlatoonNumber
FROM dbo.Fighters
JOIN dbo.Platoon ON Platoon.Id = Fighters.IdPlatoon
WHERE Fighters.Id = {AId}");
            return vFigterSet;
        }

        /*------------------------------------------*/
        public IQueryable<DictMeasure> GetMeasures()
        {
            var vMeasuresSet = DictMeasures.FromSql($"SELECT Measure.Id, Measure.Value FROM dbo.Measure");
            return vMeasuresSet;
        }

        /*------------------------------------------*/
        public DbSet<DictGoods> DictGoods { get; set; } = default!;

        /*------------------------------------------*/
        public DbSet<DictExtSrc> DictExtSrc { get; set; } = default!;

        /*------------------------------------------*/
        public DbSet<TransferAct> TransferAct { get; set; } = default!;

        /*------------------------------------------*/
        public DbSet<DictSize> DictSize { get; set; } = default!;

        /*------------------------------------------*/
        public DbSet<GoodInStock> GoodInStock { get; set; } = default!;

        /*------------------------------------------*/
        public DbSet<NFCToken> NFCToken { get; set; } = default!;

        /*------------------------------------------*/
        public DbSet<GoodImage> GoodImage { get; set; } = default!;

        /*------------------------------------------*/
        public DbSet<GoodInStockImage> GoodInStockImage { get; set; } = default!;

    }
}
