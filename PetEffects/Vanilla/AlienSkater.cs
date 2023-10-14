using Terraria;
using Terraria.ID;
using PetsOverhaul.Systems;
using Terraria.ModLoader;

namespace PetsOverhaul.PetEffects.Vanilla
{
    sealed public class AlienSkater : ModPlayer
    {
        GlobalPet Pet { get => Player.GetModPlayer<GlobalPet>(); }
        public float accelerator = 0.25f;
        public float wingTime = 1.4f;
        public float speedMult = 1.3f;
        public float accMult = 1.5f;
        public float speedAccIncr = 0.9f;
        public override void PostUpdateEquips()
        {
            if (Pet.PetInUseWithSwapCd(ItemID.MartianPetItem))
            {
                Player.runAcceleration += 0.25f;
                Player.wingTimeMax = (int)(Player.wingTimeMax * wingTime);
            }
        }
    }
    sealed public class AlienSkaterWingSpeed : GlobalItem
    {
        public override bool InstancePerEntity => true;

        public override void HorizontalWingSpeeds(Item item, Player player, ref float speed, ref float acceleration)
        {
            if (player.TryGetModPlayer(out AlienSkater alienSkater) && player.GetModPlayer<GlobalPet>().PetInUseWithSwapCd(ItemID.MartianPetItem))
            {
                speed *= alienSkater.speedMult;
                acceleration *= alienSkater.accMult;
                speed += alienSkater.speedAccIncr;
                acceleration += alienSkater.speedAccIncr;
            }

        }
    }
}
