using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public float forwardForce = 2000f;
    public float sidewaysForce = 500f;

    void FixedUpdate()
    {
        rb.AddForce(0, 0, forwardForce * Time.deltaTime);

        if (Input.GetKey("d"))
        {
            //rb.AddForce(sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
            Command moveRight = new MoveRight(rb, sidewaysForce);
            Invoker invoker = new Invoker();
            invoker.SetCommand(moveRight);
            invoker.ExecuteCommand();
        }

        if (Input.GetKey("a"))
        {
            //rb.AddForce(-sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
            Command moveLeft = new MoveLeft(rb, sidewaysForce);
            Invoker invoker = new Invoker();
            invoker.SetCommand(moveLeft);
            invoker.ExecuteCommand();
        }

        if (rb.position.y < -1f)
        {
            FindObjectOfType<GameManager>().EndGame();
        }
    }

    public void ResetPosition()
    {
        transform.position = new Vector3(0.0f, 0.0f, 0.0f);
    }

}
