using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ResourceManager
{
    public static GameObject[] LoadAll (ResourceType type)
    {
        List<GameObject> toReturn = new List<GameObject>();

        foreach (string path in ResourceTypeToPath(type))
        {
            toReturn.AddRange(Resources.LoadAll<GameObject>(path));
        }

        return toReturn.ToArray();

    }

    public static GameObject LoadSingle (ResourceType type, string name)
    {
        foreach (string pathDir in ResourceTypeToPath(type))
        {
            string path = pathDir + "/" + name;
            Debug.Log("Loading " + path);
            GameObject loaded = Resources.Load<GameObject>(path);

            if(loaded != null)
                return UnityEngine.Object.Instantiate(loaded);
        }

        return null;
    }

    private static string[] ResourceTypeToPath (ResourceType type)
    {
;
        switch (type)
        {
            case ResourceType.Costume:
                return new string[] { "Character/Costume"};
            case ResourceType.Hair:
                return new string[] { "Character/Hair"};
            case ResourceType.Face:
                return new string[] { "Character/Face"};
            case ResourceType.Beard:
                return new string[] { "Character/Beard" };
            case ResourceType.Headgear:
                return new string[] { 
                    "Character/Accessories/Hats (Head)",
                    "Character/Accessories/Headbands (Head)",
                    "Character/Accessories/Ears (Head)",
                    "Character/Accessories/Helmets (Head)",
                    "Character/Accessories/Horns (Head)",
                    "Character/Accessories/Misc (Head)"
                };
            case ResourceType.FaceAccessory:
                return new string[] {
                    "Character/Accessories/Glasses (Head)",
                    "Character/Accessories/Masks (Head)",
                };
            case ResourceType.BackAccessory:
                return new string[] {
                    "Character/Accessories/Back Props (Back)",
                    "Character/Accessories/Bags and Sacks (Back)",
                    "Character/Accessories/Baskets (Back)",
                    "Character/Accessories/Wings (Back)",
                };

            default:
                throw new ArgumentException ("undefined resource path");
        }
    }
}

public enum ResourceType { 
    Costume,
    Hair,
    Face,
    Beard,
    Headgear,
    FaceAccessory,
    BackAccessory
}
