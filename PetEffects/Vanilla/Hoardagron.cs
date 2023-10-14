using Terraria;
using Terraria.ID;
using PetsOverhaul.Systems;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

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

}
