using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerPostionSetter : MonoBehaviour
{
    public Transform SpawnerObject;

    public Transform PlaceToSpawn;
    public void SetSpawnTransform()
    {
        SpawnerObject.transform.position = PlaceToSpawn.transform.position;

    }
}
