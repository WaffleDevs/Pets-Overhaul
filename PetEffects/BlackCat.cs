using Terraria;
using Terraria.ID;
using PetsOverhaul.Systems;
using Terraria.ModLoader;

namespace PetsOverhaul.PetEffects
{
    sealed public class BlackCat : ModPlayer
    {
        GlobalPet Pet { get => Player.GetModPlayer<GlobalPet>(); }
        public float luckFlat = 0.12f;
        public float luckMoonLowest = 0.03f;
        public float luckMoonLow = 0.01f;
        public float luckMoonMid = 0.01f;
        public float luckMoonHigh = 0.03f;
        public float luckMoonHighest = 0.05f;
        public override void ModifyLuck(ref float luck)
        {
            if (Pet.PetInUseWithSwapCd(ItemID.UnluckyYarn))
            {
                luck += luckFlat;
                if (Main.dayTime == false)
                {
                    switch (Main.moonPhase)
                    {
                        case 0:
                            luck -= luckMoonLowest;
                            break;
                        case 1:
                            luck -= luckMoonLow;
                            break;
                        case 2:
                            luck += luckMoonMid;
                            break;
                        case 3:
                            luck += luckMoonHigh;
                            break;
                        case 4:
                            luck += luckMoonHighest;
                            break;
                        case 5:
                            luck += luckMoonHigh;
                            break;
                        case 6:
                            luck += luckMoonMid;
                            break;
                        case 7:
                            luck -= luckMoonLow;
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
