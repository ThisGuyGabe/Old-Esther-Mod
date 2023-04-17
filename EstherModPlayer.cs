using Terraria;
using Terraria.ModLoader;
using Terraria.Graphics.Effects;

namespace EstherMod
{
    class EstherModPlayer : ModPlayer
    {
        public bool partyBombActive;
        public float bombStart;
        private float bombProgress;

        public override void PostUpdate()
        {
            if (partyBombActive)
            {
                if (bombProgress < 260)
                {
                    bombProgress = Main.GlobalTimeWrappedHourly - bombStart;
                    Filters.Scene["Confetti"].GetShader().UseProgress(bombProgress * 0.725f);
                }

            }
        }
    }
}