using Amazon;
using Amazon.Rekognition;
using Amazon.Rekognition.Model;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;

namespace WinFormsApp;

public partial class Form1 : Form
{
    private static string _bucket = "mihailrekognitionbucket";
    private static BasicAWSCredentials _credentials = new BasicAWSCredentials("AKIA47CRZUCYIGCXZ2XN", "8Y1t6QmTMW0XH+iMKQfC5hyoNHTe4gFpM6bGFQsR");
    //private static BasicAWSCredentials _credentials = new BasicAWSCredentials("AKIA47CRZUCYJYZGDZU2", "SuanU17JgBid7TuXLcwPUvvIV4yvVtcTbFJt6dxV");
    private static string _fileName = string.Empty;
    private List<TextDetection> _textDetections = new List<TextDetection>();
    private static IAmazonS3 s3Client = new AmazonS3Client(_credentials, RegionEndpoint.EUWest1);

    public Form1()
    {
        InitializeComponent();
        this.FormClosing += async (Object sender, FormClosingEventArgs e) => await DeleteObjectNonVersionedBucketAsync();
    }

    private async void Load_btn_Click(object sender, EventArgs e)
    {
        using (OpenFileDialog openFileDialog = new OpenFileDialog())
        {
            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "Image Files(*.jpg; *.jpeg; *.bmp)|*.jpg; *.jpeg; *.bmp";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                await WritingAnObjectAsync(openFileDialog.FileName);
                RekognizeText();
                MessageBox.Show("Processing file...");
            }
        }
    }

    private async void Remove_btn_Click(object sender, EventArgs e)
    {
        await DeleteObjectNonVersionedBucketAsync();
        this.Img_pb.Image = null;
    }

    private async Task WritingAnObjectAsync(string path)
    {
        try
        {
            _fileName = $"{DateTime.Now.ToShortDateString()}T{DateTime.Now.ToLongTimeString()}{path.Substring(path.LastIndexOf('.'))}";

            using (MemoryStream ms = new MemoryStream(File.ReadAllBytes(path)))
            {
                ms.Position = 0;

                PutObjectRequest putRequest = new PutObjectRequest
                {
                    BucketName = _bucket,
                    Key = _fileName,
                    InputStream = ms,
                    ContentType = "image/jpeg"
                };

                await s3Client.PutObjectAsync(putRequest);
            }
        }
        catch (AmazonS3Exception e)
        {
            Console.WriteLine("Error encountered on server. Message:'{0}' when deleting an object", e.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine("Unknown encountered on server. Message:'{0}' when deleting an object", e.Message);
        }
    }

    private async Task<Bitmap> GetAnObjectAsync()
    {
        try
        {
            GetObjectRequest getRequest = new GetObjectRequest
            {
                BucketName = _bucket,
                Key = _fileName,
            };

            using (var getResponse = await s3Client.GetObjectAsync(getRequest))
                return new Bitmap(getResponse.ResponseStream);
        }
        catch (AmazonS3Exception e)
        {
            Console.WriteLine("Error encountered on server. Message:'{0}' when deleting an object", e.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine("Unknown encountered on server. Message:'{0}' when deleting an object", e.Message);
        }
        return new Bitmap(661, 426);
    }

    private async void RekognizeText()
    {
        AmazonRekognitionClient rekognitionClient = new AmazonRekognitionClient(_credentials, RegionEndpoint.EUWest1);

        try
        {
            DetectTextRequest detectTextRequest = new DetectTextRequest()
            {
                Image = new Amazon.Rekognition.Model.Image()
                {
                    S3Object = new Amazon.Rekognition.Model.S3Object()
                    {
                        Name = _fileName,
                        Bucket = _bucket
                    }
                }
            };

            DetectTextResponse detectTextResponse = await rekognitionClient.DetectTextAsync(detectTextRequest);
            _textDetections = detectTextResponse.TextDetections;
        }
        catch (AmazonS3Exception e)
        {
            Console.WriteLine("Error encountered on server. Message:'{0}' when deleting an object", e.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine("Unknown encountered on server. Message:'{0}' when deleting an object", e.Message);
        }
        Img_pb.Image = await GetAnObjectAsync();
    }

    private async Task DeleteObjectNonVersionedBucketAsync()
    {
        try
        {
            var deleteObjectRequest = new DeleteObjectRequest
            {
                BucketName = _bucket,
                Key = _fileName
            };
            await s3Client.DeleteObjectAsync(deleteObjectRequest);
            _fileName = string.Empty;
        }
        catch (AmazonS3Exception e)
        {
            Console.WriteLine("Error encountered on server. Message:'{0}' when deleting an object", e.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine("Unknown encountered on server. Message:'{0}' when deleting an object", e.Message);
        }
    }

    private async void Search_btn_Click(object sender, EventArgs e)
    {
        if (_fileName != string.Empty) Img_pb.Image = await GetAnObjectAsync();
        if (_textDetections.Count == 0 || Search_tb.Text.Equals(string.Empty)) return;
        Bitmap bmp = new Bitmap(Img_pb.Image);
        using (Graphics g = Graphics.FromImage(bmp))
        using (Pen pen = new Pen(Color.Red, 2))
        {
            foreach (TextDetection text in _textDetections.Where(td => td.DetectedText.ToLower().Contains(Search_tb.Text.ToLower())))
            {
                BoundingBox box = text.Geometry.BoundingBox;

                int left = (int)(box.Left * bmp.Width);
                int top = (int)(box.Top * bmp.Height);
                int width = (int)(box.Width * bmp.Width);
                int height = (int)(box.Height * bmp.Height);

                Rectangle rect = new Rectangle(left, top, width, height);

                g.DrawRectangle(pen, rect);
            }
        }

        Img_pb.Image = bmp;
    }
}