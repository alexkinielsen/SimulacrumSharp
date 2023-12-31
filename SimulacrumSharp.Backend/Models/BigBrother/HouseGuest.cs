﻿using SimulacrumSharp.Backend.Models.Enums;

namespace SimulacrumSharp.Backend.Models.BigBrother
{
    public class HouseGuest
    {
        public string Name { get; set; }
        public GenderIdentity GenderIdentity { get; set; }
        public int MoveInGroup { get; set; }
        public string Finish { get; set; }
        public int? Placement { get; set; }
        public bool IsNominee { get; set; }
        public bool IsExiled { get; set; }
        public bool IsHeadOfHousehold { get; set; }
    }
}