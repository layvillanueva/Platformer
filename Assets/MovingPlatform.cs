using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : Collidable
{
    private float startX;
    private float startY;
    public float endX;
    public float endY;

    private float speed;
    private string direction;
    private string goingTo;

    void Start()
    {
        objTransform = this.gameObject.transform;

        x = objTransform.position.x;
        width = objTransform.localScale.x;

        y = objTransform.position.y;
        height = objTransform.localScale.y;

        player = (PlayerController)FindObjectOfType(typeof(PlayerController));

        startX = x;
        startY = y;

        speed = 1;

        if(startX > endX){
            direction = "left";
            goingTo = "endX";
        } else if (startX < endX){
            direction = "right";
            goingTo = "endX";
        } else if (startY > endY){
            direction = "down";
            goingTo = "endY";
        } else if (startY < endY){
            direction = "up";
            goingTo = "endY";
        }
    }

    void Update()
    {
        if (direction.Equals("left")){
            x = x - speed * Time.deltaTime;
        } else if (direction.Equals("right")){
            x = x + speed * Time.deltaTime;
        } else if (direction.Equals("down")){
            y = y - speed * Time.deltaTime;
        } else if (direction.Equals("up")){
            y = y + speed * Time.deltaTime;
        }

        if (goingTo.Substring(goingTo.Length-1, 1).Equals("X")){
            if (goingTo.Equals("endX")){
                if (direction.Equals("left") && x <= endX){
                    x = endX;
                    goingTo = "startX";
                    direction = "right";
                } else if (direction.Equals("right") && x >= endX){
                    x = endX;
                    goingTo = "startX";
                    direction = "left";
                }
            } else if (goingTo.Equals("startX")){
                if (direction.Equals("left") && x <= startX){
                    x = startX;
                    goingTo = "endX";
                    direction = "right";
                } else if (direction.Equals("right") && x >= startX){
                    x = startX;
                    goingTo = "endX";
                    direction = "left";
                }
            }

        } else if (goingTo.Substring(goingTo.Length-1, 1).Equals("Y")){
            if (goingTo.Equals("endY")){
                if (direction.Equals("down") && y <= endY){
                    y = endY;
                    goingTo = "startY";
                    direction = "up";
                } else if (direction.Equals("up") && y >= endY){
                    y = endY;
                    goingTo = "startY";
                    direction = "down";
                }
            } else if (goingTo.Equals("startY")){
                if (direction.Equals("down") && y <= startY){
                    y = startY;
                    goingTo = "endY";
                    direction = "up";
                } else if (direction.Equals("up") && y >= startY){
                    y = startY;
                    goingTo = "endY";
                    direction = "down";
                }
            }
        }

        objTransform.position = new Vector3(x, y, 0);

        if(CollidingWith(player.gameObject.transform)){
            player.IsCollidingWithObject(this, x, y, width, height);
        }
    }
}
