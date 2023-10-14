using Terraria;
using Terraria.ID;
using PetsOverhaul.Systems;
using Terraria.ModLoader;

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
    sealed public class PigmanEat : GlobalItem
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
    }
}
