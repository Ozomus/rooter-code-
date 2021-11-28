using UnityEngine;
using UnityEngine.UI;

public class FindEnemy : MonoBehaviour
{
    
    Transform player;
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;
   
   
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();

    }

  
    void Update()
    {

        FindClosestEnemy();
        Vector2 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
        direction.Normalize();
        movement = direction;
       
    }
   
    void moveCharacter(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction *moveSpeed* Time.deltaTime));
    }



    public void FindClosestEnemy()
    {

        float distanceToClosestEnemy = Mathf.Infinity;
        Find closestEnemy = null;
        Find[] allEnemies = GameObject.FindObjectsOfType<Find>();

        foreach (Find currentEnemy in allEnemies)
        {
            float distanceToEnemy = (currentEnemy.transform.position - this.transform.position).sqrMagnitude;
            if (distanceToEnemy < distanceToClosestEnemy)
            {
                distanceToClosestEnemy = distanceToEnemy;
                closestEnemy = currentEnemy;
                 player = currentEnemy.transform;
                if(closestEnemy==currentEnemy)
                {
                    moveCharacter(movement);
                }

              



            }
        }

      
    }
   
       
}

