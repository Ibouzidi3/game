using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarSettings : MonoBehaviour
{
    SkinnedMeshRenderer skinnedMeshRenderer;
    public GameObject hairObject;
    public GameObject faceObject;
    public GameObject beardObject;
    // Start is called before the first frame update
    void Start ()
    {
        skinnedMeshRenderer = this.transform.Find ("Base").GetComponent<SkinnedMeshRenderer> ();
        if (Gamestate.avatarMaterials != null)
        {
            skinnedMeshRenderer.materials = Gamestate.avatarMaterials;
        }
    }

    public void UpdateSkin (Material[] materials)
    {
        skinnedMeshRenderer.materials = materials;
    }

    public void UpdateHair (GameObject hair)
    {
        copyTransforms (hair.transform, hairObject.transform);
        Destroy (hairObject);
        hairObject = hair;
    }

    public void UpdateFace (GameObject face)
    {
        copyTransforms (face.transform, faceObject.transform);
        Destroy (faceObject);

        faceObject = face;

    }

    public void UpdateBeard (GameObject beard)
    {
        if (beard == null)
        {
            beardObject.SetActive (false);
        }
        else
        {
            beardObject.SetActive (true);
            copyTransforms (beard.transform, beardObject.transform);
            Destroy (beardObject);

            beardObject = beard;

        }
    }

    private void copyTransforms (Transform to, Transform from)
    {
        to.parent = from.parent;
        to.position = from.position;
        to.rotation = from.rotation;
        to.localScale = from.localScale;
    }
}

public enum Gender { Female, Male }
public enum SkinColor { Black, Brown, White };