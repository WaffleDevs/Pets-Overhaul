using Terraria;
using Terraria.ID;
using PetsOverhaul.Systems;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.Localization;

namespace PetsOverhaul.PetEffects.Vanilla
{
    sealed public class CavelingGardener : ModPlayer
    {
        GlobalPet Pet { get => Player.GetModPlayer<GlobalPet>(); }
        public int cavelingRegularPlantChance = 30;
        public int cavelingGemTreeChance = 100;
        public int cavelingRarePlantChance = 15;
        public override bool OnPickup(Item item)
        {
            if (item.TryGetGlobalItem(out ItemPet itemChck) && Pet.PickupChecks(item, ItemID.GlowTulip, itemChck))
            {
                if (itemChck.gemTree)
                    item.stack += ItemPet.Randomizer(cavelingGemTreeChance * item.stack);
                if ((itemChck.tree || itemChck.gemTree || itemChck.herbBoost) && (Player.ZoneDirtLayerHeight || Player.ZoneRockLayerHeight || Player.ZoneUnderworldHeight))
                    item.stack += ItemPet.Randomizer(cavelingRegularPlantChance * item.stack);
                if (itemChck.rareHerbBoost && (Player.ZoneDirtLayerHeight || Player.ZoneRockLayerHeight || Player.ZoneUnderworldHeight))
                    item.stack += ItemPet.Randomizer(cavelingRarePlantChance * item.stack);
            }

            return true;
        }
        public override void UpdateEquips()
        {
            if (Pet.PetInUse(ItemID.GlowTulip))
                Lighting.AddLight(Player.Center, TorchID.Blue);
        }
    }
    sealed public class GlowTulip : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation) => entity.type == ItemID.GlowTulip;

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            CavelingGardener cavelingGardener = Main.LocalPlayer.GetModPlayer<CavelingGardener>();
            tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.GlowTulip")
                .Replace("<harvestChance>", cavelingGardener.cavelingRegularPlantChance.ToString())
                .Replace("<rarePlantChance>", cavelingGardener.cavelingRarePlantChance.ToString())
                .Replace("<gemstoneTreeChance>", cavelingGardener.cavelingGemTreeChance.ToString())
            ));
        }
    }
}

