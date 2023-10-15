using Terraria;
using Terraria.ID;
using PetsOverhaul.Systems;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.Localization;

namespace PetsOverhaul.PetEffects.Vanilla
{
    sealed public class Plantero : ModPlayer
    {
        public int spawnChance = 15;
        public float damageMult = 0.7f;
        public float knockBack = 0.4f;
        public int flatDmg = 15;
        GlobalPet Pet { get => Player.GetModPlayer<GlobalPet>(); }
        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (proj.GetGlobalProjectile<ProjectileSourceChecks>().planteroProj == false && Pet.PetInUseWithSwapCd(ItemID.MudBud))
                for (int i = 0; i < ItemPet.Randomizer(spawnChance + (int)(spawnChance * Pet.abilityHaste)); i++)
                {
                    Vector2 location = new(target.position.X + Main.rand.NextFloat(-2f, 2f), target.position.Y + Main.rand.NextFloat(-2f, 2f));
                    Vector2 velocity = new(Main.rand.NextFloat(-1.5f, 1.5f), Main.rand.NextFloat(-1.5f, 1.5f));
                    switch (Main.rand.Next(3))
                    {
                        case 0:
                            Projectile.NewProjectile(Player.GetSource_Misc("Plantero"), location, velocity, ProjectileID.SporeGas, (int)(damageDone * damageMult) + flatDmg, knockBack, Main.myPlayer);
                            break;
                        case 1:
                            Projectile.NewProjectile(Player.GetSource_Misc("Plantero"), location, velocity, ProjectileID.SporeGas2, (int)(damageDone * damageMult) + flatDmg, knockBack, Main.myPlayer);
                            break;
                        case 2:
                            Projectile.NewProjectile(Player.GetSource_Misc("Plantero"), location, velocity, ProjectileID.SporeGas3, (int)(damageDone * damageMult) + flatDmg, knockBack, Main.myPlayer);
                            break;
                    }
                }
        }
        public override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Pet.PetInUseWithSwapCd(ItemID.MudBud))
                for (int i = 0; i < ItemPet.Randomizer(spawnChance + (int)(spawnChance * Pet.abilityHaste)); i++)
                {
                    Vector2 location = new(target.position.X + Main.rand.NextFloat(-2f, 2f), target.position.Y + Main.rand.NextFloat(-2f, 2f));
                    Vector2 velocity = new(Main.rand.NextFloat(-1.5f, 1.5f), Main.rand.NextFloat(-1.5f, 1.5f));
                    switch (Main.rand.Next(3))
                    {
                        case 0:
                            Projectile.NewProjectile(Player.GetSource_Misc("Plantero"), location, velocity, ProjectileID.SporeGas, (int)(damageDone * damageMult) + flatDmg, knockBack, Main.myPlayer);
                            break;
                        case 1:
                            Projectile.NewProjectile(Player.GetSource_Misc("Plantero"), location, velocity, ProjectileID.SporeGas2, (int)(damageDone * damageMult) + flatDmg, knockBack, Main.myPlayer);
                            break;
                        case 2:
                            Projectile.NewProjectile(Player.GetSource_Misc("Plantero"), location, velocity, ProjectileID.SporeGas3, (int)(damageDone * damageMult) + flatDmg, knockBack, Main.myPlayer);
                            break;
                    }
                }
        }
    }
    sealed public class MudBud : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation) => entity.type == ItemID.MudBud;

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            Plantero plantero = Main.LocalPlayer.GetModPlayer<Plantero>();
            tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.MudBud")
                        .Replace("<chance>", plantero.spawnChance.ToString())
                        .Replace("<dmg>", plantero.damageMult.ToString())
                        .Replace("<flatDmg>", plantero.flatDmg.ToString())
                        .Replace("<kb>", plantero.knockBack.ToString())
                        ));
        }
    }
}
