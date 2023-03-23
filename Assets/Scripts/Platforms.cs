using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Platforms : MonoBehaviour
{
    public List<Platform> allPlatforms = new List<Platform>();

    const float forwardJumpDistance = 4.34f;
    const float verticalJumpDistance = 2.17f;

    void Start()
    {
        //Finds all platform gameobjects and adds them to the list "allPlatforms"
        foreach (Platform platform in FindObjectsOfType<Platform>().OrderBy(m => m.transform.GetSiblingIndex()).ToArray())
        {
            allPlatforms.Add(platform);
        }

        //Debug.Log(allPlatforms.Count);
    }

    public void AddNextPlatform()
    {
        Platform lastPlatform = allPlatforms.Last();
        Platform newPlatform = allPlatforms.First();
        allPlatforms.RemoveAt(0);
        Transform transform = newPlatform.gameObject.transform;
        transform.position = lastPlatform.gameObject.transform.position;

        //Move platform up
        transform.position += Vector3.up * verticalJumpDistance;

        //Move platform left or right
        System.Random rand = new System.Random();
        if (rand.Next(0, 2) == 1)
        {
            transform.position += Vector3.left * forwardJumpDistance;
        }
        else
        {
            transform.position += Vector3.right * forwardJumpDistance;
        }

        allPlatforms.Add(newPlatform);
    }
}
