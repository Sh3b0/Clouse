using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Mathf;

public static class BezierCurve
{
        /// <summary>
        /// Get value of the quadratic Bezier curve.
        /// </summary>
        /// <param name="p0">Point "from"</param>
        /// <param name="p1">Intermediate point</param>
        /// <param name="p2">Point "to"</param>
        /// <param name="t">Parameter in [0, 1]</param>
        /// <returns>Value at t.</returns>
        public static Vector3 Quadratic(Vector3 p0, Vector3 p1, Vector3 p2, float t)
        {
                t = Clamp01(t);
                return (1 - t) * (1 - t) * p0 + 
                       (1 - t) * t * 2 * p1 + 
                       t * t * p2;
        }

        /// <summary>
        /// Get value of the cubic Bezier curve.
        /// </summary>
        /// <param name="p0">Point "from"</param>
        /// <param name="p1">Intermediate point 1</param>
        /// <param name="p2">Intermediate point 2</param>
        /// <param name="p3">Point "to"</param>
        /// <param name="t">Parameter in [0, 1]</param>
        /// <returns>Value at t.</returns>
        public static Vector3 Cubic(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
        {
                t = Clamp01(t);
                return (1 - t) * (1 - t) * (1 - t) * p0 +
                       (1 - t) * (1 - t) * t * 3f * p1 +
                       (1 - t) * t * t * 3f * p2 +
                       t * t * t * p3;
        }

        /// <summary>
        /// Get value of a Bezier curve in the general form.
        /// </summary>
        /// <param name="points">Points. First is "from", last is "to", others are intermediate.</param>
        /// <param name="t">Parameter in [0, 1]</param>
        /// <returns>Value at t.</returns>
        public static Vector3 General(Vector3[] points, float t)
        {
                t = Clamp01(t);
                var result = Vector3.zero;
                var n = points.Length - 1;

                for (var i = 0; i <= n; i++)
                {
                        result += BezierCoefficient(i, n, t) * points[i];
                }

                return result;
        }

        private static float BezierCoefficient(int i, int n, float t)
        {
                return BinomialCoefficient(n, i) * 
                       Pow(t, i) * 
                       Pow(1 - t, n - i);
        }

        private static long BinomialCoefficient(int n, int k)
        {
                if (n < 0) throw new ArgumentOutOfRangeException(nameof(n));
                if (k < 0) throw new ArgumentOutOfRangeException(nameof(k));
                if (k > n) throw new ArgumentOutOfRangeException($"k must be less than or equal to n but n={n}, k={k}");
                
                return Factorial(n) / 
                       (Factorial(k) * Factorial(n - k));
        }

        private static long Factorial(int n)
        {
                while (n >= Factorials.Count)
                {
                        var lastValue = Factorials[Factorials.Count - 1];
                        var newValue = lastValue * Factorials.Count;
                        Factorials.Add(newValue);
                }

                return Factorials[n];
        } 
        
        private static readonly List<long> Factorials = new List<long> {1, 1, 2, 6, 24, 120, 720};
}