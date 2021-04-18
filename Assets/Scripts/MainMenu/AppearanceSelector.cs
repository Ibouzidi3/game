using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class AppearanceSelector : MonoBehaviour
{

    public AvatarSettings avatar;

    public GameObject hairObject;

    // General panel
    public GameObject genderGameObject;
    public GameObject costumeGameObject;
    public GameObject costumeVariantGameObject;
    public GameObject skinColorGameObject;

    private TextMeshProUGUI genderTmp;
    private TextMeshProUGUI costumeTmp;
    private TextMeshProUGUI costumeVariantTmp;
    private TextMeshProUGUI skinColorTmp;

    // Head panel
    public GameObject hairMenuGameObject;
    public GameObject hairColorMenuGameObject;
    public GameObject beardMenuGameObject;
    public GameObject beardColorMenuGameObject;
    public GameObject faceMenuGameObject;

    private TextMeshProUGUI hairTmp;
    private TextMeshProUGUI hairColorTmp;
    private TextMeshProUGUI beardTmp;
    private TextMeshProUGUI beardColorTmp;

    private TextMeshProUGUI faceTmp;

    private Gender gender = Gender.Female;
    private SkinColor skinColor = SkinColor.Black;

    private string costume;
    private int hairColorIndex = 0;
    private int hairStyleIndex = 0;
    private int beardColorIndex = 0;
    private int beardStyleIndex = 0;
    private int costumeIndex = 0;
    private int costumeVariantIndex = 0;
    private int faceIndex = 0;

    private GameObject[] allSkins;
    private GameObject[] allHair;
    private GameObject[] allFaces;
    private GameObject[] allBeards;
    private Dictionary<string, Dictionary<string, List<GameObject>>> assetTree;
    private Dictionary<string, List<Dictionary<string, GameObject>>> hairTree;
    private Dictionary<string, List<GameObject>> faceTree;
    private List<Dictionary<string, GameObject>> beardTree;

    void Start ()
    {
        LoadSkins ();
        LoadHair ();
        LoadFaces ();
        LoadBeards ();
        UpdateSkin ();
    }

    void LoadSkins ()
    {
        allSkins = ResourceManager.LoadAll (ResourceType.Costume);
        assetTree = new Dictionary<string, Dictionary<string, List<GameObject>>> ();
        BuildSkinTree ();

        costume = assetTree[gender + "-" + skinColor].Keys.ToList ()[costumeIndex];

        genderTmp = genderGameObject.GetComponent<TextMeshProUGUI> ();
        costumeTmp = costumeGameObject.GetComponent<TextMeshProUGUI> ();
        skinColorTmp = skinColorGameObject.GetComponent<TextMeshProUGUI> ();
        costumeVariantTmp = costumeVariantGameObject.GetComponent<TextMeshProUGUI> ();
    }

    void LoadHair ()
    {
        hairTree = new Dictionary<string, List<Dictionary<string, GameObject>>> ();
        allHair = ResourceManager.LoadAll (ResourceType.Hair);
        BuildHairTree ();

        hairTmp = hairMenuGameObject.GetComponent<TextMeshProUGUI> ();
        hairColorTmp = hairColorMenuGameObject.GetComponent<TextMeshProUGUI> ();
    }

    void LoadFaces ()
    {
        allFaces = ResourceManager.LoadAll (ResourceType.Face);
        BuildFaceTree ();
        faceTmp = faceMenuGameObject.GetComponent<TextMeshProUGUI> ();
    }

    void LoadBeards ()
    {
        allBeards = ResourceManager.LoadAll (ResourceType.Beard);
        BuildBeardTree ();

        beardTmp = beardMenuGameObject.GetComponent<TextMeshProUGUI> ();
        beardColorTmp = beardColorMenuGameObject.GetComponent<TextMeshProUGUI> ();

    }

    public void OnGenderToggle ()
    {
        gender = gender == Gender.Female ? Gender.Male : Gender.Female;
        costumeVariantIndex = 0;

        string gsKey = gender + "-" + skinColor;
        if (!assetTree[gsKey].ContainsKey (costume))
        {
            costumeIndex = 0;
            costumeVariantIndex = 0;
            costume = assetTree[gsKey].Keys.ToList ()[0];
        }


        UpdateSkin ();
    }

    public void OnColorRight ()
    {
        if ((int)skinColor < Enum.GetValues (typeof (SkinColor)).Length - 1)
            skinColor++;
        else
            skinColor = 0;

        UpdateSkin ();

    }

    public void OnColorLeft ()
    {
        if ((int)skinColor == 0)
            skinColor = SkinColor.White;
        else
            skinColor--;

        UpdateSkin ();

    }

    public void OnCostumeRight ()
    {
        List<string> costumeList = assetTree[gender + "-" + skinColor].Keys.ToList ();
        if (costumeIndex < costumeList.Count () - 1)
            costumeIndex++;
        else
            costumeIndex = 0;

        costumeVariantIndex = 0;
        costume = costumeList[costumeIndex];

        UpdateSkin ();
    }

    public void OnCostumeLeft ()
    {
        List<string> costumeList = assetTree[gender + "-" + skinColor].Keys.ToList ();

        if (costumeIndex == 0)
            costumeIndex = costumeList.Count () - 1;
        else
            costumeIndex--;

        costumeVariantIndex = 0;
        costume = costumeList[costumeIndex];

        UpdateSkin ();

    }

    public void OnCostumeVariantRight ()
    {
        if (costumeVariantIndex < assetTree[gender + "-" + skinColor][costume].Count () - 1)
            costumeVariantIndex++;
        else
            costumeVariantIndex = 0;

        UpdateSkin ();
    }

    public void OnCostumeVariantLeft ()
    {
        if (costumeVariantIndex == 0)
            costumeVariantIndex = assetTree[gender + "-" + skinColor][costume].Count () - 1;
        else
            costumeVariantIndex--;

        UpdateSkin ();
    }

    public void OnHairStyleChange (bool left)
    {
        int max = hairTree[gender.ToString ()].Count;

        hairStyleIndex = rotate (hairStyleIndex, max, left ? Direction.Left : Direction.Right);
        UpdateSkin ();
    }

    public void OnHairColorChange (bool left)
    {
        int max = hairTree[gender.ToString ()][hairStyleIndex].Keys.Count;

        hairColorIndex = rotate (hairColorIndex, max, left ? Direction.Left : Direction.Right);
        UpdateSkin ();
    }

    public void OnFaceChange (bool left)
    {
        int max = faceTree[gender + "_" + skinColor].Count;
        faceIndex = rotate (faceIndex, max, left ? Direction.Left : Direction.Right);
        UpdateSkin ();
    }

    public void OnBeardStyleChange (bool left)
    {
        int max = beardTree.Count;
        beardColorIndex = 0;
        beardStyleIndex = rotate (beardStyleIndex, max, left ? Direction.Left : Direction.Right);
        UpdateSkin ();
    }

    public void OnBeardColorChange (bool left)
    {
        int max = beardTree[beardStyleIndex].Keys.Count;
        beardColorIndex = rotate (beardColorIndex, max, left ? Direction.Left : Direction.Right);
        UpdateSkin ();
    }


    private void BuildSkinTree ()
    {
        foreach (GameObject skin in allSkins)
        {
            string[] elements = skin.name.Split (' ');

            Gender assetGender;

            if (elements[0] == "Female")
                assetGender = Gender.Female;
            else if (elements[0] == "Male")
                assetGender = Gender.Male;
            else
                continue;

            SkinColor assetSkinColor;

            if (elements[1] == "White")
                assetSkinColor = SkinColor.White;
            else if (elements[1] == "Brown")
                assetSkinColor = SkinColor.Brown;
            else if (elements[1] == "Black")
                assetSkinColor = SkinColor.Black;
            else
                continue;

            string costume = "";
            for (int i = 2; i < elements.Length; i++)
            {
                if (elements[i].ToCharArray ()[0] != '0')
                {
                    costume += elements[i] + " ";
                }
                else
                {
                    break;
                }
            }

            string key = assetGender + "-" + assetSkinColor;

            if (!assetTree.ContainsKey (key))
            {
                assetTree.Add (key, new Dictionary<string, List<GameObject>> ());
            }

            if (!assetTree[key].ContainsKey (costume))
            {
                assetTree[key].Add (costume, new List<GameObject> ());
            }
            assetTree[key][costume].Add (skin);
        }
    }

    private void BuildHairTree ()
    {
        foreach (GameObject hair in allHair)
        {
            string[] elements = hair.name.Split (' ');

            Gender assetGender;

            if (elements[1] == "Female")
                assetGender = Gender.Female;
            else if (elements[1] == "Male")
                assetGender = Gender.Male;
            else
                continue;

            hair.name = assetGender + "/" + hair.name;

            int hairStyleIdx = int.Parse (elements[2]);
            string hairColor = elements[3];


            string genderStr = assetGender.ToString ();

            if (!hairTree.ContainsKey (genderStr))
            {
                hairTree[genderStr] = new List<Dictionary<string, GameObject>> ();
            }

            if (hairTree[genderStr].Count < hairStyleIdx)
            {
                hairTree[genderStr].Add (new Dictionary<string, GameObject> ());
            }
            hairTree[genderStr][hairStyleIdx - 1][hairColor] = hair;
        }
    }

    private void BuildFaceTree ()
    {
        faceTree = new Dictionary<string, List<GameObject>> ();

        foreach (GameObject face in allFaces)
        {
            string[] elements = face.name.Split ('/').Last ().Split (' ');
            Gender assetGender = Gender.Female;
            try
            {
                assetGender = tryParseGender (elements[0]);
            }
            catch (ArgumentException e)
            {
                Debug.Log ("Unknown gender: " + face.name);
            }

            SkinColor skinColor = tryParseSkinColor (elements[1]);

            face.name = assetGender + "/" + face.name;
            string label = assetGender + "_" + skinColor;

            if (!faceTree.ContainsKey (label))
            {
                faceTree[label] = new List<GameObject> ();
            }

            faceTree[label].Add (face);
        }
    }

    private void BuildBeardTree ()
    {
        beardTree = new List<Dictionary<string, GameObject>> ();
        foreach (GameObject beard in allBeards)
        {
            string[] elements = beard.name.Split (' ');
            int beardStyleIdx = int.Parse (elements[1]);
            string color = elements[2];

            if (beardTree.Count < beardStyleIdx)
            {
                beardTree.Add (new Dictionary<string, GameObject> ());
            }
            beardTree[beardStyleIdx - 1][color] = beard;
        }

        Dictionary<string, GameObject> d = new Dictionary<string, GameObject> ();
        d["N/A"] = null;
        beardTree.Insert (0, d);
    }

    private void UpdateSkin ()
    {
        Gamestate.gender = gender;
        Gamestate.skinColor = skinColor;
        Gamestate.costume = costume;
        Gamestate.costumeIndex = costumeIndex;
        Gamestate.costumeVariantIndex = costumeVariantIndex;

        // Skin
        string k = gender + "-" + skinColor;
        GameObject newAsset = assetTree[k][costume][costumeVariantIndex];
        Gamestate.avatarMaterials = newAsset.transform.Find ("Base").GetComponent<SkinnedMeshRenderer> ().sharedMaterials;
        avatar.UpdateSkin (Gamestate.avatarMaterials);

        genderTmp.text = genderToString (gender);
        skinColorTmp.text = SkinColorToString (skinColor);

        string variantName = newAsset.name.Substring (newAsset.name.IndexOf ('0'));
        costumeTmp.text = costume;
        costumeVariantTmp.text = variantName;

        // Hair
        string hairColorText = hairTree[gender.ToString ()][hairStyleIndex].Keys.ElementAt (hairColorIndex);
        GameObject newHair = hairTree[gender.ToString ()][hairStyleIndex][hairColorText];
        avatar.UpdateHair (Instantiate (newHair));
        Gamestate.hair = newHair.name;

        hairColorTmp.text = hairColorText;
        hairTmp.text = "Hair Style #" + (hairStyleIndex + 1);

        // Face
        GameObject newFace = faceTree[gender + "_" + skinColor][faceIndex];
        avatar.UpdateFace (Instantiate (newFace));
        Gamestate.face = newFace.name;
        faceTmp.text = "Face #" + (faceIndex + 1);

        // Beard
        string beardColor = beardTree[beardStyleIndex].Keys.ElementAt (beardColorIndex);
        GameObject newBeard = beardTree[beardStyleIndex][beardColor];

        avatar.UpdateBeard (newBeard == null ? newBeard : Instantiate (newBeard));
        Gamestate.beard = newBeard == null ? null : newBeard.name;

        beardColorTmp.text = beardColor;
        beardTmp.text = newBeard == null ? "No beard" : "Beard Style #" + beardStyleIndex;
    }

    private string genderToString (Gender gender)
    {
        if (gender == Gender.Female) return "Female";
        else return "Male";
    }

    private string SkinColorToString (SkinColor skinColor)
    {
        switch (skinColor)
        {
            case SkinColor.Black: return "Black";
            case SkinColor.Brown: return "Brown";
            case SkinColor.White: return "White";
        }

        throw new System.Exception ("Skin color not found");
    }

    private int rotate (int value, int maxValue, Direction direction)
    {
        return direction == Direction.Left ? rotateLeft (value, maxValue) : rotateRight (value, maxValue);
    }

    private int rotateLeft (int value, int maxValue)
    {
        if (value == 0)
            value = maxValue - 1;
        else
            value--;

        return value;
    }

    private int rotateRight (int value, int maxValue)
    {
        if (value == maxValue - 1)
            value = 0;
        else
            value++;

        return value;
    }

    private Gender tryParseGender (string gender)
    {
        switch (gender)
        {
            case "Female": return Gender.Female;
            case "Male": return Gender.Male;

            default: throw new ArgumentException ("Unknown gender");
        }
    }

    private SkinColor tryParseSkinColor (string skinColor)
    {
        switch (skinColor)
        {
            case "Black": return SkinColor.Black;
            case "Brown": return SkinColor.Brown;
            case "White": return SkinColor.White;

            default: throw new ArgumentException ("Unknown skin color");
        }


    }
}

public enum Direction { Left, Right }