using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P123456 : MonoBehaviour
{

    public float speed; //'float' is short for floating point number, which is basically just a normal number

    //สร้างตัวแปรเพื่อเก็บอาวุธ
    public List<WeaponBehaviour> weapons = new List<WeaponBehaviour>();
    //กำหนดตำแหน่งอาวุธ
    public int selectedWeaponIndex;

    // Start is called before the first frame update
    void Start()
    {
        References.thePlayer = gameObject;
        //กำหนดตำแหน่งแรกของอาวุธ
        selectedWeaponIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {

        //การเคลื่อนที่ของแท่นปืน
        Vector3 inputVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Rigidbody ourRigidBody = GetComponent<Rigidbody>();
        ourRigidBody.velocity = inputVector * speed;

        //การหมุนแท่นปืนเป็นวงกลม
        Ray rayFromCameraToCursor = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        playerPlane.Raycast(rayFromCameraToCursor, out float distanceFromCamera);
        Vector3 cursorPosition = rayFromCameraToCursor.GetPoint(distanceFromCamera);
        transform.LookAt(cursorPosition);
     
        //กำหนดให้กดปุ่มเม้าซ้ายยิงปืน
        if (weapons.Count > 0 && Input.GetButton("Fire1"))
        {
            //Tell our weapon to fire
            weapons[selectedWeaponIndex].Fire(cursorPosition);
        }
        //ทำการเปลี่ยนอาวุธกดปุ่มเมาขวา
        if (Input.GetButtonDown("Fire2"))
        {
            ChangeWeaponIndex(selectedWeaponIndex + 1);
        }
     
    }
 
    //เมื่อแท่นปืนชนเข้ากับอาวุธ
    private void OnTriggerEnter(Collider other)
    {
        WeaponBehaviour theirWeapon = other.GetComponentInParent<WeaponBehaviour>();
        if (theirWeapon != null)
        {
            //เพื่ออาวุธ
            weapons.Add(theirWeapon);
            //เปลี่ยนตำแหน่ง
            theirWeapon.transform.position = transform.position;
            theirWeapon.transform.rotation = transform.rotation;
            //เปลี่ยนให้อาวุธเป็นผู้ติดตาม
            theirWeapon.transform.SetParent(transform);
        }
    }//สิ้นสุด
 
    //ทำการเปลี่ยนอาวุธ
    private void ChangeWeaponIndex(int index)
    {

        //Change our index
        selectedWeaponIndex = index;

        //If it's gone too far, loop back around
        if (selectedWeaponIndex >= weapons.Count)
        {
            
            selectedWeaponIndex = 0;
        }

        //For each weapon in our list
        for (
            int i = 0; //Declare a variable to keep track of how many iterations we've done
            i < weapons.Count; //Set a limit for how high this variable can go
            i++ //Run this after each time we iterate - increase the iteration count
        )
        {
            if (i == selectedWeaponIndex)
            {
                //If it's the one we just selected, make it visible - 'enable' it
                weapons[i].gameObject.SetActive(true);
            } else
            {
                //If it's not the one we just selected, hide it - disable it.
                weapons[i].gameObject.SetActive(false);
            }
        }
    }//สิ้นสุด
}
