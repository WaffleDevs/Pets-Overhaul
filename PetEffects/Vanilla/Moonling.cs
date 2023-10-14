using Terraria;
using Terraria.ID;
using PetsOverhaul.Systems;
using Terraria.ModLoader;
using System.Linq;

namespace PetsOverhaul.PetEffects.Vanilla
{
    sealed public class Moonling : ModPlayer
    {
        public float meleeDr = 0.15f;
        public float meleeSpd = 0.2f;
        public float meleeDmg = 0.2f;
        public int rangedPen = 20;
        public float rangedDmg = 0.15f;
        public int rangedCr = 20;
        public int magicMana = 150;
        public float magicDmg = 0.2f;
        public int magicCrit = 10;
        public float magicManaCost = 0.1f;
        public float sumDmg = 0.1f;
        public float sumWhipRng = 0.55f;
        public float sumWhipSpd = 0.2f;
        public int sumMinion = 2;
        public int sumSentry = 2;
        public int defense = 10;
        GlobalPet Pet { get => Player.GetModPlayer<GlobalPet>(); }
        public override void PostUpdateEquips()
        {
            if (Pet.PetInUseWithSwapCd(ItemID.MoonLordPetItem))
            {
                StatModifier[] stats = { Player.GetDamage<MeleeDamageClass>(), Player.GetDamage<RangedDamageClass>(), Player.GetDamage<MagicDamageClass>(), Player.GetDamage<SummonDamageClass>() };
                var highestDamage = stats.MaxBy(x => x.Additive);
                if (highestDamage == Player.GetDamage<MeleeDamageClass>())
                {
                    Player.endurance += meleeDr;
                    Player.GetAttackSpeed<MeleeDamageClass>() += meleeSpd;
                    Player.GetDamage<MeleeDamageClass>() += meleeDmg;
                    Player.statDefense += defense;
                }
                else if (highestDamage == Player.GetDamage<RangedDamageClass>())
                {
                    Player.GetArmorPenetration<RangedDamageClass>() += rangedPen;
                    Player.GetDamage<RangedDamageClass>() += rangedDmg;
                    Player.GetCritChance<RangedDamageClass>() += rangedCr;
                }
                else if (highestDamage == Player.GetDamage<MagicDamageClass>())
                {
                    Player.statManaMax2 += magicMana;
                    Player.GetDamage<MagicDamageClass>() += magicDmg;
                    Player.GetCritChance<MagicDamageClass>() += magicCrit;
                    Player.manaCost -= magicManaCost;
                }
                else if (highestDamage == Player.GetDamage<SummonDamageClass>())
                {
                    Player.GetDamage<SummonDamageClass>() += sumDmg;
                    Player.whipRangeMultiplier += sumWhipRng;
                    Player.GetAttackSpeed<SummonMeleeSpeedDamageClass>() += sumWhipSpd;
                    Player.maxMinions += sumMinion;
                    Player.maxTurrets += sumSentry;
                }
            }
        }
    }
}
