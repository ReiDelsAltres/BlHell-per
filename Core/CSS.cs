using MudBlazor;

namespace BlHell_per.Core;
public static class CSS
{
    public static string Striped(string color1, string color2)
    {
        return $"background: repeating-linear-gradient( 45deg, #71f07775{color1}, #71f07775{color1} 10px, #4db05275{color2} 10px, #4db05275{color2} 20px );\n" +
            "color: white !important;";
    }

}
