using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EstherMod.Content.Buffs
{
    public class EchoBreakCooldown : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }
    }
}
