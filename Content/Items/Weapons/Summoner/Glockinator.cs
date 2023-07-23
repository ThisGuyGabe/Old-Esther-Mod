using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using EstherMod.Content.Buffs;
using EstherMod.Content.Projectiles.Summon;
using System.Linq;

namespace EstherMod.Content.Items.Weapons.Summon
{
    public class Glockinator : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.GamepadWholeScreenUseRange[Item.type] = true;
            ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 24;
            Item.knockBack = 3f;
            Item.mana = 10;
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.value = Item.sellPrice(gold: 3, silver: 50, copper: 10);
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item44;
            Item.noMelee = true;
            Item.shootSpeed = 0.001f;
            Item.DamageType = DamageClass.Summon;
            Item.buffType = ModContent.BuffType<GlockinatorBuff>();
            Item.shoot = ModContent.ProjectileType<GlockinatorSummon>(); 
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            position = Main.MouseWorld;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (!player.HasBuff(ModContent.BuffType<GlockinatorBuff>()))
            {
                player.AddBuff(Item.buffType, 2);

                var projectile = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, Main.myPlayer);
                projectile.originalDamage = Item.damage;

                return false;
            }
            return false;
        }
    }
}
