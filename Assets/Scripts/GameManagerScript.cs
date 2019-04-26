using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK.Highlighters;
using VRTK;

public class GameManagerScript : MonoBehaviour
{
    public GameObject UmbrellaModel;
    public GameObject UmbrellaPrefab;
    public VRTK_ControllerEvents leftControllerEvents;
    public VRTK_ControllerEvents rightControllerEvents;

    private bool isHolding = false;
    private GameObject heldObj = null;

    private string leftControllerName = "[VRTK][AUTOGEN][LeftControllerScriptAlias][BasePointerRenderer_Origin_Smoothed]";
    private string rightControllerName = "[VRTK][AUTOGEN][RightControllerScriptAlias][BasePointerRenderer_Origin_Smoothed]";
    private string currentHolding = null;
    private string lastTouchpadClick = null;

    private string heldObjType;

    private void Start()
    {
        leftControllerEvents.TriggerPressed += leftControllerEvents_TriggerPressed;
        leftControllerEvents.TouchpadPressed += leftControllerEvents_TouchpadPressed;
        rightControllerEvents.TriggerPressed += rightControllerEvents_TriggerPressed;
        rightControllerEvents.TouchpadPressed += rightControllerEvents_TouchpadPressed;
    }

    public void OnSelect(GameObject target)
    {
        if (isHolding == false) {

            if (target.name == "UmbrellaSpawner")
            {
                Debug.Log("Umbrella spawn in your hand");
                heldObjType = "umbrella";
                heldObj = Instantiate(UmbrellaModel) as GameObject;
                heldObj.transform.parent = GameObject.Find(lastTouchpadClick).transform;
                currentHolding = lastTouchpadClick;
                heldObj.transform.localPosition = new Vector3(0, 0, 0);
                heldObj.transform.localEulerAngles = new Vector3(-50, 0, 0);
                heldObj.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
                isHolding = true;
            }
        }
        else
        {
            if (target.name.Contains("lemming"))
            {
                Debug.Log("Selected a lemming");

                if (lastTouchpadClick == currentHolding)
                {
                    if (heldObjType == "umbrella")
                    {
                        GameObject item = Instantiate(UmbrellaPrefab) as GameObject;
                        target.GetComponent<lemming_behavior>().give_object(item.GetComponent<LemmingObject>());
                    }
                }
            }
        }
    }

    private void leftControllerEvents_TriggerPressed(object sender, ControllerInteractionEventArgs e)
    {
        if (currentHolding == leftControllerName)
        {
            Destroy(heldObj);
            heldObj = null;
            currentHolding = null;
            isHolding = false;
        }
    }

    private void leftControllerEvents_TouchpadPressed(object sender, ControllerInteractionEventArgs e)
    {
        lastTouchpadClick = leftControllerName;
    }

    private void rightControllerEvents_TriggerPressed(object sender, ControllerInteractionEventArgs e)
    {
        if (currentHolding == rightControllerName)
        {
            Destroy(heldObj);
            heldObj = null;
            currentHolding = null;
            isHolding = false;
        }
    }

    private void rightControllerEvents_TouchpadPressed(object sender, ControllerInteractionEventArgs e)
    {
        lastTouchpadClick = rightControllerName;
    }
}