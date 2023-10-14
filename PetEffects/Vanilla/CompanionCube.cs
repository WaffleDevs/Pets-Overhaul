using Terraria;
using Terraria.ID;
using PetsOverhaul.Systems;
using Terraria.ModLoader;

namespace PetsOverhaul.PetEffects.Vanilla
{
    sealed public class CompanionCube : ModPlayer
    {
        GlobalPet Pet { get => Player.GetModPlayer<GlobalPet>(); }
        public float manaToHealth = 0.11f;
        /// <summary>
        /// This is base amount for mana to health recovery before the Potion Sickness reductions.
        /// </summary>
        public float manaToHealthUpdate = 0.11f;
        public float healthToMana = 0.2f;
        public float manaPotionNerf = 0.17f;
        public float manaToHealthNerf = 0.03f;
        public override void PreUpdate()
        {
            if (Pet.PetInUse(ItemID.CompanionCube))
                manaToHealth = manaToHealthUpdate;
        }
        public override void PostUpdateBuffs()
        {
            if (Pet.PetInUseWithSwapCd(ItemID.CompanionCube) && Player.manaSick)
            {
                if (Player.manaSickReduction > Player.manaSickLessDmg)
                    manaToHealth -= manaToHealthNerf * 2;
                else
                    manaToHealth -= manaToHealthNerf;
            }
        }
        public override void GetHealMana(Item item, bool quickHeal, ref int healValue)
        {


            if (Pet.PetInUseWithSwapCd(ItemID.CompanionCube) && Player.manaSick)
            {
                if (Player.manaSickReduction > Player.manaSickLessDmg)
                    healValue -= (int)(healValue * manaPotionNerf * 2);
                else
                    healValue -= (int)(healValue * manaPotionNerf);
            }
        }
        public override void OnConsumeMana(Item item, int manaConsumed)
        {

            if (Pet.PetInUseWithSwapCd(ItemID.CompanionCube))
                Pet.Lifesteal(manaConsumed, manaToHealth, respectLifeStealCap: false);
        }
        public override void PostHurt(Player.HurtInfo info)
        {
            if (Pet.PetInUseWithSwapCd(ItemID.CompanionCube))
                Pet.Lifesteal(info.Damage, healthToMana, manaSteal: true, respectLifeStealCap: false);
        }
    }
}
