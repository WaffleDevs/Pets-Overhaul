﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using PetsOverhaul.Buffs.TownPetBuffs;

namespace PetsOverhaul.TownPets
{
    public class OldSlime : TownPet
    {
        public override void PostUpdateBuffs()
        {
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                if (Player.Distance(Main.npc[i].Center) < auraRange && Main.npc[i].type == NPCID.TownSlimeOld && Main.npc[i].active == true)
                {
                    Player.AddBuff(ModContent.BuffType<TownPetOld>(), 2);
                    break;
                }
            }
        }
        public override void PostUpdateEquips()
        {
            if (Player.HasBuff(ModContent.BuffType<TownPetOld>()))
                Player.statDefense += oldDef;
        }
        public override void ModifyHurt(ref Player.HurtModifiers modifiers)
        {
            modifiers.Knockback *= oldKbResist;
        }
    }
}