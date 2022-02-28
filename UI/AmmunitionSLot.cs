using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;
using System;
using Terraria.GameInput;

namespace AmmunitionWorkshop.UI
{
    //internal class AmmunitionSlot : UIItemSlot
    //{


    //    //public AmmunitionSlot(Item[] itemArra, int a, int b)
    //    //{itemArray
    //    //}
        
       
    //}




	// This class wraps the vanilla ItemSlot class into a UIElement. The ItemSlot class was made before the UI system was made, so it can't be used normally with UIState. 
	// By wrapping the vanilla ItemSlot class, we can easily use ItemSlot.
	// ItemSlot isn't very modder friendly and operates based on a "Context" number that dictates how the slot behaves when left, right, or shift clicked and the background used when drawn. 
	// If you want more control, you might need to write your own UIElement.
	// I've added basic functionality for validating the item attempting to be placed in the slot via the validItem Func. 
	// See ExamplePersonUI for usage and use the Awesomify chat option of Example Person to see in action.



	internal class VanillaItemSlotWrapper : UIElement
	{
		//internal Item Item;
		internal int index;
		private readonly int _context;
		private readonly float _scale;
		internal Func<Item,bool> ValidItemFunc;



		public VanillaItemSlotWrapper(int indexx,int context = ItemSlot.Context.InventoryAmmo, float scale = 0.7f)
		{
			index = indexx;
			   _context = context;
			_scale = scale;
			//Item = new Item();
			//Item.SetDefaults(0);
			Width.Set(Terraria.GameContent.TextureAssets.InventoryBack9.Height() * scale, 0f);
			//Width.Set(Terraria.GameContent.TextureAssets.InventoryBack9.Height() * scale, 0f);
			Height.Set(Terraria.GameContent.TextureAssets.InventoryBack9.Height() * scale, 0f);
		}

		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			ValidItemFunc = (item) => {
				if (item.FitsAmmoSlot()) { return true; };
				return false;
			};
			Item itemm = Main.mouseItem.Clone();
			Player Player = Main.player[Main.myPlayer];
			float oldScale = Main.inventoryScale;
			Main.inventoryScale = _scale;
			Rectangle rectangle = GetDimensions().ToRectangle();
			if (Player.GetModPlayer<AmmWorkhopModPl>().bullets[Player.GetModPlayer<AmmWorkhopModPl>().CurrentMode, index] == null)
            {
				Player.GetModPlayer<AmmWorkhopModPl>().bullets[Player.GetModPlayer<AmmWorkhopModPl>().CurrentMode, index] = new Item();

			}

			if (ContainsPoint(Main.MouseScreen) && !PlayerInput.IgnoreMouseInterface)
			{
				Main.LocalPlayer.mouseInterface = true;
				if ((ValidItemFunc == null || ValidItemFunc(Main.mouseItem))&& !PlayerInput.GetPressedKeys().Contains(Microsoft.Xna.Framework.Input.Keys.LeftShift) && !PlayerInput.GetPressedKeys().Contains(Microsoft.Xna.Framework.Input.Keys.LeftControl))
				{
					//Player.GetModPlayer<AmmWorkhopModPl>().bullets[Player.GetModPlayer<AmmWorkhopModPl>().CurrentMode, index] = new Item();
					// Handle handles all the click and hover actions based on the context.
					ItemSlot.Handle(ref Player.GetModPlayer<AmmWorkhopModPl>().bullets[Player.GetModPlayer<AmmWorkhopModPl>().CurrentMode, index], _context);
					Player.GetModPlayer<AmmWorkhopModPl>().bullets[Player.GetModPlayer<AmmWorkhopModPl>().CurrentMode, index].stack = 1;
					Main.mouseItem = itemm;
				}
			}
			// Draw draws the slot itself and Item. Depending on context, the color will change, as will drawing other things like stack counts.

			//Logging.PublicLogger.Debug($"bullet#1 is: {ItemArray[index]}.");
			ItemSlot.Draw(spriteBatch, ref Player.GetModPlayer<AmmWorkhopModPl>().bullets[Player.GetModPlayer<AmmWorkhopModPl>().CurrentMode, index], _context, rectangle.TopLeft());
			Main.inventoryScale = oldScale;
		}
	}
}