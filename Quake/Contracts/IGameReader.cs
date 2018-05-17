using Quake.Entities;
using System.Collections.Generic;

namespace Quake.Contracts
{
    public interface IGameReader
    {
        List<Game> Reader();
    }
}
