using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleControlling : MonoBehaviour
{
    [SerializeField]private float speed = 5f;
    [SerializeField]private Rigidbody rb;
    [SerializeField] private LayerMask brickLayer;
    private Vector3 mousedown;
    private Vector3 mouseup;
    private Vector3 mouse_direction;
    private Vector3 position_people;
    private int x, z;
    private Vector3 target;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        position_people= rb.transform.position;
    }
    private void FixedUpdate()
    {
        
    }
    private Vector3 CheckBrick(int x,int z)
    {
        RaycastHit hit;
        float check_x = position_people.x +x;
        float check_z = position_people.z + z;
        Debug.Log(Physics.Raycast(new Vector3(check_x, position_people.y, check_z), new Vector3(0, -1, 0), out hit, Mathf.Infinity, brickLayer));
        //while (Physics.Raycast(new Vector3(check_x , position_people.y, check_z), new Vector3(0, -1, 0), out hit, Mathf.Infinity, brickLayer))
        //{
        //    check_x += x;
        //    check_z += z;
        //}
        //return hit.transform.position;
        return Vector3.zero;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            mousedown = Input.mousePosition;
        } 
        if (Input.GetKeyUp(KeyCode.Mouse0)) { 
            mouseup = Input.mousePosition;
            mouse_direction = mouseup - mousedown;
            Debug.Log(mouse_direction);
            if (mouse_direction.x < mouse_direction.y)
            {
                if (mouse_direction.y > 0) z = 1;
                else if (mouse_direction.y < 0) z = -1;
                else z = 0;
            }
            else if (mouse_direction.x > mouse_direction.y)
            {
                if (mouse_direction.x > 0) x = 1;
                else if (mouse_direction.x < 0) x = -1;
                else x = 0;
            }
        }
        target = CheckBrick(x,z);
        position_people = Vector3.MoveTowards(position_people, target, Time.deltaTime * speed);
        rb.position = position_people;
        
    }
}
