using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class Utility : MonoBehaviour {

}

// var shuffled = originalSequence.Shuffle();
public static class EnumerableExtensions {
    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source) {
        return source.Shuffle(new System.Random());
    }

    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, System.Random rng) {
        if (source == null) throw new ArgumentNullException("source");
        if (rng == null) throw new ArgumentNullException("rng");

        return source.ShuffleIterator(rng);
    }

    private static IEnumerable<T> ShuffleIterator<T>(
        this IEnumerable<T> source, System.Random rng) {
        var buffer = source.ToList();
        for (int i = 0; i < buffer.Count; i++) {
            int j = rng.Next(i, buffer.Count);
            yield return buffer[j];

            buffer[j] = buffer[i];
        }
    }
}
