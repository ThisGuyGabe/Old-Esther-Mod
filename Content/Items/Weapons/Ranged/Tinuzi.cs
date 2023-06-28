using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace EstherMod.Content.Items.Weapons.Ranged
{
	public class Tinuzi : ModItem
    {
    	public override void SetStaticDefaults() {
        
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults() {
        	Item.width = 48;
            Item.height = 36;
            Item.scale = 0.70f;

            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.Shoot;

            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(gold: 1, silver: 40);

        	Item.damage = 16;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 0.5f;

            Item.autoReuse = true;
            Item.noMelee = true;
            Item.UseSound = SoundID.Item11;

            Item.shoot = 1;
            Item.shootSpeed = 13.5f;
            Item.useAmmo = AmmoID.Bullet;
        }


        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) {
            velocity = velocity.RotatedByRandom(MathHelper.ToRadians(7.5f));
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.TinBar, 40);
            recipe.AddIngredient(ItemID.ShadowScale, 15);
            recipe.AddIngredient(ItemID.IllegalGunParts, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.TinBar, 40);
            recipe.AddIngredient(ItemID.TissueSample, 15);
            recipe.AddIngredient(ItemID.IllegalGunParts, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
