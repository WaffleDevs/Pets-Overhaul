using Terraria;
using Terraria.ID;
using PetsOverhaul.Systems;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.Localization;

namespace PetsOverhaul.PetEffects.Vanilla
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
                    Player.GetAttackSpeed<SummonMeleeSpeedDamageClass>() += Player.maxMinions * whipSpeed / 4;
                    Player.whipRangeMultiplier += Player.maxMinions * whipRange / 4;
                }
            }
        }
    }
    sealed public class CursedSaplingItem : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation) => entity.type == ItemID.CursedSapling;

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            CursedSapling cursedSapling = Main.LocalPlayer.GetModPlayer<CursedSapling>();
            tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.CursedSapling")
                        .Replace("<minionSlot>", cursedSapling.maxMinion.ToString())
                        .Replace("<dmg>", (cursedSapling.pumpkinWeaponDmg * 100).ToString())
                        .Replace("<ravenDmg>", (cursedSapling.ravenDmg * 100).ToString())
                        .Replace("<whipRange>", (cursedSapling.whipRange * 100).ToString())
                        .Replace("<whipSpeed>", (cursedSapling.whipSpeed * 100).ToString())
                        ));
        }
    }
}
