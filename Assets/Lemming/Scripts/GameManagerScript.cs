using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK.Highlighters;
using VRTK;

public class GameManagerScript : MonoBehaviour
{
    public GameObject UmbrellaModel;
    public GameObject UmbrellaPrefab;
    public GameObject HammerModel;
    public GameObject PickaxeModel;
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
                heldObj.transform.localScale = new Vector3(0.003f, 0.003f, 0.003f);
                isHolding = true;
            }

            if (target.name == "HammerSpawner")
            {
                Debug.Log("Hammer spawn in your hand");
                heldObjType = "hammer";
                heldObj = Instantiate(HammerModel) as GameObject;
                heldObj.transform.parent = GameObject.Find(lastTouchpadClick).transform;
                currentHolding = lastTouchpadClick;
                heldObj.transform.localPosition = new Vector3(0.0032f, 0.0199f, 0.0676f);
                heldObj.transform.localEulerAngles = new Vector3(-105.013f, -0.1560059f, -178.728f);
                heldObj.transform.localScale = new Vector3(0.004f, 0.004f, 0.004f);
                isHolding = true;
            }

            if (target.name == "PickaxeSpawner")
            {
                Debug.Log("Pickaxe spawn in your hand");
                heldObjType = "pickaxe";
                heldObj = Instantiate(PickaxeModel) as GameObject;
                heldObj.transform.parent = GameObject.Find(lastTouchpadClick).transform;
                currentHolding = lastTouchpadClick;
                heldObj.transform.localPosition = new Vector3(0.014f, 0.0065f, 0.0934f);
                heldObj.transform.localEulerAngles = new Vector3(0, -90, 0);
                heldObj.transform.localScale = new Vector3(1f, 1f, 1f);
                isHolding = true;
            }

            if (target.tag == "Elevator")
            {
                Debug.Log("Selecting elevator");
                Elevator elevator = target.GetComponentInParent<Elevator>();
                if (elevator)
                {
                    Debug.Log("Can slide");
                    elevator.Move();
                }
                else
                {
                    Debug.Log("Can't slide");
                }
            }

            if (target.tag == "Bridge")
            {
                Debug.Log("Selecting bridge");

                Bridge bridge = target.GetComponentInParent<Bridge>();
                if (bridge)
                {
                    Debug.Log("Bridge");
                    bridge.Interact();
                }

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
                    else if (heldObjType == "hammer")
                    {
                        //GameObject item = Instantiate(HammerPrefab) as GameObject;
                        Debug.Log("hammer");
                        target.GetComponent<lemming_behavior>().Build();
                    }
                    else if (heldObjType == "pickaxe")
                    {
                        //GameObject item = Instantiate(PickaxePrefab) as GameObject;
                        Debug.Log("pickaxe");

                        target.GetComponent<lemming_behavior>().Mine();
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