using System;
namespace Test_Suite
{
    public interface IMovieTests
    {       
        [TestMethod]
        public void CompareTo_Upper();
        [TestMethod]
        public void CompareTo_Lower();
        [TestMethod]
        public void CompareTo_Same_with_same_object();
        [TestMethod]
        public void CompareTo_Same_with_different_object();


        [TestMethod]
        public void ToString_all_properties();
        [TestMethod]
        public void ToString_only_Movie_Title();
    }
}

