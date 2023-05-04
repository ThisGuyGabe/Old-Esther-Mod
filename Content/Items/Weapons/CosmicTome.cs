using EstherMod.Content.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EstherMod.Content.Items.Weapons
{
    public class CosmicTome : ModItem
    {
        public override void SetDefaults()
        {
            Item.mana = 10;
            Item.damage = 16;
            Item.DamageType = DamageClass.Magic;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.width = 36;
            Item.height = 44;
            Item.UseSound = SoundID.Item8;
            Item.useAnimation = 26;
            Item.useTime = 26;
            Item.rare = ItemRarityID.Orange;
            Item.noMelee = true;
            Item.value = 10000;
            Item.crit = 14;
            Item.shoot = ModContent.ProjectileType<ChantSpark>();
            Item.shootSpeed = 7f;
        }
    }
}
