using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.Localization;
using Terraria.DataStructures;


namespace EstherMod.Content.Items.Weapons.Melee
{
    public class SantasLittleHelper : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("Festive! \nShoots three random colored presents that bounce and release coal on death.");
            // DisplayName.SetDefault("Santa's Little Helper");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Mały Pomocnik Mikołaja");
        }
        public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 48;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.damage = 14;
            Item.autoReuse = true;
            Item.shootSpeed = 3;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 2;
            Item.crit = 5;
            Item.value = Item.sellPrice(silver: 30);
            Item.rare = 2;
            Item.UseSound = SoundID.Item1;
            Item.shoot = Mod.Find<ModProjectile>("YellowPresent").Type;
            Item.shootSpeed = 12f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 oldvelocity = velocity;
            for (int i = 0; i < 1; i++)
            {
                position = new Vector2(Main.MouseWorld.X, Main.MouseWorld.Y + 100000f);
                velocity = new Vector2(0, -30);
                Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(5));
                newVelocity *= 1f - Main.rand.NextFloat(0.3f);
                newVelocity *= -1;
                Projectile.NewProjectile(source, new Vector2(position.X + Main.rand.Next(-10, 10), player.Center.Y - 600 + Main.rand.Next(-10, 10)), newVelocity, type, damage, knockback, player.whoAmI);

            }
            velocity = oldvelocity;
            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Wood, 12);
            recipe.AddIngredient(ItemID.Present, 5);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }

		public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
		{
            if (type != Mod.Find<ModProjectile>("YellowPresent").Type)
			{
				return;
			}
			type = Mod.Find<ModProjectile>("GreenPresent").Type;
			if (Main.rand.NextBool(3))
			{
				return;
			}
			type = Mod.Find<ModProjectile>("BluePresent").Type;
			if (Main.rand.NextBool(3))
			{
				return;
			}
            type = Mod.Find<ModProjectile>("YellowPresent").Type;
            if (!Main.rand.NextBool(3))
            {
                return;
            }
        }
	}
}