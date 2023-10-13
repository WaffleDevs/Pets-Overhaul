using Terraria;
using Terraria.ID;
using PetsOverhaul.Systems;
using Terraria.ModLoader;
using PetsOverhaul.Buffs;

namespace PetsOverhaul.PetEffects
{
    sealed public class ItsyBetsy : ModPlayer
    {
        public int debuffTime = 1200;
        public int maxStacks = 20;
        public float defReduction = 0.02f;
        public float missingHpRecover = 0.004f;
        GlobalPet Pet { get => Player.GetModPlayer<GlobalPet>(); }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (target.GetGlobalNPC<NpcPet>().curseCounter > maxStacks)
                target.GetGlobalNPC<NpcPet>().curseCounter = maxStacks;
            if (target.HasBuff(ModContent.BuffType<QueensDamnation>()))
                modifiers.Defense *= 1f - defReduction * target.GetGlobalNPC<NpcPet>().curseCounter;
            else
                target.GetGlobalNPC<NpcPet>().curseCounter = 0;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (target.active == false && Pet.LifestealCheck(target) && target.GetGlobalNPC<NpcPet>().curseCounter > 0)
            {
                int mult = 1;
                if (target.GetGlobalNPC<NpcPet>().curseCounter >= maxStacks)
                    mult = 2;
                Pet.Lifesteal(Player.statLifeMax2 - Player.statLife, missingHpRecover * target.GetGlobalNPC<NpcPet>().curseCounter * mult);
            }
            if (Pet.PetInUseWithSwapCd(ItemID.DD2BetsyPetItem) && hit.DamageType == DamageClass.Ranged)
            {
                target.AddBuff(ModContent.BuffType<QueensDamnation>(), debuffTime);
                target.GetGlobalNPC<NpcPet>().curseCounter++;
            }

        }
    }
}
