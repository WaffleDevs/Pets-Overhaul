using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using PetsOverhaul.Systems;
using System.Collections.Generic;
using Terraria.Localization;

using PetsOverhaul.Config;
using Terraria.GameInput;

namespace PetsOverhaul.PetEffects.Vanilla
{
    sealed public class DungeonGuardian : ModPlayer
    {
        GlobalPet Pet { get => Player.GetModPlayer<GlobalPet>(); }
        public int armorPen = 5;
        public int dungArmorPenBonus = 3;
        public int lifeRegen = 8;
        public override void PostUpdateEquips()
        {
            if (Pet.PetInUseWithSwapCd(ItemID.BoneKey))
            {
                Player.npcTypeNoAggro[NPCID.AngryBones] = true;
                Player.npcTypeNoAggro[NPCID.AngryBonesBig] = true;
                Player.npcTypeNoAggro[NPCID.AngryBonesBigHelmet] = true;
                Player.npcTypeNoAggro[NPCID.AngryBonesBigMuscle] = true;
                Player.npcTypeNoAggro[NPCID.FromNetId(NPCID.ShortBones)] = true;
                Player.npcTypeNoAggro[NPCID.FromNetId(NPCID.BigBoned)] = true;
                Player.npcTypeNoAggro[NPCID.DarkCaster] = true;
                Player.npcTypeNoAggro[NPCID.DungeonSlime] = true;
                Player.npcTypeNoAggro[NPCID.CursedSkull] = true;
                Player.npcTypeNoAggro[NPCID.BlazingWheel] = true;
                Player.npcTypeNoAggro[NPCID.SpikeBall] = true;
                Player.npcTypeNoAggro[NPCID.WaterSphere] = true;
                if (Player.ZoneDungeon == true)
                {
                    Player.AddImmuneTime(ImmunityCooldownID.TileContactDamage, 1);
                    Player.buffImmune[BuffID.Bleeding] = true;
                    Player.GetArmorPenetration<GenericDamageClass>() += dungArmorPenBonus;
                    Player.lifeRegen += lifeRegen;
                }
                Player.GetArmorPenetration<GenericDamageClass>() += armorPen;
            }
        }
    }
    sealed public class BoneKey : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation) => entity.type == ItemID.BoneKey;

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (ModContent.GetInstance<Personalization>().TooltipsEnabledWithShift && !PlayerInput.Triggers.Current.KeyStatus[TriggerNames.Down]) return;
            DungeonGuardian dungeonGuardian = Main.LocalPlayer.GetModPlayer<DungeonGuardian>();
            tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.BoneKey")
                        .Replace("<armorPen>", dungeonGuardian.armorPen.ToString())
                        .Replace("<dungArmorPen>", dungeonGuardian.dungArmorPenBonus.ToString())
                        .Replace("<dungRegen>", dungeonGuardian.lifeRegen.ToString())
                        ));
        }
    }
}
