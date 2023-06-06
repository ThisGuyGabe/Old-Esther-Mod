using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.Localization;

namespace EstherMod.Content.Items.Weapons.Melee
{
    public class Esther : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("THIS IS GETTING REWORKED THAT'S WHY NO PROJ");
            // Tooltip.SetDefault("");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Ester");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 35;
            Item.crit = 9;
            Item.rare = 3;
            Item.width = 50;
            Item.height = 50;
            Item.useAnimation = 24;
            Item.useTime = 24;
            Item.useStyle = 1;
            Item.knockBack = 5f;
            Item.shootSpeed = 25f;
            Item.DamageType = DamageClass.Melee;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.value = Item.sellPrice(gold: 4);
            Item.UseSound = SoundID.Item1;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Muramasa);
            recipe.AddIngredient(ItemID.EbonwoodSword);
            recipe.AddIngredient(ItemID.LightsBane);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Muramasa);
            recipe.AddIngredient(ItemID.ShadewoodSword);
            recipe.AddIngredient(ItemID.BloodButcherer);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
