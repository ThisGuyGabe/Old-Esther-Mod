using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.IO;
using Terraria.WorldBuilding;
using System;
using EstherMod.Content.Items.Placeable;
using static Terraria.ModLoader.ModContent;

namespace EstherMod.Content.Tiles
{
    public class Hypnoplate : ModTile
    {
        public override void SetStaticDefaults()
        {
            TileID.Sets.Ore[Type] = true;
            Main.tileSpelunker[Type] = true; // The tile will be affected by spelunker highlighting
            Main.tileOreFinderPriority[Type] = 410; // Metal Detector value, see https://terraria.gamepedia.com/Metal_Detector
            Main.tileShine2[Type] = true; // Modifies the draw color slightly.
            Main.tileShine[Type] = 1000; // How often tiny dust appear off this tile. Larger is less frequently
            Main.tileMergeDirt[Type] = true;
            Main.tileSolid[Type] = true;

            LocalizedText name = CreateMapEntryName();
            // name.SetDefault("Hypnoplate");
            AddMapEntry(new Color(53, 13, 82), name);

            DustType = DustID.Adamantite;
            ItemDrop = ItemType<HypnoplateItem>();
            HitSound = SoundID.Tink;
            // MineResist = 4f;
            MinPick = 65;
        }
    }

    public class ExampleOreSystem : ModSystem
    {
        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight)
        {
            // Because world generation is like layering several images ontop of each other, we need to do some steps between the original world generation steps.

            // The first step is an Ore. Most vanilla ores are generated in a step called "Shinies", so for maximum compatibility, we will also do this.
            // First, we find out which step "Shinies" is.
            int ShiniesIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Final Cleanup"));

            if (ShiniesIndex != -1)
            {
                // Next, we insert our pass directly after the original "Shinies" pass.
                // ExampleOrePass is a class seen bellow
                tasks.Insert(ShiniesIndex + 1, new HypnoplatePass("Placing Hypnoplate", 237.4298f));
            }
        }
    }

    public class HypnoplatePass : GenPass
    {
        public HypnoplatePass(string name, float loadWeight) : base(name, loadWeight)
        {
        }

        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
        {
            progress.Message = "Hypnoplating spawns upon the worlds skies.";

            // Ores are quite simple, we simply use a for loop and the WorldGen.TileRunner to place splotches of the specified Tile in the world.
            // "6E-05" is "scientific notation". It simply means 0.00006 but in some ways is easier to read.
            for (int k = 0; k < (int)(Main.maxTilesX * Main.maxTilesY * 4E-05); k++)
            {
                // The inside of this for loop corresponds to one single splotch of our Ore.
                // First, we randomly choose any coordinate in the world by choosing a random x and y value.
                int x = WorldGen.genRand.Next(0, Main.maxTilesX);

                // WorldGen.worldSurfaceLow is actually the highest surface tile. In practice you might want to use WorldGen.rockLayer or other WorldGen values.
                int y = WorldGen.genRand.Next(0, (int)((int)Main.worldSurface * 0.35D));
                int snowCheckY = WorldGen.genRand.Next((int)(Main.worldSurface - 5), (int)(Main.worldSurface + 5));

                // Then, we call WorldGen.TileRunner with random "strength" and random "steps", as well as the Tile we wish to place.
                // Feel free to experiment with strength and step to see the shape they generate.
                Tile snowCheck = Framing.GetTileSafely(x, snowCheckY);
                if (!(snowCheck.TileType == TileID.SnowBlock || snowCheck.TileType == TileID.IceBlock))
                    WorldGen.TileRunner(x, y, WorldGen.genRand.Next(6, 8), WorldGen.genRand.Next(6, 8), ModContent.TileType<Hypnoplate>(), true);
                //WorldGen.PlaceTile(x, y, ModContent.TileType<Hypnoplate>(), false, true);

                // Alternately, we could check the tile already present in the coordinate we are interested.
                // Wrapping WorldGen.TileRunner in the following condition would make the ore only generate in Snow.
                // Tile tile = Framing.GetTileSafely(x, y);
                // if (tile.HasTile && tile.TileType == TileID.SnowBlock) {
                // 	WorldGen.TileRunner(.....);
                // }
            }
        }
    }
}
