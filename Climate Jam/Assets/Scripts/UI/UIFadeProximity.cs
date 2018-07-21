using UnityEngine.UI;
using UnityEngine;

public class UIFadeProximity : MonoBehaviour {

    [SerializeField]
    private float min_Fade_Distance;
    [SerializeField]
    private float max_Fade_Distance;

    private Image[] childImages;

    private void Start()
    {
        childImages = transform.GetComponentsInChildren<Image>();
    }

    // Update is called once per frame
    void FixedUpdate () {

        float distFromCamera = Vector3.Distance(Camera.main.transform.position, gameObject.transform.position);

        //if distance from camera is less than the minimum, it has 100% alpha
        if (distFromCamera < min_Fade_Distance)
        {
            for(int i = 0;i < childImages.Length; i++)
            {
                childImages[i].color  = new Color(childImages[i].color.r, childImages[i].color.g, childImages[i].color.b,1);
            }
        }
 
        // if distance from camera is greater than max, 0% alpha 
        else if(distFromCamera > max_Fade_Distance)
        {
            for (int i = 0; i < childImages.Length; i++)
            {
                childImages[i].color = new Color(childImages[i].color.r, childImages[i].color.g, childImages[i].color.b, 0);

            }
        }
        //if distance from camera is between the min and max, reduce the alpha proportionally 
        else if (distFromCamera > min_Fade_Distance && distFromCamera < max_Fade_Distance)
        {
            for (int i = 0; i < childImages.Length; i++)
            {
                float alphaValue = 1.0f - Mathf.Clamp01((distFromCamera - min_Fade_Distance) / (max_Fade_Distance - min_Fade_Distance));
                childImages[i].color = new Color(childImages[i].color.r, childImages[i].color.g, childImages[i].color.b, alphaValue);

            }
        }
    }
}
