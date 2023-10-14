using Terraria;
using Terraria.ID;
using PetsOverhaul.Systems;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;

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
}
