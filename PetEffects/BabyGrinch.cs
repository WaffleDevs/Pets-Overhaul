using Terraria;
using Terraria.ID;
using PetsOverhaul.Systems;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace PetsOverhaul.PetEffects
{
    sealed public class BabyGrinch : ModPlayer
    {
        public float winterDmg = 0.1f;
        public int winterCrit = 10;
        public float grinchSlow = 1f;
        public int grinchRange = 400;
        GlobalPet Pet { get => Player.GetModPlayer<GlobalPet>(); }
        public override void ModifyWeaponDamage(Item item, ref StatModifier damage)
        {
            if (Pet.PetInUseWithSwapCd(ItemID.BabyGrinchMischiefWhistle))
            {
                if (item.netID == ItemID.ChristmasTreeSword || item.netID == ItemID.Razorpine || item.netID == ItemID.ElfMelter || item.netID == ItemID.ChainGun || item.netID == ItemID.BlizzardStaff || item.netID == ItemID.SnowmanCannon || item.netID == ItemID.NorthPole)
                    damage += winterDmg;
            }
        }
        public override void ModifyWeaponCrit(Item item, ref float crit)
        {
            if (Pet.PetInUseWithSwapCd(ItemID.BabyGrinchMischiefWhistle))
            {
                if (item.netID == ItemID.ChristmasTreeSword || item.netID == ItemID.Razorpine || item.netID == ItemID.ElfMelter || item.netID == ItemID.ChainGun || item.netID == ItemID.BlizzardStaff || item.netID == ItemID.SnowmanCannon || item.netID == ItemID.NorthPole)
                    crit += winterCrit;
            }
        }
        public override void PostUpdateEquips()
        {
            if (Pet.PetInUseWithSwapCd(ItemID.BabyGrinchMischiefWhistle))
            {
                Player.resistCold = true;
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC npc = Main.npc[i];
                    if (npc.active && Player.Distance(npc.Center) < grinchRange)
                    {
                        npc.GetGlobalNPC<NpcPet>().AddSlow(NpcPet.SlowId.Grinch, grinchSlow, 1);
                    }
                }
            }
        }
    }
}

