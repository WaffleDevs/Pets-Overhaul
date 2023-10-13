using Terraria;
using Terraria.ID;
using PetsOverhaul.Systems;
using Terraria.ModLoader;

namespace PetsOverhaul.PetEffects
{
    sealed public class SugarGlider : ModPlayer
    {
        public float speedMult = 1.1f;
        public float accMult = 1.2f;
        public float accSpeedRaise = 0.1f;
        GlobalPet Pet { get => Player.GetModPlayer<GlobalPet>(); }
        public override void PostUpdateEquips()
        {
            if (Pet.PetInUseWithSwapCd(ItemID.EucaluptusSap))
            {
                if (Player.equippedWings == null)
                {
                    Player.wings = 1;
                    Player.wingsLogic = 1;
                    Player.wingTimeMax = 0;
                }
            }
        }
    }
    sealed public class SugarGliderWing : GlobalItem
    {
        public override void HorizontalWingSpeeds(Item item, Player player, ref float speed, ref float acceleration)
        {
            if (player.TryGetModPlayer(out SugarGlider sugarGlider)&&player.GetModPlayer<GlobalPet>().PetInUseWithSwapCd(ItemID.EucaluptusSap))
            {
                speed *= sugarGlider.speedMult;
                speed += sugarGlider.accSpeedRaise;
                acceleration *= sugarGlider.accMult;
                acceleration += sugarGlider.accSpeedRaise;
            }
        }
    }

}
