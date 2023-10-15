using Terraria;
using Terraria.ID;
using PetsOverhaul.Systems;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.Localization;
using PetsOverhaul.Config;
using Terraria.GameInput;

namespace PetsOverhaul.PetEffects.Vanilla
{
    sealed public class AlienSkater : ModPlayer
    {
        GlobalPet Pet { get => Player.GetModPlayer<GlobalPet>(); }
        public float accelerator = 0.25f;
        public float wingTime = 1.4f;
        public float speedMult = 1.3f;
        public float accMult = 1.5f;
        public float speedAccIncr = 0.9f;
        public override void PostUpdateEquips()
        {
            if (Pet.PetInUseWithSwapCd(ItemID.MartianPetItem))
            {
                Player.runAcceleration += 0.25f;
                Player.wingTimeMax = (int)(Player.wingTimeMax * wingTime);
            }
        }
    }
    sealed public class AlienSkaterWing : GlobalItem
    {
        public override bool InstancePerEntity => true;

        public override void HorizontalWingSpeeds(Item item, Player player, ref float speed, ref float acceleration)
        {
            if (player.TryGetModPlayer(out AlienSkater alienSkater) && player.GetModPlayer<GlobalPet>().PetInUseWithSwapCd(ItemID.MartianPetItem))
            {
                speed *= alienSkater.speedMult;
                acceleration *= alienSkater.accMult;
                speed += alienSkater.speedAccIncr;
                acceleration += alienSkater.speedAccIncr;
            }

        }
    }
    sealed public class MartianPetItem : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation) => entity.type == ItemID.MartianPetItem;

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (ModContent.GetInstance<Personalization>().TooltipsEnabledWithShift && PlayerInput.Triggers.Current.KeyStatus[TriggerNames.Down])
            {
                AlienSkater alienSkater = Main.LocalPlayer.GetModPlayer<AlienSkater>();
                tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.MartianPetItem")
                    .Replace("<wingMult>", alienSkater.wingTime.ToString())
                    .Replace("<acc>", (alienSkater.accelerator * 100).ToString())
                    .Replace("<speedMult>", alienSkater.speedMult.ToString())
                    .Replace("<accMult>", alienSkater.accMult.ToString())
                    .Replace("<flatSpdAcc>", (alienSkater.speedAccIncr * 100).ToString())
                ));
            }
        }
    }
}
