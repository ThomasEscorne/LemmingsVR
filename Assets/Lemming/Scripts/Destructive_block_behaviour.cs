using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Destructive_block_behaviour : MonoBehaviour
{
    public int popCollapse = 4;
    private bool hasCollapse = false;
    private TextMeshPro infoText;

    private void Start()
    {
        infoText = transform.GetChild(0).GetComponent<TextMeshPro>();
        infoText.text = "x" + popCollapse;
    }

    public void addPerson()
    { popCollapse++; infoText.text = "x" + popCollapse; }
    public void substractPerson()
    {
        popCollapse--;
        if (!hasCollapse && popCollapse <= 0)
        {
            hasCollapse = true;
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<BoxCollider>().enabled = false;
            infoText.text = "";
        }
        else if (!hasCollapse)
        {
            infoText.text = "x" + popCollapse;
        }
    }
}
