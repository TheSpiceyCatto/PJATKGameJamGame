using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walls : MonoBehaviour
{
    [SerializeField] private GameObject[] redWalls;
    [SerializeField] private GameObject[] blueWalls;
    [SerializeField] private LayerMask redLayer;
    [SerializeField] private LayerMask blueLayer;
    [SerializeField] private LayerMask bulletLayer;
    private bool isRedWall = true;
    private Collider2D collider;
    void Start()
    {
        collider = GetComponent<Collider2D>();
        SwapWalls();
    }
    
    void Update()
    {
        if (InputManager.Swap)
        {
            isRedWall = !isRedWall;
            SwapWalls();
        }
    }

    private void SwapWalls()
    {
        foreach (GameObject wall in redWalls)
        {
            if (wall.TryGetComponent(out SpriteRenderer sr))
                sr.enabled = isRedWall;
            if (wall.TryGetComponent(out Collider2D collider))
                collider.enabled = isRedWall;
        }
        foreach (GameObject wall in blueWalls)
        {
            if (wall.TryGetComponent(out SpriteRenderer sr))
                sr.enabled = !isRedWall;
            if (wall.TryGetComponent(out Collider2D collider))
                collider.enabled = !isRedWall;
        }
        // collider.gameObject.layer = isRedWall
        //     ? LayerMask.NameToLayer("InteractableA")
        //     : LayerMask.NameToLayer("InteractableB");
    }
}
