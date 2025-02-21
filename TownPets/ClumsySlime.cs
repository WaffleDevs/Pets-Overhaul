﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using PetsOverhaul.Buffs.TownPetBuffs;

namespace PetsOverhaul.TownPets
{
    public class ClumsySlime : TownPet
    {
        public override void PostUpdateBuffs()
        {
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                if (Player.Distance(Main.npc[i].Center) < auraRange && Main.npc[i].type == NPCID.TownSlimePurple && Main.npc[i].active == true)
                {
                    Player.AddBuff(ModContent.BuffType<TownPetClumsy>(), 2);
                    break;
                }
            }
        }
        public override void ModifyLuck(ref float luck)
        {
            if (Player.HasBuff(ModContent.BuffType<TownPetClumsy>()))
                luck += clumsyLuck;
        }
    }
}