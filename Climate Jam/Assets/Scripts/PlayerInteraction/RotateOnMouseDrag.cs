using UnityEngine;

public class RotateOnMouseDrag : MonoBehaviour {

    private Vector2 mouse_Click_Start;
    [SerializeField]
    private float globe_Rotate_Speed_Deg = 5f;
    public void OnMouseDown()
    {
        //store the first mouse click
        mouse_Click_Start = Input.mousePosition;
    }
    public void OnMouseDrag()
    {
        //if the mouse's current horizontal position is on the right of the starting click
        if(Input.mousePosition.x > mouse_Click_Start.x)
        {
            gameObject.transform.Rotate(new Vector3(0, globe_Rotate_Speed_Deg * Time.deltaTime, 0));
        }
        //if the mouse's current horizontal position is on the left of the starting click
        else if (Input.mousePosition.x < mouse_Click_Start.x)
        {
            gameObject.transform.Rotate(new Vector3(0, -globe_Rotate_Speed_Deg * Time.deltaTime, 0));
        }
    }
}
