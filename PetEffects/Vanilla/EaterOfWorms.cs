using Terraria.Audio;
using Terraria;
using Terraria.ID;
using PetsOverhaul.Systems;
using Terraria.ModLoader;
using PetsOverhaul.Config;
using System.Collections.Generic;
using Terraria.Localization;

namespace PetsOverhaul.PetEffects.Vanilla
{
    sealed public class EaterOfWorms : ModPlayer
    {
        GlobalPet Pet { get => Player.GetModPlayer<GlobalPet>(); }
        public int tileBreakXSpread = 2;
        public int tileBreakYSpread = 2;
        public int tileBreakSpreadChance = 60;
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
                if (Main.SmartCursorShowing)
                {
                    Tile tile = Main.tile[Main.SmartCursorX, Main.SmartCursorY];

                    if (Pet.PetInUse(ItemID.EaterOfWorldsPetItem) && TileID.Sets.Ore[tile.TileType] == false && TileChecks.gemTile[tile.TileType] == false && Player.controlUseItem)
                    {
                        Player.pickSpeed -= nonOreSpeed;
                    }
                    if (Pet.PetInUse(ItemID.EaterOfWorldsPetItem) && Player.controlUseItem && Player.HeldItem.pick > 0 && Main.tile[prevX, prevY].TileType == 0 && oldTileType != 0 && (TileID.Sets.Ore[oldTileType] || TileChecks.gemTile[oldTileType]) && Main.rand.NextBool(tileBreakSpreadChance, 100))
                    {
                        for (mineX = -tileBreakXSpread; mineX <= tileBreakXSpread; mineX++)
                        {
                            for (mineY = -tileBreakYSpread; mineY <= tileBreakYSpread; mineY++)
                            {
                                if (Main.tile[prevX + mineX, prevY + mineY].TileType == oldTileType && (mineX != 0 || mineY != 0) && mineX < tileBreakXSpread && mineY < tileBreakYSpread)
                                    break;
                            }
                            if (Main.tile[prevX + mineX, prevY + mineY].TileType == oldTileType && (mineX != 0 || mineY != 0) && mineX < tileBreakXSpread && mineY < tileBreakYSpread)
                                break;
                        }
                        if (Main.tile[prevX + mineX, prevY + mineY].TileType == oldTileType && (mineX != 0 || mineY != 0) && mineX < tileBreakXSpread && mineY < tileBreakYSpread)
                        {
                            if (Player.HasEnoughPickPowerToHurtTile(prevX + mineX, prevY + mineY))
                            {
                                Player.PickTile(prevX + mineX, prevY + mineY, 5000);
                                if (ModContent.GetInstance<Personalization>().AbilitySoundDisabled == false)
                                    SoundEngine.PlaySound(SoundID.WormDig with { PitchVariance = 0.4f }, Player.position);
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
                    if (Pet.PetInUse(ItemID.EaterOfWorldsPetItem) && TileID.Sets.Ore[tile.TileType] == false && TileChecks.gemTile[tile.TileType] == false && Player.controlUseItem)
                    {
                        Player.pickSpeed -= nonOreSpeed;
                    }
                    if (Pet.PetInUse(ItemID.EaterOfWorldsPetItem) && Player.controlUseItem && Player.HeldItem.pick > 0 && Main.tile[prevX, prevY].TileType == 0 && oldTileType != 0 && (TileID.Sets.Ore[oldTileType] || TileChecks.gemTile[oldTileType]) && Main.rand.NextBool(tileBreakSpreadChance, 100))
                    {
                        for (mineX = -tileBreakXSpread; mineX <= tileBreakXSpread; mineX++)
                        {
                            for (mineY = -tileBreakYSpread; mineY <= tileBreakYSpread; mineY++)
                            {
                                if (Main.tile[prevX + mineX, prevY + mineY].TileType == oldTileType && (mineX != 0 || mineY != 0) && mineX < tileBreakXSpread && mineY < tileBreakYSpread)
                                    break;
                            }
                            if (Main.tile[prevX + mineX, prevY + mineY].TileType == oldTileType && (mineX != 0 || mineY != 0) && mineX < tileBreakXSpread && mineY < tileBreakYSpread)
                                break;
                        }
                        if (Main.tile[prevX + mineX, prevY + mineY].TileType == oldTileType && (mineX != 0 || mineY != 0) && mineX < tileBreakXSpread && mineY < tileBreakYSpread)
                        {
                            if (Player.HasEnoughPickPowerToHurtTile(prevX + mineX, prevY + mineY))
                            {
                                Player.PickTile(prevX + mineX, prevY + mineY, 5000);
                                if (ModContent.GetInstance<Personalization>().AbilitySoundDisabled == false)
                                    SoundEngine.PlaySound(SoundID.WormDig with { PitchVariance = 0.4f }, Player.position);
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
