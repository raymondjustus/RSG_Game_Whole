using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour {

    
    public bool hasTurbo;
    private Rigidbody2D rb;
	private float moveHorizontal;
    private float slowMoveHorizontal;
	private bool isStrafing;
	private float speed;
    private Vector2 noVMovement;
    private Vector2 fastVMovement;
    private Vector2 slowVMovement;
    private Vector2 turboMovement;
    private bool offRoad;
    private float screenWidth;

    //Achievement Counters
    private int appleCounter;
    private int correctsCounter;
    private bool didPerfectGame;

    public Text victoryText;

    // variables get initialized
    void Start () {

        //car does not start with any charges of turbo
        hasTurbo = false;
        //the rigidbody2d on the car
		rb = GetComponent<Rigidbody2D> ();
        //the fast horizontal speed of the car
		moveHorizontal = 7;
        //the slow horizontal speed of the car
        slowMoveHorizontal = 5;
        //the vector of the forward driving force while on the road
        fastVMovement = new Vector2(0, 10);
        //the vector of the forward driving force while off the road
        slowVMovement = new Vector2(0, 3);
        //the vector of the forward driving force after answering a question correctly
        turboMovement = new Vector2(0, 100);
        //the number of apples picked up on this run
        appleCounter = 0;
        //the number of questions answered correctly
        correctsCounter = 0;
        //keeping track of perfect game
        didPerfectGame = true;
        //setting screenwidth to the.... width of the screen ofc
        screenWidth = Screen.width;


        print("THIS SHOULD BE 0 THEN 1 " + PlayerInfo.RacesRan);

        if (!PlayerPrefs.HasKey("Races"))
        {
            print("BING 1");
            PlayerPrefs.SetInt("Races", 0);
            PlayerPrefs.Save();
        } else
        {
            print("BING 2");
            PlayerPrefs.SetInt("Races", PlayerPrefs.GetInt("Races"));
            PlayerPrefs.Save();
        }
    }

	// Use this for initialization
	void FixedUpdate () {
        int i = 0;


        //DELETE THISSSSSS
        //hasTurbo = true;

        while (i < Input.touchCount)
        {

            if (Input.GetKey("left") && Input.GetKey("right"))
            {
                MoveForward();
                //NormalizeCar();
                return;
            }


            // looks to see that the car is being pushed right, and isnt going too fast
			//TOUCH CONTROLS
            //if ((Input.GetTouch(i).position.x > screenWidth / 2) && rb.velocity.x < 20)
			// KEY CONTROLS
			if (Input.GetKey("right") && rb.velocity.x < 20)
			{
                //pushes the car to the right at a speed dependent on whether or not the car is on the road
                if (offRoad)
                {
                    if (rb.velocity.x > 10)
                    {
                        //do nothing, wait to slow down
                    }
                    else
                    {
                        rb.AddForce(new Vector2(slowMoveHorizontal, 0));
                    }
                }
                else
                {
                    rb.AddForce(new Vector2(moveHorizontal, 0));
                }

                //turns the car 1 degree per tick as it banks to the right
                if (transform.localEulerAngles.z > 145)
                {
                    BankRight();
                }
                // if the car is still tilted left when a right turn begins, gives turning a 2x boost right to help the car get there
                if (transform.localEulerAngles.z > 180)
                {
                    BankRight();
                }

                // looks to see that the car is being pushed right, and isnt going too fast
            }
			//TOUCH CONTROLS
            //else if ((Input.GetTouch(i).position.x < screenWidth / 2) && rb.velocity.x > -20)
			else if (Input.GetKey("left") && rb.velocity.x > -20)
			{
                //pushes the car to the left at a speed dependent on whether or not the car is on the road
                if (offRoad)
                {
                    if (rb.velocity.x < -10)
                    {
                        //do nothing, wait to slow down
                    }
                    else
                    {
                        rb.AddForce(new Vector2(-slowMoveHorizontal, 0));
                    }
                }
                else
                {
                    rb.AddForce(new Vector2(-moveHorizontal, 0));
                }

                //turns the car 1 degree per tick as it banks to the left
                if (transform.localEulerAngles.z < 215)
                {
                    BankLeft();
                }

                // if the car is still tilted right when a left turn begins, gives turning a 2x boost left to help the car get there
                if (transform.localEulerAngles.z < 180)
                {
                    BankLeft();
                }

                //otherwise, when no buttons are pressed, return the car to a neutral status
            }
            else
            {
                //this doesnt do anything, i dont think
                NormalizeCar();
            }
            ++i;
        }
        if (Input.touchCount == 0)
        {
            NormalizeCar();
        }
        //keeps the car perpetually moving forward
        MoveForward();

        //if the player has a charge and hits spacebar, gives a boost equal to a correct answer
        if (hasTurbo && Input.GetKey("space"))
        {
            hasTurbo = false;
            rb.AddForce(3 * turboMovement);
        }
        //print(offRoad);
        
    }

    //handles triggers the car is currently residing in
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Path"))
        {
            offRoad = false;
            
        } else if (other.gameObject.CompareTag("QuestionActivate"))
        {
            //do nothing
        } else 
        {
            offRoad = true;
        }

        // if the guess is correct, boost car and add to counter for achievement
        if (other.gameObject.CompareTag("CorrectAns"))
        {
            rb.AddForce(turboMovement);
            correctsCounter++;
            print("GOGOGOGO");
        }

        // if incorrect, nothing happens. Perfect game is lost
        if (other.gameObject.CompareTag("IncorrectAns"))
        {
            didPerfectGame = false;
        }
    }

    //if the player has a charge and hits turbo button, gives a boost equal to a correct answer
    public void hitTurbo()
    {
        //if the player has a charge and hits spacebar, gives a boost equal to a correct answer
        if (hasTurbo)
        {
            hasTurbo = false;
            rb.AddForce(3 * turboMovement);
        }
    }

    //handles triggers the car enters (namely, pickups)
    private void OnTriggerEnter2D(Collider2D other)
    {
        // if the car runs into an apple...
        if (other.gameObject.CompareTag("TurboPickup"))
        {
            // remove the apple from the game
            other.gameObject.SetActive(false);
            //and if the player didn't have a turbo charge, it does now
            if (!hasTurbo)
            {
                hasTurbo = true;
            }
            //increase the apple counter for achievements
            appleCounter++;
        }

        // THE RACE IS FINISHED. Begin countdown to mainmenu and push all gathered info to static script
        if (other.gameObject.CompareTag("FinishLine"))
        {
            // the victory screen
            victoryText.gameObject.SetActive(true);
            //getting and storing the name of the scene
            string thisScene = SceneManager.GetActiveScene().name;

            // pushing updated stats to static sheet
            PlayerInfo.ApplesPicked += appleCounter;
            PlayerInfo.CorrAnswers += correctsCounter;

            // SAVES THE NUMBER OF RACES
            if (!PlayerPrefs.HasKey("Races"))
            {
                print("Why is this happening?");
                PlayerPrefs.Save();
            }
            int tmpRaces = PlayerPrefs.GetInt("Races");
            print("This is tmp " + tmpRaces);
            PlayerPrefs.SetInt("Races", tmpRaces + 1);
            print("This is PP " + PlayerPrefs.GetInt("Races"));
            PlayerInfo.RacesRan = PlayerPrefs.GetInt("Races");
            print("This is PI " + PlayerInfo.RacesRan);
            tmpRaces = PlayerPrefs.GetInt("Races");
            print("This is tmp 2 " + tmpRaces);
            PlayerPrefs.Save();
            

            // check for and update perfect games
            if (thisScene == "Level 1")
            {
                CheckForPerfectGame(PlayerInfo.HPerfectGame);
            } else if (thisScene == "Level 2")
            {
                CheckForPerfectGame(PlayerInfo.MPerfectGame);
            } else if (thisScene == "Level 3")
            {
                CheckForPerfectGame(PlayerInfo.EPerfectGame);
            }

            // start countdown to main menu
            //StartCoroutine(ExecuteAfterTime(5));
            SceneManager.LoadScene("MainMenu");

        }
    }

    //marco helper that turns the car one degree to the left (counterclockwise)
    void BankLeft()
    {
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z + 1);
    }

    //macro helper that turns the car on degree to the right (clockwise)
    void BankRight()
    {
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z - 1);
    }

    //macro helper that moves the car forward at a speed dependent on whether or not it is on the road
    void MoveForward()
    {
        if (offRoad)
        {
            if (rb.velocity.y < 3)
            {
                rb.AddForce(slowVMovement);
            }
        }
        else
        {
            if (rb.velocity.y < 10)
            {
                rb.AddForce(fastVMovement);
            }
        }
    }

    void NormalizeCar()
    {
        //adds counter force to stop the car from drifting
        if (rb.velocity.x > 0)
        {
            rb.AddForce(new Vector2(-moveHorizontal, 0));

        }
        //adds counter force to stop the car from drifting
        if (rb.velocity.x < 0)
        {
            rb.AddForce(new Vector2(moveHorizontal, 0));
        }
        //straighten out the car depending on the angle it is at
        if (transform.localEulerAngles.z < 180)
        {
            BankLeft();
        }
        if (transform.localEulerAngles.z > 180)
        {
            BankRight();
        }
    }

    //helper that checks if the player has alreay gotten a perfect game at this difficulty, and sets it if they haven't and just did
    public void CheckForPerfectGame(bool perfectGameInfo)
    {
            if (perfectGameInfo && didPerfectGame)
            {
                perfectGameInfo = didPerfectGame;
            }
    }

    //function that will move to main menu after given amount of time passes
    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene("MainMenu");
    }
}
