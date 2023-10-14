using Terraria;
using Terraria.ID;
using PetsOverhaul.Systems;
using Terraria.ModLoader;
using Terraria.Audio;
using Microsoft.Xna.Framework;
using PetsOverhaul.Config;

namespace PetsOverhaul.PetEffects.Vanilla
{
    sealed public class SuspiciousEye : ModPlayer
    {
        GlobalPet Pet { get => Player.GetModPlayer<GlobalPet>(); }
        public int phaseCd = 9000;
        public int phaseTime = 1800;
        private int timer = 0;
        public float critMult = 0.2f;
        public float dmgMult = 1f;
        public float spdMult = 0.6f;
        public int eocDefenseConsume;
        public override void PreUpdate()
        {
            if (Pet.PetInUse(ItemID.EyeOfCthulhuPetItem))
                Pet.timerMax = phaseCd;
            if (timer >= -1)
                timer--;
        }
        public override void UpdateEquips()
        {
            if (Pet.PetInUseWithSwapCd(ItemID.EyeOfCthulhuPetItem))
            {
                if (Player.statLife < Player.statLifeMax2 / 2 && Pet.timer <= 0)
                {
                    timer = phaseTime;
                    Pet.timer = Pet.timerMax;
                    if (ModContent.GetInstance<Personalization>().AbilitySoundDisabled == false)
                        SoundEngine.PlaySound(SoundID.ForceRoar with { PitchVariance = 0.3f }, Player.position);
                    AdvancedPopupRequest popupMessage = new AdvancedPopupRequest();
                    popupMessage.Text = "ENRAGED!";
                    popupMessage.DurationInFrames = 150;
                    popupMessage.Velocity = new Vector2(0, -10);
                    popupMessage.Color = Color.DarkRed;
                    PopupText.NewText(popupMessage, Player.position);
                }
                if (timer <= phaseTime && timer >= 0)
                {
                    if (Player.statLife > Player.statLifeMax2 / 2)
                    {
                        Player.statLife = Player.statLifeMax2 / 2;
                    }
                    if (eocDefenseConsume <= 0)
                    {
                        eocDefenseConsume = Player.statDefense;
                    }
                    Player.statDefense *= 0;
                    Player.GetDamage<GenericDamageClass>() += eocDefenseConsume * dmgMult / 100;
                    Player.moveSpeed += eocDefenseConsume * spdMult / 100;
                    Player.GetCritChance<GenericDamageClass>() += eocDefenseConsume * critMult;
                    if (Player.dashDelay > 5)
                    {
                        Player.dashDelay = 5;
                        Player.eocDash = 5;
                    }
                }
                else if (timer == 0)
                {
                    AdvancedPopupRequest popupMessage = new AdvancedPopupRequest();
                    popupMessage.Text = "Calmed Down.";
                    popupMessage.DurationInFrames = 150;
                    popupMessage.Velocity = new Vector2(0, -10);
                    popupMessage.Color = Color.OrangeRed;
                    PopupText.NewText(popupMessage, Player.position);
                }
            }
        }
    }
}
