using UnityEngine;
using System.Collections;

public class Wrap : MonoBehaviour
{
    public bool telep = false;
    private void Update()
    {
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);
        Vector3 moveAdjustment = Vector3.zero;
        if (viewportPosition.x < 0)
        {
            telep = true;
            moveAdjustment.x += 1;
        }
        else if (viewportPosition.x > 1)
        {
            telep = true;
            moveAdjustment.x -= 1;
        }
        else if (viewportPosition.y < 0)
        {
            telep = true;
            moveAdjustment.y += 1;
        }
        else if (viewportPosition.y > 1)
        {
            telep = true;
            moveAdjustment.y -= 1;
        }

        transform.position = Camera.main.ViewportToWorldPoint(viewportPosition + moveAdjustment);

    }

}