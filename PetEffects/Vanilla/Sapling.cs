using Terraria;
using Terraria.ID;
using PetsOverhaul.Systems;
using Terraria.ModLoader;
using Terraria.DataStructures;
using System.Collections.Generic;
using Terraria.Localization;

namespace PetsOverhaul.PetEffects.Vanilla
{
    sealed public class Sapling : ModPlayer
    {
        public float planteraLifesteal = 0.033f;
        public float regularLifesteal = 0.012f;
        GlobalPet Pet { get => Player.GetModPlayer<GlobalPet>(); }
        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Pet.PetInUseWithSwapCd(ItemID.Seedling) && Pet.LifestealCheck(target))
            {
                if (proj.GetGlobalProjectile<ProjectileSourceChecks>().isPlanteraProjectile)
                {
                    Pet.Lifesteal(damageDone, planteraLifesteal);
                }
                else
                {
                    Pet.Lifesteal(damageDone, regularLifesteal);
                }
            }
        }
        public override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Pet.PetInUseWithSwapCd(ItemID.Seedling) && Pet.LifestealCheck(target))
            {
                if (item.type == ItemID.Seedler || item.type == ItemID.TheAxe)
                {
                    Pet.Lifesteal(damageDone, planteraLifesteal);
                }
                else
                {
                    Pet.Lifesteal(damageDone, regularLifesteal);
                }
            }
        }
    }
    sealed public class Seedling : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation) => entity.type == ItemID.Seedling;

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            Sapling sapling = Main.LocalPlayer.GetModPlayer<Sapling>();
            tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.Seedling")
                        .Replace("<lifesteal>", (sapling.regularLifesteal * 100).ToString())
                        .Replace("<planteraSteal>", (sapling.planteraLifesteal * 100).ToString())
                        ));
        }
    }
}
