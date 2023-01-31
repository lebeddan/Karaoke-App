/// <summary>
///  This class is contain properties of session.
/// </summary>

namespace Karaoke_Proj.Models
{
    public class Session
    {
        private string _name;

        public Session(string name)
        {
            _name = name;
        }

        public string Name 
        {
            get { return _name; }
            set { _name = value; }
        }
    }
}
