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
    public GameObject costumeVariantGameObject;
    public GameObject skinColorGameObject;
    public GameObject customizationPanel;
    public GameObject mainPanel;

    private TextMeshProUGUI genderTmp;
    private TextMeshProUGUI costumeTmp;
    private TextMeshProUGUI costumeVariantTmp;
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

    void Start ()
    {
        allSkins = Resources.LoadAll<GameObject> ("Character/Costume/");
        assetTree = new Dictionary<string, Dictionary<string, List<GameObject>>> ();
        BuildAssetTree ();

        costume = assetTree[gender + "-" + skinColor].Keys.ToList ()[costumeIndex];

        genderTmp = genderGameObject.GetComponent<TextMeshProUGUI> ();
        costumeTmp = costumeGameObject.GetComponent<TextMeshProUGUI> ();
        skinColorTmp = skinColorGameObject.GetComponent<TextMeshProUGUI> ();
        costumeVariantTmp = costumeVariantGameObject.GetComponent<TextMeshProUGUI> ();
        UpdateSkin ();
    }

    public void OnCharacterButtonPress ()
    {
        customizationPanel.SetActive (true);
        mainPanel.SetActive (false);
    }

    public void OnCharacterBackButtonPress ()
    {
        customizationPanel.SetActive (false);
        mainPanel.SetActive (true);
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

    private void BuildAssetTree ()
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

    private void UpdateSkin ()
    {
        string k = gender + "-" + skinColor;
        GameObject newAsset = assetTree[k][costume][costumeVariantIndex];
        costumeObject.GetComponent<SkinnedMeshRenderer> ().materials = newAsset.transform.Find ("Base").GetComponent<SkinnedMeshRenderer> ().sharedMaterials;
        genderTmp.text = genderToString (gender);
        skinColorTmp.text = SkinColorToString (skinColor);

        string variantName = newAsset.name.Substring (newAsset.name.IndexOf ('0'));
        costumeTmp.text = costume;
        costumeVariantTmp.text = variantName;

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