using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ResourceManager
{
    public static GameObject[] LoadAll (ResourceType type)
    {
        return Resources.LoadAll<GameObject> (ResourceTypeToPath (type));
    }

    public static GameObject LoadSingle (ResourceType type, string name)
    {
        string path = ResourceTypeToPath (type) + "/" + name;
        Debug.Log ("Loading " + path);

        return UnityEngine.Object.Instantiate (Resources.Load<GameObject> (path));
    }

    private static string ResourceTypeToPath (ResourceType type)
    {
        switch (type)
        {
            case ResourceType.Costume:
                return "Character/Costume";
            case ResourceType.Hair:
                return "Character/Hair";
            case ResourceType.Face:
                return "Character/Face";
            case ResourceType.Beard:
                return "Character/Beard";
            default:
                throw new ArgumentException ("undefined resource path");
        }
    }
}

public enum ResourceType { Costume, Hair, Face, Beard }
