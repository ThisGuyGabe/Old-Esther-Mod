using Terraria.ModLoader;
using Terraria;
using EstherMod.Content.Projectiles.Summon;

namespace EstherMod.Content.Buffs;

public class GlockinatorBuff : ModBuff
{
    public override void SetStaticDefaults()
    {

        Main.buffNoSave[Type] = true;
        Main.buffNoTimeDisplay[Type] = true;
    }

    public override void Update(Player player, ref int buffIndex)
    {
        if (player.ownedProjectileCounts[ModContent.ProjectileType<GlockinatorSummon>()] > 0)
        {
            player.buffTime[buffIndex] = 18000;
        }
        else
        {
            player.DelBuff(buffIndex);
            buffIndex--;
        }
    }
}