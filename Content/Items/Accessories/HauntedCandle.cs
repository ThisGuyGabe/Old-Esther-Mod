using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.Localization;

namespace EstherMod.Content.Items.Accessories
{
	public class HauntedCandle : ModItem
	{

		public override void SetDefaults()
		{

			Item.width = 40;
			Item.height = 42;
			Item.rare = 2;
			Item.accessory = true;
			Item.value = Item.sellPrice(gold: 2, silver: 64);
		}

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Haunted Candle");
			/* Tooltip.SetDefault("increases magic damage by 5%. \n" +
			"Increases critical strike chance for mage weapons by 6.\n" +
			"Maximum mana increases by 20 and the lower your health is the more damage you do works with all classes.\n" +
			"Also wearing this accessory increases spawn rates like a water candle."); */
			Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(6, 4));
			ItemID.Sets.AnimatesAsSoul[Item.type] = true;

			ItemID.Sets.ItemIconPulse[Item.type] = false;
			ItemID.Sets.ItemNoGravity[Item.type] = false;

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Nawiedzona Świeczka");

		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(Mod.Find<ModItem>("SoulPebble").Type, 10);
			recipe.AddIngredient(Mod.Find<ModItem>("SoulPearl").Type, 1);
			recipe.AddIngredient(ItemID.WaterCandle, 1);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.statManaMax2 += 20;
			player.GetDamage(DamageClass.Magic) += 0.05f;
			player.GetCritChance(DamageClass.Magic) += 6;
			player.ZoneWaterCandle = true;
			player.AddBuff(BuffID.WaterCandle, -1);

			
			if (player.statLife < 250)
			{
				player.GetDamage(DamageClass.Generic) += 0.07f;
			}

			if (player.statLife < 150)
			{
				player.GetDamage(DamageClass.Generic) += 0.13f;
			}

			if (player.statLife < 50)
			{
				player.GetDamage(DamageClass.Generic) += 0.18f;
			}
		}
	}
}
