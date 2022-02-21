using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public PlayerController playerController;

    private Vector3 lastPlayerPosiotion;
    private float distanceToMove;
    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        lastPlayerPosiotion = playerController.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        distanceToMove = playerController.transform.position.x - lastPlayerPosiotion.x;

        transform.position = new Vector3(transform.position.x + distanceToMove, transform.position.y, transform.position.z);

        lastPlayerPosiotion = playerController.transform.position;
    }
}
