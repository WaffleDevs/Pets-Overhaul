﻿using Terraria;
using Terraria.ID;
using PetsOverhaul.Systems;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.Localization;
using Terraria.GameInput;
using PetsOverhaul.Config;


namespace PetsOverhaul.PetEffects.Vanilla
{
    sealed public class BabyImp : ModPlayer
    {
        GlobalPet Pet { get => Player.GetModPlayer<GlobalPet>(); }
        public int lavaImmune = 600;
        public int lavaDef = 10;
        public float lavaSpd = 0.15f;
        public int obbyDef = 8;
        public float obbySpd = 0.08f;
        public override void PostUpdateEquips()
        {
            if (Pet.PetInUseWithSwapCd(ItemID.HellCake))
            {
                Player.lavaMax += lavaImmune;
                if (Main.tile[(int)(Player.Center.X / 16f), (int)((Player.Bottom.Y - 16f) / 16f)].LiquidAmount > 0 || Main.tile[(int)(Player.Right.X / 16f), (int)((Player.Bottom.Y - 16f) / 16f)].LiquidAmount > 0 || Player.lavaWet)
                {
                    if (Main.tile[(int)(Player.Left.X / 16f), (int)((Player.Bottom.Y - 16f) / 16f)].LiquidType == LiquidID.Lava || Main.tile[(int)(Player.Right.X / 16f), (int)((Player.Bottom.Y - 16f) / 16f)].LiquidType == LiquidID.Lava || Player.lavaWet)
                    {
                        Player.accFlipper = true;
                        Player.statDefense += lavaDef;
                        Player.moveSpeed += lavaSpd;
                    }
                }
                if (Player.HasBuff(BuffID.ObsidianSkin))
                {
                    Player.statDefense += obbyDef;
                    Player.moveSpeed -= obbySpd;
                }
            }
        }
        public override void PostUpdateRunSpeeds()
        {
            if (Pet.PetInUseWithSwapCd(ItemID.HellCake) && Player.HasBuff(BuffID.ObsidianSkin))
            {
                Player.maxRunSpeed -= obbySpd * 3;
            }
        }
    }
    sealed public class HellCake : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation) => entity.type == ItemID.HellCake;

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (ModContent.GetInstance<Personalization>().TooltipsEnabledWithShift && !PlayerInput.Triggers.Current.KeyStatus[TriggerNames.Down]) return;
            BabyImp babyImp = Main.LocalPlayer.GetModPlayer<BabyImp>();
            tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.HellCake")
                .Replace("<immuneTime>", (babyImp.lavaImmune / 60f).ToString())
                .Replace("<lavaDef>", babyImp.lavaDef.ToString())
                .Replace("<lavaSpd>", (babyImp.lavaSpd * 100).ToString())
                .Replace("<obbyDef>", babyImp.obbyDef.ToString())
                .Replace("<obbySpd>", (babyImp.obbySpd * 100).ToString())
            ));
        }
    }
}
