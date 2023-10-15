using Terraria;
using Terraria.ID;
using PetsOverhaul.Systems;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.Localization;

namespace PetsOverhaul.PetEffects.Vanilla
{
    sealed public class Spiffo : ModPlayer
    {
        public int ammoReserveChance = 15;
        public int zombieArmorPen = 6;
        public int penetrateChance = 50;
        GlobalPet Pet { get => Player.GetModPlayer<GlobalPet>(); }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (Pet.PetInUseWithSwapCd(ItemID.SpiffoPlush) && NPCID.Sets.Zombies[target.type] && modifiers.DamageType.Type == DamageClass.Ranged.Type)
            {
                modifiers.ArmorPenetration += zombieArmorPen;
            }
        }
        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Pet.PetInUseWithSwapCd(ItemID.SpiffoPlush) && target.active == false && proj.CountsAsClass<RangedDamageClass>() && proj.penetrate >= 0)
            {
                proj.penetrate += ItemPet.Randomizer(penetrateChance);
            }
        }
        public override bool CanConsumeAmmo(Item weapon, Item ammo)
        {
            if (Pet.PetInUseWithSwapCd(ItemID.SpiffoPlush) && Main.rand.NextBool(ammoReserveChance, 100))
            {
                return false;
            }
            else
                return true;
        }
    }
    sealed public class SpiffoPlush : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation) => entity.type == ItemID.SpiffoPlush;

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            Spiffo spiffo = ModContent.GetInstance<Spiffo>();
            tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.SpiffoPlush")
                        .Replace("<ammoReserve>", spiffo.ammoReserveChance.ToString())
                        .Replace("<armorPen>", spiffo.zombieArmorPen.ToString())
                        .Replace("<penChance>", spiffo.penetrateChance.ToString())
                        ));
        }
    }
}
