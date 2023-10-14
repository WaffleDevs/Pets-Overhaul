using Terraria;
using Terraria.ID;
using PetsOverhaul.Systems;
using Terraria.ModLoader;

namespace PetsOverhaul.PetEffects.Vanilla
{
    sealed public class GlitteryButterfly : ModPlayer
    {
        GlobalPet Pet { get => Player.GetModPlayer<GlobalPet>(); }
        public int wingTime = 45;
        public int bonusTimeIfExisting = 150;
        public float healthPenalty = 0.08f;
        public override void PostUpdateEquips()
        {
            if (Pet.PetInUseWithSwapCd(ItemID.BedazzledNectar))
            {
                if (Player.equippedWings == null)
                {
                    Player.statLifeMax2 -= (int)(Player.statLifeMax2 * healthPenalty);
                    Player.wings = 5;
                    Player.wingsLogic = 5; //yüksek olduğunda oyuncu yürümekten acceleration almadıysa havada kımıldamak zorlaşıyor
                    Player.wingTimeMax = wingTime;
                    Player.noFallDmg = true;
                }
                else
                {
                    if (Player.equippedWings.type == ItemID.CreativeWings)
                        Player.wingTimeMax += wingTime;
                    else
                        Player.wingTimeMax += bonusTimeIfExisting;
                }
            }
        }
    }
}
