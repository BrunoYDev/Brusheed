using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private GameObject player;


    [SerializeField] float leftLimit;
    [SerializeField] float rightLimit;
    [SerializeField] float topLimit;
    [SerializeField] float bottomLimit;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3
            (
                Mathf.Clamp
                (
                    player.transform.position.x,
                    leftLimit,
                    rightLimit
                ),

                Mathf.Clamp
                (
                    player.transform.position.y,
                    bottomLimit,
                    topLimit
                ),
                transform.position.z
            );
    }
}
