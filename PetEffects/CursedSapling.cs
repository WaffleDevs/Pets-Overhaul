using Terraria;
using Terraria.ID;
using PetsOverhaul.Systems;
using Terraria.ModLoader;

namespace PetsOverhaul.PetEffects
{
    sealed public class CursedSapling : ModPlayer
    {
        GlobalPet Pet { get => Player.GetModPlayer<GlobalPet>(); }
        public float whipSpeed = 0.025f;
        public float whipRange = 0.04f;
        public float pumpkinWeaponDmg = 0.1f;
        public float ravenDmg = 0.25f;
        public int maxMinion = 1;
        public override void ModifyWeaponDamage(Item item, ref StatModifier damage)
        {
            if (Pet.PetInUseWithSwapCd(ItemID.CursedSapling))
            {
                if (item.netID == ItemID.StakeLauncher || item.netID == ItemID.TheHorsemansBlade || item.netID == ItemID.BatScepter || item.netID == ItemID.CandyCornRifle || item.netID == ItemID.ScytheWhip || item.netID == ItemID.JackOLanternLauncher)
                {
                    damage += pumpkinWeaponDmg;
                }
                if (item.netID == ItemID.RavenStaff)
                {
                    damage += ravenDmg;
                }
            }
        }
        public override void PostUpdateEquips()
        {
            if (Pet.PetInUseWithSwapCd(ItemID.CursedSapling))
            {
                Player.maxMinions += maxMinion;
                if (Player.HeldItem.type == ItemID.ScytheWhip)
                {
                    Player.GetAttackSpeed<SummonMeleeSpeedDamageClass>() += Player.maxMinions * whipSpeed;
                    Player.whipRangeMultiplier += Player.maxMinions * whipRange;
                }
                else if (Player.HeldItem.CountsAsClass<SummonMeleeSpeedDamageClass>())
                {
                    Player.GetAttackSpeed<SummonMeleeSpeedDamageClass>() += Player.maxMinions * whipSpeed/4;
                    Player.whipRangeMultiplier += Player.maxMinions * whipRange/4;
                }
            }
        }
    }
}
