using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Microsoft.Xna.Framework;

namespace EstherMod.Content.Items.Weapons
{
    public class Zeus : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            // DisplayName.SetDefault("Zeus");
        }
        public override void SetDefaults()
        {
            Item.damage = 28;
            Item.crit = 9;
            Item.rare = 0;
            Item.width = 18;
            Item.height = 50;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.useStyle = 5;
            Item.knockBack = 1.5f;
            Item.shootSpeed = 17f;
            Item.DamageType = DamageClass.Ranged;
            Item.noMelee = true;
            Item.shoot = Mod.Find<ModProjectile>("ZeusArrow").Type;
            Item.value = Item.sellPrice(gold: 1, silver: 87);
            Item.UseSound = SoundID.Item5;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod.Find<ModItem>("GalvanicBar").Type, 4);
            recipe.AddIngredient(ItemID.WoodenBow, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips[0].OverrideColor = new Color(205, 127, 50);
        }
    }
}