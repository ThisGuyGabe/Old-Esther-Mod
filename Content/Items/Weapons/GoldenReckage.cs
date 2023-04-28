using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using EstherMod.Content.Projectiles

namespace EstherMod.Content.Items.Weapons
{
    public class GoldenReckage : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }
        public override void SetDefaults()
        {
            Item.damage = 29;
            Item.DamageType = DamageClass.Melee;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 2;
            Item.value = 10000;
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.noUseGraphic = true;
            Item.shoot = ModContent.ProjectileType<GoldenReckageProjectile>();
            Item.scale = 1.25f;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.GoldBroadsword)
                .AddIngredient(ItemID.Bone, 25)
                .AddIngredient(ItemID.GoldBar, 15)
                .AddIngredient(ItemID.Feather, 5)
                .AddIngredient(ItemID.FallenStar, 3)
                .Register();
        }
    }
}
