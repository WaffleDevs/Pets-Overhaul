using Terraria;
using Terraria.ID;
using PetsOverhaul.Systems;
using Terraria.ModLoader;

namespace PetsOverhaul.PetEffects.Vanilla
{
    sealed public class SpiderBrain : ModPlayer
    {
        public int lifePool = 0;
        public float lifePoolMaxPerc = 0.1f;
        public int cdDoAddToPool = 25;
        public float lifestealAmount = 0.065f;
        GlobalPet Pet { get => Player.GetModPlayer<GlobalPet>(); }
        public override void PreUpdate()
        {
            if (Pet.PetInUse(ItemID.BrainOfCthulhuPetItem))
            {
                Pet.timerMax = cdDoAddToPool;
            }
        }
        public override void PreUpdateBuffs()
        {
            if (Pet.PetInUse(ItemID.BrainOfCthulhuPetItem) && Pet.timer <= 0 && lifePool <= Player.statLifeMax2 * lifePoolMaxPerc)
            {
                lifePool++;
                Pet.timer = Pet.timerMax;
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Pet.PetInUseWithSwapCd(ItemID.BrainOfCthulhuPetItem) && Pet.LifestealCheck(target))
            {
                int decreaseFromPool = Pet.Lifesteal(damageDone, lifestealAmount, doLifesteal: false);
                if (decreaseFromPool >= lifePool)
                {
                    Pet.Lifesteal(lifePool, 1f);
                    lifePool = 0;
                }
                else
                {
                    lifePool -= decreaseFromPool;
                    Pet.Lifesteal(decreaseFromPool, 1f);
                }
            }
        }
    }
}
