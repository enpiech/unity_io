using System;
using System.IO;
using UnityEngine;

namespace Enpiech.IO.Runtime.Data
{
    [Serializable]
    public struct ReadFileRequestDto : IEquatable<ReadFileRequestDto>
    {
        [SerializeField]
        private string _key;

        [SerializeField]
        private string _fileName;

        public ReadFileRequestDto(string fileName, string key)
        {
            _fileName = fileName;
            _key = key;
        }

        public string Key => _key;

        public string FileName => Path.Combine(Application.persistentDataPath, _fileName);

        public bool Equals(ReadFileRequestDto other)
        {
            return _key == other._key && _fileName == other._fileName;
        }

        public override bool Equals(object? obj)
        {
            return obj is ReadFileRequestDto other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_key, _fileName);
        }

        public static bool operator ==(ReadFileRequestDto left, ReadFileRequestDto right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(ReadFileRequestDto left, ReadFileRequestDto right)
        {
            return !left.Equals(right);
        }
    }
}