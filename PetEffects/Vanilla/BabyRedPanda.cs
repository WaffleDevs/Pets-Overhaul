using Terraria;
using Terraria.ID;
using PetsOverhaul.Systems;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.Localization;

namespace PetsOverhaul.PetEffects.Vanilla
{
    sealed public class BabyRedPanda : ModPlayer
    {
        GlobalPet Pet { get => Player.GetModPlayer<GlobalPet>(); }
        public int aggroReduce = 500;
        public float regularAtkSpd = 0.06f;
        public float jungleBonusSpd = 0.04f;
        public int bambooChance = 50;
        public override void PostUpdateEquips()
        {
            if (Pet.PetInUseWithSwapCd(ItemID.BambooLeaf))
            {
                Player.aggro -= aggroReduce;
                Player.GetAttackSpeed<GenericDamageClass>() += regularAtkSpd;
                if (Player.ZoneJungle)
                    Player.GetAttackSpeed<GenericDamageClass>() += jungleBonusSpd;
            }
        }
        public override bool OnPickup(Item item)
        {
            if (item.TryGetGlobalItem(out ItemPet itemPet) && Pet.PickupChecks(item, ItemID.BambooLeaf, itemPet) && itemPet.bambooBoost)
            {
                item.stack += ItemPet.Randomizer(bambooChance * item.stack);
            }
            return true;
        }
    }
    sealed public class BambooLeaf : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation) => entity.type == ItemID.BambooLeaf;

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            BabyRedPanda babyRedPanda = Main.LocalPlayer.GetModPlayer<BabyRedPanda>();
            tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.BambooLeaf")
                .Replace("<atkSpd>", (babyRedPanda.regularAtkSpd * 100).ToString())
                .Replace("<jungleAtkSpd>", (babyRedPanda.jungleBonusSpd * 100).ToString())
                .Replace("<aggro>", babyRedPanda.aggroReduce.ToString())
                .Replace("<bambooChance>", babyRedPanda.bambooChance.ToString())
            ));
        }
    }
}
