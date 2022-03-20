using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  #region Variables
    [SerializeField] GameObject[] ingredients; 
    [SerializeField] float spawnInterval= .5f;
    [SerializeField] float initialSpawnTime = 2;   
    [SerializeField] float screenSize = 10;    
    #endregion

    void Start()
    {
      InvokeRepeating("SpawnItem", initialSpawnTime, spawnInterval); //Runs SpawnItem function with defined variables. ("Function", initial delay time, Repeat time)
    }
    void SpawnItem()
    {
        //Chooses random ingredient and position and Insantiates it 
        int rngIngredient = Random.Range(0, ingredients.Length);//chooses a random object in our ingredients array

        Vector3 rngPOS = new Vector3(Random.Range(-screenSize,screenSize),screenSize,0); //saves transform(x,y,z) 0 for z as it is unused. x and y are determined by previously defined variable screenSize
        
        Instantiate(ingredients[rngIngredient], rngPOS, ingredients[rngIngredient].transform.rotation); //Instantiates(GameObject,Vector3, rotation) with our defined variables. 

    }      
}
