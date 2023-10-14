using Terraria;
using Terraria.ID;
using PetsOverhaul.Systems;
using Terraria.ModLoader;

namespace PetsOverhaul.PetEffects.Vanilla
{
    sealed public class BabyRedPanda : ModPlayer
    {
        GlobalPet Pet { get => Player.GetModPlayer<GlobalPet>(); }
        public int aggroReduce = 500;
        public float regularAtkSpd = 0.06f;
        public float jungleBonusSpd = 0.04f;
        public int bambooChance = 50;
        public override void PostUpdateEquips()
        {
            if (Pet.PetInUseWithSwapCd(ItemID.BambooLeaf))
            {
                Player.aggro -= aggroReduce;
                Player.GetAttackSpeed<GenericDamageClass>() += regularAtkSpd;
                if (Player.ZoneJungle)
                    Player.GetAttackSpeed<GenericDamageClass>() += jungleBonusSpd;
            }
        }
        public override bool OnPickup(Item item)
        {
            if (item.TryGetGlobalItem(out ItemPet itemPet) && Pet.PickupChecks(item, ItemID.BambooLeaf, itemPet) && itemPet.bambooBoost)
            {
                item.stack += ItemPet.Randomizer(bambooChance * item.stack);
            }
            return true;
        }
    }
}
