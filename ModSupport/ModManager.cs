using System;
using System.Collections.Generic;
using System.Linq;

using PetsOverhaul.PetEffects.Vanilla;
using PetsOverhaul.Systems;

using Terraria;
using Terraria.ModLoader;

namespace PetsOverhaul.ModSupport
{
    public static class ModManager
    {
        //public static CalamitySupport Calamity;
        public static ThoriumSupport ThoriumMod;
        public static void LoadMods()
        {
            //Calamity = new CalamitySupport();
            //Calamity.InitializeMod();
            ThoriumMod = new ThoriumSupport();
            ThoriumMod.InitializeMod();
        }
    }

    public class ModSupport
    {
        public string InternalModName;
        public string[] InternalModdedItemNames;
        public Dictionary<int, int[]> MiningXpPerModdedBlock;
        public Dictionary<int, int[]> FishingXpPerModdedFish;
        public Dictionary<int, int[]> FishingXpPerModdedKill;

        public Mod ModInstance;
        public Dictionary<string, int> InternalNameToModdedItemId;
        public Dictionary<string, ModItem> InternalNameToModdedItemInstance;
        public void InitializeMod()
        {
            if (!ModLoader.TryGetMod(InternalModName, out ModInstance)) return;
            MergePetItems();
            MergeJunimoMiningXp();
            MergeJunimoFishingXp();
        }

        public void MergePetItems()
        {
            if (InternalNameToModdedItemId == null) return;
            foreach (string internalName in InternalModdedItemNames)
            {
                ModItem item;
                ModInstance.TryFind(internalName, out item);
                Console.WriteLine($"IN: {internalName}\n Type: {item.Type}");

                ModContent.GetInstance<PetRegistry>().TerrariaPetItemIds.TryAdd(internalName, item.Type);
                InternalNameToModdedItemId.TryAdd(internalName, item.Type);
            };
        }

        public void MergeJunimoMiningXp()
        {
            if (MiningXpPerModdedBlock != null) Main.LocalPlayer.GetModPlayer<Junimo>().MiningXpPerBlock.Concat(MiningXpPerModdedBlock);
        }

        public void MergeJunimoFishingXp()
        {
            if (FishingXpPerModdedFish != null) Main.LocalPlayer.GetModPlayer<Junimo>().FishingXpPerFish.Concat(FishingXpPerModdedFish);
            if (FishingXpPerModdedKill != null) Main.LocalPlayer.GetModPlayer<Junimo>().FishingXpPerKill.Concat(FishingXpPerModdedKill);
        }

        public bool IsModLoaded()
        {
            return ModInstance != null;
        }
        public bool GetModInstance(out Mod instance)
        {
            if (!IsModLoaded())
            {
                instance = null;
                return false;
            }
            instance = ModInstance;
            return true;
        }

        public bool GetItemInstance(string InternalName, out ModItem item)
        {
            if (!InternalNameToModdedItemId.ContainsKey(InternalName))
            {
                item = null;
                return false;
            }

            item = InternalNameToModdedItemInstance[InternalName];
            return true;
        }
    }
}