using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionShader : MonoBehaviour
{
    private Material[] materials;
    public int materialIndex = 1;
    void Start()
    {
        materials = GetComponent<SkinnedMeshRenderer>().materials;
        SelectionOff();
    }

    public void SelectionOn()
    {
        materials[materialIndex].SetFloat("_Visability", 5);
    }
    public void SelectionOff()
    {
        materials[materialIndex].SetFloat("_Visability", 0);
    }
}
