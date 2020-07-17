using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using MentorSpeedDatingApp.Models;
using Telerik.Windows.Controls;
// ReSharper disable PossibleNullReferenceException

namespace MentorSpeedDatingApp.Views
{
    /// <summary>
    /// Interaktionslogik für MatchingWindow.xaml
    /// </summary>
    public partial class MatchingWindow : Window
    {



        public MatchingWindow()
        {
            FluentPalette.Palette.AccentColor = (Color)ColorConverter.ConvertFromString("#FF20B613");
            FluentPalette.Palette.AccentFocusedColor = (Color)ColorConverter.ConvertFromString("#FF08D94B");
            FluentPalette.Palette.AccentMouseOverColor = (Color)ColorConverter.ConvertFromString("#FF11E800");
            FluentPalette.Palette.AccentPressedColor = (Color)ColorConverter.ConvertFromString("#FF00A40F");
            FluentPalette.Palette.AlternativeColor = (Color)ColorConverter.ConvertFromString("#FFF2F2F2");
            FluentPalette.Palette.BasicColor = (Color)ColorConverter.ConvertFromString("#33000000");
            FluentPalette.Palette.BasicSolidColor = (Color)ColorConverter.ConvertFromString("#FFCDCDCD");
            FluentPalette.Palette.ComplementaryColor = (Color)ColorConverter.ConvertFromString("#FFCCCCCC");
            FluentPalette.Palette.IconColor = (Color)ColorConverter.ConvertFromString("#CC000000");
            FluentPalette.Palette.MainColor = (Color)ColorConverter.ConvertFromString("#1A000000");
            FluentPalette.Palette.MarkerColor = (Color)ColorConverter.ConvertFromString("#FF000000");
            FluentPalette.Palette.MarkerInvertedColor = (Color)ColorConverter.ConvertFromString("#FFFFFFFF");
            FluentPalette.Palette.MarkerMouseOverColor = (Color)ColorConverter.ConvertFromString("#FF000000");
            FluentPalette.Palette.MouseOverColor = (Color)ColorConverter.ConvertFromString("#9AEDAB");
            FluentPalette.Palette.PressedColor = (Color)ColorConverter.ConvertFromString("#4C000000");
            FluentPalette.Palette.PrimaryBackgroundColor = (Color)ColorConverter.ConvertFromString("#FFFFFFFF");
            FluentPalette.Palette.PrimaryColor = (Color)ColorConverter.ConvertFromString("#66FFFFFF");
            FluentPalette.Palette.PrimaryMouseOverColor = (Color)ColorConverter.ConvertFromString("#FFFFFFFF");
            FluentPalette.Palette.ReadOnlyBackgroundColor = (Color)ColorConverter.ConvertFromString("#00FFFFFF");
            FluentPalette.Palette.ReadOnlyBorderColor = (Color)ColorConverter.ConvertFromString("#FFCDCDCD");
            FluentPalette.Palette.ValidationColor = (Color)ColorConverter.ConvertFromString("#FFE81123");
            FluentPalette.Palette.DisabledOpacity = 0.3;
            FluentPalette.Palette.InputOpacity = 0.6;
            FluentPalette.Palette.ReadOnlyOpacity = 0.5;
            StyleManager.ApplicationTheme = new FluentTheme();

            InitializeComponent();
        }

        public MatchingWindow(List<Mentor> mentorList)
        {
            this.InitializeComponent();
        }
    }
}
