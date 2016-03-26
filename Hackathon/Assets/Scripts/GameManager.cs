﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public float timer;

	public CameraScript cam;
	public int focusLevel = 5;
	public Grid grid;
	public Grid template;

	public bool GameSceneLoaded;

	void Start()
	{
		DontDestroyOnLoad(this.gameObject);
		cam.SetPosition(grid.size);
		LoadNextLevel();
	}
	
	// Update is called once per frame
	void Update () {
		if (SceneManager.GetActiveScene().name.Equals("Game"))
		{
			grid.gameObject.SetActive(true);
			template.gameObject.SetActive(true);

			if (isEqual(grid.grid, template.grid))
			{
				LoadNextLevel();
			}
		}
		else
		{
			grid.gameObject.SetActive(false);
			template.gameObject.SetActive(false);
		}

		timer += Time.deltaTime;

		if (timer >= 60 && timer <= 120)
			focusLevel = 4;
		if (timer >= 120 && timer <= 180)
			focusLevel = 3;
		if (timer >= 180 && timer <= 240)
			focusLevel = 2;
	}

	public void LoadNextLevel()
	{
		if (focusLevel >= 3)
			LoadLevel();
		else
		{
			if (Random.value < 0.1f)
			{
				SceneManager.LoadScene(2);
				Invoke("LoadRandomInkBlot", 2.0f);
			}
			else
				LoadLevel();
		}
	}

	public void LoadRandomInkBlot()
	{
		int sceneIndex = Random.Range(3, 6);
		SceneManager.LoadScene(sceneIndex);

		Invoke("LoadGameScene", 5.0f);
	}

	public void LoadGameScene()
	{
		SceneManager.LoadScene("Game");
		LoadNextLevel();
	}

	public void LoadLevel()
	{
		cam.SetPosition(focusLevel);
		grid.resetSize(focusLevel);
		template.resetSize(focusLevel);

		grid.Init();
		template.GenerateLevel(focusLevel);
		template.transform.position = new Vector2(grid.size + 1, 0);
	}

	public bool isEqual(bool[,] one, bool[,] two)
	{
		for (int x = 0; x < one.GetLength(0); x ++)
		{
			for (int y = 0; y < one.GetLength(1); y ++)
			{
				if (one[y, x] != two[y, x])
					return false;
			}
		}
		return true;
	}
}

