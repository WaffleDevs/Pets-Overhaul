using Terraria;
using Terraria.ID;
using PetsOverhaul.Systems;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using System.Collections.Generic;
using Terraria.Localization;

namespace PetsOverhaul.PetEffects.Vanilla
{
    sealed public class ZephyrFish : ModPlayer
    {
        public float powerPerQuest = 0.004f;
        public float maxQuestPower = 0.4f;
        public int baseChance = 40;
        public int windChance = 120;
        public int speedMult = 20;
        public bool amplifiedFishingChance { get; internal set; }
        GlobalPet Pet { get => Player.GetModPlayer<GlobalPet>(); }
        public override void PostUpdateEquips()
        {
            if (Pet.PetInUse(ItemID.ZephyrFish))
            {
                if (Main.windSpeedCurrent < 0)
                    Player.fishingSkill += (int)(Main.windSpeedCurrent * -speedMult);
                if (Main.windSpeedCurrent > 0)
                    Player.fishingSkill += (int)(Main.windSpeedCurrent * speedMult);
            }
        }
        public override void GetFishingLevel(Item fishingRod, Item bait, ref float fishingLevel)
        {
            if (Pet.PetInUse(ItemID.ZephyrFish))
            {
                if (Player.anglerQuestsFinished * 0.004f >= maxQuestPower)
                    fishingLevel += maxQuestPower;
                else
                    fishingLevel += Player.anglerQuestsFinished * 0.004f;
            }
        }
        public override bool Shoot(Item item, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (Pet.PetInUse(ItemID.ZephyrFish) && item.fishingPole > 0)
            {
                amplifiedFishingChance = false;
            }
            return true;
        }
        public override void CatchFish(FishingAttempt attempt, ref int itemDrop, ref int npcSpawn, ref AdvancedPopupRequest sonar, ref Vector2 sonarPosition)
        {
            if (Pet.PetInUse(ItemID.ZephyrFish))
            {
                if (Main.windSpeedCurrent > 0.2f && (attempt.heightLevel == 0 || attempt.heightLevel == 1) && attempt.X > Player.position.X / 16)
                {
                    amplifiedFishingChance = true;
                }
                else if (Main.windSpeedCurrent < -0.2f && (attempt.heightLevel == 0 || attempt.heightLevel == 1) && attempt.X < Player.position.X / 16)
                {
                    amplifiedFishingChance = true;
                }
            }
        }

        public override void ModifyCaughtFish(Item fish)
        {
            if (Pet.PetInUse(ItemID.ZephyrFish) && fish.maxStack != 1)
            {
                if (amplifiedFishingChance)
                    fish.stack += ItemPet.Randomizer(fish.stack * windChance);
                else
                    fish.stack += ItemPet.Randomizer(fish.stack * baseChance);
            }
        }
    }
    sealed public class ZephyrFishItem : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation) => entity.type == ItemID.ZephyrFish;

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            ZephyrFish zephyrFish = ModContent.GetInstance<ZephyrFish>();
            tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.ZephyrFish")
                        .Replace("<windFish>", (zephyrFish.speedMult / 8f).ToString())
                        .Replace("<regularChance>", zephyrFish.baseChance.ToString())
                        .Replace("<windChance>", zephyrFish.windChance.ToString())
                        .Replace("<anglerPower>", (zephyrFish.powerPerQuest * 100).ToString())
                        .Replace("<maxAnglerPower>", (zephyrFish.maxQuestPower * 100).ToString())
                        .Replace("<anglerQuests>", Main.LocalPlayer.anglerQuestsFinished.ToString())
                        .Replace("<currentAnglerPower>", (zephyrFish.powerPerQuest * Main.LocalPlayer.anglerQuestsFinished * 100).ToString())
                        ));
        }
    }
}
