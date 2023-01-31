/// <summary>
///  This class is contain properties of song.
/// </summary>

namespace Karaoke_Proj
{
    public class Song
    {
        private string _songName;
        private string _songAuthor;
        private string _imgPath;
        private string _lyrics;
        private string _URL;

        public Song() { }
        public Song(string name, string author, string img_path, string _url, string _lyrics)
        {
            _songName = name;
            _songAuthor = author;
            _URL = _url;
            this._imgPath = img_path;
            this._lyrics = _lyrics;
        }

        public string SongName
        {
            get { return _songName; }
        }

        public string SongAuthor
        {
            get { return _songAuthor; }
        }

        public string Img
        {
            get { return _imgPath; }
        }

        public string URL
        {
            get { return _URL; }
        }

        public string Lyrics
        {
            get { return _lyrics; } 
        }
    }
}
