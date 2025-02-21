﻿using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using PetsOverhaul.Buffs.TownPetBuffs;

namespace PetsOverhaul.TownPets
{
    public class Cat : TownPet
    {
        public override void PostUpdateBuffs()
        {
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                if (Player.Distance(Main.npc[i].Center) < auraRange && Main.npc[i].type == NPCID.TownCat && Main.npc[i].active == true)
                {
                    Player.AddBuff(ModContent.BuffType<TownPetCat>(), 2);
                    break;
                }
            }
        }
        public override void PostUpdateEquips()
        {
            if (Player.HasBuff(ModContent.BuffType<TownPetCat>()))
            Player.moveSpeed *= catSpeed;
        }
    }
}