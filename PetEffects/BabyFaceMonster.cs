using Terraria;
using Terraria.ID;
using PetsOverhaul.Systems;
using Terraria.ModLoader;
using Terraria.Audio;
using PetsOverhaul.Config;

namespace PetsOverhaul.PetEffects
{
    sealed public class BabyFaceMonster : ModPlayer
    {
        private int timer = 0;
        public int stage2time = 1200;
        public int stage1time = 900;
        public int stage1regen = 3;
        public int stage2regen = 15;
        public float stage2ShieldMult = 0.05f;
        GlobalPet Pet { get => Player.GetModPlayer<GlobalPet>(); }
        public override void PreUpdate()
        {
            if (Pet.PetInUse(ItemID.BoneRattle))
            {
                timer--;
                if (timer < -1000)
                    timer = -1000;
            }
        }
        public override void PostUpdateEquips()
        {
            if (Pet.PetInUseWithSwapCd(ItemID.BoneRattle))
            {
                if (timer == 0)
                {
                    if (ModContent.GetInstance<Personalization>().AbilitySoundDisabled == false)
                        SoundEngine.PlaySound(SoundID.Zombie21 with { Pitch = -0.7f, PitchVariance = 0.3f, Volume = 0.75f }, Player.position);
                }
                if (timer <= 0)
                {
                    if (Pet.shieldAmount< (int)(Player.statLifeMax2 * stage2ShieldMult))
                    {
                        Pet.shieldAmount = (int)(Player.statLifeMax2 * stage2ShieldMult);
                        Pet.shieldTimer = 1;
                    }
                    Player.lifeRegen += stage2regen;
                    Player.crimsonRegen = true;
                }
                if (timer == (int)(stage1time * (1 / (1 + Pet.abilityHaste))))
                {
                    if (ModContent.GetInstance<Personalization>().AbilitySoundDisabled == false)
                        SoundEngine.PlaySound(new SoundStyle(SoundID.DD2_DrakinShot.SoundPath + "0") with { Pitch = -0.7f, PitchVariance = 0.3f, Volume = 0.75f }, Player.position);
                }
                if (timer <= (int)(stage1time * (1 / (1 + Pet.abilityHaste))))
                {
                    Player.lifeRegen += stage1regen;
                    Player.crimsonRegen = true;
                }
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Pet.PetInUse(ItemID.BoneRattle))
                timer = (int)(stage1time * (1 / (1 + Pet.abilityHaste))) - 1;
        }
        public override void ModifyHurt(ref Player.HurtModifiers modifiers)
        {
            if (Pet.PetInUse(ItemID.BoneRattle))
                timer = (int)(stage2time * (1 / (1 + Pet.abilityHaste)));
        }
    }
}
