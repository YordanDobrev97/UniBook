namespace UniBook.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using UniBook.Data.Common.Repositories;
    using UniBook.Data.Models;
    using UniBook.Services.Mapping;

    using Microsoft.EntityFrameworkCore;

    public class SettingsService : ISettingsService
    {
        private readonly IDeletableEntityRepository<Setting> settingsRepository;

        public SettingsService(IDeletableEntityRepository<Setting> settingsRepository)
        {
            this.settingsRepository = settingsRepository;
        }

        public async Task<int> GetCountAsync()
        {
            return await this.settingsRepository.All().CountAsync();
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.settingsRepository.All().To<T>().ToList();
        }
    }
}
