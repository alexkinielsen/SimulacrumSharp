using SimulacrumSharp.Backend.Helpers;
using SimulacrumSharp.Backend.Helpers.Interfaces;
using System.Collections;
using SimulacrumSharp.Backend.Models.Enums;
using SimulacrumSharp.Backend.Models.BigBrother;
using SimulacrumSharp.Backend.Models.BigBrother.Events;
using SimulacrumSharp.Backend.Models.ServiceModels;
using SimulacrumSharp.Backend.Models.BigBrother.Events.Base;
using SimulacrumSharp.Backend.Services.Interfaces;

namespace SimulacrumSharp.Backend.Services.Simulation.BigBrother
{
    public class BigBrotherSimulationService : ISimulationService<BigBrotherSeason>
    {
        private readonly ICommonHelper _commonHelper;

        public BigBrotherSimulationService(
            ICommonHelper commonHelper)
        {
            _commonHelper = commonHelper;
        }

        public SimulationResponse<BigBrotherSeason> Simulate(SimulationRequest<BigBrotherSeason> request)
        {
            var houseGuests = new List<HouseGuest>
            {
                new HouseGuest { Name = "America", GenderIdentity = GenderIdentity.FEMME },
                new HouseGuest { Name = "Blue", GenderIdentity = GenderIdentity.FEMME },
                new HouseGuest { Name = "Bowie", GenderIdentity = GenderIdentity.FEMME },
                new HouseGuest { Name = "Cameron", GenderIdentity = GenderIdentity.MASC },
                new HouseGuest { Name = "Cirie", GenderIdentity = GenderIdentity.FEMME },
                new HouseGuest { Name = "Cory", GenderIdentity = GenderIdentity.MASC },
                new HouseGuest { Name = "Felicia", GenderIdentity = GenderIdentity.FEMME },
                new HouseGuest { Name = "Hisam", GenderIdentity = GenderIdentity.MASC },
                new HouseGuest { Name = "Izzy", GenderIdentity = GenderIdentity.FEMME },
                new HouseGuest { Name = "Jag", GenderIdentity = GenderIdentity.MASC },
                new HouseGuest { Name = "Jared", GenderIdentity = GenderIdentity.MASC },
                new HouseGuest { Name = "Kirsten", GenderIdentity = GenderIdentity.FEMME },
                new HouseGuest { Name = "Luke", GenderIdentity = GenderIdentity.MASC },
                new HouseGuest { Name = "Matt", GenderIdentity = GenderIdentity.MASC },
                new HouseGuest { Name = "Mecole", GenderIdentity = GenderIdentity.FEMME },
                new HouseGuest { Name = "Red", GenderIdentity = GenderIdentity.MASC },
                new HouseGuest { Name = "Reilly", GenderIdentity = GenderIdentity.FEMME },
            };

            var initialHouseGuests = new List<HouseGuest>();
            initialHouseGuests.AddRange(houseGuests
                .Except(houseGuests
                    .Where(x => x.Name.Equals("Cirie"))));

            var additionalHouseGuests = new List<HouseGuest>();
            additionalHouseGuests.AddRange(houseGuests
                .Except(initialHouseGuests));

            var castSize = houseGuests.Count();
            var startingDay = DayOfWeek.Wednesday;
            var dayCount = 100;
            var episodeCount = 35;
            var jurySize = 9;
            var finalHouseGuestSize = 2;
            var currentHoH = (HouseGuest)null;

            var activeTwist = BigBrotherTwist.BB_MULTIVERSE;

            var currentEpisode = 0;
            var activeEpisode = (BigBrotherEpisode)null;
            var currentDay = 1;
            var houseGuestsRemaining = new List<HouseGuest>();
            houseGuestsRemaining.AddRange(initialHouseGuests);

            var winner = string.Empty;

            var episodes = new List<BigBrotherEpisode>();
            while (currentDay <= dayCount)
            {
                if (currentDay.Equals(1))
                {
                    SwitchToNextEpisode(ref currentEpisode, ref activeEpisode, currentDay, houseGuestsRemaining, episodes);
                    var day1 = new BigBrotherDay
                    {
                        DayNumber = currentDay,
                        DayOfWeek = GetDayOfWeek(startingDay, currentDay),
                        HouseGuestsCompeting = new List<string>(),
                        Events = new ArrayList()
                    };
                    var dailyEvents = new List<BigBrotherEvent>();
                    day1.HouseGuestsCompeting.AddRange(initialHouseGuests.Select(x => x.Name));

                    var multiverseMoveIn = GetMultiverseMoveInEvent(houseGuests, initialHouseGuests);
                    dailyEvents.Add(multiverseMoveIn);

                    var additionalHouseGuest = GetNewHouseGuestMovesInEvent(houseGuests, initialHouseGuests, additionalHouseGuests, houseGuestsRemaining);
                    dailyEvents.Add(additionalHouseGuest);

                    var netherRegionReturn = GetNetherRegionReturnEvent(houseGuests);
                    dailyEvents.Add(netherRegionReturn);

                    day1.Events.AddRange(dailyEvents);
                    activeEpisode.Days.Add(day1);
                }
                else if (currentDay.Equals(2))
                {
                    SwitchToNextEpisode(ref currentEpisode, ref activeEpisode, currentDay, houseGuestsRemaining, episodes);
                    var day = new BigBrotherDay
                    {
                        DayNumber = currentDay,
                        DayOfWeek = GetDayOfWeek(startingDay, currentDay),
                        HouseGuestsCompeting = new List<string>(),
                        Events = new ArrayList()
                    };
                    var dailyEvents = new List<BigBrotherEvent>();
                    day.HouseGuestsCompeting.AddRange(houseGuestsRemaining.Select(x => x.Name));
                    HeadOfHouseholdCompetition headOfHouseholdCompetition = GetHeadOfHouseholdCompetition(currentHoH, currentDay, houseGuestsRemaining);
                    dailyEvents.Add(headOfHouseholdCompetition);

                    day.Events.AddRange(dailyEvents);
                    activeEpisode.Days.Add(day);
                }
                else if (currentDay.Equals(dayCount)) //Finale
                {
                    var evictedHouseGuest = _commonHelper.GetRandomElement(houseGuestsRemaining);
                    evictedHouseGuest.Placement = houseGuestsRemaining.Count;
                    evictedHouseGuest.Finish += $"Evicted - Day {currentDay} ";
                    houseGuestsRemaining.Remove(evictedHouseGuest);

                    var runnerUp = _commonHelper.GetRandomElement(houseGuestsRemaining);
                    runnerUp.Placement = houseGuestsRemaining.Count;
                    runnerUp.Finish += "Runner-Up ";
                    houseGuestsRemaining.Remove(runnerUp);

                    var winningHouseGuest = houseGuestsRemaining.Single();
                    winner = winningHouseGuest.Name;
                    winningHouseGuest.Finish += "Winner ";
                    winningHouseGuest.Placement = houseGuestsRemaining.Count;
                }
                else
                {
                    var day = new BigBrotherDay
                    {
                        DayNumber = currentDay,
                        DayOfWeek = GetDayOfWeek(startingDay, currentDay),
                        HouseGuestsCompeting = new List<string>(),
                        Events = new ArrayList()
                    };
                    var dailyEvents = new List<BigBrotherEvent>();
                    day.HouseGuestsCompeting.AddRange(houseGuestsRemaining.Select(x => x.Name));
                    var dayOfWeek = GetDayOfWeek(startingDay, currentDay);
                    switch (day.DayOfWeek)
                    {
                        case DayOfWeek.Thursday:
                            var evictedHouseGuest = _commonHelper.GetRandomElement(houseGuestsRemaining);
                            evictedHouseGuest.Finish += $"Evicted - Day {currentDay} ";
                            evictedHouseGuest.Placement = houseGuestsRemaining.Count;
                            houseGuestsRemaining.Remove(evictedHouseGuest);
                            if (currentDay.Equals(51))
                            {
                                var returnee = _commonHelper.GetRandomElement(houseGuests.Except(houseGuestsRemaining));
                                returnee.Finish += $"Returned - Day {currentDay} ";
                                foreach (var houseGuest in houseGuests)
                                {
                                    if (houseGuest.Placement.HasValue && houseGuest.Placement.Value < returnee.Placement.Value)
                                    {
                                        houseGuest.Placement++;
                                    }
                                }
                                returnee.Placement = null;

                                houseGuestsRemaining.Add(returnee);
                            }
                            if (currentDay.Equals(79))
                            {
                                var secondEvictedHouseGuest = _commonHelper.GetRandomElement(houseGuestsRemaining);
                                secondEvictedHouseGuest.Finish += $"Evicted - Day {currentDay} - Double Eviction ";
                                secondEvictedHouseGuest.Placement = houseGuestsRemaining.Count;
                                houseGuestsRemaining.Remove(secondEvictedHouseGuest);
                            }
                            break;
                        case DayOfWeek.Friday:
                            if (!currentDay.Equals(3))
                            {
                                SwitchToNextEpisode(ref currentEpisode, ref activeEpisode, currentDay, houseGuestsRemaining, episodes);
                            }
                            break;
                        case DayOfWeek.Saturday:
                            SwitchToNextEpisode(ref currentEpisode, ref activeEpisode, currentDay, houseGuestsRemaining, episodes);
                            break;
                        case DayOfWeek.Sunday:
                            break;
                        case DayOfWeek.Monday:
                            break;
                        case DayOfWeek.Tuesday:
                            if (currentDay.Equals(91))
                            {
                                evictedHouseGuest = _commonHelper.GetRandomElement(houseGuestsRemaining);
                                evictedHouseGuest.Finish += $"Evicted - Day {currentDay} ";
                                evictedHouseGuest.Placement = houseGuestsRemaining.Count;
                                houseGuestsRemaining.Remove(evictedHouseGuest);
                            }
                            SwitchToNextEpisode(ref currentEpisode, ref activeEpisode, currentDay, houseGuestsRemaining, episodes);
                            break;
                        case DayOfWeek.Wednesday:
                            break;

                    }

                    day.Events.AddRange(dailyEvents);
                    activeEpisode.Days.Add(day);
                }
                currentDay++;
            }
            episodes.Add(activeEpisode);

            var season = new BigBrotherSeason
            {
                Episodes = episodes.OrderBy(x => x.EpisodeNumber).ToList(),
                HouseGuests = houseGuests.OrderBy(x => x.Placement).ToList(),
                Winner = winner
            };
            var response = new SimulationResponse<BigBrotherSeason>
            {
                Season = season
            };
            return response;

        }

