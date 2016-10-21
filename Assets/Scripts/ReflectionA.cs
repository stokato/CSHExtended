using UnityEngine;
using System.Collections;
using System.Reflection;

public class ReflectionA : MonoBehaviour {

    class subClassA
    {
        public static int firstInt;
        public string secondInt;
        public int thirdInt;

        public subClassA(int first, int second, int third)
        {
            firstInt = first;
            this.secondInt = second.ToString();
            this.thirdInt = third;
        }
        
        public void OnUpdate()
        {
            Debug.Log("subClassA Updating A");
        }
    }

    class subClassB
    {
        public void OnUpdate()
        {
            Debug.Log("subClassB Updating B");
        }
    }

    class subClassC
    {
        public void NonUpdate()
        {

        }
    }

    subClassA ca;
    subClassB cb;
    subClassC cc;

	// Use this for initialization
	void Start () {
        FieldInfo[] fields = typeof(subClassA).GetFields();

        foreach (FieldInfo field in fields)
        {
            Debug.Log(field.Name);
        }

        ca = new subClassA(1, 2, 3);
        cb = new subClassB();
        cc = new subClassC();
	}
	
	// Update is called once per frame
	void Update () {
        ArrayList subClasses = new ArrayList();
        subClasses.Add(ca);
        subClasses.Add(cb);
        subClasses.Add(cc);

        foreach(object o in subClasses)
        {
            MethodInfo method = (MethodInfo)o.GetType().GetMethod("OnUpdate");
            if(method != null)
            {
                method.Invoke(o, null);
            }

            if(o is subClassA)
            {
                subClassA sc = (subClassA)o;
                sc.OnUpdate();
            }

            if(o is subClassB)
            {
                subClassB sc = (subClassB)o;
                sc.OnUpdate();
            }
        }
	}
}
