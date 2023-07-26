using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using System.Linq;
using Terraria.DataStructures;
using Terraria.GameContent.Drawing;

namespace CascadeMod.Content.Items.Weapons.Melee;

public class Calcium : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 38;
        Item.height = 38;
        Item.useTime = 32;
        Item.useAnimation = 32;
        Item.knockBack = 4;
        Item.DamageType = DamageClass.MeleeNoSpeed;
        Item.damage = 35;
        Item.crit = 0;
        Item.useStyle = ItemUseStyleID.HiddenAnimation;
        Item.value = Item.sellPrice(0, 3, 30);
        Item.UseSound = null;
        Item.noMelee = true;
        Item.noUseGraphic = true;
        Item.shoot = ModContent.ProjectileType<CalciumP>();
        Item.channel = true;
        Item.shootSpeed = 0;
        Item.rare = ItemRarityID.Orange;
    }
    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.BoneSword, 1)
            .AddIngredient(ItemID.Bone, 25)
            .AddRecipeGroup(RecipeGroupID.IronBar, 10)
            .AddTile(TileID.Anvils)
            .Register();
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        return !Main.projectile.Any(n => n.active && n.owner == player.whoAmI && n.type == ModContent.ProjectileType<CalciumP>());
    }
}
public class CalciumP : ModProjectile
{
    public override string Texture => "CascadeMod/Assets/Weapons/Melee/Calcium2";
    float STATE = 1;
    public override void SetStaticDefaults()
    {
        ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
        ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
    }
    public override void SetDefaults()
    {
        Projectile.penetrate = -1;
        Projectile.friendly = true;
        Projectile.hostile = false;
        Projectile.knockBack = 4;

        Projectile.width = 80;
        Projectile.height = 80;
        Projectile.scale = 1f;

        Projectile.tileCollide = false;
        Projectile.ignoreWater = true;
        Projectile.DamageType = DamageClass.Melee;
        Projectile.aiStyle = -1;
    }
    private Vector2 mouse = new Vector2();
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        var settings = new ParticleOrchestraSettings
        {
            PositionInWorld = target.Center,
            MovementVector = Projectile.velocity

        };
        ParticleOrchestrator.RequestParticleSpawn(clientOnly: false, ParticleOrchestraType.PaladinsHammer, settings);
        ParticleOrchestrator.RequestParticleSpawn(clientOnly: false, ParticleOrchestraType.PaladinsHammer, settings);
        target.AddBuff(BuffID.Bleeding, Main.rand.Next(200, 400));
    }

    public override void AI()
    {
        Lighting.AddLight(Projectile.Center, new Vector3(0.25f, 0.25f, 0.05f));
        Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Bone);
        Player player = Main.player[Projectile.owner];
        if (STATE == 1 && Projectile.ai[1] <= 0.072)
        {
            Projectile.ai[1] += 0.0045f;
            if (Projectile.scale == 1f)
            {
                Projectile.scale = 0.5f;
                mouse = player.Center.DirectionTo(Main.MouseWorld);
                Projectile.rotation = mouse.ToRotation() + 0.785398f;
            }
            Projectile.friendly = false;
            Projectile.scale += Projectile.ai[1];
        }
        else
        {
            if (STATE != 2) { SoundEngine.PlaySound(SoundID.DD2_MonkStaffSwing, player.Center); }
            Projectile.friendly = true;
            STATE = 2;
            Projectile.ai[0] += 0.04f;
            if (Projectile.ai[0] >= 0.24 && Projectile.ai[0] <= 0.3) { SoundEngine.PlaySound(SoundID.Item7, Projectile.Center); }
            if (Projectile.ai[0] >= 0.4f && Projectile.ai[0] <= 0.44){  Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, mouse * Main.rand.Next(9, 11), ProjectileID.BoneGloveProj, (int)(Projectile.damage * 0.3), 0); }
            if (Projectile.ai[0] >= 0.4f){  Projectile.scale -= Projectile.ai[1];  Projectile.alpha += 50;  }
            if (Projectile.ai[0] >= 0.6f){  Projectile.Kill();  }
        }
        Projectile.rotation = (mouse.ToRotation() + ((Projectile.rotation - 0.785398f + 1.570796f) * 9)) / 10 + 0.785398f - 1.570796f + Projectile.ai[0];
        Projectile.Center = player.Center + ((60 * Projectile.scale) * (Projectile.rotation - 0.785398f).ToRotationVector2());
        player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, Projectile.rotation - 2.35619f);
    }
}
