using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using System;

namespace EstherMod.Content.Projectiles
{
	public class DragonsKarmaProjectile : ModProjectile
	{
		public override void SetDefaults()
		{
			Projectile.width = 8;
			Projectile.height = 8;
			Projectile.aiStyle = 0;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.penetrate = 5;
			Projectile.ignoreWater = false;
			Projectile.timeLeft = 1000;
			Projectile.alpha = 255;
			Projectile.tileCollide = true;
			Projectile.extraUpdates = 10;
		}

		public override void AI()
        {
			int dust = Dust.NewDust(Projectile.Center, 1, 1, 33, 0f, 0f, 0, default(Color), 1f);
			Main.dust[dust].noGravity = true;
			Main.dust[dust].velocity *= 0.2f;
			Main.dust[dust].scale = (float)Main.rand.Next(100, 135) * 0.013f;

			int dust2 = Dust.NewDust(Projectile.Center, 1, 1, 34, 0f, 0f, 0, default(Color), 1f);
			Main.dust[dust2].noGravity = true;
			Main.dust[dust2].velocity *= 0.2f;
			Main.dust[dust2].scale = (float)Main.rand.Next(100, 135) * 0.013f;
		}
	}
}

