using System.Collections.Generic;

namespace PetsOverhaul.ModSupport
{
    public class ThoriumSupport : ModdedContent
    {
        public override string InternalModName
        {
            get { return "ThoriumMod"; }
        }
        //If these arent defined, they will be skipped

        public override string[] InternalModdedItemNames
        {
            get
            {
                return new string[] {
                    "AbyssalWhistle", // Abyssal Bunny
                    "AlienResearchNotes", // Research Probe
                    "AncientCheeseBlock",// Ancient Rat
                    "AncientDrachma", // Curious Coinling
                    "AromaticBiscuit", // Skunk
                    "BalloonBall", // Tanuki Girl
                    "BioPod", // Bio-Feeder
                    "BlisterSack", // Flying Blister
                    "BloodSausage", // Lazy Bat
                    "ChaoticMarble", // Chaotic Pet
                    "CloudyChewToy", // Wyvern Pup
                    "DelectableNut", // Lil' Mog
                    "DiverPlushie", // Princess Jellyfish
                    "DoomSayersPenny", // Mini Primordials
                    "EnergizedQuadCube", // Energized Quad-Cube
                    "ExoticMynaEgg", // Exotic Myna
                    "Experiment3", // Experiment #3
                    "FishEgg", // Clownfish
                    "ForgottenLetter", // Lost Snowy Owl
                    "FragmentedRune", // Ferret
                    "FreshPickle", // Normal Dog
                    "FrozenBalloon", // Frozen Balloon
                    "GlassShard", // Glass Shard
                    "GreenFirefly", // Amnesiac
                    "GuildsStaff", // Lil' Necromancer
                    "ModelGun", // Lyrist
                    "MoleCrate", // Skull Cat
                    "PearTreeSapling", // Partridge
                    "PinkSlimeEgg", // Pink Slime
                    "PurifiersRing", // Purifier's Ring
                    "RottenMeat", // Fly
                    "SimpleBroom", // Lil' Maid
                    "StormCloud", // Storm Cloud
                    "SubterraneanBulb", // Subterranean Angler
                    "SuspiciousMoisturizerBottle", // Living Hand
                    "SweetBeet", // Beet Cookie
                    "SwordOfDestiny", // Sword Of Destiny
                    "TortleScute", // Tortle Sage
                    "WhisperingShell", // Creature
                };
            }
        }
        public override Dictionary<int, int[]> MiningXpPerModdedBlock
        {
            get { return null; }

        public void MergeJunimoFishingXp()
        {
            if (FishingXpPerModdedFish != null) Main.LocalPlayer.GetModPlayer<Junimo>().FishingXpPerFish.Concat(FishingXpPerModdedFish);
            if (FishingXpPerModdedKill != null) Main.LocalPlayer.GetModPlayer<Junimo>().FishingXpPerKill.Concat(FishingXpPerModdedKill);
        }
        public override Dictionary<int, int[]> FishingXpPerModdedFish
        {
            get { return null; }
        }
        public override Dictionary<int, int[]> FishingXpPerModdedKill
        {
            get { return null; }
        }

    }
}