using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    public Sprite stone_tile;
    public Sprite dirt_tile;

    public float pheroStrength_alpha = 0f;
    public float minPheroStrength_alpha = 0f;
    public float maxPheroStrength_alpha = 1f;

    public float pheroStrength_beta = 0f;
    public float minPheroStrength_beta = 0f;

    private static float lowestAlphaAboveZero = 1f;
    private int counter = 0;

    public bool isBarrier = false;

    public int zone = -1;

    public bool IsBarrier() { return isBarrier; }


    public bool IsInZone() { return zone > -1; }


    public int GetZoneIndex() { return zone; }


    public float GetPheroStrength_alpha() { return pheroStrength_alpha; }


    public float GetPheroStrength_beta() { return pheroStrength_beta; }
    

    public void SetMinPheroStrength_alpha(float minStrength) {

        if (minStrength < 0) {

            minPheroStrength_alpha = 0;

        } else if (minStrength > 1) {

            minPheroStrength_alpha = 1;

        } else {

            minPheroStrength_alpha = minStrength;

        }
        
        if (pheroStrength_alpha < minPheroStrength_alpha) {

            SetPheroStrength_alpha(minPheroStrength_alpha);
        }

    }


    public void SetMaxPheroStrength_alpha(float maxStrength) {

        if (maxStrength < 0) {

            maxPheroStrength_alpha = 0;

        } else if (maxStrength > 1) {

            maxPheroStrength_alpha = 1;

        } else {

            maxPheroStrength_alpha = maxStrength;

        }

        if (pheroStrength_alpha > maxPheroStrength_alpha) {

            SetPheroStrength_alpha(maxPheroStrength_alpha);
        }

    }


    public void SetMinPheroStrength_beta(float minStrength) {

        if (minStrength < 0) {

            minPheroStrength_beta = 0;

        } else if (minStrength > 1) {

            minPheroStrength_beta = 1;

        } else {

            minPheroStrength_beta = minStrength;

        }
        
        if (pheroStrength_beta < minPheroStrength_beta) {

            SetPheroStrength_beta(minPheroStrength_beta);
        }

    }


    public void SetPheroStrength_alpha(float strength) {

        if (strength < minPheroStrength_alpha) {

            pheroStrength_alpha = minPheroStrength_alpha;

        } else if (strength > maxPheroStrength_alpha) {

            pheroStrength_alpha = maxPheroStrength_alpha;

        } else {

            pheroStrength_alpha = strength;

        }

        transform.GetComponent<SpriteRenderer>().color = GetPheroColor();

        if(pheroStrength_alpha < lowestAlphaAboveZero && pheroStrength_alpha > 0) {

            lowestAlphaAboveZero = pheroStrength_alpha;
        }
    }


    public void SetPheroStrength_beta(float strength) {

        if (strength < minPheroStrength_beta) {

            pheroStrength_beta = minPheroStrength_beta;

        } else if (strength > 1) {

            pheroStrength_beta = 1;

        } else {

            pheroStrength_beta = strength;

        }

        transform.GetComponent<SpriteRenderer>().color = GetPheroColor();
    }


    public static float GetLowestAlphaAboveZero() { return lowestAlphaAboveZero; }


    public Color GetPheroColor() {

        //return new Color(pheroStrength_alpha, Mathf.Min(pheroStrength_alpha, pheroStrength_beta), pheroStrength_beta);

        //original, working but not good
        /*
        if(Mathf.Min(pheroStrength_alpha, pheroStrength_beta) == pheroStrength_alpha) {
            return new Color(1f, 1f - pheroStrength_beta, 1f - pheroStrength_beta);
        }
        return new Color(1f - pheroStrength_alpha, 1f - pheroStrength_alpha, 1f);
        */
        if(pheroStrength_alpha > 0f) {
            return new Color(1f, 1f - pheroStrength_alpha, 1f - pheroStrength_alpha);
        }
        return new Color(1f - pheroStrength_beta, 1f - pheroStrength_beta, 1f);
    }


    public void SetBarrier(bool isBarrier) {

        if(isBarrier) {

            //GetComponent<SpriteRenderer>().color = Color.grey;
            GetComponent<SpriteRenderer>().sprite = stone_tile;
            GetComponent<BoxCollider2D>().enabled = true;
            transform.tag = "Barrier";
            zone = -1;

        } else {

            //GetComponent<SpriteRenderer>().color = Color.white;
            GetComponent<SpriteRenderer>().sprite = dirt_tile;
            GetComponent<BoxCollider2D>().enabled = false;
            transform.tag = "Tile";
        }


        this.isBarrier = isBarrier;
    }


    public void SetZoneIndex(int zoneIndex) {

        this.zone = zoneIndex;
    }
    /*
    public void Update() {


        if (counter == 100000) {

            Debug.Log("COUNTER!");
            if (pheroStrength_alpha > 0f) {

                SetPheroStrength_alpha(GetPheroStrength_alpha() - 0.1f);
            }

            counter = 0;

        } else {

            counter++;
        }
    
    }
    */
}
