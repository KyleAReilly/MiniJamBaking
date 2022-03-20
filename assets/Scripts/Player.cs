using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    #region Variables
    public GameObject cake;
    public GameObject cookies;
    public GameObject gameOverScreen; 
    [SerializeField] float speed = 1f;
    [SerializeField] AudioClip wrongSound;
    [SerializeField] AudioClip correctSound;
    [SerializeField] AudioSource playerAudio;
    [SerializeField] GameObject correctParticles;
    [SerializeField] string[] recipe = new string[]{"Cake", "Cookies"};
    private int rngRecipe;
    [SerializeField] int sugarNeeded;
    [SerializeField] int flourNeeded;
    [SerializeField] int sugarCollected;
    [SerializeField] int flourCollected;
    int butterCollected;
    int butterNeeded;

    int milkCollected;
    int milkNeeded;
    int eggsNeeded;
    int eggsCollected;
    public Text recipeText;
    public Text[] ingredientText;
    public Text timerText;
    public float timer = 120;
    private bool counting = true;
    public GameObject penalty;
    public Animator gameOver;
    private bool gameEnd =false;

    #endregion


    // Start is called before the first frame update
    void Start()
    {
        //Choose random Recipe
        rngRecipe = Random.Range(0, recipe.Length);  //assigns random index value within recipe array to rngRecipe as an int
        if(recipe[rngRecipe] == "Cake") //Runs if random recipe chosen is "Cake"
        {
            //This block assigns ingredients the player must collect to complete recipe
            sugarNeeded = 3; 
            milkNeeded = 2;
            eggsNeeded = 3;
            flourNeeded = 4;
            //

            recipeText.text = "Cake"; //display UI for Cake Recipe
       
        } else if (recipe[rngRecipe] == "Cookies") //Runs if random recipe chosen has the same value as "Cookies" in the array

        {
            //cookie: flour, sugar, butter, eggs
            //This block determines ingredients players need to collect
            sugarNeeded = 4;
            butterNeeded = 2;
            eggsNeeded = 4;
            flourNeeded = 2;
            
            recipeText.text = "Cookies"; //Display UI for Cookies Recipe
        }




    }

    // Update is called once per frame
    void Update()
    {
        //A bool to determine if timer has reached 0
        if(counting){ //if counting is true run code
        //update timer
        if (timer > 0){
        float minutes = Mathf.FloorToInt(timer / 60); 
        float seconds = Mathf.FloorToInt(timer % 60);
        timer -= Time.deltaTime; //Counts timer value down in realtime

        timerText.text = string.Format("{0:00}:{1:00}",minutes, seconds); //UI timer formatting
        } else {
            counting = false; // Timer has hit 0, Player is out of time
        }
        } else 
        {
            //GAME OVER 
            GameOver();
        }

        
        //Checks inputs and controls character based on them 
        CharacterController();
        
        //Checks for character position and transforms them to other side of screen if pos is above 9 or below -9
        ScreenWrap();

        playerAudio = GetComponent<AudioSource>();
        //PLAY AUDIO LINE
        // playerAudio.PlayOneShot(wrongSound, 1.0f);
        // playerAudio.PlayOneShot(correctSound, 1.0f);

        #region Ingredient Display

        if(recipe[rngRecipe] == "Cake")
        {
            //This block is the players display of ratio from ingredients collected to ingredients needed in total
            ingredientText[0].text = sugarCollected + "/" + sugarNeeded + "   Sugar"; 
            ingredientText[1].text = milkCollected + "/" + milkNeeded + "   Milk"; 
            ingredientText[2].text = eggsCollected + "/" + eggsNeeded + "   Eggs"; 
            ingredientText[3].text = flourCollected + "/" + flourNeeded + "   Flour"; 
            //

            recipeText.text = "Cake";
            //display UI for Cake Recipe

             ingredientText[0].text = sugarCollected + "/" + sugarNeeded + "   Sugar";
        } else if (recipe[rngRecipe] == "Cookies"){
             //This block is the players display of ratio from ingredients collected to ingredients needed in total
            ingredientText[0].text = sugarCollected + "/" + sugarNeeded + "   Sugar"; 
            ingredientText[1].text = butterCollected + "/" + butterNeeded + "   Butter"; 
            ingredientText[2].text = eggsCollected + "/" + eggsNeeded + "   Eggs"; 
            ingredientText[3].text = flourCollected + "/" + flourNeeded + "   Flour"; 
            //
        
            //Display UI for Cookies Recipe
            recipeText.text = "Cookies";
        }
        #endregion
        /* Quick Restart for testing
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Main");
        }
        */
    }
    
    //Control Character
    void CharacterController()
    {
        //read for inputs
        if(Input.GetKey(KeyCode.LeftArrow)) //Checks for left inputs
        {
            transform.position += new Vector3( -speed, 0, 0) * Time.deltaTime; //(Get; Set)
            //To optimize place this Vextor in variable and call variable

        } else if(Input.GetKey(KeyCode.RightArrow))//Checks for right arrow inputs
        {
            transform.position += new Vector3(speed,0,0)*Time.deltaTime;
            //To optimize place this Vextor in variable and call variable
        }
    }  
    void ScreenWrap() // This function is meant to create a screen wrap effect on the player character
    {
        if(transform.position.x >= 9) //needs to be assigned to variable. XBounds
        {
            transform.position = new Vector3( -8.9f, transform.position.y, transform.position.z);//Transforms player character to other side of player display
        } else if (transform.position.x <= -8.9) //ASSIGN VARIABLE
        {
            transform.position = new Vector3( 8.9f, transform.position.y, transform.position.z);//Transforms player character to other side of player display
        }
    }
