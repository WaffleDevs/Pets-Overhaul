﻿using Microsoft.Xna.Framework;
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
using Terraria.Localization;
using Terraria.GameInput;

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
        public Dictionary<int, int[]> MiningXpPerBlock = new Dictionary<int, int[]>
        {
            {1, new int[]{ ItemID.Obsidian, ItemID.SiltBlock, ItemID.SlushBlock, ItemID.DesertFossil } },
            {2, new int[]{ ItemID.CopperOre, ItemID.TinOre } },
            {3, new int[]{ ItemID.IronOre, ItemID.LeadOre, ItemID.Amethyst } },
            {4, new int[]{ ItemID.SilverOre, ItemID.TungstenOre, ItemID.Topaz, ItemID.Sapphire, ItemID.Meteorite } },
            {5, new int[]{ ItemID.GoldOre, ItemID.PlatinumOre, ItemID.Emerald, ItemID.Ruby, ItemID.Hellstone } },
            {6, new int[]{ ItemID.CrimtaneOre, ItemID.DemoniteOre, ItemID.Diamond, ItemID.Amber } },
            {8, new int[]{ ItemID.CobaltOre, ItemID.PalladiumOre } },
            {10, new int[]{ ItemID.MythrilOre, ItemID.OrichalcumOre } },
            {12, new int[]{ ItemID.AdamantiteOre, ItemID.TitaniumOre } },
            {13, new int[]{ ItemID.ChlorophyteOre, ItemID.LunarOre } }
        };

        public Dictionary<int, int[]> FishingXpPerKill = new Dictionary<int, int[]>
        {
            {15, new int[]{ NPCID.EyeballFlyingFish, NPCID.ZombieMerman } },
            {30, new int[]{ NPCID.GoblinShark, NPCID.BloodEelBody, NPCID.BloodEelTail, NPCID.BloodEelHead } },
            {50, new int[]{ NPCID.BloodNautilus } }
        };

        public Dictionary<int, int[]> FishingXpPerFish = new Dictionary<int, int[]>
        {
            {0, new int[]{ ItemID.FishingSeaweed, ItemID.OldShoe, ItemID.TinCan } },
            {2, new int[]{ ItemID.BlueJellyfish, ItemID.GreenJellyfish, ItemID.PinkJellyfish, ItemID.Obsidifish, ItemID.Prismite, ItemID.Stinkfish, ItemID.ArmoredCavefish, ItemID.Damselfish, ItemID.DoubleCod, ItemID.Ebonkoi, ItemID.FrostMinnow, ItemID.Hemopiranha, ItemID.Honeyfin, ItemID.PrincessFish, ItemID.Shrimp, ItemID.VariegatedLardfish } },
            {4, new int[]{ ItemID.ChaosFish, ItemID.FlarefinKoi } },
            {6, new int[]{ ItemID.GoldenCarp } }
        };
        public Dictionary<int, int[]> XpPerHarvestable = new Dictionary<int, int[]>
        {
            {0, new int[]{ ItemID.FishingSeaweed, ItemID.OldShoe, ItemID.TinCan } },
            {2, new int[]{ ItemID.BlueJellyfish, ItemID.GreenJellyfish, ItemID.PinkJellyfish, ItemID.Obsidifish, ItemID.Prismite, ItemID.Stinkfish, ItemID.ArmoredCavefish, ItemID.Damselfish, ItemID.DoubleCod, ItemID.Ebonkoi, ItemID.FrostMinnow, ItemID.Hemopiranha, ItemID.Honeyfin, ItemID.PrincessFish, ItemID.Shrimp, ItemID.VariegatedLardfish } },
            {4, new int[]{ ItemID.ChaosFish, ItemID.FlarefinKoi } },
            {6, new int[]{ ItemID.GoldenCarp } }
        };
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
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (junimoExpCheck() && target.active == false && target.GetGlobalNPC<NpcPet>().seaCreature)
            {
                bool hasXpValue = false;
                foreach (KeyValuePair<int, int[]> xpNum in FishingXpPerKill)
                {
                    int key = xpNum.Key;
                    int[] value = xpNum.Value;
                    if (value.Contains(target.type))
                    {
                        junimoFishingExp += key;
                        hasXpValue = true;
                    };
                }
                if (!hasXpValue) junimoMiningExp += 20;
            }

        }

        public override bool OnPickup(Item item)
        {
            Console.WriteLine(string.Format("TSIS{0} Now-TSIS{1} {2}", item.timeSinceItemSpawned, DateTime.Now, item.netID));
            if (item.TryGetGlobalItem(out ItemPet itemChck))
            {
                if (item.maxStack != 1 && Player.CanPullItem(item, Player.ItemSpace(item)) && itemChck.pickedUpBefore == false)
                {
                    if (itemChck.oreBoost) // If its an ore.
                    {
                        if (junimoExpCheck())
                        {
                            bool hasXpValue = false;
                            foreach (KeyValuePair<int, int[]> xpNum in MiningXpPerBlock)
                            {
                                int key = xpNum.Key;
                                int[] value = xpNum.Value;
                                if (value.Contains(item.type))
                                {
                                    junimoMiningExp += key * item.stack * junimoInUseMultiplier;
                                    hasXpValue = true;
                                };
                            }
                            if (!hasXpValue) junimoMiningExp += 1 * item.stack * junimoInUseMultiplier;
                        }
                        if (Player.HasItemInInventoryOrOpenVoidBag(ItemID.JunimoPetItem) || Pet.PetInUse(ItemID.JunimoPetItem))
                            item.stack += ItemPet.Randomizer(junimoMiningLevel * junimoInUseMultiplier * item.stack);
                    }

                    if (itemChck.rareHerbBoost || itemChck.herbBoost || itemChck.tree || itemChck.gemTree)
                    {
                        if (junimoExpCheck())
                        {
                            bool hasXpValue = false;
                            foreach (KeyValuePair<int, int[]> xpNum in MiningXpPerBlock)
                            {
                                int key = xpNum.Key;
                                int[] value = xpNum.Value;
                                if (value.Contains(item.type))
                                {
                                    junimoHarvestingExp += key * item.stack * junimoInUseMultiplier;
                                    hasXpValue = true;
                                };
                            }
                            if (!hasXpValue)
                            {
                                if (itemChck.rareHerbBoost) junimoMiningExp += 35 * item.stack * junimoInUseMultiplier;
                                if (itemChck.herbBoost) junimoMiningExp += 2 * item.stack * junimoInUseMultiplier;
                                if (itemChck.tree) junimoMiningExp += 1 * item.stack * junimoInUseMultiplier;
                            };
                        }
                        if (Player.HasItemInInventoryOrOpenVoidBag(ItemID.JunimoPetItem) || Pet.PetInUse(ItemID.JunimoPetItem))
                        {
                            int junimoCash = itemChck.rareHerbBoost
                                ? junimoHarvestingLevel * 50 * junimoInUseMultiplier * item.stack
                                : ItemPet.Randomizer(junimoHarvestingLevel * 25 * junimoInUseMultiplier * item.stack, 10);
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

                    if (itemChck.rareHerbBoost) // If rare herb
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
                        // If can receive xp
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

                        // Coin stuff?
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
                if (ItemID.Sets.IsFishingCrateHardmode[fish.type]) junimoFishingExp += 5 * junimoInUseMultiplier * fish.stack;
                else if (ItemID.Sets.IsFishingCrate[fish.type]) junimoFishingExp += 3 * junimoInUseMultiplier * fish.stack;

                else
                {
                    bool hasXpValue = false;
                    foreach (KeyValuePair<int, int[]> xpNum in FishingXpPerKill)
                    {
                        int key = xpNum.Key;
                        int[] value = xpNum.Value;
                        if (value.Contains(fish.type))
                        {
                            junimoMiningExp += key * junimoInUseMultiplier * fish.stack; ;
                            hasXpValue = true;
                        };
                    }
                    if (!hasXpValue) junimoMiningExp += 1 * junimoInUseMultiplier * fish.stack;
                }
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
            junimoMiningExp = Math.Clamp(junimoMiningExp, 0, maxXp);
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
            Main.NewText(String.Format("{0} harvest lvl, {1} mining lvl, {2} fishing lvl", junimoHarvestingLevel, junimoMiningLevel, junimoFishingLevel));
        }
    }

    sealed public class JunimoPetItem : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation) => entity.type == ItemID.JunimoPetItem;

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (ModContent.GetInstance<Personalization>().TooltipsEnabledWithShift && !PlayerInput.Triggers.Current.KeyStatus[TriggerNames.Down]) return;
            Junimo junimo = Main.LocalPlayer.GetModPlayer<Junimo>();
            tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.JunimoPetItem")
                        .Replace("<maxLvl>", junimo.maxLvls.ToString())
                        .Replace("<harvestingProfit>", (2.5f * junimo.junimoInUseMultiplier * junimo.junimoHarvestingLevel).ToString())
                        .Replace("<harvestingRare>", (50 * junimo.junimoInUseMultiplier * junimo.junimoHarvestingLevel).ToString())
                        .Replace("<bonusHealth>", (junimo.junimoHarvestingLevel * 0.25f * junimo.junimoInUseMultiplier).ToString())
                        .Replace("<flatHealth>", (junimo.junimoHarvestingLevel * junimo.junimoInUseMultiplier).ToString())
                        .Replace("<harvestLevel>", junimo.junimoHarvestingLevel.ToString())
                        .Replace("<harvestNext>", junimo.junimoHarvestingLevel >= junimo.maxLvls ? "Max Level!" : (junimo.junimoHarvestingLevelsToXp[junimo.junimoHarvestingLevel] - junimo.junimoHarvestingExp).ToString())
                        .Replace("<miningBonusDrop>", (junimo.junimoMiningLevel * junimo.junimoInUseMultiplier).ToString())
                        .Replace("<bonusReduction>", (junimo.junimoMiningLevel * junimo.junimoInUseMultiplier * 0.2f).ToString())
                        .Replace("<miningLevel>", junimo.junimoMiningLevel.ToString())
                        .Replace("<miningNext>", junimo.junimoMiningLevel >= junimo.maxLvls ? "Max Level!" : (junimo.junimoMiningLevelsToXp[junimo.junimoMiningLevel] - junimo.junimoMiningExp).ToString())
                        .Replace("<fishingPower>", (junimo.junimoFishingLevel * junimo.junimoInUseMultiplier * 0.5f).ToString())
                        .Replace("<bonusDamage>", (junimo.junimoFishingLevel * junimo.junimoInUseMultiplier * 0.2f).ToString())
                        .Replace("<fishingLevel>", junimo.junimoFishingLevel.ToString())
                        .Replace("<fishingNext>", junimo.junimoFishingLevel >= junimo.maxLvls ? "Max Level!" : (junimo.junimoFishingLevelsToXp[junimo.junimoFishingLevel] - junimo.junimoFishingExp).ToString())
                        ));
        }
    }
}
