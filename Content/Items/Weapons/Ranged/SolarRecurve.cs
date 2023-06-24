﻿using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.Localization;

namespace EstherMod.Content.Items.Weapons.Ranged
{
    public class SolarRecurve : ModItem
    {
        int amountshot = 2;
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Solar Recurve");
            // Tooltip.SetDefault("Shoots more arrows every time.");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Słoneczne Zakrzywienie");
        }
        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 34;
            Item.rare = ItemRarityID.Green;
            Item.damage = 8;
            Item.knockBack = 2f;
            Item.noMelee = true; // makes it do no melee damage
            Item.useTime = 22;
            Item.useAnimation = 22;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item5;
            Item.DamageType = DamageClass.Ranged;
            Item.value = Item.sellPrice(silver: 90, copper: 52);
            Item.shoot = ProjectileID.WoodenArrowFriendly;
            Item.shootSpeed = 92f;
            Item.useAmmo = AmmoID.Arrow;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float numgorra = amountshot;
            float rotation = MathHelper.ToRadians(5); //the rotation NO WAY!
            position += Vector2.Normalize(velocity) * 45f; //the position offset?
            for (int i = 0; i < numgorra; i++)
            {
                Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numgorra - 1))) * .2f; // Watch out for dividing by 0 if there is only 1 projectile.
                Projectile.NewProjectile(source, position, perturbedSpeed, type, damage, knockback, player.whoAmI);
            }
            if (amountshot <= 3)
            {
                amountshot++;
            }
            else
            {
                amountshot = 2;
            }
            return false;


        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.Cloud, 25)
            .AddIngredient(ItemID.DemoniteBar, 5)
            .AddIngredient(ItemID.FallenStar, 5)
            .AddTile(TileID.Anvils)
            .Register();
            CreateRecipe()
            .AddIngredient(ItemID.Cloud, 25)
            .AddIngredient(ItemID.CrimtaneBar, 5)
            .AddIngredient(ItemID.FallenStar, 5)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}