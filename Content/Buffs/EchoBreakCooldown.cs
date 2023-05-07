using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EstherMod.Content.Buffs
{
    public class EchoBreakCooldown : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Echo Break Cooldown");
            // Description.SetDefault("You can't use Echo Break now!");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true; //Add this so the nurse doesn't remove the buff when healing
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }
    }
}
