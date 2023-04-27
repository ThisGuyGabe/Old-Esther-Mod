using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace EstherMod
{
    public class ModGlobalNPC : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (npc.type == NPCID.CursedSkull)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Content.Items.SoulPebble>(), 2, 1, 2));
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Content.Items.SoulPearl>(), 5, 1, 1));
            }

            if (npc.type == NPCID.SkeletronHead)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Content.Items.SoulPearl>(), 1, 3, 3));
            }

            if (npc.type == NPCID.AngryBones)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Content.Items.SoulPebble>(), 2, 1, 2));
            }

            if (npc.type == NPCID.DarkCaster)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Content.Items.SoulPebble>(), 2, 1, 2));
            }

            if (npc.type == NPCID.DungeonSlime)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Content.Items.SoulPebble>(), 2, 1, 2));
            }
        }
    }
}
