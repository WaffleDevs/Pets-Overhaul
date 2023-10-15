using Terraria;
using Terraria.ID;
using PetsOverhaul.Systems;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.Localization;
using Terraria.GameInput;
using PetsOverhaul.Config;

using PetsOverhaul.Config;
using Terraria.GameInput;

namespace PetsOverhaul.PetEffects.Vanilla
{
    sealed public class GlitteryButterfly : ModPlayer
    {
        GlobalPet Pet { get => Player.GetModPlayer<GlobalPet>(); }
        public int wingTime = 45;
        public int bonusTimeIfExisting = 150;
        public float healthPenalty = 0.08f;
        public override void PostUpdateEquips()
        {
            if (Pet.PetInUseWithSwapCd(ItemID.BedazzledNectar))
            {
                if (Player.equippedWings == null)
                {
                    Player.statLifeMax2 -= (int)(Player.statLifeMax2 * healthPenalty);
                    Player.wings = 5;
                    Player.wingsLogic = 5; //yüksek olduğunda oyuncu yürümekten acceleration almadıysa havada kımıldamak zorlaşıyor
                    Player.wingTimeMax = wingTime;
                    Player.noFallDmg = true;
                }
                else
                {
                    if (Player.equippedWings.type == ItemID.CreativeWings)
                        Player.wingTimeMax += wingTime;
                    else
                        Player.wingTimeMax += bonusTimeIfExisting;
                }
            }
        }
    }
    sealed public class BedazzledNectar : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation) => entity.type == ItemID.BedazzledNectar;

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (ModContent.GetInstance<Personalization>().TooltipsEnabledWithShift && !PlayerInput.Triggers.Current.KeyStatus[TriggerNames.Down]) return;
            GlitteryButterfly glitteryButterfly = Main.LocalPlayer.GetModPlayer<GlitteryButterfly>();
            tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.BedazzledNectar")
                        .Replace("<flight>", (glitteryButterfly.wingTime / 60f).ToString())
                        .Replace("<bonusFlight>", (glitteryButterfly.bonusTimeIfExisting / 60f).ToString())
                        .Replace("<healthNerf>", (glitteryButterfly.healthPenalty * 100).ToString())
                        ));
        }
    }
}
