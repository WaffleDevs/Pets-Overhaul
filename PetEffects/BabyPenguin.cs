using Terraria;
using Terraria.ID;
using PetsOverhaul.Systems;
using Terraria.ModLoader;

namespace PetsOverhaul.PetEffects
{
    sealed public class BabyPenguin : ModPlayer
    {
        GlobalPet Pet { get => Player.GetModPlayer<GlobalPet>(); }
        internal int penguinOldChilledTime = 0;
        public int snowFish = 25;
        public int oceanFish = 15;
        public int regularFish = 5;
        public float chillingMultiplier = 0.45f;
        public override void PostUpdateEquips()
        {
            if (Pet.PetInUse(ItemID.Fish))
            {
                if (Player.ZoneSnow)
                {
                    Player.fishingSkill += snowFish;
                    Player.accFlipper = true;
                }
                else if (Player.ZoneBeach)
                {
                    Player.fishingSkill += oceanFish;
                    Player.accFlipper = true;
                }
                else
                {
                    Player.fishingSkill += regularFish;
                }
            }

            if (Pet.PetInUseWithSwapCd(ItemID.Fish) && Player.HasBuff(BuffID.Chilled))
            {
                if (Player.buffTime[Player.FindBuffIndex(BuffID.Chilled)] > penguinOldChilledTime)
                {
                    Player.buffTime[Player.FindBuffIndex(BuffID.Chilled)] -= (int)(Player.buffTime[Player.FindBuffIndex(BuffID.Chilled)] * chillingMultiplier);
                }
                penguinOldChilledTime = Player.buffTime[Player.FindBuffIndex(BuffID.Chilled)];
            }


        }
        public override void ModifyCaughtFish(Item fish)
        {
            if (Pet.PetInUse(ItemID.Fish) && (fish.type == ItemID.FrostMinnow || fish.type == ItemID.AtlanticCod))
            {
                fish.stack *= 2;
            }
        }
    }
}
