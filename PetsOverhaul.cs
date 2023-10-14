using Microsoft.Xna.Framework;

using System;
using System.Collections.Generic;


using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using PetsOverhaul.Systems;
using System.Linq;

namespace PetsOverhaul
{
    public class PetsOverhaul : Mod
    {
        public Mod calamityMod;
        public string[] CalamityPetsInternalNames = new string[]
        {

        };
        public Mod thoriumMod;
        public string[] ThoriumPetsInternalNames = new string[]
        {
            "AbyssalWhistle",
            "AncientCheeseBlock",
            "AncientDrachma",
            "AromaticBiscuit",
            "BalloonBall",
            "BioPod",
            "BlisterSack",
            "BloodSausage",
            "ChaoticMarble",
            "CloudyChewToy",
            "DelectableNut",
            "DiverPlushie",
            "DoomSayersPenny",
            "EnergizedQuadCube",
            "EnergizedQuadCube",
            "ExoticMynaEgg",
            "Experiment3",
            "FishEgg",
            "ForgottenLetter",
            "FragmentedRune",
            "FreshPickle",
            "FrozenBalloon",
            "GlassShard",
            "GreenFirefly",
            "GuildsStaff",
            "ModelGun",
            "MoleCrate",
            "PearTreeSapling",
            "PinkSlimeEgg",
            "PurifiersRing",
            "RottenMeat",
            "SimpleBroom",
            "StormCloud",
            "SubterraneanBulb",
            "SuspiciousMoisturizerBottle",
            "SweetBeet",
            "SwordOfDestiny",
            "TortleScute",
            "WhisperingShell"
        };

        public override void PostSetupContent()
        {
            Console.WriteLine("RUNNINGGG");
            if (ModLoader.TryGetMod("Calamity", out calamityMod))
            {
                foreach (string internalName in CalamityPetsInternalNames)
                {
                    ModItem item;
                    calamityMod.TryFind(internalName, out item);
                    Console.WriteLine($"IN: {internalName}\n Type: {item.Type}");

                    ModContent.GetInstance<PetRegistry>().TerrariaPetItemIds.TryAdd(internalName, item.Type);
                };
            }
            if (ModLoader.TryGetMod("ThoriumMod", out thoriumMod))
            {
                Console.WriteLine(thoriumMod.DisplayName);
                foreach (string internalName in ThoriumPetsInternalNames)
                {
                    ModItem item;
                    if (!thoriumMod.TryFind($"{internalName}", out item)) { Console.WriteLine($"Failed: {internalName}\n"); continue; };
                    Console.WriteLine($"IN: {internalName}\n Type: {item.Type}");

                    ModContent.GetInstance<PetRegistry>().TerrariaPetItemIds.TryAdd(internalName, item.Type);
                };
            }
        }
    }
}