using Terraria;
using Terraria.ID;
using PetsOverhaul.Systems;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.Localization;

namespace PetsOverhaul.PetEffects.Vanilla
{
    sealed public class Chester : ModPlayer
    {
        GlobalPet Pet { get => Player.GetModPlayer<GlobalPet>(); }
        public int placementRange = 2;
        public int chestOpenDef = 8;
        public int suckingUpRange = 64;
        public override void PostUpdateEquips()
        {
            if (Pet.PetInUse(ItemID.ChesterPetItem))
            {
                Player.blockRange += placementRange;
                if (Player.chest != -1)
                {
                    Player.statDefense += chestOpenDef;
                }
            }
        }
    }
    sealed public class ChesterItemGrab : GlobalItem
    {
        public override bool InstancePerEntity => true;
        public override void GrabRange(Item item, Player player, ref int grabRange)
        {
            if (player.GetModPlayer<GlobalPet>().PetInUse(ItemID.ChesterPetItem))
            {
                grabRange += player.GetModPlayer<Chester>().suckingUpRange;
            }
        }
    }
    sealed public class ChesterPetItem : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation) => entity.type == ItemID.ChesterPetItem;

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            Chester chester = Main.LocalPlayer.GetModPlayer<Chester>();
            tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.ChesterPetItem")
                .Replace("<pickupRange>", (chester.suckingUpRange / 16f).ToString())
                .Replace("<placementRange>", chester.placementRange.ToString())
                .Replace("<chestDef>", chester.chestOpenDef.ToString())
            ));
        }
    }
}
