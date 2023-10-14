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
using System;

namespace PetsOverhaul.PetEffects.Vanilla
{
    sealed public class Junimo : ModPlayer
    {
        GlobalPet Pet { get => Player.GetModPlayer<GlobalPet>(); }
        public int maxLvls = 27;
        public int maxXp = 2147480000;
        public int junimoHarvestingLevel = 1;
        public int junimoHarvestingExp = 0;
        public int[] junimoHarvestingLevelsToXp = new int[]
        {
            0,
            20,
            50,
            110,
            200,
            325,
            500,
            700,
            950,
            1275,
            1700,
            2175,
            2700,
            3300,
            4000,
            4700,
            5600,
            6700,
            8000,
            9500,
            11250,
            13500,
            16500,
            20000,
            25000,
            32500,
            42500
        };

        public int junimoMiningLevel = 1;
        public int junimoMiningExp = 0;
        public int[] junimoMiningLevelsToXp = new int[] {
            0,
            15,
            40,
            80,
            135,
            200,
            290,
            400,
            550,
            750,
            1100,
            1550,
            2200,
            2900,
            3800,
            5000,
            6500,
            8500,
            11000,
            14000,
            18000,
            22000,
            27000,
            33000,
            40000,
            49000,
            60000
        };

        public int junimoFishingLevel = 1;
        public int junimoFishingExp = 0;
        public int[] junimoFishingLevelsToXp = new int[] {
            0,
            5,
            15,
            30,
            50,
            75,
            105,
            140,
            190,
            240,
            300,
            375,
            460,
            555,
            675,
            800,
            950,
            1150,
            1400,
            1700,
            2100,
            2500,
            3000,
            3750,
            4750,
            6000,
            7750
        };

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
        public bool[] seaCreature15 = NPCID.Sets.Factory.CreateBoolSet(false, NPCID.EyeballFlyingFish,NPCID.ZombieMerman);
        public bool[] seaCreature30 = NPCID.Sets.Factory.CreateBoolSet(false, NPCID.GoblinShark,NPCID.BloodEelBody,NPCID.BloodEelTail,NPCID.BloodEelHead);
        public bool[] seaCreature50 = NPCID.Sets.Factory.CreateBoolSet(false,NPCID.BloodNautilus);
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
                            item.stack += ItemPet.Randomizer(junimoMiningLevel * junimoInUseMultiplier * item.stack);
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
                            int junimoCash = junimoHarvestingLevel * 50 * junimoInUseMultiplier * item.stack;
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
                            int junimoCash = ItemPet.Randomizer(junimoHarvestingLevel * 25 * junimoInUseMultiplier * item.stack, 10);
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
                int noSwapCd = Player.HasBuff(ModContent.BuffType<ObliviousPet>()) ? 1 : 2;
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
                    junimoFishingExp += 6 * junimoInUseMultiplier * fish.stack;
                else if (ItemID.Sets.IsFishingCrateHardmode[fish.type])
                    junimoFishingExp += 5 * junimoInUseMultiplier * fish.stack;
                else if (fish4[fish.type])
                    junimoFishingExp += 4 * junimoInUseMultiplier * fish.stack;
                else if (ItemID.Sets.IsFishingCrate[fish.type])
                    junimoFishingExp += 3 * junimoInUseMultiplier * fish.stack;
                else if (fish2[fish.type])
                    junimoFishingExp += 2 * junimoInUseMultiplier * fish.stack;
                else if (fish0[fish.type])
                {

                }
                else
                    junimoFishingExp += 1 * junimoInUseMultiplier * fish.stack;

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
            if (Main.dayTime == true && Main.time == 0)
            {
                anglerQuestDayCheck = false;
            }

            junimoInUseMultiplier = Pet.PetInUse(ItemID.JunimoPetItem) ? 2 : 1;

