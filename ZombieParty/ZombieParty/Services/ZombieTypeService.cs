using ZombieParty.Models.Data;
using ZombieParty.Models;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ZombieParty.Services
{
    public class ZombieTypeService : ServiceBaseAsync<ZombieType>, IZombieTypeService
    {
        public ZombieTypeService(ZombiePartyDbContext dbContext) : base(dbContext) { }

        public bool HasAssociatedZombies(int id)
        {
            var associatedZombies = _dbContext.Zombies.Where(x => x.ZombieTypeId == id).Any();
            return associatedZombies;
        }

        public override async Task DeleteAsync(int id)
        {
            var zombieType = await this.GetByIdAsync(id);
            if (zombieType == null)
                return;
            if (HasAssociatedZombies(id))
            {
                zombieType.IsDisponible = false;
                await this.EditAsync(zombieType);
            }
            else
            {
                await base.DeleteAsync(id);
            }
        }

        public IEnumerable<SelectListItem> ListZombieTypeDisponible()
        {
            var zombieTypeDisponibleList = _dbContext.ZombieTypes.Where(zt => zt.IsDisponible == true)
            .Select(t => new SelectListItem
            {
                Text = t.TypeName,
                Value = t.Id.ToString()
            }).OrderBy(t => t.Text);

            return zombieTypeDisponibleList;
        }
    }
}
