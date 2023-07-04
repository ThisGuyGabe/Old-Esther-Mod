using EstherMod.Content.Items;
using EstherMod.Core;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace EstherMod.Content.NPCs;

public class SpiritualConstruct : BaseNPC {
	public override void SetStaticDefaults() {
		//DisplayName.SetDefault("Spiritual Construct");

		Main.npcFrameCount[Type] = Main.npcFrameCount[NPCID.DemonEye];

		NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0) { // Influences how the NPC looks in the Bestiary
			Velocity = 1f // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
		};
		NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
	}

	public override void SetDefaults() {
		NPC.width = 28;
		NPC.height = 30;
		NPC.damage = 14;
		NPC.defense = 0;
		NPC.lifeMax = 70;
		NPC.HitSound = SoundID.NPCHit1;
		NPC.DeathSound = SoundID.NPCDeath1;
		NPC.value = 60f;
		NPC.knockBackResist = 0.5f;
		NPC.aiStyle = NPCAIStyleID.DemonEye;
		AnimationType = NPCID.DemonEye;
		AIType = NPCID.DemonEye;
		Banner = Item.NPCtoBanner(NPCID.DemonEye);
		BannerItem = Item.BannerToItem(Banner);
	}

	public override void AI() {
		base.AI();
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
			new FlavorTextBestiaryInfoElement("This type of zombie for some reason really likes to spread confetti around. Otherwise, it behaves just like a normal zombie.")

		});
	}
}
