namespace kamrj.Configuration.Ui
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class UiThemeInfo
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public string Name { get; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public string CssClass { get; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public UiThemeInfo(string name, string cssClass)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            Name = name;
            CssClass = cssClass;
        }
    }
}
