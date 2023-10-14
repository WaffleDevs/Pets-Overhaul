using Terraria;
using Terraria.ID;
using PetsOverhaul.Systems;
using Terraria.ModLoader;

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
}
