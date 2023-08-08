﻿using SimulacrumSharp.SimulationAPI.Models.Enums;

namespace SimulacrumSharp.SimulationAPI.Models.BigBrother.Events.Base
{
    public abstract class BigBrotherEvent
    {
        public string Name { get; set; }
        public int Sequence { get; set; }
        public BigBrotherEventType EventType { get; set; }
        public IList<string> HouseGuestsParticipating { get; set; } = new List<string>();
        public IList<string> EventNotes { get; set; } = new List<string>();
    }
}