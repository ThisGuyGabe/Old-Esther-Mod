/*using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria;
using CascadeMod.Backgrounds;
namespace CascadeMod.Common
{
    public class EstherMenu : ModMenu
	{
		private const string menuAssetPath = "CascadeMod/Assets/Textures/Menu";

		public override Asset<Texture2D> Logo => ModContent.Request<Texture2D>("CascadeMod/Assets/Textures/Logo");

		public override Asset<Texture2D> SunTexture => ModContent.Request<Texture2D>($"{menuAssetPath}/ExampleSun"); 

		public override Asset<Texture2D> MoonTexture => ModContent.Request<Texture2D>($"{menuAssetPath}/ExampliumMoon");

		public override int Music => MusicLoader.GetMusicSlot(Mod, "Assets/Music/EstherianMusic");

		public override ModSurfaceBackgroundStyle MenuBackgroundStyle => ModContent.GetInstance<CascadeModBackgroundStyle>();

		public override string DisplayName => "Esther";

		public override void OnSelected()
		{
			SoundEngine.PlaySound(SoundID.Thunder);
		}

		public override bool PreDrawLogo(SpriteBatch spriteBatch, ref Vector2 logoDrawCenter, ref float logoRotation, ref float logoScale, ref Color drawColor)
		{
			logoDrawCenter += new Vector2(0, 14);
			logoScale *= 1.45f;
			return true;
		}
	}
} */

