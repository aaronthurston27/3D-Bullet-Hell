using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    #region main variables

    public int maxMissilesActive = 4;
    private int missilesActive = 0;
    private int score = 0;
    [SerializeField]
    private Text scoreDisplay;

    public Missile missileType;
    public Warnings warningSystem;

    #endregion

    #region monobehavior methods

    void Start () {
        
    }

	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Missile newMissile = Instantiate(missileType, this.transform.position, this.transform.rotation);
            newMissile.associatedPlayer = this;
        }
        scoreDisplay.text = "Score: " + score;
        scoreDisplay.GetComponent<RectTransform>().position = new Vector2(Screen.width - 40, Screen.height - 15);
    }

    void OnTriggerExit(Collider collider)
    {
        if(collider.gameObject.layer == LayerMask.NameToLayer("Game Boundaries"))
        {
            Overseer.Instance.GameOver();
            Debug.Log(collider.gameObject.name);
        }
    }

    #endregion

    
    #region methods

    public void IncrementScore()
    {
        ++score;
    }

    #endregion
}
