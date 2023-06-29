using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EstherMod.Content.Items.Weapons.Ranged
{
    public class ForgedFury : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 36;
            Item.crit = 4;
			Item.rare = ItemRarityID.Green;

			Item.width = 20;
            Item.height = 52;
            Item.useAnimation = 24;
            Item.useTime = 24;
            Item.useStyle = 5;
            Item.knockBack = 2.5f;
            Item.shootSpeed = 12f;
            Item.DamageType = DamageClass.Ranged;
            Item.noMelee = true;
            Item.value = Item.sellPrice(gold: 1, silver: 95);
            Item.UseSound = SoundID.Item5;
            Item.shoot = 11;
            Item.useAmmo = AmmoID.Arrow;
        }

         public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) {
            if (type == ProjectileID.WoodenArrowFriendly) {
                type = ProjectileID.FireArrow; // Turns wooden Arrows into fire arrows
            }

             if (type == ProjectileID.FireArrow) {
                type = Mod.Find<ModProjectile>("ForgedFuryProjectile").Type; // Turns flaming arrows into fury arrows.
            }

        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.ShadewoodBow, 1);
            recipe.AddIngredient(ItemID.Obsidian, 15);
            recipe.AddIngredient(ItemID.HellstoneBar, 15);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.EbonwoodBow, 1);
            recipe.AddIngredient(ItemID.Obsidian, 15);
            recipe.AddIngredient(ItemID.HellstoneBar, 15);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

        }
    }
}
