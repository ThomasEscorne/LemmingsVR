using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class lemming_mining_job : MonoBehaviour
{
    public string[] tagToCheck;

    private int _rotation = 1;
    
    public void DestroyMapRadius()
    {
        var positionToMine = transform.position;
//        var length = transform.localScale;
        var length = new Vector3(0.3f, 0.3f, 0.3f);

        if (transform.eulerAngles.y >= 180)
            _rotation = -1;
        //length.x += 2;
        positionToMine.x += 0.3f * _rotation;
        positionToMine.y -= 0.2f;
        
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

        length.x = 0.5f;
        length.y -= 0.2f;
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
}