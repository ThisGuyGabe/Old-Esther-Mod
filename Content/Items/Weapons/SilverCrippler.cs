using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.Localization;

namespace EstherMod.Content.Items.Weapons
{
    public class SilverCrippler : ModItem
    {
        public override void SetDefaults()
        {

            Item.damage = 10;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 64;
            Item.height = 20;
            Item.useTime = 4;
            Item.useAnimation = 12;
            Item.useStyle = 5;
            Item.reuseDelay = 16;
            Item.noMelee = true;
            Item.knockBack = 1;
            Item.value = Item.sellPrice(gold: 1, silver: 21, copper: 60);
            Item.rare = 1;
            Item.UseSound = SoundID.Item31;
            Item.autoReuse = true;
            Item.shoot = 10;
            Item.shootSpeed = 8f;
            Item.useAmmo = AmmoID.Bullet;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Silver Crippler");
            // Tooltip.SetDefault("");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Srebrny Okaleczacz");
        }


        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            if (Main.rand.Next(3) == 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (type == 36)
            {
                type = ProjectileID.MeteorShot;
            }
            if (type == ProjectileID.Bullet)
            {
                type = 36;
            }
            return true;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10, 0);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.IllegalGunParts, 1);
            recipe.AddIngredient(ItemID.SilverBar, 5);
            recipe.AddIngredient(ItemID.Wood, 25);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.IllegalGunParts, 1);
            recipe.AddIngredient(ItemID.TungstenBar, 5);
            recipe.AddIngredient(ItemID.Wood, 25);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

        }
    }
}
