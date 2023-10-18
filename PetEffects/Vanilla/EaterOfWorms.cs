﻿using Terraria.Audio;
using Terraria;
using Terraria.ID;
using PetsOverhaul.Systems;
using Terraria.ModLoader;
using PetsOverhaul.Config;
using System.Collections.Generic;
using Terraria.Localization;

using Terraria.GameInput;

namespace PetsOverhaul.PetEffects.Vanilla
{
    sealed public class EaterOfWorms : ModPlayer
    {
        GlobalPet Pet { get => Player.GetModPlayer<GlobalPet>(); }
        List<(int X, int Y)> tilesToRandomize = new();
        public int tileBreakXSpread = 2;
        public int tileBreakYSpread = 2;
        public int tileBreakSpreadChance = 75;
        public float nonOreSpeed = 0.2f;
        internal int mineX = -2;
        internal int mineY = -2;
        internal int oldTileType = 0;
        internal int prevX = 0;
        internal int prevY = 0;
        public override void PostUpdateRunSpeeds()
        {
            if (Pet.PetInUse(ItemID.EaterOfWorldsPetItem))
            {
                tilesToRandomize.Clear();
                if (Main.SmartCursorShowing)
                {
                    Tile tile = Main.tile[Main.SmartCursorX, Main.SmartCursorY];

                    if (Pet.PetInUse(ItemID.EaterOfWorldsPetItem) && TileID.Sets.Ore[tile.TileType] == false && TileChecks.gemTiles.Contains(tile.TileType) == false && Player.controlUseItem)
                    {
                        Player.pickSpeed -= nonOreSpeed;
                    }
                    if (Pet.PetInUse(ItemID.EaterOfWorldsPetItem) && Player.controlUseItem && Player.HeldItem.pick > 0 && Main.tile[prevX, prevY].TileType == 0 && oldTileType != 0 && (TileID.Sets.Ore[oldTileType] || TileChecks.gemTiles.Contains(oldTileType)) && Main.rand.NextBool(tileBreakSpreadChance, 100))
                    {
                        for (mineX = -tileBreakXSpread; mineX <= tileBreakXSpread; mineX++)
                        {
                            for (mineY = -tileBreakYSpread; mineY <= tileBreakYSpread; mineY++)
                            {
                                if (Main.tile[prevX + mineX, prevY + mineY].TileType == oldTileType)
                                    tilesToRandomize.Add((prevX + mineX, prevY + mineY));
                            }
                            if (Main.tile[prevX + mineX, prevY + mineY].TileType == oldTileType)
                                tilesToRandomize.Add((prevX + mineX, prevY + mineY));
                        }
                        for (int i = 0; i < ItemPet.Randomizer(tileBreakSpreadChance); i++)
                        {
                            if (tilesToRandomize.Count > 0)
                            {
                                var tileToBreak = tilesToRandomize[Main.rand.Next(tilesToRandomize.Count)];
                                if (Player.HasEnoughPickPowerToHurtTile(tileToBreak.X, tileToBreak.Y))
                                {
                                    Player.PickTile(tileToBreak.X, tileToBreak.Y, 5000);
                                    if (ModContent.GetInstance<Personalization>().AbilitySoundDisabled == false)
                                        SoundEngine.PlaySound(SoundID.WormDig with { PitchVariance = 0.4f }, Player.position);
                                }
                            }
                        }
                    }
                    prevX = Main.SmartCursorX;
                    prevY = Main.SmartCursorY;
                    oldTileType = tile.TileType;
                }
                else
                {
                    Tile tile = Main.tile[Player.tileTargetX, Player.tileTargetY];
                    if (Pet.PetInUse(ItemID.EaterOfWorldsPetItem) && TileID.Sets.Ore[tile.TileType] == false && TileChecks.gemTiles.Contains(tile.TileType) == false && Player.controlUseItem)
                    {
                        Player.pickSpeed -= nonOreSpeed;
                    }
                    if (Pet.PetInUse(ItemID.EaterOfWorldsPetItem) && Player.controlUseItem && Player.HeldItem.pick > 0 && Main.tile[prevX, prevY].TileType == 0 && oldTileType != 0 && (TileID.Sets.Ore[oldTileType] || TileChecks.gemTiles.Contains(oldTileType)) && Main.rand.NextBool(tileBreakSpreadChance, 100))
                    {
                        for (mineX = -tileBreakXSpread; mineX <= tileBreakXSpread; mineX++)
                        {
                            for (mineY = -tileBreakYSpread; mineY <= tileBreakYSpread; mineY++)
                            {
                                if (Main.tile[prevX + mineX, prevY + mineY].TileType == oldTileType)
                                    tilesToRandomize.Add((prevX + mineX, prevY + mineY));
                            }
                            if (Main.tile[prevX + mineX, prevY + mineY].TileType == oldTileType)
                                tilesToRandomize.Add((prevX + mineX, prevY + mineY));
                        }
                        for (int i = 0; i < ItemPet.Randomizer(tileBreakSpreadChance); i++)
                        {
                            if (tilesToRandomize.Count > 0)
                            {
                                var tileToBreak = tilesToRandomize[Main.rand.Next(tilesToRandomize.Count)];
                                if (Player.HasEnoughPickPowerToHurtTile(tileToBreak.X,tileToBreak.Y))
                                {
                                    Player.PickTile(tileToBreak.X,tileToBreak.Y, 5000);
                                    if (ModContent.GetInstance<Personalization>().AbilitySoundDisabled == false)
                                        SoundEngine.PlaySound(SoundID.WormDig with { PitchVariance = 0.4f }, Player.position);
                                }
                            }
                        }
                    }
                    prevX = Player.tileTargetX;
                    prevY = Player.tileTargetY;
                    oldTileType = tile.TileType;
                }
            }
        }
    }
    sealed public class EaterOfWorldsPetItem : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation) => entity.type == ItemID.EaterOfWorldsPetItem;

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (ModContent.GetInstance<Personalization>().TooltipsEnabledWithShift && !PlayerInput.Triggers.Current.KeyStatus[TriggerNames.Down]) return;
            EaterOfWorms eaterOfWorms = Main.LocalPlayer.GetModPlayer<EaterOfWorms>();
            tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.EaterOfWorldsPetItem")
                       .Replace("<miningSpeed>", (eaterOfWorms.nonOreSpeed * 100).ToString())
                       .Replace("<multipleBreakChance>", eaterOfWorms.tileBreakSpreadChance.ToString())
                       .Replace("<width>", eaterOfWorms.tileBreakXSpread.ToString())
                       .Replace("<length>", eaterOfWorms.tileBreakYSpread.ToString())
                       ));
        }
    }
}
