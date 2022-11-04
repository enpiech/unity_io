using System;
using System.IO;
using System.Security.Cryptography;
using Enpiech.IO.Runtime.Data;
using UnityAtoms;
using UnityEngine;

namespace Enpiech.IO.Runtime.Functions
{
    [EditorIcon("atom-icon-purple")]
    [CreateAssetMenu(menuName = "Unity Atoms/Function/IO/Read Encrypted Data", fileName = "FC_ReadEncryptedData")]
    public sealed class ReadEncryptedDataFunction : AtomFunction<string, ReadFileRequestDto>
    {
        /// <summary>
        ///     FileStream used for reading files.
        /// </summary>
        private FileStream _dataStream;

        /// <summary>
        ///     Read serialized data from file using AES encryption
        /// </summary>
        /// <param name="key">PlayerPref key</param>
        /// <returns>Serialized data</returns>
        public override string Call(ReadFileRequestDto dto)
        {
            // Does the file exist AND does the "key" preference exist?
            if (!File.Exists(dto.FileName) || !PlayerPrefs.HasKey(dto.Key))
            {
                return string.Empty;
            }

            // Update key based on PlayerPrefs
            // (Convert the String into a Base64 byte[] array.)
            var savedKey = Convert.FromBase64String(PlayerPrefs.GetString(dto.Key));

            // Create FileStream for opening files.
            _dataStream = new FileStream(dto.FileName, FileMode.Open);

            // Create new AES instance.
            var oAes = Aes.Create();

            // Create an array of correct size based on AES IV.
            var outputIV = new byte[oAes.IV.Length];

            // Read the IV from the file.
            _dataStream.Read(outputIV, 0, outputIV.Length);

            // Create CryptoStream, wrapping FileStream
            var oStream = new CryptoStream(
                _dataStream,
                oAes.CreateDecryptor(savedKey, outputIV),
                CryptoStreamMode.Read);

            // Create a StreamReader, wrapping CryptoStream
            var reader = new StreamReader(oStream);

            // Read the entire file into a String value.
            var data = reader.ReadToEnd();

            // Always close a stream after usage.
            reader.Close();

            return data;
        }
    }
}