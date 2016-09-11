using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class WorldGenerator : MonoBehaviour {
    // Public fields are visible and their values can be changed dirrectly in the editor

    // Drag and drop here the Voxel from the Scene
    // Used to create new instaces
    public GameObject Voxel;
    public GameObject player;
    public FirstPersonController controller;

    // Specify the dimensions of the world
    public float SizeX;
    public float SizeZ;
    public float SizeY;
    public int minHeight;

    // Use this for initialization
    void Start () {
        // Start the world generation coroutine
        // StartCoroutine function always returns immediately, however you can yield the result. 
        StartCoroutine(SimpleGenerator());
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.P))
        {
            player.transform.position = player.transform.position + new Vector3(0, 100, 0);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            player.transform.position = player.transform.position + new Vector3(0, -100, 0);
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            player.GetComponent<CharacterController>().height = player.GetComponent<CharacterController>().height * 1.2f;
            controller.m_RunSpeed = controller.m_RunSpeed * 1.2f;
            controller.m_WalkSpeed = controller.m_WalkSpeed * 1.2f;
            
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            player.GetComponent<CharacterController>().height = player.GetComponent<CharacterController>().height * 0.8f;
            controller.m_RunSpeed = controller.m_RunSpeed * 0.8f;
            controller.m_WalkSpeed = controller.m_WalkSpeed * 0.8f;
        }
    }

    public static void CloneAndPlace(Vector3 newPosition, GameObject originalGameObject)
    {
        // Clone
        GameObject clone = (GameObject)Instantiate(originalGameObject, newPosition, Quaternion.identity);
        // Place
        clone.transform.position = newPosition;
        // Rename
        clone.name = "Voxel@" + clone.transform.position;
    }

    /* from docs.unity.com
    * The execution of a coroutine can be paused at any point using the  yield statement. 
    * The yield return value specifies when the coroutine is resumed. 
    * Coroutines are excellent when modelling behaviour over several frames. 
    * Coroutines have virtually no performance overhead. 
    * StartCoroutine function always returns immediately, however you can yield the result. 
    * This will wait until the coroutine has finished execution.
    */
    IEnumerator SimpleGenerator()
    {
        // In this Coroutine we will instantiate 50 voxels per frame
        uint numberOfInstances = 0;
        uint instancesPerFrame = 50;

        for (int x = 0; x < SizeX; x++)
        {
            for (int z = 0; z < SizeZ; z++)
            {
                // Compute a random height
                float height = Random.Range(minHeight - 1, SizeY);
                for (int y = 0; y <= height; y++)
                {
                    // Compute the position for every voxel
                    Vector3 newPosition = new Vector3(x, y, z);
                    // Call the method giving the new position and a Voxel instance as parameters
                    CloneAndPlace(newPosition, Voxel);
                    // Increment numberOfInstances
                    numberOfInstances++;

                    // If the number of instaces per frame was met
                    if (numberOfInstances == instancesPerFrame)
                    {
                        // Reset numberOfInstances
                        numberOfInstances = 0;
                        yield return new WaitForEndOfFrame();
                    }
                }
            }
        }
    }

}
