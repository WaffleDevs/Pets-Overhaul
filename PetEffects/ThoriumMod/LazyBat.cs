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
    sealed public class LazyBat : ModPlayer
    {
        GlobalPet Pet { get => Player.GetModPlayer<GlobalPet>(); }
        public override void PostUpdateEquips()
        {
            
        }
    }
    sealed public class BloodSausage : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            if(ModManager.ThoriumMod == null) return false;
            if(ModManager.ThoriumMod.InternalNameToModdedItemId == null) return false;
            Console.WriteLine(ModManager.ThoriumMod.InternalNameToModdedItemId);
            if(!ModManager.ThoriumMod.InternalNameToModdedItemId.ContainsKey("BloodSausage")) return false;

            return entity.type == ModManager.ThoriumMod.InternalNameToModdedItemId["BloodSausage"];
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (ModContent.GetInstance<Personalization>().TooltipsEnabledWithShift && !PlayerInput.Triggers.Current.KeyStatus[TriggerNames.Down]) return;
            LazyBat lazyBat = Main.LocalPlayer.GetModPlayer<LazyBat>();
            tooltips.Add(new(Mod, "Tooltip0", "Unimplemented"/*Language.GetTextValue("Mods.PetsOverhaul.BloodSausageTooltips.BloodSausage")*/
                
            ));
        }
    }
}
