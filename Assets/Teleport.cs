using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : Collidable
{
    public float destinationX;
    public float destinationY;

    void Update()
    {
        if(CollidingWith(player.gameObject.transform)){
            player.TeleportTo(destinationX, destinationY);
        }
    }
}
