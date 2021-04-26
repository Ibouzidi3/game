using System;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public static class ResourceManager
{
    private static Dictionary<string, Dictionary<string, int>> priceList = null;
    public static CharacterAsset[] LoadAll (ResourceType type)
    {
        if (priceList == null)
        {
            string json = Resources.Load("Character/manifest").ToString();
            priceList = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, int>>>(json);
            List<string> klist = new List<string>(priceList["Beard"].Keys);
        }


        List<CharacterAsset> toReturn = new List<CharacterAsset>();

        foreach (string path in ResourceTypeToPath(type))
        {
            foreach (GameObject gameObject in Resources.LoadAll<GameObject>(path))
            {
                Dictionary<string, int> ppr = priceList[type.ToString()];
                List<string> k = new List<string>(ppr.Keys);

                if (!ppr.ContainsKey(path + "/" + gameObject.name))
                {
                    Debug.Log("Expected " + k.ToArray()[0] + "---" + "Requested " + path + "/" + gameObject.name);
                    continue;

                }

                int price = ppr[path + "/" + gameObject.name];
                CharacterAsset characterAsset = new CharacterAsset(gameObject, price);
                toReturn.Add(characterAsset);
            }
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
                    "Character/Accessories/Crowns (Head)",
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

public class CharacterAsset
{
    public readonly GameObject gameObject;
    public readonly int price;

    public CharacterAsset(GameObject gameObject, int price)
    {
        this.gameObject = gameObject;
        this.price = price;
    }
}
