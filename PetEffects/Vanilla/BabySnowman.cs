using Terraria;
using Terraria.ID;
using PetsOverhaul.Systems;
using Terraria.ModLoader;

namespace PetsOverhaul.PetEffects.Vanilla
{
    sealed public class BabySnowman : ModPlayer
    {
        public int frostburnTime = 300;
        public float snowmanSlow = 0.2f;
        public int slowTime = 180;
        GlobalPet Pet { get => Player.GetModPlayer<GlobalPet>(); }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Pet.PetInUseWithSwapCd(ItemID.ToySled))
            {
                if (Player.armor[0].type == ItemID.FrostHelmet && Player.armor[1].type == ItemID.FrostBreastplate && Player.armor[2].type == ItemID.FrostLeggings)
                {
                    target.AddBuff(BuffID.Frostburn2, frostburnTime * 3);
                    target.GetGlobalNPC<NpcPet>().AddSlow(NpcPet.SlowId.Snowman, snowmanSlow * 3, slowTime * 3);
                }
                else
                {
                    target.AddBuff(BuffID.Frostburn2, frostburnTime);
                    target.GetGlobalNPC<NpcPet>().AddSlow(NpcPet.SlowId.Snowman, snowmanSlow, slowTime);
                }
            }
        }

    }
}
