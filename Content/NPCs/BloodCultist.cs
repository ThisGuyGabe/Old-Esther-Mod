using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;


namespace EstherMod.Content.NPCs
{
    [AutoloadHead]
    public class BloodCultist : ModNPC
    {
        /* Quest npc insane
        for esther mod */

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Blood Cultist");
        }
        public override void SetDefaults()
        {
            NPC.townNPC = true;
            NPC.friendly = true;
            NPC.width = 20;
            NPC.height = 20;
            NPC.aiStyle = 7;
            NPC.defense = 35;
            NPC.lifeMax = 350;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0.5f;
            Main.npcFrameCount[NPC.type] = 26;
            NPCID.Sets.ExtraFramesCount[NPC.type] = 0;
            NPCID.Sets.AttackFrameCount[NPC.type] = 1;
            NPCID.Sets.DangerDetectRange[NPC.type] = 450;
            NPCID.Sets.AttackType[NPC.type] = 1;
            NPCID.Sets.AttackTime[NPC.type] = 30;
            NPCID.Sets.AttackAverageChance[NPC.type] = 10;
            NPCID.Sets.HatOffsetY[NPC.type] = 4;
            AnimationType = 22;
        }



        public override bool CanTownNPCSpawn(int numTownNPCs)/* tModPorter Suggestion: Copy the implementation of NPC.SpawnAllowed_Merchant in vanilla if you to count money, and be sure to set a flag when unlocked, so you don't count every tick. */
        {
            if (NPC.downedBoss1)
            {
                return true;
            }
            return false;
        }

        public override List<string> SetNPCNameList()
        {
            return new List<string>()
            {
                "Bloodlust",
                "Hades",
                "Joker",
                "Hellord",
            };
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = "Task";
        }

        public override void OnChatButtonClicked(bool firstButton, ref string shopName)
        {
            Player player = Main.player[Main.myPlayer];
            if (firstButton)
            {

                Main.npcChatText = "Hey, My name is " + Main.npc[NPC.FindFirstNPC(NPC.type)].GivenName + " And I'm the Blood Cultist. If you get a quest item from enemies around the world bring it to me and I will reward you handsomely!";
                if (player.FindItem(Mod.Find<ModItem>("ArcaneLens").Type) != -1)
                {
                    player.inventory[player.FindItem(Mod.Find<ModItem>("ArcaneLens").Type)].stack--;
                    if (player.inventory[player.FindItem(Mod.Find<ModItem>("ArcaneLens").Type)].stack <= 0)
                    {
                        player.inventory[player.FindItem(Mod.Find<ModItem>("ArcaneLens").Type)] = new Item();
                    }
                    Main.npcChatText = "Have this bloody artifact. I have a bunch of them lying around.";
                    Main.npcChatCornerItem = Mod.Find<ModItem>("ArcaneLens").Type;
                    SoundEngine.PlaySound(SoundID.Chat);
                    player.QuickSpawnItem(Item.GetSource_None(), ItemID.BloodMoonStarter);
                }
                else
                {
                    if (player.FindItem(Mod.Find<ModItem>("ImpHorn").Type) != -1)
                    {
                        player.inventory[player.FindItem(Mod.Find<ModItem>("ImpHorn").Type)].stack--;
                        if (player.inventory[player.FindItem(Mod.Find<ModItem>("ImpHorn").Type)].stack <= 0)
                        {
                            player.inventory[player.FindItem(Mod.Find<ModItem>("ImpHorn").Type)] = new Item();
                        }
                        Main.npcChatText = "You might need these.";
                        Main.npcChatCornerItem = Mod.Find<ModItem>("ImpHorn").Type;
                        SoundEngine.PlaySound(SoundID.Chat);
                        player.QuickSpawnItem(Item.GetSource_NaturalSpawn(), ItemID.ObsidianSkinPotion, 5);
                    }
                }
            }
        }

        public override string GetChat()
        {
            if (NPCID.Dryad >= 0 && Main.rand.Next(6) == 0)
            {
                return Main.npc[NPC.FindFirstNPC(NPCID.Dryad)].GivenName + " makes me angry with her quest to purify the world, what's the point of it anyways.";
            }
            if (NPCID.ArmsDealer >= 0 && Main.rand.Next(6) == 0)
            {
                return "Hey could you tell " + Main.npc[NPC.FindFirstNPC(NPCID.ArmsDealer)].GivenName + " to scram? He keeps stealing my blood samples!";
            }
            switch (Main.rand.Next(4))
            {
                case 0:
                    return "Have you heard about the dungeon? Grim place even for me.";
                case 1:
                    return "You might be useful for me vermin.";
                default:
                    return "It was quite nice down in the temple, but I couldn't stand these lunatics.";
                case 2:
                    return "Why do I need all these items? Erm... i have a .. erm collecting habbit.";
            }
        }

        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
        {
            damage = 15;
            knockback = 2f;
        }

        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
        {
            cooldown = 5;
            randExtraCooldown = 10;
        }

        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
        {
            projType = Mod.Find<ModProjectile>("BonestromBolt").Type;
            attackDelay = 1;
        }

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {
            multiplier = 7f;
        }
    }
}