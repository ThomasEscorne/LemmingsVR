using UnityEngine;

public class lemming_mining_job : MonoBehaviour
{
    public string[] tagToCheck;

    private float _rotation = 0.5f;

    private Vector3 positionToMine;
    
    public void DestroyMapRadius()
    {
        positionToMine = gameObject.transform.position;
        var length = new Vector3(1f, 0.2f, 0f);

        if (transform.eulerAngles.y >= 180)
            _rotation = -0.5f;
        positionToMine.x += _rotation;
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
}