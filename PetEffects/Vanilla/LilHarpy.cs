using Terraria;
using Terraria.ID;
using PetsOverhaul.Systems;
using Terraria.ModLoader;
using Terraria.Audio;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.Localization;

namespace PetsOverhaul.PetEffects.Vanilla
{
    sealed public class LilHarpy : ModPlayer
    {
        public int harpyCd = 780;
        public int fuelMax = 180;
        public int harpyFlight = 180;
        GlobalPet Pet { get => Player.GetModPlayer<GlobalPet>(); }
        public override void PreUpdate()
        {
            if (Pet.PetInUse(ItemID.BirdieRattle))
                Pet.timerMax = harpyCd;
        }
        public override void PostUpdateEquips()
        {
            if (Pet.PetInUseWithSwapCd(ItemID.BirdieRattle))
            {

                if (Pet.timer == 0)
                {
                    harpyFlight = fuelMax;
                    AdvancedPopupRequest popupMessage = new();
                    popupMessage.Text = "Cooldown Refreshed!";
                    popupMessage.DurationInFrames = 120;
                    popupMessage.Velocity = new Vector2(0, -7);
                    popupMessage.Color = Color.CornflowerBlue;
                    PopupText.NewText(popupMessage, Player.position);
                }
                if (Player.equippedWings == null)
                {
                    if (harpyFlight == 180 && Player.wingTime == 0)
                    {
                        Player.wingTime = 180;
                    }
                    if (harpyFlight > 0)
                    {
                        Player.wings = 7;
                        Player.wingsLogic = 1;
                        Player.wingTimeMax = harpyFlight;
                        harpyFlight = (int)Player.wingTime;
                    }
                    Player.noFallDmg = true;
                }
                if (harpyFlight < fuelMax)
                {
                    Pet.timer = Pet.timerMax;
                }
            }
        }
    }
    sealed public class BirdieRattle : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation) => entity.type == ItemID.BirdieRattle;

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            LilHarpy lilHarpy = ModContent.GetInstance<LilHarpy>();
            tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.BirdieRattle")
                        .Replace("<flightTime>", (lilHarpy.fuelMax / 60f).ToString())
                        .Replace("<cooldown>", (lilHarpy.harpyCd / 60f).ToString())
                        ));
        }
    }
}
