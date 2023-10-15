using Terraria;
using Terraria.ID;
using PetsOverhaul.Systems;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.Localization;

namespace PetsOverhaul.PetEffects.Vanilla
{
    sealed public class MiniMinotaur : ModPlayer
    {
        public int minotaurStack = 0;
        public int minotaurCd = 12;
        public int oocTimer = 600;
        public int maxStack = 80;
        public float meleeSpd = 0.0023f;
        public float meleeDmg = 0.00125f;
        public float moveSpd = 0.0025f;
        public float defMult = 0.002f;

        GlobalPet Pet { get => Player.GetModPlayer<GlobalPet>(); }
        public override void PreUpdate()
        {
            if (Pet.PetInUse(ItemID.TartarSauce))
            {
                Pet.timerMax = minotaurCd;
                oocTimer--;
                if (oocTimer <= 0 && minotaurStack > 0)
                {
                    oocTimer = 15;
                    minotaurStack--;
                }
                if (minotaurStack > maxStack)
                    minotaurStack = maxStack;
            }
        }
        public override void PostUpdateEquips()
        {
            if (Pet.PetInUseWithSwapCd(ItemID.TartarSauce) && minotaurStack > 0)
            {
                Player.GetAttackSpeed<MeleeDamageClass>() += meleeSpd * minotaurStack;
                Player.statDefense.FinalMultiplier *= 1 + defMult * minotaurStack;
                Player.GetDamage<MeleeDamageClass>() += meleeDmg * minotaurStack;
                Player.moveSpeed -= moveSpd * minotaurStack;
            }
        }
        public override void PostUpdateRunSpeeds()
        {
            if (Pet.PetInUseWithSwapCd(ItemID.TartarSauce) && minotaurStack > 0)
            { Player.maxRunSpeed -= moveSpd * minotaurStack * 3; }
        }
        public override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Pet.PetInUseWithSwapCd(ItemID.TartarSauce) && Pet.timer <= 0 && item.CountsAsClass<MeleeDamageClass>())
            {
                if (minotaurStack < 80)
                {
                    minotaurStack += 2;
                }
                if (minotaurStack > 80)
                {
                    minotaurStack = 80;
                }
                Pet.timer = Pet.timerMax;
                oocTimer = 600;
            }
        }
        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Pet.PetInUseWithSwapCd(ItemID.TartarSauce) && Pet.timer <= 0 && proj.CountsAsClass<MeleeDamageClass>())
            {
                if (minotaurStack < 80)
                {
                    minotaurStack += 1;
                }
                if (minotaurStack > 80)
                {
                    minotaurStack = 80;
                }
                Pet.timer = Pet.timerMax;
                oocTimer = 600;
            }
        }
    }
    sealed public class TartarSauce : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation) => entity.type == ItemID.TartarSauce;

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            MiniMinotaur miniMinotaur = Main.LocalPlayer.GetModPlayer<MiniMinotaur>();
            tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.TartarSauce")
                        .Replace("<cooldown>", (miniMinotaur.minotaurCd / 60f).ToString())
                        .Replace("<maxStack>", miniMinotaur.maxStack.ToString())
                        .Replace("<maxDef>", (miniMinotaur.defMult * 100 * miniMinotaur.maxStack).ToString())
                        .Replace("<maxMeleeSpd>", (miniMinotaur.meleeSpd * 100 * miniMinotaur.maxStack).ToString())
                        .Replace("<maxDmg>", (miniMinotaur.meleeDmg * 100 * miniMinotaur.maxStack).ToString())
                        .Replace("<maxSpd>", (miniMinotaur.moveSpd * 100 * miniMinotaur.maxStack).ToString())
                        .Replace("<meleeSpd>", (miniMinotaur.meleeSpd * 100).ToString())
                        .Replace("<moveSpd>", (miniMinotaur.moveSpd * 100).ToString())
                        .Replace("<dmg>", (miniMinotaur.meleeDmg * 100).ToString())
                        .Replace("<def>", (miniMinotaur.defMult * 100).ToString())
                        ));
        }
    }
}
