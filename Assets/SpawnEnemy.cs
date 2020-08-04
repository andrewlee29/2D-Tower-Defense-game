/*
 * Copyright (c) 2017 Razeware LLC
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * Notwithstanding the foregoing, you may not use, copy, modify, merge, publish, 
 * distribute, sublicense, create a derivative work, and/or sell copies of the 
 * Software in any work that is designed, intended, or marketed for pedagogical or 
 * instructional purposes related to programming, coding, application development, 
 * or information technology.  Permission for such use, copying, modification,
 * merger, publication, distribution, sublicensing, creation of derivative works, 
 * or sale is expressly withheld.
 *    
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Wave
{
    public GameObject enemyPrefab;
    public float spawnInterval = 2;
    public int maxEnemies = 20;
}

public class SpawnEnemy : MonoBehaviour
{

    public GameObject[] waypoints;
    public GameObject testEnemyPrefab;
    //New Code:
    //public GameObject enemyPrefab1;
    //public GameObject enemyPrefab2;
    public List<GameObject> enemies = new List<GameObject>();

    public Wave[] waves;
    public int timeBetweenWaves = 5;

    private GameManagerBehavior gameManager;

    private float lastSpawnTime;
    private int enemiesSpawned = 0;

    // Use this for initialization
    void Start()
    {
        lastSpawnTime = Time.time;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
        //enemies.Add(enemyPrefab1);
        //enemies.Add(enemyPrefab2);
    }

    // Update is called once per frame
    void Update()
    {
        // 1
        int currentWave = gameManager.Wave;
        if (currentWave < waves.Length)
        {
            // 2
            float timeInterval = Time.time - lastSpawnTime;
            float spawnInterval = waves[currentWave].spawnInterval;
            if (((enemiesSpawned == 0 && timeInterval > timeBetweenWaves) ||
                 timeInterval > spawnInterval) &&
                enemiesSpawned < waves[currentWave].maxEnemies)
            {
                //New:
                int i = SpawnRate();
                GameObject chosenPrefab = enemies[i];
                if (i == 4)
                {
                    var scriptRef = chosenPrefab.GetComponent<HealthBar>();
                    if(scriptRef != null)
                    {
                        scriptRef.SetHP(Random.Range(300, 400));
                    }
                }
                ////////
                // 3  
                lastSpawnTime = Time.time;
               //OLD GameObject newEnemy = (GameObject)Instantiate(waves[currentWave].enemyPrefab);
                GameObject newEnemyTest = (GameObject)Instantiate(chosenPrefab);
                //OLD newEnemy.GetComponent<MoveEnemy>().waypoints = waypoints;
                newEnemyTest.GetComponent<MoveEnemy>().waypoints = waypoints;
                enemiesSpawned++;
            }
            // 4 
            if (enemiesSpawned == waves[currentWave].maxEnemies &&
                GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                gameManager.Wave++;
                gameManager.Gold = Mathf.RoundToInt(gameManager.Gold * 1.1f);
                enemiesSpawned = 0;
                lastSpawnTime = Time.time;
            }
            // 5 
        }
        else
        {
            gameManager.gameOver = true;
            GameObject gameOverText = GameObject.FindGameObjectWithTag("GameWon");
            gameOverText.GetComponent<Animator>().SetBool("gameOver", true);
        }
    }
    //New Code: handles spawning different enemies
    public int SpawnRate()
    {
        int rate = Random.Range(1, 50);
        Debug.Log("RNG: " + rate);
        if (rate >= 1 && rate <= 20)
        {
            Debug.Log("Pos: 1");
            return 4;
        }
        else if (rate >= 21 && rate <= 35)
        {
            Debug.Log("Pos: 2");
            return 1;
        }
        else if (rate >= 36 && rate <= 43)
        {
            Debug.Log("Pos: 3");
            return 2;
        }
        else if (rate >= 44 && rate <= 49)
        {
            Debug.Log("Pos: 4");
            return 3;
        }
        else if (rate == 50)
        {
            Debug.Log("Pos: 5");
            return 4;
        }
        return 1;
    }
}
