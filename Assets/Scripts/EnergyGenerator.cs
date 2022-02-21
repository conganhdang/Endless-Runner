using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyGenerator : MonoBehaviour
{
    public ObjectPooler energyPool;

    public float distanceBetweenCoins;
    
    public void SpawnEnergy(Vector3 startPoint)
    {
        // GameObject energy1 = energyPool.GetPooledObject();
        // energy1.transform.position = startPoint;
        // energy1.SetActive(true);

        GameObject energy6 = energyPool.GetPooledObject();
        energy6.transform.position = new Vector3(startPoint.x, startPoint.y + distanceBetweenCoins, startPoint.z);
        energy6.SetActive(true);

        GameObject energy2 = energyPool.GetPooledObject();
        energy2.transform.position = new Vector3(startPoint.x - distanceBetweenCoins, startPoint.y, startPoint.z);
        energy2.SetActive(true);

        GameObject energy3 = energyPool.GetPooledObject();
        energy3.transform.position = new Vector3(startPoint.x + distanceBetweenCoins, startPoint.y, startPoint.z);
        energy3.SetActive(true);

        // GameObject energy4 = energyPool.GetPooledObject();
        // energy4.transform.position = new Vector3(startPoint.x - distanceBetweenCoins, startPoint.y + distanceBetweenCoins, startPoint.z);
        // energy4.SetActive(true);

        // GameObject energy5 = energyPool.GetPooledObject();
        // energy5.transform.position = new Vector3(startPoint.x + distanceBetweenCoins, startPoint.y + distanceBetweenCoins, startPoint.z);
        // energy5.SetActive(true);
    }
}