        private void SwitchToNextEpisode(ref int currentEpisode, ref BigBrotherEpisode activeEpisode, int currentDay, List<HouseGuest> houseGuestsRemaining, List<BigBrotherEpisode> episodes)
        {
            currentEpisode++;
            if (activeEpisode != null)
            {
                episodes.Add(activeEpisode);
            }

            var episode = new BigBrotherEpisode
            {
                EpisodeNumber = currentEpisode,
                Name = $"Episode {currentEpisode} - Day {currentDay}",
                HouseGuestsCompeting = new List<string>(),
                Days = new List<BigBrotherDay>()
            };
            activeEpisode = episode;
            episode.HouseGuestsCompeting.AddRange(houseGuestsRemaining.Select(x => x.Name));
        }

        private HeadOfHouseholdCompetition GetHeadOfHouseholdCompetition(HouseGuest currentHoH, int currentDay, List<HouseGuest> houseGuestsRemaining)
        {
            if (currentHoH != null)
            {
                currentHoH.IsHeadOfHousehold = false;
            }

            var headOfHouseholdCompetition = new HeadOfHouseholdCompetition
            {
                Name = $"Head of Household - Day {currentDay}",
                Sequence = 10,
                EventType = BigBrotherEventType.HEAD_OF_HOUSEHOLD_COMPETITION,
                HouseGuestsParticipating = new List<string>(),
                Winners = new List<string>()
            };
            var houseGuestsParticipating = houseGuestsRemaining
                .Where(x => !x.Equals(currentHoH))
                .Where(x => !x.IsNominee);
            headOfHouseholdCompetition.HouseGuestsParticipating.AddRange(houseGuestsParticipating.Select(x => x.Name));

            var headOfHousehold = _commonHelper.GetRandomElement(houseGuestsParticipating);
            headOfHousehold.IsHeadOfHousehold = true;
            currentHoH = headOfHousehold;
            headOfHouseholdCompetition.Winners.Add(headOfHousehold.Name);

            var participantString = _commonHelper.FormatListOfNamesString(headOfHouseholdCompetition.HouseGuestsParticipating.ToList());
            var winnersString = _commonHelper.FormatListOfNamesString(headOfHouseholdCompetition.Winners.ToList());
            var hohNote = $"{participantString} participated in a Head of Household competition. {winnersString} won out, becoming the new Head of Household";
            headOfHouseholdCompetition.EventNotes.Add(hohNote);
            return headOfHouseholdCompetition;
        }

