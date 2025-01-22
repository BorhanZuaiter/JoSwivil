using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;

namespace Domain.Helpers
{
    public class StorageConnection
    {
        private readonly AmazonS3Client _client;

        public StorageConnection()
        {
            var config = new AmazonS3Config
            {
                ServiceURL = "https://mazadkom.fra1.digitaloceanspaces.com",
                ForcePathStyle = true
            };

            var credentials = new Amazon.Runtime.BasicAWSCredentials("DO00AEFGLWD8HL8D3H7A", "VDivJMi1a9a9uSLoaZvWRKSejI+WH4W2yGD4XMhwVLc");
            _client = new AmazonS3Client(credentials, config);
        }
        public async Task<string> SaveOnStorage(IFormFile formFile, string path)
        {
            using var memoryStream = new MemoryStream();
            formFile.CopyTo(memoryStream);

            var fileName = Path.Combine(Path.ChangeExtension(Path.GetRandomFileName(), Path.GetExtension(formFile.FileName)));
            var filePath = $"production/Resources/{path}/{fileName}";
            var uploadRequest = new PutObjectRequest
            {
                BucketName = "mazadkom",
                Key = filePath,
                InputStream = memoryStream,
                ContentType = formFile.ContentType,
                CannedACL = S3CannedACL.PublicRead
            };

            await _client.PutObjectAsync(uploadRequest);

            return $"https://mazadkom.fra1.digitaloceanspaces.com/mazadkom/{filePath}";
        }
        public async Task<string> UpdateFileAsync(string oldFilePath, IFormFile formFile, string path)
        {
            if (oldFilePath.Contains("digitaloceanspaces.com"))
            {
                int indexOfImages = oldFilePath.IndexOf("production/Resources");
                string oldpath = oldFilePath.Substring(indexOfImages);
                await _client.DeleteObjectAsync(new DeleteObjectRequest
                {
                    BucketName = "mazadkom",
                    Key = oldpath,
                });
            }

            using var memoryStream = new MemoryStream();
            formFile.CopyTo(memoryStream);

            var fileName = Path.Combine(Path.ChangeExtension(Path.GetRandomFileName(), Path.GetExtension(formFile.FileName)));
            var filePath = $"production/Resources/{path}/{fileName}";
            var uploadRequest = new PutObjectRequest
            {
                BucketName = "mazadkom",
                Key = filePath,
                InputStream = memoryStream,
                ContentType = formFile.ContentType,
                CannedACL = S3CannedACL.PublicRead
            };
            await _client.PutObjectAsync(uploadRequest);

            return $"https://mazadkom.fra1.digitaloceanspaces.com/mazadkom/{filePath}";
        }
    }
}
