using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] public Vector3[] positions;
    public float moveTime = 5.0f;
    public uint startPos = 0;

    private uint currentPos;
    private Coroutine curCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        currentPos = startPos;
        if (currentPos >= positions.Length)
            currentPos = 0;

        if (positions.Length > 0)
            this.transform.localPosition = positions[currentPos];
    }

    private void Update()
    {
    }

    public void Move()
    {
        if (positions.Length < 1)
            return;

        currentPos++;
        if (currentPos >= positions.Length)
            currentPos = 0;

        if (curCoroutine != null)
            StopCoroutine(curCoroutine); // Stop previous coroutine
        curCoroutine = StartCoroutine(SmoothMovement(positions[currentPos]));
    }

    private IEnumerator SmoothMovement(Vector3 end)
    {
        float sqrRemainingDistance = (transform.localPosition - end).sqrMagnitude;
        float inverseMoveTime = 1 / moveTime;

        while (sqrRemainingDistance > float.Epsilon)
        {
            Vector3 newPosition = Vector3.MoveTowards(transform.localPosition, end, inverseMoveTime * Time.deltaTime);
            transform.localPosition = newPosition;
            sqrRemainingDistance = (transform.localPosition - end).sqrMagnitude;

            yield return null;
        }

    }
}
