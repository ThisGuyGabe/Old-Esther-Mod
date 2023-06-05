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
            HitSound = SoundID.Tink;
            // MineResist = 4f;
            MinPick = 65;
        }
    }
}
