using UnityEngine;
using UnityEngine.UI;

public class StoryBackgroundController : MonoBehaviour
{
    public Image image;
    private bool isMovingUp = false;
    private int num_click = 0;
    private Vector2 targetPosition = Vector2.zero;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (num_click<2)
            {
                targetPosition = new Vector2(image.rectTransform.anchoredPosition.x, -650f);
                isMovingUp = true;
            }
            else
            {
                targetPosition = new Vector2(image.rectTransform.anchoredPosition.x, -250f);
                isMovingUp = false;
            }
            num_click++;
        }

        if (isMovingUp)
        {
            image.rectTransform.anchoredPosition = Vector2.MoveTowards(image.rectTransform.anchoredPosition, targetPosition, Time.deltaTime * 500f);
        }
        else if (num_click>7)
        {
            image.rectTransform.anchoredPosition = Vector2.MoveTowards(image.rectTransform.anchoredPosition, targetPosition, Time.deltaTime * 500f);
        }
    }
}