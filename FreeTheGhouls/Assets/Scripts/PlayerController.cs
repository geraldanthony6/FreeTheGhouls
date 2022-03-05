using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    
    public PlayerMovementController playerMovement;
    Vector3 pos1 = new Vector3(95, 3.6f, -10); 

    Vector3 pos2 = new Vector3(205, 3.6f, -10);
    [SerializeField]private GameObject[] trappedSouls;
    [SerializeField]private Transform[] trappedSoulsNewPosition;
    [SerializeField]private Transform gameCam;
    [SerializeField]private GameObject spawnManagerLevel1;
    [SerializeField]private GameObject spawnManagerLevel2;
    [SerializeField]private GameObject spawnManagerLevel3;

    public Image[] healthHearts;
    public Text timer;
    public Text runTimeText;
    public Text GameWonText;
    public float timeStart;
    public float runTime;

    [SerializeField]private int playerHealth = 3;
    private bool level1Complete = false;
    private bool level2Complete = false;
    private bool level3Complete = false;
    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovementController>();
    }

    // Update is called once per frame
    void Update()
    {
        timeStart += Time.deltaTime;
        timer.text = timeStart.ToString("F2");

        if(playerHealth == 2){
            healthHearts[2].gameObject.SetActive(false);
        } 
        if(playerHealth == 1){
            healthHearts[1].gameObject.SetActive(false);
        } 
        if(playerHealth == 0){
            healthHearts[0].gameObject.SetActive(false);
            SceneManager.LoadSceneAsync(0);
        }
    }

    void FixedUpdate()
    {
        if(transform.position.x > 45 && transform.position.x < 160)
        {
            spawnManagerLevel2.gameObject.SetActive(true);
            spawnManagerLevel1.gameObject.SetActive(false);
        } else if(transform.position.x > 160)
        {
            spawnManagerLevel2.gameObject.SetActive(false);
            spawnManagerLevel3.gameObject.SetActive(true);
        }

        if(level3Complete == true)
        {
            spawnManagerLevel3.gameObject.SetActive(false);
            runTime = timeStart;
            
        }

        if(level1Complete == true && gameCam.position.x < pos1.x){
            gameCam.position = Vector3.MoveTowards(gameCam.position, pos1, 8f * Time.deltaTime);
            trappedSouls[0].transform.position = Vector2.MoveTowards(trappedSouls[0].transform.position, trappedSoulsNewPosition[0].position, 10f * Time.deltaTime);
        } 
        if(level2Complete == true && gameCam.position.x < pos2.x){
            gameCam.position = Vector3.MoveTowards(gameCam.position, pos2, 8f * Time.deltaTime);
            trappedSouls[1].transform.position = Vector2.MoveTowards(trappedSouls[1].transform.position, trappedSoulsNewPosition[1].position, 10f * Time.deltaTime);
        }
        if(level3Complete == true){
            trappedSouls[2].transform.position = Vector2.MoveTowards(trappedSouls[2].transform.position, trappedSoulsNewPosition[2].position, 10f * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Level1Boost"))
        {
            level1Complete = true;
            playerMovement.playerSpeed += 3;
        } 
        if(other.CompareTag("Enemy"))
        {
            playerHealth -= 1;
        }
        if(other.CompareTag("Level2Boost"))
        {
            playerMovement.playerSpeed += 3;
            level2Complete = true;
        } if(other.CompareTag("Level3Boost"))
        {
            level3Complete = true;
            runTimeText.gameObject.SetActive(true);
            GameWonText.gameObject.SetActive(true);
            timer.gameObject.SetActive(false);
            runTimeText.text = timeStart.ToString(" 0 " + "seconds");
        }

    }



}
