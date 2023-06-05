using System.Collections;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.WorldBuilding;
using EstherMod.Content.Tiles;

namespace EstherMod.Common.Systems
{
	public class OreOnDeathSystem : GlobalNPC
	{
		public static bool spawnHypnoplate = false;

		public override void OnKill(NPC npc) {

			// Generates ore in the sky when Brain of Cthulhu is defeated.
			if (npc.type == NPCID.BrainofCthulhu && !NPC.downedBoss2) {

				if (!spawnHypnoplate) { // Spawns Hypno Plate in the world when Brain of Cthulu is defeated.
                    Main.NewText("Hypnoplating spawns upon the worlds skies.", 170, 0, 0);
                    for (int k = 0; k < (int)(Main.maxTilesX * Main.maxTilesY * 4E-05); k++)
            		{
                		int x = WorldGen.genRand.Next(0, Main.maxTilesX);

                		int y = WorldGen.genRand.Next(0, (int)((int)Main.worldSurface * 0.35D));
                		int snowCheckY = WorldGen.genRand.Next((int)(Main.worldSurface - 5), (int)(Main.worldSurface + 5));

                		Tile snowCheck = Framing.GetTileSafely(x, snowCheckY);
                		if (!(snowCheck.TileType == TileID.SnowBlock || snowCheck.TileType == TileID.IceBlock))
                   			WorldGen.TileRunner(x, y, WorldGen.genRand.Next(6, 8), WorldGen.genRand.Next(6, 8), ModContent.TileType<Hypnoplate>(), true);
            		}

                	spawnHypnoplate = true;
				}
			}

			// Generates ore in the sky when Eater of Worlds is suppose to die.
			if (npc.type >= NPCID.EaterofWorldsHead && npc.type <= NPCID.EaterofWorldsTail && !NPC.downedBoss2) {

				if (!spawnHypnoplate) { // Spawns Hypno Plate in the world when Brain of Cthulu is defeated.
                    Main.NewText("Hypnoplating spawns upon the worlds skies.", 170, 0, 170);
                    for (int k = 0; k < (int)(Main.maxTilesX * Main.maxTilesY * 4E-05); k++)
            		{
                		int x = WorldGen.genRand.Next(0, Main.maxTilesX);

                		int y = WorldGen.genRand.Next(0, (int)((int)Main.worldSurface * 0.35D));
                		int snowCheckY = WorldGen.genRand.Next((int)(Main.worldSurface - 5), (int)(Main.worldSurface + 5));

                		Tile snowCheck = Framing.GetTileSafely(x, snowCheckY);
                		if (!(snowCheck.TileType == TileID.SnowBlock || snowCheck.TileType == TileID.IceBlock))
                   			WorldGen.TileRunner(x, y, WorldGen.genRand.Next(6, 8), WorldGen.genRand.Next(6, 8), ModContent.TileType<Hypnoplate>(), true);
            		}
                	spawnHypnoplate = true;
				}
			}
		}
	}
}
