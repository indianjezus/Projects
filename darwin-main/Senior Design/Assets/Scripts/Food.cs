using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D collision) {

        if(collision.transform.tag == "Creature") {

            List<Tile> vt = collision.transform.GetComponent<CreatureMovement>().GetVisitedTiles();
            Debug.Log(vt.Count + " tile visited before finding food");
            for (int i = 0; i < vt.Count; i++) {
                /*
                if (vt[i].GetPheroStrength_beta() == 1f) {
                    vt[i].SetPheroStrength_beta(0.5f);
                    
                }
                */
                vt[i].SetPheroStrength_alpha(/*(Tile.GetLowestAlphaAboveZero()) + */vt[i].GetPheroStrength_alpha() + i * 0.001f);
            }
            /*
            if (vt.Count < 50) {

                for(int i = 0; i < vt.Count; i++) {


                    //vt[i].SetPheroStrength_alpha((Tile.GetLowestAlphaAboveZero()) + vt[i].GetPheroStrength_alpha() + (vt.Count - i) * 0.001f);
                
                }

            } else {

                for (int i = vt.Count - 51; i < vt.Count; i++) {


                    //vt[i].SetPheroStrength_alpha((Tile.GetLowestAlphaAboveZero()) + vt[i].GetPheroStrength_alpha() + (50 - i) * 0.001f);

                }
            }
            */

        }

        //Debug.Log(collision.transform.tag + " DESTROYED");
        Destroy(collision.gameObject);
    }
}
