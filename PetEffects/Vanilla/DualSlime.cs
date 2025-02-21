﻿using Terraria;
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
    sealed public class DualSlime : ModPlayer
    {
        GlobalPet Pet { get => Player.GetModPlayer<GlobalPet>(); }
        // King, in SlimePrince
        public float wetSpeed = 0.9f;
        public float wetDmg = 0.07f;
        public float wetDef = 0.07f;
        public float slimyKb = 1.45f;
        public float slimyJump = 1.8f;
        public float wetDealtLower = 0.94f;
        public float wetRecievedHigher = 1.07f;
        public float bonusKb = 1.45f;
        public float healthDmg = 0.012f;
        public int burnCap = 45;
        // Queen, in SlimePrincess
        public float slow = 0.59f;
        public float haste = 0.26f;
        public int shield = 6;
        public int shieldTime = 240;
        public float dmgBoost = 1.22f;
        public int baseCounterChnc = 90;
    }

    sealed public class ResplendentDessert : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation) => entity.type == ItemID.ResplendentDessert;

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (ModContent.GetInstance<Personalization>().TooltipsEnabledWithShift && !PlayerInput.Triggers.Current.KeyStatus[TriggerNames.Down]) return;
            DualSlime babyOgre = Main.LocalPlayer.GetModPlayer<DualSlime>();
            tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.ResplendentDessert")
                .Replace("<approxWeak>", "10")
            ));
        }
    }
}
