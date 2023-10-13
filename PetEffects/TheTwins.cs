using Terraria;
using Terraria.ID;
using PetsOverhaul.Systems;
using Terraria.ModLoader;

namespace PetsOverhaul.PetEffects
{
    sealed public class TheTwins : ModPlayer
    {
        public int healthDmgCd = 42;
        public int closeRange = 112;
        public int longRange = 560;
        public float defLifestealDmgMult = 0.0001f;
        public float regularEnemyHpDmg = 0.01f;
        public float bossHpDmg = 0.001f;
        public int infernoTime = 240;
        public float defMult = 1.5f;
        GlobalPet Pet { get => Player.GetModPlayer<GlobalPet>(); }
        public override void PreUpdate()
        {
            if (Pet.PetInUse(ItemID.TwinsPetItem))
            {
                Pet.timerMax = healthDmgCd;
            }
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (Pet.PetInUseWithSwapCd(ItemID.TwinsPetItem))
            {
                if (Player.Distance(target.Center) > longRange && Pet.timer <= 0)
                {
                    if (target.boss == false || target.GetGlobalNPC<NpcPet>().nonBossTrueBosses[target.type] == false)
                    {
                        modifiers.FlatBonusDamage += (int)(target.lifeMax * regularEnemyHpDmg);
                    }
                    else
                    {
                        modifiers.FlatBonusDamage += (int)(target.lifeMax * bossHpDmg);
                    }
                    Pet.timer = Pet.timerMax;
                }

            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Player.Distance(target.Center) < closeRange && Pet.LifestealCheck(target)&& Pet.PetInUseWithSwapCd(ItemID.TwinsPetItem))
            {
                target.AddBuff(BuffID.CursedInferno, infernoTime);
                Pet.Lifesteal(Player.statDefense*defMult * (Player.endurance+1f), hit.Damage * defLifestealDmgMult);

            }
        }
    }
}
