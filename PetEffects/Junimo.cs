using Microsoft.Xna.Framework;
using System.Linq;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using PetsOverhaul.Systems;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using PetsOverhaul.Config;
using Terraria.DataStructures;
using PetsOverhaul.Buffs;
using System.IO;
namespace PetsOverhaul.PetEffects
{
    sealed public class Junimo : ModPlayer
    {
        GlobalPet Pet { get => Player.GetModPlayer<GlobalPet>(); }
        public int maxLvls = 27;
        public int junimoHarvestingLevel = 1;
        public int junimoHarvestingExp = 0;
        public int junimoHarvestingLevelExpNeeded = 0;
        public int junimoMiningLevel = 1;
        public int junimoMiningExp = 0;
        public int junimoMiningLevelExpNeeded = 0;
        public int junimoFishingLevel = 1;
        public int junimoFishingExp = 0;
        public int junimoFishingLevelExpNeeded = 0;
        public int junimoInUseMultiplier = 1;
        public bool anglerQuestDayCheck = false;
        public bool[] fish4 = ItemID.Sets.Factory.CreateBoolSet(false, ItemID.ChaosFish, ItemID.FlarefinKoi);
        public bool[] fish2 = ItemID.Sets.Factory.CreateBoolSet(false, ItemID.BlueJellyfish, ItemID.GreenJellyfish, ItemID.PinkJellyfish, ItemID.Obsidifish, ItemID.Prismite, ItemID.Stinkfish, ItemID.ArmoredCavefish, ItemID.Damselfish, ItemID.DoubleCod, ItemID.Ebonkoi, ItemID.FrostMinnow, ItemID.Hemopiranha, ItemID.Honeyfin, ItemID.PrincessFish, ItemID.Shrimp, ItemID.VariegatedLardfish);
        public bool[] fish0 = ItemID.Sets.Factory.CreateBoolSet(false, ItemID.FishingSeaweed, ItemID.OldShoe, ItemID.TinCan);
        public bool[] mining1 = ItemID.Sets.Factory.CreateBoolSet(false, ItemID.Obsidian, ItemID.SiltBlock, ItemID.SlushBlock, ItemID.DesertFossil);
        public bool[] mining2 = ItemID.Sets.Factory.CreateBoolSet(false, ItemID.CopperOre, ItemID.TinOre);
        public bool[] mining3 = ItemID.Sets.Factory.CreateBoolSet(false, ItemID.IronOre, ItemID.LeadOre, ItemID.Amethyst);
        public bool[] mining4 = ItemID.Sets.Factory.CreateBoolSet(false, ItemID.SilverOre, ItemID.TungstenOre, ItemID.Topaz, ItemID.Sapphire, ItemID.Meteorite);
        public bool[] mining5 = ItemID.Sets.Factory.CreateBoolSet(false, ItemID.GoldOre, ItemID.PlatinumOre, ItemID.Emerald, ItemID.Ruby, ItemID.Hellstone);
        public bool[] mining6 = ItemID.Sets.Factory.CreateBoolSet(false, ItemID.CrimtaneOre, ItemID.DemoniteOre, ItemID.Diamond, ItemID.Amber);
        public bool[] mining8 = ItemID.Sets.Factory.CreateBoolSet(false, ItemID.CobaltOre, ItemID.PalladiumOre);
        public bool[] mining10 = ItemID.Sets.Factory.CreateBoolSet(false, ItemID.MythrilOre, ItemID.OrichalcumOre);
        public bool[] mining12 = ItemID.Sets.Factory.CreateBoolSet(false, ItemID.AdamantiteOre, ItemID.TitaniumOre);
        public bool[] mining13 = ItemID.Sets.Factory.CreateBoolSet(false, ItemID.ChlorophyteOre, ItemID.LunarOre);
        public bool junimoExpCheck()
        {
            if (ModContent.GetInstance<Personalization>().JunimoExpWhileNotInInv == false || Player.HasItemInInventoryOrOpenVoidBag(ItemID.JunimoPetItem) || Pet.PetInUse(ItemID.JunimoPetItem))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public override bool OnPickup(Item item)
        {
            if (item.TryGetGlobalItem(out ItemPet itemChck))
            {
                if (item.maxStack != 1 && Player.CanPullItem(item, Player.ItemSpace(item)) && itemChck.pickedUpBefore == false)
                {
                    if (itemChck.oreBoost)
                    {
                        if (junimoExpCheck())
                        {
                            if (mining1[item.type])
                            {
                                junimoMiningExp += 1 * item.stack * junimoInUseMultiplier;
                            }
                            else if (mining2[item.type])
                            {
                                junimoMiningExp += 2 * item.stack * junimoInUseMultiplier;
                            }
                            else if (mining3[item.type])
                            {
                                junimoMiningExp += 3 * item.stack * junimoInUseMultiplier;
                            }
                            else if (mining4[item.type])
                            {
                                junimoMiningExp += 4 * item.stack * junimoInUseMultiplier;
                            }
                            else if (mining5[item.type])
                            {
                                junimoMiningExp += 5 * item.stack * junimoInUseMultiplier;
                            }
                            else if (mining6[item.type])
                            {
                                junimoMiningExp += 6 * item.stack * junimoInUseMultiplier;
                            }
                            else if (mining8[item.type])
                            {
                                junimoMiningExp += 8 * item.stack * junimoInUseMultiplier;
                            }
                            else if (mining10[item.type])
                            {
                                junimoMiningExp += 10 * item.stack * junimoInUseMultiplier;
                            }
                            else if (mining12[item.type])
                            {
                                junimoMiningExp += 12 * item.stack * junimoInUseMultiplier;
                            }
                            else if (mining13[item.type])
                            {
                                junimoMiningExp += 13 * item.stack * junimoInUseMultiplier;
                            }
                            else
                            {
                                junimoMiningExp += 1 * item.stack * junimoInUseMultiplier;
                            }
                        }
                        if (Player.HasItemInInventoryOrOpenVoidBag(ItemID.JunimoPetItem) || Pet.PetInUse(ItemID.JunimoPetItem))
                            item.stack += ItemPet.Randomizer(junimoMiningLevel * junimoInUseMultiplier*item.stack);
                    }
                    if (itemChck.rareHerbBoost)
                    {
                        if (junimoExpCheck())
                        {
                            if (item.type == ItemID.LifeFruit)
                                junimoHarvestingExp += 100 * item.stack * junimoInUseMultiplier;
                            else
                                junimoHarvestingExp += 35 * item.stack * junimoInUseMultiplier;
                        }
                        if (Player.HasItemInInventoryOrOpenVoidBag(ItemID.JunimoPetItem) || Pet.PetInUse(ItemID.JunimoPetItem))
                        {
                            int junimoCash = junimoHarvestingLevel * 50 * junimoInUseMultiplier* item.stack;
                            if (junimoCash > 1000000)
                            {
                                Player.QuickSpawnItem(Player.GetSource_Misc("Junimo"), ItemID.PlatinumCoin, junimoCash / 1000000);
                                junimoCash %= 1000000;
                            }
                            if (junimoCash > 10000)
                            {
                                Player.QuickSpawnItem(Player.GetSource_Misc("Junimo"), ItemID.GoldCoin, junimoCash / 10000);
                                junimoCash %= 10000;
                            }
                            if (junimoCash > 100)
                            {
                                Player.QuickSpawnItem(Player.GetSource_Misc("Junimo"), ItemID.SilverCoin, junimoCash / 100);
                                junimoCash %= 100;
                            }
                            Player.QuickSpawnItem(Player.GetSource_Misc("Junimo"), ItemID.CopperCoin, junimoCash);
                        }
                    }
                    if (itemChck.herbBoost || itemChck.tree)
                    {
                        if (junimoExpCheck())
                        {
                            if (itemChck.tree == true)
                            {
                                junimoHarvestingExp += 1 * item.stack * junimoInUseMultiplier;
                            }
                            if (itemChck.herbBoost == true)
                            {
                                junimoHarvestingExp += 2 * item.stack * junimoInUseMultiplier;
                            }
                        }
                        if (Player.HasItemInInventoryOrOpenVoidBag(ItemID.JunimoPetItem) || Pet.PetInUse(ItemID.JunimoPetItem))
                        {
                            int junimoCash = ItemPet.Randomizer(junimoHarvestingLevel * 25 * junimoInUseMultiplier *item.stack, 10);
                            if (junimoCash > 1000000)
                            {
                                Player.QuickSpawnItem(Player.GetSource_Misc("Junimo"), ItemID.PlatinumCoin, junimoCash / 1000000);
                                junimoCash %= 1000000;
                            }
                            if (junimoCash > 10000)
                            {
                                Player.QuickSpawnItem(Player.GetSource_Misc("Junimo"), ItemID.GoldCoin, junimoCash / 10000);
                                junimoCash %= 10000;
                            }
                            if (junimoCash > 100)
                            {
                                Player.QuickSpawnItem(Player.GetSource_Misc("Junimo"), ItemID.SilverCoin, junimoCash / 100);
                                junimoCash %= 100;
                            }
                            Player.QuickSpawnItem(Player.GetSource_Misc("Junimo"), ItemID.CopperCoin, junimoCash);
                        }
                    }
                    
                }
            }
            return true;
        }
        public override void PostUpdateEquips()
        {
            if (Pet.PetInUse(ItemID.JunimoPetItem) || Player.HasItemInInventoryOrOpenVoidBag(ItemID.JunimoPetItem))
            {
                int noSwapCd = Player.HasBuff(ModContent.BuffType<ObliviousPet>()) ?  1 : 2;
                Player.endurance += junimoMiningLevel * 0.002f * junimoInUseMultiplier;
                Player.GetDamage<GenericDamageClass>() *= 1f + junimoFishingLevel * 0.002f * noSwapCd;
                if (Player.statLifeMax2 * junimoHarvestingLevel * 0.0025f * junimoInUseMultiplier > junimoHarvestingLevel * noSwapCd)
                {
                    Player.statLifeMax2 += (int)(Player.statLifeMax2 * junimoHarvestingLevel * 0.0025f * noSwapCd);
                }
                else
                {
                    Player.statLifeMax2 += junimoHarvestingLevel * noSwapCd;
                }
            }

        }
        public override void AnglerQuestReward(float rareMultiplier, List<Item> rewardItems)
        {
            if (anglerQuestDayCheck == false && junimoExpCheck())
            {
                junimoFishingExp += 35 * junimoInUseMultiplier;
                anglerQuestDayCheck = true;
            }
        }
        public override void ModifyCaughtFish(Item fish)
        {
            if (junimoExpCheck())
            {
                if (fish.type == ItemID.GoldenCarp)
                    junimoFishingExp += 6 * junimoInUseMultiplier*fish.stack;
                else if (ItemID.Sets.IsFishingCrateHardmode[fish.type])
                    junimoFishingExp += 5 * junimoInUseMultiplier*fish.stack;
                else if (fish4[fish.type])
                    junimoFishingExp += 4 * junimoInUseMultiplier*fish.stack;
                else if (ItemID.Sets.IsFishingCrate[fish.type])
                    junimoFishingExp += 3 * junimoInUseMultiplier*fish.stack;
                else if (fish2[fish.type])
                    junimoFishingExp += 2 * junimoInUseMultiplier*fish.stack;
                else if (fish0[fish.type])
                {

                }
                else
                    junimoFishingExp += 1 * junimoInUseMultiplier*fish.stack;

            }
        }
        public override void GetFishingLevel(Item fishingRod, Item bait, ref float fishingLevel)
        {
            if (Pet.PetInUse(ItemID.JunimoPetItem))
            {
                fishingLevel += junimoFishingLevel * 0.01f;
            }
            else if (Player.HasItemInInventoryOrOpenVoidBag(ItemID.JunimoPetItem))
            {
                fishingLevel += junimoFishingLevel * 0.005f;
            }
        }
        public override void PreUpdate()
        {
            maxLvls = 27;
            if (Main.dayTime == true && Main.time == 0)
            {
                anglerQuestDayCheck = false;
            }
            if (Pet.PetInUse(ItemID.JunimoPetItem))
            {
                junimoInUseMultiplier = 2;
            }
            else
            {
                junimoInUseMultiplier = 1;
            }
            if (junimoHarvestingLevel < 1)
            {
                junimoHarvestingLevel = 1;
            }
            if (junimoHarvestingLevel > maxLvls)
            {
                junimoHarvestingLevel = maxLvls;
            }
            if (junimoMiningLevel < 1)
            {
                junimoMiningLevel = 1;
            }
            if (junimoMiningLevel > maxLvls)
            {
                junimoMiningLevel = maxLvls;
            }
            if (junimoFishingLevel < 1)
            {
                junimoFishingLevel = 1;
            }
            if (junimoFishingLevel > maxLvls)
            {
                junimoFishingLevel = maxLvls;
            }
            if (junimoHarvestingExp < 0)
            {
                junimoHarvestingExp = 0;
            }
            if (junimoMiningExp < 0)
            {
                junimoMiningExp = 0;
            }
            if (junimoFishingExp < 0)
            {
                junimoFishingExp = 0;
            }
            if (junimoMiningExp > 2147480000)
            {
                junimoMiningExp = 2147480000;
            }
            if (junimoHarvestingExp > 2147480000)
            {
                junimoHarvestingExp = 2147480000;
            }
            if (junimoFishingExp > 2147480000)
            {
                junimoFishingExp = 2147480000;
            }
            AdvancedPopupRequest popupMessage = new();
            popupMessage.DurationInFrames = 300;
            popupMessage.Velocity = new Vector2(Main.rand.NextFloat(-3, 3), Main.rand.NextFloat(-15, -10));
            bool notificationOff = ModContent.GetInstance<Personalization>().JunimoNotifOff;
            bool soundOff = ModContent.GetInstance<Personalization>().AbilitySoundDisabled;
            switch (junimoHarvestingLevel)
            {
                case 1:
                    junimoHarvestingLevelExpNeeded = 20;
                    if (junimoHarvestingExp >= junimoHarvestingLevelExpNeeded)
                    {
                        if (notificationOff == false)
                        {
                            if (soundOff == false)
                                SoundEngine.PlaySound(SoundID.Item35 with { PitchVariance = 0.2f, Pitch = 0.5f }, Player.position);
                            popupMessage.Color = Color.LightGreen;
                            popupMessage.Text = "Junimo harvesting level up!";
                            PopupText.NewText(popupMessage, Player.position);
                        }
                        junimoHarvestingLevel++;
                    }
                    break;
                case 2:
                    junimoHarvestingLevelExpNeeded = 50;
                    if (junimoHarvestingExp >= junimoHarvestingLevelExpNeeded)
                    {

                        if (notificationOff == false)
                        {
                            if (soundOff == false)
                                SoundEngine.PlaySound(SoundID.Item35 with { PitchVariance = 0.2f, Pitch = 0.5f }, Player.position);
                            popupMessage.Color = Color.LightGreen;
                            popupMessage.Text = "Junimo harvesting level up!";
                            PopupText.NewText(popupMessage, Player.position);
                        }
                        junimoHarvestingLevel++;
                    }
                    break;
                case 3:
                    junimoHarvestingLevelExpNeeded = 110;
                    if (junimoHarvestingExp >= junimoHarvestingLevelExpNeeded)
                    {
                        if (notificationOff == false)
                        {
                            if (soundOff == false)
                                SoundEngine.PlaySound(SoundID.Item35 with { PitchVariance = 0.2f, Pitch = 0.5f }, Player.position);
                            popupMessage.Color = Color.LightGreen;
                            popupMessage.Text = "Junimo harvesting level up!";
                            PopupText.NewText(popupMessage, Player.position);
                        }
                        junimoHarvestingLevel++;
                    }
                    break;
                case 4:
                    junimoHarvestingLevelExpNeeded = 200;
                    if (junimoHarvestingExp >= junimoHarvestingLevelExpNeeded)
                    {
                        if (notificationOff == false)
                        {
                            if (soundOff == false)
                                SoundEngine.PlaySound(SoundID.Item35 with { PitchVariance = 0.2f, Pitch = 0.5f }, Player.position);
                            popupMessage.Color = Color.LightGreen;
                            popupMessage.Text = "Junimo harvesting level up!";
                            PopupText.NewText(popupMessage, Player.position);
                        }
                        junimoHarvestingLevel++;
                    }
                    break;
                case 5:
                    junimoHarvestingLevelExpNeeded = 325;
                    if (junimoHarvestingExp >= junimoHarvestingLevelExpNeeded)
                    {
                        if (notificationOff == false)
                        {
                            if (soundOff == false)
                                SoundEngine.PlaySound(SoundID.Item35 with { PitchVariance = 0.2f, Pitch = 0.5f }, Player.position);
                            popupMessage.Color = Color.LightGreen;
                            popupMessage.Text = "Junimo harvesting level up!";
                            PopupText.NewText(popupMessage, Player.position);
                        }
                        junimoHarvestingLevel++;
                    }
                    break;
                case 6:
                    junimoHarvestingLevelExpNeeded = 500;
                    if (junimoHarvestingExp >= junimoHarvestingLevelExpNeeded)
                    {
                        if (notificationOff == false)
                        {
                            if (soundOff == false)
                                SoundEngine.PlaySound(SoundID.Item35 with { PitchVariance = 0.2f, Pitch = 0.5f }, Player.position);
                            popupMessage.Color = Color.LightGreen;
                            popupMessage.Text = "Junimo harvesting level up!";
                            PopupText.NewText(popupMessage, Player.position);
                        }
                        junimoHarvestingLevel++;
                    }
                    break;
                case 7:
                    junimoHarvestingLevelExpNeeded = 700;
                    if (junimoHarvestingExp >= junimoHarvestingLevelExpNeeded)
                    {
                        if (notificationOff == false)
                        {
                            if (soundOff == false)
                                SoundEngine.PlaySound(SoundID.Item35 with { PitchVariance = 0.2f, Pitch = 0.5f }, Player.position);
                            popupMessage.Color = Color.LightGreen;
                            popupMessage.Text = "Junimo harvesting level up!";
                            PopupText.NewText(popupMessage, Player.position);
                        }
                        junimoHarvestingLevel++;
                    }
                    break;
                case 8:
                    junimoHarvestingLevelExpNeeded = 950;
                    if (junimoHarvestingExp >= junimoHarvestingLevelExpNeeded)
                    {
                        if (notificationOff == false)
                        {
                            if (soundOff == false)
                                SoundEngine.PlaySound(SoundID.Item35 with { PitchVariance = 0.2f, Pitch = 0.5f }, Player.position);
                            popupMessage.Color = Color.LightGreen;
                            popupMessage.Text = "Junimo harvesting level up!";
                            PopupText.NewText(popupMessage, Player.position);
                        }
                        junimoHarvestingLevel++;
                    }
                    break;
                case 9:
                    junimoHarvestingLevelExpNeeded = 1275;
                    if (junimoHarvestingExp >= junimoHarvestingLevelExpNeeded)
                    {
                        if (notificationOff == false)
                        {
                            if (soundOff == false)
                                SoundEngine.PlaySound(SoundID.Item35 with { PitchVariance = 0.2f, Pitch = 0.5f }, Player.position);
                            popupMessage.Color = Color.LightGreen;
                            popupMessage.Text = "Junimo harvesting level up!";
                            PopupText.NewText(popupMessage, Player.position);
                        }
                        junimoHarvestingLevel++;
                    }
                    break;
                case 10:
                    junimoHarvestingLevelExpNeeded = 1700;
                    if (junimoHarvestingExp >= junimoHarvestingLevelExpNeeded)
                    {
                        if (notificationOff == false)
                        {
                            if (soundOff == false)
                                SoundEngine.PlaySound(SoundID.Item35 with { PitchVariance = 0.2f, Pitch = 0.5f }, Player.position);
                            popupMessage.Color = Color.LightGreen;
                            popupMessage.Text = "Junimo harvesting level up!";
                            PopupText.NewText(popupMessage, Player.position);
                        }
                        junimoHarvestingLevel++;
                    }
                    break;
                case 11:
                    junimoHarvestingLevelExpNeeded = 2175;
                    if (junimoHarvestingExp >= junimoHarvestingLevelExpNeeded)
                    {
                        if (notificationOff == false)
                        {
                            if (soundOff == false)
                                SoundEngine.PlaySound(SoundID.Item35 with { PitchVariance = 0.2f, Pitch = 0.5f }, Player.position);
                            popupMessage.Color = Color.LightGreen;
                            popupMessage.Text = "Junimo harvesting level up!";
                            PopupText.NewText(popupMessage, Player.position);
                        }
                        junimoHarvestingLevel++;
                    }
                    break;
                case 12:
                    junimoHarvestingLevelExpNeeded = 2700;
                    if (junimoHarvestingExp >= junimoHarvestingLevelExpNeeded)
                    {
                        if (notificationOff == false)
                        {
                            if (soundOff == false)
                                SoundEngine.PlaySound(SoundID.Item35 with { PitchVariance = 0.2f, Pitch = 0.5f }, Player.position);
                            popupMessage.Color = Color.LightGreen;
                            popupMessage.Text = "Junimo harvesting level up!";
                            PopupText.NewText(popupMessage, Player.position);
                        }
                        junimoHarvestingLevel++;
                    }
                    break;
                case 13:
                    junimoHarvestingLevelExpNeeded = 3300;
                    if (junimoHarvestingExp >= junimoHarvestingLevelExpNeeded)
                    {
                        if (notificationOff == false)
                        {
                            if (soundOff == false)
                                SoundEngine.PlaySound(SoundID.Item35 with { PitchVariance = 0.2f, Pitch = 0.5f }, Player.position);
                            popupMessage.Color = Color.LightGreen;
                            popupMessage.Text = "Junimo harvesting level up!";
                            PopupText.NewText(popupMessage, Player.position);
                        }
                        junimoHarvestingLevel++;
                    }
                    break;
                case 14:
                    junimoHarvestingLevelExpNeeded = 4000;
                    if (junimoHarvestingExp >= junimoHarvestingLevelExpNeeded)
                    {
                        if (notificationOff == false)
                        {
                            if (soundOff == false)
                                SoundEngine.PlaySound(SoundID.Item35 with
                                {
                                    PitchVariance = 0.2f,
                                    Pitch = 0.5f
                                }, Player.position);
                            popupMessage.Color = Color.LightGreen;
                            popupMessage.Text = "Junimo harvesting level up!";
                            PopupText.NewText(popupMessage, Player.position);
                        }
                        junimoHarvestingLevel++;
                    }
                    break;
                case 15:
                    junimoHarvestingLevelExpNeeded = 4700;
                    if (junimoHarvestingExp >= junimoHarvestingLevelExpNeeded)
                    {
                        if (notificationOff == false)
                        {
                            if (soundOff == false)
                                SoundEngine.PlaySound(SoundID.Item35 with
                                {
                                    PitchVariance = 0.2f,
                                    Pitch = 0.5f
                                }, Player.position);
                            popupMessage.Color = Color.LightGreen;
                            popupMessage.Text = "Junimo harvesting level up!";
                            PopupText.NewText(popupMessage, Player.position);
                        }
                        junimoHarvestingLevel++;
                    }
                    break;
                case 16:
                    junimoHarvestingLevelExpNeeded = 5600;
                    if (junimoHarvestingExp >= junimoHarvestingLevelExpNeeded)
                    {
                        if (notificationOff == false)
                        {
                            if (soundOff == false)
                                SoundEngine.PlaySound(SoundID.Item35 with
                                {
                                    PitchVariance = 0.2f,
                                    Pitch = 0.5f
                                }, Player.position);
                            popupMessage.Color = Color.LightGreen;
                            popupMessage.Text = "Junimo harvesting level up!";
                            PopupText.NewText(popupMessage, Player.position);
                        }
                        junimoHarvestingLevel++;
                    }
                    break;
                case 17:
                    junimoHarvestingLevelExpNeeded = 6700;
                    if (junimoHarvestingExp >= junimoHarvestingLevelExpNeeded)
                    {
                        if (notificationOff == false)
                        {
                            if (soundOff == false)
                                SoundEngine.PlaySound(SoundID.Item35 with
                                {
                                    PitchVariance = 0.2f,
                                    Pitch = 0.5f
                                }, Player.position);
                            popupMessage.Color = Color.LightGreen;
                            popupMessage.Text = "Junimo harvesting level up!";
                            PopupText.NewText(popupMessage, Player.position);
                        }
                        junimoHarvestingLevel++;
                    }
                    break;
                case 18:
                    junimoHarvestingLevelExpNeeded = 8000;
                    if (junimoHarvestingExp >= junimoHarvestingLevelExpNeeded)
                    {
                        if (notificationOff == false)
                        {
                            if (soundOff == false)
                                SoundEngine.PlaySound(SoundID.Item35 with
                                {
                                    PitchVariance = 0.2f,
                                    Pitch = 0.5f
                                }, Player.position);
                            popupMessage.Color = Color.LightGreen;
                            popupMessage.Text = "Junimo harvesting level up!";
                            PopupText.NewText(popupMessage, Player.position);
                        }
                        junimoHarvestingLevel++;
                    }
                    break;
                case 19:
                    junimoHarvestingLevelExpNeeded = 9500;
                    if (junimoHarvestingExp >= junimoHarvestingLevelExpNeeded)
                    {
                        if (notificationOff == false)
                        {
                            if (soundOff == false)
                                SoundEngine.PlaySound(SoundID.Item35 with
                                {
                                    PitchVariance = 0.2f,
                                    Pitch = 0.5f
                                }, Player.position);
                            popupMessage.Color = Color.LightGreen;
                            popupMessage.Text = "Junimo harvesting level up!";
                            PopupText.NewText(popupMessage, Player.position);
                        }
                        junimoHarvestingLevel++;
                    }
                    break;
                case 20:
                    junimoHarvestingLevelExpNeeded = 11250;
                    if (junimoHarvestingExp >= junimoHarvestingLevelExpNeeded)
                    {
                        if (notificationOff == false)
                        {
                            if (soundOff == false)
                                SoundEngine.PlaySound(SoundID.Item35 with
                                {
                                    PitchVariance = 0.2f,
                                    Pitch = 0.5f
                                }, Player.position);
                            popupMessage.Color = Color.LightGreen;
                            popupMessage.Text = "Junimo harvesting level up!";
                            PopupText.NewText(popupMessage, Player.position);
                        }
                        junimoHarvestingLevel++;
                    }
                    break;
                case 21:
                    junimoHarvestingLevelExpNeeded = 13500;
                    if (junimoHarvestingExp >= junimoHarvestingLevelExpNeeded)
                    {
                        if (notificationOff == false)
                        {
                            if (soundOff == false)
                                SoundEngine.PlaySound(SoundID.Item35 with
                                {
                                    PitchVariance = 0.2f,
                                    Pitch = 0.5f
                                }, Player.position);
                            popupMessage.Color = Color.LightGreen;
                            popupMessage.Text = "Junimo harvesting level up!";
                            PopupText.NewText(popupMessage, Player.position);
                        }
                        junimoHarvestingLevel++;
                    }
                    break;
                case 22:
                    junimoHarvestingLevelExpNeeded = 16500;
                    if (junimoHarvestingExp >= junimoHarvestingLevelExpNeeded)
                    {
                        if (notificationOff == false)
                        {
                            if (soundOff == false)
                                SoundEngine.PlaySound(SoundID.Item35 with
                                {
                                    PitchVariance = 0.2f,
                                    Pitch = 0.5f
                                }, Player.position);
                            popupMessage.Color = Color.LightGreen;
                            popupMessage.Text = "Junimo harvesting level up!";
                            PopupText.NewText(popupMessage, Player.position);
                        }
                        junimoHarvestingLevel++;
                    }
                    break;
                case 23:
                    junimoHarvestingLevelExpNeeded = 20000;
                    if (junimoHarvestingExp >= junimoHarvestingLevelExpNeeded)
                    {
                        if (notificationOff == false)
                        {
                            if (soundOff == false)
                                SoundEngine.PlaySound(SoundID.Item35 with
                                {
                                    PitchVariance = 0.2f,
                                    Pitch = 0.5f
                                }, Player.position);
                            popupMessage.Color = Color.LightGreen;
                            popupMessage.Text = "Junimo harvesting level up!";
                            PopupText.NewText(popupMessage, Player.position);
                        }
                        junimoHarvestingLevel++;
                    }
                    break;
                case 24:
                    junimoHarvestingLevelExpNeeded = 25000;
                    if (junimoHarvestingExp >= junimoHarvestingLevelExpNeeded)
                    {
                        if (notificationOff == false)
                        {
                            if (soundOff == false)
                                SoundEngine.PlaySound(SoundID.Item35 with
                                {
                                    PitchVariance = 0.2f,
                                    Pitch = 0.5f
                                }, Player.position);
                            popupMessage.Color = Color.LightGreen;
                            popupMessage.Text = "Junimo harvesting level up!";
                            PopupText.NewText(popupMessage, Player.position);
                        }
                        junimoHarvestingLevel++;
                    }
                    break;
                case 25:
                    junimoHarvestingLevelExpNeeded = 32500;
                    if (junimoHarvestingExp >= junimoHarvestingLevelExpNeeded)
                    {
                        if (notificationOff == false)
                        {
                            if (soundOff == false)
                                SoundEngine.PlaySound(SoundID.Item35 with
                                {
                                    PitchVariance = 0.2f,
                                    Pitch = 0.5f
                                }, Player.position);
                            popupMessage.Color = Color.LightGreen;
                            popupMessage.Text = "Junimo harvesting level up!";
                            PopupText.NewText(popupMessage, Player.position);
                        }
                        junimoHarvestingLevel++;
                    }
                    break;
                case 26:
                    junimoHarvestingLevelExpNeeded = 42500;
                    if (junimoHarvestingExp >= junimoHarvestingLevelExpNeeded)
                    {
                        if (notificationOff == false)
                        {
                            if (soundOff == false)
                                SoundEngine.PlaySound(SoundID.Item35 with
                                {
                                    PitchVariance = 0.2f,
                                    Pitch = 0.5f
                                }, Player.position);
                            popupMessage.Color = Color.LightGreen;
                            popupMessage.Text = "Junimo harvesting maxed!";
                            PopupText.NewText(popupMessage, Player.position);
                        }
                        junimoHarvestingLevel++;
                    }
                    break;
                case 27:
                    break;
            }
            switch (junimoMiningLevel)
            {
                case 1:
                    junimoMiningLevelExpNeeded = 15;
                    if (junimoMiningExp >= junimoMiningLevelExpNeeded)
                    {
                        if (notificationOff == false)
                        {
                            if (soundOff == false)
                                SoundEngine.PlaySound(SoundID.Item35 with
                                {
                                    PitchVariance = 0.2f,
                                    Pitch = 0.5f
                                }, Player.position);
                            popupMessage.Color = Color.LightGray;
                            popupMessage.Text = "Junimo mining level up!";
                            PopupText.NewText(popupMessage, Player.position);
                        }
                        junimoMiningLevel++;
                    }
                    break;
                case 2:
                    junimoMiningLevelExpNeeded = 40;
                    if (junimoMiningExp >= junimoMiningLevelExpNeeded)
                    {
                        if (notificationOff == false)
                        {
                            if (soundOff == false)
                                SoundEngine.PlaySound(SoundID.Item35 with
                                {
                                    PitchVariance = 0.2f,
                                    Pitch = 0.5f
                                }, Player.position);
                            popupMessage.Color = Color.LightGray;
                            popupMessage.Text = "Junimo mining level up!";
                            PopupText.NewText(popupMessage, Player.position);
                        }
                        junimoMiningLevel++;
                    }
                    break;
                case 3:
                    junimoMiningLevelExpNeeded = 80;
                    if (junimoMiningExp >= junimoMiningLevelExpNeeded)
                    {
                        if (notificationOff == false)
                        {
                            if (soundOff == false)
                                SoundEngine.PlaySound(SoundID.Item35 with
                                {
                                    PitchVariance = 0.2f,
                                    Pitch = 0.5f
                                }, Player.position);
                            popupMessage.Color = Color.LightGray;
                            popupMessage.Text = "Junimo mining level up!";
                            PopupText.NewText(popupMessage, Player.position);
                        }
                        junimoMiningLevel++;
                    }
                    break;
                case 4:
                    junimoMiningLevelExpNeeded = 135;
                    if (junimoMiningExp >= junimoMiningLevelExpNeeded)
                    {
                        if (notificationOff == false)
                        {
                            if (soundOff == false)
                                SoundEngine.PlaySound(SoundID.Item35 with
                                {
                                    PitchVariance = 0.2f,
                                    Pitch = 0.5f
                                }, Player.position);
                            popupMessage.Color = Color.LightGray;
                            popupMessage.Text = "Junimo mining level up!";
                            PopupText.NewText(popupMessage, Player.position);
                        }
                        junimoMiningLevel++;
                    }
                    break;
                case 5:
                    junimoMiningLevelExpNeeded = 200;
                    if (junimoMiningExp >= junimoMiningLevelExpNeeded)
                    {
                        if (notificationOff == false)
                        {
                            if (soundOff == false)
                                SoundEngine.PlaySound(SoundID.Item35 with
                                {
                                    PitchVariance = 0.2f,
                                    Pitch = 0.5f
                                }, Player.position);
                            popupMessage.Color = Color.LightGray;
                            popupMessage.Text = "Junimo mining level up!";
                            PopupText.NewText(popupMessage, Player.position);
                        }
                        junimoMiningLevel++;
                    }
                    break;
                case 6:
                    junimoMiningLevelExpNeeded = 290;
                    if (junimoMiningExp >= junimoMiningLevelExpNeeded)
                    {
                        if (notificationOff == false)
                        {
                            if (soundOff == false)
                                SoundEngine.PlaySound(SoundID.Item35 with
                                {
                                    PitchVariance = 0.2f,
                                    Pitch = 0.5f
                                }, Player.position);
                            popupMessage.Color = Color.LightGray;
                            popupMessage.Text = "Junimo mining level up!";
                            PopupText.NewText(popupMessage, Player.position);
                        }
                        junimoMiningLevel++;
                    }
                    break;
                case 7:
                    junimoMiningLevelExpNeeded = 400;
                    if (junimoMiningExp >= junimoMiningLevelExpNeeded)
                    {
                        if (notificationOff == false)
                        {
                            if (soundOff == false)
                                SoundEngine.PlaySound(SoundID.Item35 with
                                {
                                    PitchVariance = 0.2f,
                                    Pitch = 0.5f
                                }, Player.position);
                            popupMessage.Color = Color.LightGray;
                            popupMessage.Text = "Junimo mining level up!";
                            PopupText.NewText(popupMessage, Player.position);
                        }
                        junimoMiningLevel++;
                    }
                    break;
                case 8:
                    junimoMiningLevelExpNeeded = 550;
                    if (junimoMiningExp >= junimoMiningLevelExpNeeded)
                    {
                        if (notificationOff == false)
                        {
                            if (soundOff == false)
                                SoundEngine.PlaySound(SoundID.Item35 with
                                {
                                    PitchVariance = 0.2f,
                                    Pitch = 0.5f
                                }, Player.position);
                            popupMessage.Color = Color.LightGray;
                            popupMessage.Text = "Junimo mining level up!";
                            PopupText.NewText(popupMessage, Player.position);
                        }
                        junimoMiningLevel++;
                    }
                    break;
                case 9:
                    junimoMiningLevelExpNeeded = 750;
                    if (junimoMiningExp >= junimoMiningLevelExpNeeded)
                    {
                        if (notificationOff == false)
                        {
                            if (soundOff == false)
                                SoundEngine.PlaySound(SoundID.Item35 with
                                {
                                    PitchVariance = 0.2f,
                                    Pitch = 0.5f
                                }, Player.position);
                            popupMessage.Color = Color.LightGray;
                            popupMessage.Text = "Junimo mining level up!";
                            PopupText.NewText(popupMessage, Player.position);
                        }
                        junimoMiningLevel++;
                    }
                    break;
                case 10:
                    junimoMiningLevelExpNeeded = 1100;
                    if (junimoMiningExp >= junimoMiningLevelExpNeeded)
                    {
                        if (notificationOff == false)
                        {
                            if (soundOff == false)
                                SoundEngine.PlaySound(SoundID.Item35 with
                                {
                                    PitchVariance = 0.2f,
                                    Pitch = 0.5f
                                }, Player.position);
                            popupMessage.Color = Color.LightGray;
                            popupMessage.Text = "Junimo mining level up!";
                            PopupText.NewText(popupMessage, Player.position);
                        }
                        junimoMiningLevel++;
                    }
                    break;
                case 11:
                    junimoMiningLevelExpNeeded = 1550;
                    if (junimoMiningExp >= junimoMiningLevelExpNeeded)
                    {
                        if (notificationOff == false)
                        {
                            if (soundOff == false)
                                SoundEngine.PlaySound(SoundID.Item35 with
                                {
                                    PitchVariance = 0.2f,
                                    Pitch = 0.5f
                                }, Player.position);
                            popupMessage.Color = Color.LightGray;
                            popupMessage.Text = "Junimo mining level up!";
                            PopupText.NewText(popupMessage, Player.position);
                        }
                        junimoMiningLevel++;
                    }
                    break;
                case 12:
                    junimoMiningLevelExpNeeded = 2200;
                    if (junimoMiningExp >= junimoMiningLevelExpNeeded)
                    {
                        if (notificationOff == false)
                        {
                            if (soundOff == false)
                                SoundEngine.PlaySound(SoundID.Item35 with
                                {
                                    PitchVariance = 0.2f,
                                    Pitch = 0.5f
                                }, Player.position);
                            popupMessage.Color = Color.LightGray;
                            popupMessage.Text = "Junimo mining level up!";
                            PopupText.NewText(popupMessage, Player.position);
                        }
                        junimoMiningLevel++;
                    }
                    break;
                case 13:
                    junimoMiningLevelExpNeeded = 2900;
                    if (junimoMiningExp >= junimoMiningLevelExpNeeded)
                    {
                        if (notificationOff == false)
                        {
                            if (soundOff == false)
                                SoundEngine.PlaySound(SoundID.Item35 with
                                {
                                    PitchVariance = 0.2f,
                                    Pitch = 0.5f
                                }, Player.position);
                            popupMessage.Color = Color.LightGray;
                            popupMessage.Text = "Junimo mining level up!";
                            PopupText.NewText(popupMessage, Player.position);
                        }
                        junimoMiningLevel++;
                    }
                    break;
                case 14:
                    junimoMiningLevelExpNeeded = 3800;
                    if (junimoMiningExp >= junimoMiningLevelExpNeeded)
                    {
                        if (notificationOff == false)
                        {
                            if (soundOff == false)
                                SoundEngine.PlaySound(SoundID.Item35 with
                                {
                                    PitchVariance = 0.2f,
                                    Pitch = 0.5f
                                }, Player.position);
                            popupMessage.Color = Color.LightGray;
                            popupMessage.Text = "Junimo mining level up!";
                            PopupText.NewText(popupMessage, Player.position);
                        }
                        junimoMiningLevel++;
                    }
                    break;
                case 15:
                    junimoMiningLevelExpNeeded = 5000;
                    if (junimoMiningExp >= junimoMiningLevelExpNeeded)
                    {
                        if (notificationOff == false)
                        {
                            if (soundOff == false)
                                SoundEngine.PlaySound(SoundID.Item35 with
                                {
                                    PitchVariance = 0.2f,
                                    Pitch = 0.5f
                                }, Player.position);
                            popupMessage.Color = Color.LightGray;
                            popupMessage.Text = "Junimo mining level up!";
                            PopupText.NewText(popupMessage, Player.position);
                        }
                        junimoMiningLevel++;
                    }
                    break;
                case 16:
                    junimoMiningLevelExpNeeded = 6500;
                    if (junimoMiningExp >= junimoMiningLevelExpNeeded)
                    {
                        if (notificationOff == false)
                        {
                            if (soundOff == false)
                                SoundEngine.PlaySound(SoundID.Item35 with
                                {
                                    PitchVariance = 0.2f,
                                    Pitch = 0.5f
                                }, Player.position);
                            popupMessage.Color = Color.LightGray;
                            popupMessage.Text = "Junimo mining level up!";
                            PopupText.NewText(popupMessage, Player.position);
                        }
                        junimoMiningLevel++;
                    }
                    break;
                case 17:
                    junimoMiningLevelExpNeeded = 8500;
                    if (junimoMiningExp >= junimoMiningLevelExpNeeded)
                    {
                        if (notificationOff == false)
                        {
                            if (soundOff == false)
                                SoundEngine.PlaySound(SoundID.Item35 with
                                {
                                    PitchVariance = 0.2f,
                                    Pitch = 0.5f
                                }, Player.position);
                            popupMessage.Color = Color.LightGray;
                            popupMessage.Text = "Junimo mining level up!";
                            PopupText.NewText(popupMessage, Player.position);
                        }
                        junimoMiningLevel++;
                    }
                    break;
                case 18:
                    junimoMiningLevelExpNeeded = 11000;
                    if (junimoMiningExp >= junimoMiningLevelExpNeeded)
                    {
                        if (notificationOff == false)
                        {
                            if (soundOff == false)
                                SoundEngine.PlaySound(SoundID.Item35 with
                                {
                                    PitchVariance = 0.2f,
                                    Pitch = 0.5f
                                }, Player.position);
                            popupMessage.Color = Color.LightGray;
                            popupMessage.Text = "Junimo mining level up!";
                            PopupText.NewText(popupMessage, Player.position);
                        }
                        junimoMiningLevel++;
                    }
                    break;
                case 19:
                    junimoMiningLevelExpNeeded = 14000;
                    if (junimoMiningExp >= junimoMiningLevelExpNeeded)
                    {
                        if (notificationOff == false)
                        {
                            if (soundOff == false)
                                SoundEngine.PlaySound(SoundID.Item35 with
                                {
                                    PitchVariance = 0.2f,
                                    Pitch = 0.5f
                                }, Player.position);
                            popupMessage.Color = Color.LightGray;
                            popupMessage.Text = "Junimo mining level up!";
                            PopupText.NewText(popupMessage, Player.position);
                        }
                        junimoMiningLevel++;
                    }
                    break;
                case 20:
                    junimoMiningLevelExpNeeded = 18000;
                    if (junimoMiningExp >= junimoMiningLevelExpNeeded)
                    {
                        if (notificationOff == false)
                        {
                            if (soundOff == false)
                                SoundEngine.PlaySound(SoundID.Item35 with
                                {
                                    PitchVariance = 0.2f,
                                    Pitch = 0.5f
                                }, Player.position);
                            popupMessage.Color = Color.LightGray;
                            popupMessage.Text = "Junimo mining level up!";
                            PopupText.NewText(popupMessage, Player.position);
                        }
                        junimoMiningLevel++;
                    }
                    break;
                case 21:
                    junimoMiningLevelExpNeeded = 22000;
                    if (junimoMiningExp >= junimoMiningLevelExpNeeded)
                    {
                        if (notificationOff == false)
                        {
                            if (soundOff == false)
                                SoundEngine.PlaySound(SoundID.Item35 with
                                {
                                    PitchVariance = 0.2f,
                                    Pitch = 0.5f
                                }, Player.position);
                            popupMessage.Color = Color.LightGray;
                            popupMessage.Text = "Junimo mining level up!";
                            PopupText.NewText(popupMessage, Player.position);
                        }
                        junimoMiningLevel++;
                    }
                    break;
                case 22:
                    junimoMiningLevelExpNeeded = 27000;
                    if (junimoMiningExp >= junimoMiningLevelExpNeeded)
                    {
                        if (notificationOff == false)
                        {
                            if (soundOff == false)
                                SoundEngine.PlaySound(SoundID.Item35 with
                                {
                                    PitchVariance = 0.2f,
                                    Pitch = 0.5f
                                }, Player.position);
                            popupMessage.Color = Color.LightGray;
                            popupMessage.Text = "Junimo mining level up!";
                            PopupText.NewText(popupMessage, Player.position);
                        }
                        junimoMiningLevel++;
                    }
                    break;
                case 23:
                    junimoMiningLevelExpNeeded = 33000;
                    if (junimoMiningExp >= junimoMiningLevelExpNeeded)
                    {
                        if (notificationOff == false)
                        {
                            if (soundOff == false)
                                SoundEngine.PlaySound(SoundID.Item35 with
                                {
                                    PitchVariance = 0.2f,
                                    Pitch = 0.5f
                                }, Player.position);
                            popupMessage.Color = Color.LightGray;
                            popupMessage.Text = "Junimo mining level up!";
                            PopupText.NewText(popupMessage, Player.position);
                        }
                        junimoMiningLevel++;
                    }
                    break;
                case 24:
                    junimoMiningLevelExpNeeded = 40000;
                    if (junimoMiningExp >= junimoMiningLevelExpNeeded)
                    {
                        if (notificationOff == false)
                        {
                            if (soundOff == false)
                                SoundEngine.PlaySound(SoundID.Item35 with
                                {
                                    PitchVariance = 0.2f,
                                    Pitch = 0.5f
                                }, Player.position);
                            popupMessage.Color = Color.LightGray;
                            popupMessage.Text = "Junimo mining level up!";
                            PopupText.NewText(popupMessage, Player.position);
                        }
                        junimoMiningLevel++;
                    }
                    break;
                case 25:
                    junimoMiningLevelExpNeeded = 49000;
                    if (junimoMiningExp >= junimoMiningLevelExpNeeded)
                    {
                        if (notificationOff == false)
                        {
                            if (soundOff == false)
                                SoundEngine.PlaySound(SoundID.Item35 with
                                {
                                    PitchVariance = 0.2f,
                                    Pitch = 0.5f
                                }, Player.position);
                            popupMessage.Color = Color.LightGray;
                            popupMessage.Text = "Junimo mining level up!";
                            PopupText.NewText(popupMessage, Player.position);
                        }
                        junimoMiningLevel++;
                    }
                    break;
                case 26:
                    junimoMiningLevelExpNeeded = 60000;
                    if (junimoMiningExp >= junimoMiningLevelExpNeeded)
                    {
                        if (notificationOff == false)
                        {
                            if (soundOff == false)
                                SoundEngine.PlaySound(SoundID.Item35 with
                                {
                                    PitchVariance = 0.2f,
                                    Pitch = 0.5f
                                }, Player.position);
                            popupMessage.Color = Color.LightGray;
                            popupMessage.Text = "Junimo mining level maxed!";
                            PopupText.NewText(popupMessage, Player.position);
                        }
                        junimoMiningLevel++;
                    }
                    break;
                case 27:
                    break;
            }
            switch (junimoFishingLevel)
            {
                case 1:
                    junimoFishingLevelExpNeeded = 5;
                    if (junimoFishingExp >= junimoFishingLevelExpNeeded)
                    {
                        if (notificationOff == false)
                        {
                            if (soundOff == false)
                                SoundEngine.PlaySound(SoundID.Item35 with
                                {
                                    PitchVariance = 0.2f,
                                    Pitch = 0.5f
                                }, Player.position);
                            popupMessage.Color = Color.LightSkyBlue;
                            popupMessage.Text = "Junimo fishing level up!";
                            PopupText.NewText(popupMessage, Player.position);
                        }
                        junimoFishingLevel++;
                    }
                    break;
                case 2:
                    junimoFishingLevelExpNeeded = 15;
                    if (junimoFishingExp >= junimoFishingLevelExpNeeded)
                    {
                        if (notificationOff == false)
                        {
                            if (soundOff == false)
                                SoundEngine.PlaySound(SoundID.Item35 with
                                {
                                    PitchVariance = 0.2f,
                                    Pitch = 0.5f
                                }, Player.position);
                            popupMessage.Color = Color.LightSkyBlue;
                            popupMessage.Text = "Junimo fishing level up!";
                            PopupText.NewText(popupMessage, Player.position);
                        }
                        junimoFishingLevel++;
                    }
                    break;
                case 3:
                    junimoFishingLevelExpNeeded = 30;
                    if (junimoFishingExp >= junimoFishingLevelExpNeeded)
                    {
                        if (notificationOff == false)
                        {
                            if (soundOff == false)
                                SoundEngine.PlaySound(SoundID.Item35 with
                                {
                                    PitchVariance = 0.2f,
                                    Pitch = 0.5f
                                }, Player.position);
                            popupMessage.Color = Color.LightSkyBlue;
                            popupMessage.Text = "Junimo fishing level up!";
                            PopupText.NewText(popupMessage, Player.position);
                        }
                        junimoFishingLevel++;
                    }
                    break;
                case 4:
                    junimoFishingLevelExpNeeded = 50;
                    if (junimoFishingExp >= junimoFishingLevelExpNeeded)
                    {
                        if (notificationOff == false)
                        {
                            if (soundOff == false)
                                SoundEngine.PlaySound(SoundID.Item35 with
                                {
                                    PitchVariance = 0.2f,
                                    Pitch = 0.5f
                                }, Player.position);
                            popupMessage.Color = Color.LightSkyBlue;
                            popupMessage.Text = "Junimo fishing level up!";
                            PopupText.NewText(popupMessage, Player.position);
                        }
                        junimoFishingLevel++;
                    }
                    break;
                case 5:
                    junimoFishingLevelExpNeeded = 75;
                    if (junimoFishingExp >= junimoFishingLevelExpNeeded)
                    {
                        if (notificationOff == false)
                        {
                            if (soundOff == false)
                                SoundEngine.PlaySound(SoundID.Item35 with
                                {
                                    PitchVariance = 0.2f,
                                    Pitch = 0.5f
                                }, Player.position);
                            popupMessage.Color = Color.LightSkyBlue;
                            popupMessage.Text = "Junimo fishing level up!";
                            PopupText.NewText(popupMessage, Player.position);
                        }
                        junimoFishingLevel++;
                    }
                    break;
                case 6:
                    junimoFishingLevelExpNeeded = 105;
                    if (junimoFishingExp >= junimoFishingLevelExpNeeded)
                    {
                        if (notificationOff == false)
                        {
                            if (soundOff == false)
                                SoundEngine.PlaySound(SoundID.Item35 with
                                {
                                    PitchVariance = 0.2f,
                                    Pitch = 0.5f
                                }, Player.position);
                            popupMessage.Color = Color.LightSkyBlue;
                            popupMessage.Text = "Junimo fishing level up!";
                            PopupText.NewText(popupMessage, Player.position);
                        }
                        junimoFishingLevel++;
                    }
                    break;
                case 7:
                    junimoFishingLevelExpNeeded = 140;
                    if (junimoFishingExp >= junimoFishingLevelExpNeeded)
                    {
                        if (notificationOff == false)
                        {
                            if (soundOff == false)
                                SoundEngine.PlaySound(SoundID.Item35 with
                                {
                                    PitchVariance = 0.2f,
                                    Pitch = 0.5f
                                }, Player.position);
                            popupMessage.Color = Color.LightSkyBlue;
                            popupMessage.Text = "Junimo fishing level up!";
                            PopupText.NewText(popupMessage, Player.position);
                        }
                        junimoFishingLevel++;
                    }
                    break;
                case 8:
                    junimoFishingLevelExpNeeded = 190;
                    if (junimoFishingExp >= junimoFishingLevelExpNeeded)
                    {
                        if (notificationOff == false)
                        {
                            if (soundOff == false)
                                SoundEngine.PlaySound(SoundID.Item35 with
                                {
                                    PitchVariance = 0.2f,
                                    Pitch = 0.5f
                                }, Player.position);
                            popupMessage.Color = Color.LightSkyBlue;
                            popupMessage.Text = "Junimo fishing level up!";
                            PopupText.NewText(popupMessage, Player.position);
                        }
                        junimoFishingLevel++;
                    }
                    break;
                case 9:
                    junimoFishingLevelExpNeeded = 240;
                    if (junimoFishingExp >= junimoFishingLevelExpNeeded)
                    {
                        if (notificationOff == false)
                        {
                            if (soundOff == false)
                                SoundEngine.PlaySound(SoundID.Item35 with
                                {
                                    PitchVariance = 0.2f,
                                    Pitch = 0.5f
                                }, Player.position);
                            popupMessage.Color = Color.LightSkyBlue;
                            popupMessage.Text = "Junimo fishing level up!";
                            PopupText.NewText(popupMessage, Player.position);
                        }
                        junimoFishingLevel++;
                    }
                    break;
                case 10:
                    junimoFishingLevelExpNeeded = 300;
                    if (junimoFishingExp >= junimoFishingLevelExpNeeded)
                    {
                        if (notificationOff == false)
                        {
                            if (soundOff == false)
                                SoundEngine.PlaySound(SoundID.Item35 with
                                {
                                    PitchVariance = 0.2f,
                                    Pitch = 0.5f
                                }, Player.position);
                            popupMessage.Color = Color.LightSkyBlue;
                            popupMessage.Text = "Junimo fishing level up!";
                            PopupText.NewText(popupMessage, Player.position);
                        }
                        junimoFishingLevel++;
                    }
                    break;
                case 11:
                    junimoFishingLevelExpNeeded = 375;
                    if (junimoFishingExp >= junimoFishingLevelExpNeeded)
                    {
                        if (notificationOff == false)
                        {
                            if (soundOff == false)
                                SoundEngine.PlaySound(SoundID.Item35 with
                                {
                                    PitchVariance = 0.2f,
                                    Pitch = 0.5f
                                }, Player.position);
                            popupMessage.Color = Color.LightSkyBlue;
                            popupMessage.Text = "Junimo fishing level up!";
                            PopupText.NewText(popupMessage, Player.position);
                        }
                        junimoFishingLevel++;
                    }
                    break;
                case 12:
                    junimoFishingLevelExpNeeded = 460;
                    if (junimoFishingExp >= junimoFishingLevelExpNeeded)
                    {
                        if (notificationOff == false)
                        {
                            if (soundOff == false)
                                SoundEngine.PlaySound(SoundID.Item35 with
                                {
                                    PitchVariance = 0.2f,
                                    Pitch = 0.5f
                                }, Player.position);
                            popupMessage.Color = Color.LightSkyBlue;
                            popupMessage.Text = "Junimo fishing level up!";
                            PopupText.NewText(popupMessage, Player.position);
                        }
                        junimoFishingLevel++;
                    }
                    break;
                case 13:
                    junimoFishingLevelExpNeeded = 555;
                    if (junimoFishingExp >= junimoFishingLevelExpNeeded)
                    {
                        if (notificationOff == false)
                        {
                            if (soundOff == false)
                                SoundEngine.PlaySound(SoundID.Item35 with
                                {
                                    PitchVariance = 0.2f,
                                    Pitch = 0.5f
                                }, Player.position);
                            popupMessage.Color = Color.LightSkyBlue;
                            popupMessage.Text = "Junimo fishing level up!";
                            PopupText.NewText(popupMessage, Player.position);
                        }
                        junimoFishingLevel++;
                    }
                    break;
                case 14:
                    junimoFishingLevelExpNeeded = 675;
                    if (junimoFishingExp >= junimoFishingLevelExpNeeded)
                    {
                        if (notificationOff == false)
                        {
                            if (soundOff == false)
                                SoundEngine.PlaySound(SoundID.Item35 with
                                {
                                    PitchVariance = 0.2f,
                                    Pitch = 0.5f
                                }, Player.position);
                            popupMessage.Color = Color.LightSkyBlue;
                            popupMessage.Text = "Junimo fishing level up!";
                            PopupText.NewText(popupMessage, Player.position);
                        }
                        junimoFishingLevel++;
                    }
                    break;
                case 15:
                    junimoFishingLevelExpNeeded = 800;
                    if (junimoFishingExp >= junimoFishingLevelExpNeeded)
                    {
                        if (notificationOff == false)
                        {
                            if (soundOff == false)
                                SoundEngine.PlaySound(SoundID.Item35 with
                                {
                                    PitchVariance = 0.2f,
                                    Pitch = 0.5f
                                }, Player.position);
                            popupMessage.Color = Color.LightSkyBlue;
                            popupMessage.Text = "Junimo fishing level up!";
                            PopupText.NewText(popupMessage, Player.position);
                        }
                        junimoFishingLevel++;
                    }
                    break;
                case 16:
                    junimoFishingLevelExpNeeded = 950;
                    if (junimoFishingExp >= junimoFishingLevelExpNeeded)
                    {
                        if (notificationOff == false)
                        {
                            if (soundOff == false)
                                SoundEngine.PlaySound(SoundID.Item35 with
                                {
                                    PitchVariance = 0.2f,
                                    Pitch = 0.5f
                                }, Player.position);
                            popupMessage.Color = Color.LightSkyBlue;
                            popupMessage.Text = "Junimo fishing level up!";
                            PopupText.NewText(popupMessage, Player.position);
                        }
                        junimoFishingLevel++;
                    }
                    break;
                case 17:
                    junimoFishingLevelExpNeeded = 1150;
                    if (junimoFishingExp >= junimoFishingLevelExpNeeded)
                    {
                        if (notificationOff == false)
                        {
                            if (soundOff == false)
                                SoundEngine.PlaySound(SoundID.Item35 with
                                {
                                    PitchVariance = 0.2f,
                                    Pitch = 0.5f
                                }, Player.position);
                            popupMessage.Color = Color.LightSkyBlue;
                            popupMessage.Text = "Junimo fishing level up!";
                            PopupText.NewText(popupMessage, Player.position);
                        }
                        junimoFishingLevel++;
                    }
                    break;
                case 18:
                    junimoFishingLevelExpNeeded = 1400;
                    if (junimoFishingExp >= junimoFishingLevelExpNeeded)
                    {
                        if (notificationOff == false)
                        {
                            if (soundOff == false)
                                SoundEngine.PlaySound(SoundID.Item35 with
                                {
                                    PitchVariance = 0.2f,
                                    Pitch = 0.5f
                                }, Player.position);
                            popupMessage.Color = Color.LightSkyBlue;
                            popupMessage.Text = "Junimo fishing level up!";
                            PopupText.NewText(popupMessage, Player.position);
                        }
                        junimoFishingLevel++;
                    }
                    break;
                case 19:
                    junimoFishingLevelExpNeeded = 1700;
                    if (junimoFishingExp >= junimoFishingLevelExpNeeded)
                    {
                        if (notificationOff == false)
                        {
                            if (soundOff == false)
                                SoundEngine.PlaySound(SoundID.Item35 with
                                {
                                    PitchVariance = 0.2f,
                                    Pitch = 0.5f
                                }, Player.position);
                            popupMessage.Color = Color.LightSkyBlue;
                            popupMessage.Text = "Junimo fishing level up!";
                            PopupText.NewText(popupMessage, Player.position);
                        }
                        junimoFishingLevel++;
                    }
                    break;
                case 20:
                    junimoFishingLevelExpNeeded = 2100;
                    if (junimoFishingExp >= junimoFishingLevelExpNeeded)
                    {
                        if (notificationOff == false)
                        {
                            if (soundOff == false)
                                SoundEngine.PlaySound(SoundID.Item35 with
                                {
                                    PitchVariance = 0.2f,
                                    Pitch = 0.5f
                                }, Player.position);
                            popupMessage.Color = Color.LightSkyBlue;
                            popupMessage.Text = "Junimo fishing level up!";
                            PopupText.NewText(popupMessage, Player.position);
                        }
                        junimoFishingLevel++;
                    }
                    break;
                case 21:
                    junimoFishingLevelExpNeeded = 2500;
                    if (junimoFishingExp >= junimoFishingLevelExpNeeded)
                    {
                        if (notificationOff == false)
                        {
                            if (soundOff == false)
                                SoundEngine.PlaySound(SoundID.Item35 with
                                {
                                    PitchVariance = 0.2f,
                                    Pitch = 0.5f
                                }, Player.position);
                            popupMessage.Color = Color.LightSkyBlue;
                            popupMessage.Text = "Junimo fishing level up!";
                            PopupText.NewText(popupMessage, Player.position);
                        }
                        junimoFishingLevel++;
                    }
                    break;
                case 22:
                    junimoFishingLevelExpNeeded = 3000;
                    if (junimoFishingExp >= junimoFishingLevelExpNeeded)
                    {
                        if (notificationOff == false)
                        {
                            if (soundOff == false)
                                SoundEngine.PlaySound(SoundID.Item35 with
                                {
                                    PitchVariance = 0.2f,
                                    Pitch = 0.5f
                                }, Player.position);
                            popupMessage.Color = Color.LightSkyBlue;
                            popupMessage.Text = "Junimo fishing level up!";
                            PopupText.NewText(popupMessage, Player.position);
                        }
                        junimoFishingLevel++;
                    }
                    break;
                case 23:
                    junimoFishingLevelExpNeeded = 3750;
                    if (junimoFishingExp >= junimoFishingLevelExpNeeded)
                    {
                        if (notificationOff == false)
                        {
                            if (soundOff == false)
                                SoundEngine.PlaySound(SoundID.Item35 with
                                {
                                    PitchVariance = 0.2f,
                                    Pitch = 0.5f
                                }, Player.position);
                            popupMessage.Color = Color.LightSkyBlue;
                            popupMessage.Text = "Junimo fishing level up!";
                            PopupText.NewText(popupMessage, Player.position);
                        }
                        junimoFishingLevel++;
                    }
                    break;
                case 24:
                    junimoFishingLevelExpNeeded = 4750;
                    if (junimoFishingExp >= junimoFishingLevelExpNeeded)
                    {
                        if (notificationOff == false)
                        {
                            if (soundOff == false)
                                SoundEngine.PlaySound(SoundID.Item35 with
                                {
                                    PitchVariance = 0.2f,
                                    Pitch = 0.5f
                                }, Player.position);
                            popupMessage.Color = Color.LightSkyBlue;
                            popupMessage.Text = "Junimo fishing level maxed!";
                            PopupText.NewText(popupMessage, Player.position);
                        }
                        junimoFishingLevel++;
                    }
                    break;
                case 25:
                    junimoFishingLevelExpNeeded = 6000;
                    if (junimoFishingExp >= junimoFishingLevelExpNeeded)
                    {
                        if (notificationOff == false)
                        {
                            if (soundOff == false)
                                SoundEngine.PlaySound(SoundID.Item35 with
                                {
                                    PitchVariance = 0.2f,
                                    Pitch = 0.5f
                                }, Player.position);
                            popupMessage.Color = Color.LightSkyBlue;
                            popupMessage.Text = "Junimo fishing level maxed!";
                            PopupText.NewText(popupMessage, Player.position);
                        }
                        junimoFishingLevel++;
                    }
                    break;
                case 26:
                    junimoFishingLevelExpNeeded = 7750;
                    if (junimoFishingExp >= junimoFishingLevelExpNeeded)
                    {
                        if (notificationOff == false)
                        {
                            if (soundOff == false)
                                SoundEngine.PlaySound(SoundID.Item35 with
                                {
                                    PitchVariance = 0.2f,
                                    Pitch = 0.5f
                                }, Player.position);
                            popupMessage.Color = Color.LightSkyBlue;
                            popupMessage.Text = "Junimo fishing level maxed!";
                            PopupText.NewText(popupMessage, Player.position);
                        }
                        junimoFishingLevel++;
                    }
                    break;
                case 27:
                    break;
                    break;
            }
        }
        public override void SaveData(TagCompound tag)
        {
            tag.Add("AnglerCheck", anglerQuestDayCheck);
            tag.Add("harvestlvl", junimoHarvestingLevel);
            tag.Add("harvestexp", junimoHarvestingExp);
            tag.Add("harvestnextexp", junimoHarvestingLevelExpNeeded);
            tag.Add("mininglvl", junimoMiningLevel);
            tag.Add("miningexp", junimoMiningExp);
            tag.Add("miningnextexp", junimoMiningLevelExpNeeded);
            tag.Add("fishinglvl", junimoFishingLevel);
            tag.Add("fishingexp", junimoFishingExp);
            tag.Add("fishingnextexp", junimoFishingLevelExpNeeded);
            tag.Add("levelCaps", maxLvls);
        }
        public override void LoadData(TagCompound tag)
        {
            anglerQuestDayCheck = tag.GetBool("AnglerCheck");
            junimoHarvestingLevel = tag.GetInt("harvestlvl");
            junimoHarvestingExp = tag.GetInt("harvestexp");
            junimoHarvestingLevelExpNeeded = tag.GetInt("harvestnextexp");
            junimoMiningLevel = tag.GetInt("mininglvl");
            junimoMiningExp = tag.GetInt("miningexp");
            junimoMiningLevelExpNeeded = tag.GetInt("miningnextexp");
            junimoFishingLevel = tag.GetInt("fishinglvl");
            junimoFishingExp = tag.GetInt("fishingexp");
            junimoFishingLevelExpNeeded = tag.GetInt("fishingnextexp");
            maxLvls = tag.GetInt("levelCaps");
        }
    }
}
