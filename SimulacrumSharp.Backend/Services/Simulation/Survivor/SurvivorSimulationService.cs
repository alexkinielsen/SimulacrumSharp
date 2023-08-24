using SimulacrumSharp.Backend.Models.Enums;
using SimulacrumSharp.Backend.Models.ServiceModels;
using SimulacrumSharp.Backend.Models.Survivor;
using SimulacrumSharp.Backend.Services.Interfaces.Simulation.Survivor;
using SimulacrumSharp.Backend.Helpers;
using SimulacrumSharp.Backend.Helpers.Interfaces;
using System.Linq;
using SimulacrumSharp.Backend.Services.Interfaces;

namespace SimulacrumSharp.Backend.Services.Simulation.Survivor
{
    public class SurvivorSimulationService : ISimulationService<SurvivorSeason>
    {
        private readonly ICommonHelper _commonHelper;
        private readonly ITribalCouncilService _tribalCouncilService;

        public SurvivorSimulationService(
            ICommonHelper commonHelper,
            ITribalCouncilService tribalCouncilService)
        {
            _commonHelper = commonHelper;
            _tribalCouncilService = tribalCouncilService;
        }

        public SimulationResponse<SurvivorSeason> Simulate(SimulationRequest<SurvivorSeason> request)
        {
            var castaways = new List<Castaway>
            {
                new Castaway { Name = "Richard" },
                new Castaway { Name = "Kelly" },
                new Castaway { Name = "Rudy" },
                new Castaway { Name = "Susan" },
                new Castaway { Name = "Sean" },
                new Castaway { Name = "Colleen" },
                new Castaway { Name = "Gervase" },
                new Castaway { Name = "Jenna" },
                new Castaway { Name = "Greg" },
                new Castaway { Name = "Gretchen" },
                new Castaway { Name = "Joel" },
                new Castaway { Name = "Dirk" },
                new Castaway { Name = "Ramona" },
                new Castaway { Name = "Stacey" },
                new Castaway { Name = "B. B." },
                new Castaway { Name = "Sonja" }
            };

            var tribes = new List<Tribe>
            {
                new Tribe { Name = "Pagong" },
                new Tribe { Name = "Tagi" },
                new Tribe { Name = "Rattana", IsMergeTribe = true }
            };

            var castSize = castaways.Count();
            var dayCount = 39;
            var episodeCount = 13;
            var jurySize = 7;
            var finalTribalCouncilSize = 2;
            var currentPhase = SurvivorPhase.Tribal;
            var inJuryPhase = false;

            var currentEpisode = 1;
            var currentDay = 1;
            var castawaysRemaining = new List<Castaway>();
            castawaysRemaining.AddRange(castaways);

            var startingTribes = tribes.Where(x => !x.IsMergeTribe).ToList();
            DetermineTribes(currentDay, castawaysRemaining, startingTribes);
            var activeTribes = startingTribes;

            var winner = string.Empty;

            var episodes = new List<SurvivorEpisode>();
            while (currentEpisode <= episodeCount)
            {
                var episode = new SurvivorEpisode
                {
                    EpisodeNumber = currentEpisode,
                    Name = $"Episode {currentEpisode}",
                    CastawaysCompeting = new List<string>(),
                    TribalCouncils = new List<TribalCouncil>()
                };

                episode.CastawaysCompeting.AddRange(castawaysRemaining.Select(x => x.Name));
                if (!inJuryPhase && castawaysRemaining.Count <= jurySize + finalTribalCouncilSize)
                {
                    inJuryPhase = true;
                }

                if (currentEpisode == episodeCount)
                {
                    while (castawaysRemaining.Count > finalTribalCouncilSize)
                    {
                        DetermineImmunity(castawaysRemaining, activeTribes, currentPhase);
                        foreach (var attendingTribe in activeTribes.Where(x => !x.IsImmune))
                        {
                            var tribalCouncil = _tribalCouncilService.SimulateTribalCouncil(episode, currentDay, castawaysRemaining, attendingTribe, inJuryPhase);
                            episode.TribalCouncils.Add(tribalCouncil);
                        }

                        currentDay += 1;
                    }

                    var jury = castaways
                        .Where(x => x.OnJury)
                        .ToList();
                    var finalTribalCouncil = _tribalCouncilService.SimulateFinalTribalCouncil(episode, currentDay, castawaysRemaining, jury);

                    winner = finalTribalCouncil.SoleSurvivor;
                    episode.TribalCouncils.Add(finalTribalCouncil);
                    episodes.Add(episode);
                    currentEpisode += 1;
                }
                else
                {
                    if (currentDay == 19)
                    {
                        var mergeTribe = tribes.Where(x => x.IsMergeTribe).ToList();
                        DetermineTribes(currentDay, castawaysRemaining, mergeTribe);
                        currentPhase = SurvivorPhase.Individual;
                        activeTribes = mergeTribe;
                    }

                    DetermineImmunity(castawaysRemaining, activeTribes, currentPhase);

                    currentDay += 2;

                    foreach (var attendingTribe in activeTribes.Where(x => !x.IsImmune))
                    {
                        var tribalCouncil = _tribalCouncilService.SimulateTribalCouncil(episode, currentDay, castawaysRemaining, attendingTribe, inJuryPhase);
                        episode.TribalCouncils.Add(tribalCouncil);
                    }

                    episodes.Add(episode);

                    currentDay += 1;
                    currentEpisode += 1;
                }
            }
            var season = new SurvivorSeason
            {
                Episodes = episodes.OrderBy(x => x.EpisodeNumber).ToList(),
                Castaways = castaways.OrderBy(x => x.Placement).ToList(),
                Winner = winner
            };
            var response = new SimulationResponse<SurvivorSeason>
            {
                Season = season
            };
            return response;
        }

