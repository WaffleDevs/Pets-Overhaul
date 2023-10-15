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
    sealed public class SpiderBrain : ModPlayer
    {
        public int lifePool = 0;
        public float lifePoolMaxPerc = 0.1f;
        public int cdDoAddToPool = 25;
        public float lifestealAmount = 0.065f;
        GlobalPet Pet { get => Player.GetModPlayer<GlobalPet>(); }
        public override void PreUpdate()
        {
            if (Pet.PetInUse(ItemID.BrainOfCthulhuPetItem))
            {
                Pet.timerMax = cdDoAddToPool;
            }
        }
        public override void PreUpdateBuffs()
        {
            if (Pet.PetInUse(ItemID.BrainOfCthulhuPetItem) && Pet.timer <= 0 && lifePool <= Player.statLifeMax2 * lifePoolMaxPerc)
            {
                lifePool++;
                Pet.timer = Pet.timerMax;
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Pet.PetInUseWithSwapCd(ItemID.BrainOfCthulhuPetItem) && Pet.LifestealCheck(target))
            {
                int decreaseFromPool = Pet.Lifesteal(damageDone, lifestealAmount, doLifesteal: false);
                if (decreaseFromPool >= lifePool)
                {
                    Pet.Lifesteal(lifePool, 1f);
                    lifePool = 0;
                }
                else
                {
                    lifePool -= decreaseFromPool;
                    Pet.Lifesteal(decreaseFromPool, 1f);
                }
            }
        }
    }
    sealed public class BrainOfCthulhuPetItem : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation) => entity.type == ItemID.BrainOfCthulhuPetItem;

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (ModContent.GetInstance<Personalization>().TooltipsEnabledWithShift && !PlayerInput.Triggers.Current.KeyStatus[TriggerNames.Down]) return;
            SpiderBrain spiderBrain = Main.LocalPlayer.GetModPlayer<SpiderBrain>();
            tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.BrainOfCthulhuPetItem")
                        .Replace("<lifesteal>", (spiderBrain.lifestealAmount * 100).ToString())
                        .Replace("<maxPool>", (spiderBrain.lifePoolMaxPerc * 100).ToString())
                        .Replace("<healthRecovery>", (spiderBrain.cdDoAddToPool / 60f).ToString())
                        ));
        }
    }
}
