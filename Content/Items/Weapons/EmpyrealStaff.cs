using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.Localization;
namespace EstherMod.Content.Items.Weapons
{

    public class EmpyrealStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("Use this during night for a mana discount, faster speed, but less damage.");
            // DisplayName.SetDefault("Empyreal Staff");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Różdżka Empirejska");
        }
        public override void SetDefaults()
        {
            Item.width = 36;
            Item.height = 36;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useTime = 23;
            Item.useAnimation = 23;
            Item.autoReuse = true;
            Item.staff[Item.type] = true;
            Item.shoot = ProjectileID.HallowStar;
            Item.mana = 10;
            Item.shootSpeed = 12;
            Item.DamageType = DamageClass.Magic;
            Item.damage = 23;
            Item.knockBack = 7;
            Item.crit = 7;
            Item.value = Item.sellPrice(silver: 60, copper: 20);
            Item.rare = 1;
            Item.UseSound = SoundID.Item72;

        }

        public override void UpdateInventory(Player player)
        {
            if (Main.dayTime == false)
            {
                Item.mana = 5;
                Item.useTime = 10;
                Item.useAnimation = 10;
                Item.damage = 11;
            }
            else
            {
                Item.mana = 10;
                Item.useTime = 20;
                Item.useAnimation = 20;
                Item.damage = 16;
            }
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.Cloud, 25)
            .AddIngredient(ItemID.DemoniteBar, 5)
            .AddIngredient(ItemID.FallenStar, 5)
            .AddTile(TileID.Anvils)
            .Register();
            CreateRecipe()
            .AddIngredient(ItemID.Cloud, 25)
            .AddIngredient(ItemID.CrimtaneBar, 5)
            .AddIngredient(ItemID.FallenStar, 5)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }

}