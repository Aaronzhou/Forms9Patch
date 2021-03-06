﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Media;
using WApplication = Windows.UI.Xaml.Application;
using Xamarin.Forms;
using Windows.UI.Text;

namespace Forms9Patch.UWP
{
    static class FontExtensions
    {
        const double lineHeightToFontSizeRatio = 1.4;
        const double roundToNearest = 1;

        internal static double LineHeightForFontSize(double fontSize)
        {
            //return Math.Ceiling(1.25 * fontSize / 4) * 4;
            //return Math.Ceiling(lineHeightToFontSizeRatio * fontSize / roundToNearest) * roundToNearest;
            return lineHeightToFontSizeRatio * fontSize;
        }

        internal static double FontSizeFromLineHeight(double lineHeight)
        {
            //var uwpLineHeight = Math.Floor(lineHeight / 4) * 4;
            //var uwpLineHeight = Math.Floor(lineHeight / roundToNearest) * roundToNearest - 1;
            var uwpLineHeight = lineHeight;
            var fontSize = uwpLineHeight / lineHeightToFontSizeRatio;
            return fontSize;
        }

        internal static double ClipFontSize(double size, Forms9Patch.Label label)
        {
            return ClipFontSize(size, label.MinFontSize, label.FontSize);
        }

        internal static double ClipFontSize(double size, double min, double max)
        {
            if (max == -1)
                max = DefaultFontSize();
            if (min == -1)
                min = 4;
            if (size >= max)
                return max;
            return ClipFontSize(size, min);
        }

        internal static double DefaultFontSize()
        {
            return (double)Windows.UI.Xaml.Application.Current.Resources["ControlContentThemeFontSize"];
        }

        internal static double ClipFontSize(double size, double min)
        {
            if (size < 0)
                return DefaultFontSize() * Math.Abs(size);
            if (size < min)
                return min;
            return size;
        }

        internal static double DecipheredFontSize(this Forms9Patch.Label label)
        {
            if (label == null)
                return DefaultFontSize();
            return ClipFontSize(label.FontSize, DecipheredMinFontSize(label));
        }

        internal static double DecipheredMinFontSize(this Forms9Patch.Label label)
        {
            if (label == null)
                return 4;
            if (label.MinFontSize <= 0)
                return 4;
            return label.MinFontSize;
        }


        internal static double GetFontSize(this NamedSize size)
        {
            // These are values pulled from the mapped sizes on Windows Phone, WinRT has no equivalent sizes, only intents.
            switch (size)
            {
                case NamedSize.Default:
                    return DefaultFontSize();
                case NamedSize.Micro:
                    return 18.667 - 3;
                case NamedSize.Small:
                    return 18.667;
                case NamedSize.Medium:
                    return 22.667;
                case NamedSize.Large:
                    return 32;
                default:
                    throw new ArgumentOutOfRangeException("size");
            }
        }

        public static void ApplyFont(this Control self, Font font)
        {
            self.FontSize = font.UseNamedSize ? font.NamedSize.GetFontSize() : font.FontSize;
            self.FontFamily = FontService.GetWinFontFamily(font.FontFamily);
            self.FontStyle = font.FontAttributes.HasFlag(FontAttributes.Italic) ? FontStyle.Italic : FontStyle.Normal;
            self.FontWeight = font.FontAttributes.HasFlag(FontAttributes.Bold) ? FontWeights.Bold : FontWeights.Normal;
        }

        public static void ApplyFont(this TextBlock self, Font font)
        {
            self.FontSize = font.UseNamedSize ? font.NamedSize.GetFontSize() : font.FontSize;
            self.FontFamily = FontService.GetWinFontFamily(font.FontFamily);
            self.FontStyle = font.FontAttributes.HasFlag(FontAttributes.Italic) ? FontStyle.Italic : FontStyle.Normal;
            self.FontWeight = font.FontAttributes.HasFlag(FontAttributes.Bold) ? FontWeights.Bold : FontWeights.Normal;
        }

        public static void ApplyFont(this TextElement self, Font font)
        {
            self.FontSize = font.UseNamedSize ? font.NamedSize.GetFontSize() : font.FontSize;
            self.FontFamily = FontService.GetWinFontFamily(font.FontFamily);
            self.FontStyle = font.FontAttributes.HasFlag(FontAttributes.Italic) ? FontStyle.Italic : FontStyle.Normal;
            self.FontWeight = font.FontAttributes.HasFlag(FontAttributes.Bold) ? FontWeights.Bold : FontWeights.Normal;
        }

        internal static void ApplyFont(this Control self, IFontElement element)
        {
            self.FontSize = element.FontSize;
            self.FontFamily = FontService.GetWinFontFamily(element.FontFamily);
            self.FontStyle = element.FontAttributes.HasFlag(FontAttributes.Italic) ? FontStyle.Italic : FontStyle.Normal;
            self.FontWeight = element.FontAttributes.HasFlag(FontAttributes.Bold) ? FontWeights.Bold : FontWeights.Normal;
        }


    }
}
