using EstherMod.Content.Buffs;
using EstherMod.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using System.IO;

namespace EstherMod.Content.Items.Weapons
{
    public class EchoBreaker : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("EthericalBow"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            // Tooltip.SetDefault("");
        }

        public override void SetDefaults()
        {
            Item.damage = 30;
            Item.DamageType = DamageClass.Melee;
            Item.crit = 16;
            Item.width = 60;
            Item.height = 60;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 6;
            Item.value = 8000;
            Item.rare = 1;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.channel = true;
        }
        public override void ModifyHitNPC(Player player, NPC target, ref NPC.HitModifiers modifiers)
        {
            if (player.altFunctionUse == 2)
            {
                modifiers.SourceDamage *= 4;
                target.AddBuff(BuffID.Frostburn, 300);
            }
            base.ModifyHitNPC(player, target, ref modifiers);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            player.AddBuff(ModContent.BuffType<EchoBreakCooldown>(), 1500);
            SoundStyle style = new SoundStyle("Terraria/Sounds/Item_107");
            SoundEngine.PlaySound(style);
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (player.altFunctionUse == 2) {
                damage *= 4;
            }
            base.ModifyShootStats(player, ref position, ref velocity, ref type, ref damage, ref knockback);
        }

        public override bool AltFunctionUse(Player player)
        {
            return !player.HasBuff<EchoBreakCooldown>();
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.useTime = 18;
                Item.useAnimation = 18;
                Item.shoot = ModContent.ProjectileType<EchoBreakerProjectile>();
            }
            else
            {
                Item.useTime = 30;
                Item.useAnimation = 30;
                Item.shoot = ProjectileID.None;
            }
            return base.CanUseItem(player);
        }
    }
}
