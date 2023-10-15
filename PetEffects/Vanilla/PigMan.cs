using Terraria;
using Terraria.ID;
using PetsOverhaul.Systems;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.Localization;

namespace PetsOverhaul.PetEffects.Vanilla
{
    sealed public class Pigman : ModPlayer
    {
        public int foodChance = 15;
        public int potionChance = 10;
        public int shieldCooldown = 420;
        public int tier1Shield = 10;
        public int tier2Shield = 15;
        public int tier3Shield = 20;
        public int shieldTime = 1800;
    }
    sealed public class PigPetItem : GlobalItem
    {
        public override bool InstancePerEntity => true;
        public override bool ConsumeItem(Item item, Player player)
        {
            GlobalPet Pet = player.GetModPlayer<GlobalPet>();
            Pigman pig = player.GetModPlayer<Pigman>();
            if (Pet.PetInUse(ItemID.PigPetItem))
            {
                Pet.timerMax = pig.shieldCooldown;
                if (BuffID.Sets.IsWellFed[item.buffType])
                {
                    if (Pet.timer <= 0 && Pet.PetInUseWithSwapCd(ItemID.PigPetItem))
                    {
                        Pet.shieldTimer += pig.shieldTime;
                        if (item.buffType == BuffID.WellFed)
                            Pet.shieldAmount += pig.tier1Shield;
                        if (item.buffType == BuffID.WellFed2)
                            Pet.shieldAmount += pig.tier2Shield;
                        if (item.buffType == BuffID.WellFed3)
                            Pet.shieldAmount += pig.tier3Shield;
                        Pet.timer = Pet.timerMax;
                    }
                    if (Main.rand.NextBool(pig.foodChance, 100))
                        return false;
                }
                else if (Main.debuff[item.buffType] == false && Main.rand.NextBool(pig.potionChance, 100))
                    return false;
            }
            return true;
        }
        public override bool AppliesToEntity(Item entity, bool lateInstantiation) => entity.type == ItemID.PigPetItem;

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            Pigman pigman = Main.LocalPlayer.GetModPlayer<Pigman>();
            tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.PigPetItem")
                       .Replace("<foodChance>", pigman.foodChance.ToString())
                       .Replace("<potionChance>", pigman.potionChance.ToString())
                       .Replace("<shield1>", pigman.tier1Shield.ToString())
                       .Replace("<shield2>", pigman.tier2Shield.ToString())
                       .Replace("<shield3>", pigman.tier3Shield.ToString())
                       .Replace("<shieldTime>", (pigman.shieldTime / 60f).ToString())
                       .Replace("<cooldown>", (pigman.shieldCooldown / 60f).ToString())
                       ));
        }
    }
}
