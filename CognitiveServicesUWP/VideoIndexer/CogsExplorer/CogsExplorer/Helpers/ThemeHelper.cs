using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace CogsExplorer.Helpers
{
    public static class ThemeHelper
    {
        public static void UpdateSystemAssets()
        {
            Color accentColor = (Color)App.Current.Resources["AppMainBlueColor"];
            
            App.Current.Resources["SystemAccentColor"] = accentColor;
            ((SolidColorBrush)App.Current.Resources["SystemControlHighlightAccentBrush"]).Color = accentColor;
            ((SolidColorBrush)App.Current.Resources["SystemControlBackgroundAccentBrush"]).Color = accentColor;
            ((SolidColorBrush)App.Current.Resources["SystemControlDisabledAccentBrush"]).Color = accentColor;
            ((SolidColorBrush)App.Current.Resources["SystemControlForegroundAccentBrush"]).Color = accentColor;
            ((SolidColorBrush)App.Current.Resources["SystemControlHighlightAccentBrush"]).Color = accentColor;
            ((SolidColorBrush)App.Current.Resources["SystemControlHighlightAltAccentBrush"]).Color = accentColor;
            ((SolidColorBrush)App.Current.Resources["SystemControlHighlightAltListAccentHighBrush"]).Color = accentColor;
            ((SolidColorBrush)App.Current.Resources["SystemControlHighlightAltListAccentLowBrush"]).Color = accentColor;
            ((SolidColorBrush)App.Current.Resources["SystemControlHighlightAltListAccentMediumBrush"]).Color = accentColor;
            ((SolidColorBrush)App.Current.Resources["SystemControlHighlightListAccentHighBrush"]).Color = accentColor;
            ((SolidColorBrush)App.Current.Resources["SystemControlHighlightListAccentLowBrush"]).Color = accentColor;
            ((SolidColorBrush)App.Current.Resources["SystemControlHighlightListAccentMediumBrush"]).Color = accentColor;
            ((SolidColorBrush)App.Current.Resources["SystemControlHyperlinkTextBrush"]).Color = accentColor;

            ((SolidColorBrush)App.Current.Resources["SearchBoxButtonPointerOverBackgroundThemeBrush"]).Color = accentColor;
            ((SolidColorBrush)App.Current.Resources["SearchBoxButtonBackgroundThemeBrush"]).Color = accentColor;
            ((SolidColorBrush)App.Current.Resources["SearchBoxFocusedBorderThemeBrush"]).Color = accentColor;
            ((SolidColorBrush)App.Current.Resources["SearchBoxHitHighlightForegroundThemeBrush"]).Color = accentColor;
            ((SolidColorBrush)App.Current.Resources["SearchBoxHitHighlightSelectedForegroundThemeBrush"]).Color = Colors.White;
            
            ((SolidColorBrush)App.Current.Resources["ContentDialogBorderThemeBrush"]).Color = accentColor;
            ((SolidColorBrush)App.Current.Resources["JumpListDefaultEnabledBackground"]).Color = accentColor;
            ((SolidColorBrush)App.Current.Resources["ContentDialogBorderThemeBrush"]).Color = accentColor;

        }
    }
}
