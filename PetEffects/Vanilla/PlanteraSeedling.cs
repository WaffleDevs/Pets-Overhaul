using Terraria;
using Terraria.ID;
using PetsOverhaul.Systems;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.Localization;

namespace PetsOverhaul.PetEffects.Vanilla
{
    sealed public class PlanteraSeedling : ModPlayer
    {
        public float secondMultiplier = 0f;
        GlobalPet Pet { get => Player.GetModPlayer<GlobalPet>(); }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (Pet.PetInUseWithSwapCd(ItemID.PlanteraPetItem) && modifiers.DamageType.CountsAsClass<RangedDamageClass>())
            {
                float multiplyDamage = 0;
                multiplyDamage += 2 / (target.life * 1f / target.lifeMax + 1);
                multiplyDamage += (2 / (target.life * 1f / target.lifeMax + 1) - 1) * secondMultiplier;
                modifiers.FinalDamage *= multiplyDamage;
            }
        }
    }
    sealed public class PlanteraPetItem : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation) => entity.type == ItemID.PlanteraPetItem;

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            PlanteraSeedling planteraSeedling = ModContent.GetInstance<PlanteraSeedling>();
            tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.PlanteraPetItem")
                        .Replace("<maxAmount>", (planteraSeedling.secondMultiplier * 100 + 100).ToString())
                        ));
        }
    }
}
