using Terraria;
using Terraria.ID;
using PetsOverhaul.Systems;
using Terraria.ModLoader;

namespace PetsOverhaul.PetEffects.Vanilla
{
    sealed public class Turtle : ModPlayer
    {
        GlobalPet Pet { get => Player.GetModPlayer<GlobalPet>(); }
        public float moveSpd = 0.12f;
        public float def = 1.11f;
        public float kbResist = 0.25f;
        public float dmgReduce = 0.05f;
        public override void PostUpdateEquips()
        {
            if (Pet.PetInUseWithSwapCd(ItemID.Seaweed))
            {
                Player.statDefense.FinalMultiplier *= def;
                Player.moveSpeed -= moveSpd;
                Player.maxRunSpeed -= moveSpd * 3;
                Player.GetDamage<GenericDamageClass>() -= dmgReduce;
            }
        }
        public override void PostUpdateRunSpeeds()
        {
            if (Pet.PetInUseWithSwapCd(ItemID.Seaweed))
            {
                Player.maxRunSpeed -= moveSpd * 3;
            }
        }
        public override void ModifyHurt(ref Player.HurtModifiers modifiers)
        {
            if (Pet.PetInUseWithSwapCd(ItemID.Seaweed))
            {
                modifiers.Knockback *= kbResist;
            }
        }
    }
}
