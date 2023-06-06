using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace EstherMod.Content.Items
{
	public class DayBreakStar : ModItem
	{
		public override void SetStaticDefaults()
		{

		}

		public override void SetDefaults()
		{
			Item.useStyle = 4;
			Item.useAnimation = 30;
			Item.useTime = 30;
			Item.rare = ItemRarityID.Pink;
			Item.width = 40;
			Item.height = 36;
			Item.UseSound = SoundID.Item4;
			Item.value = Item.sellPrice(gold: 4);
		}

        public override bool? UseItem(Player player)
		{
			if(player.itemTime == 0 && player.itemAnimation > 0)
			{
				player.itemTime = Item.useTime;
				if(Main.dayTime)
				{
					for (int i = 0; i < 50; i++)
					{
						Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
						Dust d = Dust.NewDustPerfect(Main.LocalPlayer.Top, DustID.BlueCrystalShard, speed * 5, Scale: 1.5f);
						d.noGravity = true;
					}
					Main.dayTime = false;
				}
				else
				{
					for (int i = 0; i < 50; i++)
					{
						Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
						Dust d = Dust.NewDustPerfect(Main.LocalPlayer.Top, DustID.YellowStarDust, speed * 5, Scale: 1.5f);
						d.noGravity = true;
					}
					Main.dayTime = true;
				}
				Main.time = 0.0;
			}
			return false;
		}
	}
}