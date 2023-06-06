using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.Localization;

namespace EstherMod.Content.Items.Weapons.Melee
{

    public class FireStormSlasher : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Firestorm Slasher");
            // Tooltip.SetDefault("Upon hitting an enemy they explode dealing 2 times more damage than the sword itself.");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Rzeźnik Ognistej Burzy");
        }
        public override void SetDefaults()
        {
            Item.width = 50;
            Item.height = 50;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.damage = 42;
            Item.autoReuse = true;
            Item.scale = 1.5f;

            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 5;
            Item.crit = 33;

            Item.value = Item.sellPrice(gold: 5);
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item1;
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            Lighting.AddLight(new Vector2(hitbox.X, hitbox.Y), new Vector3(0.9f, 0.4f, 0.3f));
        }
        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, new Vector3(0.9f, 0.4f, 0.3f));
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            Random rand = new Random();
            Projectile.NewProjectile(Item.GetSource_None(), target.Center, new Vector2(0, 0), ProjectileID.SolarWhipSwordExplosion, hit.SourceDamage * 2, hit.Knockback, player.whoAmI);
            target.AddBuff(BuffID.OnFire, rand.Next(120, 240));
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.HellstoneBar, 10);
            recipe.AddIngredient(ItemID.Meteorite, 5);
            recipe.AddIngredient(ItemID.Obsidian, 20);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}

