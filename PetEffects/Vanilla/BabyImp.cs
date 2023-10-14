using Terraria;
using Terraria.ID;
using PetsOverhaul.Systems;
using Terraria.ModLoader;

namespace PetsOverhaul.PetEffects.Vanilla
{
    sealed public class BabyImp : ModPlayer
    {
        GlobalPet Pet { get => Player.GetModPlayer<GlobalPet>(); }
        public int lavaImmune = 600;
        public int lavaDef = 10;
        public float lavaSpd = 0.15f;
        public int obbyDef = 8;
        public float obbySpd = 0.08f;
        public override void PostUpdateEquips()
        {
            if (Pet.PetInUseWithSwapCd(ItemID.HellCake))
            {
                Player.lavaMax += lavaImmune;
                if (Main.tile[(int)(Player.Center.X / 16f), (int)((Player.Bottom.Y - 16f) / 16f)].LiquidAmount > 0 || Main.tile[(int)(Player.Right.X / 16f), (int)((Player.Bottom.Y - 16f) / 16f)].LiquidAmount > 0 || Player.lavaWet)
                {
                    if (Main.tile[(int)(Player.Left.X / 16f), (int)((Player.Bottom.Y - 16f) / 16f)].LiquidType == LiquidID.Lava || Main.tile[(int)(Player.Right.X / 16f), (int)((Player.Bottom.Y - 16f) / 16f)].LiquidType == LiquidID.Lava || Player.lavaWet)
                    {
                        Player.accFlipper = true;
                        Player.statDefense += lavaDef;
                        Player.moveSpeed += lavaSpd;
                    }
                }
                if (Player.HasBuff(BuffID.ObsidianSkin))
                {
                    Player.statDefense += obbyDef;
                    Player.moveSpeed -= obbySpd;
                }
            }
        }
        public override void PostUpdateRunSpeeds()
        {
            if (Pet.PetInUseWithSwapCd(ItemID.HellCake) && Player.HasBuff(BuffID.ObsidianSkin))
            {
                Player.maxRunSpeed -= obbySpd * 3;
            }
        }
    }
}
