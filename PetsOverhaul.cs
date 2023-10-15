using Microsoft.Xna.Framework;

using System;
using System.Collections.Generic;


using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using PetsOverhaul.Systems;
using System.Linq;
using PetsOverhaul.ModSupport;

namespace PetsOverhaul
{
    public class PetsOverhaul : Mod
    {
        public override void PostSetupContent()
        {
            ModManager.LoadMods();
        }
    }
}