using Terraria;
using Terraria.ID;
using PetsOverhaul.Systems;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace PetsOverhaul.PetEffects.Vanilla
{
    sealed public class VoltBunny : ModPlayer
    {
        public float movespdFlat = 0.05f;
        public float movespdMult = 1.1f;
        public float movespdToDmg = 0.2f;
        public float lightningRod = 0.1f;
        private int lightningRodTime = 0;
        public int lightningRodMax = 300;
        GlobalPet Pet { get => Player.GetModPlayer<GlobalPet>(); }
        public override void PostUpdateEquips()
        {
            if (Pet.PetInUseWithSwapCd(ItemID.LightningCarrot))
            {
                Player.moveSpeed += movespdFlat;
                Player.moveSpeed *= movespdMult;
                Player.GetDamage<GenericDamageClass>() += (Player.moveSpeed - 1f) * movespdToDmg;
                Player.buffImmune[BuffID.Electrified] = false;
                if (lightningRodTime > 0)
                {
                    lightningRodTime--;
                    Player.GetDamage<GenericDamageClass>() += lightningRod;
                }
                if (Player.HasBuff(BuffID.Electrified))
                {
                    AdvancedPopupRequest popupMessage = new();
                    popupMessage.Text = "Lightning Rod makes the user immune to Electrified!";
                    popupMessage.DurationInFrames = 120;
                    popupMessage.Velocity = new Vector2(0, -7);
                    popupMessage.Color = Color.LightGoldenrodYellow;
                    PopupText.NewText(popupMessage, Player.position);
                    Player.ClearBuff(BuffID.Electrified);
                    lightningRodTime = lightningRodMax;
                }
            }
        }
    }
}
