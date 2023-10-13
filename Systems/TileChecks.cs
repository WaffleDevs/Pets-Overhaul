using Microsoft.Xna.Framework;
using PetsOverhaul.PetEffects;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.WorldBuilding;
namespace PetsOverhaul.Systems
{
    public class TileChecks : GlobalTile
    {
        public static bool commonPlant = false;
        public static bool rarePlant = false;
        public static bool tileIsOreGemOrExtractable = false;
        public static bool tileIsPlacedByPlayer = false;
        public static bool bamboo = false;
        public static bool choppable = false;
        public static bool commonBlock = false;
        public static bool dirt = false;
        public static bool gemTree = false;
        /// <summary>
        /// Includes tiles that are choppable by using an Axe or Chainsaws.
        /// </summary>
        public static bool[] choppableTiles = TileID.Sets.Factory.CreateBoolSet(false, TileID.Trees, TileID.Cactus, TileID.MushroomTrees, TileID.PalmTree, TileID.TreeAsh, TileID.VanityTreeSakura, TileID.VanityTreeYellowWillow, TileID.Bamboo);
        /// <summary>
        /// Includes Gem tiles.
        /// </summary>
        public static bool[] gemTile = TileID.Sets.Factory.CreateBoolSet(false, TileID.Amethyst, TileID.Topaz, TileID.Sapphire, TileID.Emerald, TileID.Ruby, TileID.AmberStoneBlock, TileID.Diamond, TileID.ExposedGems);
        /// <summary>
        /// Includes tiles that are extractable by an Extractinator.
        /// </summary>
        public static bool[] extractableTile = TileID.Sets.Factory.CreateBoolSet(false, TileID.DesertFossil, TileID.Slush, TileID.Silt);
        /// <summary>
        /// Includes plants that grows rarely 
        /// </summary>
        public static bool[] rarePlantTile = TileID.Sets.Factory.CreateBoolSet(false, TileID.DyePlants, TileID.LifeFruit);
        public override void KillTile(int i, int j, int type, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            if (PlayerPlacedBlockList.placedBlocksByPlayer.Contains(new Vector2(i, j)) && fail == false)
            {
                tileIsPlacedByPlayer = true;
                PlayerPlacedBlockList.placedBlocksByPlayer.Remove(new Vector2(i, j));
            }
            else
            {
                tileIsPlacedByPlayer = false;
            }
            if (type == TileID.BloomingHerbs || (type == TileID.JunglePlants && Main.tile[i, j].TileFrameX == 8 * 18)|| type == TileID.MatureHerbs || type == TileID.Pumpkins || (type == TileID.Plants && (Main.tile[i, j].TileFrameX == 8 * 18)) || (type == TileID.CorruptPlants && (Main.tile[i, j].TileFrameX == 8 * 18)) || type == TileID.MushroomPlants || (type == TileID.CrimsonPlants && (Main.tile[i, j].TileFrameX == 15 * 18))) //Büyük bi spirte sheeti üzerinden 8 inci 16 (2padding) pixelli tile'ı seçiyoruz
            {
                commonPlant = true;
            }
            else
            {
                commonPlant = false;
            }
            if (rarePlantTile[type])
            {
                rarePlant = true;
            }
            else
            {
                rarePlant = false;
            }
            if (TileID.Sets.Ore[type] || gemTile[type] || extractableTile[type] || type == TileID.Obsidian || type == TileID.LunarOre || type == TileID.FossilOre)
            {
                tileIsOreGemOrExtractable = true;
            }
            else
            {
                tileIsOreGemOrExtractable = false;
            }
            if (type == TileID.Bamboo)
            {
                bamboo = true;
            }
            else
            {
                bamboo = false;
            }
            if (TileID.Sets.Dirt[type])
            {
                dirt = true;
            }
            else
            {
                dirt = false;
            }
            if (TileID.Sets.Stone[type] || type == TileID.SnowBlock || TileID.Sets.Conversion.Moss[type] || TileID.Sets.Grass[type] || TileID.Sets.Ash[type] || TileID.Sets.Mud[type] || type == TileID.ClayBlock || type == TileID.Granite || type == TileID.Marble || TileID.Sets.Conversion.Sand[type] || TileID.Sets.Conversion.Sandstone[type] || TileID.Sets.Conversion.HardenedSand[type] || TileID.Sets.Conversion.Ice[type])
            {
                commonBlock = true;
            }
            else
            {
                commonBlock = false;
            }
            if (choppableTiles[type])
            {
                choppable = true;
            }
            else
            {
                choppable = false;
            }
            if (TileID.Sets.CountsAsGemTree[type])
            {
                gemTree = true;
            }
            else
            {
                gemTree = false;
            }
        }
        public override void PlaceInWorld(int i, int j, int type, Item item)
        {
            PlayerPlacedBlockList.placedBlocksByPlayer.Add(new Vector2(i, j));
        }
        public override bool CanReplace(int i, int j, int type, int tileTypeBeingPlaced)
        {
            if (PlayerPlacedBlockList.placedBlocksByPlayer.Contains(new Vector2(i, j)))
            {
                tileIsPlacedByPlayer = true;
            }
            else
            {
                tileIsPlacedByPlayer = false;
                PlayerPlacedBlockList.placedBlocksByPlayer.Add(new Vector2(i, j));
            }
            return true;
        }
    }
}