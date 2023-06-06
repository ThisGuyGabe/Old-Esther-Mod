using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.Localization;
using Terraria.DataStructures;

namespace EstherMod.Content.Items.Accessories
{
	public class TheSoul : ModItem
	{

		public override void SetDefaults()
		{
			Item.width = 40;
			Item.height = 46;
			Item.rare = ItemRarityID.Pink;
			Item.accessory = true;
			Item.value = Item.sellPrice(gold: 9, silver: 48);
			Item.defense += -6;
		}

		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(4, 16)); // 16 frames
			ItemID.Sets.AnimatesAsSoul[Item.type] = true;

			ItemID.Sets.ItemIconPulse[Item.type] = false;
			ItemID.Sets.ItemNoGravity[Item.type] = false;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.SoulofNight, 5);
			recipe.AddIngredient(ItemID.SoulofLight, 5);
			recipe.AddIngredient(ItemID.SoulofSight, 5);
			recipe.AddIngredient(ItemID.SoulofMight, 5);
			recipe.AddIngredient(ItemID.SoulofFright, 5);
			recipe.AddIngredient(ItemID.SoulofFlight, 5);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetDamage(DamageClass.Generic) += -0.04f; // this is the percentage
			player.GetAttackSpeed(DamageClass.Generic) += 0.05f;
			player.statLifeMax2 += 50;
			player.endurance += -0.5f;
			player.jumpBoost = true;
		}
	}
}
