import os

string = """using Terraria;
using Terraria.ID;
using PetsOverhaul.Systems;
using Terraria.ModLoader;

namespace PetsOverhaul.PetEffects.ModName
{
    sealed public class PetName : ModPlayer
    {
        GlobalPet Pet { get => Player.GetModPlayer<GlobalPet>(); }
        public override void PostUpdateEquips()
        {
            //if (Pet.PetInUseWithSwapCd(ItemID.MartianPetItem)) // Run code once.
            //if (Pet.PetInUse(ItemID.MartianPetItem)) // Run code every update.
            //{
            //    
            //}
        }
    }
}"""


modName = input("Mod?: ")
if(not os.path.exists(modName)):
    os.mkdir(modName)
string = string.replace("ModName", modName)

listOfPets = input("List?:").replace("\"", "").replace("\n", "").split(',')
if len(listOfPets) > 1 and not listOfPets[len(listOfPets)-1] == "":
    print("single line")
    listOfPets.sort()
    for x in listOfPets:
        f = open(modName + "/" + x + ".cs", "wt")
        f.write(string.replace("PetName", x))
        f.close()

    print("Done. Made {} files.".format(len(listOfPets)))
else:
    print("multiline")
    def recurse():
        i = input("next").replace("\"", "").replace(",", "").replace(" ", "")
        f = open(modName + "/" + i + ".cs", "wt")
        f.write(string.replace("PetName", i))
        f.close()
        print("Done. Made {} file.".format(i))
        if f:
            recurse()
    recurse()