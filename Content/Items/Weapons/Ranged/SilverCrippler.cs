using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace EstherMod.Content.Items.Weapons.Ranged
{
    public class SilverCrippler : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 10;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 54;
            Item.height = 28;
            Item.useTime = 4;
            Item.useAnimation = 12;
            Item.useStyle = 5;
            Item.reuseDelay = 16;
            Item.noMelee = true;
            Item.knockBack = 1;
            Item.value = Item.sellPrice(gold: 1, silver: 21, copper: 60);
			Item.rare = ItemRarityID.Green;
			Item.UseSound = SoundID.Item31;
            Item.autoReuse = true;
            Item.shoot = 10;
            Item.shootSpeed = 8f;
            Item.useAmmo = AmmoID.Bullet;
        }
        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            if (Main.rand.NextBool(3))
            {
                return true;
            }
            else
            {
                return false;
            }

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
