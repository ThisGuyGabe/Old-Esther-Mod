namespace EstherMod
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Terraria;
    using Terraria.Graphics.Effects;
    using Terraria.Graphics.Shaders;
    using Terraria.ID;
    using Terraria.ModLoader;
    using static Terraria.ModLoader.ModContent;
    using ReLogic.Content;

    public class EsthrMod : Mod
    {

        public override void Load()
        {
            if (Main.netMode != NetmodeID.Server)
            {
                Ref<Effect> screenRef = new Ref<Effect>(ModContent.Request<Effect>("EstherMod/Effects/CustomScreenShaders", AssetRequestMode.ImmediateLoad).Value);

                Filters.Scene["HydraShockwave"] = new Filter(new ScreenShaderData(screenRef, "HydraShockwave"), EffectPriority.VeryHigh);
                Filters.Scene["HydraShockwave"].Load();
                Filters.Scene["Confetti"] = new Filter(new ScreenShaderData(screenRef, "Confetti"), EffectPriority.VeryHigh);
                Filters.Scene["Confetti"].Load();

                Filters.Scene["UnderworldFilter"] = new Filter(new ScreenShaderData(screenRef, "UnderworldFilter"), EffectPriority.VeryHigh);
                Filters.Scene["UnderworldFilter"].Load();


                Filters.Scene["Shockwave"] = new Filter(new ScreenShaderData(screenRef, "Shockwave"), EffectPriority.VeryHigh);
                Filters.Scene["Shockwave"].Load();
            }
        }
    }
}