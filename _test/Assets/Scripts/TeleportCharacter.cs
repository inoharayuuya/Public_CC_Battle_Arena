using UnityEngine;

public class TeleportCharacter : MonoBehaviour
{
    public float distance = 5f;
    public float moveDelay = 2f;
    private bool canMove = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canMove)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 charPos = transform.position;
            Vector2 direction = mousePos - charPos;
            Vector2 targetPos = charPos + direction.normalized * distance;
            transform.position = targetPos;
            canMove = false;
            Invoke("EnableMovement", moveDelay);
        }
    }

    void EnableMovement()
    {
        canMove = true;
    }
}
