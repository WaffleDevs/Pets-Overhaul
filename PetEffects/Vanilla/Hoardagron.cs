using Terraria;
using Terraria.ID;
using PetsOverhaul.Systems;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using System.Collections.Generic;
using Terraria.Localization;

namespace PetsOverhaul.PetEffects.Vanilla
{
    sealed public class Hoardagron : ModPlayer
    {
        public bool arrow = false;
        public bool specialist = false;
        public float arrowSpd = 0.9f;
        public float bulletSpd = 1.8f;
        public float specialTreshold = 0.2f;
        public float specialBossTreshold = 0.06f;
        public int arrowPen = 1;
        public GlobalPet Pet { get => Player.GetModPlayer<GlobalPet>(); }
        public override void ModifyShootStats(Item item, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (Pet.PetInUseWithSwapCd(ItemID.DD2PetDragon))
            {

                if (AmmoID.Sets.IsArrow[item.useAmmo])
                {
                    arrow = true;
                    velocity *= arrowSpd;
                }
                else
                    arrow = false;
                if (AmmoID.Sets.IsBullet[item.useAmmo])
                    velocity *= bulletSpd;

                if (AmmoID.Sets.IsSpecialist[item.useAmmo])
                {
                    specialist = true;
                }
                else
                    specialist = false;
            }
        }
        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref NPC.HitModifiers modifiers)
        {
            if (Pet.PetInUseWithSwapCd(ItemID.DD2PetDragon) && proj.GetGlobalProjectile<HoardagronProj>().special)
            {
                if ((target.boss == true || target.GetGlobalNPC<NpcPet>().nonBossTrueBosses[target.type]) && target.life < (int)(target.lifeMax * specialBossTreshold))
                    modifiers.SetCrit();
                else if (target.life < (int)(target.lifeMax * specialTreshold))
                    modifiers.SetCrit();
            }
        }
    }
    sealed public class HoardagronProj : GlobalProjectile
    {
        public override bool InstancePerEntity => true;
        public bool special;
        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            Hoardagron player = Main.player[projectile.owner].GetModPlayer<Hoardagron>();

            if (player.Pet.PetInUseWithSwapCd(ItemID.DD2PetDragon))
            {
                if (player.arrow && projectile.penetrate >= 0)
                {
                    projectile.penetrate += player.arrowPen;
                }
                if (player.specialist)
                    special = true;
                else
                    special = false;
            }
        }
    }
    sealed public class DD2PetDragon : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation) => entity.type == ItemID.DD2PetDragon;

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            Hoardagron hoardagron = Main.LocalPlayer.GetModPlayer<Hoardagron>();
            tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.DD2PetDragon")
                        .Replace("<arrowVelo>", hoardagron.arrowSpd.ToString())
                        .Replace("<arrowPierce>", hoardagron.arrowPen.ToString())
                        .Replace("<bulletVelo>", hoardagron.bulletSpd.ToString())
                        .Replace("<treshold>", (hoardagron.specialTreshold * 100).ToString())
                        .Replace("<bossTreshold>", (hoardagron.specialBossTreshold * 100).ToString())
                        ));
        }
    }
}
