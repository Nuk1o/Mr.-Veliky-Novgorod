using System.Collections.Generic;

namespace Game.User
{
    public class UserModel
    {
        public int id;
        public string name;
        public string email;
        public bool email_verified_at;
        public string avatar;
        public string phone;
        public List<Review> reviews; 
    }

    public class Review
    {
        public int id;
        public string comment;
        public int rating;
        public Attraction attraction;
    }

    public class Attraction
    {
        public int id;
        public string name;
        public string image;
    }
}