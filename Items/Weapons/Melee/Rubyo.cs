using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace EstherMod.Content.Items.Weapons.Melee
{
    public class Rubyo : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Rub-yo");
            // Tooltip.SetDefault("");
            ItemID.Sets.Yoyo[Item.type] = true;
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 16;
            Item.crit = 5;
            Item.rare = 1;
            Item.width = 30;
            Item.height = 32;
            Item.useAnimation = 25;
            Item.useTime = 25;
            Item.useStyle = 1;
            Item.knockBack = 2.5f;
            Item.shootSpeed = 8;
            Item.DamageType = DamageClass.Melee;
            Item.channel = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.value = Item.sellPrice(silver: 92, copper: 28);
            Item.UseSound = SoundID.Item1;
            Item.shoot = Mod.Find<ModProjectile>("RubyoProj").Type;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.LargeRuby, 1);
            recipe.AddIngredient(ItemID.Chain, 15);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}