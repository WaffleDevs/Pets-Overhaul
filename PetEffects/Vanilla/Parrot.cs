using Terraria;
using Terraria.ID;
using PetsOverhaul.Systems;
using Terraria.Audio;
using Terraria.ModLoader;
using PetsOverhaul.Config;
using System.Collections.Generic;
using Terraria.Localization;

namespace PetsOverhaul.PetEffects.Vanilla
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
    sealed public class ParrotCracker : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation) => entity.type == ItemID.ParrotCracker;

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            Parrot parrot = Main.LocalPlayer.GetModPlayer<Parrot>();
            tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.ParrotCracker")
                        .Replace("<chance>", parrot.chance.ToString())
                        ));
        }
    }
}
