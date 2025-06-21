using System.ComponentModel;

namespace BlHell_per.Core.ReMud;

public enum Size
{
    //
    // Сводка:
    //     The smallest size.
    [Description("small")]
    Small,
    //
    // Сводка:
    //     A medium size.
    [Description("medium")]
    Medium,
    //
    // Сводка:
    //     The large size.
    [Description("large")]
    Large,
    //
    // Сводка:
    //     A super larg size.
    [Description("super-large")]
    SuperLarge,
    //
    // Сводка:
    //     A gigantic size.
    [Description("gigantic")]
    Gigantic,
    //
    // Сводка:
    //     A gigantic size.
    [Description("absolute")]
    Absolute
}
