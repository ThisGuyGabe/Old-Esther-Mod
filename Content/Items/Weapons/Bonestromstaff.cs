using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;

namespace EstherMod.Content.Items.Weapons
{
	public class Bonestromstaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Bonestrom Staff");
			// Tooltip.SetDefault("Shoots a sigil that summons necromantidc shards that home in on your cursor.");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Laska Kościoburzy");
        }

        public override void SetDefaults()
        {
            Item.damage = 65;
            Item.DamageType = DamageClass.Magic;
            Item.width = 48;
            Item.height = 50;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 6;
            Item.value = 100000;
            Item.rare = 2;
            Item.UseSound = SoundID.Item20;
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("BonestromSigil").Type;
            Item.shootSpeed = 9f;
            Item.mana = 4;
            Item.channel = true;
            Item.staff[Item.type] = true;
            Item.value = Item.sellPrice(gold: 2, silver: 40, copper: 20);
        }
        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[Item.shoot] < 1;

        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            position = Main.MouseWorld;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.Bone, 15)
            .AddIngredient(ItemID.HellstoneBar, 8)
            .AddIngredient(ItemID.Ruby, 1)
            .AddTile(TileID.Anvils)
            .Register();
        }
	
	public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D texture = ModContent.Request<Texture2D>("EstherMod/Content/GlowMasks/BonestromStaff_Glowmask", AssetRequestMode.ImmediateLoad).Value;
            spriteBatch.Draw
            (
                texture,
                new Vector2
                (
                    Item.position.X - Main.screenPosition.X + Item.width * 0.5f,
                    Item.position.Y - Main.screenPosition.Y + Item.height - texture.Height * 0.5f + 2f
                ),
                new Rectangle(0, 0, texture.Width, texture.Height),
                Color.White,
                rotation,
                texture.Size() * 0.5f,
                scale,
                SpriteEffects.None,
                0f
            );
        }
    }
} 
