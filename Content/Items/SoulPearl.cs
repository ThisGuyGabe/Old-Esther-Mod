using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;

namespace EstherMod.Content.Items
{
	public class SoulPearl : ModItem
	{
		public override void SetStaticDefaults() {
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;
		}

		public override void SetDefaults() {
			Item.width = 22;
			Item.height = 34;
			Item.rare = ItemRarityID.Green;
			Item.maxStack = 999;
			Item.value = Item.sellPrice(silver: 12);
		}
	}
}
