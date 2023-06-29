using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace EstherMod.Content.Items.Weapons.Melee
{
    public class SoulSlaughter : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 22;
            Item.useStyle = ItemUseStyleID.Rapier;
            Item.useTime = 3;
            Item.useAnimation = 3;
            Item.damage = 17;
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("SoulSlaughter_Projectile").Type;
            Item.shootSpeed = 0f;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 5;
            Item.noUseGraphic = true;
            Item.crit = 15;
            Item.value = Item.sellPrice(gold: 2, silver: 86);
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item1;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            return player.ownedProjectileCounts[Item.shoot] < 1;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod.Find<ModItem>("SoulPebble").Type, 14);
            recipe.AddIngredient(Mod.Find<ModItem>("SoulPearl").Type, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}