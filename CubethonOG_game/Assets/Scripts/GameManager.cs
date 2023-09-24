using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    bool gameHasEnded = false;
    public float restartDelay = 1f;
    public GameObject completeLevelUI;
    public GameObject replayUI;
    public GameObject whoopsUI;
    bool instantReplay = false;
    GameObject player;
    float replayStartTime;
    public AudioSource grunt;
    public GameObject colorFilterUI;

    private void OnEnable()
    {
        PlayerCollision.OnHitObstacle += EndGame;
        PlaySound.OnHitPlaySound += PlaySound_OnPlaySound;
        ColorChange.OnHitColorChange += ColorChange_OnColorChange;

    }

    private void OnDisable()
    {
        PlayerCollision.OnHitObstacle -= EndGame;
        PlaySound.OnHitPlaySound -= PlaySound_OnPlaySound;
        ColorChange.OnHitColorChange += ColorChange_OnColorChange;

    }

    // Start is called before the first frame update
    void Start()
    {
        PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();
        player = playerMovement.gameObject;

        if (CommandLog.commands.Count > 0)
        {
            instantReplay = true;
            replayStartTime = Time.timeSinceLevelLoad;
        }

    }

    private void PlaySound_OnPlaySound(Collision playSound)
    {
        player.GetComponent<PlayerMovement>().enabled = false;
        if (playSound != null)
        {
            grunt.Play();

        }
    }

    private void ColorChange_OnColorChange(Collision colorChange)
    {
        player.GetComponent<PlayerMovement>().enabled = false;
        ColorChange.OnHitColorChange += ColorChange_OnColorChange;
        if (colorChange != null)
        {
            colorFilterUI.SetActive(true);

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (instantReplay)
        {
            RunInstantReplay();
        }
    }
    public void CompleteLevel()
    {
        Debug.Log("win");
        completeLevelUI.SetActive(true);
    }

    public void EndGame(Collision collisionInfo)
    {
        player.GetComponent<PlayerMovement>().enabled = false;
        PlayerCollision.OnHitObstacle -= EndGame;

        if (collisionInfo != null)
        {
            Debug.Log("Hit: " + collisionInfo.collider.name);
            whoopsUI.SetActive(true);
            //grunt.Play();
         
        }

        // this flag prevents responding to multiple hit events:
        if (!gameHasEnded)
        {
            gameHasEnded = true;
            Invoke("Restart", 2f);
        }
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void RunInstantReplay()
    {
        if (CommandLog.commands.Count == 0)
        {
            return;
        }

        replayUI.SetActive(true);
        Command command = CommandLog.commands.Peek();
        if (Time.timeSinceLevelLoad >= command.timestamp) // + replayStartTime)
        {
            command = CommandLog.commands.Dequeue();
            command._player = player.GetComponent<Rigidbody>();
            Invoker invoker = new Invoker();
            invoker.disableLog = true;
            invoker.SetCommand(command);
            invoker.ExecuteCommand();
        }
    }
}