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
    sealed public class SharkPup : ModPlayer
    {
        public float seaCreatureResist = 0.8f;
        public float seaCreatureDamage = 1.2f;
        public int shieldOnCatch = 5;
        public int shieldTime = 600;
        public int fishingPow = 10;
        GlobalPet Pet { get => Player.GetModPlayer<GlobalPet>(); }
        public override void ModifyHitByNPC(NPC npc, ref Player.HurtModifiers modifiers)
        {
            if (Pet.PetInUseWithSwapCd(ItemID.SharkBait) && npc.GetGlobalNPC<NpcPet>().seaCreature)
            {
                modifiers.FinalDamage *= seaCreatureResist;
            }
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (Pet.PetInUseWithSwapCd(ItemID.SharkBait) && target.GetGlobalNPC<NpcPet>().seaCreature)
            {
                modifiers.FinalDamage *= seaCreatureDamage;
            }
        }
        public override void UpdateEquips()
        {
            if (Pet.PetInUse(ItemID.SharkBait))
            {
                Player.fishingSkill += fishingPow;
            }
        }
        public override void ModifyCaughtFish(Item fish)
        {
            if (Pet.PetInUseWithSwapCd(ItemID.SharkBait))
            {
                if (shieldOnCatch >= Pet.shieldAmount)
                {
                    Pet.shieldAmount = shieldOnCatch;
                    Pet.shieldTimer = shieldTime;
                }
            }
        }
    }
    sealed public class SharkBait : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation) => entity.type == ItemID.SharkBait;

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (ModContent.GetInstance<Personalization>().TooltipsEnabledWithShift && !PlayerInput.Triggers.Current.KeyStatus[TriggerNames.Down]) return;
            SharkPup sharkPup = Main.LocalPlayer.GetModPlayer<SharkPup>();
            tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.SharkBait")
                        .Replace("<fishingPower>", sharkPup.fishingPow.ToString())
                        .Replace("<seaCreatureDmg>", sharkPup.seaCreatureDamage.ToString())
                        .Replace("<seaCreatureResist>", sharkPup.seaCreatureResist.ToString())
                        .Replace("<shield>", sharkPup.shieldOnCatch.ToString())
                        .Replace("<shieldTime>", (sharkPup.shieldTime / 60f).ToString())
                        ));
        }
    }
}
