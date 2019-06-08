using UnityEngine;

public class lemming_mining_job : MonoBehaviour
{
    public string[] tagToCheck;

    private int _rotation = 1;
    
    public void DestroyMapRadius()
    {
        var positionToMine = transform.position;
        var length = new Vector3(1f, 0.1f, 0f);

        if (transform.eulerAngles.y >= 180)
            _rotation = -1;
        positionToMine.x *= _rotation;
        positionToMine.y += 0.25f;
        
        var objectsInRange = Physics.OverlapBox(positionToMine, length);
        foreach (Collider col in objectsInRange)
        {
            foreach (var value in tagToCheck)
            {
                if (col.gameObject.tag == value && col.gameObject != this.gameObject)
                {
                    Destroy(col.gameObject);
                }
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