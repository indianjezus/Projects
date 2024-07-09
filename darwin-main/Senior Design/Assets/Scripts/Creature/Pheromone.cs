using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pheromone : MonoBehaviour {

    private static Gradient gradient = new Gradient();

    private static GradientColorKey[] colors = {
        new GradientColorKey(Color.white, 0.0f),
        new GradientColorKey(Color.green, 1.0f)
    };

    private static GradientAlphaKey[] alphas = {
        new GradientAlphaKey(1.0f, 0.0f),
        new GradientAlphaKey(0.0f, 1.0f)
    };

    public static Color GetPheroColor(float strength) {

        gradient.SetKeys(colors, alphas);

        return gradient.Evaluate(strength);
    }
}
