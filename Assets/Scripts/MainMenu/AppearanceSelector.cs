using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class AppearanceSelector : MonoBehaviour
{

    public AvatarSettings avatar;

    public GameObject hairObject;

    // Menu controller
    public MenuController menuController;

    // General panel
    public GameObject genderGameObject;
    public GameObject costumeGameObject;
    public GameObject costumeVariantGameObject;
    public GameObject skinColorGameObject;

    // General panel
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

    // Accessories panel
    public TextMeshProUGUI headgearTmp;
    public TextMeshProUGUI headgearTypeTmp;
    public TextMeshProUGUI faceAccessoryTmp;
    public TextMeshProUGUI faceAccessoryTypeTmp;
    public TextMeshProUGUI backAccessoryTmp;
    public TextMeshProUGUI backAccessoryTypeTmp;


    private TextMeshProUGUI faceTmp;

    private Gender gender = Gender.Female;
    private SkinColor skinColor = SkinColor.Black;

    // Hair
    private int hairColorIndex = 0;
    private int hairStyleIndex = 0;
    private CharacterAsset[] allHair;
    private Dictionary<string, List<Dictionary<string, CharacterAsset>>> hairTree;

    // Beard
    private int beardColorIndex = 0;
    private int beardStyleIndex = 0;
    private CharacterAsset[] allBeards;
    private List<Dictionary<string, CharacterAsset>> beardTree;


    // Skins
    private string costume;
    private int costumeIndex = 0;
    private int costumeVariantIndex = 0;
    private CharacterAsset[] allSkins;
    private Dictionary<string, Dictionary<string, List<CharacterAsset>>> assetTree;

    // Faces
    private CharacterAsset[] allFaces;
    private int faceIndex = 0;
    private Dictionary<string, List<CharacterAsset>> faceTree;

    // Headgears
    private int headgearIndex = 0;
    private int headgearTypeIndex = 0;
    private CharacterAsset[] allHeadgears;
    private Dictionary<string, Dictionary<string, CharacterAsset>> headgearTree;

    // Face accessories
    private int faceAccessoryIndex = 0;
    private int faceAccessoryTypeIndex = 0;
    private CharacterAsset[] allFaceAccessories;
    private Dictionary<string, Dictionary<string, CharacterAsset>> faceAccessoryTree;

    // Back accessories
    private int backAccessoryIndex = 0;
    private int backAccessoryTypeIndex = 0;
    private CharacterAsset[] allBackAccessories;
    private Dictionary<string, Dictionary<string, CharacterAsset>> backAccessoryTree;

    void Start ()
    {
        LoadSkins ();
        LoadHair ();
        LoadFaces ();
        LoadBeards ();
        LoadAccessories();
        UpdateSkin ();
    }

    void LoadSkins ()
    {
        allSkins = ResourceManager.LoadAll (ResourceType.Costume);
        assetTree = new Dictionary<string, Dictionary<string, List<CharacterAsset>>> ();
        BuildSkinTree ();

        costume = assetTree[gender + "-" + skinColor].Keys.ToList ()[costumeIndex];

        genderTmp = genderGameObject.GetComponent<TextMeshProUGUI> ();
        costumeTmp = costumeGameObject.GetComponent<TextMeshProUGUI> ();
        skinColorTmp = skinColorGameObject.GetComponent<TextMeshProUGUI> ();
        costumeVariantTmp = costumeVariantGameObject.GetComponent<TextMeshProUGUI> ();
    }

    void LoadHair ()
    {
        hairTree = new Dictionary<string, List<Dictionary<string, CharacterAsset>>> ();
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

    void LoadAccessories()
    {
        allHeadgears = ResourceManager.LoadAll(ResourceType.Headgear);
        allFaceAccessories = ResourceManager.LoadAll(ResourceType.FaceAccessory);
        allBackAccessories = ResourceManager.LoadAll(ResourceType.BackAccessory);

        headgearTree = BuildAccessoryTree(allHeadgears);
        faceAccessoryTree = BuildAccessoryTree(allFaceAccessories);
        backAccessoryTree = BuildAccessoryTree(allBackAccessories);
    }

    public void OnBuyButton(ResourceType resourceType)
    {

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

    public void OnHeadgearChange(bool left)
    {
        headgearTypeIndex = 0;
        headgearIndex = rotate(headgearIndex, headgearTree.Keys.Count, left ? Direction.Left : Direction.Right);
        UpdateSkin();
    }

    public void OnHeadgearTypeChange(bool left)
    {
        string type = headgearTree.Keys.ElementAt(headgearIndex);
        headgearTypeIndex = rotate(headgearTypeIndex, headgearTree[type].Keys.Count, left ? Direction.Left : Direction.Right);
        UpdateSkin();
    }


    public void OnFaceAccessoryChange(bool left)
    {
        faceAccessoryTypeIndex = 0;
        faceAccessoryIndex = rotate(faceAccessoryIndex, faceAccessoryTree.Keys.Count, left ? Direction.Left : Direction.Right);
        UpdateSkin();
    }

    public void OnFaceAccessoryTypeChange(bool left)
    {
        string type = faceAccessoryTree.Keys.ElementAt(faceAccessoryIndex);

        faceAccessoryTypeIndex = rotate(faceAccessoryTypeIndex, faceAccessoryTree[type].Keys.Count, left ? Direction.Left : Direction.Right);
        UpdateSkin();
    }

    public void OnBackAccessoryChange(bool left)
    {
        backAccessoryTypeIndex = 0;
        backAccessoryIndex = rotate(backAccessoryIndex, backAccessoryTree.Keys.Count, left ? Direction.Left : Direction.Right);
        UpdateSkin();
    }
    public void OnBackAccessoryTypeChange(bool left)
    {
        string type = backAccessoryTree.Keys.ElementAt(backAccessoryIndex);
        backAccessoryTypeIndex = rotate(backAccessoryTypeIndex, backAccessoryTree[type].Keys.Count, left ? Direction.Left : Direction.Right);
        UpdateSkin();
    }

    private void BuildSkinTree ()
    {
        foreach (CharacterAsset skin in allSkins)
        {
            string[] elements = skin.gameObject.name.Split (' ');

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
                assetTree.Add (key, new Dictionary<string, List<CharacterAsset>> ());
            }

            if (!assetTree[key].ContainsKey (costume))
            {
                assetTree[key].Add (costume, new List<CharacterAsset> ());
            }
            assetTree[key][costume].Add (skin);
        }
    }

    private void BuildHairTree ()
    {
        foreach (CharacterAsset hair in allHair)
        {
            string[] elements = hair.gameObject.name.Split (' ');

            Gender assetGender;

            if (elements[1] == "Female")
                assetGender = Gender.Female;
            else if (elements[1] == "Male")
                assetGender = Gender.Male;
            else
                continue;

            hair.gameObject.name = assetGender + "/" + hair.gameObject.name;

            int hairStyleIdx = int.Parse (elements[2]);
            string hairColor = elements[3];


            string genderStr = assetGender.ToString ();

            if (!hairTree.ContainsKey (genderStr))
            {
                hairTree[genderStr] = new List<Dictionary<string, CharacterAsset>> ();
            }

            if (hairTree[genderStr].Count < hairStyleIdx)
            {
                hairTree[genderStr].Add (new Dictionary<string, CharacterAsset> ());
            }
            hairTree[genderStr][hairStyleIdx - 1][hairColor] = hair;
        }
    }

    private void BuildFaceTree ()
    {
        faceTree = new Dictionary<string, List<CharacterAsset>> ();

        foreach (CharacterAsset face in allFaces)
        {
            string[] elements = face.gameObject.name.Split ('/').Last ().Split (' ');
            Gender assetGender = Gender.Female;
            try
            {
                assetGender = tryParseGender (elements[0]);
            }
            catch (ArgumentException e)
            {
                Debug.Log ("Unknown gender: " + face.gameObject.name);
            }

            SkinColor skinColor = tryParseSkinColor (elements[1]);

            face.gameObject.name = assetGender + "/" + face.gameObject.name;
            string label = assetGender + "_" + skinColor;

            if (!faceTree.ContainsKey (label))
            {
                faceTree[label] = new List<CharacterAsset> ();
            }

            faceTree[label].Add (face);
        }
    }

    private void BuildBeardTree ()
    {
        beardTree = new List<Dictionary<string, CharacterAsset>> ();
        foreach (CharacterAsset beard in allBeards)
        {
            string[] elements = beard.gameObject.name.Split (' ');
            int beardStyleIdx = int.Parse (elements[1]);
            string color = elements[2];

            if (beardTree.Count < beardStyleIdx)
            {
                beardTree.Add (new Dictionary<string, CharacterAsset> ());
            }
            beardTree[beardStyleIdx - 1][color] = beard;
        }

        Dictionary<string, CharacterAsset> d = new Dictionary<string, CharacterAsset> ();
        d["N/A"] = new CharacterAsset(null, 0);
        beardTree.Insert (0, d);
    }

    // Type > Flavour > Object
    private Dictionary<string, Dictionary<string, CharacterAsset>> BuildAccessoryTree(CharacterAsset[] accessories)
    {
        Dictionary<string, Dictionary<string, CharacterAsset>> accessoryTree = new Dictionary<string, Dictionary<string, CharacterAsset>>();

        foreach (CharacterAsset accessory in accessories)
        {
            string[] elements = accessory.gameObject.name.Split(' ');
            string accessoryType = elements[0];
            string flavour = String.Join(" ",elements.Skip(1));


            if (!accessoryTree.ContainsKey(accessoryType))
                accessoryTree[accessoryType] = new Dictionary<string, CharacterAsset>();

            accessoryTree[accessoryType][flavour] = accessory;
        }

        return accessoryTree;
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
        CharacterAsset newAsset = assetTree[k][costume][costumeVariantIndex];
        Gamestate.avatarMaterials = newAsset.gameObject.transform.Find ("Base").GetComponent<SkinnedMeshRenderer> ().sharedMaterials;
        avatar.UpdateSkin (Gamestate.avatarMaterials);

        genderTmp.text = genderToString (gender);
        skinColorTmp.text = SkinColorToString (skinColor);

        string variantName = newAsset.gameObject.name.Substring (newAsset.gameObject.name.IndexOf ('0'));
        costumeTmp.text = costume;
        costumeVariantTmp.text = variantName;
        menuController.costumePrice = newAsset.price;

        // Hair
        string hairColorText = hairTree[gender.ToString ()][hairStyleIndex].Keys.ElementAt (hairColorIndex);
        CharacterAsset newHair = hairTree[gender.ToString ()][hairStyleIndex][hairColorText];
        avatar.UpdateHair (Instantiate (newHair.gameObject));
        Gamestate.hair = newHair.gameObject.name;

        hairColorTmp.text = hairColorText;
        hairTmp.text = "Hair Style #" + (hairStyleIndex + 1);
        menuController.hairPrice = newHair.price;

        // Face
        CharacterAsset newFace = faceTree[gender + "_" + skinColor][faceIndex];
        avatar.UpdateFace (Instantiate (newFace.gameObject));
        Gamestate.face = newFace.gameObject.name;
        faceTmp.text = "Face #" + (faceIndex + 1);
        menuController.facePrice = newFace.price;

        // Beard
        string beardColor = beardTree[beardStyleIndex].Keys.ElementAt (beardColorIndex);
        CharacterAsset newBeard = beardTree[beardStyleIndex][beardColor];

        avatar.UpdateBeard(newBeard.gameObject == null ? newBeard.gameObject : Instantiate(newBeard.gameObject));
        Gamestate.beard = newBeard.gameObject == null ? null : newBeard.gameObject.name;

        beardColorTmp.text = beardColor;
        beardTmp.text = newBeard == null ? "No beard" : "Beard Style #" + beardStyleIndex;
        menuController.beardPrice = newBeard.price;


        // Headgear
        string headgear = headgearTree.Keys.ElementAt(headgearIndex);
        string headgearType = headgearTree[headgear].Keys.ElementAt(headgearTypeIndex);
        CharacterAsset newHeadgear = headgearTree[headgear][headgearType];

        avatar.UpdateHeadgear(newHeadgear.gameObject == null ? newHeadgear.gameObject : Instantiate(newHeadgear.gameObject));
        Gamestate.headgear = newHeadgear.gameObject == null ? null : newHeadgear.gameObject.name;

        headgearTmp.text = headgear;
        headgearTypeTmp.text = headgearType;
        menuController.headgearPrice = newHeadgear.price;

        // Face accessory
        string faceAccessory = faceAccessoryTree.Keys.ElementAt(faceAccessoryIndex);
        string faceAccessoryType = faceAccessoryTree[faceAccessory].Keys.ElementAt(faceAccessoryTypeIndex);
        CharacterAsset newFaceAccessory = faceAccessoryTree[faceAccessory][faceAccessoryType];

        avatar.UpdateFaceAccessory(newFaceAccessory.gameObject == null ? newFaceAccessory.gameObject : Instantiate(newFaceAccessory.gameObject));
        Gamestate.faceAccessory = newFaceAccessory.gameObject == null ? null : newFaceAccessory.gameObject.name;

        faceAccessoryTmp.text = faceAccessory;
        faceAccessoryTypeTmp.text = faceAccessoryType;
        menuController.faceAccessoryPrice = newFaceAccessory.price;

        // Back accessory
        string backAccessory = backAccessoryTree.Keys.ElementAt(backAccessoryIndex);
        string backAccessoryType = backAccessoryTree[backAccessory].Keys.ElementAt(backAccessoryTypeIndex);
        CharacterAsset newBackAccessory = backAccessoryTree[backAccessory][backAccessoryType];

        avatar.UpdateBackAccessory(newBackAccessory.gameObject == null ? newBackAccessory.gameObject : Instantiate(newBackAccessory.gameObject));
        Gamestate.backAccessory = newBackAccessory == null ? null : newBackAccessory.gameObject.name;

        backAccessoryTmp.text = backAccessory;
        backAccessoryTypeTmp.text = backAccessoryType;
        menuController.backAccessoryPrice = newBackAccessory.price;
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