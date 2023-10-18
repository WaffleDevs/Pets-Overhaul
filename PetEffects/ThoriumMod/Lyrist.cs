using Terraria;
using Terraria.ID;
using PetsOverhaul.Systems;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.Localization;
using PetsOverhaul.Config;
using Terraria.GameInput;
using PetsOverhaul.ModSupport;
using System;

namespace PetsOverhaul.PetEffects.ThoriumMod
{
    sealed public class Lyrist : ModPlayer
    {
        GlobalPet Pet { get => Player.GetModPlayer<GlobalPet>(); }
        public override void PostUpdateEquips()
        {
            
        }
    }
    sealed public class ModelGun : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            if (!ModManager.IsModLoaded("ThoriumMod")) return false;
            if(ModManager.Mods["ThoriumMod"].InternalNameToModdedItemId == null) return false;
            if(!ModManager.Mods["ThoriumMod"].InternalNameToModdedItemId.ContainsKey("ModelGun")) return false;

            return entity.type == ModManager.Mods["ThoriumMod"].InternalNameToModdedItemId["ModelGun"];
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (ModContent.GetInstance<Personalization>().TooltipsEnabledWithShift && !PlayerInput.Triggers.Current.KeyStatus[TriggerNames.Down]) return;
            Lyrist lyrist = Main.LocalPlayer.GetModPlayer<Lyrist>();
            tooltips.Add(new(Mod, "Tooltip0", "Pet Overhaul effects coming soon!"/*Language.GetTextValue("Mods.PetsOverhaul.ModelGunTooltips.ModelGun")*/
                
            ));
        }
    }
}
