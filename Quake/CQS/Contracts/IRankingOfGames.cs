using System.Collections.Generic;

namespace Quake.CQS.Contracts
{
    public interface IRankingOfGames
    {
        List<KillsByPlayers> FindPlayerBy(string name);
    }
}