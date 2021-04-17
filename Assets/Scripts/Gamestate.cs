using UnityEngine;
using System.Collections;
using UnityEditor;

public static class Gamestate
{
    // Avatar
    public static Gender gender = Gender.Female;
    public static SkinColor skinColor = SkinColor.Black;

    public static string costume;
    public static int costumeIndex = 0;
    public static int costumeVariantIndex = 0;

    public static Material[] avatarMaterials;
    public static string hair;
    public static string face;
    public static string beard;

    // Misc
    public static int coins = 0;
    public static int coinsCurrentGame = 0;
}
