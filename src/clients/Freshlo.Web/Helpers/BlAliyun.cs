using Aliyun.OSS;
using Freshlo.DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Freshlo.Web.Helpers
{
    public class BlAliyun
    {

        public static string PutIconObjectFromFile(string filename, string itemId, string fileToUpload, AliyunCredential credential, string folderName, string aliyunfolder)
        {

            string key = aliyunfolder + "/" + folderName + "/" + itemId + ".png";
            var client = new OssClient(credential.Endpoint, credential.AccessKeyId, credential.AccessKeySecret);
            try
            {
                client.PutObject(credential.BucketName, key, fileToUpload);
                return "true";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
        public static string PutIconObjectFromFile2(string filename, string itemId, string fileToUpload, AliyunCredential credential, string folderName, string aliyunfolder)
        {

            string key = aliyunfolder + "/" + folderName + "/" + itemId + ".pdf";
            var client = new OssClient(credential.Endpoint, credential.AccessKeyId, credential.AccessKeySecret);
            try
            {
                client.PutObject(credential.BucketName, key, fileToUpload);
                return "true";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
        public static string PutIconObjectFromFile1(string filename, string itemId, string fileToUpload, AliyunCredential credential, string folderName, string aliyunfolder)
        {

            string key = aliyunfolder + "/" + folderName + "/" + itemId + ".png";
            var client = new OssClient(credential.Endpoint, credential.AccessKeyId, credential.AccessKeySecret);
            try
            {
                client.PutObject(credential.BucketName, key, fileToUpload);
                return "true";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
        public static List<string> GetObjectFromFile(string ItemId, AliyunCredential credential)
        {
            var imageName = new List<string>();
            var client = new OssClient(credential.Endpoint, credential.AccessKeyId, credential.AccessKeySecret);
            var key = "HurTex/Product-image/" + ItemId;
            try
            {
                var listResult = client.ListObjects(credential.BucketName, key);

                foreach (var summary in listResult.ObjectSummaries)
                {
                    if (summary.Key.Contains("_"))
                    {
                        imageName.Add(summary.Key);
                    }
                }

            }
            catch (Exception ex)
            {
                // Ignored
            }
            return imageName;
        }
        public static string PutGallaryObjectFromFile(string filename, string itemId, string fileToUpload, AliyunCredential credential, string folderName)
        {
            string key = "HurTex/Product-image/" + itemId;
            var client = new OssClient(credential.Endpoint, credential.AccessKeyId, credential.AccessKeySecret);
            try
            {
                client.PutObject(credential.BucketName, key, fileToUpload);
                return "true";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
        public static List<byte[]> GetDowloadObject(AliyunCredential credential, string key, string itemId)
        {
            var ItemId = itemId.Split("_")[0];
            List<byte[]> data = new List<byte[]>();
            try
            {
                string galleryImage = "https://freshlo.oss-ap-south-1.aliyuncs.com/" + key;
                string iconImage = "https://freshlo.oss-ap-south-1.aliyuncs.com/HurTex/Product-image/" + ItemId + "_0" + ".png";
                using (var webClient = new WebClient())
                {
                    byte[] galleryimageBytes = webClient.DownloadData(galleryImage);
                    data.Add((galleryimageBytes));
                    try
                    {
                        byte[] iconImageByte = webClient.DownloadData(iconImage);
                        data.Add(iconImageByte);
                    }
                    catch (Exception e)
                    {

                    }

                }
                return data;
            }
            catch (Exception e)
            {
                return data;
            }

        }
        public static bool DeleteObjectFromFile(string key, AliyunCredential credential)
        {
            var client = new OssClient(credential.Endpoint, credential.AccessKeyId, credential.AccessKeySecret);
            try
            {
                key = "HurTex/" + key;
                client.DeleteObject(credential.BucketName, key);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static List<string> GetObjectIcon(string ItemId, AliyunCredential credential)
        {
            var imageName = new List<string>();
            var client = new OssClient(credential.Endpoint, credential.AccessKeyId, credential.AccessKeySecret);
            var key = "HurTex/Product-image/" + ItemId + "";
            try
            {
                var listResult = client.ListObjects(credential.BucketName, key);

                foreach (var summary in listResult.ObjectSummaries)
                {
                    imageName.Add(summary.Key);
                }
            }
            catch (Exception ex)
            {
                // Ignored
            }
            return imageName;
        }


        public static List<string> GetNewObjectFromFile(string ItemId, AliyunCredential credential, int imagesCount)
        {
            var imageName = new List<string>();
            var client = new OssClient(credential.Endpoint, credential.AccessKeyId, credential.AccessKeySecret);
            var key = "HurTex/Product-image/" + ItemId;
            int a = 0;
            try
            {
                var listResult = client.ListObjects(credential.BucketName, key);
                key = listResult.ObjectSummaries.Last().Key;
                if (listResult.ObjectSummaries.Count() > 0)
                {
                    foreach (var summary in listResult.ObjectSummaries)
                    {
                        if (a > 0)
                        {
                            if (summary.Key.Contains("_"))
                            {
                                imageName.Add(summary.Key);
                            }
                        }
                        a++;
                    }
                }
            }
            catch (Exception ex)
            {
                // Ignored
            }
            return imageName;
        }



        public static string GetLastObjectFromFile(string ItemId, AliyunCredential credential, int imagesCount)
        {
            var imageName = "";
            var client = new OssClient(credential.Endpoint, credential.AccessKeyId, credential.AccessKeySecret);
            var key = "HurTex/Product-image/" + ItemId;
            int a = 0;
            try
            {
                var listResult = client.ListObjects(credential.BucketName, key);
                imageName = listResult.ObjectSummaries.Last().Key.Split("_")[1].Split(".png")[0];
                
            }
            catch (Exception ex)
            {
                // Ignored
            }
            return imageName;
        }

    }
}
