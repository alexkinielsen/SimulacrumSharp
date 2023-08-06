using SimulacrumSharp.SimulationAPI.Helpers;
using SimulacrumSharp.SimulationAPI.Helpers.Interfaces;
using SimulacrumSharp.SimulationAPI.Models.Survivor;
using SimulacrumSharp.SimulationAPI.Services.Interfaces.Simulation.Survivor;

namespace SimulacrumSharp.SimulationAPI.Services.Simulation.Survivor
{
    public class TribalCouncilService : ITribalCouncilService
    {
        private readonly ICommonHelper _commonHelper;

        private SurvivorEpisode _episode;

        public TribalCouncilService(
            ICommonHelper commonHelper)
        {
            _commonHelper = commonHelper;
        }

        public TribalCouncil SimulateTribalCouncil(SurvivorEpisode episode, int day, IList<Castaway> castawaysRemaining, Tribe attendingTribe, bool addToJury)
        {
            _episode = episode;
            var tribalCouncil = new TribalCouncil()
            {
                Name = $"Final {castawaysRemaining.Count} - {attendingTribe.Name} Tribe",
                AttendingTribe = attendingTribe.Name,
                CastawaysAttending = new List<string>(),
                AttendeesWithImmunity = castawaysRemaining.Where(x => attendingTribe.Castaways.Contains(x.Name)).Where(x => x.IsImmune).Select(x => x.Name).ToList(),
                EpisodeNumber = episode.EpisodeNumber,
                Day = day
            };
            tribalCouncil.CastawaysAttending.AddRange(attendingTribe.Castaways);

            var votedOut = GetVotedOutCastaway(castawaysRemaining, tribalCouncil);

            tribalCouncil.VotedOut = votedOut.Name;
            castawaysRemaining.Remove(votedOut);
            attendingTribe.Castaways.Remove(votedOut.Name);
            tribalCouncil.CastawaysRemaining = castawaysRemaining.Select(x => x.Name).ToList();
            votedOut.Placement = tribalCouncil.CastawaysRemaining.Count + 1;
            if (addToJury)
            {
                votedOut.Tribe = "Jury";
                votedOut.OnJury = true;
                votedOut.TribeHistory.Add(day, "Jury");
            }
            else
            {
                votedOut.Tribe = "Pre-Jury";
                votedOut.OnJury = false;
                votedOut.TribeHistory.Add(day, "Pre-Jury");
            }

            return tribalCouncil;
        }

        public TribalCouncil SimulateFinalTribalCouncil(SurvivorEpisode episode, int day, IList<Castaway> finalists, IList<Castaway> jury)
        {
            _episode = episode;

            foreach (var castaway in finalists)
            {
                castaway.IsImmune = false;
            }
            var finalistNames = _commonHelper.FormatListOfNamesString(finalists.Select(x => x.Name).ToList());
            var juryNames = _commonHelper.FormatListOfNamesString(jury.Select(x => x.Name).ToList());
            var tribalCouncil = new TribalCouncil()
            {
                Name = $"Final {finalists.Count} - {finalistNames}",
                EpisodeNumber = episode.EpisodeNumber,
                Day = day,
                CastawaysAttending = new List<string>(),
                CastawaysRemaining = finalists.Select(x => x.Name).ToList()
            };
            tribalCouncil.CastawaysAttending.AddRange(finalists.Select(x => x.Name).ToList());

            var finalVotingRound = GetFinalVotingRound(tribalCouncil, finalists, jury);

            var soleSurvivor = GetSoleSurvivor(finalists, finalVotingRound);
            soleSurvivor.Placement = 1;
            tribalCouncil.SoleSurvivor = soleSurvivor.Name;

            var losingFinalists = finalists
                .Where(x => !x.Equals(soleSurvivor))
                .ToList();
            foreach(var loser in losingFinalists)
            {
                loser.Placement = 2;
                tribalCouncil.CastawaysRemaining.Remove(loser.Name);
                finalists.Remove(loser);
            }

            var finalNote = $"At {tribalCouncil.Name}, {finalistNames} faced a jury consisting of {juryNames}. After the jury voted, {soleSurvivor.Name} was named the winner, with {soleSurvivor.WinningVotes.Count} to win.";
            _episode.EpisodeNotes.Add(finalNote);
            Console.WriteLine(finalNote);

            return tribalCouncil;
        }

