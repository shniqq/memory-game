using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace MemoryGame.Utils
{
    public interface IRandom
    {
        float Value { get; }

        float Range(float minInclusive, float maxExclusive);

        int Range(int minInclusive, int maxExclusive);
    }

    public class Random : IRandom, IDisposable
    {
        private readonly bool _saveToFile;

        private readonly List<object> _randomNumbers = new();

        public Random([InjectOptional] int? seed, bool saveToFile)
        {
            _saveToFile = saveToFile;
            if (seed.HasValue)
            {
                UnityEngine.Random.InitState(seed.Value);
            }
        }

        public float Value
        {
            get
            {
                var value = UnityEngine.Random.value;
                _randomNumbers.Add(value);
                return value;
            }
        }

        public float Range(float minInclusive, float maxExclusive)
        {
            var value = UnityEngine.Random.Range(minInclusive, maxExclusive);
            _randomNumbers.Add(value);
            return value;
        }

        public int Range(int minInclusive, int maxExclusive)
        {
            var value = UnityEngine.Random.Range(minInclusive, maxExclusive);
            _randomNumbers.Add(value);
            return value;
        }

        public void Dispose()
        {
            if (_saveToFile)
            {
#if UNITY_EDITOR
                var path = UnityEditor.EditorUtility.SaveFilePanel("Save Random Numbers?", Application.dataPath,
                    "RandomNumbers",
                    "txt");
                if (!string.IsNullOrWhiteSpace(path))
                {
                    var text = _randomNumbers.Aggregate(string.Empty, (s, o) => $"{s}{Environment.NewLine}{o}");
                    File.WriteAllText(path, text);
                }
#endif
            }
        }
    }

    public class RandomFromFile : IRandom
    {
        private readonly List<float> _numbers;
        private int _currentIndex;

        public RandomFromFile(TextAsset randomNumbers)
        {
            _numbers = randomNumbers.text.Split(Environment.NewLine).Where(e => !string.IsNullOrWhiteSpace(e)).Select(
                s =>
                {
                    if (float.TryParse(s, out var number))
                    {
                        return number;
                    }

                    return int.Parse(s);
                }).ToList();
        }

        public float Value
        {
            get
            {
                var number = _numbers[(int)Mathf.Repeat(_currentIndex, _numbers.Count)];
                _currentIndex++;
                return number;
            }
        }

        public float Range(float minInclusive, float maxExclusive)
        {
            var number = _numbers[(int)Mathf.Repeat(_currentIndex, _numbers.Count)];
            _currentIndex++;
            return number;
        }

        public int Range(int minInclusive, int maxExclusive)
        {
            var number = _numbers[(int)Mathf.Repeat(_currentIndex, _numbers.Count)];
            _currentIndex++;
            return Convert.ToInt32(number);
        }
    }
}