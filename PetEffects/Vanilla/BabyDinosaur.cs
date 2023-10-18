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
    sealed public class BabyDinosaur : ModPlayer
    {
        GlobalPet Pet { get => Player.GetModPlayer<GlobalPet>(); }
        public int chance = 175; // 17.5% because its with 1000
        public static void AddItemsToPool()
        {
            GlobalPet.pool.Clear();
            GlobalPet.ItemWeight(ItemID.TinOre, 10);
            GlobalPet.ItemWeight(ItemID.CopperOre, 10);
            GlobalPet.ItemWeight(ItemID.Amethyst, 9);
            GlobalPet.ItemWeight(ItemID.IronOre, 9);
            GlobalPet.ItemWeight(ItemID.LeadOre, 9);
            GlobalPet.ItemWeight(ItemID.Topaz, 8);
            GlobalPet.ItemWeight(ItemID.Sapphire, 8);
            GlobalPet.ItemWeight(ItemID.SilverOre, 8);
            GlobalPet.ItemWeight(ItemID.TungstenOre, 8);
            GlobalPet.ItemWeight(ItemID.GoldOre, 7);
            GlobalPet.ItemWeight(ItemID.PlatinumOre, 7);
            GlobalPet.ItemWeight(ItemID.Emerald, 7);
            GlobalPet.ItemWeight(ItemID.Ruby, 7);
            GlobalPet.ItemWeight(ItemID.Diamond, 6);
            GlobalPet.ItemWeight(ItemID.Amber, 6);
        }
        public override bool OnPickup(Item item)
        {
            if (item.TryGetGlobalItem(out ItemPet itemPet) && itemPet.oreBoost && Pet.PickupChecks(item, ItemID.AmberMosquito, itemPet))
            {
                AddItemsToPool();
                if (GlobalPet.pool.Count > 0)
                    for (int i = 0; i < ItemPet.Randomizer(chance * item.stack, 1000); i++)
                    {
                        Player.QuickSpawnItem(Player.GetSource_Misc("BabyDinosaur"), GlobalPet.pool[Main.rand.Next(GlobalPet.pool.Count)], 1);
                    }
                GlobalPet.pool.Clear();

            }
            return true;
        }
    }
    sealed public class AmberMosquito : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation) => entity.type == ItemID.AmberMosquito;

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (ModContent.GetInstance<Personalization>().TooltipsEnabledWithShift && !PlayerInput.Triggers.Current.KeyStatus[TriggerNames.Down]) return;
            BabyDinosaur babyDinosaur = Main.LocalPlayer.GetModPlayer<BabyDinosaur>();
            tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.AmberMosquito")
                .Replace("<oreChance>", (babyDinosaur.chance / 10f).ToString())
            ));
        }
    }
}
