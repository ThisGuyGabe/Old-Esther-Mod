using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Microsoft.Xna.Framework;
using CascadeMod.Content.Projectiles;

namespace CascadeMod.Content.Items.Weapons.Melee
{
    public class PristineMallet : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 54;
            Item.DamageType = DamageClass.Melee;
            Item.width = 48;
            Item.height = 48;
            Item.useTime = 34;
            Item.useAnimation = 34;
            Item.hammer = 95;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 3;
            Item.value = 51250;
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item1;
            Item.shootSpeed = 15f;
            Item.channel = true;
            Item.scale = 1.25f;
        }
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            SoundEngine.PlaySound(new SoundStyle("CascadeMod/Assets/Sounds/Items/Weapons/Bonk"));
            Projectile.NewProjectile(player.GetSource_FromThis(), target.Center, new Vector2(0, -7.5f), ModContent.ProjectileType<Onomatopoeia>(), 0, 0);
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.WhitePaint, 50)
                .AddIngredient(ItemID.Bone, 15)
                .AddIngredient(ItemID.Diamond, 5)
                .AddIngredient(ItemID.HammerStatue)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}
