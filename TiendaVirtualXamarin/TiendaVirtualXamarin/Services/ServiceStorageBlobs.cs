using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace TiendaVirtualXamarin.Services
{
    public class ServiceStorageBlobs
    {
        private BlobServiceClient client;
        protected string Productos = "productos";
        protected string Usuarios = "usuarios";

        public ServiceStorageBlobs(BlobServiceClient client)
        {
            this.client = client;
        }

        public async Task DeleteBlobAsync(string containerName, string blobName)
        {
            BlobContainerClient folder = this.client.GetBlobContainerClient(containerName);
            await folder.DeleteBlobAsync(blobName);
        }

        public async Task<List<string>> GetBlobsAsync(string containerName)
        {
            BlobContainerClient folder = this.client.GetBlobContainerClient(containerName);
            List<string> imagenes = new List<string>();
            //List<Blob> items = await folder.GetBlobsAsync();
            //foreach (BlobItem images in folder.GetBlobsAsync())
            //{
            //    imagenes.Add(images.Name);
            //}
            return imagenes;
        }

        public async Task UploadBlobAsync(string containerName, string blobName, Stream stream)
        {
            BlobContainerClient containerClient = this.client.GetBlobContainerClient(containerName);
            BlobClient blobClient = containerClient.GetBlobClient(blobName);
            await blobClient.UploadAsync(stream, overwrite: true);
        }
    }
}
