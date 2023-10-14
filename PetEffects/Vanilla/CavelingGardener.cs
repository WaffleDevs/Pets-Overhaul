using Terraria;
using Terraria.ID;
using PetsOverhaul.Systems;
using Terraria.ModLoader;

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
}

