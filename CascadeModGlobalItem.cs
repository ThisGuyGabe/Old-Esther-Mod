using Terraria;
using Terraria.ModLoader;

namespace CascadeMod {
	public class CascadeModGlobalItem : GlobalItem {
		public override void SetDefaults(Item item) {
			item.autoReuse = true;
		}
	}
}
