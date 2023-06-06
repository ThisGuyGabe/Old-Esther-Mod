using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.Localization;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;

namespace EstherMod.Content.Items.Weapons.Melee
{
    public class BloomingBane : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Blooming Bane");
            // Tooltip.SetDefault("Wielded by the purest of warriors \nHas a chance to inflict poison for a short duration when hitting an enemy");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Kwitnący Pogromca");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 26;
            Item.crit = 24;
            Item.rare = 3;
            Item.width = 40;
            Item.height = 40;
            Item.useAnimation = 25;
            Item.noUseGraphic = true;
            Item.channel = true;
            Item.useTime = 25;
            Item.useStyle = 5;
            Item.knockBack = 5f;
            Item.shootSpeed = 25f;
            Item.DamageType = DamageClass.Melee;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.value = Item.sellPrice(gold: 1, silver: 40, copper: 60);
            Item.UseSound = SoundID.Item1;
            Item.shoot = Mod.Find<ModProjectile>("BloomingBaneProjectile").Type;
            Item.shootSpeed = 15f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.JungleSpores, 5);
            recipe.AddIngredient(ItemID.Stinger, 3);
            recipe.AddIngredient(ItemID.Vine, 1);
            recipe.AddIngredient(ItemID.Terragrim, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}


