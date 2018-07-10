using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Overseer : MonoBehaviour {

    #region static variables

    private static Overseer instance;

    public static Overseer Instance
    {
        get
        {
            if (instance == null)
                return FindObjectOfType<Overseer>();
            return instance;
        }
    }

    #endregion

    #region main Variables

    public Collider WorldCollider;

    public bool gameStarted = false;

    #endregion

    #region monoBehavior Methods

    // Use this for initialization
    void Start () {
        gameStarted = true;
        Cursor.visible = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
            EditorApplication.isPaused = !EditorApplication.isPaused;
	}

    #endregion

    #region methods

    public void GameOver()
    {
        gameStarted = false;
        Debug.Log("Game Over");
        Time.timeScale = 0;
        Cursor.visible = true;
    }

    #endregion
}
