using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Area that saves the checkpoint if a character enters it
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class ChekpointArea : MonoBehaviour {

    public Transform checkpoint;
    public Sprite greenCheckpoint;
    public Sprite redCheckpoint;
    static private ChekpointArea prevCheckpoint = null;

    private PhysicsConfig pConfig;

    // Start is called before the first frame update
    void Start() {
        pConfig = GameObject.FindObjectOfType<PhysicsConfig>();
        if (!pConfig) {
            pConfig = (PhysicsConfig) new GameObject().AddComponent(typeof(PhysicsConfig));
            pConfig.gameObject.name = "Physics Config";
            Debug.LogWarning("PhysicsConfig not found on the scene! Using default config.");
        }
    }

    public void SetCheckPointSprite(Sprite sprite) {
        GetComponent<SpriteRenderer>().sprite = sprite;
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other) {
        CheckpointSystem cs = other.GetComponent<CheckpointSystem>();
        // Sprite greenCheckpoint = Resources.Load<Sprite>("Sprites/Checkpoints/greenFlag");

        // if prev flag exist, turn the prev flag back to red
        if (prevCheckpoint) {
            prevCheckpoint.SetCheckPointSprite(redCheckpoint);
        }

        // int CheckpointNumber = int.Parse(gameObject.name.Replace("RedCheckpoint", ""));
        if (cs) {
            cs.softCheckpoint = checkpoint.position;
            // check the type of cs on debug console
            // Debug.Log(cs);
            this.SetCheckPointSprite(greenCheckpoint);
            prevCheckpoint = this;
        }
        
    }
}