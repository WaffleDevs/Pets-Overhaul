using Terraria;
using Terraria.ID;
using PetsOverhaul.Systems;
using Terraria.ModLoader;
using PetsOverhaul.Config;
using Terraria.Audio;
using static Terraria.ID.ArmorIDs;
using System.Collections.Generic;
using Terraria.Localization;

using PetsOverhaul.Config;
using Terraria.GameInput;

namespace PetsOverhaul.PetEffects.Vanilla
{
    sealed public class EyeballSpring : ModPlayer
    {
        GlobalPet Pet { get => Player.GetModPlayer<GlobalPet>(); }
        public float acceleration = 0.15f;
        public float jumpBoost = 5f;
        public float ascentPenaltyMult = 0.6f;
        public override void PostUpdateEquips()
        {
            if (Pet.PetInUseWithSwapCd(ItemID.EyeSpring))
            {
                if (Player.jump > 0 && Pet.jumpRegistered == false)
                {
                    if (ModContent.GetInstance<Personalization>().HurtSoundDisabled == false)
                        SoundEngine.PlaySound(SoundID.Item56 with { Volume = 0.5f, Pitch = -0.3f, PitchVariance = 0.1f }, Player.position);
                    Pet.jumpRegistered = true;
                }
                Player.runAcceleration += acceleration;
                Player.jumpSpeedBoost += jumpBoost;
            }
        }
    }
    sealed public class EyeSpring : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation) => entity.type == ItemID.EyeSpring;

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (ModContent.GetInstance<Personalization>().TooltipsEnabledWithShift && !PlayerInput.Triggers.Current.KeyStatus[TriggerNames.Down]) return;
            EyeballSpring eyeballSpring = Main.LocalPlayer.GetModPlayer<EyeballSpring>();
            tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.EyeSpring")
                        .Replace("<jumpBoost>", (eyeballSpring.jumpBoost * 100).ToString())
                        .Replace("<acceleration>", (eyeballSpring.acceleration * 100).ToString())
                        .Replace("<ascNerf>", eyeballSpring.ascentPenaltyMult.ToString())
                        ));
        }
    }
    sealed public class EyeballSpringWing : GlobalItem
    { 
        public override bool InstancePerEntity => true;
        public override void VerticalWingSpeeds(Item item, Player player, ref float ascentWhenFalling, ref float ascentWhenRising, ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            if (player.TryGetModPlayer(out EyeballSpring eyeballs) && player.GetModPlayer<GlobalPet>().PetInUseWithSwapCd(ItemID.EyeSpring))
            {
                maxAscentMultiplier *= eyeballs.ascentPenaltyMult;
            }

        }
    }
}
