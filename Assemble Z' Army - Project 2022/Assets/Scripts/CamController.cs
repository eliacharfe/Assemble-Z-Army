
using UnityEngine;

public class CamController : MonoBehaviour
{
    public float speed = 20f;

    public float scrollSpeed = 20f;

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;


        if (Input.GetKey("w"))
        {
            pos.z += speed * Time.deltaTime;
        }
        if (Input.GetKey("a"))
        {
            pos.z -= speed * Time.deltaTime;
        }
        if (Input.GetKey("s"))
        {
            pos.x += speed * Time.deltaTime;
        }
        if (Input.GetKey("d"))
        {
            pos.x -= speed * Time.deltaTime;
        }

        float scroll = Input.GetAxis("MouseScrollWheel");
        pos.y -= scroll * scrollSpeed * 100f * Time.deltaTime;
        Debug.Log(pos);
        transform.position = pos;
    }
}
