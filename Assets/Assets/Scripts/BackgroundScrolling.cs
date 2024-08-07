using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScrolling : MonoBehaviour
{
    public GameObject background1; // Tấm nền 1
    public GameObject background2; // Tấm nền 2
    public float scrollSpeed = 2f; // Tốc độ di chuyển
    public float backgroundHeight = 25f; // Chiều cao của nền

    void Update()
    {
        // Di chuyển các tấm nền xuống
        background1.transform.Translate(Vector3.down * scrollSpeed * Time.deltaTime);
        background2.transform.Translate(Vector3.down * scrollSpeed * Time.deltaTime);

        // Kiểm tra nếu background1 ra khỏi màn hình
        if (background1.transform.position.y < -backgroundHeight)
        {
            ResetPosition(background1, background2);
        }

        // Kiểm tra nếu background2 ra khỏi màn hình
        if (background2.transform.position.y < -backgroundHeight)
        {
            ResetPosition(background2, background1);
        }
    }

    void ResetPosition(GameObject bgToReset, GameObject bgOther)
    {
        bgToReset.transform.position = new Vector3(
            bgToReset.transform.position.x,
            bgOther.transform.position.y + backgroundHeight,
            bgToReset.transform.position.z);
    }
}
