using Terraria;
using Terraria.ID;
using PetsOverhaul.Systems;
using Terraria.ModLoader;

namespace PetsOverhaul.PetEffects
{
    sealed public class FennecFox : ModPlayer
    {
        GlobalPet Pet { get => Player.GetModPlayer<GlobalPet>(); }
        public float sizeDecrease = 0.85f;
        public float speedIncrease = 0.15f;
        public float meleeSpdIncrease = 0.18f;
        public float meleeDmg = 0.05f;
        public override void ModifyItemScale(Item item, ref float scale)
        {
            if (Pet.PetInUseWithSwapCd(ItemID.ExoticEasternChewToy))
                scale *= sizeDecrease;
        }
        public override void PostUpdateEquips()
        {
            if (Pet.PetInUseWithSwapCd(ItemID.ExoticEasternChewToy))
            {
                if(Player.HeldItem.CountsAsClass<MeleeDamageClass>())
                {
                    Player.moveSpeed += speedIncrease;
                    Player.GetAttackSpeed<MeleeDamageClass>() += meleeSpdIncrease;
                }
                Player.GetDamage<MeleeDamageClass>() += meleeDmg;
            }
        }
    }
}
