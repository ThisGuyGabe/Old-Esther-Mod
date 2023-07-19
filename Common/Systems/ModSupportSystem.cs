using Terraria.ModLoader;

namespace EstherMod.Common.Systems
{
	public class ModSupportSystem : ModSystem
	{
		public override void PostSetupContent() 
		{
			CensusSupport();
		}

		private void CensusSupport() 
		{
			if (!ModLoader.TryGetMod("Census", out Mod Census)) {
				return;
			}
		}
	}
}
