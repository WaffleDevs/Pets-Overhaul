using Terraria;
using Terraria.ID;
using PetsOverhaul.Systems;
using Terraria.ModLoader;

namespace PetsOverhaul.PetEffects.Vanilla
{
    public class Squashling : ModPlayer
    {
        GlobalPet Pet { get => Player.GetModPlayer<GlobalPet>(); }
        public int squashlingCommonChance = 80;
        public int squashlingRareChance = 8;
        public override bool OnPickup(Item item)
        {
            if (item.TryGetGlobalItem(out ItemPet itemChck) && Pet.PickupChecks(item, ItemID.MagicalPumpkinSeed, itemChck))
            {
                if (itemChck.herbBoost == true)
                    item.stack += ItemPet.Randomizer(squashlingCommonChance * item.stack);
                if (itemChck.rareHerbBoost == true)
                    item.stack += ItemPet.Randomizer(squashlingRareChance * item.stack);
            }
            return true;
        }
    }
}

