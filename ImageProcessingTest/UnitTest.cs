using ImageProcessing;

namespace ImageProcessingTest
{
    public class Function
    {
        public static String GetPath(String imageName)
        {
            String path = Directory.GetCurrentDirectory() + "/../../../Images/" + imageName;
            return path;
        }
    }

    public interface ITest
    {
        ProcessImage GetInstance();
        void AcceptedCountTest();
        void RejectedCountTest();
        void AcceptedIndicesTest();
        void RejectedIndicesTest();
    }

    [TestClass]
    public class AppleTest : ITest
    {
        public ProcessImage GetInstance()
        {
            int[] accepted = { 28, 30, 32, 36, 255, 255 };
            int[] rejected1 = { 171, 74, 56, 255, 255, 255 };

            List<int[]> rejectedList = new List<int[]> { rejected1 };

            ProcessImage processImage =  new ProcessImage(accepted, rejectedList, ColorSpaceMethod.HSV);

            String imagePath = Function.GetPath("apple.jpg");
            processImage.SetInputImage(imagePath);

            return processImage;
        }

        [TestMethod]
        public void AcceptedCountTest()
        {
            ProcessImage processImage = GetInstance();

            int expectedAcceptedCount = 2;
            int actualAcceptedCount = processImage.GetAcceptedCount();

            Assert.AreEqual(expectedAcceptedCount, actualAcceptedCount);
        }

        [TestMethod]
        public void RejectedCountTest()
        {
            ProcessImage processImage = GetInstance();

            int expectedRejectedCount = 2;
            int actualRejectedCount = processImage.GetRejectedCount();

            Assert.AreEqual(expectedRejectedCount, actualRejectedCount);
        }

        [TestMethod]
        public void AcceptedIndicesTest()
        {
            ProcessImage processImage = GetInstance();

            List<int> expectedAcceptedIndices = new List<int>() { 1, 3 };

            List<int> actualAcceptedIndices = processImage.GetAcceptedIndices();

            CollectionAssert.AreEqual(expectedAcceptedIndices, actualAcceptedIndices);
        }

        [TestMethod]
        public void RejectedIndicesTest()
        {
            ProcessImage processImage = GetInstance();

            List<int> expectedRejectedIndices = new List<int>() { 0, 2 };

            List<int> actualRejectedIndices = processImage.GetRejectedIndices();

            CollectionAssert.AreEqual(expectedRejectedIndices, actualRejectedIndices);
        }

    }

    [TestClass]
    public class PaprikaTest : ITest
    {
        public ProcessImage GetInstance()
        {
            int[] accepted = { 17, 45, 230, 25, 230, 255 };
            int[] rejected1 = { 38, 35, 0, 50, 255, 255 };
            int[] rejected2 = { 0, 55, 120, 10, 255, 255 };

            List<int[]> rejectedList = new List<int[]> { rejected1, rejected2 };

            ProcessImage processImage = new ProcessImage(accepted, rejectedList, ColorSpaceMethod.HSV);

            String imagePath = Function.GetPath("apple2.jpeg");
            processImage.SetInputImage(imagePath);

            return processImage;
        }

        [TestMethod]
        public void AcceptedCountTest()
        {
            ProcessImage processImage = GetInstance();

            int expectedAcceptedCount = 1;
            int actualAcceptedCount = processImage.GetAcceptedCount();

            Assert.AreEqual(expectedAcceptedCount, actualAcceptedCount);
        }

        [TestMethod]
        public void RejectedCountTest()
        {
            ProcessImage processImage = GetInstance();

            int expectedRejectedCount = 2;
            int actualRejectedCount = processImage.GetRejectedCount();

            Assert.AreEqual(expectedRejectedCount, actualRejectedCount);
        }

        [TestMethod]
        public void AcceptedIndicesTest()
        {
            ProcessImage processImage = GetInstance();

            List<int> expectedAcceptedIndices = new List<int>() { 1 };

            List<int> actualAcceptedIndices = processImage.GetAcceptedIndices();

            CollectionAssert.AreEqual(expectedAcceptedIndices, actualAcceptedIndices);
        }

        [TestMethod]
        public void RejectedIndicesTest()
        {
            ProcessImage processImage = GetInstance();

            List<int> expectedRejectedIndices = new List<int>() { 0, 2 };

            List<int> actualRejectedIndices = processImage.GetRejectedIndices();

            CollectionAssert.AreEqual(expectedRejectedIndices, actualRejectedIndices);
        }
    }
}