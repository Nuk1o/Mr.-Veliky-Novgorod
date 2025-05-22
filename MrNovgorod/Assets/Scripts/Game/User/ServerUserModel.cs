namespace Game.User
{
    [System.Serializable]
    public class ServerUserModel
    {
        public int id;
        public string name;
        public string email;
        public bool email_verified_at;
        public string avatar;
        public string phone;
        public ServerReview[] reviews; 
    }
    
    [System.Serializable]
    public class ServerReview
    {
        public int id;
        public string comment;
        public int rating;
        public ServerAttraction attraction;
    }
    
    [System.Serializable]
    public class ServerAttraction
    {
        public int id;
        public string name;
        public string image;
    }
}