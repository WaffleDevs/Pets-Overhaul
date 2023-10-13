using Terraria;
using Terraria.ID;
using PetsOverhaul.Systems;
using Terraria.ModLoader;

namespace PetsOverhaul.PetEffects
{
    sealed public class PlanteraSeedling : ModPlayer
    {
        public float secondMultiplier = 0f;
        GlobalPet Pet { get => Player.GetModPlayer<GlobalPet>(); }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (Pet.PetInUseWithSwapCd(ItemID.PlanteraPetItem) && modifiers.DamageType.CountsAsClass<RangedDamageClass>())
            {
                float multiplyDamage = 0;
                multiplyDamage += 2 / ((target.life * 1f) / target.lifeMax + 1);
                multiplyDamage += ((2 / ((target.life * 1f) / target.lifeMax + 1)) - 1) * secondMultiplier;
                modifiers.FinalDamage *= multiplyDamage;
            }
        }
    }
}
