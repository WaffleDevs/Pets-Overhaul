using Terraria;
using Terraria.ID;
using PetsOverhaul.Systems;
using Terraria.ModLoader;

namespace PetsOverhaul.PetEffects.Vanilla
{
    sealed public class Destroyer : ModPlayer
    {
        GlobalPet Pet { get => Player.GetModPlayer<GlobalPet>(); }
        public int ironskinBonusDef = 8;
        public float flatDefMult = 1.22f;
        public float defItemMult = 0.5f;
        public int flatAmount = 10;
        public override void PostUpdateEquips()
        {
            if (Pet.PetInUseWithSwapCd(ItemID.DestroyerPetItem))
            {
                if (Player.HasBuff(BuffID.Ironskin))
                {
                    Player.statDefense += ironskinBonusDef;
                }
                Player.statDefense.FinalMultiplier *= flatDefMult;
            }
        }
        public override bool OnPickup(Item item)
        {
            if (item.TryGetGlobalItem(out ItemPet itemPet) && Pet.PickupChecks(item, ItemID.DestroyerPetItem, itemPet) && itemPet.oreBoost)
            {
                item.stack += ItemPet.Randomizer(Player.statDefense * defItemMult * item.stack + flatAmount);
            }
            return true;
        }
    }
}
