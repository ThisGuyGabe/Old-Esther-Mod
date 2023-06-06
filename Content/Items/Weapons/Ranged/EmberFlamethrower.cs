using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using System;

namespace EstherMod.Content.Items.Weapons.Ranged
{

	public class EmberFlamethrower : ModItem
	{
        public override void SetStaticDefaults()
        {

        }
		// A useTime of 4 with a useAnimation of 20 means this weapon will shoot out 5 jets of fire in one shot.
		// Vanilla Flamethrower uses values of 6 and 30 respectively, which is also 5 jets in one shot, but over 30 frames instead of 20.
		public override void SetDefaults()
		{
			Item.damage = 69;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 50;
			Item.height = 18;
			Item.useTime = 4;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.knockBack = 2;
			Item.rare = ItemRarityID.Green;
			Item.UseSound = SoundID.Item34;
			Item.autoReuse = true;
			Item.shoot = Mod.Find<ModProjectile>("EmberFlamethrowerProj").Type;
			Item.shootSpeed = 9f; 
			Item.useAmmo = AmmoID.Gel;
		}
		public override bool CanUseItem(Player player)
		{
			return !player.wet;
		}

		public override bool CanConsumeAmmo(Item ammo, Player player)
		{
			return player.itemAnimation >= player.itemAnimationMax - 4;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 54f; 
			if (Collision.CanHit(position, 6, 6, position + muzzleOffset, 6, 6))
			{
				position += muzzleOffset;
			}
			
			return true;
		}
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(0, -2);
		}
	}
}