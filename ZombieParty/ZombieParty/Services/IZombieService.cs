using ZombieParty.Models;

namespace ZombieParty.Services
{
    public interface IZombieService : IServiceBaseAsync<Zombie>
    {
        public Task<IReadOnlyList<Zombie>> GetAllByZombieTypeAsync(int zombieTypeId);

        public Task<IReadOnlyList<Zombie>> GetAllIndexAsync();

        public bool ZombieNameExist(string name);
    }
}
