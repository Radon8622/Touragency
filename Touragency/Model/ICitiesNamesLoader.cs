using System.Collections.Generic;

namespace Touragency.Model
{
    public interface ICitiesNamesLoader
    {
        public IEnumerable<string> GetCitiesNames();
    }
}
