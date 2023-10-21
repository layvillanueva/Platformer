using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Transform objTransform;

    public float horizV;
    private float maxHorizV;
    public float horizA;
    private float decelMultiplier;
    private float x;

    private bool isGrounded;
    public float vertV;
    private float jumpHeight;
    private float gravity;
    private float y;

    private int score;
    public Text displayText;

    void Start()
    {
        objTransform = this.gameObject.transform;

        horizV = 0;
        maxHorizV = 6;
        horizA = 0;
        decelMultiplier = 16;
        x = 0;

        isGrounded = true;
        vertV = 0;
        jumpHeight = 3;
        gravity = -6;
        y = 1;

        score = 0;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow)) {
            if (horizV > 0){
                horizA = 0;
                horizV = 0;
            }

            if (horizV < -maxHorizV){
                horizA = 0;
                horizV = -maxHorizV;
            } else {
                horizA = horizA - 8 * Time.deltaTime;
            }

        } else if (Input.GetKey(KeyCode.RightArrow)) {
            if (horizV < 0){
                horizA = 0;
                horizV = 0;
            } 

            if (horizV > maxHorizV){
                horizA = 0;
                horizV = maxHorizV;
            } else {
                horizA = horizA + 8 * Time.deltaTime;
            }

        } else {
            if(isGrounded){
                decelMultiplier = 16;
            } else {
                decelMultiplier = 0.5f;
            }

            horizA = horizA - (horizV * decelMultiplier) * Time.deltaTime;

            if (Mathf.Round(horizV) == 0){
                horizA = 0;
                horizV = 0;
            }
        }

        horizV = horizV + horizA * Time.deltaTime;
        x = x + horizV * Time.deltaTime;


        if (isGrounded){
            if (Input.GetKeyDown(KeyCode.Space)){
                // formula from unity documentation
                vertV += Mathf.Sqrt(jumpHeight * -3 * gravity);

                isGrounded = false;
            }

            if(Mathf.Round(vertV) < 0)
                isGrounded = false;
        }

        vertV = vertV + gravity * Time.deltaTime;
        y = y + vertV * Time.deltaTime;

        objTransform.position = new Vector3(x, y, 0);

        displayText.text = score.ToString();
    }

    public void IsCollidingWithObject(Collidable other, float otherX, float otherY, float otherWidth, float otherHeight)
    {
        if(other.gameObject.tag.Equals("Wall")){
            float width = this.gameObject.transform.localScale.x;
            if (horizV > 0 && x - width / 2 < otherX - otherWidth / 2){
                horizA = 0;
                horizV = 0;
                x = otherX - otherWidth / 2 - width / 2;
            } else if (horizV < 0 && x + width / 2 > otherX + otherWidth / 2){
                horizA = 0;
                horizV = 0;
                x = otherX + otherWidth / 2 + width / 2;            
            }
        }

        if(other.gameObject.tag.Equals("Platform")){
            float height = this.gameObject.transform.localScale.y;
            if (vertV > 0){
                vertV = 0;
                y = otherY - otherHeight / 2 - height / 2 - 0.1f;
            } else if (vertV < 0){
                vertV = -1;
                y = otherY + otherHeight / 2 + height / 2;
                isGrounded = true;
            }
        }    
    }
    
    public void TeleportTo(float destinationX, float destinationY)
    {
        horizA = 0;
        horizV = 0;
        vertV = 0;

        x = destinationX;
        y = destinationY;
    }

    public void AddScore()
    {
        score++;
    }
}
