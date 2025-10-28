// using UnityEngine;

// public class MovingPlatform : MonoBehaviour
// {
//     public float speed = 3f;
//     public float moveDistance = 5f;

//     private Vector2 startPos;
//     private bool movingRight = true;

//     void Start()
//     {
//         startPos = transform.position;
//     }

//     void Update()
//     {
//         Vector2 newPos = transform.position;

//         if (movingRight)
//         {
//             newPos.x += speed * Time.deltaTime;

//             if (newPos.x >= startPos.x + moveDistance)
//                 movingRight = false;
//         }
//         else
//         {
//             newPos.x -= speed * Time.deltaTime;

//             if (newPos.x <= startPos.x - moveDistance)
//                 movingRight = true;
//         }

//         transform.position = newPos;
//     }
// }


// using UnityEngine;

// public class MovingPlatform : MonoBehaviour
// {
//     public float speed = 3f;
//     public float moveDistance = 5f;
//     private Vector2 startPos;
//     private bool movingRight = true;


//     void Start()
//     {
//         startPos = transform.position;

//     }

//     void Update()
//     {
//         if (movingRight)
//         {
//             Vector2 newposition = transform.position;
//             newposition.x += speed * Time.deltaTime;
//             if (newposition.x >= startPos.x + moveDistance)
//                 movingRight = false;
//         }
//         else
//         {
//             Vector2 newposition = transform.position;
//             newposition.x -= speed * Time.deltaTime;
//             if (newposition.x <= startPos.x - moveDistance)
//             {
//                 movingRight = true;
//             }
//         }
//     }
// }


// using UnityEngine;

// public class MobingPlatform : MonoBehaviour
// {
//     public float speed = 3f;
//     public float moveDistance = 5f;
//     private Vector2 startPos;
//     private bool movingRight = true;


//     void Start()
//     {
//         startPos = transform.position;

//     }

//     void Update()
//     {
//         if (movingRight)
//         {
//             Vector2 newposition = transform.position;
//             newposition.x += speed * Time.deltaTime;
//             if (newposition.x >= startPos.x + moveDistance)
//             {
//                 movingRight = false;
//             }
//         }
//         else
//         {
//             Vector2 newposition = transform.position;
//             newposition.x -= speed * Time.deltaTime;
//             if (newposition.x <= startPos.x - moveDistance)
//             {
//                 movingRight = true;
//             }
//         }

//     }
// }

// using UnityEngine;
// using UnityEngine.Rendering;

// public class MovingPlateform : MonoBehaviour
// {
//     public float speed = 3f;
//     public float moveDistance = 5f;
//     private Vector2 startPos;
//     private bool movingRight = true;

//     void Start()
//     {

//         {
//             startPos = transform.position;
//         }
//     }


//     void Update()
//     {
//         if (movingRight)
//         {
//             Vector2 newposition = transform.position;
//             newposition.x += speed * Time.deltaTime;
//             if (newposition.x >= startPos.x + moveDistance)
//             {
//                 movingRight = false;
//             }


//         }
//         else
//         {
//             Vector2 newposition = transform.position;
//             newposition.x -= speed * Time.deltaTime;
//             if (newposition.x <= startPos.x - moveDistance)
//             {
//             movingRight = true;
//         }
//     }
// }
// }

// using UnityEngine;

// public class MovingPlatform : MonoBehaviour
// {
//     public float speed = 3f;
//     public float moveDistance = 5f;

//     private Vector2 startPos;
//     private bool movingRight = true;

//     void Start()
//     {
//         startPos = transform.position;
//     }

//     void Update()
//     {
//         Vector2 newPos = transform.position;

//         if (movingRight)
//         {
//             newPos.x += speed * Time.deltaTime;
//             if (newPos.x >= startPos.x + moveDistance)
//                 movingRight = false;
//         }
//         else
//         {
//             newPos.x -= speed * Time.deltaTime;
//             if (newPos.x <= startPos.x - moveDistance)
//                 movingRight = true;
//         }

//         transform.position = newPos;
//     }

//     void OnCollisionEnter2D(Collision2D collision)
//     {
//         Debug.Log("Collision with: " + collision.gameObject.name);
//         if (collision.gameObject.CompareTag("Player"))
//         {
//             // Parent the player to the PlatformParent (this GameObject)
//             collision.transform.SetParent(transform);
//         }
//     }

//     void OnCollisionExit2D(Collision2D collision)
//     {
//         Debug.Log("Collision exit: " + collision.gameObject.name);
//         if (collision.gameObject.CompareTag("Player"))
//         {
//             Debug.Log("Player left platform!");
//             collision.transform.SetParent(null);
//         }
//     }
// }


//     void OnCollisionEnter2D(Collision2D collision)
//     {
//         if (collision.gameObject.CompareTag("Player"))
//         {
//             collision.transform.SetParent(transform);
//         }
//     }

//     void OnCollisionExit2D(Collision2D collision)
//     {

//         if (collision.gameObject.CompareTag("Player"))
//         {
//             collision.transform.SetParent(null);
//         }
//     }
// }

using System;
using Unity.Mathematics;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed = 2f;
    public float moveDistance = 10f;
    private Vector2 startpos;
    private bool isMovingRight = true;

    void Start()
    {
        startpos = transform.position;


    }

    void Update()
    {
        Vector2 currentpos = transform.position;
        if (isMovingRight)
        {
            currentpos.x += speed * Time.deltaTime;
            if (currentpos.x >= startpos.x + moveDistance)
            {
                isMovingRight = false;
            }
        }
        else
        {
            currentpos.x -= speed * Time.deltaTime;
            if (currentpos.x <= startpos.x - moveDistance)
            {
                isMovingRight = true;
            }
        }
        transform.position = currentpos;

    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerToDetach = collision.transform;
            Invoke(nameof(DetachPlayer), 0.01f);
        }
    }

    private Transform playerToDetach;

    private void DetachPlayer()
    {
        if (playerToDetach != null)
        {
            playerToDetach.SetParent(null);
            playerToDetach = null;

        }



    }


  


}






