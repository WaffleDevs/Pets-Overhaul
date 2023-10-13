using Terraria;
using Terraria.ModLoader;

namespace PetsOverhaul.TownPets
{
    public class TownPet : ModPlayer
    {
        public int auraRange = 2000;
        public float bunnyJump = 0.08f;
        public float catSpeed = 1.025f;
        public int dogFish = 1;
        public float clumsyLuck = 0.01f;
        public float squireDamage = 0.008f;
        public float critHitsAreCool = 1f;
        public int oldDef = 1;
        public float divaDisc = 0.5f;
        public float mysticHaste = 0.02f;
        public float nerdBuildSpeed = 1.05f;
        public float oldKbResist = 0.95f;

        public override void PreUpdate()
        {
            if (NPC.downedMoonlord)
            {
                auraRange = 2400;
                bunnyJump = 0.15f;
                catSpeed = 1.075f;
                dogFish = 3;
                squireDamage = 0.03f;
                critHitsAreCool = 3f;
                oldDef = 3;
                clumsyLuck = 0.03f;
                divaDisc = 1.5f;
                mysticHaste = 0.06f;
                nerdBuildSpeed = 1.3f;
                oldKbResist = 0.85f;
            }
            else if (Main.hardMode)
            {
                auraRange = 2200;
                bunnyJump = 0.11f;
                catSpeed = 1.05f;
                dogFish = 2;
                squireDamage = 0.02f;
                critHitsAreCool = 2f;
                oldDef = 2;
                clumsyLuck = 0.02f;
                divaDisc = 1f;
                mysticHaste = 0.04f;
                nerdBuildSpeed = 1.2f;
                oldKbResist = 0.90f;
            }
            else
            {
                auraRange = 2000;
                bunnyJump = 0.08f;
                catSpeed = 1.025f;
                dogFish = 1;
                squireDamage = 0.008f;
                critHitsAreCool = 1f;
                oldDef = 1;
                clumsyLuck = 0.01f;
                divaDisc = 0.5f;
                mysticHaste = 0.02f;
                nerdBuildSpeed = 1.1f;
                oldKbResist = 0.95f;
            }
        }
    }
}
