Imports System
Imports System.IO
Imports System.Text
Imports System.Security.Cryptography

Public Class clsRijndaelEncryptionHandler
    ' <summary>
    ' Encrypts specified plaintext using Rijndael symmetric key algorithm
    ' and returns a base64-encoded result.
    ' </summary>
    ' <param name="plainText">
    ' Plaintext value to be encrypted.
    ' </param>
    ' <param name="passPhrase">
    ' Passphrase from which a pseudo-random password will be derived. The 
    ' derived password will be used to generate the encryption key. 
    ' Passphrase can be any string. In this example we assume that this 
    ' passphrase is an ASCII string.
    ' </param>
    ' <param name="saltValue">
    ' Salt value used along with passphrase to generate password. Salt can 
    ' be any string. In this example we assume that salt is an ASCII string.
    ' </param>
    ' <param name="hashAlgorithm">
    ' Hash algorithm used to generate password. Allowed values are: "MD5" and
    ' "SHA1". SHA1 hashes are a bit slower, but more secure than MD5 hashes.
    ' </param>
    ' <param name="passwordIterations">
    ' Number of iterations used to generate password. One or two iterations
    ' should be enough.
    ' </param>
    ' <param name="initVector">
    ' Initialization vector (or IV). This value is required to encrypt the 
    ' first block of plaintext data. For RijndaelManaged class IV must be 
    ' exactly 16 ASCII characters long.
    ' </param>
    ' <param name="keySize">
    ' Size of encryption key in bits. Allowed values are: 128, 192, and 256. 
    ' Longer keys are more secure than shorter keys.
    ' </param>
    ' <returns>
    ' Encrypted value formatted as a base64-encoded string.
    ' </returns>

    Const NUM_PW_ITERATIONS As Integer = 1
    Const HASH_ALGORITHM As String = "SHA1"
    Const SALT_VALUE As String = "pRi5m533kRu135"
    Const INIT_VECTOR As String = "@3k8573j4083j410"
    Const KEY_SIZE As Integer = 192
    Private m_passPhrase As String
    Private m_Password As PasswordDeriveBytes

    Private m_SymmetricKey As RijndaelManaged
    Private m_Hashgen As SHA1Managed
    Private m_Encryptor As ICryptoTransform
    Private m_Decryptor As ICryptoTransform

    Private m_KeyBytes As Byte()
    Private m_saltValueBytes As Byte()
    Private m_initVectorBytes As Byte()

    Sub New(ByVal passPhrase As String)
        Me.m_passPhrase = passPhrase

        ' Convert strings into byte arrays.
        ' Let us assume that strings only contain ASCII codes.
        ' If strings include Unicode characters, use Unicode, UTF7, or UTF8 
        ' encoding.
        'Dim initVectorBytes As Byte()
        Me.m_initVectorBytes = Encoding.ASCII.GetBytes(INIT_VECTOR)

        'Dim saltValueBytes As Byte()
        Me.m_saltValueBytes = Encoding.ASCII.GetBytes(SALT_VALUE)

        ' First, we must create a password, from which the key will be derived.
        ' This password will be generated from the specified passphrase and 
        ' salt value. The password will be created using the specified hash 
        ' algorithm. Password creation can be done in several iterations.
        Me.m_Password = New PasswordDeriveBytes(passPhrase, _
                                           Me.m_saltValueBytes, _
                                           HASH_ALGORITHM, _
                                           NUM_PW_ITERATIONS)

        ' Use the password to generate pseudo-random bytes for the encryption
        ' key. Specify the size of the key in bytes (instead of bits).
        'Dim keyBytes As Byte()
        Me.m_KeyBytes = Me.m_Password.GetBytes(CInt(KEY_SIZE / 8))

        ' Create uninitialized Rijndael encryption object.
        Me.m_SymmetricKey = New RijndaelManaged

        ' It is reasonable to set encryption mode to Cipher Block Chaining
        ' (CBC). Use default options for other symmetric key parameters.
        Me.m_SymmetricKey.Mode = CipherMode.CBC

        ' Generate encryptor from the existing key bytes and initialization 
        ' vector. Key size will be defined based on the number of the key 
        ' bytes.
        Me.m_Encryptor = Me.m_SymmetricKey.CreateEncryptor(Me.m_KeyBytes, Me.m_initVectorBytes)

        Me.m_Decryptor = Me.m_SymmetricKey.CreateDecryptor(Me.m_KeyBytes, Me.m_initVectorBytes)




    End Sub

    Public Function GetHashedPassword() As String
        'return me.m_SymmetricKey.
    End Function

    Public Function Encrypt(ByVal plainText As String) As String

        ' Convert our plaintext into a byte array.
        ' Let us assume that plaintext contains UTF8-encoded characters.
        Dim plainTextBytes As Byte()
        plainTextBytes = Encoding.UTF8.GetBytes(plainText)

        ' Define memory stream which will be used to hold encrypted data.
        Dim memoryStream As MemoryStream
        memoryStream = New MemoryStream

        ' Define cryptographic stream (always use Write mode for encryption).
        Dim cryptoStream As CryptoStream
        cryptoStream = New CryptoStream(memoryStream, _
                                        Me.m_Encryptor, _
                                        CryptoStreamMode.Write)
        ' Start encrypting.
        cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length)

        ' Finish encrypting.
        cryptoStream.FlushFinalBlock()

        ' Convert our encrypted data from a memory stream into a byte array.
        Dim cipherTextBytes As Byte()
        cipherTextBytes = memoryStream.ToArray()

        ' Close both streams.
        memoryStream.Close()
        cryptoStream.Close()

        ' Convert encrypted data into a base64-encoded string.
        Dim cipherText As String
        cipherText = Convert.ToBase64String(cipherTextBytes)

        ' Return encrypted string.
        Return cipherText
    End Function

    Public Function MakeArbitraryHash(ByVal Sourcetext As String) As String
        If Me.m_Hashgen Is Nothing Then
            Me.m_Hashgen = New SHA1Managed
        End If
        'Create an encoding object to ensure the encoding standard for the source text
        Dim Ue As New System.Text.ASCIIEncoding
        'Retrieve a byte array based on the source text
        Dim ByteSourceText() As Byte = Ue.GetBytes(Sourcetext)
        'Compute the hash value from the source
        Dim SHA1_hash() As Byte = Me.m_Hashgen.ComputeHash(ByteSourceText)
        'And convert it to String format for return
        Dim SHA1string As String = HexConverter.ToHexString(SHA1_hash)

        Return SHA1string
    End Function

    ' <summary>
    ' Decrypts specified ciphertext using Rijndael symmetric key algorithm.
    ' </summary>
    ' <param name="cipherText">
    ' Base64-formatted ciphertext value.
    ' </param>
    ' <param name="passPhrase">
    ' Passphrase from which a pseudo-random password will be derived. The 
    ' derived password will be used to generate the encryption key. 
    ' Passphrase can be any string. In this example we assume that this 
    ' passphrase is an ASCII string.
    ' </param>
    ' <param name="saltValue">
    ' Salt value used along with passphrase to generate password. Salt can 
    ' be any string. In this example we assume that salt is an ASCII string.
    ' </param>
    ' <param name="hashAlgorithm">
    ' Hash algorithm used to generate password. Allowed values are: "MD5" and
    ' "SHA1". SHA1 hashes are a bit slower, but more secure than MD5 hashes.
    ' </param>
    ' <param name="passwordIterations">
    ' Number of iterations used to generate password. One or two iterations
    ' should be enough.
    ' </param>
    ' <param name="initVector">
    ' Initialization vector (or IV). This value is required to encrypt the 
    ' first block of plaintext data. For RijndaelManaged class IV must be 
    ' exactly 16 ASCII characters long.
    ' </param>
    ' <param name="keySize">
    ' Size of encryption key in bits. Allowed values are: 128, 192, and 256. 
    ' Longer keys are more secure than shorter keys.
    ' </param>
    ' <returns>
    ' Decrypted string value.
    ' </returns>
    ' <remarks>
    ' Most of the logic in this function is similar to the Encrypt 
    ' logic. In order for decryption to work, all parameters of this function
    ' - except cipherText value - must match the corresponding parameters of 
    ' the Encrypt function which was called to generate the 
    ' ciphertext.
    ' </remarks>

    Public Function Decrypt(ByVal cipherText As String) As String

        ' Convert strings defining encryption key characteristics into byte
        ' arrays. Let us assume that strings only contain ASCII codes.
        ' If strings include Unicode characters, use Unicode, UTF7, or UTF8
        ' encoding.
        'Dim initVectorBytes As Byte()
        'initVectorBytes = Encoding.ASCII.GetBytes(initVector)

        'Dim saltValueBytes As Byte()
        'saltValueBytes = Encoding.ASCII.GetBytes(saltValue)

        ' Convert our ciphertext into a byte array.
        Dim cipherTextBytes As Byte()
        cipherTextBytes = Convert.FromBase64String(cipherText)

        ' Define memory stream which will be used to hold encrypted data.
        Dim memoryStream As MemoryStream
        memoryStream = New MemoryStream(cipherTextBytes)

        ' Define memory stream which will be used to hold encrypted data.
        Dim cryptoStream As CryptoStream
        cryptoStream = New CryptoStream(memoryStream, _
                                        Me.m_Decryptor, _
                                        CryptoStreamMode.Read)

        ' Since at this point we don't know what the size of decrypted data
        ' will be, allocate the buffer long enough to hold ciphertext;
        ' plaintext is never longer than ciphertext.
        Dim plainTextBytes As Byte()
        ReDim plainTextBytes(cipherTextBytes.Length)

        ' Start decrypting.
        Dim decryptedByteCount As Integer
        decryptedByteCount = cryptoStream.Read(plainTextBytes, _
                                               0, _
                                               plainTextBytes.Length)

        ' Close both streams.
        memoryStream.Close()
        cryptoStream.Close()

        ' Convert decrypted data into a string. 
        ' Let us assume that the original plaintext string was UTF8-encoded.
        Dim plainText As String
        plainText = Encoding.UTF8.GetString(plainTextBytes, _
                                            0, _
                                            decryptedByteCount)

        ' Return decrypted string.
        Return plainText
    End Function

    Class HexConverter

        Private Shared hexDigits As Char() = {"0"c, "1"c, "2"c, "3"c, "4"c, "5"c, "6"c, "7"c, "8"c, "9"c, "A"c, "B"c, "C"c, "D"c, "E"c, "F"c}

        Public Shared Function ToHexString(ByVal bytes() As Byte) As String

            Dim hexStr As String = ""
            Dim i As Integer

            Dim sb As New System.Text.StringBuilder

            For i = 0 To bytes.Length - 1

                sb.Append(bytes(i).ToString("X").PadLeft(2, "0"c))

            Next

            hexStr = sb.ToString
            Return hexStr.ToUpper

        End Function 'ToHexString

    End Class 'HexConverter


End Class