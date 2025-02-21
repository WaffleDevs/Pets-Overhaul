﻿using Terraria;
using Terraria.ID;
using PetsOverhaul.Systems;
using PetsOverhaul.Buffs;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.Localization;

using PetsOverhaul.Config;
using Terraria.GameInput;

namespace PetsOverhaul.PetEffects.Vanilla
{
    sealed public class HoneyBee : ModPlayer
    {
        public GlobalPet Pet { get => Player.GetModPlayer<GlobalPet>(); }
        public float bottledHealth = 0.18f;
        public float honeyfinHealth = 0.25f;
        public float selfPotionIncrease = 0.1f;
        public int honeyOverdoseTime = 3600;
        public int bottledHoneyBuff = 1200;
        public int honeyfinHoneyBuff = 600;
        public float abilityHaste = 0.15f;
        public int range = 1600;
        public override void GetHealLife(Item item, bool quickHeal, ref int healValue)
        {
            if (Player.GetModPlayer<GlobalPet>().PetInUse(ItemID.QueenBeePetItem))
            {
                healValue += (int)(healValue * selfPotionIncrease);
            }
        }
    }
    sealed public class QueenBeePetItem : GlobalItem
    {
        public override bool InstancePerEntity => true;
        public override bool ConsumeItem(Item item, Player player)
        {
            if (player.TryGetModPlayer(out HoneyBee honeyBee) && honeyBee.Pet.PetInUse(ItemID.QueenBeePetItem))
            {
                if (item.type == ItemID.BottledHoney)
                {
                    player.AddBuff(ModContent.BuffType<HoneyOverdose>(), (int)(honeyBee.honeyOverdoseTime * (1 / (1 + player.GetModPlayer<GlobalPet>().abilityHaste))));
                    if (player.active && player.HasBuff(ModContent.BuffType<HoneyOverdose>()) == false)
                    {
                        player.statLife += (int)(player.statLifeMax2 * honeyBee.bottledHealth) / 2;
                        player.HealEffect((int)(player.statLifeMax2 * honeyBee.bottledHealth) / 2);
                    }
                    for (int i = 0; i < Main.maxPlayers; i++)
                    {
                        Player targetPlayer = Main.player[i];
                        if (targetPlayer.active && targetPlayer.HasBuff(ModContent.BuffType<HoneyOverdose>()) == false && player.Distance(targetPlayer.Center) < honeyBee.range && player.whoAmI != targetPlayer.whoAmI)
                        {
                            if (targetPlayer.statLife + (int)(targetPlayer.statLifeMax2 * honeyBee.bottledHealth) > targetPlayer.statLifeMax2)
                                targetPlayer.statLife = targetPlayer.statLifeMax2;
                            else
                                targetPlayer.statLife += (int)(targetPlayer.statLifeMax2 * honeyBee.bottledHealth);
                            targetPlayer.HealEffect((int)(targetPlayer.statLifeMax2 * honeyBee.bottledHealth));
                            targetPlayer.AddBuff(BuffID.Honey, 1200);
                            targetPlayer.AddBuff(ModContent.BuffType<HoneyOverdose>(), (int)(honeyBee.honeyOverdoseTime * (1 / (1 + player.GetModPlayer<GlobalPet>().abilityHaste))));

                        }
                    }
                }
                if (item.type == ItemID.Honeyfin)
                {
                    player.AddBuff(ModContent.BuffType<HoneyOverdose>(), (int)(honeyBee.honeyOverdoseTime * (1 / (1 + player.GetModPlayer<GlobalPet>().abilityHaste))));
                    if (player.active && player.HasBuff(ModContent.BuffType<HoneyOverdose>()) == false)
                    {
                        player.statLife += (int)(player.statLifeMax2 * honeyBee.honeyfinHealth) / 2;
                        player.HealEffect((int)(player.statLifeMax2 * honeyBee.honeyfinHealth) / 2);
                    }
                    for (int i = 0; i < Main.maxPlayers; i++)
                    {
                        Player targetPlayer = Main.player[i];
                        if (Main.player[i].active && !Main.player[i].HasBuff(ModContent.BuffType<HoneyOverdose>()) && player.Distance(Main.player[i].Center) < honeyBee.range && player.whoAmI != Main.player[i].whoAmI)
                        {
                            if (Main.player[i].statLife + (int)(Main.player[i].statLifeMax2 * honeyBee.honeyfinHealth) > Main.player[i].statLifeMax2)
                                Main.player[i].statLife = Main.player[i].statLifeMax2;
                            else
                                Main.player[i].statLife += (int)(Main.player[i].statLifeMax2 * honeyBee.honeyfinHealth);
                            Main.player[i].HealEffect((int)(Main.player[i].statLifeMax2 * honeyBee.honeyfinHealth));
                            Main.player[i].AddBuff(BuffID.Honey, 600);
                            Main.player[i].AddBuff(ModContent.BuffType<HoneyOverdose>(), (int)(honeyBee.honeyOverdoseTime * (1 / (1 + player.GetModPlayer<GlobalPet>().abilityHaste))));
                        }
                    }
                }

            }
            return true;
        }
        public override bool AppliesToEntity(Item entity, bool lateInstantiation) => entity.type == ItemID.QueenBeePetItem;

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (ModContent.GetInstance<Personalization>().TooltipsEnabledWithShift && !PlayerInput.Triggers.Current.KeyStatus[TriggerNames.Down]) return;
            HoneyBee honeyBee = Main.LocalPlayer.GetModPlayer<HoneyBee>();
            tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.QueenBeePetItem")
                       .Replace("<extraHeal>", (honeyBee.selfPotionIncrease * 100).ToString())
                       .Replace("<range>", (honeyBee.range / 16f).ToString())
                       .Replace("<bottledHealth>", (honeyBee.bottledHealth * 100).ToString())
                       .Replace("<honeyfinHealth>", (honeyBee.honeyfinHealth * 100).ToString())
                       .Replace("<bottledHoneyTime>", (honeyBee.bottledHoneyBuff / 60f).ToString())
                       .Replace("<honeyfinHoneyTime>", (honeyBee.honeyfinHoneyBuff / 60f).ToString())
                       .Replace("<abilityHaste>", (honeyBee.abilityHaste * 100).ToString())
                       ));
        }
    }
}
