using UnityEngine;

public class GameOverTrigger : MonoBehaviour
{
    private float time = 0;
    private float maxTime = 2.0f;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!GameManager.instance.isGameOver)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Planet"))
            {
                time += Time.deltaTime;
                if (time >= maxTime)
                {
                    GameManager.instance.isGameOver = true;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!GameManager.instance.isGameOver)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Planet"))
            {
                time = 0.0f;
            }
        }
    }
}
