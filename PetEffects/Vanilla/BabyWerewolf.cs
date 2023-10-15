using Terraria;
using Terraria.ID;
using PetsOverhaul.Systems;
using Terraria.ModLoader;
using PetsOverhaul.Buffs;
using System.Collections.Generic;
using Terraria.Localization;

namespace PetsOverhaul.PetEffects.Vanilla
{
    sealed public class BabyWerewolf : ModPlayer
    {
        public float critDmgReduction = 0.3f;
        public float critChance = 1.2f;
        public float damageMultPerStack = 0.02f;
        public float maulCritDmgIncrease = 0.006f;
        public int maxStacks = 15;
        public int debuffLength = 1200;
        GlobalPet Pet { get => Player.GetModPlayer<GlobalPet>(); }
        public override void PostUpdateEquips()
        {
            if (Pet.PetInUse(ItemID.FullMoonSqueakyToy) && Main.moonPhase == 0)
            {
                Player.wereWolf = true;
                Player.forceWerewolf = true;
                Player.npcTypeNoAggro[NPCID.Werewolf] = true;
            }
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (target.GetGlobalNPC<NpcPet>().maulCounter > maxStacks)
                target.GetGlobalNPC<NpcPet>().maulCounter = maxStacks;
            if (target.HasBuff(ModContent.BuffType<Mauled>()))
                modifiers.FinalDamage *= target.GetGlobalNPC<NpcPet>().maulCounter * 0.02f + 1;
            else
                target.GetGlobalNPC<NpcPet>().maulCounter = 0;
            if (Pet.PetInUseWithSwapCd(ItemID.FullMoonSqueakyToy))
            {
                modifiers.CritDamage -= critDmgReduction - target.GetGlobalNPC<NpcPet>().maulCounter * maulCritDmgIncrease;
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Pet.PetInUseWithSwapCd(ItemID.FullMoonSqueakyToy) && hit.Crit)
            {
                target.AddBuff(ModContent.BuffType<Mauled>(), debuffLength);
                target.GetGlobalNPC<NpcPet>().maulCounter++;
            }
        }
        public override void ModifyWeaponCrit(Item item, ref float crit)
        {
            if (Pet.PetInUseWithSwapCd(ItemID.FullMoonSqueakyToy))
            {
                crit *= critChance;
            }
        }
    }
    sealed public class FullMoonSqueakyToy : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation) => entity.type == ItemID.FullMoonSqueakyToy;

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            BabyWerewolf babyWerewolf = ModContent.GetInstance<BabyWerewolf>();
            tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.FullMoonSqueakyToy")
                .Replace("<critMult>", babyWerewolf.critChance.ToString())
                .Replace("<crDmgReduction>", (babyWerewolf.critDmgReduction * 100).ToString())
                .Replace("<maxStacks>", babyWerewolf.maxStacks.ToString())
                .Replace("<stackDmg>", (babyWerewolf.damageMultPerStack * 100).ToString())
                .Replace("<stackCritDmg>", (babyWerewolf.maulCritDmgIncrease * 100).ToString())
            ));
        }
    }
}
