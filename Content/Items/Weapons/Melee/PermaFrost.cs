using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.Localization;
namespace EstherMod.Content.Items.Weapons.Melee
{
    public class PermaFrost : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("When you hit an enemy it shoots an icicle that deals damage.");
            // DisplayName.SetDefault("Perma Frost");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Stałomróz");
        }
        public override void SetDefaults()
        {
            Item.width = 60;
            Item.height = 60;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 22;
            Item.useAnimation = 22;
            Item.damage = 64;
            Item.autoReuse = true;
            Item.shootSpeed = 3;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 5;
            Item.crit = 0;
            Item.value = Item.sellPrice(gold: 5, silver: 41);
            Item.rare = 5;
            Item.UseSound = SoundID.Item1;
        }
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Frostburn, 240); 
            Projectile.NewProjectile(Item.GetSource_None(), player.Center, Vector2.Normalize(Main.MouseWorld - player.Center) * 8f, ModContent.ProjectileType<Projectiles.Frostpike>(), damageDone, player.whoAmI);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.BorealWood, 7)
            .AddIngredient(ItemID.TitaniumBar, 9)
            .AddIngredient(ItemID.FrostCore, 1)
            .AddTile(TileID.MythrilAnvil)
            .Register();

            CreateRecipe()
            .AddIngredient(ItemID.BorealWood, 7)
            .AddIngredient(ItemID.AdamantiteBar, 9)
            .AddIngredient(ItemID.FrostCore, 1)
            .AddTile(TileID.MythrilAnvil)
            .Register();
        }
    }
}