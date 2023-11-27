using ImageProcessing;

namespace ImageProcessingTest
{
    [TestClass]
    public class UnitTest1
    {
        private String GetPath(String imageName)
        {
            String path = Directory.GetCurrentDirectory() + "/../../../Images/" + imageName;
            return path;
        }

        private ProcessImage GetAppleTest()
        {
            int[] accepted = { 28, 30, 32, 36, 255, 255 };
            int[] rejected1 = { 171, 74, 56, 255, 255, 255 };

            List<int[]> rejectedList = new List<int[]> { rejected1 };

            ProcessImage processImage =  new ProcessImage(accepted, rejectedList, ColorSpaceMethod.HSV);

            String imagePath = GetPath("apple.jpg");
            processImage.SetInputImage(imagePath);

            return processImage;
        }

        [TestMethod]
        public void AppleAcceptedCountTest()
        {
            ProcessImage processImage = GetAppleTest();

            int expectedAcceptedCount = 2;
            int actualAcceptedCount = processImage.GetAcceptedCount();

            Assert.AreEqual(expectedAcceptedCount, actualAcceptedCount);
        }

        [TestMethod]
        public void AppleRejectedCountTest()
        {
            ProcessImage processImage = GetAppleTest();

            int expectedRejectedCount = 2;
            int actualRejectedCount = processImage.GetRejectedCount();

            Assert.AreEqual(expectedRejectedCount, actualRejectedCount);
        }

        [TestMethod]
        public void AppleAcceptedIndicesTest()
        {
            ProcessImage processImage = GetAppleTest();

            List<int> expectedAcceptedIndices = new List<int>();
            expectedAcceptedIndices.Add(1);
            expectedAcceptedIndices.Add(3);

            List<int> actualAcceptedIndices = processImage.GetAcceptedIndices();

            CollectionAssert.AreEqual(expectedAcceptedIndices, actualAcceptedIndices);
        }

        [TestMethod]
        public void AppleRejectedIndicesTest()
        {
            ProcessImage processImage = GetAppleTest();

            List<int> expectedRejectedIndices = new List<int>();
            expectedRejectedIndices.Add(0);
            expectedRejectedIndices.Add(2);

            List<int> actualRejectedIndices = processImage.GetRejectedIndices();

            CollectionAssert.AreEqual(expectedRejectedIndices, actualRejectedIndices);
        }

    }
}