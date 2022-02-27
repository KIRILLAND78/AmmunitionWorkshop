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
				MyInterface.SetState(MyUI);
			}
			base.OnWorldLoad();
		}
		private GameTime _lastUpdateUiGameTime;

		public override void UpdateUI(GameTime gameTime)
		{
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
        public override void OnInitialize()
		{ // 1
			DraggableUIPanel panel = new DraggableUIPanel("ammunitionpanel"); // 2
			panel.Width.Set(400, 0); // 3
			panel.Height.Set(150, 0); // 3
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
		}
		private void Butt_Click(UIMouseEvent evt, UIItemSlot listeningElement)
		{
			//Main.player[Main.myPlayer].HeldItem.type;
		}
	}
}