        private DayOfWeek GetDayOfWeek(DayOfWeek startingDay, int currentDay)
        {
            return (DayOfWeek)((currentDay + ((int)startingDay - 1)) % 7);
        }

        private ExileReturn GetNetherRegionReturnEvent(List<HouseGuest> houseGuests)
        {
            var netherRegionReturn = new ExileReturn
            {
                Name = "Nether Region Return",
                Sequence = 30,
                EventType = BigBrotherEventType.RETURN_FROM_EXILE,
                HouseGuestsParticipating = new List<string>()
            };

            var exiledHouseGuests = houseGuests.Where(x => x.IsExiled);
            netherRegionReturn.HouseGuestsParticipating.AddRange(exiledHouseGuests.Select(x => x.Name));

            var returningFromNetherRegionString = _commonHelper.FormatListOfNamesString(netherRegionReturn.HouseGuestsParticipating.ToList());

            var returnNote = $"Some time after the move-in challenge, {returningFromNetherRegionString} made their return from the Nether Region and rejoined the others, with a warning from the Nether Region.";
            netherRegionReturn.EventNotes.Add(returnNote);

            foreach (var houseGuest in exiledHouseGuests)
            {
                houseGuest.IsExiled = false;
            }

            return netherRegionReturn;
        }

        private NewHouseGuestMovesIn GetNewHouseGuestMovesInEvent(List<HouseGuest> houseGuests, List<HouseGuest> initialHouseGuests, List<HouseGuest> additionalHouseGuests, List<HouseGuest> houseGuestsRemaining)
        {
            var additionalHouseGuest = new NewHouseGuestMovesIn
            {
                Name = "The 17th HouseGuest",
                Sequence = 20,
                EventType = BigBrotherEventType.NEW_HOUSEGUEST_MOVES_IN,
                HouseGuestsParticipating = new List<string>()
            };
            var initialHouseGuestNames = initialHouseGuests.Where(x => !x.IsExiled).Select(x => x.Name);
            var initialHouseGuestString = _commonHelper.FormatListOfNamesString(initialHouseGuestNames.ToList());
            additionalHouseGuest.HouseGuestsParticipating.AddRange(initialHouseGuestNames);

            var additionalHouseGuestNames = additionalHouseGuests.Select(x => x.Name);
            var additionalHouseGuestString = _commonHelper.FormatListOfNamesString(additionalHouseGuestNames.ToList());
            additionalHouseGuest.HouseGuestsParticipating.AddRange(additionalHouseGuestNames);

            houseGuestsRemaining.AddRange(additionalHouseGuests);

            var additionalHouseGuestsNote = $"After returning inside, {initialHouseGuestString} found {additionalHouseGuestString} in the kitchen pouring champagne, officially bringing the total HouseGuests to a whopping {houseGuests.Count}!";
            additionalHouseGuest.EventNotes.Add(additionalHouseGuestsNote);
            return additionalHouseGuest;
        }

