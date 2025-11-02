using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantComponent : MonoBehaviour
{
 
    public static readonly HashSet<PlantComponent> AllPlants = new HashSet<PlantComponent>();
 
    void Start()
    {
        AllPlants.Add(this);
    }
 
    void OnDestroy()
    {
        AllPlants.Remove(this);
    }
}
