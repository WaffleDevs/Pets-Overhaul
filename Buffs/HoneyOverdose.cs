using PetsOverhaul.PetEffects;
using PetsOverhaul.Systems;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace PetsOverhaul.Buffs
{
    sealed public class HoneyOverdose : ModBuff
    {
        public float abilityHasteIncr = 0;
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }
        public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
        {
            rare = 0;
            tip = Lang.GetBuffDescription(ModContent.BuffType<HoneyOverdose>()).Replace("<AbilityHaste>", abilityHasteIncr.ToString());
        }
        public override void Update(Player player, ref int buffIndex)
        {
            abilityHasteIncr = player.GetModPlayer<HoneyBee>().abilityHaste;
            player.GetModPlayer<GlobalPet>().abilityHaste += abilityHasteIncr;
        }
    }
}
