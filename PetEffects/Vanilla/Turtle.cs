using Terraria;
using Terraria.ID;
using PetsOverhaul.Systems;
using Terraria.ModLoader;
using System.Collections.Generic;
using System;
using Terraria.Localization;

namespace PetsOverhaul.PetEffects.Vanilla
{
    sealed public class Turtle : ModPlayer
    {
        GlobalPet Pet { get => Player.GetModPlayer<GlobalPet>(); }
        public float moveSpd = 0.12f;
        public float def = 1.11f;
        public float kbResist = 0.25f;
        public float dmgReduce = 0.05f;
        public override void PostUpdateEquips()
        {
            if (Pet.PetInUseWithSwapCd(ItemID.Seaweed))
            {
                Player.statDefense.FinalMultiplier *= def;
                Player.moveSpeed -= moveSpd;
                Player.maxRunSpeed -= moveSpd * 3;
                Player.GetDamage<GenericDamageClass>() -= dmgReduce;
            }
        }
        public override void PostUpdateRunSpeeds()
        {
            if (Pet.PetInUseWithSwapCd(ItemID.Seaweed))
            {
                Player.maxRunSpeed -= moveSpd * 3;
            }
        }
        public override void ModifyHurt(ref Player.HurtModifiers modifiers)
        {
            if (Pet.PetInUseWithSwapCd(ItemID.Seaweed))
            {
                modifiers.Knockback *= kbResist;
            }
        }
    }
    sealed public class Seaweed : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation) => entity.type == ItemID.Seaweed;

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            Turtle turtle = ModContent.GetInstance<Turtle>();
            tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.Seaweed")
            .Replace("<def>", turtle.def.ToString())
                        .Replace("<kbResist>", (1 - turtle.kbResist).ToString())
                        .Replace("<moveSpd>", (turtle.moveSpd * 100).ToString())
                        .Replace("<dmg>", (turtle.dmgReduce * 100).ToString())
                        ));
        }
    }
}
