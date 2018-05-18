using System.Collections.Generic;

namespace Quake.Entities.Contracts
{
    public interface IGames
    {
        void Save(List<Game> games);
    }
}
