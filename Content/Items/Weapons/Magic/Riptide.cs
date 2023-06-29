using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EstherMod.Content.Items.Weapons.Magic
{
	public class Riptide : ModItem
	{
		public override Vector2? HoldoutOffset() {
			return new Vector2(-2.5f, -5f);
		}

		public override void SetDefaults() {

			Item.width = 50;
			Item.height = 50;

			Item.useStyle = ItemUseStyleID.Shoot;
			Item.useTime = 32;
			Item.useAnimation = 32;
			Item.autoReuse = true;
			Item.noMelee = true;

			Item.UseSound = SoundID.Item21 with {
				Volume = 2
			};

			Item.DamageType = DamageClass.Magic;
			Item.damage = 32;
			Item.knockBack = 4;
			Item.mana = 20;

			Item.rare = ItemRarityID.LightRed;
			Item.value = Item.sellPrice(gold: 2, silver: 67);

			Item.shoot = Item.shoot = ModContent.ProjectileType<RiptideExplosion>(); // TODO Projectile Sprite is a placeholder until artist makes one.
			Item.shootSpeed = 0f; 
		}

		public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            position = Main.MouseWorld;
        }
	}
}
