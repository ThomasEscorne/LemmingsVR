using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTexture : MonoBehaviour
{
    public enum EDirection {
        Down,
        Up,
        Left,
        Right
    }

    public float scrollSpeed;

    public EDirection Direction;

   Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction;

        float offset = Time.time * scrollSpeed;

        if (Direction == EDirection.Down)
            direction = new Vector2(0, offset * -1);
        else if (Direction == EDirection.Up)
            direction = new Vector2(0, offset);
        else if (Direction == EDirection.Left)
            direction = new Vector2(offset * -1, 0);
        else
            direction = new Vector2(offset, 0);


        rend.material.SetTextureOffset("_MainTex", direction);
    }
}
