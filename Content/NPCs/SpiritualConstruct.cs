using EstherMod.Content.Items;
using EstherMod.Core;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using Microsoft.Xna.Framework;

namespace EstherMod.Content.NPCs;

public class SpiritualConstruct : BaseNPC 
{
	public int frame = 0;
	public double counting;
	public override void SetStaticDefaults() {
		//DisplayName.SetDefault("Spiritual Construct");

		Main.npcFrameCount[NPC.type] = 6;

		NPCID.Sets.NPCBestiaryDrawModifiers value = new(0) { // Influences how the NPC looks in the Bestiary
			Velocity = 1f // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
		};
		NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
	}

	public override void SetDefaults() {
		NPC.width = 68;
		NPC.height = 30;
		NPC.damage = 14;
		NPC.defense = 0;
		NPC.lifeMax = 70;
		NPC.HitSound = SoundID.NPCHit1;
		NPC.DeathSound = SoundID.NPCDeath1;
		NPC.value = 60f;
		NPC.knockBackResist = 0.5f;
		NPC.aiStyle = 0;
		AnimationType = NPCID.DemonEye;
		Banner = Item.NPCtoBanner(NPCID.DemonEye);
		BannerItem = Item.BannerToItem(Banner); // custom banner later on
		NPC.noTileCollide = true;
		NPC.noGravity = true;
		NPC.lavaImmune = false;
	}

	public override void AI() 
	{
		NPC.TargetClosest(true);
		Player target = Main.player[NPC.target];
		Vector2 ToPlayer = NPC.DirectionTo(target.Center) * 3;
		NPC.velocity = ToPlayer;

		NPC.ai[0] += 1f;
		if (NPC.ai[0] >= 30f) 
		{
			Projectile.NewProjectile(NPC.GetSource_FromAI(), target.Center, target.Center, ProjectileID.TerraBeam, 0, 0f, Main.myPlayer);
		}
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot) {
		npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<EthericScrap>(), 2)); // 50% Chance
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo) {
		return (float)(SpawnCondition.OverworldNightMonster.Chance * 0.2);
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry) {
		// We can use AddRange instead of calling Add multiple times in order to add multiple items at once
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
			// Sets the spawning conditions of this NPC that is listed in the bestiary.
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime,
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,

			// Sets the description of this NPC that is listed in the bestiary.
			new FlavorTextBestiaryInfoElement("Yeah someone else can put a description :3")

		});
	}
	public override void FindFrame(int frameHeight) 
	{
		if (frame == 0) 
		{
			counting += 1.0;
			if (counting < 8.0) 
			{
				NPC.frame.Y = 0;
			}
			else if (counting < 16.0) 
			{
				NPC.frame.Y = frameHeight;
			}
			else if (counting < 24.0) 
			{
				NPC.frame.Y = frameHeight * 2;
			}
			else if (counting < 32.0) 
			{
				NPC.frame.Y = frameHeight * 3;
			}
			else 
			{
				counting = 0.0;
			}
		}
		else if(frame == 1) 
		{
			NPC.frame.Y = frameHeight * 4;
		} else
		{
			NPC.frame.Y = frameHeight * 5;
		}
	}
}
