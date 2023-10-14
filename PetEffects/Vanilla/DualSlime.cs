using Terraria;
using Terraria.ID;
using PetsOverhaul.Systems;
using Terraria.ModLoader;

namespace PetsOverhaul.PetEffects.Vanilla
{
    sealed public class DualSlime : ModPlayer
    {
        GlobalPet Pet { get => Player.GetModPlayer<GlobalPet>(); }
        // King, in SlimePrince
        public float wetSpeed = 0.9f;
        public float wetDmg = 0.07f;
        public float wetDef = 0.07f;
        public float slimyKb = 1.45f;
        public float slimyJump = 1.8f;
        public float wetDealtLower = 0.94f;
        public float wetRecievedHigher = 1.07f;
        public float bonusKb = 1.45f;
        public float healthDmg = 0.012f;
        public int burnCap = 45;
        // Queen, in SlimePrincess
        public float slow = 0.59f;
        public float haste = 0.26f;
        public int shield = 6;
        public int shieldTime = 240;
        public float dmgBoost = 1.22f;
        public int baseCounterChnc = 90;
    }
}
