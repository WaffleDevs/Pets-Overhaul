using Terraria;
using Terraria.ID;
using PetsOverhaul.Systems;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace PetsOverhaul.PetEffects.Vanilla
{
    sealed public class Sapling : ModPlayer
    {
        public float planteraLifesteal = 0.033f;
        public float regularLifesteal = 0.012f;
        GlobalPet Pet { get => Player.GetModPlayer<GlobalPet>(); }
        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Pet.PetInUseWithSwapCd(ItemID.Seedling) && Pet.LifestealCheck(target))
            {
                if (proj.GetGlobalProjectile<ProjectileSourceChecks>().isPlanteraProjectile)
                {
                    Pet.Lifesteal(damageDone, planteraLifesteal);
                }
                else
                {
                    Pet.Lifesteal(damageDone, regularLifesteal);
                }
            }
        }
        public override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Pet.PetInUseWithSwapCd(ItemID.Seedling) && Pet.LifestealCheck(target))
            {
                if (item.type == ItemID.Seedler || item.type == ItemID.TheAxe)
                {
                    Pet.Lifesteal(damageDone, planteraLifesteal);
                }
                else
                {
                    Pet.Lifesteal(damageDone, regularLifesteal);
                }
            }
        }
    }
}
