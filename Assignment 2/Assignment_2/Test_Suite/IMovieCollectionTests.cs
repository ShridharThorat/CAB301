using System;
namespace Test_Suite
{
    public interface IMovieCollectionTests
    {
        // IsEmpty
        [TestMethod]
        public void IsEmpty_True_empty_collection();
        [TestMethod]
        public void IsEmpty_False_single_collection();


        // Insert
        [TestMethod]
        public void Insert_root(); // Insert a node into an empty tree
        [TestMethod]
        public void Insert_root_RChild(); // Inserts correct node as right child
        [TestMethod]
        public void Insert_root_LChild(); // Inserts correct node as left child
        [TestMethod]
        public void Insert__duplicate(); // returns false


        // ToArray
        [TestMethod]
        public void ToArray_empty_collection(); // empty collection is IMovie[0]
        [TestMethod]
        public void ToArray_single_collection(); // Works for just root
        [TestMethod]
        public void ToArray_large_collection(); // Ordered correctly in large


        // Delete
        [TestMethod]
        public void Delete_root_single_collection(); // if(parent == null)
        [TestMethod]
        public void Delete_root_has_only_LChild_leaf();
        [TestMethod]
        public void Delete_root_has_only_RChild_leaf();
        [TestMethod]
        public void Delete_node_has_only_LChild_leaf(); // else if (hasLeftChild && !hasRightChild)
        [TestMethod]
        public void Delete_node_has_only_RChild_leaf(); // else if (!hasLeftChild && hasRightChild)
        [TestMethod]
        public void Delete_node_has_LChild_and_RChild_as_leaves(); // else if ((thisMovie.LChild != null) && (thisMovie.RChild != null)) 
        [TestMethod]
        public void Delete_node_from_empty_collection();
        [TestMethod]
        public void Delete_node_not_in_large_collection();


        // Search
        [TestMethod]
        public void Search_empty_collection();
        [TestMethod]
        public void Search_exists_single_collection();
        [TestMethod]
        public void Search_doesnt_exist_single_collection();
        [TestMethod]
        public void Search_exists_large_collection();
        [TestMethod]
        public void Search_doesnt_exist_large_collection();


        //NoDVDS
        [TestMethod]
        public void NoDVDS_empty_collection();
        [TestMethod]
        public void NoDVDS_single_collection();
        [TestMethod]
        public void NoDVDS_large_collection();


        // Clear
        [TestMethod]
        public void Clear_empty_collection();
        [TestMethod]
        public void Clear_single_collection();
        [TestMethod]
        public void Clear_large_collection();

    }
}

