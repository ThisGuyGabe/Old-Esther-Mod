using System.Collections.Generic;
using EstherMod.Content.Projectiles;
using EstherMod.Core;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EstherMod.Content.NPCs;

[AutoloadHead]
public sealed class BloodCultist : BaseNPC {
	public enum Menu {
		None,
		Quest,
		Fuse
	}
	public static Menu SelectedMenu { get; set; } = Menu.None;

	public override void SetStaticDefaults() {
		Main.npcFrameCount[NPC.type] = 26;
		NPCID.Sets.ExtraFramesCount[NPC.type] = 0;
		NPCID.Sets.AttackFrameCount[NPC.type] = 1;
		NPCID.Sets.DangerDetectRange[NPC.type] = 450;
		NPCID.Sets.AttackType[NPC.type] = 1;
		NPCID.Sets.AttackTime[NPC.type] = 30;
		NPCID.Sets.AttackAverageChance[NPC.type] = 10;
		NPCID.Sets.HatOffsetY[NPC.type] = 4;
	}

	public override void SetDefaults() {
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
		AnimationType = 22;
	}

	public override bool CanTownNPCSpawn(int numTownNPCs) => NPC.downedBoss1;

	public override List<string> SetNPCNameList() {
		return new List<string>() {
			"Bloodlust",
			"Hades",
			"Joker",
			"Hellord",
		};
	}

	public override void SetChatButtons(ref string button, ref string button2) {
		button = "Quests";
		button2 = "Fuse";
	}

	public override void OnChatButtonClicked(bool firstButton, ref string shopName) {
		if (firstButton) {
			BloodCultistUI.bloodCultistUi.SetState(BloodCultistUI.questUI);
			SelectedMenu = Menu.Quest;
		}
		else {
			BloodCultistUI.bloodCultistUi.SetState(BloodCultistUI.fuseUI);
			SelectedMenu = Menu.Fuse;
		}
	}

	public override string GetChat() {
		int armsDealerIndex = NPC.FindFirstNPC(NPCID.ArmsDealer);
		int dryadIndex = NPC.FindFirstNPC(NPCID.Dryad);

		if (dryadIndex != -1 && Main.rand.NextBool(6)) {
			return $"{Main.npc[dryadIndex].GivenName} makes me angry with her quest to purify the world, what's the point of it anyways.";
		}
		if (armsDealerIndex != -1 && Main.rand.NextBool(6)) {
			return $"Hey could you tell {Main.npc[armsDealerIndex].GivenName} to scram? He keeps stealing my blood samples!";
		}

		return Main.rand.Next(4) switch {
			0 => "Have you heard about the dungeon? Grim place even for me.",
			1 => "You might be useful for me vermin.",
			2 => "Why do I need all these items? Erm... i have a .. erm collecting habbit.",
			_ => "It was quite nice down in the temple, but I couldn't stand these lunatics.",
		};
	}

	public override void AI() {
		// Disables happiness button
		Main.LocalPlayer.currentShoppingSettings.HappinessReport = "";
	}

	public override void TownNPCAttackStrength(ref int damage, ref float knockback) {
		damage = 15;
		knockback = 2f;
	}

	public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown) {
		cooldown = 5;
		randExtraCooldown = 10;
	}

	public override void TownNPCAttackProj(ref int projType, ref int attackDelay) {
		projType = ModContent.ProjectileType<BonestromBolt>();
		attackDelay = 1;
	}

	public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset) {
		multiplier = 7f;
	}
}