using SharpTrooper.Core;
using SharpTrooper.Entities;

namespace StarWarsUniverseApp.Services
{
    public class StarWarsFilmService
    {
        private readonly SharpTrooperCore _sharpTrooperCore;

        public StarWarsFilmService()
        {
            _sharpTrooperCore = new SharpTrooperCore();
        }

        public async Task<IEnumerable<Film>> GetAllFilmsAsync()
        {
            var films = (await _sharpTrooperCore.GetAllFilmsAsync()).results;
            return films;
        }

        public async Task<Film> GetFilmByIdAsync(string id)
        {
            var film = await _sharpTrooperCore.GetFilmAsync(id);
            return film;
        }

        public async Task<IEnumerable<People>> GetAllPeopleAsync()
        {
            var allPeople = new List<People>();
            var currentPage = "1";
            SharpEntityResults<People> result;

            do
            {
                result = await _sharpTrooperCore.GetAllPeopleAsync(currentPage);
                allPeople.AddRange(result.results);

                currentPage = result.nextPageNo;
            }
            while (!string.IsNullOrEmpty(currentPage));

            return allPeople;
        }

        public async Task<People> GetPeopleByIdAsync(string id)
        {
            var person = await _sharpTrooperCore.GetPeopleAsync(id);
            return person;
        }
    }
}
