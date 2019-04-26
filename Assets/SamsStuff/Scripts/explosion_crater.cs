using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosion_crater : MonoBehaviour
{
    public string[] tagToCheck;

    public float explosionRadius = 0.5f;

    
    //On collision enter is just used for testing purposes,
    //When a lemming explodes, just send its current position to the DestroyMapRadius method
    
    private void OnCollisionEnter(Collision other)
    {
        DestroyMapRadius(other.gameObject.GetComponent<Transform>().position, explosionRadius);
        Destroy(gameObject);
    }

    void DestroyMapRadius(Vector3 location, float radius)
    {
        Collider[] objectsInRange = Physics.OverlapSphere(location, radius);
        
        foreach (Collider col in objectsInRange)
        {
            foreach (var value in tagToCheck)
            {
                if (col.gameObject.tag == value)
                    Destroy(col.gameObject);                
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        
        Gizmos.DrawWireSphere(gameObject.transform.position, explosionRadius);

    }
}
