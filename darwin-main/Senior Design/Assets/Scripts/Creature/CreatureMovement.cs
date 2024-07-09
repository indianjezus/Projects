using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureMovement : MonoBehaviour {

    public float laserLength = 3f;

    public bool drawRay = true;

    public float vel = 0.2f;

    private Rigidbody2D rb;

    private int dir = -1;

    private int count = 0;

    private Tile target = null;

    private List<Tile> lastTenTargeted = new List<Tile>();

    private List<Tile> visitedTiles = new List<Tile>();

    void Start() {

        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {

        /*
        // old code DO NOT DELETE!
        RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, transform.up, laserLength);
        
        if(hit.Length == 1) {

            int num = Random.Range(-20, 20);

            if(num == 5 || num == -5) {

                transform.Rotate(new Vector3(0, 0, transform.rotation.z + num * dir));

            } else if (num == 0) {

                dir *= -1;
            }
        }
        
        for(int i = 0; i < hit.Length; i++) {

            if(hit[i].collider.tag == "Creature" && i == 0) { continue; }

            transform.Rotate(new Vector3(0, 0, transform.rotation.z + 5f * dir));
        }
        
        transform.position += transform.up * Time.deltaTime * vel;

        if (drawRay) {
            Debug.DrawRay(transform.position, transform.up * laserLength, Color.red);
        }
        */


        /*
        RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, transform.up, laserLength);

        if (hit.Length > 1) {

            bool rotate = false;

            for (int i = 1; i < hit.Length; i++) {

                if (hit[i].transform.tag == "Food") {
                    rotate = false;
                    break;
                }

                if (hit[i].transform.tag == "Barrier" || hit[i].transform.tag == "Creature") {
                    rotate = true;
                    break;
                }
            }

            if (rotate) {

                transform.Rotate(new Vector3(0, 0, transform.rotation.z + -5f));
                transform.position += transform.up * Time.deltaTime * vel;

            } else {

                transform.position += transform.up * Time.deltaTime * vel;
            }

        } else {
        
            target = FindNewTargetTile(0.8f);
            float angle = Mathf.Atan2(target.transform.position.y - transform.position.y, target.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle - 90f));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 100 * Time.deltaTime);
            transform.position += transform.up * Time.deltaTime * vel;

            DropPheromones();
        }
        */

        Tile newTarget = FindNewTargetTile(0.8f);
        if(lastTenTargeted.Count == 50){

            lastTenTargeted.Remove(lastTenTargeted[0]);
            lastTenTargeted.Add(newTarget);
        } else {

            lastTenTargeted.Add(newTarget);
        }

        bool isStuck = true;
        for(int i = 0; i < lastTenTargeted.Count; i++) {
            if(newTarget != lastTenTargeted[i]) {

                isStuck = false;
                break;
            }
        }

        if (isStuck && lastTenTargeted.Count == 50) {

            Debug.Log("STUCK!");
            transform.Rotate(new Vector3(0, 0, transform.rotation.z + -15f));
            transform.position += transform.up * Time.deltaTime * vel;

        } else {

            target = newTarget;
            //target = FindNewTargetTile(0.8f);
            float angle = Mathf.Atan2(target.transform.position.y - transform.position.y, target.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle - 90f));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 360 * Time.deltaTime);
            transform.position += transform.up * Time.deltaTime * vel;
            DropPheromones();
        }

        if (drawRay) {
            Debug.DrawRay(transform.position, transform.up * laserLength, Color.red);
        }
        
    }


    public List<Tile> GetVisitedTiles() { return visitedTiles; }


    private void DropPheromones() {
        /*
        //original, mostly working code
        Tile nearest = GetNearestTile();
        nearest.SetPheroStrength_beta(nearest.GetPheroStrength_beta() + 0.05f);
        */

        if(!visitedTiles.Contains(GetNearestTile())) {
            visitedTiles.Add(GetNearestTile());
        }

        Tile[] closest = GetNearestTiles();

        for(int i = 0; i < closest.Length; i++) {

            if (closest[i] == null || closest[i].IsBarrier()) { continue; }
            if(i < 5) {
                closest[i].SetPheroStrength_beta(closest[i].GetPheroStrength_beta() + 0.1f);
            } else {
                closest[i].SetPheroStrength_beta(closest[i].GetPheroStrength_beta() + 0.1f);
            }
        }
    }


    private Tile GetNearestTile() {

        int x = (int)(transform.position.x * 10f);
        int y = (int)(transform.position.y * 10f);

        int x1 = Mathf.FloorToInt(((x/10f + 17.5f) / 0.2f));
        int y1 = Mathf.FloorToInt(((y/10f + 17.5f) / 0.2f));

        return MapGenerator.GetTileAt(x1, y1);

    }


    private Tile[] GetNearestTiles() {

        int x = (int)(transform.position.x * 10f);
        int y = (int)(transform.position.y * 10f);

        int x1 = Mathf.FloorToInt(((x / 10f + 17.5f) / 0.2f));
        int y1 = Mathf.FloorToInt(((y / 10f + 17.5f) / 0.2f));

        Tile[] rtrn = {
            MapGenerator.GetTileAt(x1, y1),

            MapGenerator.GetTileAt(x1 + 1, y1),
            MapGenerator.GetTileAt(x1 - 1, y1),
            MapGenerator.GetTileAt(x1, y1 + 1),
            MapGenerator.GetTileAt(x1, y1 - 1),

            MapGenerator.GetTileAt(x1 + 1, y1 + 1),
            MapGenerator.GetTileAt(x1 - 1, y1 - 1),
            MapGenerator.GetTileAt(x1 - 1, y1 + 1),
            MapGenerator.GetTileAt(x1 + 1, y1 - 1)
        };
        return rtrn;
        //return MapGenerator.GetTileAt(x1, y1);

    }


    private int[] GetNearestTilePos() {

        int x = (int)(transform.position.x * 10f);
        int y = (int)(transform.position.y * 10f);

        int x1 = Mathf.FloorToInt(((x / 10f + 17.5f) / 0.2f));
        int y1 = Mathf.FloorToInt(((y / 10f + 17.5f) / 0.2f));

        int[] rtrn = { x1, y1 };
        return rtrn;
    }


    private List<Tile> GetConnectedTiles() {

        int[] nearest = GetNearestTilePos();

        List<Tile> rtrn = new List<Tile>();

        for (int i = -1; i < 2; i++) {

            for (int j = -1; j < 2; j++) {

                if(i == 0 && j == 0) {
                    continue;
                }

                Tile t = MapGenerator.GetTileAt(nearest[0] + j, nearest[1] + i);

                if(t != null && !t.IsBarrier()) {

                    rtrn.Add(t);
                }

            }
        }

        return rtrn;
    }
    
    private Tile FindNewTargetTile(float threshold) {

        int[] nearest = GetNearestTilePos();

        Tile nearest_tile = GetNearestTile();

        List<Tile> alphas = new List<Tile>();
        //float largestAlpha = 0f;
        Tile largestAlpha = nearest_tile;

        Tile largestBeta = nearest_tile;

        Tile smallestBeta = nearest_tile;

        List<Tile> validTiles = new List<Tile>();

        for(int i = -1; i < 2; i++) {

            for(int j = -1; j < 2; j++) {

                if (i == 0 && j == 0) { continue; }

                Tile curr = MapGenerator.GetTileAt(nearest[0] + i, nearest[1] + j);

                if (curr == null) { continue; }

                if (curr.IsBarrier()) { continue; }

                validTiles.Add(curr);

                //if (curr.GetPheroStrength_beta() >= threshold) { continue; }
                /*
                if (curr.GetPheroStrength_alpha() > largestAlpha) {

                    largestAlpha = curr.GetPheroStrength_alpha();
                    alphas.Clear();
                    alphas.Add(curr);

                } else if(largestAlpha > 0 && curr.GetPheroStrength_alpha() == largestAlpha) {

                    alphas.Add(curr);
                }
                */
                if(curr.GetPheroStrength_alpha() > largestAlpha.GetPheroStrength_alpha()) {

                    largestAlpha = curr;
                     
                }

                if (curr.GetPheroStrength_beta() >= threshold) { continue; }

                if (curr.GetPheroStrength_beta() > largestBeta.GetPheroStrength_beta()) {

                    largestBeta = curr;
                }

                if (curr.GetPheroStrength_beta() < smallestBeta.GetPheroStrength_beta()) {

                    smallestBeta = curr;
                }
            }
        }
        
        
        if (largestAlpha != nearest_tile) {
            Debug.Log("LARGEST ALPHA");
            return largestAlpha;
        }
        
        /*
        if (alphas.Count == 1) {

            Debug.Log("LARGEST ALPHA");
            return alphas[0];

        } else if (alphas.Count > 1) {

            Debug.Log("LARGEST BETA AMONG LARGEST ALPHA");

            Tile largest = alphas[0];

            for(int i = 1; i < alphas.Count; i++) {

                if(alphas[i].GetPheroStrength_beta() > largest.GetPheroStrength_beta()) {

                    largest = alphas[i];

                }
            }

            return largest;
        }
        */
        if (smallestBeta != nearest_tile) {
            Debug.Log("SMALLEST BETA");
            return smallestBeta;
        }

        
        if (largestBeta != nearest_tile) {
            Debug.Log("LARGEST BETA");
            return largestBeta;
        }
        

        if (validTiles.Count == 0) {
            Debug.Log("An ant is out of bounds and has been destroyed");
            Destroy(gameObject);
        }


        Debug.Log("RANDOM");
        int rand = Random.Range(0, validTiles.Count);

        return validTiles[rand];
    }


    
}
