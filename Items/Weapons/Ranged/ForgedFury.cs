using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.Localization;

namespace EstherMod.Content.Items.Weapons.Ranged
{
    public class ForgedFury : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Forged Fury");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Wykuta Furia");
        }
        public override void SetDefaults()
        {
            Item.damage = 22;
            Item.crit = 4;
            Item.rare = 2;
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

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.ShadewoodBow, 1);
            recipe.AddIngredient(ItemID.AshBlock, 15);
            recipe.AddIngredient(ItemID.Hellstone, 15);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.EbonwoodBow, 1);
            recipe.AddIngredient(ItemID.AshBlock, 15);
            recipe.AddIngredient(ItemID.Hellstone, 15);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

        }
    }
}