using PetsOverhaul.Systems;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using PetsOverhaul.PetEffects.Vanilla;

namespace PetsOverhaul.Buffs
{
    public class SparkleSlimy : GlobalBuff
    {
        public override void Update(int type, NPC npc, ref int buffIndex)
        {
            Main.buffNoTimeDisplay[BuffID.GelBalloonBuff] = false;
            Main.buffNoTimeDisplay[BuffID.Wet] = false;
            Main.buffNoTimeDisplay[BuffID.Slimed] = false;
            if (type == BuffID.GelBalloonBuff&&GlobalPet.QueenSlimePetActive(out Player queenSlime))
            {
                npc.GetGlobalNPC<NpcPet>().AddSlow(NpcPet.SlowId.QueenSlime, queenSlime.GetModPlayer<SlimePrincess>().slow, 1);
            }
        }
    }
}
