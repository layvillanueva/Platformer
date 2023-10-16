using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private float x;
    private float width;
    private float y;
    private float height;

    private PlayerController player;

    void Start()
    {
        Transform objTransform = this.gameObject.transform;

        x = objTransform.position.x;
        width = objTransform.localScale.x;

        y = objTransform.position.y;
        height = objTransform.localScale.y;

        player = (PlayerController)FindObjectOfType(typeof(PlayerController));
    }

    void Update()
    {
        if(CollidingWith(player.gameObject.transform)){
            player.IsCollidingWithObject(this.gameObject, x, y, width, height);
        }
    }

    bool CollidingWith(Transform other)
    {
        Vector3 otherPosition = other.position;
        Vector3 otherScale = other.localScale;

        return ((x + width / 2 > otherPosition.x - otherScale.x / 2) &&
                (otherPosition.x + otherScale.x / 2 > x - width / 2) &&
                (y + height / 2 > otherPosition.y - otherScale.y / 2) &&
                (otherPosition.y + otherScale.y / 2 > y - height / 2));
    }
}
