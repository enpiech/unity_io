using System;
using System.IO;
using System.Security.Cryptography;
using Enpiech.IO.Runtime.Data;
using UnityAtoms;
using UnityEngine;

namespace Enpiech.IO.Runtime.Actions
{
    [EditorIcon("atom-icon-purple")]
    [CreateAssetMenu(menuName = "Unity Atoms/Actions/IO/Write Encrypted Data", fileName = "AC_WriteEncryptedData")]
    public sealed class WriteEncryptedDataAction : AtomAction<WriteFileRequestDto>
    {
        /// <summary>
        ///     FileStream used for writing files.
        /// </summary>
        private FileStream _dataStream;

        /// <summary>
        ///     Write serialized data to file using AES encryption
        /// </summary>
        public override void Do(WriteFileRequestDto dto)
        {
            // Create new AES instance.
            var iAes = Aes.Create();

            // Update the internal key.
            var savedKey = iAes.Key;

            // Convert the byte[] into a Base64 String.
            var key = Convert.ToBase64String(savedKey);

            // Update the PlayerPrefs
            PlayerPrefs.SetString(dto.Key, key);

            // Create a FileStream for creating files.
            _dataStream = new FileStream(dto.FileName, FileMode.Create);

            // Save the new generated IV.
            var inputIV = iAes.IV;

            // Write the IV to the FileStream unencrypted.
            _dataStream.Write(inputIV, 0, inputIV.Length);

            // Create CryptoStream, wrapping FileStream.
            var iStream = new CryptoStream(
                _dataStream,
                iAes.CreateEncryptor(iAes.Key, iAes.IV),
                CryptoStreamMode.Write);

            // Create StreamWriter, wrapping CryptoStream.
            var sWriter = new StreamWriter(iStream);

            // Write to the innermost stream (which will encrypt).
            sWriter.Write(dto.SerializedData);

            // Close StreamWriter.
            sWriter.Close();

            // Close CryptoStream.
            iStream.Close();

            // Close FileStream.
            _dataStream.Close();
        }
    }
}