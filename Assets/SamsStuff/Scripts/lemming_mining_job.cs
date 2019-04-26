using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class lemming_mining_job : MonoBehaviour
{
    public string[] tagToCheck;

    private int _rotation = 1;
    
    public void DestroyMapRadius()
    {
        var positionToMine = transform.position;
        var length = transform.localScale;

        if (transform.eulerAngles.y >= 180)
            _rotation = -1;
        length.x += 2;
        positionToMine.x += length.x * _rotation;
        positionToMine.y -= 1;
        
        var objectsInRange = Physics.OverlapBox(positionToMine, length);
        foreach (Collider col in objectsInRange)
        {
            foreach (var value in tagToCheck)
            {
                if (col.gameObject.tag == value)
                    Destroy(col.gameObject);
            }
        }
    }

    public void SetFacingWallTag()
    {
        RaycastHit hit;
        var positionToHit = transform.position;

        positionToHit.y++;
        if (Physics.Raycast(positionToHit, transform.TransformDirection(Vector3.forward) * 100, out hit))
            hit.transform.gameObject.tag = "wall";
        if (Physics.Raycast(positionToHit, transform.TransformDirection(Vector3.back) * 100, out hit))
            hit.transform.gameObject.tag = "wall";
    }
}