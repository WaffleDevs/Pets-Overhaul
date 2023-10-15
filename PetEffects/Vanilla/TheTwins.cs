using Terraria;
using Terraria.ID;
using PetsOverhaul.Systems;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.Localization;

namespace PetsOverhaul.PetEffects.Vanilla
{
    sealed public class TheTwins : ModPlayer
    {
        public int healthDmgCd = 42;
        public int closeRange = 112;
        public int longRange = 560;
        public float defLifestealDmgMult = 0.0001f;
        public float regularEnemyHpDmg = 0.01f;
        public float bossHpDmg = 0.001f;
        public int infernoTime = 240;
        public float defMult = 1.5f;
        GlobalPet Pet { get => Player.GetModPlayer<GlobalPet>(); }
        public override void PreUpdate()
        {
            if (Pet.PetInUse(ItemID.TwinsPetItem))
            {
                Pet.timerMax = healthDmgCd;
            }
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (Pet.PetInUseWithSwapCd(ItemID.TwinsPetItem))
            {
                if (Player.Distance(target.Center) > longRange && Pet.timer <= 0)
                {
                    if (target.boss == false || target.GetGlobalNPC<NpcPet>().nonBossTrueBosses[target.type] == false)
                    {
                        modifiers.FlatBonusDamage += (int)(target.lifeMax * regularEnemyHpDmg);
                    }
                    else
                    {
                        modifiers.FlatBonusDamage += (int)(target.lifeMax * bossHpDmg);
                    }
                    Pet.timer = Pet.timerMax;
                }

            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Player.Distance(target.Center) < closeRange && Pet.LifestealCheck(target) && Pet.PetInUseWithSwapCd(ItemID.TwinsPetItem))
            {
                target.AddBuff(BuffID.CursedInferno, infernoTime);
                Pet.Lifesteal(Player.statDefense * defMult * (Player.endurance + 1f), hit.Damage * defLifestealDmgMult);

            }
        }
    }
    sealed public class TwinsPetItem : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation) => entity.type == ItemID.TwinsPetItem;

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            TheTwins theTwins = Main.LocalPlayer.GetModPlayer<TheTwins>();
            tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.TwinsPetItem")
                        .Replace("<closeRange>", (theTwins.closeRange / 16f).ToString())
                        .Replace("<cursedTime>", (theTwins.infernoTime / 60f).ToString())
                        .Replace("<defLifesteal>", theTwins.defMult.ToString())
                        .Replace("<dealtDmgLifesteal>", (theTwins.defLifestealDmgMult * 100).ToString())
                        .Replace("<longRange>", (theTwins.longRange / 16f).ToString())
                        .Replace("<hpDmg>", (theTwins.regularEnemyHpDmg * 100).ToString())
                        .Replace("<bossHpDmg>", (theTwins.bossHpDmg * 100).ToString())
                        .Replace("<hpDmgCooldown>", (theTwins.healthDmgCd / 60f).ToString())
                        ));
        }
    }
}
