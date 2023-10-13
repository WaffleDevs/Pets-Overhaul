using Terraria;
using Terraria.ID;
using PetsOverhaul.Systems;
using Terraria.ModLoader;

namespace PetsOverhaul.PetEffects
{
    sealed public class ShadowMimic : ModPlayer
    {
        GlobalPet Pet { get => Player.GetModPlayer<GlobalPet>(); }
        public int npcCoin = 15;
        public int npcItem = 6; 
        public int bossCoin = 5;
        public int bossItem = 10;
        public int bagCoin = 3;
        public int bagItem = 5;
        public override bool OnPickup(Item item)
        {
            if (item.TryGetGlobalItem(out ItemPet itemChck) && Pet.PickupChecks(item, ItemID.OrnateShadowKey,itemChck))
            {
                if (itemChck.itemFromNpc == true)
                {
                    if (item.IsACoin)
                        item.stack += ItemPet.Randomizer(npcCoin * item.stack);
                    else
                        item.stack += ItemPet.Randomizer(npcItem * item.stack);
                }
                if (itemChck.itemFromBoss == true&& ItemID.Sets.BossBag[item.type]==false)
                {
                    if (item.IsACoin)
                        item.stack += ItemPet.Randomizer(bossCoin * item.stack);
                    else
                        item.stack += ItemPet.Randomizer(bossItem * item.stack);
                }
                if (itemChck.itemFromBag == true)
                {
                    if (item.IsACoin)
                        item.stack += ItemPet.Randomizer(bagCoin * item.stack);
                    else
                        item.stack += ItemPet.Randomizer(bagItem * item.stack);
                }
            }
            return true;
        }
    }
}
