using Terraria;
using Terraria.ID;
using PetsOverhaul.Systems;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.Localization;

namespace PetsOverhaul.PetEffects.Vanilla
{
    sealed public class EverscreamSapling : ModPlayer
    {
        public int cooldown = 240;
        public float critMult = 0.6f;
        public float dmgIncr = 0.3f;
        public float howMuchCrit = 10f;
        public float missingManaPercent = 0.12f;
        public int flatRecovery = 5;
        public int manaIncrease = 100;
        GlobalPet Pet { get => Player.GetModPlayer<GlobalPet>(); }
        public override void PreUpdate()
        {
            if (Pet.PetInUse(ItemID.EverscreamPetItem))
                Pet.timerMax = cooldown;
        }
        public override void PostUpdateEquips()
        {
            if (Pet.PetInUseWithSwapCd(ItemID.EverscreamPetItem))
            {
                Player.statManaMax2 += manaIncrease;
                Player.GetCritChance<MagicDamageClass>() *= critMult;
                float currentMana = Player.statMana;
                float maxMana = Player.statManaMax2;
                float dmgBoost = currentMana / maxMana; //c# eğer float olduğu spesifik olarak belirtilmezse küçük sayıdan büyük sayıyı bölerken
                Player.GetDamage<MagicDamageClass>() += dmgBoost * dmgIncr; //2 integer olarak alıp bölme işlemini 0 döndürüyor
                Player.GetCritChance<MagicDamageClass>() += dmgBoost * howMuchCrit;
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Pet.PetInUseWithSwapCd(ItemID.EverscreamPetItem) && Pet.LifestealCheck(target) && hit.Crit && Pet.timer <= 0)
            {
                Pet.Lifesteal(Player.statManaMax2 - Player.statMana, missingManaPercent, flatRecovery, true, false);
                Pet.timer = Pet.timerMax;
            }
        }
    }
    sealed public class EverscreamPetItem : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation) => entity.type == ItemID.EverscreamPetItem;

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            EverscreamSapling everscreamSapling = Main.LocalPlayer.GetModPlayer<EverscreamSapling>();
            tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.EverscreamPetItem")
                        .Replace("<magicCritNerf>", everscreamSapling.critMult.ToString())
                        .Replace("<maxMana>", everscreamSapling.manaIncrease.ToString())
                        .Replace("<missingMana>", (everscreamSapling.missingManaPercent * 100).ToString())
                        .Replace("<flatMana>", everscreamSapling.flatRecovery.ToString())
                        .Replace("<manaRecoveryCd>", (everscreamSapling.cooldown / 60f).ToString())
                        .Replace("<dmg>", (everscreamSapling.dmgIncr * 100).ToString())
                        .Replace("<crit>", everscreamSapling.howMuchCrit.ToString())
                        ));
        }
    }
}
