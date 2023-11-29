using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenCvSharp;
using OpenCvSharp.Extensions;
using OpenCvSharp.Internal;

namespace ImageProcessing
{
    public enum ColorSpaceMethod
    {
        BGR,
        HSV
    };

    public class ProcessImage
    {
        private int[] _acceptedRange;
        private List<int[]> _rejectedRange;

        private Mat _originalImage;
        private Mat _originalImageCopy;
        private Mat _annotatedImage;
        private Mat _acceptedBinaryImage;
        private List<Mat> _rejectedBinaryImage;

        public List<int> _acceptedXPosition;
        private List<int> _rejectedXPosition;

        private List<int> _acceptedIndices;
        private List<int> _rejectedIndices;

        private int _acceptedCount;
        private int _rejectedCount;

        ColorSpaceMethod _method;

        public ProcessImage(int[] acceptedRangeArray, List<int[]> rejectedRangeArray, ColorSpaceMethod method)
        {
            this._acceptedRange = new int[acceptedRangeArray.Length];
            acceptedRangeArray.CopyTo(this._acceptedRange, 0);

            this._rejectedRange = new List<int[]>(rejectedRangeArray);

            this._method = method;

            this._originalImage = new Mat();
            this._originalImageCopy = new Mat();
            this._annotatedImage = new Mat();
            this._acceptedBinaryImage = new Mat();
            this._rejectedBinaryImage = new List<Mat>();
            this._acceptedXPosition = new List<int>();
            this._rejectedXPosition = new List<int>();
            this._acceptedIndices = new List<int>();
            this._rejectedIndices = new List<int>();
        }

        private Mat Segment(int[] range)
        {
            Mat mask = new Mat();

            Scalar low = new Scalar(range[0], range[1], range[2]);
            Scalar high = new Scalar(range[3], range[4], range[5]);

            Cv2.InRange(this._originalImage, low, high, mask);

            return mask;
        }

        private int CreateContourAndDrawBoundingBox(Mat binaryImage, Scalar scalar, bool is_accepted)
        {
            int count = 0;

            Point[][] contours;
            HierarchyIndex[] hierarchy;
            Cv2.FindContours(binaryImage, out contours, out hierarchy, RetrievalModes.External, ContourApproximationModes.ApproxSimple);

            foreach (var contour in contours)
            {
                double area = Cv2.ContourArea(contour);
                if (area < 100)
                {
                    continue;
                }

                Rect boundingBox = Cv2.BoundingRect(contour);
                Cv2.Rectangle(this._annotatedImage, boundingBox, scalar, 2);

                int centerX = boundingBox.X + boundingBox.Width / 2;

                if (is_accepted) { this._acceptedXPosition.Add(centerX); }
                else { this._rejectedXPosition.Add(centerX); }

                count++;
            }

            return count;
        }

        private void SetIndices()
        {
            List<int> allXPosition = this._acceptedXPosition.Concat(this._rejectedXPosition).OrderBy(x => x).ToList();

            this._acceptedIndices = this._acceptedXPosition.Select(x => allXPosition.IndexOf(x)).ToList();
            this._rejectedIndices = this._rejectedXPosition.Select(x => allXPosition.IndexOf(x)).ToList();

            this._acceptedIndices.Sort();
            this._rejectedIndices.Sort();
        }

        private void Processing()
        {
            this._acceptedBinaryImage = this.Segment(_acceptedRange).Clone();
            this._rejectedBinaryImage = new List<Mat>();
            foreach (var range_t in this._rejectedRange)
            {
                this._rejectedBinaryImage.Add(this.Segment(range_t));
            }

            this._acceptedCount = this.CreateContourAndDrawBoundingBox(this._acceptedBinaryImage, Scalar.Blue, true);
            foreach (Mat binaries in this._rejectedBinaryImage)
            {
                this._rejectedCount = this.CreateContourAndDrawBoundingBox(binaries, Scalar.Red, false);
            }

            this.SetIndices();
        }

        public void SetInputImage(String path)
        {
            this._originalImage = Cv2.ImRead(path);

            if (this._originalImage.Empty()) return;

            this._originalImageCopy = this._originalImage.Clone();
            this._annotatedImage = this._originalImage.Clone();

            if (this._method == ColorSpaceMethod.HSV)
            {
                Cv2.CvtColor(this._originalImage, this._originalImage, ColorConversionCodes.BGR2HSV);
            }

            this.Processing();
        }

        public void SetInputImage(Mat image)
        {
            this._originalImage = image.Clone();

            if (this._originalImage.Empty()) return;

            this._originalImageCopy = this._originalImage.Clone();
            this._annotatedImage = this._originalImage.Clone();

            if (this._method == ColorSpaceMethod.HSV)
            {
                Cv2.CvtColor(this._originalImage, this._originalImage, ColorConversionCodes.BGR2HSV);
            }

            this.Processing();
        }

        public List<int> GetAcceptedIndices()
        {
            if (this._originalImage.Empty())
            {
                throw new Exception("Image has not been assigned");
            }

            return new List<int>(this._acceptedIndices);
        }

        public List<int> GetRejectedIndices()
        {
            if (this._originalImage.Empty())
            {
                throw new Exception("Image has not been assigned");
            }

            return new List<int>(this._rejectedIndices);
        }

        public int GetAcceptedCount()
        {
            if (this._originalImage.Empty())
            {
                throw new Exception("Image has not been assigned");
            }

            return this._acceptedCount;
        }

        public int GetRejectedCount()
        {
            if (this._originalImage.Empty())
            {
                throw new Exception("Image has not been assigned");
            }

            return this._rejectedCount;
        }

        public Mat GetAnnotatedImage()
        {
            if (this._originalImage.Empty())
            {
                throw new Exception("Image has not been assigned");
            }

            return this._annotatedImage.Clone();
        }

        public Mat GetOriginalImage()
        {
            if (this._originalImage.Empty())
            {
                throw new Exception("Image has not been assigned");
            }

            return this._originalImageCopy.Clone();
        }

        public System.Drawing.Bitmap GetOriginalBitmapImage()
        {
            if (this._originalImage.Empty())
            {
                throw new Exception("Image has not been assigned");
            }

            System.Drawing.Bitmap bitmap = this._originalImageCopy.ToBitmap();

            return bitmap;
        }

        public System.Drawing.Bitmap GetAnnotatedBitmapImage()
        {
            if (this._originalImage.Empty())
            {
                throw new Exception("Image has not been assigned");
            }

            System.Drawing.Bitmap myimage = BitmapConverter.ToBitmap(this._annotatedImage);

            return myimage;
        }
    }
}