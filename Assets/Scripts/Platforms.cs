using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platforms : MonoBehaviour
{
    public List<Platform> allPlatforms = new List<Platform>();

    void Start()
    {
        //Finds all platform gameobjects and adds them to the list "allPlatforms"
        foreach (Platform platform in FindObjectsOfType<Platform>())
        {
            allPlatforms.Add(platform);
        }

        //Debug.Log(allPlatforms.Count);
    }


}
