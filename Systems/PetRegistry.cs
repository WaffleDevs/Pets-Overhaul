using System;
using System.Collections.Generic;

using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace PetsOverhaul.Systems
{
    /// <summary>
    /// ModPlayer class that implements several helper functions to reduce repitition.
    /// </summary>
    sealed public class PetRegistry : ModPlayer
    {
        public int[] TerrariaPetItemIds = new int[] {
            ItemID.Seaweed,
            ItemID.AmberMosquito,
            ItemID.EatersBone,
            ItemID.BoneRattle,
            ItemID.BabyGrinchMischiefWhistle,
            ItemID.Nectar,
            ItemID.HellCake,
            ItemID.Fish,
            ItemID.BambooLeaf,
            ItemID.BoneKey,
            ItemID.ToySled,
            ItemID.StrangeGlowingMushroom,
            ItemID.FullMoonSqueakyToy,
            ItemID.BerniePetItem,
            ItemID.UnluckyYarn,
            ItemID.BlueEgg,
            ItemID.GlowTulip,
            ItemID.ChesterPetItem,
            ItemID.CompanionCube,
            ItemID.CursedSapling,
            ItemID.DirtiestBlock,
            ItemID.BallOfFuseWire,
            ItemID.CelestialWand,
            ItemID.EyeSpring,
            ItemID.ExoticEasternChewToy,
            ItemID.BedazzledNectar,
            ItemID.GlommerPetItem,
            ItemID.DD2PetDragon,
            ItemID.JunimoPetItem,
            ItemID.BirdieRattle,
            ItemID.LizardEgg,
            ItemID.TartarSauce,
            ItemID.ParrotCracker,
            ItemID.PigPetItem,
            ItemID.MudBud,
            ItemID.DD2PetGato,
            ItemID.DogWhistle,
            ItemID.Seedling,
            ItemID.SpiderEgg,
            ItemID.OrnateShadowKey,
            ItemID.SharkBait,
            ItemID.SpiffoPlush,
            ItemID.MagicalPumpkinSeed,
            ItemID.EucaluptusSap,
            ItemID.TikiTotem,
            ItemID.LightningCarrot,
            ItemID.ZephyrFish,
            ItemID.EyeOfCthulhuPetItem,
            ItemID.BrainOfCthulhuPetItem,
            ItemID.EaterOfWorldsPetItem,
            ItemID.KingSlimePetItem,
            ItemID.QueenBeePetItem,
            ItemID.DeerclopsPetItem,
            ItemID.SkeletronPetItem,
            ItemID.QueenSlimePetItem,
            ItemID.SkeletronPrimePetItem,
            ItemID.DestroyerPetItem,
            ItemID.TwinsPetItem,
            ItemID.EverscreamPetItem,
            ItemID.MartianPetItem,
            ItemID.DD2OgrePetItem,
            ItemID.DukeFishronPetItem,
            ItemID.LunaticCultistPetItem,
            ItemID.DD2BetsyPetItem,
            ItemID.IceQueenPetItem,
            ItemID.PlanteraPetItem,
            ItemID.MoonLordPetItem,
            ItemID.ResplendentDessert,
            ItemID.Carrot,
        };
        public Dictionary<int, SoundStyle> PetItemIdToHurtSound = new Dictionary<int, SoundStyle>(){
            {ItemID.Seaweed, SoundID.NPCHit24 with { PitchVariance = 0.4f }},
            {ItemID.LunaticCultistPetItem, SoundID.NPCHit55 with { PitchVariance = 0.6f }},
            {ItemID.LizardEgg, SoundID.NPCHit26 with { PitchVariance = 0.6f }},
            {ItemID.BoneKey, SoundID.NPCHit2 with { PitchVariance = 0.05f, Pitch = 0.1f }},
            {ItemID.SkeletronPetItem, SoundID.NPCHit2 with { PitchVariance = 0.05f, Pitch = 0.1f }},
            {ItemID.ToySled, SoundID.NPCHit11 with { Pitch = -0.5f, PitchVariance = 0.2f }},
            {ItemID.FullMoonSqueakyToy, SoundID.NPCHit6 with { PitchVariance = 0.4f }},
            {ItemID.CursedSapling, SoundID.NPCHit7 with { PitchVariance = 0.4f }},
            {ItemID.CelestialWand, SoundID.NPCHit5 with { PitchVariance = 0.2f, Pitch = 0.5f }},
            {ItemID.UnluckyYarn, SoundID.Meowmere with { PitchVariance = 0.4f, Pitch = 0.6f }},
            {ItemID.CompanionCube, SoundID.NPCHit55 with { Pitch = -0.3f, PitchVariance = 0.5f }},
            {ItemID.ParrotCracker, SoundID.NPCHit46 with { PitchVariance = 0.5f }},
            {ItemID.GlommerPetItem, SoundID.NPCHit35 with { PitchVariance = 0.2f, Pitch = -0.5f }},
            {ItemID.SpiderEgg, SoundID.NPCHit29 with { PitchVariance = 0.3f }},
            {ItemID.OrnateShadowKey, SoundID.NPCHit4 with { PitchVariance = 0.5f }},
            {ItemID.DestroyerPetItem, SoundID.NPCHit4 with { PitchVariance = 0.5f }},
            {ItemID.SkeletronPetItem, SoundID.NPCHit4 with { PitchVariance = 0.5f }},
            {ItemID.TwinsPetItem, SoundID.NPCHit4 with { PitchVariance = 0.5f }},
            {ItemID.PigPetItem, SoundID.Zombie39 with { PitchVariance = 0.3f }},
            {ItemID.LightningCarrot, SoundID.NPCHit34 with { PitchVariance = 0.5f }},
            {ItemID.BrainOfCthulhuPetItem, SoundID.NPCHit9 with { Pitch = 0.1f, PitchVariance = 0.4f }},
            {ItemID.DD2OgrePetItem, SoundID.DD2_OgreHurt with { PitchVariance = 0.7f, Volume = 0.7f }},
            {ItemID.MartianPetItem, SoundID.NPCHit39 with { Pitch = 0.2f, PitchVariance = 0.5f }},
            {ItemID.DD2BetsyPetItem, SoundID.DD2_BetsyHurt with { Pitch = 0.3f, PitchVariance = 0.5f }},
            {ItemID.DukeFishronPetItem, SoundID.Zombie39 with { PitchVariance = 0.8f }},

            };

        public bool isPetItem(int itemId)
        {
            if (Array.IndexOf(TerrariaPetItemIds, itemId) != -1) return true;
            //if (Array.IndexOf(CalamityPetItemIds, itemId) != -1) return true;

            return false;
        }

        public ReLogic.Utilities.SlotId playSoundForItemId(int itemId)
        {
            SoundStyle itemsHurtSound = SoundID.MenuClose;

            if (PetItemIdToHurtSound.ContainsKey(itemId)) itemsHurtSound = PetItemIdToHurtSound[itemId];
            else if (itemId == ItemID.BerniePetItem)
            {
                if (Player.Male == true)
                {
                    itemsHurtSound = SoundID.DSTMaleHurt with { PitchVariance = 0.2f };
                }
                else itemsHurtSound = SoundID.DSTFemaleHurt with { PitchVariance = 0.2f };

            }
            else if (itemId == ItemID.ChesterPetItem)
            {
                if (Main.rand.NextBool(2))
                {
                    itemsHurtSound = SoundID.ChesterOpen with { PitchVariance = 0.2f, Pitch = -0.6f };
                }
                else
                {
                    itemsHurtSound = SoundID.ChesterClose with { PitchVariance = 0.2f, Pitch = -0.6f };
                }

            }
            else if (itemId==ItemID.MoonLordPetItem)
            {
                switch (Main.rand.Next(3))
                {
                    case 0:
                        itemsHurtSound = SoundID.Zombie100 with { PitchVariance = 0.5f, Volume = 0.5f };
                        break;
                    case 1:
                        itemsHurtSound = SoundID.Zombie101 with { PitchVariance = 0.5f, Volume = 0.5f };
                        break;
                    case 2:
                        itemsHurtSound = SoundID.Zombie102 with { PitchVariance = 0.5f, Volume = 0.5f };
                        break;
                }
            }


            if (itemsHurtSound == SoundID.MenuClose) return ReLogic.Utilities.SlotId.Invalid;


            return SoundEngine.PlaySound(itemsHurtSound);
        }
    }
}
