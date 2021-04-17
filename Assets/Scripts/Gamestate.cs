using UnityEngine;
using System.Collections;
using UnityEditor;

public static class Gamestate
{
    public static Gender gender = Gender.Female;
    public static SkinColor skinColor = SkinColor.Black;

    public static string costume;
    public static int costumeIndex = 0;
    public static int costumeVariantIndex = 0;

    public static Material[] avatarMaterials;
    public static GameObject hair;
}
