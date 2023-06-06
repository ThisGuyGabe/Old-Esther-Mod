using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.Localization;

namespace EstherMod.Content.Items.Weapons.Ranged
{
    public class PhoenixFlame : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Ogień Feniksa");
        }
        public override void SetDefaults()
        {
            Item.damage = 65;
            Item.rare = ItemRarityID.Yellow;
            Item.width = 28;
            Item.height = 80;
            Item.useAnimation = 25;
            Item.useTime = 8;
            Item.reuseDelay = 20;

            Item.consumeAmmoOnLastShotOnly = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 5f;

            Item.useStyle = 5;
            Item.shootSpeed = 10f;
            Item.DamageType = DamageClass.Ranged;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.value = Item.sellPrice(gold: 18, silver: 42);
            Item.UseSound = SoundID.Item5;
            Item.shoot = 1;
            Item.useAmmo = AmmoID.Arrow;
            Item.crit = 4;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.HellwingBow, 2);
            recipe.AddIngredient(ItemID.LihzahrdPowerCell, 10);
            recipe.AddIngredient(ItemID.SoulofFright, 15);
            recipe.AddIngredient(ItemID.HellstoneBar, 30);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) {
            if (type == ProjectileID.WoodenArrowFriendly) {
                type = Mod.Find<ModProjectile>("PhoenixFlameProjectile").Type; // Turns Wooden Arrows into Phoenix arrows
            }

            if (type == ProjectileID.FireArrow) {
                type = ProjectileID.HellfireArrow; // Turns Flaming Arrows into Hellfire Arrows
            }
        }
    }
}
