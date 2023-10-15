using Terraria;
using Terraria.ID;
using PetsOverhaul.Systems;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.Localization;

namespace PetsOverhaul.PetEffects.Vanilla
{
    sealed public class BabyEater : ModPlayer
    {
        GlobalPet Pet { get => Player.GetModPlayer<GlobalPet>(); }
        public float moveSpd = 0.10f;
        public float jumpSpd = 0.5f;
        public int fallDamageTile = 20;
        public override void PostUpdateEquips()
        {
            if (Pet.PetInUseWithSwapCd(ItemID.EatersBone))
            {
                if (Player.ZoneCorrupt || Player.ZoneCrimson)
                    Player.extraFall += fallDamageTile;
                Player.moveSpeed += moveSpd;
                Player.jumpSpeedBoost += jumpSpd;
                Player.autoJump = true;
            }
        }
        public override void PostUpdate()
        {
            if (Pet.PetInUse(ItemID.EatersBone))
            {
                Player.armorEffectDrawShadow = true;
            }
        }
    }
    sealed public class EatersBone : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation) => entity.type == ItemID.EatersBone;

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            BabyEater babyEater = ModContent.GetInstance<BabyEater>();
            tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.EatersBone")
                .Replace("<moveSpd>", (babyEater.moveSpd * 100).ToString())
                .Replace("<jumpSpd>", (babyEater.jumpSpd * 100).ToString())
                .Replace("<fallRes>", babyEater.fallDamageTile.ToString())
            ));
        }
    }
}
