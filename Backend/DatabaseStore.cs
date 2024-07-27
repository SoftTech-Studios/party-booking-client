namespace PartyBookingClient.Backend;

public class DatabaseStore
{
    private Dictionary<string, string> _userStore = new Dictionary<string, string>();

    private string _currentlyLoggedInUser = string.Empty;
    
    public DatabaseStore()
    {
        _userStore.Add("admin@playland.com", "adminpassword");
    }

    public bool CheckDetails(string user, string password)
    {
        if (!_userStore.ContainsKey(user)) return false;

        if (_userStore[user] == password) return true;

        return false;
    }

    public void Update(string username, string password)
    {
        if (!_userStore.ContainsKey(username)) return;

        _userStore[username] = password;
    }

    public void Login(string username) => _currentlyLoggedInUser = username;
    public bool IsAdmin => _currentlyLoggedInUser == "admin@playland.com";
    public bool IsLoggedIn => !string.IsNullOrEmpty(_currentlyLoggedInUser);

    public void AddUser(string username, string password)
    {
        _userStore.Add(username, password);
    }
}