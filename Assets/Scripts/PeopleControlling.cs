using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleControlling : MonoBehaviour
{
    [SerializeField]private float speed = 5f;
    [SerializeField] private LayerMask brickLayer;
    [SerializeField] private GameObject Brick;
    private Vector3 mousedown;
    private Vector3 mouseup;
    private Vector3 mouse_direction;
    private int x , z;
    private Vector3 target;
    private bool isMoving;
    private float brickHeight;
    // Start is called before the first frame update
    void Start()
    {
        target = transform.position;
        isMoving = false;
        brickHeight = 0.3f;
    }
    private void FixedUpdate()
    {
        
    }
    private Vector3 CheckBrick(int x,int z)
    {
        RaycastHit hit;
        float check_x = transform.position.x + x;
        float check_z = transform.position.z + z;
        Vector3 hitPosition = Vector3.zero;
        int count = 0;
        while (Physics.Raycast(new Vector3(check_x, transform.position.y +1, check_z), new Vector3(0, -1, 0), out hit, Mathf.Infinity, brickLayer))
        {
            hitPosition = new Vector3(hit.transform.position.x,transform.position.y,hit.transform.position.z);
            check_x += x;
            check_z += z;
            count++;
        }
        if (count == 0) return transform.position;
        return hitPosition;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            mousedown = Input.mousePosition;
        }
        if (Input.GetKeyUp(KeyCode.Mouse0)) {
            if (!isMoving)
            {

                x = 0; z = 0;
                mouseup = Input.mousePosition;
                mouse_direction = mouseup - mousedown;
                if (Mathf.Abs(mouse_direction.x) < Mathf.Abs(mouse_direction.y))
                {
                    if (mouse_direction.y > 0) z = 1;
                    else z = -1;
                }
                else
                {
                    if (mouse_direction.x > 0) x = 1;
                    else x = -1;
                }
                target = CheckBrick(x,z);
                isMoving= true;
            }
        }
        if (isMoving)
        {
            
            transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);
            RaycastHit brick;
            if(Physics.Raycast(transform.position, Vector3.down, out brick, Mathf.Infinity, brickLayer))
            {
                target += new Vector3(0, brickHeight, 0);
                Destroy(brick.transform.gameObject);
                transform.position = transform.position + new Vector3(0, brickHeight, 0);
                Instantiate(Brick, gameObject.transform);
            }
        }
        if (Vector3.Distance(transform.position, target) <=0.01f)
        {
            isMoving = false;
        }
    }
}
