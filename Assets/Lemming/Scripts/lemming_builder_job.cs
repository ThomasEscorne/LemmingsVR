using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Numerics;
using UnityEngine;
using UnityEngine.Tilemaps;
using Vector3 = UnityEngine.Vector3;

public class lemming_builder_job : MonoBehaviour
{
    private GameObject     _quad; 
    private Tilemap        tilemap;
    public GameObject    floor;
    private int        _rotation = 1;


    void Start()
    {
        tilemap = GameObject.Find("Tilemap").GetComponent<Tilemap>();
    }
    

    private void InitQuad()
    {
        PhysicMaterial physicMaterial = new PhysicMaterial();

        physicMaterial.dynamicFriction = 1f;
        physicMaterial.staticFriction = 1f;
        physicMaterial.bounciness = 0.0f;
        var tmpPosition = transform.position;
        var checkPosition = transform.position;
        float xOffset = 2f * _rotation;
        float yOffset = 1f;
        
        tmpPosition.x += xOffset;
        tmpPosition.y += yOffset;
        Vector3Int currentCell = tilemap.WorldToCell(tmpPosition);
        Vector3 cellCenter = tilemap.GetCellCenterWorld(currentCell);

        checkPosition.x += 1 * _rotation;
        Vector3Int checkCurrentCell = tilemap.WorldToCell(checkPosition);
        Vector3 checkCellCenter = tilemap.GetCellCenterWorld(checkCurrentCell);
        
        if (Physics.OverlapSphere(checkCellCenter,1).Length > 0)
        {
            GameObject fillerFloor = floor;
            fillerFloor.transform.position = checkCellCenter;
            Instantiate(fillerFloor, tilemap.transform.parent);
        }

        _quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
        _quad.transform.eulerAngles = new Vector3(45.0f, 90.0f * _rotation, 0);
        _quad.transform.position = cellCenter;
        _quad.tag = "ground";
        _quad.transform.parent = tilemap.transform;
        _quad.GetComponent<MeshCollider>().material = physicMaterial;
    }

    public GameObject InitGroundTiles(float xOffset, float yOffset)
    {
        var tmpPosition = transform.position;
        tmpPosition.x += xOffset;
        tmpPosition.y += yOffset;
        Vector3Int currentCell = tilemap.WorldToCell(tmpPosition);
        Vector3 cellCenter = tilemap.GetCellCenterWorld(currentCell);

        floor.transform.position = cellCenter;
        GameObject tile = Instantiate(floor, tilemap.transform.parent);
        return (tile);
    }

    public void CreateRamp(int nbFloorTiles)
    {
        if (transform.eulerAngles.y >= 180)
            _rotation = -1;
        float xOffset = 3f * _rotation;
        float yOffset = 1f;
        InitQuad();

        for (var i = 0; i < nbFloorTiles; i++)
        {
            GameObject tile = InitGroundTiles(xOffset, yOffset);
            if (i == 1)
                tile.gameObject.name = "built_floor";
            xOffset += _rotation;
        }
    }
}