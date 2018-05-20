using System.Collections.Generic;

namespace Quake.CQRS.Contracts
{
    public interface IRankingOfGames
    {
        List<KillsByPlayers> FindPlayerBy(string name);
    }
}
