//CAB301 assessment 2 - 2023
//An implementation of Movie ADT
using System;
using System.Collections.Generic;

namespace Assignment_2
{
    public class Movie : IMovie
    {
        private string title;  // the titleof this movie
        private MovieGenre genre;  // the genre of this movie
        private MovieClassification classification; // the classification of this movie
        private int duration; // the duration of this movie in minutes
        private int availablecopies; // the number of DVDs (copies) of this movie that are currently available in the library
        private int totalcopies; // the total number of copies of this movie

        // a constructor 
        public Movie(string t, MovieGenre g, MovieClassification c, int d, int n)
        {
            title = t;
            genre = g;
            classification = c;
            duration = d;
            availablecopies = n;
            totalcopies = n;
        }

        // another constructor
        public Movie(string t)
        {
            title = t;
        }

        // get and set the tile of this movie
        public string Title { get { return title; } set { title = value; } }

        //get and set the genre of this movie
        public MovieGenre Genre { get { return genre; } set { genre = value; } }

        //get and set the classification of this movie
        public MovieClassification Classification { get { return classification; } set { classification = value; } }

        //get and set the duration of this movie
        public int Duration { get { return duration; } set { duration = value; } }

        //get and set the number of DVDs of this movie currently available in the library
        public int AvailableCopies { get { return availablecopies; } set { availablecopies = value; } }

        //get and set the total number of DVDs of this movie in the library
        public int TotalCopies { get { return totalcopies; } set { totalcopies = value; } }


        public int CompareTo(IMovie another)
        {
            if (another == null) { return 0; }
            int comparison = String.CompareOrdinal(this.Title, another.Title);
            if (comparison < 0) { return -1; }
            else if (comparison > 0) { return 1; }
            else { return 0; }
        }

        public override string ToString()
        {
            return $"Movie(Title: {Title}, Genre: {Genre}, Classification: {Classification}, Duration: {Duration})";
        }
    }

}