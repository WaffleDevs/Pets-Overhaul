using Terraria;
using Terraria.ID;
using PetsOverhaul.Systems;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.Localization;

namespace PetsOverhaul.PetEffects.Vanilla
{
    sealed public class DirtiestBlock : ModPlayer
    {
        GlobalPet Pet { get => Player.GetModPlayer<GlobalPet>(); }
        public int dirtCoin = 3;
        public int soilCoin = 2;
        public int everythingCoin = 1;
        public override bool OnPickup(Item item)
        {
            if (item.TryGetGlobalItem(out ItemPet itemChck) && Pet.PickupChecks(item, ItemID.DirtiestBlock, itemChck))
            {
                if (itemChck.dirt == true)
                {
                    Player.QuickSpawnItem(Player.GetSource_Misc("DirtiestBlock"), ItemID.CopperCoin, item.stack * dirtCoin);
                }
                else if (itemChck.commonBlock == true)
                {
                    Player.QuickSpawnItem(Player.GetSource_Misc("DirtiestBlock"), ItemID.CopperCoin, item.stack * soilCoin);
                }
                else if (itemChck.blockNotByPlayer == true)
                {
                    Player.QuickSpawnItem(Player.GetSource_Misc("DirtiestBlock"), ItemID.CopperCoin, item.stack * everythingCoin);
                }
            }
            return true;

        }
    }
    sealed public class DirtiestBlockItem : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation) => entity.type == ItemID.DirtiestBlock;

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            DirtiestBlock dirtiestBlock = ModContent.GetInstance<DirtiestBlock>();
            tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.DirtiestBlock")
                        .Replace("<any>", dirtiestBlock.everythingCoin.ToString())
                        .Replace("<soil>", dirtiestBlock.soilCoin.ToString())
                        .Replace("<dirt>", dirtiestBlock.dirtCoin.ToString())
                        ));
        }
    }
}
