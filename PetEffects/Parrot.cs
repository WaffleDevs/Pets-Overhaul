using Terraria;
using Terraria.ID;
using PetsOverhaul.Systems;
using Terraria.Audio;
using Terraria.ModLoader;
using PetsOverhaul.Config;

namespace PetsOverhaul.PetEffects
{
    sealed public class Parrot : ModPlayer
    {
        GlobalPet Pet { get => Player.GetModPlayer<GlobalPet>(); }
        public int chance = 13;
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Pet.PetInUseWithSwapCd(ItemID.ParrotCracker))
            {
                for (int i = 0; i < ItemPet.Randomizer(chance); i++)
                {
                    target.StrikeNPC(hit);
                    if (ModContent.GetInstance<Personalization>().AbilitySoundDisabled == false)
                        SoundEngine.PlaySound(SoundID.Zombie78 with { PitchVariance = 1f, MaxInstances = 3 }, target.position);
                }
            }
        }
    }
}
