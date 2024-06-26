using ZombieParty.Models.Data;
using ZombieParty.Models;

namespace ZombieParty.Services
{
    public class ZombieTypeService : ServiceBaseAsync<ZombieType>, IZombieTypeService
    {
        public ZombieTypeService(ZombiePartyDbContext dbContext) : base(dbContext) { }
    }
}