        private MultiverseMoveIn GetMultiverseMoveInEvent(List<HouseGuest> houseGuests, List<HouseGuest> initialHouseGuests)
        {
            var multiverseMoveIn = new MultiverseMoveIn
            {
                Name = "Multiverse Move In",
                Sequence = 10,
                EventType = BigBrotherEventType.LIVE_MOVE_IN,
                HouseGuestsParticipating = new List<string>(),
                MoveInGroups = new Dictionary<int, List<string>>(),
                EventNotes = new List<string>()
            };
            multiverseMoveIn.HouseGuestsParticipating.AddRange(initialHouseGuests.Select(x => x.Name));

            var moveInGroups = new List<List<string>>();
            var houseGuestsToMoveIn = houseGuests
                .Where(x => multiverseMoveIn.HouseGuestsParticipating.Contains(x.Name))
                .ToList();

            multiverseMoveIn.ChallengeStations = new Dictionary<string, List<string>>
                    {
                        { "Scramble-Verse", new List<string>() },
                        { "Humili-Verse", new List<string>() },
                        { "Comic-Verse", new List<string>() },
                        { "Scary-Verse", new List<string>() }
                    };

            var groupNumber = 0;
            while (houseGuestsToMoveIn.Count > 0)
            {
                groupNumber++;
                multiverseMoveIn.MoveInGroups.Add(groupNumber, new List<string>());

                while (multiverseMoveIn.MoveInGroups[groupNumber].Count < 4)
                {
                    var genderToGet = multiverseMoveIn.MoveInGroups[groupNumber].Count % 2 == 0
                        ? GenderIdentity.FEMME
                        : GenderIdentity.MASC;
                    var houseGuest = AddHouseGuestToMoveInGroup(groupNumber, houseGuestsToMoveIn, genderToGet);
                    if (houseGuest != null)
                    {
                        multiverseMoveIn.MoveInGroups[groupNumber].Add(houseGuest.Name);
                        houseGuest.MoveInGroup = groupNumber;
                        houseGuestsToMoveIn.Remove(houseGuest);
                    }
                    else
                    {
                        break;
                    }
                }
                var moveInGroupString = _commonHelper.FormatListOfNamesString(multiverseMoveIn.MoveInGroups[groupNumber]);
                var groupNote = $"During the live move in, {moveInGroupString} made up group #{groupNumber} and entered the house.";
                multiverseMoveIn.EventNotes.Add(groupNote);

                var challengeStations = multiverseMoveIn.ChallengeStations.Keys.ToList();
                foreach (var houseGuest in multiverseMoveIn.MoveInGroups[groupNumber])
                {
                    var selectedStation = _commonHelper.GetRandomElement(challengeStations);

                    challengeStations.Remove(selectedStation);
                    multiverseMoveIn.ChallengeStations[selectedStation].Add(houseGuest);

                    var stationNote = $"{houseGuest} saunters over and claims a spot at the {selectedStation} station.";
                    multiverseMoveIn.EventNotes.Add(stationNote);
                }
            }

            foreach (var station in multiverseMoveIn.ChallengeStations)
            {
                var challengeLoser = _commonHelper.GetRandomElement(station.Value);

                var nominee = houseGuests.Single(x => x.Name.Equals(challengeLoser));

                nominee.IsNominee = true;

                var stationParticipants = _commonHelper.FormatListOfNamesString(station.Value);
                var nomineeNote = string.Empty;

                if (station.Key.Equals("Scary-Verse"))
                {
                    nominee.IsExiled = true;
                    nomineeNote = $"At the {station.Key} station, {stationParticipants} faced off, but {challengeLoser} was the first one to let go, and they were dragged into the Nether Region, as well as became the fourth nominee for eviction.";
                }
                else
                {
                    nomineeNote = $"At the {station.Key} station, {stationParticipants} faced off, but {challengeLoser} was the last one to finish, and was nominated for eviction.";
                }

                multiverseMoveIn.EventNotes.Add(nomineeNote);
            }

            return multiverseMoveIn;
        }

        private HouseGuest AddHouseGuestToMoveInGroup(int groupNumber, IList<HouseGuest> houseGuestsToMoveIn, GenderIdentity gender)
        {
            var houseGuest = _commonHelper.GetRandomElement(houseGuestsToMoveIn
                .Where(x => x.GenderIdentity.Equals(gender)));
            if (houseGuest is null)
            {
                return null;
            }

            return houseGuest;
        }
    }
}
