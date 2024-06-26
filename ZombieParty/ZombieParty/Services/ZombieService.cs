﻿using ZombieParty.Models.Data;
using ZombieParty.Models;
using System.Xml.Linq;
using ZombieParty.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace ZombieParty.Services
{
    public class ZombieService : ServiceBaseAsync<Zombie>, IZombieService
    {
        public ZombieService(ZombiePartyDbContext dbContext) : base(dbContext) { }

        public async Task<IReadOnlyList<Zombie>> GetAllByZombieTypeAsync(int zombieTypeId) {
            return await _dbContext.Zombies.Where(z => z.ZombieTypeId == zombieTypeId).ToListAsync();
        }

        public async Task<IReadOnlyList<Zombie>> GetAllIndexAsync()
        {
            return await _dbContext.Set<Zombie>().OrderBy(z => z.Name).Include(z => z.ZombieType).ToListAsync();
        }

        public bool ZombieNameExist(string name)
        {
            var ZombieSameName = _dbContext.Zombies.Where(x => x.Name == name).Any();
            return ZombieSameName;
        }

    }
}
