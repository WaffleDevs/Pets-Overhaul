using Microsoft.Xna.Framework;
using PetsOverhaul.Config;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using PetsOverhaul.Systems;

namespace PetsOverhaul.PetEffects
{
    sealed public class PhantasmalDragon : ModPlayer
    {
        GlobalPet Pet { get => Player.GetModPlayer<GlobalPet>(); }
        public int phantasmDragonCooldown = 120;
        public int iceFlat = 125;
        public float iceMult = 0.2f;
        public int IceDamage(int damageDone)
        {
            int dmg = (int)(damageDone * iceMult)+iceFlat;
            return dmg;
        }
        public int fireFlat = 100;
        public float fireMult = 0.15f;
        public int FireDamage(int damageDone)
        {
            int dmg = (int)(damageDone * fireMult) + fireFlat;
            return dmg;
        }
        public int lightFlat = 50;
        public float lightMult = 0.1f;
        public int LightDamage(int damageDone)
        {
            int dmg = (int)(damageDone * lightMult) + lightFlat;
            return dmg;
        }
        public int icePierce = 25;
        public int lightPierce = 10;
        public override void PreUpdate()
        {
            if (Pet.PetInUse(ItemID.LunaticCultistPetItem))
                Pet.timerMax= phantasmDragonCooldown;
        }
        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Pet.PetInUseWithSwapCd(ItemID.LunaticCultistPetItem) && Pet.timer<=0 && proj.GetGlobalProjectile<ProjectileSourceChecks>().phantasmProjectile == false)
            {
                switch (Main.rand.Next(3))
                {
                    case 0:
                        int ice = Projectile.NewProjectile(Player.GetSource_Misc("PhantasmalDragon"), new Vector2(target.Center.X, target.Center.Y), Main.rand.NextVector2Circular(5f, 5f), ProjectileID.CultistBossIceMist, IceDamage(damageDone), hit.Knockback, Player.whoAmI);
                        Main.projectile[ice].friendly = true;
                        Main.projectile[ice].hostile = false;
                        Main.projectile[ice].tileCollide = false;
                        Main.projectile[ice].netImportant = true;
                        Main.projectile[ice].penetrate = icePierce;
                        break;
                    case 1:
                        for (int i = 0; i < 4; i++)
                        {
                            if (ModContent.GetInstance<Personalization>().AbilitySoundDisabled == false)
                                SoundEngine.PlaySound(SoundID.NPCHit55 with { PitchVariance = 0.5f, MaxInstances = 10, Volume = 0.2f }, Player.position);
                            Dust.NewDustDirect(new Vector2(Player.Center.X + Main.rand.NextFloat(-25f, 25f), Player.Center.Y - 400f), 25, 25, DustID.FlameBurst, 0, 0, 25);
                            int fire = Projectile.NewProjectile(Player.GetSource_Misc("PhantasmalDragon"), new Vector2(target.Center.X + Main.rand.NextFloat(-25f, 25f), target.Center.Y - 400f), new Vector2(Main.rand.NextFloat(5f, -5f), 7f), ProjectileID.CultistBossFireBall, FireDamage(damageDone), hit.Knockback, Player.whoAmI);
                            Main.projectile[fire].friendly = true;
                            Main.projectile[fire].hostile = false;
                            Main.projectile[fire].tileCollide = true;
                            Main.projectile[fire].netImportant = true;
                        }
                        break;
                    case 2:
                        for (int i = 0; i < 5; i++)
                        {
                            if (ModContent.GetInstance<Personalization>().AbilitySoundDisabled == false)
                                SoundEngine.PlaySound(SoundID.Zombie90 with { PitchVariance = 0.5f, MaxInstances = 10, Volume = 0.3f }, Player.position);
                            int light = Projectile.NewProjectile(Player.GetSource_Misc("PhantasmalDragon"), new Vector2(target.Center.X - Main.rand.Next(-300, 300), target.Center.Y - Main.rand.Next(-300, 300)), Vector2.Zero, ProjectileID.HallowBossSplitShotCore, LightDamage(damageDone), hit.Knockback, Player.whoAmI);
                            Main.projectile[light].friendly = true;
                            Main.projectile[light].hostile = false;
                            Main.projectile[light].tileCollide = false;
                            Main.projectile[light].netImportant = true;
                            Main.projectile[light].penetrate = lightPierce;
                        }
                        break;

                }
                Pet.timer = Pet.timerMax;
            }
        }
        public override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Pet.PetInUseWithSwapCd(ItemID.LunaticCultistPetItem) && Pet.timer <= 0)
            {
                switch (Main.rand.Next(3))
                {
                    case 0:
                        int ice = Projectile.NewProjectile(Player.GetSource_Misc("PhantasmalDragon"), new Vector2(target.Center.X, target.Center.Y), Main.rand.NextVector2Circular(5f, 5f), ProjectileID.CultistBossIceMist, IceDamage(damageDone), hit.Knockback, Player.whoAmI);
                        Main.projectile[ice].friendly = true;
                        Main.projectile[ice].hostile = false;
                        Main.projectile[ice].tileCollide = false;
                        Main.projectile[ice].netImportant = true;
                        Main.projectile[ice].penetrate = icePierce;
                        break;
                    case 1:
                        for (int i = 0; i < 4; i++)
                        {
                            if (ModContent.GetInstance<Personalization>().AbilitySoundDisabled == false)
                                SoundEngine.PlaySound(SoundID.NPCHit55 with { PitchVariance = 0.5f, MaxInstances = 10, Volume = 0.2f }, Player.position);
                            Dust.NewDustDirect(new Vector2(Player.Center.X + Main.rand.NextFloat(-25f, 25f), Player.Center.Y - 400f), 25, 25, DustID.FlameBurst, 0, 0, 25);
                            int fire = Projectile.NewProjectile(Player.GetSource_Misc("PhantasmalDragon"), new Vector2(target.Center.X + Main.rand.NextFloat(-25f, 25f), target.Center.Y - 400f), new Vector2(Main.rand.NextFloat(5f, -5f), 7f), ProjectileID.CultistBossFireBall, FireDamage(damageDone), hit.Knockback, Player.whoAmI);
                            Main.projectile[fire].friendly = true;
                            Main.projectile[fire].hostile = false;
                            Main.projectile[fire].tileCollide = true;
                            Main.projectile[fire].netImportant = true;
                        }
                        break;
                    case 2:
                        for (int i = 0; i < 5; i++)
                        {
                            if (ModContent.GetInstance<Personalization>().AbilitySoundDisabled == false)
                                SoundEngine.PlaySound(SoundID.Zombie90 with { PitchVariance = 0.5f, MaxInstances = 10, Volume = 0.3f }, Player.position);
                            int light = Projectile.NewProjectile(Player.GetSource_Misc("PhantasmalDragon"), new Vector2(target.Center.X - Main.rand.Next(-300, 300), target.Center.Y - Main.rand.Next(-300, 300)), Vector2.Zero, ProjectileID.HallowBossSplitShotCore, LightDamage(damageDone), hit.Knockback, Player.whoAmI);
                            Main.projectile[light].friendly = true;
                            Main.projectile[light].hostile = false;
                            Main.projectile[light].tileCollide = false;
                            Main.projectile[light].netImportant = true;
                            Main.projectile[light].penetrate = lightPierce;
                        }
                        break;

                }
                Pet.timer = Pet.timerMax;
            }
        }
    }
}
