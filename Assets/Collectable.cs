using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : Collidable
{
    void Update()
    {
        if(CollidingWith(player.gameObject.transform)){
            Destroy(this.gameObject);
            player.AddScore();
        }
    }
}
