using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collidable : MonoBehaviour
{
    protected float x;
    protected float width;
    protected float y;
    protected float height;

    protected Transform objTransform;

    protected PlayerController player;

    void Start()
    {
        objTransform = this.gameObject.transform;

        x = objTransform.position.x;
        width = objTransform.localScale.x;

        y = objTransform.position.y;
        height = objTransform.localScale.y;

        if(this.gameObject.tag.Equals("Wall")){
            width += 0.1f;
        }

        player = (PlayerController)FindObjectOfType(typeof(PlayerController));
    }

    void Update()
    {
        if(CollidingWith(player.gameObject.transform)){
            player.IsCollidingWithObject(this, x, y, width, height);
        }
    }

    protected bool CollidingWith(Transform other)
    {
        Vector3 otherPosition = other.position;
        Vector3 otherScale = other.localScale;

        return ((x + width / 2 > otherPosition.x - otherScale.x / 2) &&
                (otherPosition.x + otherScale.x / 2 > x - width / 2) &&
                (y + height / 2 > otherPosition.y - otherScale.y / 2) &&
                (otherPosition.y + otherScale.y / 2 > y - height / 2));
    }
}
