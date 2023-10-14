using Terraria;
using Terraria.ID;
using PetsOverhaul.Systems;
using Terraria.ModLoader;

namespace PetsOverhaul.PetEffects.Vanilla
{
    sealed public class BabyEater : ModPlayer
    {
        GlobalPet Pet { get => Player.GetModPlayer<GlobalPet>(); }
        public float moveSpd = 0.10f;
        public float jumpSpd = 0.5f;
        public int fallDamageTile = 20;
        public override void PostUpdateEquips()
        {
            if (Pet.PetInUseWithSwapCd(ItemID.EatersBone))
            {
                if (Player.ZoneCorrupt || Player.ZoneCrimson)
                    Player.extraFall += fallDamageTile;
                Player.moveSpeed += moveSpd;
                Player.jumpSpeedBoost += jumpSpd;
                Player.autoJump = true;
            }
        }
        public override void PostUpdate()
        {
            if (Pet.PetInUse(ItemID.EatersBone))
            {
                Player.armorEffectDrawShadow = true;
            }
        }
    }

}
