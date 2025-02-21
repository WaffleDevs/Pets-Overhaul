﻿using Terraria;
using Terraria.ID;
using PetsOverhaul.TownPets;
using Terraria.ModLoader;

namespace PetsOverhaul.Buffs.TownPetBuffs
{
    public class TownPetClumsy : ModBuff
    {
        public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
        {
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                if (Main.LocalPlayer.Distance(Main.npc[i].Center) < Main.LocalPlayer.GetModPlayer<TownPet>().auraRange && Main.npc[i].type == NPCID.TownSlimePurple)
                {
                    buffName = Lang.GetBuffName(ModContent.BuffType<TownPetClumsy>()).Replace("<Name>", Main.npc[i].FullName);
                    break;
                }
                else
                {
                    buffName = "Unlucky Aura";
                }
            }
            tip = Lang.GetBuffDescription(ModContent.BuffType<TownPetClumsy>()).Replace("<ClumsyLuck>", Main.LocalPlayer.GetModPlayer<TownPet>().clumsyLuck.ToString());
            rare = 0;
        }
    }
}
