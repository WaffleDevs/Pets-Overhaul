using Terraria;
using Terraria.ID;
using PetsOverhaul.Systems;
using Terraria.ModLoader;

namespace PetsOverhaul.PetEffects
{
    sealed public class SharkPup : ModPlayer
    {
        public float seaCreatureResist = 0.8f;
        public float seaCreatureDamage = 1.2f;
        public int shieldOnCatch = 5;
        public int shieldTime = 600;
        public int fishingPow = 10;
        GlobalPet Pet { get => Player.GetModPlayer<GlobalPet>(); }
        public override void ModifyHitByNPC(NPC npc, ref Player.HurtModifiers modifiers)
        {
            if (Pet.PetInUseWithSwapCd(ItemID.SharkBait)&&npc.GetGlobalNPC<NpcPet>().seaCreature)
            {
                modifiers.FinalDamage *= seaCreatureResist;
            }
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (Pet.PetInUseWithSwapCd(ItemID.SharkBait) && target.GetGlobalNPC<NpcPet>().seaCreature)
            {
                modifiers.FinalDamage *= seaCreatureDamage;
            }
        }
        public override void UpdateEquips()
        {
            if (Pet.PetInUse(ItemID.SharkBait))
            {
                Player.fishingSkill += fishingPow;
            }
        }
        public override void ModifyCaughtFish(Item fish)
        {
            if (Pet.PetInUseWithSwapCd(ItemID.SharkBait))
            {
                if (shieldOnCatch>=Pet.shieldAmount)
                {
                    Pet.shieldAmount = shieldOnCatch;
                    Pet.shieldTimer = shieldTime;
                }
            }
        }
    }
}
