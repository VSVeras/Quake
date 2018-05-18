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
                            case "ClientUserinfoChanged":
                                MappingChangedPlayerName(currentGame, row);
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

        private Game MappingGame(List<Game> games)
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

        private void MappingPlayes(Game currentGame, string row)
        {
            var findText = " ClientConnect: ";
            try
            {
                string firstPart = row.Substring(row.IndexOf(findText) + findText.Length);
                int id = Int32.Parse(firstPart);
                currentGame.Add(new Player(id));
            }
            catch
            {
                throw;
            }
        }

        private void MappingChangedPlayerName(Game currentGame, string row)
        {
            var findText = " ClientUserinfoChanged: ";

            try
            {
                string firstPart = row.Substring(row.IndexOf(findText) + findText.Length);
                int id = Int32.Parse(firstPart.Substring(0, firstPart.IndexOf(@"n\")));

                firstPart = row.Substring(row.IndexOf(@"n\") + 2);
                string name = firstPart.Substring(0, firstPart.IndexOf(@"\t\"));

                currentGame.ChangeNameOf(new Player(id), name);
            }
            catch
            {
                throw;
            }
        }
    }
}
