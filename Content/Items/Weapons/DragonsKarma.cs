using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Graphics.Effects;

namespace EstherMod.Content.Items.Weapons
{
	public class DragonsKarma : ModItem
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Dragon's Karma");
			// Tooltip.SetDefault("");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults() {
			
			Item.width = 92;
			Item.height = 38; 
			Item.rare = ItemRarityID.Lime;
			Item.value = Item.sellPrice(0, 8, 6, 6);
			Item.useTime = 32;
			Item.useAnimation = 32; 
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.autoReuse = true; 
			Item.UseSound = SoundID.Item36; 
			Item.DamageType = DamageClass.Ranged;
			Item.damage = 79;
			Item.knockBack = 5f;
			Item.noMelee = true;
			Item.shoot = Mod.Find<ModProjectile>("DragonsKarmaProjectile").Type;
			Item.shootSpeed = 16f;
			Item.useAmmo = AmmoID.Bullet;
			Item.crit = 14;
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
			EstherModPlayer modPlayer = player.GetModPlayer<EstherModPlayer>();

			modPlayer.partyBombActive = false;

			if (Filters.Scene["Confetti"].Active)
			{
				Filters.Scene["Confetti"].Deactivate();
			}
		}

		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				Item.width = 92;
				Item.height = 38;
				Item.rare = ItemRarityID.Lime;
				Item.value = Item.sellPrice(0, 8, 6, 6);
				Item.useTime = 32;
				Item.useAnimation = 32;
				Item.useStyle = ItemUseStyleID.Shoot;
				Item.autoReuse = true;
				Item.UseSound = SoundID.Item36;
				Item.DamageType = DamageClass.Ranged;
				Item.damage = 79;
				Item.knockBack = 5f;
				Item.noMelee = true;
				Item.shoot = Mod.Find<ModProjectile>("PartyBombProjectile").Type;
				Item.shootSpeed = 16f;
				Item.useAmmo = AmmoID.Bullet;
				Item.crit = 14;
			}
			else
			{
				Item.width = 92;
				Item.height = 38;
				Item.rare = ItemRarityID.Lime;
				Item.value = Item.sellPrice(0, 8, 6, 6);
				Item.useTime = 32;
				Item.useAnimation = 32;
				Item.useStyle = ItemUseStyleID.Shoot;
				Item.autoReuse = true;
				Item.UseSound = SoundID.Item36;
				Item.DamageType = DamageClass.Ranged;
				Item.damage = 79;
				Item.knockBack = 5f;
				Item.noMelee = true;
				Item.shoot = Mod.Find<ModProjectile>("DragonsKarmaProjectile").Type;
				Item.shootSpeed = 16f;
				Item.useAmmo = AmmoID.Bullet;
				Item.crit = 14;
			}
			return base.CanUseItem(player);
		}

		public override void AddRecipes() 
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(Mod.Find<ModItem>("SilverCrippler").Type, 1);
			recipe.AddIngredient(ItemID.ShroomiteBar, 10);
			recipe.AddIngredient(ItemID.Nanites, 50);
			recipe.AddIngredient(ItemID.Megashark, 1);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}

		public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
		{
			type = Mod.Find<ModProjectile>("PartyBombProjectile").Type;
		}

		public override Vector2? HoldoutOffset() {
			return new Vector2(-2f, -2f);
		}
	}
}
