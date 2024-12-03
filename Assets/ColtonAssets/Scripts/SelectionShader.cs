using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionShader : MonoBehaviour
{
    private Material[] materials;
    private int selection = 1;
    private int target = 2;
    void Start()
    {
        materials = GetComponent<SkinnedMeshRenderer>().materials;
        SelectionOff();
        TargetOff();
    }

    public void SelectionOn()
    {
        materials[selection].SetFloat("_Visability", 5);
    }
    public void SelectionOff()
    {
        materials[selection].SetFloat("_Visability", 0);
    }
    public void TargetOn()
    {
        materials[target].SetFloat("_Visability", 5);
        Debug.Log("ononon");
    }
    public void TargetOff()
    {
        materials[target].SetFloat("_Visability", 0);
        Debug.Log("oooofffff");
    }

}
