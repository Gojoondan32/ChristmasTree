using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotOGoldHandler : MonoBehaviour, iDecoration
{
    [SerializeField] private int points = 10;
    [SerializeField] private int multiplier = 1;
    private PotOGoldDecoration myDeco;

    public bool upgrading = false;
    public bool completed = false;
    [SerializeField] private float upgradeTimer = 0f;
    public void DestroyDecoration()
    {
        Destroy(this.gameObject);
    }

    public Decoration GetDecoration()
    {
        if (myDeco != null)
        {
            return myDeco;
        }
        else
        {
            return null;
        }
    }

    private void Awake()
    {
        myDeco = new PotOGoldDecoration(points, multiplier);
    }

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Deco Type: " + myDeco.MyDecorationType.ToString() + "  Deco Points: " + myDeco.GetPoints().ToString());
        upgrading = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (upgrading)
        {
            upgradeTimer += Time.deltaTime;


            if (upgradeTimer >= 15f)
            {
                upgradeTimer = 15f;
                completed = true;
            }
        }
    }

    public void SetUpgrading(bool state)
    {
        upgrading = state;
    }

    public bool GetUpgradeComplete()
    {
        return completed;
    }
}