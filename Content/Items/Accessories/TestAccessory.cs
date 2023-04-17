using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Creative;
using Terraria.Localization;

namespace EstherMod.Content.Items.Accessories
{
	public class TestAccessory : ModItem
	{

		public override void SetDefaults()
		{

			Item.width = 40;
			Item.height = 42;
			Item.rare = 2;
			Item.accessory = true;
			Item.value = 24500;
		}

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("READ THE DESCRIPTION OF THIS ITEM");
			// Tooltip.SetDefault("This is a test item for haunted candle it stops life regen so u can see if haunted candle is balanced or not");
			Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(6, 4));
			ItemID.Sets.AnimatesAsSoul[Item.type] = true;

			ItemID.Sets.ItemIconPulse[Item.type] = false;
			ItemID.Sets.ItemNoGravity[Item.type] = false;

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;


		}


		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.AddBuff(BuffID.Bleeding, -1);
		}
	}
}
