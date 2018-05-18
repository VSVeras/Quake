using Quake.Entities;
using Quake.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Quake.Infrastructure.Infrastructure.Readers
{
    public class GamesLogFileReader : IGamesLogFileReader
    {
        private readonly string LogFilePath;
        private readonly Regex actionsGames;

        public GamesLogFileReader(string logFilePath)
        {
            LogFilePath = logFilePath;
            actionsGames = new Regex("(InitGame|ClientConnect|ClientUserinfoChanged|Kill)");
        }

        public List<Game> Reader()
        {
            try
            {
                var games = new List<Game>();
                Game currentGame = null;

                using (StreamReader reader = new StreamReader(LogFilePath, Encoding.UTF8))
                {
                    while (reader.Peek() >= 0)
                    {
                        var row = reader.ReadLine();

                        Match action = actionsGames.Match(row);

                        switch (action.Value)
                        {
                            case "InitGame":
                                currentGame = MappingGame(games);
                                break;
                            case "ClientConnect":
                                MappingPlayes(currentGame, row);
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

        private static Game MappingGame(List<Game> games)
        {
            try
            {
                Game newGame = new Game();
                games.Add(newGame);
                return newGame;
            }
            catch
            {
                throw;
            }
        }

        private void MappingPlayes(Game game, string row)
        {
            var findText = " ClientConnect: ";
            try
            {
                int id = Int32.Parse(row.Substring(row.IndexOf(findText) + findText.Length));
                game.Add(new Player(id));
            }
            catch
            {
                throw;
            }
        }
    }
}
