//A class that models a node of a binary search tree
//An instance of this class is a node in the binary search tree 
public class BTreeNode
{
	private IMovie movie; // movie
	private BTreeNode? lchild; // reference to its left child 
	private BTreeNode? rchild; // reference to its right child

	public BTreeNode(IMovie movie)
	{
		this.movie = movie;
		lchild = null;
		rchild = null;
	}

	public IMovie Movie
	{
		get { return movie; }
		set { movie = value; }
	}

	public BTreeNode? LChild
	{
		get { return lchild; }
		set { lchild = value; }
	}

	public BTreeNode? RChild
	{
		get { return rchild; }
		set { rchild = value; }
	}
}

// invariant: no duplicate movie in this movie collection
public class MovieCollection : IMovieCollection
{
	private BTreeNode? root; // the root of the binary search tree in which movies are stored 
	private int count; // the number of movies currently stored in this movie collection 
	public int Number { get { return count; } }

	// constructor - create an empty movie collection
	public MovieCollection()
	{
		root = null;
		count = 0;
	}

	public bool IsEmpty()
	{
		if (this.Number == 0)
		{
			Console.WriteLine("Movie collection is empty.");
			return true;
		}
		else return false;
	}

	public bool Insert(IMovie movie)
	{
		BTreeNode? current = root;
		BTreeNode newNode = new BTreeNode(movie);
		bool inserted = false;
		if (current == null)    // Tree is empty
		{
			root = newNode;
			count++;
			inserted = true;
		}
		else
		{
			while (inserted != true)
			{
				int comparison = movie.CompareTo(current.Movie);
				switch (comparison)
				{
					case 1: // movie is after current
						if (current.RChild == null)     // if there is no movie after current
						{
							current.RChild = newNode;   // add it to the tree
							count++;
							// Console.WriteLine("movie is a RChild");
							inserted = true;
						}
						else current = current.RChild;  // otherwise compare with right child
						break;
					case -1:                            // if there is no movie before current
						if (current.LChild == null)
						{
							current.LChild = newNode;
							count++;
							inserted = true;
							// Console.WriteLine("movie is a LChild");
						}
						else current = current.LChild;
						break;
					case 0:                             // Movie already exists
						Console.WriteLine($"{newNode.Movie.Title} already exists");
						return inserted;
				}
			}
		}
		return inserted;
	}

	public bool Delete(IMovie movie)
	{
		if (movie == null) { return false; }
		BTreeNode? parent = null;   // parent of this movie
		BTreeNode? thisMovie = root;  // traversing from begining
		bool thisMovieIsLeft = false;  // false is right, true is left
		bool thisMovieIsRight = false;  // false is right, true is left
		int? comparison = null;

		// Quick exit if the collection is empty
		if (this.IsEmpty())
		{
			Console.WriteLine("Insert movies before being able to delete them");
			return false;
		}
		// Find this movie & its parent in the tree. Not done using Search
		// so we can get the parent as well
		while (thisMovie != null && thisMovie.Movie.CompareTo(movie) != 0)
		{
			comparison = thisMovie.Movie.CompareTo(movie);
			parent = thisMovie;
			switch (comparison)
			{
				case -1: // thisMovie is greater than movie
					thisMovie = thisMovie.RChild;
					thisMovieIsRight = true;
					thisMovieIsLeft = false;
					break;
				case 1: //  thisMovie is greater than movie
					// therefore, movie is to the left
					thisMovie = thisMovie.LChild;
					thisMovieIsLeft = true;
					thisMovieIsRight = false;
					break;
			}
		}

		// Quick exit if the movie is null (doesn't exist)
		if (thisMovie == null)
		{
			Console.WriteLine("This movie doesn't exist");
			return false;
		}
		bool hasLeftChild = (thisMovie.LChild != null);
		bool hasRightChild = (thisMovie.RChild != null);


		// This movie has no children (is a leaf)
		if (!hasLeftChild && !hasRightChild)
		{
			if (thisMovie == root) { root = null; }
			if (parent != null)
			{
				if (thisMovieIsLeft) parent.LChild = null;
				else if (thisMovieIsRight) parent.RChild = null;
			}
			count--;
		}

		// This movie has only a left child
		else if (hasLeftChild && !hasRightChild) // & right is null
		{
			if (thisMovie == root) { root = null; root = thisMovie.LChild; }
			if (parent != null)
			{
				if (thisMovieIsLeft) parent.LChild = thisMovie.LChild; // parent's left child is now this movies left child
				else if (thisMovieIsRight) parent.RChild = thisMovie.LChild; // parent's right child is now this movies left child
			}
			count--;
		}
		// This movie has only a right child
		else if (!hasLeftChild && hasRightChild)  // & left is null
		{
			if (thisMovie == root) { root = null; root = thisMovie.RChild; }
			if (parent != null)
			{
				if (thisMovieIsLeft) parent!.LChild = thisMovie.RChild; // parent's left child is now this movies right child
				else if (thisMovieIsRight) parent.RChild = thisMovie.RChild; // parent's right child is now this movies right child
			}
			count--;
		}

		// This movie has 2 children
		// explicit instead of variables so no warning thrown
		else if ((thisMovie.LChild != null) && (thisMovie.RChild != null))
		{
			// find the rightmost node in the left subtree                
			BTreeNode replacement = thisMovie.LChild;
			BTreeNode replacementParent = thisMovie;
			Find_RightMostNode_In_Left_Subtree(thisMovie, ref replacement, ref replacementParent);
			// Replace thisMovie with replacement
			thisMovie.Movie = replacement.Movie;
			if (replacement == thisMovie.LChild) thisMovie.LChild = replacement.LChild;
			else replacementParent.RChild = replacement.LChild;
			count--;
		}

		return true;
	}

