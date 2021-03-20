using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class AppearanceSelector : MonoBehaviour
{
    public GameObject costumeObject;
    public GameObject hairObject;
    public GameObject genderGameObject;
    public GameObject costumeGameObject;
    public GameObject skinColorGameObject;

    private TextMeshProUGUI genderTmp;
    private TextMeshProUGUI costumeTmp;
    private TextMeshProUGUI skinColorTmp;

    enum Gender { Female, Male }
    private Gender gender = Gender.Female;

    enum SkinColor { Black, Brown, White };
    private SkinColor skinColor = SkinColor.Black;

    private string costume;
    private int costumeIndex = 0;
    private int costumeVariantIndex = 0;

    private GameObject[] allSkins;
    private Dictionary<string, Dictionary<string, List<GameObject>>> assetTree;

    private

    // Start is called before the first frame update
    void Start ()
    {
        allSkins = Resources.LoadAll<GameObject> ("Character/Costume/");
        assetTree = new Dictionary<string, Dictionary<string, List<GameObject>>> ();
        BuildAssetTree ();

        costume = assetTree[gender + "-" + skinColor].Keys.ToList ()[costumeIndex];

        genderTmp = genderGameObject.GetComponent<TextMeshProUGUI> ();
        costumeTmp = costumeGameObject.GetComponent<TextMeshProUGUI> ();
        skinColorTmp = skinColorGameObject.GetComponent<TextMeshProUGUI> ();
        UpdateSkin ();
    }

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetKeyDown (KeyCode.G))
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

        if (Input.GetKeyDown (KeyCode.S))
        {
            skinColor++;
            if ((int)skinColor >= Enum.GetValues (typeof (SkinColor)).Length)
            {
                skinColor = 0;
            }
            UpdateSkin ();
        }

        if (Input.GetKeyDown (KeyCode.C))
        {
            List<string> costumeList = assetTree[gender + "-" + skinColor].Keys.ToList ();

            costumeVariantIndex = 0;

            if (costumeIndex < costumeList.Count () - 1)
                costumeIndex++;
            else
                costumeIndex = 0;

            costume = costumeList[costumeIndex];

            UpdateSkin ();
        }

        if (Input.GetKeyDown (KeyCode.V))
        {
            if (costumeVariantIndex < assetTree[gender + "-" + skinColor][costume].Count () - 1)
            {
                costumeVariantIndex++;
            }
            else
            {
                costumeVariantIndex = 0;
            }

            UpdateSkin ();

        }
    }

    private void BuildAssetTree ()
    {
        foreach (GameObject skin in allSkins)
        {
            string[] elements = skin.name.Split (' ');

            Gender assetGender = elements[0] == "Female" ? Gender.Female : Gender.Male;

            SkinColor assetSkinColor;

            if (elements[1] == "White")
                assetSkinColor = SkinColor.White;
            else if (elements[1] == "Brown")
                assetSkinColor = SkinColor.Brown;
            else
                assetSkinColor = SkinColor.Black;

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

    private void UpdateSkin ()
    {
        string k = gender + "-" + skinColor;
        GameObject newAsset = assetTree[k][costume][costumeVariantIndex];
        costumeObject.GetComponent<SkinnedMeshRenderer> ().materials = newAsset.transform.Find ("Base").GetComponent<SkinnedMeshRenderer> ().sharedMaterials;
        genderTmp.text = genderToString (gender);
        skinColorTmp.text = SkinColorToString (skinColor);
        costumeTmp.text = newAsset.name;

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
}