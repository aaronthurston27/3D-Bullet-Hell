using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour {

    #region main variables

    public float missileSpeed = 10f;

    public Player associatedPlayer;

    public GameObject explosionEffect;

    #endregion

    #region monobehavior methods
    void Start () {

        transform.SetParent(GameObject.Find("Active Missiles").transform, true); // Organize all instantiate missiles by setting their parent to the "Active" transform.

	}
    
	void Update () {

        MoveForward();
		
	}

    void OnTriggerEnter(Collider collider) // Upon colliding with a game world object.
    {
        if(collider.gameObject.GetComponent<MovingBlock>()) // If we hit a moving, enemy block.
        {
            associatedPlayer.IncrementScore(); // Increment the score.
            Destroy(collider.gameObject); // Destroy the block we collided with, as well as the missile we shot.
            Destroy(this.gameObject);
            --BlockSpawner.Instance.blocksSpawned; // Decrement the blocks spawned.
            GameObject explosion = Instantiate(explosionEffect,this.transform.position,this.transform.rotation); // Instantiate an explosion where the missile collided. 
            explosion.transform.SetParent(GameObject.Find("Particle FX").transform,true);
        }
        else if(collider.gameObject.layer == LayerMask.NameToLayer("Game Boundaries")) // Destroy the missile if it goes out of bounds.
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerExit(Collider collider) // If we exit the game boundaries for some reason, destroy the missile.
    {
        if(collider == Overseer.Instance.WorldCollider)
            Destroy(this.gameObject);
        
    }

    #endregion

    #region methods
    //
    void MoveForward() // Move this missile forward.
    {
        Vector3 newPosition = (this.transform.forward).normalized * missileSpeed * Time.deltaTime;
        this.transform.position += newPosition;
    }

    #endregion
}
