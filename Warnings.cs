using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warnings : MonoBehaviour {

    #region enum

    enum WarningDirections
    {
        LEFT_WARNING,
        BOTTOM_WARNING,
        RIGHT_WARNING
    };

    #endregion

    #region main variables
    public Player associatedPlayer;
    public RectTransform warnings;
    public RectTransform[] warningsArray;
    public MovingBlock incomingBlock;
    private Rect screenRect = new Rect(0, 0, Screen.width/ 2 - 20, Screen.height/2 - 20);
    #endregion

    #region monobehavior methods

    void Start () {

        warnings.gameObject.SetActive(false); // Hide impact warning when starting the game.
	}
	
	void Update () {
        if(incomingBlock && warnings.gameObject.activeInHierarchy) // If there is a block about to hit us, display the warning.
            DisplayWarning();
	}

    #endregion

    #region methods

    public void ActivateWarning(MovingBlock blockWeCollidedWith) // Activate this player's warning system.
    {
        incomingBlock = blockWeCollidedWith; // Set the incoming block.
        // Calculate the position of the block relative to what we can see. If the block is on our blindsides, display the warning symbol.
        Vector3 blockPositon = (incomingBlock.transform.position - associatedPlayer.transform.position).normalized;
        float direction = Vector3.SignedAngle(Camera.main.transform.forward.normalized, blockPositon, Vector3.up);
        if(direction > 60 || direction < -60)
            warnings.gameObject.SetActive(true);
    }

    private void DisplayWarning()
    {
        Vector3 blockPositon = (incomingBlock.transform.position - associatedPlayer.transform.position).normalized;
        float direction = Vector3.SignedAngle(Camera.main.transform.forward.normalized, blockPositon, Vector3.up);
        // Change the location of the warning symbol depending on where the block is about to hit us.
        if (direction < -60 && direction > -180)
            warnings.transform.localPosition = new Vector3(-screenRect.xMax, 0, 0);
        else if (direction > 60 && direction < 100)
            warnings.transform.localPosition = new Vector3(screenRect.xMax, 0, 0);
        else if (direction > 100 || direction < -100)
            warnings.transform.localPosition = new Vector3(0, -screenRect.yMax, 0);
        else
        {
            warnings.gameObject.SetActive(false);
            return;
        }
        warnings.gameObject.SetActive(true);
    }

    #endregion
}
