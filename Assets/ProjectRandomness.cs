using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectRandomness : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public int SpawnRate()
    {
        int rate = Random.Range(1, 50);
        if(rate >= 1 && rate <= 20)
        {
            return 1;
        }
        else if(rate >= 21 && rate <= 35)
        {
            return 2;
        }
        else if (rate >= 36 && rate <= 43)
        {
            return 3;
        }
        else if (rate >= 44 && rate <= 49)
        {
            return 4;
        }
        else if (rate == 50)
        {
            return 5;
        }
        return 1;
    }
}
