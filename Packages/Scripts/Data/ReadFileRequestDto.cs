using System;
using System.IO;
using UnityEngine;

namespace IO.Scripts.Data
{
    [Serializable]
    public class ReadFileRequestDto
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
    }
}