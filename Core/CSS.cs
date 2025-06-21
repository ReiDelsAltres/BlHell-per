namespace BlHell_per.Core;


public static class CSS
{

    public static ICssStyle GetStripedBackground(string color1, string color2) =>
        new StripedBackground() { FirstColor = color1, SecondColor = color2 };


    #region CSSStructs
    private struct StripedBackground : ICssStyle
    {
        public required string FirstColor { get; set; }
        public required string SecondColor { get; set; }

        public string Css
        {
            get => $"background: repeating-linear-gradient( 45deg, {FirstColor}, {FirstColor} 10px, " +
                $"{SecondColor} 10px, {SecondColor} 20px ) !important;\n";
        }
    }
    #endregion CSSStructs
}
public interface ICssStyle
{
    public string Css { get; }
}
