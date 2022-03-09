using Terraria.ModLoader;
using Terraria.UI;
using Terraria;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.GameContent.UI.Elements;
using AmmunitionWorkshop.UI;

namespace AmmunitionWorkshop
{
	public class AmmunitionWorkshop : Mod
	{
		public static ModKeybind menukeybind;
		public static ModKeybind changeleft;
		public static ModKeybind changeright;
		public override void Load()
		{
			menukeybind = KeybindLoader.RegisterKeybind(this, "Call ammunition menu", "N");
			changeleft = KeybindLoader.RegisterKeybind(this, "Previous ammo set", "K");
			changeright = KeybindLoader.RegisterKeybind(this, "Next ammo set", "L");
			base.Load();
		}
		public override void Unload()
		{
			menukeybind = null;
		}
	}
	public class AmmunitionWorkshopSystems : ModSystem
    {
		public UserInterface MyInterface;
		internal TheUI MyUI;

        public override void OnWorldLoad()
        {
			
			if (!Main.dedServ)
			{
				MyInterface = new UserInterface();

				MyUI = new TheUI();
				MyUI.Activate(); // Activate calls Initialize() on the UIState if not initialized, then calls OnActivate and then calls Activate on every child element
			}
			base.OnWorldLoad();
		}
		private GameTime _lastUpdateUiGameTime;

		public override void UpdateUI(GameTime gameTime)
		{
			if (AmmunitionWorkshop.menukeybind.JustPressed)
            {
				if (MyInterface?.CurrentState != null)
				{
					MyInterface.SetState(null);
                }
                else
                {
					MyInterface.SetState(MyUI);
				}

			}
			_lastUpdateUiGameTime = gameTime;
			if (MyInterface?.CurrentState != null)
			{
				MyInterface.Update(gameTime);
			}
		}
		public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
		{
			int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
			if (mouseTextIndex != -1)
			{
				layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
					"AmmunitionWorkshop: BulletsInterface",
					delegate
					{
						if (_lastUpdateUiGameTime != null && MyInterface?.CurrentState != null)
						{
							MyInterface.Draw(Main.spriteBatch, _lastUpdateUiGameTime);
						}
						return true;
					},
					   InterfaceScaleType.UI));
			}
		}


	}

	class TheUI : UIState {
		UIText textstate = null;
        public override void OnInitialize()
		{ // 1
			DraggableUIPanel panel = new DraggableUIPanel("ammunitionpanel"); // 2
			panel.Width.Set(400, 0); // 3
			panel.Height.Set(145, 0); // 3
			panel.HAlign = panel.VAlign = 0.4f;
			Append(panel); // 4
			for (int i = 0; i < 3; i++)
            {
				for (int u = 0; u < 7; u++)
				{
					VanillaItemSlotWrapper slotty = new VanillaItemSlotWrapper(i * 7 + u);
					//UIItemSlot slot = new UIItemSlot(Main.player[Main.myPlayer].GetModPlayer<AmmWorkhopModPl>().GetArrayItems(), i*10+u, ItemSlot.Context.InventoryAmmo);
					slotty.Width.Set(30, 0);
					//slotty.Height.Set(30, 0);
					slotty.Top.Set(40*i, 0);
					slotty.Left.Set(40*u, 0);

					//slot.OnClick += Butt_Click;
					panel.Append(slotty);
				}
				//slotty.Width.Set(30, 0);
				//slotty.Height.Set(30, 0);
				//slotty.Top.Set(120, 0);
				//slotty.Left.Set(120, 0);
				//panel.Append(slotty);
			}
			UIImageButton left = new UIImageButton(Terraria.GameContent.TextureAssets.ScrollLeftButton);
			left.OnClick += Butt1_Click;
			left.Top.Set(20, 0);
			left.Left.Set(275, 0);
			panel.Append(left);

			UIImageButton right = new UIImageButton(Terraria.GameContent.TextureAssets.ScrollRightButton);
			right.OnClick += Butt2_Click;
			right.Top.Set(20, 0);
			right.Left.Set(360, 0);
			panel.Append(right);

			UIText text = new UIText("Ammo set: " + (Main.player[Main.myPlayer].GetModPlayer<AmmWorkhopModPl>().CurrentMode + 1).ToString());
			textstate = text;
			text.Top.Set(40, 0);
			text.Left.Set(275, 0);
			text.Width.Set(100,0);
			panel.Append(text);
			textstate.SetText("Ammo set: " + (Main.player[Main.myPlayer].GetModPlayer<AmmWorkhopModPl>().CurrentMode + 1).ToString());
		}
		private void Butt2_Click(UIMouseEvent evt, UIElement listeningElement)
		{
			Main.player[Main.myPlayer].GetModPlayer<AmmWorkhopModPl>().CurrentMode++;
			if (Main.player[Main.myPlayer].GetModPlayer<AmmWorkhopModPl>().CurrentMode > 2)
			{
				Main.player[Main.myPlayer].GetModPlayer<AmmWorkhopModPl>().CurrentMode = 0;
			}
			textstate.SetText("Ammo set: "+ (Main.player[Main.myPlayer].GetModPlayer<AmmWorkhopModPl>().CurrentMode+1).ToString());
			
			//text.HAlign = text.Width;
		}
		private void Butt1_Click(UIMouseEvent evt, UIElement listeningElement)
		{
			Main.player[Main.myPlayer].GetModPlayer<AmmWorkhopModPl>().CurrentMode--;
			if (Main.player[Main.myPlayer].GetModPlayer<AmmWorkhopModPl>().CurrentMode < 0)
            {
				Main.player[Main.myPlayer].GetModPlayer<AmmWorkhopModPl>().CurrentMode = 2;
			}
			textstate.SetText("Ammo set: " + (Main.player[Main.myPlayer].GetModPlayer<AmmWorkhopModPl>().CurrentMode + 1).ToString());
		}
	}
}