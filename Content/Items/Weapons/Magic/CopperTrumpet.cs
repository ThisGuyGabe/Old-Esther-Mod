using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using System;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.Localization;


namespace EstherMod.Content.Items.Weapons.Magic
{
	public class CopperTrumpet : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 5;
			Item.DamageType = DamageClass.Magic;
			Item.width = 36;
			Item.height = 18;
			Item.useTime = 16;
			Item.useAnimation = 16;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 1.5f;
			Item.value = Item.sellPrice(silver: 12);
			Item.rare = 2;
			Item.UseSound = new SoundStyle($"{nameof(EstherMod)}/Assets/Sounds/Items/Weapons/Trumpet");
			Item.autoReuse = true;
			Item.shoot = Mod.Find<ModProjectile>("TrumpetThing").Type;               
			Item.shootSpeed = 3.5f;
			Item.mana = 4;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.CopperBar, 4);
			recipe.AddIngredient(ItemID.Lens, 2);  
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}
} 