using EstherMod.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EstherMod.Content.Items.Weapons.Melee;

public sealed class FireStormSlasher : BaseItem {
	public override void SetDefaults() {
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

		Item.value = Item.sellPrice(gold: 5);
		Item.rare = ItemRarityID.Orange;
		Item.UseSound = SoundID.Item1;
	}

	public override void MeleeEffects(Player player, Rectangle hitbox) {
		Lighting.AddLight(new Vector2(hitbox.X, hitbox.Y), new Vector3(0.9f, 0.4f, 0.3f));
	}

	public override void PostUpdate() {
		Lighting.AddLight(Item.Center, new Vector3(0.9f, 0.4f, 0.3f));
	}

	public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone) {
		Projectile.NewProjectile(Item.GetSource_None(), target.Center,Vector2.Zero, ProjectileID.SolarWhipSwordExplosion, hit.SourceDamage * 2, hit.Knockback, player.whoAmI);
		target.AddBuff(BuffID.OnFire, Main.rand.Next(120, 240));
	}

	public override void AddRecipes() {
		CreateRecipe()
			.AddIngredient(ItemID.HellstoneBar, 10)
			.AddIngredient(ItemID.Meteorite, 5)
			.AddIngredient(ItemID.Obsidian, 20)
			.AddTile(TileID.Anvils)
			.Register();
	}
}