	// Helper method for Delete
	// Finds the right most node (replacement) in the left sub tree of thisMovie
	private static void Find_RightMostNode_In_Left_Subtree(BTreeNode thisMovie, ref BTreeNode replacement, ref BTreeNode replacementParent)
	{
		while (replacement.RChild != null)
		{
			replacementParent = replacement;
			replacement = replacement.RChild;
		}
	}


	public IMovie? Search(string title)
	{
		IMovie? returnedMovie = null;
		if (title == null)
		{
			Console.WriteLine($"The movie title '{title}' does not exist in the collection");
		}
		else if (this.root == null) // explicit to prevent warnings
		{
			Console.WriteLine("Cannot search in an empty collection");
		}
		else
		{
			// Traversing and finding the movie
			BTreeNode? start = root;
			IMovie thisMovie = new Movie(title);
			int i = 0;
			while (i <= this.Number)
			{
				int comparison = start.Movie.CompareTo(thisMovie);
				if (comparison == 0)
				{
					returnedMovie = start.Movie;
				}
				else if (start.LChild != null)
				{
					start = start.LChild;
				}
				else if (start.RChild != null)
				{
					start = start.RChild;
				}
				i++;
			}
		}
		return returnedMovie;
	}

	public int NoDVDs()
	{
		int total = CountNoDVDs(root);
		return total;
	}

	// Recursively counts the number of dvds for each node in the tree
	// starting at the 'node'
	private int CountNoDVDs(BTreeNode? node)
	{
		if (node == null) { return 0; } // node doesn't exist -> no dvds
		int dvds = node.Movie.TotalCopies;
		dvds += CountNoDVDs(node.LChild); // left-subtree for a node
		dvds += CountNoDVDs(node.RChild); // right-subtree for a node
		return dvds;
	}

	public IMovie[] ToArray()
	{
		int i = 0;
		IMovie[] array = new IMovie[this.Number]; // creates an array of same size
		InOrder_Traversal(ref i, this.root, array); // start traversal from root
		return array;
	}

	// Ordered alphabetically using the In-order traversal method that
	// is an implementation of the depth first search method
	private void InOrder_Traversal(ref int i, BTreeNode? currentNode, IMovie[] array)
	{
		if (currentNode != null)
		{
			// find the left leaf of the current node and work 
			if (currentNode.LChild != null)
			{
				InOrder_Traversal(ref i, currentNode.LChild, array);
			}
			// runs once left leaf is found. Then added to array
			array[i] = currentNode.Movie; i++;
			// once left most are added, traverse right branch if it exists
			if (currentNode.RChild != null)
			{
				InOrder_Traversal(ref i, currentNode.RChild, array);
			}
		}
	}

	// garbage collector should delete all other nodes
	// since every node's existence depends on the root's existence
	public void Clear()
	{
		root = null; 
		this.count = 0;
	}
}