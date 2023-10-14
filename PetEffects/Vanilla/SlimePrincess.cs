using Terraria;
using Terraria.ID;
using PetsOverhaul.Systems;
using Terraria.ModLoader;

namespace PetsOverhaul.PetEffects.Vanilla
{
    sealed public class SlimePrincess : ModPlayer
    {
        public float slow = 0.65f;
        public float haste = 0.3f;
        public int shield = 7;
        public int shieldTime = 240;
        public int hitCounter = 0;
        public float dmgBoost = 1.25f;
        public Player queenSlime;
        public Player dualSlime;
        public int baseCounterChnc = 100;
        GlobalPet Pet { get => Player.GetModPlayer<GlobalPet>(); }
        public override void PostUpdateEquips()
        {
            if (GlobalPet.QueenSlimePetActive(out queenSlime) && Player.HasBuff(BuffID.GelBalloonBuff))
            {
                Pet.abilityHaste += queenSlime.GetModPlayer<SlimePrincess>().haste;
            }
            else if (GlobalPet.DualSlimePetActive(out dualSlime) && Player.HasBuff(BuffID.GelBalloonBuff))
            {
                Pet.abilityHaste += dualSlime.GetModPlayer<DualSlime>().haste;
            }
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (GlobalPet.QueenSlimePetActive(out queenSlime) && Player.HasBuff(BuffID.GelBalloonBuff))
            {
                hitCounter += ItemPet.Randomizer(queenSlime.GetModPlayer<SlimePrincess>().baseCounterChnc + (int)(Pet.abilityHaste * 100));
                if (hitCounter >= 6)
                {
                    modifiers.FinalDamage *= queenSlime.GetModPlayer<SlimePrincess>().dmgBoost;
                    if (queenSlime.GetModPlayer<SlimePrincess>().shield > Pet.shieldAmount)
                    {
                        Pet.shieldAmount += queenSlime.GetModPlayer<SlimePrincess>().shield;
                        Pet.shieldTimer = queenSlime.GetModPlayer<SlimePrincess>().shieldTime;
                    }
                    hitCounter -= 6;
                }
            }
            else if (GlobalPet.DualSlimePetActive(out dualSlime) && Player.HasBuff(BuffID.GelBalloonBuff))
            {
                hitCounter += ItemPet.Randomizer(dualSlime.GetModPlayer<DualSlime>().baseCounterChnc + (int)(Pet.abilityHaste * 100));
                if (hitCounter >= 6)
                {
                    modifiers.FinalDamage *= dualSlime.GetModPlayer<DualSlime>().dmgBoost;
                    if (dualSlime.GetModPlayer<DualSlime>().shield > Pet.shieldAmount)
                    {
                        Pet.shieldAmount += dualSlime.GetModPlayer<DualSlime>().shield;
                        Pet.shieldTimer = dualSlime.GetModPlayer<DualSlime>().shieldTime;
                    }
                    hitCounter -= 6;
                }
            }
        }
    }
}
