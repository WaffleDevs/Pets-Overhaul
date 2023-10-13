using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using System.Collections.Generic;
using Terraria.Localization;
using PetsOverhaul.Items;
using PetsOverhaul.Config;
using Terraria.GameInput;
using PetsOverhaul.PetEffects;

namespace PetsOverhaul.Systems
{
    public class ItemTooltipChanges : GlobalItem
    {
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            Player player = Main.LocalPlayer;
            if (ModContent.GetInstance<Personalization>().TooltipsEnabledWithShift && !PlayerInput.Triggers.Current.KeyStatus[TriggerNames.Down])
            {
                tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.Config.TooltipShiftToggleInGame")));
            }
            else
            {
                if (player.TryGetModPlayer(out Turtle turtle) && item.netID == ItemID.Seaweed)
                {
                    tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.Seaweed")
                        .Replace("<def>", turtle.def.ToString())
                        .Replace("<kbResist>", (1 - turtle.kbResist).ToString())
                        .Replace("<moveSpd>", (turtle.moveSpd * 100).ToString())
                        .Replace("<dmg>", (turtle.dmgReduce * 100).ToString())
                        ));
                }
                if (player.TryGetModPlayer(out BabyDinosaur babyDinosaur) && item.netID == ItemID.AmberMosquito)
                {
                    tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.AmberMosquito")
                        .Replace("<oreChance>", (babyDinosaur.chance / 10f).ToString())
                        ));
                }
                if (player.TryGetModPlayer(out BabyEater babyEater) && item.netID == ItemID.EatersBone)
                {
                    tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.EatersBone")
                        .Replace("<moveSpd>", (babyEater.moveSpd * 100).ToString())
                        .Replace("<jumpSpd>", (babyEater.jumpSpd * 100).ToString())
                        .Replace("<fallRes>", babyEater.fallDamageTile.ToString())
                        ));
                }
                if (player.TryGetModPlayer(out BabyFaceMonster babyFaceMonster) && item.netID == ItemID.BoneRattle)
                {
                    tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.BoneRattle")
                        .Replace("<stage1Time>", ((babyFaceMonster.stage2time - babyFaceMonster.stage1time) / 60f).ToString())
                        .Replace("<stage2Time>", (babyFaceMonster.stage2time / 60f).ToString())
                        .Replace("<stage1Regen>", babyFaceMonster.stage1regen.ToString())
                        .Replace("<stage2Regen>", babyFaceMonster.stage2regen.ToString())
                        .Replace("<shieldAmount>", (babyFaceMonster.stage2ShieldMult * 100).ToString())
                        ));
                }
                if (player.TryGetModPlayer(out BabyGrinch babyGrinch) && item.netID == ItemID.BabyGrinchMischiefWhistle)
                {
                    tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.BabyGrinchMischiefWhistle")
                        .Replace("<slowAmount>", (babyGrinch.grinchSlow * 100).ToString())
                        .Replace("<slowRange>", (babyGrinch.grinchRange / 16f).ToString())
                        .Replace("<dmg>", (babyGrinch.winterDmg * 100).ToString())
                        .Replace("<crit>", babyGrinch.winterCrit.ToString())
                        ));
                }
                if (player.TryGetModPlayer(out BabyHornet babyHornet) && item.netID == ItemID.Nectar)
                {
                    tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.Nectar")
                        .Replace("<antidotePercent>", (babyHornet.healthRecovery * 100).ToString())
                        .Replace("<antidoteCd>", (babyHornet.nectarCooldown / 60f).ToString())
                        .Replace("<moveSpd>",(babyHornet.moveSpdIncr*100).ToString())
                        .Replace("<def>", babyHornet.defReduction.ToString())
                        .Replace("<dmgCrit>", (babyHornet.dmgReduction * 100).ToString())
                        .Replace("<maxMinion>", babyHornet.maxMinion.ToString())
                        .Replace("<regularChance>", babyHornet.beeChance.ToString())
                        .Replace("<summonChance>", (babyHornet.beeChance * 2).ToString())
                        .Replace("<beeDmg>", babyHornet.beeDmg.ToString())
                        .Replace("<beeKb>", babyHornet.beeKb.ToString())
                        ));
                }
                if (player.TryGetModPlayer(out BabyImp babyImp) && item.netID == ItemID.HellCake)
                {
                    tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.HellCake")
                        .Replace("<immuneTime>", (babyImp.lavaImmune / 60f).ToString())
                        .Replace("<lavaDef>", babyImp.lavaDef.ToString())
                        .Replace("<lavaSpd>", (babyImp.lavaSpd * 100).ToString())
                        .Replace("<obbyDef>", babyImp.obbyDef.ToString())
                        .Replace("<obbySpd>", (babyImp.obbySpd * 100).ToString())
                        ));
                }
                if (player.TryGetModPlayer(out BabyPenguin babyPenguin) && item.netID == ItemID.Fish)
                {
                    tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.Fish")
                        .Replace("<fp>", babyPenguin.regularFish.ToString())
                        .Replace("<oceanFp>", babyPenguin.oceanFish.ToString())
                        .Replace("<snowFp>", babyPenguin.snowFish.ToString())
                        .Replace("<chilledMult>", babyPenguin.chillingMultiplier.ToString())
                        ));
                }
                if (player.TryGetModPlayer(out BabyRedPanda babyRedPanda) && item.netID == ItemID.BambooLeaf)
                {
                    tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.BambooLeaf")
                        .Replace("<atkSpd>", (babyRedPanda.regularAtkSpd * 100).ToString())
                        .Replace("<jungleAtkSpd>", (babyRedPanda.jungleBonusSpd * 100).ToString())
                        .Replace("<aggro>", babyRedPanda.aggroReduce.ToString())
                        .Replace("<bambooChance>", babyRedPanda.bambooChance.ToString())
                        ));
                }
                if (player.TryGetModPlayer(out DungeonGuardian dungeonGuardian) && item.netID == ItemID.BoneKey)
                {
                    tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.BoneKey")
                        .Replace("<armorPen>", dungeonGuardian.armorPen.ToString())
                        .Replace("<dungArmorPen>", dungeonGuardian.dungArmorPenBonus.ToString())
                        .Replace("<dungRegen>", dungeonGuardian.lifeRegen.ToString())
                        ));
                }
                if (player.TryGetModPlayer(out BabySnowman babySnowman) && item.netID == ItemID.ToySled)
                {
                    tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.ToySled")
                        .Replace("<frostburnTime>", (babySnowman.frostburnTime / 60f).ToString())
                        .Replace("<slowAmount>", (babySnowman.snowmanSlow * 100).ToString())
                        .Replace("<slowTime>", (babySnowman.slowTime / 60f).ToString())
                        ));
                }
                if (player.TryGetModPlayer(out BabyTruffle babyTruffle) && item.netID == ItemID.StrangeGlowingMushroom)
                {
                    tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.StrangeGlowingMushroom")
                        .Replace("<buffRecover>", (babyTruffle.buffIncrease / 60f).ToString())
                        .Replace("<cooldown>", (babyTruffle.shroomPotionCd / 60f).ToString())
                        .Replace("<floatIncr>", (babyTruffle.increaseFloat * 100).ToString())
                        .Replace("<intIncr>", babyTruffle.increaseInt.ToString())
                        .Replace("<moveSpd>", (babyTruffle.moveSpd * 100).ToString())
                        ));
                }
                if (player.TryGetModPlayer(out BabyWerewolf babyWerewolf) && item.netID == ItemID.FullMoonSqueakyToy)
                {
                    tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.FullMoonSqueakyToy")
                        .Replace("<critMult>", babyWerewolf.critChance.ToString())
                        .Replace("<crDmgReduction>", (babyWerewolf.critDmgReduction * 100).ToString())
                        .Replace("<maxStacks>", babyWerewolf.maxStacks.ToString())
                        .Replace("<stackDmg>", (babyWerewolf.damageMultPerStack * 100).ToString())
                        .Replace("<stackCritDmg>", (babyWerewolf.maulCritDmgIncrease * 100).ToString())
                        ));
                }
                if (player.TryGetModPlayer(out Bernie bernie) && item.netID == ItemID.BerniePetItem)
                {
                    tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.BerniePetItem")
                        .Replace("<burnRange>", (bernie.bernieRange / 16f).ToString())
                        .Replace("<burnDrainMana>", (bernie.burnDrain * 4 * 0.05f).ToString())
                        .Replace("<burnDrainHealth>", (bernie.burnDrain * 2 * 0.05f).ToString())
                        .Replace("<maxDrain>", bernie.maxBurning.ToString())
                        ));
                }
                if (player.TryGetModPlayer(out BlackCat blackCat) && item.netID == ItemID.UnluckyYarn)
                {
                    tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.UnluckyYarn")
                        .Replace("<flatLuck>", blackCat.luckFlat.ToString())
                        .Replace("<minimumMoon>", blackCat.luckMoonLowest.ToString())
                        .Replace("<maximumMoon>", blackCat.luckMoonHighest.ToString())
                        ));
                }
                if (player.TryGetModPlayer(out BlueChicken blueChicken) && item.netID == ItemID.BlueEgg)
                {
                    tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.BlueEgg")
                        .Replace("<plantChance>", blueChicken.plantChance.ToString())
                        .Replace("<rarePlantChance>", blueChicken.rarePlantChance.ToString())
                        .Replace("<choppableChance>", blueChicken.treeChance.ToString())
                        .Replace("<eggMinute>", (blueChicken.blueEggTimer / 3600).ToString())
                        ));
                }
                if (player.TryGetModPlayer(out CavelingGardener cavelingGardener) && item.netID == ItemID.GlowTulip)
                {
                    tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.GlowTulip")
                        .Replace("<harvestChance>", cavelingGardener.cavelingRegularPlantChance.ToString())
                        .Replace("<rarePlantChance>", cavelingGardener.cavelingRarePlantChance.ToString())
                        .Replace("<gemstoneTreeChance>", cavelingGardener.cavelingGemTreeChance.ToString())
                        ));
                }
                if (player.TryGetModPlayer(out Chester chester) && item.netID == ItemID.ChesterPetItem)
                {
                    tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.ChesterPetItem")
                        .Replace("<pickupRange>", (chester.suckingUpRange / 16f).ToString())
                        .Replace("<placementRange>", chester.placementRange.ToString())
                        .Replace("<chestDef>", chester.chestOpenDef.ToString())
                        ));
                }
                if (player.TryGetModPlayer(out CompanionCube companionCube) && item.netID == ItemID.CompanionCube)
                {
                    tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.CompanionCube")
                        .Replace("<manaToHealth>", (companionCube.manaToHealth * 100).ToString())
                        .Replace("<healthToMana>", (companionCube.healthToMana * 100).ToString())
                        .Replace("<manaPotionNerf>", (companionCube.manaPotionNerf * 100).ToString())
                        .Replace("<manaToHealthNerf>", (companionCube.manaToHealthNerf * 100).ToString())
                        .Replace("<halfOfSickness>", (Player.manaSickLessDmg * 100).ToString())
                        ));
                }
                if (player.TryGetModPlayer(out CursedSapling cursedSapling) && item.netID == ItemID.CursedSapling)
                {
                    tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.CursedSapling")
                        .Replace("<minionSlot>", cursedSapling.maxMinion.ToString())
                        .Replace("<dmg>", (cursedSapling.pumpkinWeaponDmg * 100).ToString())
                        .Replace("<ravenDmg>", (cursedSapling.ravenDmg * 100).ToString())
                        .Replace("<whipRange>", (cursedSapling.whipRange * 100).ToString())
                        .Replace("<whipSpeed>", (cursedSapling.whipSpeed * 100).ToString())
                        ));
                }
                if (player.TryGetModPlayer(out DirtiestBlock dirtiestBlock) && item.netID == ItemID.DirtiestBlock)
                {
                    tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.DirtiestBlock")
                        .Replace("<any>", dirtiestBlock.everythingCoin.ToString())
                        .Replace("<soil>", dirtiestBlock.soilCoin.ToString())
                        .Replace("<dirt>", dirtiestBlock.dirtCoin.ToString())
                        ));
                }
                if (player.TryGetModPlayer(out DynamiteKitten dynamiteKitten) && item.netID == ItemID.BallOfFuseWire)
                {
                    tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.BallOfFuseWire")
                        .Replace("<kb>", dynamiteKitten.kbMult.ToString())
                        .Replace("<dmg>", dynamiteKitten.damageMult.ToString())
                        .Replace("<armorPen>", dynamiteKitten.armorPen.ToString())
                        ));
                }
                if (player.TryGetModPlayer(out Estee estee) && item.netID == ItemID.CelestialWand)
                {
                    tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.CelestialWand")
                        .Replace("<maxMana>", (estee.manaIncrease * 100).ToString())
                        .Replace("<dmgPenalty>", estee.penaltyMult.ToString())
                        .Replace("<manaToDmg>", (estee.manaMagicIncreasePer1 * 100).ToString())
                        ));
                }
                if (player.TryGetModPlayer(out EyeballSpring eyeballSpring) && item.netID == ItemID.EyeSpring)
                {
                    tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.EyeSpring")
                        .Replace("<jumpBoost>", (eyeballSpring.jumpBoost * 100).ToString())
                        .Replace("<acceleration>", (eyeballSpring.acceleration * 100).ToString())
                        .Replace("<ascNerf>", eyeballSpring.ascentPenaltyMult.ToString())
                        ));
                }
                if (player.TryGetModPlayer(out FennecFox fennecFox) && item.netID == ItemID.ExoticEasternChewToy)
                {
                    tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.ExoticEasternChewToy")
                        .Replace("<meleeSpd>", (fennecFox.meleeSpdIncrease * 100).ToString())
                        .Replace("<moveSpd>", (fennecFox.speedIncrease * 100).ToString())
                        .Replace("<sizeNerf>", fennecFox.sizeDecrease.ToString())
                        .Replace("<dmg>", (fennecFox.meleeDmg * 100).ToString())
                        ));
                }
                if (player.TryGetModPlayer(out GlitteryButterfly glitteryButterfly) && item.netID == ItemID.BedazzledNectar)
                {
                    tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.BedazzledNectar")
                        .Replace("<flight>", (glitteryButterfly.wingTime / 60f).ToString())
                        .Replace("<bonusFlight>", (glitteryButterfly.bonusTimeIfExisting / 60f).ToString())
                        .Replace("<healthNerf>", (glitteryButterfly.healthPenalty * 100).ToString())
                        ));
                }
                if (player.TryGetModPlayer(out Glommer glommer) && item.netID == ItemID.GlommerPetItem)
                {
                    tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.GlommerPetItem")
                        .Replace("<sanityAmount>", (glommer.glommerSanityAura * 100).ToString())
                        .Replace("<sanityRange>", (glommer.glommerSanityRange / 16f).ToString())
                        .Replace("<manaRecover>", glommer.glommerSanityRecover.ToString())
                        .Replace("<manaRecoverCd>", (glommer.glommerSanityTime / 60f).ToString())
                        ));
                }
                if (player.TryGetModPlayer(out Hoardagron hoardagron) && item.netID == ItemID.DD2PetDragon)
                {
                    tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.DD2PetDragon")
                        .Replace("<arrowVelo>", hoardagron.arrowSpd.ToString())
                        .Replace("<arrowPierce>", hoardagron.arrowPen.ToString())
                        .Replace("<bulletVelo>", hoardagron.bulletSpd.ToString())
                        .Replace("<treshold>", (hoardagron.specialTreshold * 100).ToString())
                        .Replace("<bossTreshold>", (hoardagron.specialBossTreshold * 100).ToString())
                        ));
                }
                if (player.TryGetModPlayer(out Junimo junimo) && item.netID == ItemID.JunimoPetItem)
                {
                    tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.JunimoPetItem")
                        .Replace("<maxLvl>", junimo.maxLvls.ToString())
                        .Replace("<harvestingProfit>", (2.5f * junimo.junimoInUseMultiplier * junimo.junimoHarvestingLevel).ToString())
                        .Replace("<harvestingRare>", (50 * junimo.junimoInUseMultiplier * junimo.junimoHarvestingLevel).ToString())
                        .Replace("<bonusHealth>", (junimo.junimoHarvestingLevel * 0.25f * junimo.junimoInUseMultiplier).ToString())
                        .Replace("<flatHealth>", (junimo.junimoHarvestingLevel * junimo.junimoInUseMultiplier).ToString())
                        .Replace("<harvestLevel>", junimo.junimoHarvestingLevel.ToString())
                        .Replace("<harvestNext>", junimo.junimoHarvestingLevel >= junimo.maxLvls ? "Max Level!" : (junimo.junimoHarvestingLevelExpNeeded-junimo.junimoHarvestingExp).ToString())
                        .Replace("<miningBonusDrop>", (junimo.junimoMiningLevel * junimo.junimoInUseMultiplier).ToString())
                        .Replace("<bonusReduction>", (junimo.junimoMiningLevel * junimo.junimoInUseMultiplier * 0.2f).ToString())
                        .Replace("<miningLevel>", junimo.junimoMiningLevel.ToString())
                        .Replace("<miningNext>", junimo.junimoMiningLevel >= junimo.maxLvls ? "Max Level!" : (junimo.junimoMiningLevelExpNeeded-junimo.junimoMiningExp).ToString())
                        .Replace("<fishingPower>", (junimo.junimoFishingLevel * junimo.junimoInUseMultiplier * 0.5f).ToString())
                        .Replace("<bonusDamage>", (junimo.junimoFishingLevel * junimo.junimoInUseMultiplier * 0.2f).ToString())
                        .Replace("<fishingLevel>", junimo.junimoFishingLevel.ToString())
                        .Replace("<fishingNext>", junimo.junimoFishingLevel >= junimo.maxLvls ? "Max Level!" : (junimo.junimoFishingLevelExpNeeded-junimo.junimoFishingExp).ToString())
                        ));
                }
                if (player.TryGetModPlayer(out LilHarpy lilHarpy) && item.netID == ItemID.BirdieRattle)
                {
                    tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.BirdieRattle")
                        .Replace("<flightTime>", (lilHarpy.fuelMax / 60f).ToString())
                        .Replace("<cooldown>", (lilHarpy.harpyCd / 60f).ToString())
                        ));
                }
                if (player.TryGetModPlayer(out Lizard lizard) && item.netID == ItemID.LizardEgg)
                {
                    tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.LizardEgg")
                        .Replace("<transformTime>", (lizard.transformTime / 60f).ToString())
                        .Replace("<hitCount>", lizard.maxSteroidCount.ToString())
                        .Replace("<hitDmg>", lizard.dmgMultIncrease.ToString())
                        .Replace("<hitFlat>", lizard.dmgFlatIncrease.ToString())
                        .Replace("<lifesteal>", (lizard.lizardLifesteal * 100).ToString())
                        .Replace("<maxHpRecovery>", (lizard.lizardLifestealHealth * 100).ToString())
                        .Replace("<transformCooldown>", (lizard.transformCd / 60f).ToString())
                        ));
                }
                if (player.TryGetModPlayer(out MiniMinotaur miniMinotaur) && item.netID == ItemID.TartarSauce)
                {
                    tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.TartarSauce")
                        .Replace("<cooldown>", (miniMinotaur.minotaurCd / 60f).ToString())
                        .Replace("<maxStack>", miniMinotaur.maxStack.ToString())
                        .Replace("<maxDef>", (miniMinotaur.defMult * 100 * miniMinotaur.maxStack).ToString())
                        .Replace("<maxMeleeSpd>", (miniMinotaur.meleeSpd * 100 * miniMinotaur.maxStack).ToString())
                        .Replace("<maxDmg>", (miniMinotaur.meleeDmg * 100 * miniMinotaur.maxStack).ToString())
                        .Replace("<maxSpd>", (miniMinotaur.moveSpd * 100 * miniMinotaur.maxStack).ToString())
                        .Replace("<meleeSpd>", (miniMinotaur.meleeSpd * 100).ToString())
                        .Replace("<moveSpd>", (miniMinotaur.moveSpd * 100).ToString())
                        .Replace("<dmg>", (miniMinotaur.meleeDmg * 100).ToString())
                        .Replace("<def>", (miniMinotaur.defMult * 100).ToString())
                        ));
                }
                if (player.TryGetModPlayer(out Parrot parrot) && item.netID == ItemID.ParrotCracker)
                {
                    tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.ParrotCracker")
                        .Replace("<chance>", parrot.chance.ToString())
                        ));
                }
                if (player.TryGetModPlayer(out Pigman pigman) && item.netID == ItemID.PigPetItem)
                {
                    tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.PigPetItem")
                        .Replace("<foodChance>", pigman.foodChance.ToString())
                        .Replace("<potionChance>", pigman.potionChance.ToString())
                        .Replace("<shield1>", pigman.tier1Shield.ToString())
                        .Replace("<shield2>", pigman.tier2Shield.ToString())
                        .Replace("<shield3>", pigman.tier3Shield.ToString())
                        .Replace("<shieldTime>", (pigman.shieldTime / 60f).ToString())
                        .Replace("<cooldown>", (pigman.shieldCooldown / 60f).ToString())
                        ));
                }
                if (player.TryGetModPlayer(out Plantero plantero) && item.netID == ItemID.MudBud)
                {
                    tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.MudBud")
                        .Replace("<chance>", plantero.spawnChance.ToString())
                        .Replace("<dmg>", plantero.damageMult.ToString())
                        .Replace("<flatDmg>", plantero.flatDmg.ToString())
                        .Replace("<kb>", plantero.knockBack.ToString())
                        ));
                }
                if (player.TryGetModPlayer(out PropellerGato propellerGato) && item.netID == ItemID.DD2PetGato)
                {
                    tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.DD2PetGato")
                        .Replace("<crit>", propellerGato.bonusCritChance.ToString())
                        .Replace("<maxSentry>", propellerGato.turretIncrease.ToString())
                        ));
                }
                if (player.TryGetModPlayer(out Puppy puppy) && item.netID == ItemID.DogWhistle)
                {
                    tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.DogWhistle")
                        .Replace("<critter>", puppy.catchChance.ToString())
                        .Replace("<rareCritter>", puppy.rareCatchChance.ToString())
                        .Replace("<rareCritterCoin>", (puppy.rareCritterCoin / 100f).ToString())
                        .Replace("<rareEnemyCoin>", (puppy.rareEnemyCoin / 100f).ToString())
                        ));
                }
                if (player.TryGetModPlayer(out Sapling sapling) && item.netID == ItemID.Seedling)
                {
                    tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.Seedling")
                        .Replace("<lifesteal>", (sapling.regularLifesteal * 100).ToString())
                        .Replace("<planteraSteal>", (sapling.planteraLifesteal * 100).ToString())
                        ));
                }
                if (player.TryGetModPlayer(out Spider spider) && item.netID == ItemID.SpiderEgg)
                {
                    tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.SpiderEgg")
                        .Replace("<poisonTime>", (spider.poisonTime / 60f).ToString())
                        .Replace("<poiPerc>", spider.poisonDmgMult.ToString())
                        .Replace("<poiFlat>", spider.poisonFlatDmg.ToString())
                        .Replace("<poiKb>", spider.kbIncreasePoison.ToString())
                        .Replace("<poiCrit>", spider.poisonCrit.ToString())
                        .Replace("<venomPerc>", spider.venomDmgMult.ToString())
                        .Replace("<venomFlat>", spider.venomFlatDmg.ToString())
                        .Replace("<venomKb>", spider.kbIncreaseVenom.ToString())
                        .Replace("<venomCrit>", spider.venomCrit.ToString())
                        ));
                }
                if (player.TryGetModPlayer(out ShadowMimic shadowMimic) && item.netID == ItemID.OrnateShadowKey)
                {
                    tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.OrnateShadowKey")
                        .Replace("<npcCoin>", shadowMimic.npcCoin.ToString())
                        .Replace("<npcItem>", shadowMimic.npcItem.ToString())
                        .Replace("<bossCoin>", shadowMimic.bossCoin.ToString())
                        .Replace("<bossItem>", shadowMimic.bossItem.ToString())
                        .Replace("<bagCoin>", shadowMimic.bagCoin.ToString())
                        .Replace("<bagItem>", shadowMimic.bagItem.ToString())
                        ));
                }
                if (player.TryGetModPlayer(out SharkPup sharkPup) && item.netID == ItemID.SharkBait)
                {
                    tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.SharkBait")
                        .Replace("<fishingPower>", sharkPup.fishingPow.ToString())
                        .Replace("<seaCreatureDmg>", sharkPup.seaCreatureDamage.ToString())
                        .Replace("<seaCreatureResist>", sharkPup.seaCreatureResist.ToString())
                        .Replace("<shield>", sharkPup.shieldOnCatch.ToString())
                        .Replace("<shieldTime>", (sharkPup.shieldTime / 60f).ToString())
                        ));
                }
                if (player.TryGetModPlayer(out Spiffo spiffo) && item.netID == ItemID.SpiffoPlush)
                {
                    tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.SpiffoPlush")
                        .Replace("<ammoReserve>", spiffo.ammoReserveChance.ToString())
                        .Replace("<armorPen>", spiffo.zombieArmorPen.ToString())
                        .Replace("<penChance>", spiffo.penetrateChance.ToString())
                        ));
                }
                if (player.TryGetModPlayer(out Squashling squashling) && item.netID == ItemID.MagicalPumpkinSeed)
                {
                    tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.MagicalPumpkinSeed")
                        .Replace("<plant>", squashling.squashlingCommonChance.ToString())
                        .Replace("<rarePlant>", squashling.squashlingRareChance.ToString())
                        ));
                }
                if (player.TryGetModPlayer(out SugarGlider sugarGlider) && item.netID == ItemID.EucaluptusSap)
                {
                    tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.EucaluptusSap")
                        .Replace("<speed>", sugarGlider.speedMult.ToString())
                        .Replace("<acceleration>", sugarGlider.accMult.ToString())
                        .Replace("<flatIncrease>", (sugarGlider.accSpeedRaise * 100).ToString())
                        ));
                }
                if (player.TryGetModPlayer(out TikiSpirit tikiSpirit) && item.netID == ItemID.TikiTotem)
                {
                    tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.TikiTotem")
                        .Replace("<atkSpdToDmg>", (tikiSpirit.atkSpdToDmgConversion * 100).ToString())
                        .Replace("<atkSpdToRange>", (tikiSpirit.atkSpdToRangeConversion * 100).ToString())
                        .Replace("<nonWhipCrit>", tikiSpirit.nonWhipCrit.ToString())
                        .Replace("<whipCrit>", tikiSpirit.whipCritBonus.ToString())
                        ));
                }
                if (player.TryGetModPlayer(out VoltBunny voltBunny) && item.netID == ItemID.LightningCarrot)
                {
                    tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.LightningCarrot")
                        .Replace("<flatSpd>", (voltBunny.movespdFlat * 100).ToString())
                        .Replace("<multSpd>", voltBunny.movespdMult.ToString())
                        .Replace("<spdToDmg>", (voltBunny.movespdToDmg * 100).ToString())
                        .Replace("<electricRod>", (voltBunny.lightningRod * 100).ToString())
                        .Replace("<electricRodDuration>", (voltBunny.lightningRodMax / 60f).ToString())
                        ));
                }
                if (player.TryGetModPlayer(out ZephyrFish zephyrFish) && item.netID == ItemID.ZephyrFish)
                {
                    tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.ZephyrFish")
                        .Replace("<windFish>", (zephyrFish.speedMult / 8f).ToString())
                        .Replace("<regularChance>", zephyrFish.baseChance.ToString())
                        .Replace("<windChance>", zephyrFish.windChance.ToString())
                        .Replace("<anglerPower>", (zephyrFish.powerPerQuest * 100).ToString())
                        .Replace("<maxAnglerPower>", (zephyrFish.maxQuestPower * 100).ToString())
                        .Replace("<anglerQuests>", player.anglerQuestsFinished.ToString())
                        .Replace("<currentAnglerPower>", (zephyrFish.powerPerQuest * player.anglerQuestsFinished * 100).ToString())
                        ));
                }
                if (player.TryGetModPlayer(out SuspiciousEye suspiciousEye) && item.netID == ItemID.EyeOfCthulhuPetItem)
                {
                    tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.EyeOfCthulhuPetItem")
                        .Replace("<defToDmg>", (suspiciousEye.dmgMult * 100).ToString())
                        .Replace("<defToSpd>", (suspiciousEye.spdMult * 100).ToString())
                        .Replace("<defToCrit>", (suspiciousEye.critMult * 100).ToString())
                        .Replace("<enrageLength>", (suspiciousEye.phaseTime / 60f).ToString())
                        .Replace("<enrageCd>", (suspiciousEye.phaseCd / 360f).ToString())
                        ));
                }
                if (player.TryGetModPlayer(out SpiderBrain spiderBrain) && item.netID == ItemID.BrainOfCthulhuPetItem)
                {
                    tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.BrainOfCthulhuPetItem")
                        .Replace("<lifesteal>", (spiderBrain.lifestealAmount * 100).ToString())
                        .Replace("<maxPool>", (spiderBrain.lifePoolMaxPerc * 100).ToString())
                        .Replace("<healthRecovery>", (spiderBrain.cdDoAddToPool / 60f).ToString())
                        ));
                }
                if (player.TryGetModPlayer(out EaterOfWorms eaterOfWorms) && item.netID == ItemID.EaterOfWorldsPetItem)
                {
                    tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.EaterOfWorldsPetItem")
                        .Replace("<miningSpeed>", (eaterOfWorms.nonOreSpeed*100).ToString())
                        .Replace("<multipleBreakChance>", eaterOfWorms.tileBreakSpreadChance.ToString())
                        .Replace("<width>", eaterOfWorms.tileBreakXSpread.ToString())
                        .Replace("<length>", eaterOfWorms.tileBreakYSpread.ToString())
                        ));
                }
                if (player.TryGetModPlayer(out SlimePrince slimePrince) && item.netID == ItemID.KingSlimePetItem)
                {
                    tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.KingSlimePetItem")
                        .Replace("<burnHp>", (slimePrince.healthDmg * 100).ToString())
                        .Replace("<burnCap>", slimePrince.burnCap.ToString())
                        .Replace("<extraKb>", slimePrince.bonusKb.ToString())
                        .Replace("<jumpSpd>", (slimePrince.slimyJump * 100).ToString())
                        .Replace("<kbBoost>", slimePrince.slimyKb.ToString())
                        .Replace("<enemyDmgRecieve>", slimePrince.wetRecievedHigher.ToString())
                        .Replace("<enemyDmgDeal>", slimePrince.wetDealtLower.ToString())
                        .Replace("<dmg>", (slimePrince.wetDmg * 100).ToString())
                        .Replace("<def>", (slimePrince.wetDef * 100).ToString())
                        .Replace("<moveSpd>", (slimePrince.wetSpeed * 100).ToString())
                        ));
                }
                if (player.TryGetModPlayer(out HoneyBee honeyBee) && item.netID == ItemID.QueenBeePetItem)
                {
                    tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.QueenBeePetItem")
                        .Replace("<extraHeal>", (honeyBee.selfPotionIncrease * 100).ToString())
                        .Replace("<range>", (honeyBee.range / 16f).ToString())
                        .Replace("<bottledHealth>", (honeyBee.bottledHealth * 100).ToString())
                        .Replace("<honeyfinHealth>", (honeyBee.honeyfinHealth * 100).ToString())
                        .Replace("<bottledHoneyTime>", (honeyBee.bottledHoneyBuff / 60f).ToString())
                        .Replace("<honeyfinHoneyTime>", (honeyBee.honeyfinHoneyBuff / 60f).ToString())
                        .Replace("<abilityHaste>", (honeyBee.abilityHaste * 100).ToString())
                        ));
                }
                if (player.TryGetModPlayer(out TinyDeerclops tinyDeerclops) && item.netID == ItemID.DeerclopsPetItem)
                {
                    tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.DeerclopsPetItem")
                        .Replace("<treshold>", (tinyDeerclops.healthTreshold * 100).ToString())
                        .Replace("<tresholdTime>", (tinyDeerclops.damageStoreTime / 60f).ToString())
                        .Replace("<immunityTime>", (tinyDeerclops.immuneTime / 60f).ToString())
                        .Replace("<slowAmount>", (tinyDeerclops.slow * 100).ToString())
                        .Replace("<range>", (tinyDeerclops.range / 16f).ToString())
                        .Replace("<debuffTime>", (tinyDeerclops.applyTime / 60f).ToString())
                        .Replace("<cooldown>", (tinyDeerclops.cooldown / 60f).ToString())
                        ));
                }
                if (player.TryGetModPlayer(out SkeletronJr skeletronJr) && item.netID == ItemID.SkeletronPetItem)
                {
                    tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.SkeletronPetItem")
                        .Replace("<recievedMult>", (skeletronJr.playerTakenMult * 100).ToString())
                        .Replace("<recievedHowLong>", skeletronJr.playerDamageTakenSpeed.ToString())
                        .Replace("<dealtMult>", (skeletronJr.enemyDamageIncrease * 100).ToString())
                        ));
                }
                if (player.TryGetModPlayer(out SlimePrincess slimePrincess) && item.netID == ItemID.QueenSlimePetItem)
                {
                    tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.QueenSlimePetItem")
                        .Replace("<slow>", (slimePrincess.slow * 100).ToString())
                        .Replace("<haste>", (slimePrincess.haste * 100).ToString())
                        .Replace("<dmg>", slimePrincess.dmgBoost.ToString())
                        .Replace("<shield>", slimePrincess.shield.ToString())
                        .Replace("<shieldTime>", (slimePrincess.shieldTime / 60f).ToString())
                        ));
                }
                if (player.TryGetModPlayer(out MiniPrime miniPrime) && item.netID == ItemID.SkeletronPrimePetItem)
                {
                    tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.SkeletronPrimePetItem")
                        .Replace("<shieldMaxHealthAmount>", (miniPrime.shieldMult * 100).ToString())
                        .Replace("<shieldCooldown>", (miniPrime.shieldRecovery / 300f).ToString())
                        .Replace("<dmg>", (miniPrime.dmgIncrease * 100).ToString())
                        .Replace("<crit>", miniPrime.critIncrease.ToString())
                        .Replace("<def>", miniPrime.defIncrease.ToString())
                        .Replace("<shieldLifetime>", (miniPrime.shieldTime / 60f).ToString())
                        ));
                }
                if (player.TryGetModPlayer(out Destroyer destroyer) && item.netID == ItemID.DestroyerPetItem)
                {
                    tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.DestroyerPetItem")
                        .Replace("<defMultChance>", (destroyer.defItemMult * 100).ToString())
                        .Replace("<flatAmount>", destroyer.flatAmount.ToString())
                        .Replace("<defMultIncrease>", destroyer.flatDefMult.ToString())
                        .Replace("<ironskinDef>", destroyer.ironskinBonusDef.ToString())
                        ));
                }
                if (player.TryGetModPlayer(out TheTwins theTwins) && item.netID == ItemID.TwinsPetItem)
                {
                    tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.TwinsPetItem")
                        .Replace("<closeRange>", (theTwins.closeRange / 16f).ToString())
                        .Replace("<cursedTime>", (theTwins.infernoTime / 60f).ToString())
                        .Replace("<defLifesteal>", theTwins.defMult.ToString())
                        .Replace("<dealtDmgLifesteal>", (theTwins.defLifestealDmgMult * 100).ToString())
                        .Replace("<longRange>", (theTwins.longRange / 16f).ToString())
                        .Replace("<hpDmg>", (theTwins.regularEnemyHpDmg * 100).ToString())
                        .Replace("<bossHpDmg>", (theTwins.bossHpDmg * 100).ToString())
                        .Replace("<hpDmgCooldown>", (theTwins.healthDmgCd / 60f).ToString())
                        ));
                }
                if (player.TryGetModPlayer(out EverscreamSapling everscreamSapling) && item.netID == ItemID.EverscreamPetItem)
                {
                    tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.EverscreamPetItem")
                        .Replace("<magicCritNerf>", everscreamSapling.critMult.ToString())
                        .Replace("<maxMana>", everscreamSapling.manaIncrease.ToString())
                        .Replace("<missingMana>", (everscreamSapling.missingManaPercent * 100).ToString())
                        .Replace("<flatMana>", everscreamSapling.flatRecovery.ToString())
                        .Replace("<manaRecoveryCd>", (everscreamSapling.cooldown / 60f).ToString())
                        .Replace("<dmg>", (everscreamSapling.dmgIncr * 100).ToString())
                        .Replace("<crit>", everscreamSapling.howMuchCrit.ToString())
                        ));
                }
                if (player.TryGetModPlayer(out AlienSkater alienSkater) && item.netID == ItemID.MartianPetItem)
                {
                    tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.MartianPetItem")
                        .Replace("<wingMult>", alienSkater.wingTime.ToString())
                        .Replace("<acc>", (alienSkater.accelerator * 100).ToString())
                        .Replace("<speedMult>", alienSkater.speedMult.ToString())
                        .Replace("<accMult>", alienSkater.accMult.ToString())
                        .Replace("<flatSpdAcc>", (alienSkater.speedAccIncr * 100).ToString())
                        ));
                }
                if (player.TryGetModPlayer(out BabyOgre babyOgre) && item.netID == ItemID.DD2OgrePetItem)
                {
                    tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.DD2OgrePetItem")
                        .Replace("<moveSpdNerf>", babyOgre.movespdNerf.ToString())
                        .Replace("<atkSpdNerf>", babyOgre.atkSpdMult.ToString())
                        .Replace("<dmgNerf>", (babyOgre.nonMeleedmg * 100).ToString())
                        .Replace("<horizontalNerf>", babyOgre.horizontalMult.ToString())
                        .Replace("<verticalNerf>", babyOgre.verticalMult.ToString())
                        .Replace("<trueMeleeMults>", babyOgre.trueMeleeMultipliers.ToString())
                        .Replace("<trueMeleeCrit>", babyOgre.crit.ToString())
                        .Replace("<healthIncrease>", (babyOgre.healthIncrease * 100).ToString())
                        .Replace("<defMult>", babyOgre.defMult.ToString())
                        .Replace("<damageReduction>", (babyOgre.dr * 100).ToString())
                        ));
                }
                if (player.TryGetModPlayer(out TinyFishron tinyFishron) && item.netID == ItemID.DukeFishronPetItem)
                {
                    tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.DukeFishronPetItem")
                        .Replace("<baseMult>", tinyFishron.fishingPowerPenalty.ToString())
                        .Replace("<anglerFishingPower>", tinyFishron.fpPerQuest.ToString())
                        .Replace("<flatChance>", tinyFishron.stackChance.ToString())
                        .Replace("<fishingPowerChance>", (tinyFishron.multiplier * 100).ToString())
                        .Replace("<bobberChance>", tinyFishron.bobberChance.ToString())
                        .Replace("<anglerQuests>", player.anglerQuestsFinished.ToString())
                        .Replace("<currentAnglerWithBaseMult>", (player.anglerQuestsFinished * tinyFishron.fpPerQuest+tinyFishron.fishingPowerPenalty).ToString())
                        ));
                }
                if (player.TryGetModPlayer(out PhantasmalDragon phantasmalDragon) && item.netID == ItemID.LunaticCultistPetItem)
                {
                    tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.LunaticCultistPetItem")
                        .Replace("<cooldown>", (phantasmalDragon.phantasmDragonCooldown / 60f).ToString())
                        .Replace("<icePierce>", phantasmalDragon.icePierce.ToString())
                        .Replace("<icePercent>", (phantasmalDragon.iceMult * 100).ToString())
                        .Replace("<iceFlat>", phantasmalDragon.iceFlat.ToString())
                        .Replace("<lightPercent>", (phantasmalDragon.lightMult * 100).ToString())
                        .Replace("<lightFlat>", phantasmalDragon.lightFlat.ToString())
                        .Replace("<lightPierce>", phantasmalDragon.lightPierce.ToString())
                        .Replace("<firePercent>", (phantasmalDragon.fireMult * 100).ToString())
                        .Replace("<fireFlat>", phantasmalDragon.fireFlat.ToString())
                        ));
                }
                if (player.TryGetModPlayer(out ItsyBetsy itsyBetsy) && item.netID == ItemID.DD2BetsyPetItem)
                {
                    tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.DD2BetsyPetItem")
                        .Replace("<debuffTime>", (itsyBetsy.debuffTime / 60f).ToString())
                        .Replace("<defDecrease>", (itsyBetsy.defReduction * 100).ToString())
                        .Replace("<maxStack>", itsyBetsy.maxStacks.ToString())
                        .Replace("<missingHpSteal>", (itsyBetsy.missingHpRecover * 100).ToString())
                        ));
                }
                if (player.TryGetModPlayer(out IceQueen iceQueen) && item.netID == ItemID.IceQueenPetItem)
                {
                    tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.IceQueenPetItem")
                        .Replace("<frozenTombTime>", (iceQueen.tombTime / 60f).ToString())
                        .Replace("<range>", (iceQueen.queenRange / 16f).ToString())
                        .Replace("<slowAmount>", (iceQueen.slowAmount * 100).ToString())
                        .Replace("<healthRecovery>", (iceQueen.tombTime / 3).ToString())
                        .Replace("<baseDmg>", iceQueen.freezeDamage.ToString())
                        .Replace("<postTombImmunity>", (iceQueen.immuneTime / 60f).ToString())
                        .Replace("<tombCooldown>", (iceQueen.cooldown / 3600f).ToString())
                        ));
                }
                if (player.TryGetModPlayer(out PlanteraSeedling planteraSeedling) && item.netID == ItemID.PlanteraPetItem)
                {
                    tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.PlanteraPetItem")
                        .Replace("<maxAmount>", (planteraSeedling.secondMultiplier * 100 + 100).ToString())
                        ));
                }
                if (player.TryGetModPlayer(out Moonling moonling) && item.netID == ItemID.MoonLordPetItem)
                {
                    tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.MoonLordPetItem")
                        .Replace("<sumRange>", (moonling.sumWhipRng * 100).ToString())
                        .Replace("<sumSpd>", (moonling.sumWhipSpd * 100).ToString())
                        .Replace("<sumDmg>", (moonling.sumDmg * 100).ToString())
                        .Replace("<sumMax>", moonling.sumMinion.ToString())
                        .Replace("<mana>", moonling.magicMana.ToString())
                        .Replace("<manaCost>", (moonling.magicManaCost * 100).ToString())
                        .Replace("<magicCrit>", moonling.magicCrit.ToString())
                        .Replace("<magicDmg>", (moonling.magicDmg * 100).ToString())
                        .Replace("<armorPen>", moonling.rangedPen.ToString())
                        .Replace("<rangedCrit>", moonling.rangedCr.ToString())
                        .Replace("<rangedDmg>", (moonling.rangedDmg * 100).ToString())
                        .Replace("<dr>", (moonling.meleeDr * 100).ToString())
                        .Replace("<meleeSpd>", (moonling.meleeSpd * 100).ToString())
                        .Replace("<meleeDmg>", (moonling.meleeDmg * 100).ToString())
                        .Replace("<def>", moonling.defense.ToString())
                        ));
                }
                if (player.TryGetModPlayer(out DualSlime dualSlime) && item.netID == ItemID.ResplendentDessert)
                {
                    tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.ResplendentDessert")
                        .Replace("<approxWeak>", "10")
                        ));
                }
                if (player.TryGetModPlayer(out CarrotBunny carrotBunny) && item.netID == ItemID.Carrot)
                {
                    tooltips.Add(new(Mod, "Tooltip0", Language.GetTextValue("Mods.PetsOverhaul.PetItemTooltips.Carrot")
                        .Replace("<moveSpeed>", (carrotBunny.spdPerStk * 100).ToString())
                        .Replace("<jumpSpeed>", (carrotBunny.jumpPerStk * 100).ToString())
                        ));
                }
            }

        }
    }
}