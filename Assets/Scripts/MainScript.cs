using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScript : MonoBehaviour
{
    public GameObject cube;
    public GameObject parent;
    private GameObject temporaryObject;

    void Update() 
    {
        SpawnCube();
    }

    private void SpawnCube()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            temporaryObject = Instantiate(cube, cube.transform.position, Quaternion.identity) as GameObject;
            temporaryObject.transform.SetParent(parent.transform);
            temporaryObject.transform.localPosition = new Vector3(-2, 30, 0);
        }
    }
}
