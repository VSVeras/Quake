using Quake.Entities;
using System.Collections.Generic;

namespace Quake.Infrastructure.Contracts
{
    public interface IGamesLogFileReader
    {
        List<Game> Reader();
    }
}
