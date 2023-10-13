using Terraria;
using Terraria.ID;
using PetsOverhaul.Systems;
using Terraria.ModLoader;

namespace PetsOverhaul.PetEffects
{
    sealed public class MiniPrime : ModPlayer
    {
        public int shieldRecovery = 7500; //all 5 shields timer combined
        public float dmgIncrease = 0.07f;
        public int critIncrease = 7;
        public float defIncrease = 1.15f;
        public float shieldMult = 0.06f;
        public int shieldTime = 300;
        int lastShield = 0;
        GlobalPet Pet { get => Player.GetModPlayer<GlobalPet>(); }
        public override void PreUpdate()
        {
            if (Pet.PetInUse(ItemID.SkeletronPrimePetItem))
                Pet.timerMax = shieldRecovery;
        }
        public override void PostUpdateEquips()
        {
            if (Pet.PetInUseWithSwapCd(ItemID.SkeletronPrimePetItem))
            {
                if (Pet.timer <= Pet.timerMax * 0.8f && Player.statLife <= Player.statLifeMax2 * 0.25f)
                {
                    if (Pet.shieldAmount < lastShield)
                    {
                        Pet.timer += Pet.timerMax / 5;
                        Pet.shieldTimer = shieldTime;
                    }
                    else
                    {
                        Pet.shieldAmount = (int)(Player.statLifeMax2 * shieldMult);
                        Pet.shieldTimer = 1;
                    }
                }
                else if (Pet.timer <= Pet.timerMax * 0.6f && Player.statLife <= Player.statLifeMax2 * 0.5f)
                {
                    if (Pet.shieldAmount < lastShield)
                    {
                        Pet.timer += Pet.timerMax / 5;
                        Pet.shieldTimer = shieldTime;

                    }
                    else
                    {
                        Pet.shieldAmount = (int)(Player.statLifeMax2 * shieldMult);
                        Pet.shieldTimer = 1;
                    }
                }
                else if (Pet.timer <= Pet.timerMax * 0.4f && Player.statLife <= Player.statLifeMax2 * 0.75f)
                {
                    if (Pet.shieldAmount < lastShield)
                    {
                        Pet.timer += Pet.timerMax / 5;
                        Pet.shieldTimer = shieldTime;
                    }
                    else
                    {
                        Pet.shieldAmount = (int)(Player.statLifeMax2 * shieldMult);
                        Pet.shieldTimer = 1;
                    }
                }
                else if (Pet.timer <= Pet.timerMax * 0.2f && Player.statLife < Player.statLifeMax2)
                {
                    if (Pet.shieldAmount < lastShield)
                    {
                        Pet.timer += Pet.timerMax / 5;
                        Pet.shieldTimer = shieldTime;
                    }
                    else
                    {
                        Pet.shieldAmount = (int)(Player.statLifeMax2 * shieldMult);
                        Pet.shieldTimer = 1;
                    }
                }
                else if (Pet.timer <= 0 && Player.statLife <= Player.statLifeMax2)
                {
                    if (Pet.shieldAmount < lastShield)
                    {
                        Pet.timer += Pet.timerMax / 5;
                        Pet.shieldTimer = shieldTime;
                    }
                    else
                    {
                        Pet.shieldAmount = (int)(Player.statLifeMax2 * shieldMult);
                        Pet.shieldTimer = 1;
                    }
                }
                lastShield = Pet.shieldAmount;
                if (Pet.shieldAmount > 0)
                {
                    Player.GetDamage<GenericDamageClass>() += dmgIncrease;
                    Player.GetCritChance<GenericDamageClass>() += critIncrease;
                    Player.statDefense *= defIncrease;
                }

            }
        }
    }
}
