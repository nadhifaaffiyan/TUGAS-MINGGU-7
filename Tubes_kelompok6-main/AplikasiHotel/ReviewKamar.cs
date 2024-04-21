using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplikasiHotel
{
    public class ReviewKamar<T>
    {
        public T RoomNumber { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
    }

    public class HotelReviewSystem<T>
    {
        private List<ReviewKamar<T>> reviews;

        public HotelReviewSystem()
        {
            reviews = new List<ReviewKamar<T>>();
        }

        public void AddReview(RoomReview<T> review)
        {
            if (review.Rating < 1 || review.Rating > 5)
            {
                Console.WriteLine("Error: Rating must be between 1 and 5.");
            }
            else
            {
                reviews.Add(review);
            }
        }

        public List<ReviewKamar<T>> GetReviews()
        {
            return reviews;
        }
    }
}