﻿using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using PetsOverhaul.TownPets;

namespace PetsOverhaul.Buffs.TownPetBuffs
{
    public class TownPetBunny : ModBuff
    {
        public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
        {
            
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                if (Main.npc[i].type == NPCID.TownBunny && Main.LocalPlayer.Distance(Main.npc[i].Center) < Main.LocalPlayer.GetModPlayer<TownPet>().auraRange)
                {
                    buffName = Lang.GetBuffName(ModContent.BuffType<TownPetBunny>()).Replace("<Name>", Main.npc[i].FullName);
                    break;
                }
                else
                {
                    buffName = "Bunny Aura";
                }
            }
            tip = Lang.GetBuffDescription(ModContent.BuffType<TownPetBunny>()).Replace("<BunnyJump>", Main.LocalPlayer.GetModPlayer<TownPet>().bunnyJump.ToString());
            rare = 0;
        }
    }
}
