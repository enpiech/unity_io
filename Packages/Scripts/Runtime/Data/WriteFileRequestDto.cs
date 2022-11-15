using System;
using System.IO;
using UnityEngine;

namespace Enpiech.IO.Runtime.Data
{
    [Serializable]
    public class WriteFileRequestDto : IEquatable<WriteFileRequestDto>
    {
        [SerializeField]
        private string _key;

        [SerializeField]
        private string _fileName;

        [SerializeField]
        private string _serializedData;

        public WriteFileRequestDto(string fileName, string key, string data)
        {
            _fileName = fileName;
            _key = key;
            _serializedData = data;
        }

        public string Key => _key;

        public string FileName => Path.Combine(Application.persistentDataPath, _fileName);

        public string SerializedData => _serializedData;

        public bool Equals(WriteFileRequestDto? other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return _key == other._key && _fileName == other._fileName && _serializedData == other._serializedData;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return Equals((WriteFileRequestDto)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_key, _fileName, _serializedData);
        }

        public static bool operator ==(WriteFileRequestDto? left, WriteFileRequestDto? right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(WriteFileRequestDto? left, WriteFileRequestDto? right)
        {
            return !Equals(left, right);
        }
    }
}