        private Castaway GetSoleSurvivor(IList<Castaway> finalists, VotingRound finalVotingRound)
        {
            var soleSurvivor = new Castaway();
            if (finalVotingRound.MostVotedCastaways.Count().Equals(1))
            {
                soleSurvivor = finalists
                    .Single(x => finalVotingRound.MostVotedCastaways.Contains(x.Name));
            }
            else
            {
                soleSurvivor = _commonHelper.GetRandomElement(finalists.Where(x => finalVotingRound.MostVotedCastaways.Contains(x.Name)));
            }

            return soleSurvivor;
        }

        private Castaway GetVotedOutCastaway(IList<Castaway> castawaysRemaining, TribalCouncil tribalCouncil)
        {
            var eligibleCastaways = castawaysRemaining
                .Where(x => tribalCouncil.CastawaysAttending.Contains(x.Name))
                .Where(x => !x.IsImmune)
                .ToList();
            var votingRounds = new List<VotingRound>();

            if (eligibleCastaways.Count().Equals(1))
            {
                var eliminatedByDefault = eligibleCastaways.Single();

                var defaultNote = $"At {tribalCouncil.Name}, {eliminatedByDefault.Name} was the only castaway eligible to receive votes, they they were eliminated by default from the {tribalCouncil.AttendingTribe} Tribe.";
                _episode.EpisodeNotes.Add(defaultNote);
                Console.WriteLine(defaultNote);
            }

            var roundNumber = 1;
            var initialVotingRound = GetVotingRound(castawaysRemaining, tribalCouncil, eligibleCastaways, roundNumber);

            var mostVotedCastaways = eligibleCastaways
                .Where(x => initialVotingRound.MostVotedCastaways.Contains(x.Name))
                .ToList();

            if (mostVotedCastaways.Count().Equals(0))
            {
                var eliminated = _commonHelper.GetRandomElement(eligibleCastaways);

                var eligibleCastawayNames = _commonHelper.FormatListOfNamesString(eligibleCastaways.Select(x => x.Name).ToList());
                var fireMakingNote = $"At {tribalCouncil.Name}, no votes were cast, so {eligibleCastawayNames} participated in a firemaking challenge. {eliminated.Name} was unable to make fire, so they were eliminated from the {tribalCouncil.AttendingTribe} Tribe.";
                _episode.EpisodeNotes.Add(fireMakingNote);
                Console.WriteLine(fireMakingNote);

                return eliminated;
            }
            if (mostVotedCastaways.Count().Equals(1))
            {
                var votedOut = mostVotedCastaways.Single();

                var votedOutNote = $"At {tribalCouncil.Name}, {votedOut.Name} was voted out of the {tribalCouncil.AttendingTribe} Tribe.";
                _episode.EpisodeNotes.Add(votedOutNote);
                Console.WriteLine(votedOutNote);

                return votedOut;
            }

            roundNumber++;
            var secondVotingRound = GetVotingRound(castawaysRemaining, tribalCouncil, mostVotedCastaways, roundNumber);

            var mostVotedCastawaysInRevote = eligibleCastaways
                .Where(x => secondVotingRound.MostVotedCastaways.Contains(x.Name))
                .ToList();

            if (mostVotedCastawaysInRevote.Count().Equals(0))
            {
                var eliminated = _commonHelper.GetRandomElement(eligibleCastaways);

                var errorNote = $"At {tribalCouncil.Name}, an unexpected error occurred on the revote and no votes were cast, so {eliminated.Name} was randomly selected from the most voted castaways and eliminated from the {tribalCouncil.AttendingTribe} Tribe.";
                _episode.EpisodeNotes.Add(errorNote);
                Console.WriteLine(errorNote);

                return eliminated;
            }
            if (mostVotedCastawaysInRevote.Count().Equals(1))
            {
                var votedOut = mostVotedCastawaysInRevote.Single();

                var namesInTheRevote = _commonHelper.FormatListOfNamesString(mostVotedCastaways.Select(x => x.Name).ToList());
                var revoteNote = $"At {tribalCouncil.Name}, after a revote between {namesInTheRevote}, {votedOut.Name} was voted out of the {tribalCouncil.AttendingTribe} Tribe.";
                _episode.EpisodeNotes.Add(revoteNote);
                Console.WriteLine(revoteNote);

                return votedOut;
            }

            var castawaysDrawingRocks = castawaysRemaining
                .Except(castawaysRemaining.Where(x => x.IsImmune))
                .Where(x => tribalCouncil.CastawaysAttending.Contains(x.Name))
                .Except(mostVotedCastawaysInRevote)
                .ToList();

            var purpleRockVictim = _commonHelper.GetRandomElement(castawaysDrawingRocks);

            var namesInDeadlock = _commonHelper.FormatListOfNamesString(mostVotedCastawaysInRevote.Select(x => x.Name).ToList());
            var namesDrawingRocks = _commonHelper.FormatListOfNamesString(castawaysDrawingRocks.Select(x => x.Name).ToList());
            var rockDrawNote = $"At {tribalCouncil.Name}, the {tribalCouncil.AttendingTribe} could not break a tie between {namesInDeadlock}, so {namesDrawingRocks} had to draw rocks and {purpleRockVictim.Name} drew the purple rock, eliminating them from the {tribalCouncil.AttendingTribe} Tribe.";
            _episode.EpisodeNotes.Add(rockDrawNote);
            Console.WriteLine(rockDrawNote);

            return purpleRockVictim;
        }

