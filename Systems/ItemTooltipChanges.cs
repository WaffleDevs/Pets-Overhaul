using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using System.Collections.Generic;
using Terraria.Localization;
using PetsOverhaul.Items;
using PetsOverhaul.Config;
using Terraria.GameInput;
using PetsOverhaul.PetEffects.Vanilla;
using System;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace PetsOverhaul.Systems
{
    public class ItemTooltipChanges : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation) => ModContent.GetInstance<PetRegistry>().TerrariaPetItemIds.ContainsValue(entity.type);
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (ModContent.GetInstance<Personalization>().TooltipsEnabledWithShift && !PlayerInput.Triggers.Current.KeyStatus[TriggerNames.Down])
            {
                tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.Config.TooltipShiftToggleInGame")));
            }
        }
    }
}