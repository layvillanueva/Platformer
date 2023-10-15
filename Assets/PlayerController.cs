using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Transform objTransform;

    public float horizV;
    private float maxHorizV;
    public float horizA;
    private float x;

    private bool isJumping;
    public float vertV;
    private float jumpHeight;
    private float gravity;
    private float y;

    void Start()
    {
        objTransform = this.gameObject.transform;

        horizV = 0;
        maxHorizV = 6;
        horizA = 0;
        x = 0;

        isJumping = false;
        vertV = 0;
        jumpHeight = 2.5f;
        gravity = -6;
        y = 1;
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
            horizA = horizA - (horizV * 16) * Time.deltaTime;

            if (Mathf.Round(horizV) == 0){
                horizA = 0;
                horizV = 0;
            }
        }

        horizV = horizV + horizA * Time.deltaTime;
        x = x + horizV * Time.deltaTime;


        if (!isJumping){
            if (Input.GetKeyDown(KeyCode.Space)){
                // formula from unity documentation
                vertV += Mathf.Sqrt(jumpHeight * -3 * gravity);

                isJumping = true;
            }
        }

        vertV = vertV + gravity * Time.deltaTime;
        y = y + vertV * Time.deltaTime;

        objTransform.position = new Vector3(x, y, 0);
    }

    public void IsCollidingWithObject(GameObject other)
    {
        if(!other.tag.Equals("Platform")){
            if (horizV > 0){
                horizA = 0;
                horizV = 0;
                x -= 0.01f;
            } else if (horizV < 0){
                horizA = 0;
                horizV = 0;
                x += 0.01f;            
            }
        }

        if (vertV > 0){
            vertV = 0;
            y -= 0.1f;
        } else if (vertV < 0){
            vertV = 0;
            y += 0.01f;
            isJumping = false;
        }
    }
}
