using Microsoft.AspNetCore.Mvc.Rendering;
using ZombieParty.Models;

namespace ZombieParty.Services
{
    public interface IZombieTypeService : IServiceBaseAsync<ZombieType>
    {
        public bool HasAssociatedZombies(int id);

        public IEnumerable<SelectListItem> ListZombieTypeDisponible();
    }
}
