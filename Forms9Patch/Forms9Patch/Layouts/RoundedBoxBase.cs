﻿using System;
using Xamarin.Forms;

namespace Forms9Patch
{
	/// <summary>
	/// Rounded box base.
	/// </summary>
	public static class RoundedBoxBase 
	{
		/// <summary>
		/// Backing store for the HasShadow bindable property.
		/// </summary>
		/// <remarks></remarks>
		public static readonly BindableProperty HasShadowProperty = BindableProperty.Create ("RbbHasShadow", typeof(bool), typeof(RoundedBoxBase), false, BindingMode.OneWay, UpdateBasePadding);

		/// <summary>
		/// Inverts shadow to create an embossed effect
		/// </summary>
		public static readonly BindableProperty ShadowInvertedProperty = BindableProperty.Create("RbbShadowInverted", typeof(bool), typeof(RoundedBoxBase), false);

		/// <summary>
		/// Backing store for the OutlineColor bindable property.
		/// </summary>
		/// <remarks></remarks>
		public static readonly BindableProperty OutlineColorProperty = BindableProperty.Create ("RbbOutlineColor", typeof(Color), typeof(RoundedBoxBase), Color.Default);

		/// <summary>
		/// Backing store for the OutlineRadius bindable property.
		/// </summary>
		public static readonly BindableProperty OutlineRadiusProperty = BindableProperty.Create("RbbOutlineRadius", typeof (float), typeof (RoundedBoxBase), (object) -1.0f);

		/// <summary>
		/// Backing store for the OutlineWidth bindable property.
		/// </summary>
		public static readonly BindableProperty OutlineWidthProperty = BindableProperty.Create("RbbOutlineWidth", typeof (float), typeof (RoundedBoxBase), (object) -1.0f);

		/// <summary>
		/// Identifies the Padding bindable property.
		/// </summary>
		/// <remarks></remarks>
		public static readonly BindableProperty PaddingProperty = BindableProperty.Create ("RbbPadding", typeof(Thickness), typeof(RoundedBoxBase), new Thickness (0), BindingMode.OneWay, UpdateBasePadding);

		/// <summary>
		/// The elliptical property backing store.
		/// </summary>
		public static readonly BindableProperty IsEllipticalProperty = BindableProperty.Create("RbbIsElliptical", typeof(bool), typeof(RoundedBoxBase), false);

		internal static bool UpdateBasePadding(BindableObject bindable, object newValue) {
			var layout = bindable as Layout;
			Thickness layoutPadding;
			if (newValue is bool)
				layoutPadding = (Thickness)layout.GetValue (RoundedBoxBase.PaddingProperty);
			else
				layoutPadding = (Thickness)newValue;
			var materialButton = layout as MaterialButton;
			var hasShadow = (bool)layout.GetValue (RoundedBoxBase.HasShadowProperty);
			var makeRoomForShadow = materialButton == null ? hasShadow : (bool)layout.GetValue (MaterialButton.HasShadowProperty);

			if (makeRoomForShadow) {
				
				var shadowPadding = ShadowPadding (layout);

				Thickness newPadding;
				double xLeft = layoutPadding.Left + shadowPadding.Left;
				double xTop = layoutPadding.Top + shadowPadding.Top;
				double xRight = layoutPadding.Right + shadowPadding.Right;
				double xBottom = layoutPadding.Bottom + shadowPadding.Bottom;
				newPadding = new Thickness (xLeft, xTop, xRight, xBottom);

				layout.SetValue (Layout.PaddingProperty, newPadding);
				//System.Diagnostics.Debug.WriteLine ("\tRoundedBoxBase.UpdateBasePadding newPadding: " + newPadding.Description ());
			} else {
				layout.SetValue (Layout.PaddingProperty, layoutPadding);
				//System.Diagnostics.Debug.WriteLine ("\tRoundedBoxBase.UpdateBasePadding layoutPadding: " + layoutPadding.Description ());
			}
			//var contentView = bindable as ContentView;
			return true;
		}


		internal static Thickness ShadowPadding(Layout layout) {
			if (layout == null)
				return new Thickness (0);
			var materialButton = layout as MaterialButton;
			var hasShadow = (bool)layout.GetValue (RoundedBoxBase.HasShadowProperty);
			var makeRoomForShadow = materialButton == null ? hasShadow : (bool)layout.GetValue (MaterialButton.HasShadowProperty);

			if (makeRoomForShadow) {
				SegmentType type = materialButton == null ? SegmentType.Not : materialButton.SegmentType;
				StackOrientation orientation = materialButton == null ? StackOrientation.Horizontal : materialButton.SegmentOrientation;

				var shadowX = Settings.ShadowOffset.X;
				var shadowY = Settings.ShadowOffset.Y;
				var shadowR = Settings.ShadowRadius;

				// additional padding alocated for the button's shadow
				var padL = orientation == StackOrientation.Horizontal && (type == SegmentType.Mid || type == SegmentType.End) ? 0 : Math.Max (0, shadowR - shadowX);
				var padR = orientation == StackOrientation.Horizontal && (type == SegmentType.Start || type == SegmentType.Mid) ? 0 : Math.Max (0, shadowR + shadowX);
				var padT = orientation == StackOrientation.Vertical && (type == SegmentType.Mid || type == SegmentType.End) ? 0 : Math.Max (0, shadowR - shadowY);
				var padB = orientation == StackOrientation.Vertical && (type == SegmentType.Start || type == SegmentType.Mid) ? 0 : Math.Max (0, shadowR + shadowY);

				return new Thickness (padL, padT, padR, padB);
			} else
				return new Thickness (0);
		}

	}
}

