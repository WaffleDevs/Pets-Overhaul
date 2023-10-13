using Terraria;
using Terraria.ID;
using PetsOverhaul.Systems;
using Terraria.ModLoader;

namespace PetsOverhaul.PetEffects
{
    sealed public class Spider : ModPlayer
    {
        public int venomFlatDmg = 13;
        public int poisonFlatDmg = 5;
        public float venomDmgMult = 1.15f;
        public float poisonDmgMult = 1.05f;
        public int venomCrit = 10;
        public int poisonCrit = 6;
        public float kbIncreaseVenom = 1.6f;
        public float kbIncreasePoison = 1.25f;
        public int poisonTime = 600;
        GlobalPet Pet { get => Player.GetModPlayer<GlobalPet>(); }
        public override void ModifyHitNPCWithItem(Item item, NPC target, ref NPC.HitModifiers modifiers)
        {
            if (Pet.PetInUseWithSwapCd(ItemID.SpiderEgg))
            {
                if (target.HasBuff(BuffID.Venom))
                {
                    modifiers.FinalDamage *= venomDmgMult;
                    modifiers.FlatBonusDamage += venomFlatDmg;
                    modifiers.Knockback *= kbIncreaseVenom;
                    if (item.crit+Player.GetTotalCritChance(modifiers.DamageType) + venomCrit >= 100)
                    {
                        modifiers.SetCrit();
                    }
                    else if (Main.rand.NextBool(item.crit + (int)Player.GetTotalCritChance(modifiers.DamageType) + venomCrit, 100))
                    {
                        modifiers.SetCrit();
                    }
                    else
                    {
                        modifiers.DisableCrit();
                    }
                }
                else if (target.HasBuff(BuffID.Poisoned))
                {
                    modifiers.FinalDamage *= poisonDmgMult;
                    modifiers.FlatBonusDamage += poisonFlatDmg;
                    modifiers.Knockback *= kbIncreasePoison;
                    if (item.crit + Player.GetTotalCritChance(modifiers.DamageType) + poisonCrit >= 100)
                    {
                        modifiers.SetCrit();
                    }
                    else if (Main.rand.NextBool(item.crit + (int)Player.GetTotalCritChance(modifiers.DamageType) + poisonCrit, 100))
                    {
                        modifiers.SetCrit();
                    }
                    else
                    {
                        modifiers.DisableCrit();
                    }
                }
                target.AddBuff(BuffID.Poisoned, poisonTime);
                if (target.buffImmune[BuffID.Venom] == false)
                {
                    target.buffImmune[BuffID.Poisoned] = false;
                }
            }
        }
        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref NPC.HitModifiers modifiers)
        {
            if (Pet.PetInUseWithSwapCd(ItemID.SpiderEgg))
            {
                if (target.HasBuff(BuffID.Venom))
                {
                    modifiers.FinalDamage *= venomDmgMult;
                    modifiers.FlatBonusDamage += venomFlatDmg;
                    modifiers.Knockback *= kbIncreaseVenom;
                    if (proj.CritChance + venomCrit >= 100)
                    {
                        modifiers.SetCrit();
                    }
                    else if (Main.rand.NextBool(proj.CritChance+ venomCrit, 100))
                    {
                        modifiers.SetCrit();
                    }
                    else
                    {
                        modifiers.DisableCrit();
                    }
                }
                else if (target.HasBuff(BuffID.Poisoned))
                {
                    modifiers.FinalDamage *= poisonDmgMult;
                    modifiers.FlatBonusDamage += poisonFlatDmg;
                    modifiers.Knockback *= kbIncreasePoison;
                    if (proj.CritChance + poisonCrit >= 100)
                    {
                        modifiers.SetCrit();
                    }
                    else if (Main.rand.NextBool(proj.CritChance + poisonCrit, 100))
                    {
                        modifiers.SetCrit();
                    }
                    else
                    {
                        modifiers.DisableCrit();
                    }
                }
                target.AddBuff(BuffID.Poisoned, poisonTime);
                if (target.buffImmune[BuffID.Venom] == false)
                {
                    target.buffImmune[BuffID.Poisoned] = false;
                }
            }
        }
    }
}
