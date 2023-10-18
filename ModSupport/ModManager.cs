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