        private void DetermineImmunity(List<Castaway> castaways, List<Tribe> tribes, SurvivorPhase currentPhase)
        {
            foreach (var castaway in castaways)
            {
                castaway.IsImmune = false;
            }
            foreach (var tribe in tribes)
            {
                tribe.IsImmune = false;
            }
            switch (currentPhase)
            {
                case SurvivorPhase.Tribal:
                    var immuneTribe = _commonHelper.GetRandomElement(tribes);
                    immuneTribe.IsImmune = true;
                    foreach (var castaway in castaways.Where(x => immuneTribe.Castaways.Contains(x.Name)))
                    {
                        castaway.IsImmune = true;
                    }
                    break;
                case SurvivorPhase.Individual:
                    var immuneCastaway = _commonHelper.GetRandomElement(castaways);
                    immuneCastaway.IsImmune = true;
                    break;
            }
        }

        private void DetermineTribes(int currentDay, List<Castaway> castaways, List<Tribe> tribes, TribeSwapType swapType = TribeSwapType.RandomEven)
        {
            foreach (var castaway in castaways)
            {
                castaway.Tribe = null;
            }
            foreach (var tribe in tribes)
            {
                tribe.Castaways.Clear();
            }

            var castawaysWithoutTribe = new List<string>();
            castawaysWithoutTribe.AddRange(castaways.Select(x => x.Name));

            while (castawaysWithoutTribe.Any())
            {
                var castaway = _commonHelper.GetRandomElement(castaways.Where(x => castawaysWithoutTribe.Contains(x.Name)).ToList());

                var smallestTribeCount = tribes.Min(x => x.Castaways.Count);
                var tribesWithLeastMembers = tribes
                    .Where(x => x.Castaways.Count.Equals(smallestTribeCount))
                    .ToList();
                var tribe = _commonHelper.GetRandomElement(tribesWithLeastMembers);

                castaway.Tribe = tribe.Name;
                tribe.Castaways.Add(castaway.Name);
                castaway.TribeHistory.Add(currentDay, tribe.Name);
                castawaysWithoutTribe.Remove(castaway.Name);
            }
        }
    }
}
