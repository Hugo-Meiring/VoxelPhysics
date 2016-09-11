using UnityEngine;
using System.Collections;

public class ClickOnFaceScript : MonoBehaviour {
    // Public fields are visible and their values can be changed dirrectly in the editor
    // represents the position displacement needed to compute the position of the new instance
    public Vector3 delta;

    // This function is triggered when the mouse cursor is over the GameObject on which this script runs
    void OnMouseOver()
    {
        // If the left mouse button is pressed
        if (Input.GetMouseButtonDown(0))
        {
            // Display a message in the console tab
            Debug.Log("Left click!");
            // Destroy the parent of the face we clicked
            Destroy(this.transform.parent.gameObject);
        }

        if (Input.GetMouseButtonDown(1))
        {
            // Display a message in Console tab
            Debug.Log("Right click!");
            // Call method from generator class
            WorldGenerator.CloneAndPlace(this.transform.parent.transform.position + delta, this.transform.parent.gameObject);
        }
    }
}