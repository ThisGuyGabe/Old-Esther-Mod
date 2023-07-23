using System;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using CascadeMod.Content.Buffs;
using Terraria.Audio;

namespace CascadeMod.Content.Projectiles.Summon
{
    public class GlockinatorSummon : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
            Main.projFrames[Projectile.type] = 2;
            Main.projPet[Projectile.type] = true;

            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 15;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }

        public sealed override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 22;
            Projectile.tileCollide = false;
            Projectile.friendly = false;
            Projectile.hostile = false;
            Projectile.minion = true;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.minionSlots = 1f;
            Projectile.penetrate = -1;
        }
        public override bool? CanCutTiles()
        {
            return false;
        }
        public override bool MinionContactDamage()
        {
            return true;
        }
        public override void AI()
        {
            Projectile.ai[0]++;
            Player owner = Main.player[Projectile.owner];
            if (!CheckActive(owner))
            {
                return;
            }
            GeneralBehavior(owner);
            SearchForTargets(owner, out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter, out NPC target);
            Movement(owner, foundTarget, targetCenter);
            Visuals();
        }

        private void GeneralBehavior(Player owner)
        {
            Projectile.Center = owner.Center + new Vector2(0, -35);
        }
        private bool CheckActive(Player owner)
        {
            if (owner.dead || !owner.active)
            {
                owner.ClearBuff(ModContent.BuffType<GlockinatorBuff>());

                return false;
            }

            if (owner.HasBuff(ModContent.BuffType<GlockinatorBuff>()))
            {
                Projectile.timeLeft = 2;
            }

            return true;
        }
        private void SearchForTargets(Player owner, out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter, out NPC target)
        {
            distanceFromTarget = 700f;
            targetCenter = Projectile.position;
            foundTarget = false;

            if (owner.HasMinionAttackTargetNPC)
            {
                target = Main.npc[owner.MinionAttackTargetNPC];
                float between = Vector2.Distance(target.Center, Projectile.Center);
                if (between < 2000f)
                {
                    distanceFromTarget = between;
                    targetCenter = target.Center;
                    foundTarget = true;
                }
            }

            if (!foundTarget)
            {
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    target = Main.npc[i];

                    if (target.CanBeChasedBy())
                    {
                        float between = Vector2.Distance(target.Center, Projectile.Center);
                        bool closest = Vector2.Distance(Projectile.Center, targetCenter) > between;
                        bool inRange = between < distanceFromTarget;
                        bool lineOfSight = Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, target.position, target.width, target.height);
                        bool closeThroughWall = between < 100f;

                        if (((closest && inRange) || !foundTarget) && (lineOfSight || closeThroughWall))
                        {
                            distanceFromTarget = between;
                            targetCenter = target.Center;
                            foundTarget = true;
                        }
                    }
                }
            }
            target = null;

        }

        private void Movement(Player owner, bool foundTarget, Vector2 targetCenter)
        {
            if (foundTarget)
            {
                if (Projectile.ai[0] % 20 != 10)
                {
                    Projectile.rotation = (Projectile.rotation * 4 + Projectile.Center.DirectionTo(targetCenter).ToRotation()) / 5;
                }
                if (Projectile.ai[0] % 20 == 10)
                {
                    SoundEngine.PlaySound(SoundID.Item11, Projectile.Center);
                    Projectile.rotation = Projectile.Center.DirectionTo(targetCenter).ToRotation();
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Projectile.rotation.ToRotationVector2() * 20, ProjectileID.Bullet, Projectile.damage, 2);
                    Projectile.Center = Projectile.Center + (Projectile.Center.DirectionTo(targetCenter) * -3);
                    if (Projectile.frame == 0)
                    {
                        Projectile.rotation -= 1.570796f;
                                               
                    }
                    if (Projectile.frame == 1)
                    {
                        Projectile.rotation += 1.570796f;
                    }
                }
            }
            else
            {
                Projectile.rotation += 0.01f;
            }
        }
        private void Visuals()
        {
            Lighting.AddLight(Projectile.Center, Color.Orange.ToVector3() * (float)((Math.Cos(Projectile.ai[0] * 5) * 0.5) + 0.5));
            Projectile.frame = 0;
            float degrees = (float)(Projectile.rotation * 180 / Math.PI);
            if (degrees > 90) { Projectile.frame = 0; }
            if (degrees < -90) { Projectile.frame = 1; }
        }
    }
}
