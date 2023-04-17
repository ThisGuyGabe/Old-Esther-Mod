using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.Localization;
using Microsoft.Xna.Framework;

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
    }
} 