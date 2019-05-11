using System;
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
        //length.x += 2;
        positionToMine.x += length.x * _rotation;
        positionToMine.y -= 2;
        
        var objectsInRange = Physics.OverlapBox(positionToMine, length);
        foreach (Collider col in objectsInRange)
        {
            foreach (var value in tagToCheck)
            {
                if (col.gameObject.tag == value)
                {
                    Destroy(col.gameObject);                    
                }
            }
        }

        length.x++;
        length.y -= 1;
        var setToWalls = Physics.OverlapBox(positionToMine, length);
        foreach (Collider col in setToWalls)
        {
            col.transform.gameObject.tag = "wall";
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
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Check that it is being run in Play Mode, so it doesn't try to draw this in Editor mode
        //Draw a cube where the OverlapBox is (positioned where your GameObject is as well as a size)
        var positionToMine = transform.position;

        positionToMine.x += transform.localScale.x;
        positionToMine.y -= 2;
        //positionToMine.x += 2;
        positionToMine.y -= 1;
        
        
        Gizmos.DrawWireCube(positionToMine, transform.localScale);
    }
}