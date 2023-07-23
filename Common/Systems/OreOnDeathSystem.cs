/*using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CascadeMod.Content.Tiles;

namespace CascadeMod.Common.Systems;

public sealed class OreOnDeathSystem : GlobalNPC {
	public static bool spawnHypnoplate = false;

	public sealed override bool AppliesToEntity(NPC entity, bool lateInstantiation) {
		return entity.type >= NPCID.EaterofWorldsHead && entity.type <= NPCID.EaterofWorldsTail || entity.type == NPCID.BrainofCthulhu;
	}

	public sealed override void OnKill(NPC npc) {
		if (!spawnHypnoplate && npc.boss) {
			SpawnHypnoplate();
			spawnHypnoplate = true;
		}
	}

	private static void SpawnHypnoplate() {
		for (int k = 0; k < (int)(Main.maxTilesX * Main.maxTilesY * 4E-05); k++) {
			int x = WorldGen.genRand.Next(0, Main.maxTilesX);

			int y = WorldGen.genRand.Next(0, (int)((int)Main.worldSurface * 0.35D));
			int snowCheckY = WorldGen.genRand.Next((int)(Main.worldSurface - 5), (int)(Main.worldSurface + 5));

			Tile snowCheck = Framing.GetTileSafely(x, snowCheckY);
			if (!(snowCheck.TileType == TileID.SnowBlock || snowCheck.TileType == TileID.IceBlock))
				WorldGen.TileRunner(x, y, WorldGen.genRand.Next(6, 8), WorldGen.genRand.Next(6, 8), ModContent.TileType<Hypnoplate>(), true);
		}
	}
}
*/