using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class main : MonoBehaviour
{
    GameObject head;
    GameObject meal;
    float x = 0, y = 0, z = 0, d=0.5f;
    float idealDistance = 0.8f, stepCoef = 0.05f;
    float lastItemCoef = 0.2f;
    List<GameObject> items = new List<GameObject>();

    Vector3 newPlace(Vector3 firstPlace, Vector3 secondPlace, float coef){
        Vector3 difference = firstPlace - secondPlace;
        return secondPlace +stepCoef * difference * (1 - idealDistance*coef / difference.magnitude);
    }
    void newPlaces(){
        for(int i= 1; i<items.Count; i++) {
            float coef = 1 - (i*(1-lastItemCoef) / items.Count);//calculate size for last sphere
            items[i].transform.position = newPlace(
                                                    items[i - 1].transform.position
                                                    , items[i].transform.position
                                                    ,coef
                                                );
            items[i].transform.localScale = new Vector3(coef, coef, coef);
            //make tail smaller
        }
    }
    void Start()
    {
        meal = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        meal.transform.position = new Vector3(3, 2, 0);
        meal.GetComponent<Renderer>().material.color = Color.red;
        meal.name = "meal";
        head = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        items.Add(head);
        items.Add(GameObject.CreatePrimitive(PrimitiveType.Sphere));
        items.Add(GameObject.CreatePrimitive(PrimitiveType.Sphere));
    }

    void checkMeal() {
        if((meal.transform.position - head.transform.position).magnitude < 2) {
            meal.transform.position = new Vector3(Random.Range(-7,7),Random.Range(-7,7), Random.Range(0,3));
            items.Add(GameObject.CreatePrimitive(PrimitiveType.Sphere));
            //meal color depends on z
        }
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow) && x<10) {x += d;}
        if (Input.GetKey(KeyCode.LeftArrow) && x > -10) { x -= d; }
        if (Input.GetKey(KeyCode.UpArrow) && y < 8) { y += d; }
        if (Input.GetKey(KeyCode.DownArrow) && y > -8) { y -= d; }
        if (Input.GetKey(KeyCode.PageUp) && z < 10) { z += d; }
        if (Input.GetKey(KeyCode.PageDown) && z > 0) { z -= d; }
        head.transform.position = new Vector3(x, y, z);
        checkMeal();
        //items[1].transform.position = newPlace(head.transform.position, items[1].transform.position);
        newPlaces();
    }


}
