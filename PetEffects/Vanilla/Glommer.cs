using Terraria;
using Terraria.ID;
using PetsOverhaul.Systems;
using Terraria.ModLoader;

namespace PetsOverhaul.PetEffects.Vanilla
{
    sealed public class Glommer : ModPlayer
    {
        public int glommerSanityTime = 45;
        public int glommerSanityRecover = 1;
        public float glommerSanityAura = 0.2f;
        public int glommerSanityRange = 4000;
        GlobalPet Pet { get => Player.GetModPlayer<GlobalPet>(); }
        public override void PreUpdate()
        {
            if (Pet.PetInUse(ItemID.GlommerPetItem))
                Pet.timerMax = glommerSanityTime;

        }
        public override void PostUpdateEquips()
        {
            if (Pet.PetInUse(ItemID.GlommerPetItem) && Main.rand.NextBool(18000))
                Player.QuickSpawnItem(Player.GetSource_Misc("Glommer"), ItemID.PoopBlock);
            if (Pet.PetInUseWithSwapCd(ItemID.GlommerPetItem))
            {
                for (int i = 0; i < Main.maxPlayers; i++)
                {
                    Player plr = Main.player[i];
                    if (Player.Distance(plr.Center) < glommerSanityRange && plr.active && plr.whoAmI != 255)
                        plr.GetModPlayer<GlobalPet>().abilityHaste += glommerSanityAura;
                }
                Player.statManaMax2 += (int)(Pet.abilityHaste * Player.statManaMax2);
                if (Pet.timer <= 0 && Player.statMana != Player.statManaMax2)
                {
                    Player.statMana += glommerSanityRecover;
                    Pet.timer = Pet.timerMax;
                }
            }
        }
    }
}
