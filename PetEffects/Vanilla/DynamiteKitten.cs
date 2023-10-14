using Terraria;
using Terraria.ID;
using PetsOverhaul.Systems;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using PetsOverhaul.Projectiles;

namespace PetsOverhaul.PetEffects.Vanilla
{
    sealed public class DynamiteKitten : ModPlayer
    {
        public int cooldown = 120;
        public float damageMult = 0.6f;
        public float kbMult = 1.7f;
        public int armorPen = 15;
        GlobalPet Pet { get => Player.GetModPlayer<GlobalPet>(); }
        public override void PreUpdate()
        {
            if (Pet.PetInUse(ItemID.BallOfFuseWire))
                Pet.timerMax = cooldown;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Pet.PetInUseWithSwapCd(ItemID.BallOfFuseWire) && Pet.timer <= 0)
            {
                int boom = Projectile.NewProjectile(Player.GetSource_Misc("DynamiteKitten"), target.position, Vector2.Zero, ModContent.ProjectileType<DynamiteKittyBoom>(), (int)(damageDone * damageMult), hit.Knockback * kbMult, Main.myPlayer);
                Main.projectile[boom].ArmorPenetration = armorPen;
                Pet.timer = Pet.timerMax;
            }
        }
    }
}
