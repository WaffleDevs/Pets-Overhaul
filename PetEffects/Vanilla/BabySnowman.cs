using Terraria;
using Terraria.ID;
using PetsOverhaul.Systems;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.Localization;

namespace PetsOverhaul.PetEffects.Vanilla
{
    sealed public class BabySnowman : ModPlayer
    {
        public int frostburnTime = 300;
        public float snowmanSlow = 0.2f;
        public int slowTime = 180;
        GlobalPet Pet { get => Player.GetModPlayer<GlobalPet>(); }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Pet.PetInUseWithSwapCd(ItemID.ToySled))
            {
                if (Player.armor[0].type == ItemID.FrostHelmet && Player.armor[1].type == ItemID.FrostBreastplate && Player.armor[2].type == ItemID.FrostLeggings)
                {
                    target.AddBuff(BuffID.Frostburn2, frostburnTime * 3);
                    target.GetGlobalNPC<NpcPet>().AddSlow(NpcPet.SlowId.Snowman, snowmanSlow * 3, slowTime * 3);
                }
                else
                {
                    target.AddBuff(BuffID.Frostburn2, frostburnTime);
                    target.GetGlobalNPC<NpcPet>().AddSlow(NpcPet.SlowId.Snowman, snowmanSlow, slowTime);
                }
            }
        }

    }
    sealed public class ToySled : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation) => entity.type == ItemID.ToySled;

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            BabySnowman babySnowman = Main.LocalPlayer.GetModPlayer<BabySnowman>();
            tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.ToySled")
                .Replace("<frostburnTime>", (babySnowman.frostburnTime / 60f).ToString())
                .Replace("<slowAmount>", (babySnowman.snowmanSlow * 100).ToString())
                .Replace("<slowTime>", (babySnowman.slowTime / 60f).ToString())
            ));
        }
    }
}
