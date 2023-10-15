using Terraria;
using Terraria.ID;
using PetsOverhaul.Systems;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.Localization;

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
    sealed public class TikiTotem : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation) => entity.type == ItemID.TikiTotem;

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            TikiSpirit tikiSpirit = ModContent.GetInstance<TikiSpirit>();
            tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.TikiTotem")
                       .Replace("<atkSpdToDmg>", (tikiSpirit.atkSpdToDmgConversion * 100).ToString())
                       .Replace("<atkSpdToRange>", (tikiSpirit.atkSpdToRangeConversion * 100).ToString())
                       .Replace("<nonWhipCrit>", tikiSpirit.nonWhipCrit.ToString())
                       .Replace("<whipCrit>", tikiSpirit.whipCritBonus.ToString())
                       ));
        }
    }
}
