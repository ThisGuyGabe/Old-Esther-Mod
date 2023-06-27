using System;
using System.Collections.Generic;
using System.Reflection;
using EstherMod.Content.NPCs;
using EstherMod.Core.Fusions;
using EstherMod.Core.Quests;
using EstherMod.Core.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.UI.Chat;

namespace EstherMod.Core;

public sealed class BloodCultistUI : ILoadable {
	private sealed class UpdateUIsSystem : ModSystem {
		public override void UpdateUI(GameTime gameTime) {
			bloodCultistUi?.Update(gameTime);
		}

		public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers) {
			var npcSignsDialogIndex = layers.FindIndex(layer => layer.Name == "Vanilla: NPC / Sign Dialog");
			if (npcSignsDialogIndex != -1) {
				layers.Insert(npcSignsDialogIndex, new LegacyGameInterfaceLayer(
					"EstherMod/Blood Cultist UI",
					delegate {
						if (Main.LocalPlayer.TalkNPC is NPC { ModNPC: BloodCultist } && bloodCultistUi?.CurrentState != null) {
							bloodCultistUi.Draw(Main.spriteBatch, new GameTime());
						}
						return true;
					},
					InterfaceScaleType.UI));
			}
		}
	}
	public sealed class ChatBoxPanel : UIPanel {
		public int AmountOfLines { get; set; }

		public ChatBoxPanel(int amountOfLines) {
			AmountOfLines = amountOfLines;

			Width = StyleDimension.FromPixels(500f);
			Height = StyleDimension.FromPixels((AmountOfLines + 1) * 30f);
			MarginTop = 100f;
			HAlign = 0.5f;

			OnUpdate += (UIElement element) => {
				if (element.Height.Pixels != (AmountOfLines + 1) * 30f) {
					element.Height.Pixels = (AmountOfLines + 1) * 30f;
					element.Recalculate();
				}
			};
		}
	}

	public sealed class QuestUI : UIState {
		private const int DefaultAmountOfLines = 10;

		public int AmountOfLines {
			get => panel.AmountOfLines;
			set => panel.AmountOfLines = Math.Max(value, DefaultAmountOfLines);
		}

		private readonly ChatBoxPanel panel = new(DefaultAmountOfLines);
		private UITextWScrollbar descriptionText;
		private UIScrollbar questScrollbar;
		private QuestElement currentQuestChosen;

		private UIPanel takeQuestPanel;
		private UIText takeQuestText;
		private UIPanel rewardsPanel;
		private UITextWScrollbar rewardsText;

		public override void OnInitialize() {
			Append(panel);

			InitDescriptionPanel();
			InitRewardsPanel();
			InitTakeQuestPanel();

			var questPanel = new UIPanel() {
				Width = StyleDimension.FromPixelsAndPercent(-5f, 0.5f),
				Height = StyleDimension.FromPercent(0.925f),
				PaddingTop = 5f
			};
			var questList = new UIList() {
				Width = StyleDimension.Fill,
				Height = StyleDimension.Fill,
				ListPadding = 5f
			};
			questScrollbar = new UIScrollbar {
				Width = StyleDimension.FromPixels(20f),
				Height = StyleDimension.Fill,
				Left = StyleDimension.FromPixels(15f),
				HAlign = 1f,
				VAlign = 1f
			};
			questScrollbar.SetView(100f, 1000f);
			questList.SetScrollbar(questScrollbar);

			var quests = new QuestElement[QuestSystem.quests.Count];
			foreach (var quest in QuestSystem.quests) {
				var questElement = new QuestElement(quest) {
					Width = StyleDimension.Fill,
					Height = StyleDimension.FromPixels(100f),
					Left = StyleDimension.FromPixels(-2f)
				};
				questElement.OnLeftClick += (UIMouseEvent evt, UIElement listeningElement) => {
					var questElement = listeningElement as QuestElement;
					currentQuestChosen = questElement;

					questScrollbar.SetView(100f, 1000f);

					SoundEngine.PlaySound(SoundID.MenuOpen);
					if (!questElement.locked) {
						descriptionText.SetText(
							questElement.quest.Description
							.FormatWith(new { PlayerName = Main.LocalPlayer.name, WorldName = Main.worldName })
						);
					}
					else {
						descriptionText.SetText("???");
					}

					if (rewardsPanel.Parent == null) {
						panel.Append(rewardsPanel);
						panel.Append(takeQuestPanel);
					}
				};
				questList.Add(questElement);
			}

			questPanel.Append(questList);
			questPanel.Append(questScrollbar);
			panel.Append(questPanel);
		}

		private void InitDescriptionPanel() {
			var descriptionPanel = new UIPanel() {
				Width = StyleDimension.FromPixelsAndPercent(-5f, 0.5f),
				Height = StyleDimension.FromPixelsAndPercent(-5f, 0.5f),
				HAlign = 1f
			};
			descriptionText = new UITextWScrollbar("No quest chosen!", textScale: 0.75f) {
				Width = StyleDimension.Fill,
				Height = StyleDimension.Fill,
				Left = StyleDimension.FromPixels(-2.5f),
				WrappedTextBottomPadding = 7.5f,
				IsWrapped = true
			};
			var descriptionScrollbar = new UIScrollbar {
				Width = StyleDimension.FromPixels(20f),
				Height = StyleDimension.Fill,
				Left = StyleDimension.FromPixels(15f),
				HAlign = 1f,
				VAlign = 1f
			};
			descriptionScrollbar.SetView(100f, 1000f);

			descriptionText.SetScrollbar(descriptionScrollbar);
			descriptionPanel.Append(descriptionText);
			descriptionPanel.Append(descriptionScrollbar);
			panel.Append(descriptionPanel);
		}

		private void InitRewardsPanel() {
			rewardsPanel = new UIPanel() {
				Width = StyleDimension.FromPixelsAndPercent(-5f, 0.5f),
				Height = StyleDimension.FromPercent(0.3f),
				HAlign = 1f,
				VAlign = 0.75f,
			};
			rewardsText = new UITextWScrollbar(string.Empty, 0.75f) {
				Width = StyleDimension.Fill,
				Height = StyleDimension.Fill,
				Left = StyleDimension.FromPixels(-2.5f),
				WrappedTextBottomPadding = 7.5f,
				IsWrapped = true
			};
			rewardsText.OnUpdate += (UIElement affectedElement) => {
				if (!currentQuestChosen.locked) {
					string[] rewardTexts = new string[currentQuestChosen.quest.Rewards.Count];
					for (int i = 0; i < currentQuestChosen.quest.Rewards.Count; i++) {
						rewardTexts[i] = Language.GetTextValue("Mods.EstherMod.EstherMod.Quests.RewardPrefix", currentQuestChosen.quest.Rewards[i].Text);
					}
					rewardsText.SetText(string.Join('\n', rewardTexts));
				}
				else {
					rewardsText.SetText("???");
				}
			};
			var rewardsScrollbar = new UIScrollbar {
				Width = StyleDimension.FromPixels(20f),
				Height = StyleDimension.Fill,
				Left = StyleDimension.FromPixels(15f),
				HAlign = 1f,
				VAlign = 1f
			};
			rewardsText.SetScrollbar(rewardsScrollbar);
			rewardsPanel.Append(rewardsScrollbar);
			rewardsPanel.Append(rewardsText);
		}

		private void InitTakeQuestPanel() {
			takeQuestPanel = new UIPanel() {
				Width = StyleDimension.FromPixelsAndPercent(-5f, 0.5f),
				Height = StyleDimension.FromPercent(0.1f),
				HAlign = 1f,
				VAlign = 0.925f,
			};
			takeQuestPanel.OnLeftClick += (UIMouseEvent evt, UIElement listeningElement) => {
				if (currentQuestChosen != null && Main.LocalPlayer.TryGetModPlayer(out QuestPlayer questPlayer)) {
					if (currentQuestChosen.quest.IsCompleted(Main.LocalPlayer)) {
						foreach (var reward in currentQuestChosen.quest.Rewards) {
							reward.Grant(Main.LocalPlayer);
						}
						questPlayer.CompletedQuests2.Add(currentQuestChosen.quest.FullName);

						if (Main.netMode == NetmodeID.MultiplayerClient) {
							Esther.Instance.Packet_ClaimQuestRewards(Main.myPlayer, currentQuestChosen.quest.FullName);
						}
					}
					else {
						bool value = currentQuestChosen.quest.TryAssign(Main.LocalPlayer);
						if (value) {
							Main.NewText($"Took '{currentQuestChosen.quest.DisplayName.Value}' quest!", 255, 255, 200);
						}
						else if (currentQuestChosen.locked) {
							Main.NewText("This quest is locked!", 255, 255, 200);
						}
						else {
							Main.NewText("You can't take any more quests!", 255, 255, 200);
						}
					}
				}
			};
			takeQuestText = new UIText(string.Empty) {
				Width = StyleDimension.Fill,
				Height = StyleDimension.Fill,
				HAlign = 0.5f,
				VAlign = 0.5f
			};
			takeQuestText.OnUpdate += (UIElement affectedElement) => {
				if (currentQuestChosen != null) {
					takeQuestText.TextColor = Color.White;

					if (currentQuestChosen.locked) {
						takeQuestText.SetText("???");
						return;
					}
					if (currentQuestChosen.quest.IsCompleted(Main.LocalPlayer) && Main.LocalPlayer.TryGetModPlayer(out QuestPlayer questPlayer)) {
						if (!questPlayer.CompletedQuests2.Contains(currentQuestChosen.quest.FullName)) {
							takeQuestText.SetText(Language.GetText("Mods.EstherMod.EstherMod.Quests.TakeReward"));
						}
						else {
							takeQuestText.SetText(Language.GetText("Mods.EstherMod.EstherMod.Quests.QuestCompleted"));
							takeQuestText.TextColor = Color.Yellow;
						}
					}
					else if (currentQuestChosen.quest.Ordinal != -1) {
						takeQuestText.SetText(Language.GetText("Mods.EstherMod.EstherMod.Quests.InProgress"));
					}
					else {
						takeQuestText.SetText(Language.GetText("Mods.EstherMod.EstherMod.Quests.TakeQuest"));
					}
				}
			};
			takeQuestText.OnMouseOver += (_, _) => SoundEngine.PlaySound(SoundID.MenuTick);
			takeQuestPanel.Append(takeQuestText);
		}
	}
	public sealed class FuseUI : UIState {
		private const int DefaultAmountOfLines = 10;

		public int AmountOfLines {
			get => panel.AmountOfLines;
			set => panel.AmountOfLines = Math.Max(value, DefaultAmountOfLines);
		}

		private readonly ChatBoxPanel panel = new(DefaultAmountOfLines);

		private UIConfigurableItemSlot mainSlot; // Item 1
		private UIConfigurableItemSlot secondSlot; // Item 2
		private UIConfigurableItemSlot resultSlot; // Result

		public override void OnInitialize() {
			Append(panel);

			mainSlot = new UIConfigurableItemSlot(new Item(), ItemSlot.Context.InventoryItem, new UIConfigurableItemSlot.Options(true, false, false)) {
				HAlign = 0.1f,
				VAlign = 0.25f,
			};
			mainSlot.OnMouseOver += (_, _) => SoundEngine.PlaySound(SoundID.MenuTick);
			panel.Append(mainSlot);

			secondSlot = new UIConfigurableItemSlot(new Item(), ItemSlot.Context.InventoryItem, new UIConfigurableItemSlot.Options(true, false, false)) {
				HAlign = 0.1f,
				VAlign = 0.75f,
			};
			secondSlot.OnMouseOver += (_, _) => SoundEngine.PlaySound(SoundID.MenuTick);
			panel.Append(secondSlot);

			resultSlot = new UIConfigurableItemSlot(new Item(), ItemSlot.Context.InventoryItem, new UIConfigurableItemSlot.Options(false, false, false)) {
				Left = StyleDimension.FromPercent(0.75f),
				VAlign = 0.5f,
			};
			resultSlot.OnMouseOver += (_, _) => SoundEngine.PlaySound(SoundID.MenuTick);
			resultSlot.OnUpdate += (UIElement affectedElement) => {
				var rightSlot = affectedElement as UIConfigurableItemSlot;

				bool anyAir = false;
				anyAir |= mainSlot.Item.IsAir;
				anyAir |= secondSlot.Item.IsAir;
				if (anyAir) {
					rightSlot.Item.TurnToAir();
				}
			};
			panel.Append(resultSlot);

			var fusionIcon = new UIImage(ModContent.Request<Texture2D>("EstherMod/Assets/FusionIcon", AssetRequestMode.ImmediateLoad)) {
				HAlign = 0.5f,
				VAlign = 0.5f
			};
			fusionIcon.OnMouseOver += (_, _) => SoundEngine.PlaySound(SoundID.MenuTick);
			fusionIcon.OnLeftClick += (UIMouseEvent evt, UIElement listeningElement) => {
				if (Main.HoverItem.IsAir && !mainSlot.Item.IsAir && !secondSlot.Item.IsAir && !resultSlot.Item.IsAir) {
					var item = resultSlot.Item;
					Utils.Swap(ref Main.mouseItem, ref item);
					resultSlot.Item = item;

					mainSlot.Item.TurnToAir();
					secondSlot.Item.TurnToAir();
				}
			};
			panel.Append(fusionIcon);
		}

		public override void Update(GameTime gameTime) {
			Main.playerInventory = true;

			base.Update(gameTime);

			if (mainSlot.Item.IsAir && secondSlot.Item.IsAir && !resultSlot.Item.IsAir) {
				resultSlot.Item.TurnToAir();
			}
			else if (mainSlot.Item.IsAir && !secondSlot.Item.IsAir && resultSlot.Item.IsAir) {
				if (FusionDatabase.ItemUsages.TryGetValue(secondSlot.Item.type, out var usages) && usages.Count != 0) {
					var fusion = usages[0];
					if (resultSlot.Item.type != fusion.Result) {
						resultSlot.Item.SetDefaults(fusion.Result);
						resultSlot.Color = Color.White * 0.5f;
					}
				}
			}
			else if (!mainSlot.Item.IsAir && secondSlot.Item.IsAir && resultSlot.Item.IsAir) {
				if (FusionDatabase.ItemUsages.TryGetValue(mainSlot.Item.type, out var usages) && usages.Count != 0) {
					var fusion = usages[0];
					if (resultSlot.Item.type != fusion.Result) {
						resultSlot.Item.SetDefaults(fusion.Result);
						resultSlot.Color = Color.White * 0.5f;
					}
				}
			}
			if (!mainSlot.Item.IsAir && !secondSlot.Item.IsAir) {
				var fusion = FusionDatabase.FusionsByMaterials[mainSlot.Item.type, secondSlot.Item.type] ?? FusionDatabase.FusionsByMaterials[secondSlot.Item.type, mainSlot.Item.type];
				if (fusion != null && resultSlot.Item.type != fusion.Result) {
					resultSlot.Item.SetDefaults(fusion.Result);
					resultSlot.Color = Color.White;
				}
			}

			resultSlot.Option.OverrideHover = !resultSlot.Item.IsAir;
		}

		public override void OnDeactivate() {
			if (!mainSlot.Item.IsAir)
				Main.LocalPlayer.QuickSpawnItem(null, mainSlot.Item, mainSlot.Item.stack);
			if (!secondSlot.Item.IsAir)
				Main.LocalPlayer.QuickSpawnItem(null, secondSlot.Item, secondSlot.Item.stack);
		}
	}

	public static UserInterface bloodCultistUi;

	public void Load(Mod mod) {
		if (Main.dedServ) return;

		IL_Main.GUIChatDrawInner += GUIChatDrawInnerPatch;

		bloodCultistUi = new();
	}

	public void Unload() {
		if (Main.dedServ) return;

		IL_Main.GUIChatDrawInner -= GUIChatDrawInnerPatch;

		bloodCultistUi = null;
	}

	/*
		C#:
			int amountOfLines = _textDisplayCache.AmountOfLines;
			bool flag2 = false;
			if (editSign) {
				textBlinkerCount++;
				if (textBlinkerCount >= 20) {
					if (textBlinkerState == 0) {
						textBlinkerState = 1;
					}
					else {
						textBlinkerState = 0;
					}
					textBlinkerCount = 0;
				}
				if (textBlinkerState == 1) {
					flag2 = true;
					textLines[amountOfLines - 1].Add(new TextSnippet("|", Color.White));
				}
				instance.DrawWindowsIMEPanel(new Vector2(screenWidth / 2, 90f), 0.5f);
			}
		[+]	bool skipVanillaDrawing = false;
		[+]	int oldAmountOfLines = amountOfLines;
		[+]	if (Main.LocalNPC.TalkNPC is NPC { ModNPC: BloodCultist })
		[+]	{
		[+]		if (BloodCultist.SelectedMode != BloodCultist.Menu.None) {
		[+]			if (bloodCultistQuestUi?.CurrentState == null) {
		[+]				bloodCultistQuestState.AmountOfLines = amountOfLines;
		[+]				bloodCultistFuseState.AmountOfLines = amountOfLines;
		[+]				amountOfLines = bloodCultistQuestState.AmountOfLines - 1;
		[+]			}
		[+]			skipVanillaDrawing = true;
		[+]		}
		[+] }
		[+]	else  {
		[+]		BloodCultist.SelectedMode = BloodCultist.Menu.None;
		[+]	}
		[+]	if (skipVanillaDrawing) {
				spriteBatch.Draw(TextureAssets.ChatBack.Value, new Vector2(screenWidth / 2 - TextureAssets.ChatBack.Width() / 2, 100f), new Rectangle(0, 0, TextureAssets.ChatBack.Width(), (amountOfLines + 1) * 30), color, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
				spriteBatch.Draw(TextureAssets.ChatBack.Value, new Vector2(screenWidth / 2 - TextureAssets.ChatBack.Width() / 2, 100 + (amountOfLines + 1) * 30), new Rectangle(0, TextureAssets.ChatBack.Height() - 30, TextureAssets.ChatBack.Width(), 30), color, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
		[+]	}
			TextSnippet hoveredSnippet = null;
			for (int i = 0; i < amountOfLines; i++) {
				List<TextSnippet> text = textLines[i];
				int hoveredSnippetNum;
		[+]		if (!skipVanillaDrawing) {
					ChatManager.DrawColorCodedStringWithShadow(spriteBatch, FontAssets.MouseText.Value, text.ToArray(), new Vector2(170 + (screenWidth - 800) / 2, 120 + i * 30), 0f, color2, Color.Black, Vector2.Zero, Vector2.One, out hoveredSnippetNum);
		[+]		}
		[+]		else {
		[+]			hoveredSnippetNum = -1;
		[+]		}
				if (hoveredSnippetNum > -1) {
					hoveredSnippet = text[hoveredSnippetNum];
				}
			}
		IL:
			// int amountOfLines = _textDisplayCache.AmountOfLines;
			IL_01f4: ldarg.0
			IL_01f5: ldfld class Terraria.Main/TextDisplayCache Terraria.Main::_textDisplayCache
			IL_01fa: callvirt instance int32 Terraria.Main/TextDisplayCache::get_AmountOfLines()
			IL_01ff: stloc.s 6

			// bool flag2 = false;
			IL_0201: ldc.i4.0
			IL_0202: stloc.s 7

			// if (editSign)
			IL_0204: ldsfld bool Terraria.Main::editSign
			IL_0209: brfalse IL_0296

			// textBlinkerCount++;
			IL_020e: ldarg.0
			IL_020f: ldarg.0
			IL_0210: ldfld int32 Terraria.Main::textBlinkerCount
			IL_0215: ldc.i4.1
			IL_0216: add
			IL_0217: stfld int32 Terraria.Main::textBlinkerCount

			// if (textBlinkerCount >= 20)
			IL_021c: ldarg.0
			IL_021d: ldfld int32 Terraria.Main::textBlinkerCount
			IL_0222: ldc.i4.s 20
			IL_0224: blt.s IL_0245

			// if (textBlinkerState == 0)
			IL_0226: ldarg.0
			IL_0227: ldfld int32 Terraria.Main::textBlinkerState
			IL_022c: brtrue.s IL_0237

			// textBlinkerState = 1;
			IL_022e: ldarg.0
			IL_022f: ldc.i4.1
			IL_0230: stfld int32 Terraria.Main::textBlinkerState

			// textBlinkerState = 0;
			IL_0235: br.s IL_023e
			IL_0237: ldarg.0
			IL_0238: ldc.i4.0
			IL_0239: stfld int32 Terraria.Main::textBlinkerState

			// textBlinkerCount = 0;
			IL_023e: ldarg.0
			IL_023f: ldc.i4.0
			IL_0240: stfld int32 Terraria.Main::textBlinkerCount

			// if (textBlinkerState == 1)
			IL_0245: ldarg.0
			IL_0246: ldfld int32 Terraria.Main::textBlinkerState
			IL_024b: ldc.i4.1
			IL_024c: bne.un.s IL_0275

			// flag2 = true;
			IL_024e: ldc.i4.1
			IL_024f: stloc.s 7

			// textLines[amountOfLines - 1].Add(new TextSnippet("|", Color.White));
			IL_0251: ldloc.s 5
			IL_0253: ldloc.s 6
			IL_0255: ldc.i4.1
			IL_0256: sub
			IL_0257: callvirt instance !0 class [System.Collections]System.Collections.Generic.List`1<class [System.Collections]System.Collections.Generic.List`1<class Terraria.UI.Chat.TextSnippet>>::get_Item(int32)
			IL_025c: ldstr "|"
			IL_0261: call valuetype [FNA]Microsoft.Xna.Framework.Color [FNA]Microsoft.Xna.Framework.Color::get_White()
			IL_0266: ldc.r4 1
			IL_026b: newobj instance void Terraria.UI.Chat.TextSnippet::.ctor(string, valuetype [FNA]Microsoft.Xna.Framework.Color, float32)
			IL_0270: callvirt instance void class [System.Collections]System.Collections.Generic.List`1<class Terraria.UI.Chat.TextSnippet>::Add(!0)

			// instance.DrawWindowsIMEPanel(new Vector2(screenWidth / 2, 90f), 0.5f);
			IL_0275: ldsfld class Terraria.Main Terraria.Main::'instance'
			IL_027a: ldsfld int32 Terraria.Main::screenWidth
			IL_027f: ldc.i4.2
			IL_0280: div
			IL_0281: conv.r4
			IL_0282: ldc.r4 90
			IL_0287: newobj instance void [FNA]Microsoft.Xna.Framework.Vector2::.ctor(float32, float32)
			IL_028c: ldc.r4 0.5
			IL_0291: callvirt instance void Terraria.Main::DrawWindowsIMEPanel(valuetype [FNA]Microsoft.Xna.Framework.Vector2, float32)

			// (no C# code)
			IL_0296: ldsfld class [FNA]Microsoft.Xna.Framework.Graphics.SpriteBatch Terraria.Main::spriteBatch
			IL_0000: pop

			// bool skipVanillaDrawing = false;
		[+]	IL_0000: ldc.i4.0
		[+]	IL_0000: stloc skipVanillaDrawing

		[+]	IL_0000: ldloca skipVanillaDrawing
		[+]	IL_0000: ldloca oldAmountOfLines
		[+]	IL_0000: ldloca amountOfLines
		[+]	// delegate call

		[+]	IL_0000: ldloc skipVanillaDrawing
		[+]	IL_0000: brtrue.s skipVanillaBoxDrawingLabel

			// spriteBatch.Draw(TextureAssets.ChatBack.Value, new Vector2(screenWidth / 2 - TextureAssets.ChatBack.Width() / 2, 100f), new Rectangle(0, 0, TextureAssets.ChatBack.Width(), (amountOfLines + 1) * 30), color, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
		[+]	IL_0000: ldsfld class [FNA]Microsoft.Xna.Framework.Graphics.SpriteBatch Terraria.Main::spriteBatch
			IL_029b: ldsfld class [ReLogic]ReLogic.Content.Asset`1<class [FNA]Microsoft.Xna.Framework.Graphics.Texture2D> Terraria.GameContent.TextureAssets::ChatBack
			IL_02a0: callvirt instance !0 class [ReLogic]ReLogic.Content.Asset`1<class [FNA]Microsoft.Xna.Framework.Graphics.Texture2D>::get_Value()
			IL_02a5: ldsfld int32 Terraria.Main::screenWidth
			IL_02aa: ldc.i4.2
			IL_02ab: div
			IL_02ac: ldsfld class [ReLogic]ReLogic.Content.Asset`1<class [FNA]Microsoft.Xna.Framework.Graphics.Texture2D> Terraria.GameContent.TextureAssets::ChatBack
			IL_02b1: call int32 Terraria.Utils::Width(class [ReLogic]ReLogic.Content.Asset`1<class [FNA]Microsoft.Xna.Framework.Graphics.Texture2D>)
			IL_02b6: ldc.i4.2
			IL_02b7: div
			IL_02b8: sub
			IL_02b9: conv.r4
			IL_02ba: ldc.r4 100
			IL_02bf: newobj instance void [FNA]Microsoft.Xna.Framework.Vector2::.ctor(float32, float32)
			IL_02c4: ldc.i4.0
			IL_02c5: ldc.i4.0
			IL_02c6: ldsfld class [ReLogic]ReLogic.Content.Asset`1<class [FNA]Microsoft.Xna.Framework.Graphics.Texture2D> Terraria.GameContent.TextureAssets::ChatBack
			IL_02cb: call int32 Terraria.Utils::Width(class [ReLogic]ReLogic.Content.Asset`1<class [FNA]Microsoft.Xna.Framework.Graphics.Texture2D>)
			IL_02d0: ldloc.s 6
			IL_02d2: ldc.i4.1
			IL_02d3: add
			IL_02d4: ldc.i4.s 30
			IL_02d6: mul
			IL_02d7: newobj instance void [FNA]Microsoft.Xna.Framework.Rectangle::.ctor(int32, int32, int32, int32)
			IL_02dc: newobj instance void valuetype [System.Runtime]System.Nullable`1<valuetype [FNA]Microsoft.Xna.Framework.Rectangle>::.ctor(!0)
			IL_02e1: ldloc.0
			IL_02e2: ldc.r4 0.0
			IL_02e7: ldloca.s 20
			IL_02e9: initobj [FNA]Microsoft.Xna.Framework.Vector2
			IL_02ef: ldloc.s 20
			IL_02f1: ldc.r4 1
			IL_02f6: ldc.i4.0
			IL_02f7: ldc.r4 0.0

			// spriteBatch.Draw(TextureAssets.ChatBack.Value, new Vector2(screenWidth / 2 - TextureAssets.ChatBack.Width() / 2, 100 + (amountOfLines + 1) * 30), new Rectangle(0, TextureAssets.ChatBack.Height() - 30, TextureAssets.ChatBack.Width(), 30), color, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
			IL_02fc: callvirt instance void [FNA]Microsoft.Xna.Framework.Graphics.SpriteBatch::Draw(class [FNA]Microsoft.Xna.Framework.Graphics.Texture2D, valuetype [FNA]Microsoft.Xna.Framework.Vector2, valuetype [System.Runtime]System.Nullable`1<valuetype [FNA]Microsoft.Xna.Framework.Rectangle>, valuetype [FNA]Microsoft.Xna.Framework.Color, float32, valuetype [FNA]Microsoft.Xna.Framework.Vector2, float32, valuetype [FNA]Microsoft.Xna.Framework.Graphics.SpriteEffects, float32)
			IL_0301: ldsfld class [FNA]Microsoft.Xna.Framework.Graphics.SpriteBatch Terraria.Main::spriteBatch
			IL_0306: ldsfld class [ReLogic]ReLogic.Content.Asset`1<class [FNA]Microsoft.Xna.Framework.Graphics.Texture2D> Terraria.GameContent.TextureAssets::ChatBack
			IL_030b: callvirt instance !0 class [ReLogic]ReLogic.Content.Asset`1<class [FNA]Microsoft.Xna.Framework.Graphics.Texture2D>::get_Value()
			IL_0310: ldsfld int32 Terraria.Main::screenWidth
			IL_0315: ldc.i4.2
			IL_0316: div
			IL_0317: ldsfld class [ReLogic]ReLogic.Content.Asset`1<class [FNA]Microsoft.Xna.Framework.Graphics.Texture2D> Terraria.GameContent.TextureAssets::ChatBack
			IL_031c: call int32 Terraria.Utils::Width(class [ReLogic]ReLogic.Content.Asset`1<class [FNA]Microsoft.Xna.Framework.Graphics.Texture2D>)
			IL_0321: ldc.i4.2
			IL_0322: div
			IL_0323: sub
			IL_0324: conv.r4
			IL_0325: ldc.i4.s 100
			IL_0327: ldloc.s 6
			IL_0329: ldc.i4.1
			IL_032a: add
			IL_032b: ldc.i4.s 30
			IL_032d: mul
			IL_032e: add
			IL_032f: conv.r4
			IL_0330: newobj instance void [FNA]Microsoft.Xna.Framework.Vector2::.ctor(float32, float32)
			IL_0335: ldc.i4.0
			IL_0336: ldsfld class [ReLogic]ReLogic.Content.Asset`1<class [FNA]Microsoft.Xna.Framework.Graphics.Texture2D> Terraria.GameContent.TextureAssets::ChatBack
			IL_033b: call int32 Terraria.Utils::Height(class [ReLogic]ReLogic.Content.Asset`1<class [FNA]Microsoft.Xna.Framework.Graphics.Texture2D>)
			IL_0340: ldc.i4.s 30
			IL_0342: sub
			IL_0343: ldsfld class [ReLogic]ReLogic.Content.Asset`1<class [FNA]Microsoft.Xna.Framework.Graphics.Texture2D> Terraria.GameContent.TextureAssets::ChatBack
			IL_0348: call int32 Terraria.Utils::Width(class [ReLogic]ReLogic.Content.Asset`1<class [FNA]Microsoft.Xna.Framework.Graphics.Texture2D>)
			IL_034d: ldc.i4.s 30
			IL_034f: newobj instance void [FNA]Microsoft.Xna.Framework.Rectangle::.ctor(int32, int32, int32, int32)
			IL_0354: newobj instance void valuetype [System.Runtime]System.Nullable`1<valuetype [FNA]Microsoft.Xna.Framework.Rectangle>::.ctor(!0)
			IL_0359: ldloc.0
			IL_035a: ldc.r4 0.0
			IL_035f: ldloca.s 20
			IL_0361: initobj [FNA]Microsoft.Xna.Framework.Vector2
			IL_0367: ldloc.s 20
			IL_0369: ldc.r4 1
			IL_036e: ldc.i4.0
			IL_036f: ldc.r4 0.0
			IL_0374: callvirt instance void [FNA]Microsoft.Xna.Framework.Graphics.SpriteBatch::Draw(class [FNA]Microsoft.Xna.Framework.Graphics.Texture2D, valuetype [FNA]Microsoft.Xna.Framework.Vector2, valuetype [System.Runtime]System.Nullable`1<valuetype [FNA]Microsoft.Xna.Framework.Rectangle>, valuetype [FNA]Microsoft.Xna.Framework.Color, float32, valuetype [FNA]Microsoft.Xna.Framework.Vector2, float32, valuetype [FNA]Microsoft.Xna.Framework.Graphics.SpriteEffects, float32)

		[+]	// skipVanillaBoxDrawingLabel:
			// TextSnippet textSnippet = null;
			IL_0379: ldnull
			IL_037a: stloc.s 8

			// for (int i = 0; i < amountOfLines; i++)
			IL_037c: ldc.i4.0
			IL_037d: stloc.s 21

			// List<TextSnippet> list = textLines[i];
			IL_037f: br IL_0404
			// loop start (head: IL_0404)
				IL_0384: ldloc.s 5
				IL_0386: ldloc.s 21
				IL_0388: callvirt instance !0 class [System.Collections]System.Collections.Generic.List`1<class [System.Collections]System.Collections.Generic.List`1<class Terraria.UI.Chat.TextSnippet>>::get_Item(int32)
				IL_038d: stloc.s 22

		[+]		// if (!skipVanillaDrawing)
		[+]		IL_0000: ldloc skipVanillaDrawing
		[+]		IL_0000: brtrue.s skipVanillaTextDrawingLabel
				// ChatManager.DrawColorCodedStringWithShadow(spriteBatch, FontAssets.MouseText.Value, list.ToArray(), new Vector2(170 + (screenWidth - 800) / 2, 120 + i * 30), 0f, color2, Color.Black, Vector2.Zero, Vector2.One, out var hoveredSnippet);
				IL_038f: ldsfld class [FNA]Microsoft.Xna.Framework.Graphics.SpriteBatch Terraria.Main::spriteBatch
				IL_0394: ldsfld class [ReLogic]ReLogic.Content.Asset`1<class [ReLogic]ReLogic.Graphics.DynamicSpriteFont> Terraria.GameContent.FontAssets::MouseText
				IL_0399: callvirt instance !0 class [ReLogic]ReLogic.Content.Asset`1<class [ReLogic]ReLogic.Graphics.DynamicSpriteFont>::get_Value()
				IL_039e: ldloc.s 22
				IL_03a0: callvirt instance !0[] class [System.Collections]System.Collections.Generic.List`1<class Terraria.UI.Chat.TextSnippet>::ToArray()
				IL_03a5: ldc.i4 170
				IL_03aa: ldsfld int32 Terraria.Main::screenWidth
				IL_03af: ldc.i4 800
				IL_03b4: sub
				IL_03b5: ldc.i4.2
				IL_03b6: div
				IL_03b7: add
				IL_03b8: conv.r4
				IL_03b9: ldc.i4.s 120
				IL_03bb: ldloc.s 21
				IL_03bd: ldc.i4.s 30
				IL_03bf: mul
				IL_03c0: add
				IL_03c1: conv.r4
				IL_03c2: newobj instance void [FNA]Microsoft.Xna.Framework.Vector2::.ctor(float32, float32)
				IL_03c7: ldc.r4 0.0
				IL_03cc: ldloc.2
				IL_03cd: call valuetype [FNA]Microsoft.Xna.Framework.Color [FNA]Microsoft.Xna.Framework.Color::get_Black()
				IL_03d2: call valuetype [FNA]Microsoft.Xna.Framework.Vector2 [FNA]Microsoft.Xna.Framework.Vector2::get_Zero()
				IL_03d7: call valuetype [FNA]Microsoft.Xna.Framework.Vector2 [FNA]Microsoft.Xna.Framework.Vector2::get_One()
				IL_03dc: ldloca.s 23
				IL_03de: ldc.r4 -1
				IL_03e3: ldc.r4 2
				IL_03e8: call valuetype [FNA]Microsoft.Xna.Framework.Vector2 Terraria.UI.Chat.ChatManager::DrawColorCodedStringWithShadow(class [FNA]Microsoft.Xna.Framework.Graphics.SpriteBatch, class [ReLogic]ReLogic.Graphics.DynamicSpriteFont, class Terraria.UI.Chat.TextSnippet[], valuetype [FNA]Microsoft.Xna.Framework.Vector2, float32, valuetype [FNA]Microsoft.Xna.Framework.Color, valuetype [FNA]Microsoft.Xna.Framework.Color, valuetype [FNA]Microsoft.Xna.Framework.Vector2, valuetype [FNA]Microsoft.Xna.Framework.Vector2, int32&, float32, float32)
				IL_03ed: pop
		[+]		IL_0000: br.s skipVanillaTextDrawingLabel2
		
		[+]		// skipVanillaTextDrawingLabel:
		[+]		IL_0000: ldc.i4.m1
		[+]		IL_0000: stloc 23

		[+]		// skipVanillaTextDrawingLabel2:
				// if (hoveredSnippet > -1)
				IL_03ee: ldloc.s 23
				IL_03f0: ldc.i4.m1
				IL_03f1: ble.s IL_03fe

				// textSnippet = list[hoveredSnippet];
				IL_03f3: ldloc.s 22
				IL_03f5: ldloc.s 23
				IL_03f7: callvirt instance !0 class [System.Collections]System.Collections.Generic.List`1<class Terraria.UI.Chat.TextSnippet>::get_Item(int32)
				IL_03fc: stloc.s 8

				// for (int i = 0; i < amountOfLines; i++)
				IL_03fe: ldloc.s 21
				IL_0400: ldc.i4.1
				IL_0401: add
				IL_0402: stloc.s 21

				// for (int i = 0; i < amountOfLines; i++)
				IL_0404: ldloc.s 21
				IL_0406: ldloc.s 6
		[+]		IL_0000: pop
		[+]		IL_0000: ldloc.s oldAmountOfLines
				IL_0408: blt IL_0384
			// end loop
	 */
	public static void GUIChatDrawInnerPatch(ILContext il) {
		var c = new ILCursor(il);
		try {
			int amountOfLinesIndex = -1;
			int skipVanillaDrawingIndex = c.AddVariable<bool>();
			int oldAmountOfLinesIndex = c.AddVariable<int>();
			int hoveredSnippetIndex = -1;

			var skipVanillaBoxDrawingLabel = c.DefineLabel();
			var skipVanillaTextDrawingLabel = c.DefineLabel();
			var skipVanillaTextDrawingLabel2 = c.DefineLabel();
			var skipVanillaTextDrawingLabel3 = c.DefineLabel();

			c.GotoNext(i => i.MatchCallvirt(typeof(Main).GetNestedType("TextDisplayCache", BindingFlags.NonPublic | BindingFlags.Public).GetMethod("get_AmountOfLines", BindingFlags.Instance | BindingFlags.Public)));
			c.GotoNext(i => i.MatchStloc(out amountOfLinesIndex));

			if (amountOfLinesIndex == -1) throw new InvalidOperationException("Amounts of lines variable index somehow wasn't found!");

			c.GotoNext(i => i.MatchCallOrCallvirt<Main>("DrawWindowsIMEPanel"));
			c.GotoNext(MoveType.After, i => i.MatchLdsfld<Main>("spriteBatch"));

			// Pop sprite batch.
			c.Emit(OpCodes.Pop);

			// Set skipVanillaDrawing variable to false.
			c.Emit(OpCodes.Ldc_I4, 0);
			c.Emit(OpCodes.Stloc, skipVanillaDrawingIndex);

			c.Emit(OpCodes.Ldloca, skipVanillaDrawingIndex);
			c.Emit(OpCodes.Ldloca, oldAmountOfLinesIndex);
			c.Emit(OpCodes.Ldloca, amountOfLinesIndex);
			c.EmitDelegate(static (ref bool skipVanillaDrawing, ref int oldAmountOfLines, ref int amountOfLines) => {
				oldAmountOfLines = amountOfLines;

				if (Main.LocalPlayer.TalkNPC is NPC { ModNPC: BloodCultist }) {
					if (BloodCultist.SelectedMenu != BloodCultist.Menu.None) {
						if (bloodCultistUi?.CurrentState != null) {
							amountOfLines = 9;
						}
						skipVanillaDrawing = true;
					}
				}
				else {
					BloodCultist.SelectedMenu = BloodCultist.Menu.None;
					bloodCultistUi?.SetState(null);
				}
			});

			// Skip vanilla box drawing if skipVanillaDrawing variable is true.
			c.Emit(OpCodes.Ldloc, skipVanillaDrawingIndex);
			c.Emit(OpCodes.Brtrue_S, skipVanillaBoxDrawingLabel);

			// Push sprite batch that we have popped earlier.
			c.Emit(OpCodes.Ldsfld, typeof(Main).GetField("spriteBatch", BindingFlags.Static | BindingFlags.Public));
			for (int i = 0; i < 2; i++) {
				c.GotoNext(MoveType.After, i => i.MatchCallvirt(typeof(SpriteBatch).GetMethod("Draw", BindingFlags.Instance | BindingFlags.Public, new Type[] {
					typeof(Texture2D),
					typeof(Vector2),
					typeof(Rectangle?),
					typeof(Color),
					typeof(float),
					typeof(Vector2),
					typeof(float),
					typeof(SpriteEffects),
					typeof(float)
				})));
			}

			c.MarkLabel(skipVanillaBoxDrawingLabel);

			for (int i = 0; i < 3; i++) {
				c.GotoNext(MoveType.After, i => i.MatchStloc(out _));
			}

			// Skip vanilla text drawing if skipVanillaDrawingIndex is true.
			c.Emit(OpCodes.Ldloc, skipVanillaDrawingIndex);
			c.Emit(OpCodes.Brtrue_S, skipVanillaTextDrawingLabel);
			c.GotoNext(MoveType.After,
				i => i.MatchCall(typeof(ChatManager), "DrawColorCodedStringWithShadow"),
				i => i.MatchPop()
			);

			c.Emit(OpCodes.Br_S, skipVanillaTextDrawingLabel2);

			c.GotoNext(i => i.MatchLdloc(out hoveredSnippetIndex));
			if (hoveredSnippetIndex == -1) throw new InvalidOperationException("Hovered snipped index variable index somehow wasn't found!");

			c.MarkLabel(skipVanillaTextDrawingLabel);

			// Set hoveredSnipper variable to -1, so it cannot be used by any collections.
			c.Emit(OpCodes.Ldc_I4_M1);
			c.Emit(OpCodes.Stloc, hoveredSnippetIndex);

			c.MarkLabel(skipVanillaTextDrawingLabel2);

			// Replace amountOfLines variable with oldAmountOfLines variable, since we have changed original one and don't want to get out of the bounds.
			c.GotoNext(MoveType.After, i => i.MatchLdloc(amountOfLinesIndex));
			c.Emit(OpCodes.Pop);
			c.Emit(OpCodes.Ldloc, oldAmountOfLinesIndex);
		}
		catch {
			MonoModHooks.DumpIL(Esther.Instance, il);
		}
		MonoModHooks.DumpIL(Esther.Instance, il);
	}
}
