using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CascadeMod.Content.Buffs {
	public class Cooldown : ModBuff {
		public override void SetStaticDefaults() {
			Main.buffNoTimeDisplay[Type] = false;
			Main.debuff[Type] = true;
			BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
		}
	}
}
