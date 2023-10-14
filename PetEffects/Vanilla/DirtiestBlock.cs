using Terraria;
using Terraria.ID;
using PetsOverhaul.Systems;
using Terraria.ModLoader;

namespace PetsOverhaul.PetEffects.Vanilla
{
    sealed public class DirtiestBlock : ModPlayer
    {
        GlobalPet Pet { get => Player.GetModPlayer<GlobalPet>(); }
        public int dirtCoin = 3;
        public int soilCoin = 2;
        public int everythingCoin = 1;
        public override bool OnPickup(Item item)
        {
            if (item.TryGetGlobalItem(out ItemPet itemChck) && Pet.PickupChecks(item, ItemID.DirtiestBlock, itemChck))
            {
                if (itemChck.dirt == true)
                {
                    Player.QuickSpawnItem(Player.GetSource_Misc("DirtiestBlock"), ItemID.CopperCoin, item.stack * dirtCoin);
                }
                else if (itemChck.commonBlock == true)
                {
                    Player.QuickSpawnItem(Player.GetSource_Misc("DirtiestBlock"), ItemID.CopperCoin, item.stack * soilCoin);
                }
                else if (itemChck.blockNotByPlayer == true)
                {
                    Player.QuickSpawnItem(Player.GetSource_Misc("DirtiestBlock"), ItemID.CopperCoin, item.stack * everythingCoin);
                }
            }
            return true;

        }
    }
}
