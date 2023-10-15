using Terraria;
using Terraria.ID;
using PetsOverhaul.Systems;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.Localization;

namespace PetsOverhaul.PetEffects.Vanilla
{
    sealed public class FennecFox : ModPlayer
    {
        GlobalPet Pet { get => Player.GetModPlayer<GlobalPet>(); }
        public float sizeDecrease = 0.85f;
        public float speedIncrease = 0.15f;
        public float meleeSpdIncrease = 0.18f;
        public float meleeDmg = 0.05f;
        public override void ModifyItemScale(Item item, ref float scale)
        {
            if (Pet.PetInUseWithSwapCd(ItemID.ExoticEasternChewToy))
                scale *= sizeDecrease;
        }
        public override void PostUpdateEquips()
        {
            if (Pet.PetInUseWithSwapCd(ItemID.ExoticEasternChewToy))
            {
                if (Player.HeldItem.CountsAsClass<MeleeDamageClass>())
                {
                    Player.moveSpeed += speedIncrease;
                    Player.GetAttackSpeed<MeleeDamageClass>() += meleeSpdIncrease;
                }
                Player.GetDamage<MeleeDamageClass>() += meleeDmg;
            }
        }
    }
    sealed public class ExoticEasternChewToy : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation) => entity.type == ItemID.ExoticEasternChewToy;

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            FennecFox fennecFox = Main.LocalPlayer.GetModPlayer<FennecFox>();
            tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.ExoticEasternChewToy")
                        .Replace("<meleeSpd>", (fennecFox.meleeSpdIncrease * 100).ToString())
                        .Replace("<moveSpd>", (fennecFox.speedIncrease * 100).ToString())
                        .Replace("<sizeNerf>", fennecFox.sizeDecrease.ToString())
                        .Replace("<dmg>", (fennecFox.meleeDmg * 100).ToString())
                        ));
        }
    }
}
