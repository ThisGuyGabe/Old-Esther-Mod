using EstherMod.Content.Buffs;
using EstherMod.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace EstherMod.Content.Items.Weapons.Melee;

public sealed class EchoBreaker : ModItem {
	public override void SetDefaults() {
		Item.damage = 30;
		Item.DamageType = DamageClass.Melee;
		Item.crit = 16;
		Item.width = 60;
		Item.height = 60;
		Item.useTime = 30;
		Item.useAnimation = 30;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.knockBack = 6;
		Item.value = Item.sellPrice(gold: 1, silver: 33, copper: 28);
		Item.rare = ItemRarityID.Green;
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;
		Item.channel = true;
	}

	public override bool AltFunctionUse(Player player) {
		return !player.HasBuff<EchoBreakCooldown>();
	}

	public override void ModifyHitNPC(Player player, NPC target, ref NPC.HitModifiers modifiers) {
		if (player.altFunctionUse == 2) {
			modifiers.SourceDamage *= 4;

			target.AddBuff(BuffID.Frostburn, 300);
		}
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
		if (player.altFunctionUse == 2) {
			SoundEngine.PlaySound(SoundID.Item107);

			player.AddBuff(ModContent.BuffType<EchoBreakCooldown>(), 600);
			return true;
		}
		else {
			return false;
		}
	}

	public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) {
		if (player.altFunctionUse == 2) {
			damage *= 4;
		}
	}

	public override float UseAnimationMultiplier(Player player) {
		if (player.altFunctionUse == 2) {
			return 0.4f;
		}
		return 1f;
	}

	public override bool CanUseItem(Player player) {
		if (player.altFunctionUse == 2) {
			Item.useTime = 18;
			Item.useAnimation = 18;
			Item.shoot = ModContent.ProjectileType<EchoBreakerProjectile>();
		}
		else {
			Item.useTime = 30;
			Item.useAnimation = 30;
		}
		return true;
	}
}
