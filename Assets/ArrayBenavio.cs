using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrayBenavio : MonoBehaviour
{
     // Start is called before the first frame update
public float speed = 1.0f;
public WeaponBehaviour [] weapons;
public int arraySize;
int selectedWeaponIndex = 0;
void Start()
{
References.thePlayer = gameObject;
weapons = new WeaponBehaviour [arraySize];
for (int index =0;index<arraySize;index++){
weapons[index] = new WeaponBehaviour();
}
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
gameObject.transform.LookAt(cursorPosition);

//กำหนดให้กดปุ่มเม้าซ้ายยิงปืน
// if (weapons.Count > 0 && Input.GetButton("Fire1"))
// {
// //Tell our weapon to fire
// weapons[selectedWeaponIndex].Fire(cursorPosition);
// }

if (Input.GetButton("Fire1"))
{
//Tell our weapon to fire
weapons[selectedWeaponIndex].Fire(cursorPosition);
}
}
//เมื่อแท่นปืนชนเข้ากับอาวุธ
private void OnTriggerEnter(Collider other)
{
WeaponBehaviour theirWeapon = other.GetComponentInParent<WeaponBehaviour>();
if (theirWeapon != null)
{
//เพื่ออาวุธ
//weapons.Add(theirWeapon);
weapons[selectedWeaponIndex] = theirWeapon;
//เปลี่ยนตำแหน่ง
theirWeapon.transform.position = transform.position;
theirWeapon.transform.rotation = transform.rotation;
//เปลี่ยนให้อาวุธเป็นผู้ติดตาม
theirWeapon.transform.SetParent(transform);
}
}
}
