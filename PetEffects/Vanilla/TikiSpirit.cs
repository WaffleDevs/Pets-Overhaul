using Terraria;
using Terraria.ID;
using PetsOverhaul.Systems;
using Terraria.ModLoader;

namespace PetsOverhaul.PetEffects.Vanilla
{
    sealed public class TikiSpirit : ModPlayer
    {
        public int whipCritBonus = 10;
        public int nonWhipCrit = 8;
        public float atkSpdToDmgConversion = 0.3f;
        public float atkSpdToRangeConversion = 0.15f;
        GlobalPet Pet { get => Player.GetModPlayer<GlobalPet>(); }
        public override void PostUpdateEquips()
        {
            if (Pet.PetInUseWithSwapCd(ItemID.TikiTotem))
            {
                if (Player.GetTotalAttackSpeed<SummonMeleeSpeedDamageClass>() - 1 > 0)
                {
                    Player.GetDamage<SummonDamageClass>() += (Player.GetTotalAttackSpeed<SummonMeleeSpeedDamageClass>() - 1) * atkSpdToDmgConversion;
                    Player.whipRangeMultiplier += (Player.GetTotalAttackSpeed<SummonMeleeSpeedDamageClass>() - 1) * atkSpdToRangeConversion;
                }
            }
        }
        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref NPC.HitModifiers modifiers)
        {
            if (Pet.PetInUseWithSwapCd(ItemID.TikiTotem))
            {
                if (ProjectileID.Sets.IsAWhip[proj.type])
                {
                    if (proj.CritChance + whipCritBonus >= 100)
                    {
                        modifiers.SetCrit();
                    }
                    else if (Main.rand.NextBool(proj.CritChance + whipCritBonus, 100))
                    {
                        modifiers.SetCrit();
                    }
                    else
                    {
                        modifiers.DisableCrit();
                    }
                }
                else if (proj.IsMinionOrSentryRelated)
                {
                    if (proj.CritChance - nonWhipCrit >= 100)
                    {
                        modifiers.SetCrit();
                    }
                    else if (Main.rand.NextBool(proj.CritChance - nonWhipCrit, 100))
                    {
                        modifiers.SetCrit();
                    }
                    else
                    {
                        modifiers.DisableCrit();
                    }
                }
            }
        }
    }
}
