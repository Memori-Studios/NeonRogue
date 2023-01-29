using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingRandomizer : MonoBehaviour
{
    public void RandomizeBuilding()
    {
        int randomNumber = UnityEngine.Random.Range(0, transform.childCount);

        foreach (Transform child in transform)
            child.gameObject.SetActive(false);

        transform.GetChild(randomNumber).gameObject.SetActive(true);
    }
}
