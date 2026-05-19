using System.Collections.Generic;

namespace kamrj.Configuration.Ui
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public static class UiThemes
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public static List<UiThemeInfo> All { get; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

        static UiThemes()
        {
            All = new List<UiThemeInfo>
            {
                new UiThemeInfo("Red", "red"),
                new UiThemeInfo("Pink", "pink"),
                new UiThemeInfo("Purple", "purple"),
                new UiThemeInfo("Deep Purple", "deep-purple"),
                new UiThemeInfo("Indigo", "indigo"),
                new UiThemeInfo("Blue", "blue"),
                new UiThemeInfo("Light Blue", "light-blue"),
                new UiThemeInfo("Cyan", "cyan"),
                new UiThemeInfo("Teal", "teal"),
                new UiThemeInfo("Green", "green"),
                new UiThemeInfo("Light Green", "light-green"),
                new UiThemeInfo("Lime", "lime"),
                new UiThemeInfo("Yellow", "yellow"),
                new UiThemeInfo("Amber", "amber"),
                new UiThemeInfo("Orange", "orange"),
                new UiThemeInfo("Deep Orange", "deep-orange"),
                new UiThemeInfo("Brown", "brown"),
                new UiThemeInfo("Grey", "grey"),
                new UiThemeInfo("Blue Grey", "blue-grey"),
                new UiThemeInfo("Black", "black")
            };
        }
    }
}
