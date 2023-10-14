using Terraria;
using Terraria.ID;
using PetsOverhaul.Systems;
using Terraria.ModLoader;

namespace PetsOverhaul.PetEffects.Vanilla
{
    sealed public class Bernie : ModPlayer
    {
        public int bernieRange = 3200;
        private int timer = 0;
        public int burnDrain = 6;
        public int maxBurning = 5;
        public int EnemiesBurning { get; internal set; }
        GlobalPet Pet { get => Player.GetModPlayer<GlobalPet>(); }
        public override void PreUpdate()
        {
            if (Pet.PetInUse(ItemID.BerniePetItem))
                timer++;
        }
        public override void PostUpdateEquips()
        {
            if (Pet.PetInUseWithSwapCd(ItemID.BerniePetItem))
            {

                Player.buffImmune[BuffID.Burning] = true;
                Player.buffImmune[BuffID.OnFire] = true;
                Player.buffImmune[BuffID.OnFire3] = true;
                Player.buffImmune[BuffID.Frostburn] = true;
                Player.buffImmune[BuffID.CursedInferno] = true;
                Player.buffImmune[BuffID.ShadowFlame] = true;
                Player.buffImmune[BuffID.Frostburn2] = true;
                EnemiesBurning = 0;
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC npc = Main.npc[i];
                    if (npc.active && Player.Distance(npc.Center) < bernieRange)
                    {
                        if (timer % 2 == 1)
                        {
                            for (int a = 0; a < NPC.maxBuffs; a++)
                            {
                                if (Pet.burnDebuffs[npc.buffType[a]])
                                    npc.buffTime[a]++;
                            }

                        }
                        for (int a = 0; a < NPC.maxBuffs; a++)
                        {
                            if (Pet.burnDebuffs[npc.buffType[a]])
                            {
                                EnemiesBurning++;
                                break;
                            }
                            if (EnemiesBurning >= 5)
                                break;
                        }
                    }
                }
                if (timer >= burnDrain)
                {
                    if (EnemiesBurning > 5)
                        EnemiesBurning = 5;
                    Pet.Lifesteal(burnDrain * 2 * EnemiesBurning, 0.005f * (Pet.abilityHaste + 1f), respectLifeStealCap: false);
                    Pet.Lifesteal(burnDrain * 4 * EnemiesBurning, 0.005f * (Pet.abilityHaste + 1f), respectLifeStealCap: false, manaSteal: true);
                    timer = 0;
                }
            }
        }
    }
}
