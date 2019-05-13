using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTexture : MonoBehaviour
{
    public enum Direction
    {
        Down,
        Up,
        Left,
        Right
    }
    public float ScrollSpeed;

    public Direction Way;

    Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float offset = Time.time * ScrollSpeed;
        Vector2 way;

        if (Way == Direction.Down)
            way = new Vector2(0, offset * -1);
        else if (Way == Direction.Up)
            way = new Vector2(0, offset);
        else if (Way == Direction.Left)
            way = new Vector2(offset *-1, 0);
        else
            way = new Vector2(offset, 0);


        rend.material.SetTextureOffset("_MainTex", way);
    }
}
