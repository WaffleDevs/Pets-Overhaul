using Terraria;
using Terraria.ID;
using PetsOverhaul.Systems;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.Localization;
using Terraria.GameInput;
using PetsOverhaul.Config;

using PetsOverhaul.Config;
using Terraria.GameInput;

namespace PetsOverhaul.PetEffects.Vanilla
{
    sealed public class Glommer : ModPlayer
    {
        public int glommerSanityTime = 45;
        public int glommerSanityRecover = 1;
        public float glommerSanityAura = 0.2f;
        public int glommerSanityRange = 4000;
        GlobalPet Pet { get => Player.GetModPlayer<GlobalPet>(); }
        public override void PreUpdate()
        {
            if (Pet.PetInUse(ItemID.GlommerPetItem))
                Pet.timerMax = glommerSanityTime;

        }
        public override void PostUpdateEquips()
        {
            if (Pet.PetInUse(ItemID.GlommerPetItem) && Main.rand.NextBool(18000))
                Player.QuickSpawnItem(Player.GetSource_Misc("Glommer"), ItemID.PoopBlock);
            if (Pet.PetInUseWithSwapCd(ItemID.GlommerPetItem))
            {
                for (int i = 0; i < Main.maxPlayers; i++)
                {
                    Player plr = Main.player[i];
                    if (Player.Distance(plr.Center) < glommerSanityRange && plr.active && plr.whoAmI != 255)
                        plr.GetModPlayer<GlobalPet>().abilityHaste += glommerSanityAura;
                }
                Player.statManaMax2 += (int)(Pet.abilityHaste * Player.statManaMax2);
                if (Pet.timer <= 0 && Player.statMana != Player.statManaMax2)
                {
                    Player.statMana += glommerSanityRecover;
                    Pet.timer = Pet.timerMax;
                }
            }
        }
    }
    sealed public class GlommerPetItem : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation) => entity.type == ItemID.GlommerPetItem;

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (ModContent.GetInstance<Personalization>().TooltipsEnabledWithShift && !PlayerInput.Triggers.Current.KeyStatus[TriggerNames.Down]) return;
            Glommer glommer = Main.LocalPlayer.GetModPlayer<Glommer>();
            tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.GlommerPetItem")
                        .Replace("<sanityAmount>", (glommer.glommerSanityAura * 100).ToString())
                        .Replace("<sanityRange>", (glommer.glommerSanityRange / 16f).ToString())
                        .Replace("<manaRecover>", glommer.glommerSanityRecover.ToString())
                        .Replace("<manaRecoverCd>", (glommer.glommerSanityTime / 60f).ToString())
                        ));
        }
    }
}
