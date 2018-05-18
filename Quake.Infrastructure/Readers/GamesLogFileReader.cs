using Quake.Entities;
using Quake.Infrastructure.Contracts;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Quake.Infrastructure.Readers
{
    public class GamesLogFileReader : IGamesLogFileReader
    {
        private readonly string LogFilePath;
        private readonly Regex actionsGames = new Regex("(InitGame)");

        public GamesLogFileReader(string logFilePath)
        {
            LogFilePath = logFilePath;
        }

        public List<Game> Reader()
        {
            try
            {
                var games = new List<Game>();

                using (StreamReader reader = new StreamReader(LogFilePath, Encoding.UTF8))
                {
                    while (reader.Peek() >= 0)
                    {
                        var row = reader.ReadLine();

                        Match action = actionsGames.Match(row);

                        switch (action.Value)
                        {
                            case "InitGame":
                                games.Add(new Game());
                                break;
                            default:
                                break;
                        }
                    }
                    reader.Close();
                    reader.Dispose();
                }
                return games;
            }
            catch
            {
                throw;
            }
        }
    }
}
