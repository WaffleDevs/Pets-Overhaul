using Terraria;
using Terraria.ID;
using PetsOverhaul.Systems;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.Audio;
using PetsOverhaul.Config;

namespace PetsOverhaul.PetEffects
{
    sealed public class TinyDeerclops : ModPlayer
    {
        public List<Point> deerclopsTakenDamage = new(100);
        public int damageStoreTime = 300;
        public float healthTreshold = 0.4f;
        public int range = 480;
        public float slow = 0.4f;
        public int applyTime = 300;
        public int immuneTime = 180;
        public int cooldown = 1800;
        GlobalPet Pet { get => Player.GetModPlayer<GlobalPet>(); }
        public override void OnHurt(Player.HurtInfo info)
        {
            if (Pet.PetInUse(ItemID.DeerclopsPetItem))
                deerclopsTakenDamage.Add(new Point(info.Damage, 0));
        }
        public override void PreUpdate()
        {
            if (Pet.PetInUse(ItemID.DeerclopsPetItem))
                Pet.timerMax = cooldown;
        }
        public override void PostUpdateEquips()
        {
            if (Pet.PetInUseWithSwapCd(ItemID.DeerclopsPetItem))
            {
                if (deerclopsTakenDamage.Count > 0)
                {
                    for (int i = 0; i < deerclopsTakenDamage.Count; i++) //List'lerde struct'lar bir nevi readonly olarak çalıştığından, değeri alıp tekrar atıyoruz
                    {
                        var point = deerclopsTakenDamage[i];
                        point.Y++;
                        deerclopsTakenDamage[i] = point;
                    }
                    int indexToRemove = deerclopsTakenDamage.FindIndex(x => x.Y >= damageStoreTime);
                    if (indexToRemove != -1)
                    {
                        deerclopsTakenDamage.RemoveAt(indexToRemove);
                    }
                    int totalDamage = 0;
                    deerclopsTakenDamage.ForEach(x => totalDamage += x.X);
                    if (totalDamage > Player.statLifeMax2 * healthTreshold && Pet.timer <= 0)
                    {
                        Pet.timer = Pet.timerMax;
                        if (ModContent.GetInstance<Personalization>().AbilitySoundDisabled == false)
                            SoundEngine.PlaySound(SoundID.DeerclopsScream with { PitchVariance = 0.4f, MaxInstances = 5 }, Player.position);
                        for (int i = 0; i < Main.maxNPCs; i++)
                        {
                            NPC npc = Main.npc[i];
                            if (npc.active && Player.Distance(npc.Center) < range)
                            {
                                npc.GetGlobalNPC<NpcPet>().AddSlow(NpcPet.SlowId.Deerclops, slow, applyTime);
                                if (npc.active && (npc.townNPC == false || npc.isLikeATownNPC == false || npc.friendly == false) && (npc.boss == false || npc.GetGlobalNPC<NpcPet>().nonBossTrueBosses[npc.type] == false))
                                npc.AddBuff(BuffID.Confused, applyTime);
                                if (npc.active && (npc.townNPC == false || npc.isLikeATownNPC == false || npc.friendly == false))
                                npc.AddBuff(BuffID.Frostburn, applyTime);
                            }
                        }
                        Player.SetImmuneTimeForAllTypes(immuneTime);
                    }
                }
            }
        }
    }
}
