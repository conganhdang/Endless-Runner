using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondGenerator : MonoBehaviour
{
    public ObjectPooler diamondPool;

    public float distanceBetweenCoins;
    
    public void SpawnDiamond(Vector3 startPoint)
    {
        GameObject diamond1 = diamondPool.GetPooledObject();
        diamond1.transform.position = startPoint;
        diamond1.SetActive(true);

        GameObject diamond2 = diamondPool.GetPooledObject();
        diamond2.transform.position = new Vector3(startPoint.x - distanceBetweenCoins, startPoint.y, startPoint.z);
        diamond2.SetActive(true);

        GameObject diamond3 = diamondPool.GetPooledObject();
        diamond3.transform.position = new Vector3(startPoint.x + distanceBetweenCoins, startPoint.y, startPoint.z);
        diamond3.SetActive(true);
    }
}