private void OnTriggerEnter2D(Collider2D other) {
    
    if(!gameEnd){
switch (rngRecipe)
{
    case 0: 

    Debug.Log("Cake was chosen as recipe");
    
    

    

    
    //if collide with proper ingredients make dings and  update UI
    if(other.CompareTag("Sugar") && sugarCollected < sugarNeeded )
    {
        
        //Plays appropriate audio for collecting correct item
        playerAudio.PlayOneShot(correctSound);
        sugarCollected++;

        //Shoots Star particles out in celebration of collecting correct item
         GameObject Clone = Instantiate(correctParticles, new Vector3(transform.position.x, transform.position.y, -3), Quaternion.identity);
         Destroy(Clone, .5f);        
    } else if (other.CompareTag("Milk") && milkCollected < milkNeeded)
    {
        playerAudio.PlayOneShot(correctSound);
        milkCollected++;
         GameObject Clone = Instantiate(correctParticles, new Vector3(transform.position.x, transform.position.y, -3), Quaternion.identity);
         Destroy(Clone, .5f); 
    } 
    else if (other.CompareTag("Eggs") && eggsCollected < eggsNeeded)
    {
        playerAudio.PlayOneShot(correctSound);
        eggsCollected++;
         GameObject Clone = Instantiate(correctParticles, new Vector3(transform.position.x, transform.position.y, -3), Quaternion.identity);
         Destroy(Clone, .5f); 

    } else if (other.CompareTag("Flour") && flourCollected < flourNeeded)
    {
        playerAudio.PlayOneShot(correctSound);
        flourCollected++;
         GameObject Clone = Instantiate(correctParticles, new Vector3(transform.position.x, transform.position.y, -3), Quaternion.identity);
         Destroy(Clone, .5f); 

    } else if (flourCollected == flourNeeded && eggsCollected == eggsNeeded && sugarCollected == sugarNeeded && milkNeeded == milkCollected)
    {
        //YOU WIN 
        //PLAY ENDING ANIMATION AND SHOW RESULT
        cake.SetActive(true);
        YouWin();


    } else 
    {
        playerAudio.PlayOneShot(wrongSound);
        timer -= 10;
        GameObject clone1 = Instantiate(penalty, penalty.transform.position, penalty.transform.rotation);
        Destroy(clone1, 1);
       
    }



    break;
    case 1: 
    Debug.Log("Cookies was chosen as recipe");
    //if collide with proper ingredients make dings and  update UI

     
    
    

    

    
    //if collide with proper ingredients make dings and  update UI
    if(other.CompareTag("Sugar") && sugarCollected < sugarNeeded )
    {
        
        //Plays appropriate audio for collecting correct item
        playerAudio.PlayOneShot(correctSound);
        sugarCollected++;

        //Shoots Star particles out in celebration of collecting correct item
         GameObject Clone = Instantiate(correctParticles, new Vector3(transform.position.x, transform.position.y, -3), Quaternion.identity);
         Destroy(Clone, .5f);        
    } else if (other.CompareTag("Butter") && butterCollected < butterNeeded)
    {
        playerAudio.PlayOneShot(correctSound);
        butterCollected++;
         GameObject Clone = Instantiate(correctParticles, new Vector3(transform.position.x, transform.position.y, -3), Quaternion.identity);
         Destroy(Clone, .5f); 
    } 
    else if (other.CompareTag("Eggs") && eggsCollected < eggsNeeded)
    {
        playerAudio.PlayOneShot(correctSound);
        eggsCollected++;
         GameObject Clone = Instantiate(correctParticles, new Vector3(transform.position.x, transform.position.y, -3), Quaternion.identity);
         Destroy(Clone, .5f); 

    } else if (other.CompareTag("Flour") && flourCollected < flourNeeded)
    {
        playerAudio.PlayOneShot(correctSound);
        flourCollected++;
         GameObject Clone = Instantiate(correctParticles, new Vector3(transform.position.x, transform.position.y, -3), Quaternion.identity);
         Destroy(Clone, .5f); 

    } else if (flourCollected == flourNeeded && eggsCollected == eggsNeeded && sugarCollected == sugarNeeded && milkNeeded == milkCollected)
    {
        //YOU WIN 
        //PLAY ENDING ANIMATION AND SHOW RESULT
        cookies.SetActive(true);
        YouWin();
    } else 
    {
       
       

       
        playerAudio.PlayOneShot(wrongSound);
        timer -= 10;
        GameObject clone1 = Instantiate(penalty, penalty.transform.position, penalty.transform.rotation);
        Destroy(clone1, 1);
       
    }
    break;
    }
    }
Destroy(other.gameObject);
    
   
        
    }
    void GameOver()
    {
        gameEnd = true;
        Debug.Log(gameEnd);
        //END IT ALL GGS MATE
        Debug.Log("Get rekt");
      
        gameOverScreen.SetActive(true);
        gameOver.SetBool("GameOver", true);
       // Time.timeScale = 0;
      

    }
    void YouWin()
    {
        gameOver.SetBool("YouWin", true);
       // Time.timeScale = 0;
        


    }
}


