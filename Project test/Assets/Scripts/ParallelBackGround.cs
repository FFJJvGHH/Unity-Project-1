using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallelBackGround : MonoBehaviour
{
    private GameObject cam, obj;
    [SerializeField] private float parallaxEffect;
    private float xPosition, camxPosition;
    private float length;

    void Start()
    {
        cam = GameObject.Find("Virtual Camera");
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        xPosition = transform.position.x;

    }

    // Update is called once per frame
    void Update()
    {
        float distanceMoved = (cam.transform.position.x) * (1 - parallaxEffect);
        float distanceToMove = (cam.transform.position.x) * parallaxEffect;
        transform.position = new Vector3(xPosition + distanceToMove, transform.position.y);
        if (distanceMoved > xPosition + length)
        {
            xPosition = xPosition + length;
        }
        else if (distanceMoved < xPosition - length)
        {
            xPosition = xPosition - length;
        }
    }
}