            junimoHarvestingLevel = Math.Clamp(junimoHarvestingLevel, 1, maxLvls);
            junimoMiningLevel = Math.Clamp(junimoMiningLevel, 1, maxLvls);
            junimoFishingLevel = Math.Clamp(junimoFishingLevel, 1, maxLvls);

            junimoHarvestingExp = Math.Clamp(junimoHarvestingExp, 0, maxXp);
            junimoMiningExp = Math.Clamp(junimoHarvestingExp, 0, maxXp);
            junimoFishingExp = Math.Clamp(junimoFishingExp, 0, maxXp);

            AdvancedPopupRequest popupMessage = new()
            {
                DurationInFrames = 300,
                Velocity = new Vector2(Main.rand.NextFloat(-3, 3), Main.rand.NextFloat(-15, -10))
            };

            bool notificationOff = ModContent.GetInstance<Personalization>().JunimoNotifOff;
            bool soundOff = ModContent.GetInstance<Personalization>().AbilitySoundDisabled;

            if (junimoHarvestingExp >= junimoHarvestingLevelsToXp[junimoHarvestingLevel] && junimoHarvestingLevel != maxLvls)
            {
                junimoHarvestingLevel++;
                if (notificationOff == false)
                {
                    if (soundOff == false)
                        SoundEngine.PlaySound(SoundID.Item35 with { PitchVariance = 0.2f, Pitch = 0.5f }, Player.position);
                    popupMessage.Color = Color.LightGreen;
                    popupMessage.Text = $"Junimo harvesting level {(junimoHarvestingLevel == maxLvls ? "maxed" : "up")}!";
                    PopupText.NewText(popupMessage, Player.position);
                }
            }

            if (junimoMiningExp >= junimoMiningLevelsToXp[junimoMiningLevel] && junimoMiningLevel != maxLvls)
            {
                junimoMiningLevel++;
                if (notificationOff == false)
                {
                    if (soundOff == false)
                        SoundEngine.PlaySound(SoundID.Item35 with
                        {
                            PitchVariance = 0.2f,
                            Pitch = 0.5f
                        }, Player.position);
                    popupMessage.Color = Color.LightGray;
                    popupMessage.Text = $"Junimo mining level {(junimoMiningLevel == maxLvls ? "maxed" : "up")}!";
                    PopupText.NewText(popupMessage, Player.position);
                }
            }

            if (junimoFishingExp >= junimoFishingLevelsToXp[junimoFishingLevel] && junimoFishingLevel != maxLvls)
            {
                junimoFishingLevel++;
                if (notificationOff == false)
                {
                    if (soundOff == false)
                        SoundEngine.PlaySound(SoundID.Item35 with
                        {
                            PitchVariance = 0.2f,
                            Pitch = 0.5f
                        }, Player.position);
                    popupMessage.Color = Color.LightSkyBlue;
                    popupMessage.Text = $"Junimo fishing level {(junimoFishingLevel == maxLvls ? "maxed" : "up")}!";
                    PopupText.NewText(popupMessage, Player.position);
                }
            }
        }
        public override void SaveData(TagCompound tag)
        {
            tag.Add("AnglerCheck", anglerQuestDayCheck);
            tag.Add("harvestlvl", junimoHarvestingLevel);
            tag.Add("harvestexp", junimoHarvestingExp);
            tag.Add("mininglvl", junimoMiningLevel);
            tag.Add("miningexp", junimoMiningExp);
            tag.Add("fishinglvl", junimoFishingLevel);
            tag.Add("fishingexp", junimoFishingExp);
        }
        public override void LoadData(TagCompound tag)
        {
            anglerQuestDayCheck = tag.GetBool("AnglerCheck");
            junimoHarvestingLevel = tag.GetInt("harvestlvl");
            junimoHarvestingExp = tag.GetInt("harvestexp");
            junimoMiningLevel = tag.GetInt("mininglvl");
            junimoMiningExp = tag.GetInt("miningexp");
            junimoFishingLevel = tag.GetInt("fishinglvl");
            junimoFishingExp = tag.GetInt("fishingexp");
        }
    }
}
