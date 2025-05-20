using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField] private GameObject player;

    public void Init(GameObject player)
    {
        this.player = player;
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -100);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -100);
    }
}
