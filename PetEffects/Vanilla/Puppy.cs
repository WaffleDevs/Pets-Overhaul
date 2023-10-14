using Terraria;
using Terraria.ID;
using PetsOverhaul.Systems;
using Terraria.Audio;
using PetsOverhaul.Config;
using Terraria.ModLoader;

namespace PetsOverhaul.PetEffects.Vanilla
{
    sealed public class Puppy : ModPlayer
    {
        GlobalPet Pet { get => Player.GetModPlayer<GlobalPet>(); }
        public int catchChance = 65;
        public int rareCatchChance = 15;
        public int rareCritterCoin = 250;
        public int rareEnemyCoin = 400;
        public override void UpdateEquips()
        {
            if (Pet.PetInUse(ItemID.DogWhistle))
                Player.AddBuff(BuffID.Hunter, 1);
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Pet.PetInUse(ItemID.DogWhistle) && target.active == false && target.rarity > 0 && target.CountsAsACritter == false && target.SpawnedFromStatue == false)
                Player.QuickSpawnItem(Player.GetSource_Misc("Puppy"), ItemID.SilverCoin, ItemPet.Randomizer(rareEnemyCoin * target.rarity));
        }
        public override void OnCatchNPC(NPC npc, Item item, bool failed)
        {

            if (Pet.PetInUse(ItemID.DogWhistle) && failed == false && npc.CountsAsACritter && npc.SpawnedFromStatue == false && npc.releaseOwner == 255)
            {
                if (npc.rarity > 0)
                {
                    Player.QuickSpawnItem(Player.GetSource_Misc("Puppy"), ItemID.SilverCoin, ItemPet.Randomizer(rareCritterCoin * npc.rarity));
                    Player.QuickSpawnItem(Player.GetSource_CatchEntity(npc), npc.catchItem, ItemPet.Randomizer(rareCatchChance));
                }
                else
                    Player.QuickSpawnItem(Player.GetSource_CatchEntity(npc), npc.catchItem, ItemPet.Randomizer(catchChance));
                if (ModContent.GetInstance<Personalization>().AbilitySoundDisabled == false)
                    SoundEngine.PlaySound(SoundID.Item65 with { PitchVariance = 0.3f, MaxInstances = 5 }, Player.position);
            }
        }
    }
}
