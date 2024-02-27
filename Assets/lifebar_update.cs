using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class lifebar_update : MonoBehaviour
{

    public PlayerHealth playerHealth;
    public UIDocument doc;

    public ProgressBar bar;

    // Start is called before the first frame update
    void Start()
    {
        doc = GetComponent<UIDocument>();
        bar = doc.rootVisualElement.Q("Life") as ProgressBar;
    }

    // Update is called once per frame
    void Update()
    {
        bar.SetValueWithoutNotify(playerHealth.health);
    }
}
