using Terraria;
using Terraria.ID;
using PetsOverhaul.Systems;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.Localization;

namespace PetsOverhaul.PetEffects.Vanilla
{
    sealed public class PropellerGato : ModPlayer
    {
        public int bonusCritChance = 10;
        public int turretIncrease = 1;
        GlobalPet Pet { get => Player.GetModPlayer<GlobalPet>(); }
        public override void PostUpdateEquips()
        {
            if (Pet.PetInUseWithSwapCd(ItemID.DD2PetGato))
            {
                Player.maxTurrets++;
            }
        }
        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref NPC.HitModifiers modifiers)
        {
            if (Pet.PetInUseWithSwapCd(ItemID.DD2PetGato) && proj.GetGlobalProjectile<ProjectileSourceChecks>().isFromSentry)
            {
                if (proj.CritChance + bonusCritChance >= 100)
                {
                    modifiers.SetCrit();
                }
                else if (Main.rand.NextBool(proj.CritChance + bonusCritChance, 100))
                {
                    modifiers.SetCrit();
                }
                else
                {
                    modifiers.DisableCrit();
                }
            }
        }
    }
    sealed public class DD2PetGato : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation) => entity.type == ItemID.DD2PetGato;

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            PropellerGato propellerGato = Main.LocalPlayer.GetModPlayer<PropellerGato>();
            tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.DD2PetGato")
                        .Replace("<crit>", propellerGato.bonusCritChance.ToString())
                        .Replace("<maxSentry>", propellerGato.turretIncrease.ToString())
                        ));
        }
    }
}
