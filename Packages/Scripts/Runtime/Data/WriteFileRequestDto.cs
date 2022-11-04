using System;
using System.IO;
using UnityEngine;

namespace Enpiech.IO.Runtime.Data
{
    [Serializable]
    public class WriteFileRequestDto
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
    }
}