using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.Localization;

namespace EstherMod.Content.Items.Weapons
{
    public class ThePhoenixFlame : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Ogień Feniksa");
        }
        public override void SetDefaults()
        {
            Item.damage = 85;
            Item.crit = 29;
            Item.rare = ItemRarityID.Yellow;
            Item.width = 28;
            Item.height = 80;
            Item.useAnimation = 18;
            Item.useTime = 6;
            Item.reuseDelay = 14;

            Item.consumeAmmoOnLastShotOnly = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 2.5f;

            Item.useStyle = 5;
            Item.knockBack = 4.5f;
            Item.shootSpeed = 6f;
            Item.DamageType = DamageClass.Ranged;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.value = Item.sellPrice(gold: 18, silver: 42);
            Item.UseSound = SoundID.Item5;
            Item.shoot = 1;
            Item.useAmmo = AmmoID.Arrow;
            Item.crit = 4;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            const int NumProjectiles = 1;

            for (int i = 0; i < NumProjectiles; i++)
            {
                Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(15));
                newVelocity *= 15f - Main.rand.NextFloat(0.2f);
                Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);
            }

            return false;
        }


        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.FragmentSolar, 10);
            recipe.AddIngredient(ItemID.LunarBar, 15);
            recipe.AddIngredient(Mod.Find<ModItem>("Vulcano").Type, 1);
            recipe.AddIngredient(ItemID.Phantasm, 1);
            recipe.AddIngredient(ItemID.DaedalusStormbow, 1);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) {
            if (type == ProjectileID.WoodenArrowFriendly) {
                type = ProjectileID.Hellwing; // Turns Wooden Arrows into Hellwing bats 
                // Currently bugged the bats don't disappear quickly.
            }

            if (type == ProjectileID.FireArrow) {
                type = ProjectileID.HellfireArrow; // Turns Flaming Arrows into Hellfire Arrows
            }
        }
    }
}