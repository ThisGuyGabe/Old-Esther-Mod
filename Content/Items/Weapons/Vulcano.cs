using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.Localization;

namespace EstherMod.Content.Items.Weapons
{
    public class Vulcano : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Vulcano");
            // Tooltip.SetDefault("");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Wulkan");
        }
        public override void SetDefaults()
        {
            Item.damage = 71;
            Item.crit = 21;
            Item.rare = ItemRarityID.LightRed;
            Item.width = 32;
            Item.height = 72;
            Item.useAnimation = 16;
            Item.useTime = 8;
            Item.useStyle = 5;
            Item.knockBack = 4.5f;
            Item.shootSpeed = 17f;
            Item.DamageType = DamageClass.Ranged;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.value = Item.sellPrice(gold: 5, silver: 67);
            Item.UseSound = SoundID.Item5;
            Item.shoot = 28;
            Item.useAmmo = AmmoID.Arrow;
            Item.reuseDelay = 10;
        }

        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            return !(player.itemAnimation < Item.useAnimation - 2);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), position.X, position.Y, velocity.X, velocity.Y, ProjectileID.FireArrow, damage, knockback, player.whoAmI);
            return true;
        }


        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.SoulofFright, 10);
            recipe.AddIngredient(Mod.Find<ModItem>("ForgedFury").Type, 1);
            recipe.AddIngredient(ItemID.HallowedBar, 20);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}