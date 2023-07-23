using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CascadeMod.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using System.Linq;
using CascadeMod.Core;

namespace CascadeMod.Content.Items.Weapons.Magic
{
    public class EmpyrealStaff : BaseItem
    {
        public override void SetStaticDefaults()
        {
            Item.staff[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.damage = 15;
            Item.DamageType = DamageClass.Magic;
            Item.width = 20;
            Item.height = 38;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.mana = 4;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 2;
            Item.value = 7800;
            Item.crit = 16;
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.DD2_BetsysWrathShot;
            Item.channel = true;
            Item.shoot = ModContent.ProjectileType<Starlight>();
            Item.shootSpeed = 0.0001f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            return !Main.projectile.Any(n => n.active && n.owner == player.whoAmI && n.type == ModContent.ProjectileType<Starlight>());
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup(RecipeGroupID.Wood, 12);
            recipe.AddIngredient(ItemID.FallenStar, 8);
            recipe.AddIngredient(ItemID.Daybloom, 2);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
