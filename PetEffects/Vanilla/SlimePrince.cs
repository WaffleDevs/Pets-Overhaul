using Terraria;
using Terraria.ID;
using PetsOverhaul.Systems;
using Terraria.ModLoader;
using Terraria.DataStructures;
using System.Collections.Generic;
using Terraria.Localization;

namespace PetsOverhaul.PetEffects.Vanilla
{
    sealed public class SlimePrince : ModPlayer
    {
        public float wetSpeed = 0.10f;
        public float wetDmg = 0.08f;
        public float wetDef = 0.08f;
        public float slimyKb = 1.5f;
        public float slimyJump = 2f;
        public float wetDealtLower = 0.93f;
        public float wetRecievedHigher = 1.08f;
        public float bonusKb = 1.5f;
        public float healthDmg = 0.015f;
        public int burnCap = 50;
        public Player slimeDual;
        public Player slimePrince;
        GlobalPet Pet { get => Player.GetModPlayer<GlobalPet>(); }
        public override void PostUpdateEquips()
        {
            if (GlobalPet.KingSlimePetActive(out slimePrince) && Player.HasBuff(BuffID.Wet)) //Burda kullanan oyuncunun statlarını almıyor.
            {
                Player.moveSpeed += slimePrince.GetModPlayer<SlimePrince>().wetSpeed;
                Player.GetDamage<GenericDamageClass>() += slimePrince.GetModPlayer<SlimePrince>().wetDmg;
                Player.statDefense *= slimePrince.GetModPlayer<SlimePrince>().wetDef;
            }
            else if (GlobalPet.DualSlimePetActive(out slimeDual) && Player.HasBuff(BuffID.Wet))//Burda kullanan oyuncunun statlarını almıyor.
            {
                Player.moveSpeed += slimeDual.GetModPlayer<DualSlime>().wetSpeed;
                Player.GetDamage<GenericDamageClass>() += slimeDual.GetModPlayer<DualSlime>().wetDmg;
                Player.statDefense *= slimeDual.GetModPlayer<DualSlime>().wetDef;
            }
            if (GlobalPet.KingSlimePetActive(out slimePrince) && Player.HasBuff(BuffID.Slimed))
            {
                Player.GetKnockback<GenericDamageClass>() *= slimePrince.GetModPlayer<SlimePrince>().slimyKb;
                Player.jumpSpeedBoost += slimePrince.GetModPlayer<SlimePrince>().slimyJump;
            }
            else if (GlobalPet.DualSlimePetActive(out slimeDual) && Player.HasBuff(BuffID.Slimed))
            {
                Player.GetKnockback<GenericDamageClass>() *= slimeDual.GetModPlayer<DualSlime>().slimyKb;
                Player.jumpSpeedBoost += slimeDual.GetModPlayer<DualSlime>().slimyJump;
            }
        }
    }
    sealed public class SlimePrinceandDualNpc : GlobalNPC
    {
        public Player slimeDual;
        public Player slimePrince;
        public override bool InstancePerEntity => true;
        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (GlobalPet.KingSlimePetActive(out slimePrince) && npc.HasBuff(BuffID.Slimed) && (npc.HasBuff(BuffID.Burning) || npc.HasBuff(BuffID.OnFire) || npc.HasBuff(BuffID.OnFire3) || npc.HasBuff(BuffID.Frostburn) || npc.HasBuff(BuffID.CursedInferno) || npc.HasBuff(BuffID.ShadowFlame) || npc.HasBuff(BuffID.Frostburn2)))
            {
                if (npc.lifeMax * slimePrince.GetModPlayer<SlimePrince>().healthDmg > slimePrince.GetModPlayer<SlimePrince>().burnCap)
                {
                    npc.lifeRegen -= slimePrince.GetModPlayer<SlimePrince>().burnCap;
                }
                else
                {
                    npc.lifeRegen -= (int)(npc.lifeMax * slimePrince.GetModPlayer<SlimePrince>().healthDmg);
                }
            }
            else if (GlobalPet.DualSlimePetActive(out slimeDual) && npc.HasBuff(BuffID.Slimed) && (npc.HasBuff(BuffID.Burning) || npc.HasBuff(BuffID.OnFire) || npc.HasBuff(BuffID.OnFire3) || npc.HasBuff(BuffID.Frostburn) || npc.HasBuff(BuffID.CursedInferno) || npc.HasBuff(BuffID.ShadowFlame) || npc.HasBuff(BuffID.Frostburn2)))
            {
                if (npc.lifeMax * slimeDual.GetModPlayer<DualSlime>().healthDmg > slimeDual.GetModPlayer<DualSlime>().burnCap)
                {
                    npc.lifeRegen -= slimeDual.GetModPlayer<DualSlime>().burnCap;
                }
                else
                {
                    npc.lifeRegen -= (int)(npc.lifeMax * slimeDual.GetModPlayer<DualSlime>().healthDmg);
                }
            }
        }
        public override void ModifyHitPlayer(NPC npc, Player target, ref Player.HurtModifiers modifiers)
        {
            if (GlobalPet.KingSlimePetActive(out slimePrince) && npc.HasBuff(BuffID.Wet))
            {
                modifiers.FinalDamage *= slimePrince.GetModPlayer<SlimePrince>().wetDealtLower;
            }
            else if (GlobalPet.DualSlimePetActive(out slimeDual) && npc.HasBuff(BuffID.Wet))
            {
                modifiers.FinalDamage *= slimeDual.GetModPlayer<DualSlime>().wetDealtLower;
            }
        }
        public override void ModifyHitByItem(NPC npc, Player player, Item item, ref NPC.HitModifiers modifiers)
        {
            if (GlobalPet.KingSlimePetActive(out slimePrince) && npc.HasBuff(BuffID.Wet))
            {
                modifiers.FinalDamage *= slimePrince.GetModPlayer<SlimePrince>().wetRecievedHigher;
            }
            else if (GlobalPet.DualSlimePetActive(out slimeDual) && npc.HasBuff(BuffID.Wet))
            {
                modifiers.FinalDamage *= slimeDual.GetModPlayer<DualSlime>().wetRecievedHigher;
            }
            if (GlobalPet.KingSlimePetActive(out slimePrince) && npc.HasBuff(BuffID.Slimed))
            {
                modifiers.Knockback *= slimePrince.GetModPlayer<SlimePrince>().bonusKb;
            }
            else if (GlobalPet.DualSlimePetActive(out slimeDual) && npc.HasBuff(BuffID.Slimed))
            {
                modifiers.Knockback *= slimeDual.GetModPlayer<DualSlime>().bonusKb;
            }
        }
        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref NPC.HitModifiers modifiers)
        {
            if (GlobalPet.KingSlimePetActive(out slimePrince) && npc.HasBuff(BuffID.Wet))
            {
                modifiers.FinalDamage *= slimePrince.GetModPlayer<SlimePrince>().wetRecievedHigher;
            }
            else if (GlobalPet.DualSlimePetActive(out slimeDual) && npc.HasBuff(BuffID.Wet))
            {
                modifiers.FinalDamage *= slimeDual.GetModPlayer<DualSlime>().wetRecievedHigher;
            }
            if (GlobalPet.KingSlimePetActive(out slimePrince) && npc.HasBuff(BuffID.Slimed))
            {
                modifiers.Knockback *= slimePrince.GetModPlayer<SlimePrince>().bonusKb;
            }
            else if (GlobalPet.DualSlimePetActive(out slimeDual) && npc.HasBuff(BuffID.Slimed))
            {
                modifiers.Knockback *= slimeDual.GetModPlayer<DualSlime>().bonusKb;
            }
        }
    }
    sealed public class PrinceSlimeandDualSlimeEnemyProj : GlobalProjectile
    {
        public override bool InstancePerEntity => true;
        public bool fromNpcAndWet = false;
        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            if (source is EntitySource_Parent parent3 && parent3.Entity is NPC npc && npc.HasBuff(BuffID.Wet))
            {
                fromNpcAndWet = true;
            }
            else
            {
                fromNpcAndWet = false;
            }
        }
        public override void ModifyHitPlayer(Projectile projectile, Player target, ref Player.HurtModifiers modifiers)
        {
            if (fromNpcAndWet == true)
            {
                if (GlobalPet.KingSlimePetActive(out Player prince))
                {
                    modifiers.FinalDamage *= prince.GetModPlayer<SlimePrince>().wetDealtLower;
                }
                else if (GlobalPet.DualSlimePetActive(out Player dual))
                {
                    modifiers.FinalDamage *= dual.GetModPlayer<DualSlime>().wetDealtLower;
                }
            }
        }
    }
    sealed public class KingSlimePetItem : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation) => entity.type == ItemID.KingSlimePetItem;

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            SlimePrince slimePrince = Main.LocalPlayer.GetModPlayer<SlimePrince>();
            tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.KingSlimePetItem")
                        .Replace("<burnHp>", (slimePrince.healthDmg * 100).ToString())
                        .Replace("<burnCap>", slimePrince.burnCap.ToString())
                        .Replace("<extraKb>", slimePrince.bonusKb.ToString())
                        .Replace("<jumpSpd>", (slimePrince.slimyJump * 100).ToString())
                        .Replace("<kbBoost>", slimePrince.slimyKb.ToString())
                        .Replace("<enemyDmgRecieve>", slimePrince.wetRecievedHigher.ToString())
                        .Replace("<enemyDmgDeal>", slimePrince.wetDealtLower.ToString())
                        .Replace("<dmg>", (slimePrince.wetDmg * 100).ToString())
                        .Replace("<def>", (slimePrince.wetDef * 100).ToString())
                        .Replace("<moveSpd>", (slimePrince.wetSpeed * 100).ToString())
                        ));
        }
    }
}
