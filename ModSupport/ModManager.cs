using System;
using System.Collections.Generic;
using System.Linq;

using PetsOverhaul.PetEffects.Vanilla;
using PetsOverhaul.Systems;

using Terraria;
using Terraria.ModLoader;

namespace PetsOverhaul.ModSupport
{
    public static class ModManager
    {
        //public static CalamitySupport Calamity;
        public static ThoriumSupport ThoriumMod;
        public static void LoadMods()
        {
            //Calamity = new CalamitySupport();
            //Calamity.InitializeMod();
            ThoriumMod = new ThoriumSupport();
            ThoriumMod.InitializeMod();
        }
    }
}