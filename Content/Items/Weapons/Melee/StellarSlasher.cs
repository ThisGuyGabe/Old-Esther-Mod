using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using CascadeMod.Content.Projectiles;
using Microsoft.Xna.Framework;
using CascadeMod.Content.Dusts;
using Microsoft.CodeAnalysis;
using System;
using static Terraria.NPC;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;
using Terraria.ModLoader.IO;
using Terraria.GameContent.UI.Elements;
using System.Collections.Generic;
using Terraria.GameContent;
using Terraria.GameInput;

namespace CascadeMod.Content.Items.Weapons.Melee
{
    public class StellarSlasher : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }
        public override void SetDefaults()
        {
            Item.damage = 14;
            Item.DamageType = DamageClass.Melee;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 31;
            Item.useAnimation = 31;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 2;
            Item.value = 10000;
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<StellarSlash>();
            Item.shootSpeed = 15f;
            Item.channel = true;
            Item.scale = 1.25f;
        }
        public override void ModifyShootStats(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref Microsoft.Xna.Framework.Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            damage = (int)Math.Round(damage*0.4);
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            int counter = 0;

            Microsoft.Xna.Framework.Vector2 vector = new Microsoft.Xna.Framework.Vector2(0,0);
            while (counter != 2)
            {
                vector = new Microsoft.Xna.Framework.Vector2(target.Center.X + Main.rand.Next(-10, 10), target.Center.Y + Main.rand.Next(-10, 10));
                Dust.NewDust(vector, 0, 0, ModContent.DustType<GlowLine>(), vector.DirectionTo(target.Center).X * 5, vector.DirectionTo(target.Center).Y * 5, 0, new Color(0.34f,0.78f,0.99f));
                if (Main.netMode == NetmodeID.SinglePlayer)
                {
                    HitInfo info = new HitInfo();
                    info.Damage = damageDone / 6 * 2 + Main.rand.Next(-1, 1);
                    
                    target.StrikeNPC(info, false, false);
                }
                else
                {
                    HitInfo info = new HitInfo();
                    info.Damage = damageDone / 6 * 2 + Main.rand.Next(-1, 1);
                    NetMessage.SendStrikeNPC(target, info, 0);
                }
                counter++;
            }
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.WoodenSword)
                .AddIngredient(ItemID.Cloud, 25)
                .AddIngredient(ItemID.FallenStar, 5)
                .Register();
        }
    }
}