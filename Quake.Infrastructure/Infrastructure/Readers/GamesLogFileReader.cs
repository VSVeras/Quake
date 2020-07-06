using Quake.Entities;
using Quake.Infrastructure.Contracts;
using Quake.Services;
using Quake.ValueObjects;
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
        private const int _deadByTheWorld = 1022;

        public GamesLogFileReader(string logFilePath)
        {
            LogFilePath = logFilePath;
            actionsGames = new Regex("(InitGame|ClientConnect|ClientUserinfoChanged|Kill)");
        }

        public List<Game> Reader()
        {
            List<Game> games;
            using (StreamReader readerLog = new StreamReader(LogFilePath, Encoding.UTF8))
            {
                games = ProcessTheRows(readerLog);
            }
            return games;
        }

        private List<Game> ProcessTheRows(StreamReader reader)
        {
            var games = new List<Game>();
            Game currentGame = null;

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

                    case "Kill":
                        MappingKillPlayer(currentGame, row);
                        break;

                    default:
                        break;
                }
            }

            return games;
        }

        private Game MappingGame(List<Game> games)
        {
            Game newGame = new Game(new GeneratorStatisticsBecauseOfDeath());
            games.Add(newGame);

            return newGame;
        }

        private void MappingPlayes(Game currentGame, string row)
        {
            const string findText = " ClientConnect: ";

            string firstPart = row.Substring(row.IndexOf(findText) + findText.Length);
            int id = Int32.Parse(firstPart);

            currentGame.Add(new Player(id));
        }

        private void MappingChangedPlayerName(Game currentGame, string row)
        {
            const string findText = " ClientUserinfoChanged: ";

            string firstPart = row.Substring(row.IndexOf(findText) + findText.Length);
            int id = Int32.Parse(firstPart.Substring(0, firstPart.IndexOf(@"n\")));

            firstPart = row.Substring(row.IndexOf(@"n\") + 2);
            string name = firstPart.Substring(0, firstPart.IndexOf(@"\t\"));

            currentGame.ChangeNameOf(new Player(id), name);
        }

        private void MappingKillPlayer(Game currentGame, string row)
        {
            const string findText = " Kill: ";

            string firstPart = row.Substring(row.IndexOf(findText) + findText.Length);
            firstPart = firstPart.Substring(0, firstPart.IndexOf(": "));
            string[] infor = firstPart.Split(' ');

            var idFirstPlayer = Int32.Parse(infor[0]);
            var idSecondPlayer = Int32.Parse(infor[1]);
            var meansOfDeath = (MeansOfDeath)Int32.Parse(infor[2]);

            Kill(currentGame, idFirstPlayer, idSecondPlayer, meansOfDeath);
        }

        private void Kill(Game currentGame, int idFirstPlayer, int idSecondPlayer, MeansOfDeath meansOfDeath)
        {
            if (idFirstPlayer == _deadByTheWorld)
            {
                var killer = new Player(idSecondPlayer);
                currentGame.KillByNaturalDeath(killer, meansOfDeath);
            }
            else
            {
                var killer = new Player(idFirstPlayer);
                var victim = new Player(idSecondPlayer);
                currentGame.KillForMurder(killer, victim, meansOfDeath);
            }
        }
    }
}