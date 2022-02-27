using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

namespace AmmunitionWorkshop.UI
{
	public class DraggableUIPanel : UIPanel
	{
		public static Asset<Texture2D> DragTexture { get; internal set; } = null!;

		public string UniqueName { get; }

		public Vector2 Offset { get; private set; }
		public bool Draggable { get; set; }
		public bool Dragging { get; private set; }

		public List<UIElement> AdditionalDragTargets { get; init; } = new();

		public DraggableUIPanel(string uniqueName, bool draggable = true)
		{
			UniqueName = uniqueName;
			Draggable = draggable;
		}

		public void AddDragTarget(UIElement element)
		{
			AdditionalDragTargets.Add(element);
		}

		public override void MouseDown(UIMouseEvent evt)
		{
			DragStart(evt);
			base.MouseDown(evt);
		}

		public override void MouseUp(UIMouseEvent evt)
		{
			DragEnd(evt);
			base.MouseUp(evt);
		}

		private void DragStart(UIMouseEvent evt)
		{
			if (evt.Target != this && !AdditionalDragTargets.Contains(evt.Target))
				return;

			CalculatedStyle innerDimensions = GetInnerDimensions();
			if (Draggable)
			{
				Offset = new Vector2(evt.MousePosition.X - Left.Pixels, evt.MousePosition.Y - Top.Pixels);
				Dragging = true;
			}
		}

		private void DragEnd(UIMouseEvent evt)
		{
			if (evt.Target == this || AdditionalDragTargets.Contains(evt.Target))
			{
				Dragging = false;
			}

		}

		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			CalculatedStyle dimensions = GetOuterDimensions();
			if (ContainsPoint(Main.MouseScreen))
			{
				Main.LocalPlayer.mouseInterface = true;
				Main.LocalPlayer.cursorItemIconEnabled = false;
				Main.ItemIconCacheUpdate(0);
			}

			if (Dragging)
			{
				Left.Set(Main.MouseScreen.X - Offset.X, 0f);
				Top.Set(Main.MouseScreen.Y - Offset.Y, 0f);
				Recalculate();
			}
			else
			{
				if (Parent != null && !dimensions.ToRectangle().Intersects(Parent.GetDimensions().ToRectangle()))
				{
					var parentSpace = Parent.GetDimensions().ToRectangle();
					Left.Pixels = Utils.Clamp(Left.Pixels, Width.Pixels - parentSpace.Right, 0); // TODO: Adjust automatically for Left.Percent (measure from left or right edge)
					Top.Pixels = Utils.Clamp(Top.Pixels, 0, parentSpace.Bottom - Height.Pixels);
					Recalculate();
				}
			}


			base.DrawSelf(spriteBatch);
		}

		private void DrawDragAnchor(SpriteBatch spriteBatch, Texture2D texture, Color color)
		{
			CalculatedStyle dimensions = GetDimensions();

			//	Rectangle hitbox = GetInnerDimensions().ToRectangle();
			//	Main.spriteBatch.Draw(Main.magicPixel, hitbox, Color.LightBlue * 0.6f);

			Point point = new((int)(dimensions.X + dimensions.Width - 12), (int)(dimensions.Y + dimensions.Height - 12));
			spriteBatch.Draw(texture, new Rectangle(point.X - 2, point.Y - 2, 12 - 2, 12 - 2), new Rectangle(12 + 4, 12 + 4, 12, 12), color);
			spriteBatch.Draw(texture, new Rectangle(point.X - 4, point.Y - 4, 12 - 4, 12 - 4), new Rectangle(12 + 4, 12 + 4, 12, 12), color);
			spriteBatch.Draw(texture, new Rectangle(point.X - 6, point.Y - 6, 12 - 6, 12 - 6), new Rectangle(12 + 4, 12 + 4, 12, 12), color);
		}
	}
}