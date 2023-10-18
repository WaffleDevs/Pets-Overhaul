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
        public static Dictionary<string, ModdedContent> Mods = new Dictionary<string, ModdedContent> { };
        public static void LoadMods()
        {
            //Calamity = new CalamitySupport();
            //Calamity.InitializeMod();
            ModdedContent ThoriumMod = new ThoriumSupport();
            ThoriumMod.InitializeMod();
            Mods.Add(ThoriumMod.InternalModName, ThoriumMod);
        }

        public static bool IsModLoaded(string modName)
        {
            return Mods.ContainsKey(modName);
        }
        public static bool GetModInstance(string modName, out Mod instance)
        {
            if (!IsModLoaded(modName))
            {
                instance = null;
                return false;
            }
            instance = Mods[modName].ModInstance;
            return true;
        }

        public static bool GetItemInstance(string modName, string InternalName, out ModItem item)
        {
            if (!Mods[modName].InternalNameToModdedItemId.ContainsKey(InternalName))
            {
                item = null;
                return false;
            }

            item = Mods[modName].InternalNameToModdedItemInstance[InternalName];
            return true;
        }
    }

    public class ModdedContent
    {
        public virtual string InternalModName { get; set; }
        //If these arent defined, they will be skipped

        public virtual string[] InternalModdedItemNames { get; set; }
        public virtual Dictionary<int, int[]> MiningXpPerModdedBlock { get; set; }
        public virtual Dictionary<int, int[]> FishingXpPerModdedFish { get; set; }
        public virtual Dictionary<int, int[]> FishingXpPerModdedKill { get; set; }

        public Mod ModInstance;
        public Dictionary<string, int> InternalNameToModdedItemId = new Dictionary<string, int> { };
        public Dictionary<string, ModItem> InternalNameToModdedItemInstance = new Dictionary<string, ModItem>();

        public void InitializeMod()
        {
            if (!ModLoader.TryGetMod(InternalModName, out ModInstance)) return;
            MergePetItems();
            MergeJunimoMiningXp();
            MergeJunimoFishingXp();
        }

        public void MergePetItems()
        {
            if (InternalModdedItemNames == null) return;
            foreach (string internalName in InternalModdedItemNames)
            {
                ModInstance.TryFind(internalName, out ModItem item);
                //Console.WriteLine($"IN: {internalName}\n Type: {item.Type}");

                ModContent.GetInstance<PetRegistry>().TerrariaPetItemIds.TryAdd(internalName, item.Type);
                InternalNameToModdedItemId.TryAdd(internalName, item.Type);
            };
        }

        public void MergeJunimoMiningXp()
        {
            if (MiningXpPerModdedBlock != null) _ = Main.LocalPlayer.GetModPlayer<Junimo>().MiningXpPerBlock.Concat(MiningXpPerModdedBlock);
        }

        public void MergeJunimoFishingXp()
        {
            if (FishingXpPerModdedFish != null) _ = Main.LocalPlayer.GetModPlayer<Junimo>().FishingXpPerFish.Concat(FishingXpPerModdedFish);
            if (FishingXpPerModdedKill != null) _ = Main.LocalPlayer.GetModPlayer<Junimo>().FishingXpPerKill.Concat(FishingXpPerModdedKill);
        }

    }
}