using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    public bool open = false;
    public float moveTime = 3f;

    private Coroutine curCoroutine;

    void Start()
    {
        Vector3 rot = transform.rotation.eulerAngles;
        rot.z = (open) ? 0 : -90;
        transform.rotation = Quaternion.Euler(rot);
    }

    public void Interact()
    {
        open = !open;

        if (curCoroutine != null)
            StopCoroutine(curCoroutine); // Stop previous coroutine
        curCoroutine = StartCoroutine(SmoothRotation((open) ? 0 : -90));
    }

    private IEnumerator SmoothRotation(float zAngle)
    {
        Quaternion from = transform.rotation;
        Quaternion to = transform.rotation;
        to =  Quaternion.Euler ( new Vector3 (from.eulerAngles.x, from.eulerAngles.y,  zAngle) );
    
        float elapsed = 0.0f;
        while (elapsed < moveTime)
        {
            transform.rotation = Quaternion.Slerp(from, to, elapsed / moveTime );
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.rotation = to;
    }
}
