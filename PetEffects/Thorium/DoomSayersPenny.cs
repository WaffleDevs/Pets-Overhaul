using Terraria;
using Terraria.ID;
using PetsOverhaul.Systems;
using Terraria.ModLoader;

namespace PetsOverhaul.PetEffects.Thorium
{
    sealed public class DoomSayersPenny : ModPlayer
    {
        GlobalPet Pet { get => Player.GetModPlayer<GlobalPet>(); }
        public override void PostUpdateEquips()
        {
            //if (Pet.PetInUseWithSwapCd(ItemID.MartianPetItem)) // Run code once.
            //if (Pet.PetInUse(ItemID.MartianPetItem)) // Run code every update.
            //{
            //    
            //}
        }
    }
}