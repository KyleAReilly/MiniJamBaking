using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class falling : MonoBehaviour
{

    [SerializeField] float fallSpeed;
    [SerializeField] float rng;
    [SerializeField] float yBounds = -8f; //bottom of players display

    [SerializeField] float low = 0f;
    [SerializeField] float high = 100f;
    
    void Start()
    {
        rng = Random.Range(low, high); //Chooses random value for rotation of objects
    }

    
    void Update()
    {
        transform.Translate(0,-fallSpeed * Time.deltaTime,0, Space.World); // Determines fall speed. 0 placeholders are for x and z and are not used as this is purely based on the Y axis
        transform.Rotate(0,0, rng * Time.deltaTime); //(x, y, z) Applies our defined rotation to the z axis and sets x and y axis' to 0. Multiply by delta time so objects act the same no matter ones CPU

        Destroy(); //Checks Y axis position and destroys object if off player display
    }
    //destroy function checks bounds and deletes objects to optimize game
    void Destroy()
    {
        if (gameObject.transform.position.y < yBounds) //Compares current Y axis to our defined Y axis boundary
        {
            Destroy(gameObject); // destroys object if it falls below boundary
        }
    }
}
