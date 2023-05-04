using EstherMod.Content.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EstherMod.Content.Items.Placeable
{
    public class HypnoplateItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("Hypnoplate");
        }

        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTurn = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.autoReuse = true;
            Item.width = 18;
            Item.height = 14;
            Item.value = 100;
            Item.maxStack = 999;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<Hypnoplate>();
        }
    }
}
