using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EstherMod.Content.Items.Weapons.Melee
{
    public class NaturaEvolutionis : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.Yoyo[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.damage = 9;
            Item.crit = 5;
            Item.rare = ItemRarityID.Green;
            Item.width = 32;
            Item.height = 46;
            Item.useAnimation = 35;
            Item.useTime = 35;
            Item.useStyle = 1;
            Item.knockBack = 2.5f;
            Item.shootSpeed = 6;
            Item.DamageType = DamageClass.Melee;
            Item.channel = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.value = Item.sellPrice(silver: 73, copper: 22);
            Item.UseSound = SoundID.Item1;
            Item.shoot = Mod.Find<ModProjectile>("NaturaProjectile").Type;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.JungleYoyo);
            recipe.AddIngredient(ItemID.Stinger, 5);
            recipe.AddIngredient(ItemID.RichMahogany, 25);
            recipe.AddIngredient(ItemID.Vine, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}