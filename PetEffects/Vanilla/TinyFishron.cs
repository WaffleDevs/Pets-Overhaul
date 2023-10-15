using Terraria;
using Terraria.ID;
using PetsOverhaul.Systems;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.Localization;

namespace PetsOverhaul.PetEffects.Vanilla
{
    sealed public class TinyFishron : ModPlayer
    {
        public float fishingPowerPenalty = 0.5f;
        public float fpPerQuest = 0.002f;
        public float maxQuestPower = 0.5f;
        public int bobberChance = 105;
        public int stackChance = 10;
        public float multiplier = 1f;
        GlobalPet Pet { get => Player.GetModPlayer<GlobalPet>(); }
        public override void GetFishingLevel(Item fishingRod, Item bait, ref float fishingLevel)
        {
            if (Pet.PetInUse(ItemID.DukeFishronPetItem))
            {
                float fishingPowerMult = 0;
                fishingPowerMult += fishingPowerPenalty;
                if (Player.anglerQuestsFinished * fpPerQuest > maxQuestPower)
                {
                    fishingPowerMult += maxQuestPower;
                }
                else
                    fishingPowerMult += Player.anglerQuestsFinished * fpPerQuest;
                fishingLevel += fishingPowerMult;
            }
        }
        public override void ModifyCaughtFish(Item fish)
        {
            if (Pet.PetInUse(ItemID.DukeFishronPetItem) && fish.maxStack != 1)
            {
                fish.stack += ItemPet.Randomizer((stackChance + (int)(Player.fishingSkill * multiplier)) * fish.stack);
            }
        }
        public override bool Shoot(Item item, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (Pet.PetInUse(ItemID.DukeFishronPetItem) && item.fishingPole > 0)
            {
                for (int i = 0; i < ItemPet.Randomizer(bobberChance); i++)
                {
                    Vector2 bobberSpeed = velocity + new Vector2(Main.rand.NextFloat(-50f, 50f) * 0.05f, Main.rand.NextFloat(-50f, 50f) * 0.05f);
                    Projectile.NewProjectile(source, position, bobberSpeed, ProjectileID.FishingBobber, 0, 0f, Player.whoAmI);
                }
            }
            return true;
        }
    }
    sealed public class DukeFishronPetItem : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation) => entity.type == ItemID.DukeFishronPetItem;

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            TinyFishron tinyFishron = Main.LocalPlayer.GetModPlayer<TinyFishron>();
            tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.DukeFishronPetItem")
                        .Replace("<baseMult>", tinyFishron.fishingPowerPenalty.ToString())
                        .Replace("<anglerFishingPower>", tinyFishron.fpPerQuest.ToString())
                        .Replace("<flatChance>", tinyFishron.stackChance.ToString())
                        .Replace("<fishingPowerChance>", (tinyFishron.multiplier * 100).ToString())
                        .Replace("<bobberChance>", tinyFishron.bobberChance.ToString())
                        .Replace("<anglerQuests>", Main.LocalPlayer.anglerQuestsFinished.ToString())
                        .Replace("<currentAnglerWithBaseMult>", (Main.LocalPlayer.anglerQuestsFinished * tinyFishron.fpPerQuest + tinyFishron.fishingPowerPenalty).ToString())
                        ));
        }
    }
}
