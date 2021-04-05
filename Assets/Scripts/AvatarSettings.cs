using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarSettings : MonoBehaviour
{
    SkinnedMeshRenderer skinnedMeshRenderer;
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
}

public enum Gender { Female, Male }
public enum SkinColor { Black, Brown, White };