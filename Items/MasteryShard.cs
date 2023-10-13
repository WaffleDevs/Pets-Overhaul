using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace PetsOverhaul.Items
{
    public class MasteryShard : ModItem
    {
        public override void SetStaticDefaults()
        {
            Main.RegisterItemAnimation(Item.type,new DrawAnimationVertical(7,8));
            ItemID.Sets.AnimatesAsSoul[Item.type] = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
        }
        public override void SetDefaults()
        {
            Item.maxStack = 9999;
            Item.rare = ItemRarityID.Master;
            Item.value = 0;
            Item.master = true;
            Item.width = 24;
            Item.height = 24;
        }
        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, new Vector3(2.55f,1.10f,1f) * 0.8f* Main.essScale);
        }
    }
}
