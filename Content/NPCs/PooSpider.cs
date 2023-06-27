using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace EstherMod.Content.NPCs {
	public class PooSpider : ModNPC {
		public override void SetStaticDefaults() {
			Main.npcFrameCount[Type] = 5;
			Main.npcCatchable[Type] = true;
			NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, new(0) {
				Velocity = 1f
			});
		}

		public override void SetDefaults() {
			NPC.CloneDefaults(NPCID.Bunny);
			NPC.catchItem = (short)ModContent.ItemType<PooSpiderItem>();
			NPC.friendly = true;
			AIType = NPCID.Bunny;
		}

		public override void FindFrame(int frameHeight) {
			if (Math.Abs(NPC.velocity.X) < 1f) {
				NPC.frame.Y = 0;
				return;
			}
			NPC.frameCounter += 1;
			if (NPC.frameCounter >= 8) {
				NPC.frame.Y += frameHeight;
				if (NPC.frame.Y >= NPC.frame.Height * frameHeight) {
					NPC.frame.Y = 0;
				}
				NPC.frameCounter = 0;
			}
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo) {
			return (float)(SpawnCondition.OverworldDayGrassCritter.Chance * 0.4);
		}
	}
	public class PooSpiderItem : ModItem {
		public override void SetStaticDefaults() => Item.ResearchUnlockCount = 5;

		public override void SetDefaults() {
			Item.useStyle = ItemUseStyleID.Swing;
			Item.autoReuse = true;
			Item.useTurn = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.maxStack = 999;
			Item.consumable = true;
			Item.width = 12;
			Item.height = 12;
			Item.noUseGraphic = true;

			Item.makeNPC = (short)ModContent.NPCType<PooSpider>();
		}
	}
}
