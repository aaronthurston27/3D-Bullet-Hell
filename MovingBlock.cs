using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlock : MonoBehaviour {

    #region main variables

    public float speed = 3f;

    public float followtime = 2.5f; // How long should we follow the target?

    public Player currentPlayerSpace; // I REALLY don't want to add this. But right now there is no other way. I should research other ways to destroy a incoming sphere while removing from the player warning space list.

    #endregion

    #region monobehavior methods

    void Start() {

        // Set Parent Transform
        this.transform.SetParent(GameObject.Find("Active Spheres").transform, true);

        // Rotate towards our goal.
        RotateTowardsPlayer();
    }
	
	void Update () {
        // Move towards our goal. If Follow time > 0, move towards the player. Else, move in a straight line.
        if (followtime > 0)
        {
            transform.position = Vector3.MoveTowards(this.transform.position, Camera.main.transform.position, speed * Time.deltaTime);
            followtime -= Time.deltaTime;
        }
        else
        {
            transform.Translate(Vector3.forward.normalized * speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider collider)
    {

        if(collider.gameObject.GetComponent<Player>()) // If we collide with a player, disable the warning system if neccessary and destroy the block.
        {
            if (collider.gameObject.GetComponent<Player>().warningSystem.incomingBlock == this)
            {
                collider.gameObject.GetComponent<Player>().warningSystem.incomingBlock = null;
                collider.gameObject.GetComponent<Player>().warningSystem.warnings.gameObject.SetActive(false);
            }
            Destroy(this.gameObject);
        }
        else if(collider.gameObject.layer == LayerMask.NameToLayer("Warnings"))
        {
            Warnings warningSystem = collider.gameObject.GetComponent<Warnings>();

            if (!warningSystem)
                return;

            warningSystem.ActivateWarning(this);

            this.speed = speed / 2.0f;
        }
        else if(collider.gameObject.layer == LayerMask.NameToLayer("Game Boundaries")) // Destroy this block if it goes out of bounds.
        {
            Destroy(this.gameObject);
        }
        
    }
    

    void OnTriggerExit(Collider collider) // This should be the world collider exit.
    {
        if (collider == Overseer.Instance.WorldCollider)
        {
            if (collider.gameObject.layer == LayerMask.NameToLayer("Warnings"))
            {
                if (collider.gameObject.GetComponent<Warnings>().incomingBlock == this)
                {
                    collider.gameObject.GetComponent<Warnings>().incomingBlock = null;
                    collider.gameObject.GetComponent<Player>().warningSystem.warnings.gameObject.SetActive(false);
                }
            }
            Destroy(this.gameObject);
            BlockSpawner.Instance.blocksSpawned -= 1;
            return;
        }
        if(collider.gameObject.layer == LayerMask.NameToLayer("Warnings"))
        {
            if (collider.gameObject.GetComponent<Warnings>().incomingBlock == this)
            {
                collider.gameObject.GetComponent<Warnings>().incomingBlock = null;
                collider.gameObject.GetComponent<Warnings>().warnings.gameObject.SetActive(false);
                this.speed *= 2;
            }
        }
    }

    void OnDestroy()
    {
        if(BlockSpawner.Instance)
            --BlockSpawner.Instance.blocksSpawned;
    }

    #endregion

    #region methods

    private void RotateTowardsPlayer()
    {
        Quaternion newRotation = Quaternion.LookRotation(Camera.main.transform.position - this.transform.position);
        this.transform.rotation = newRotation;
    }

    #endregion
}