        private VotingRound GetFinalVotingRound(TribalCouncil tribalCouncil, IList<Castaway> finalists, IList<Castaway> jury)
        {
            var votingRound = new VotingRound
            {
                Name = $"{tribalCouncil.Name} - Jury Vote"
            };
            
            foreach(var juror in jury)
            {
                var votingToWin = _commonHelper.GetRandomElement(finalists);
                votingRound.Votes.Add(juror.Name, votingToWin.Name);
                juror.VotingHistory.Add(votingRound.Name, votingToWin.Name);
                votingToWin.WinningVotes.Add(juror.Name);
            }

            votingRound.MostVotedCastaways = votingRound.Votes.Values.Mode().ToList();

            tribalCouncil.VotingRounds.Add(votingRound);
            return votingRound;
        }

        private VotingRound GetVotingRound(IList<Castaway> castawaysRemaining, TribalCouncil tribalCouncil, IList<Castaway> eligibleCastaways, int roundNumber)
        {
            var votingRound = new VotingRound
            {
                Name = $"{tribalCouncil.Name} - Round {roundNumber}"
            };
            var castawaysVoting = tribalCouncil.CastawaysAttending;

            if (roundNumber.Equals(2))
            {
                castawaysVoting = castawaysVoting
                    .Except(eligibleCastaways.Select(x => x.Name))
                    .ToList();
            }
            if (eligibleCastaways.Count.Equals(2))
            {
                castawaysVoting = castawaysVoting
                    .Except(eligibleCastaways.Select(x => x.Name))
                    .ToList();
                if (!castawaysVoting.Any())
                {
                    tribalCouncil.VotingRounds.Add(votingRound);
                    return votingRound;
                }
            }

            foreach (var castawayName in castawaysVoting)
            {
                var votingCastaway = castawaysRemaining.Single(x => castawayName.Equals(x.Name));
                var eligibleToVoteOut = new List<Castaway>();
                eligibleToVoteOut.AddRange(eligibleCastaways);
                eligibleToVoteOut.Remove(votingCastaway);

                var votingOut = _commonHelper.GetRandomElement(eligibleToVoteOut);
                votingRound.Votes.Add(castawayName, votingOut.Name);
                votingCastaway.VotingHistory.Add(votingRound.Name, votingOut.Name);
                if (votingOut.VotesAgainst.Keys.Contains(votingRound.Name))
                {
                    votingOut.VotesAgainst[votingRound.Name].Add(votingCastaway.Name);
                }
                else
                {
                    votingOut.VotesAgainst.Add(votingRound.Name, new List<string> { votingCastaway.Name });
                }
                
            }
            votingRound.MostVotedCastaways = votingRound.Votes.Values.Mode().ToList();

            tribalCouncil.VotingRounds.Add(votingRound);
            return votingRound;
        }
    }
}
