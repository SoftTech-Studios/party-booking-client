using PartyBookingClient.Components.Pages;

namespace PartyBookingClient.Backend;

public class DatabaseStore
{
    private Dictionary<string, string> _userStore = new Dictionary<string, string>();
    private Dictionary<string, Signup.SignupModel> _profiles = new Dictionary<string, Signup.SignupModel>();
    private Dictionary<string, BookingManagement.Booking[]> _bookings = new Dictionary<string, BookingManagement.Booking[]>();
    
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
    public void Logout() => _currentlyLoggedInUser = "";
    public bool IsAdmin => _currentlyLoggedInUser == "admin@playland.com";
    public bool IsLoggedIn => !string.IsNullOrEmpty(_currentlyLoggedInUser);
    public string? Username => _profiles.GetValueOrDefault(_currentlyLoggedInUser)?.Name;

    public BookingManagement.Booking[] GetAllBookings()
    {
        var bookings = _bookings.Values.ToArray();

        var list = new List<BookingManagement.Booking>();

        foreach (var booking in bookings)
        {
            list.AddRange(booking);
        }

        return list.ToArray();
    }

    public BookingManagement.Booking[]? UserBookings => _bookings.GetValueOrDefault(_currentlyLoggedInUser);

    public void AddUser(string username, string password)
    {
        _userStore.Add(username, password);
    }

    public void AddBooking(BookingManagement.Booking booking)
    {
        var currentBookings = _bookings.GetValueOrDefault(_currentlyLoggedInUser);

        BookingManagement.Booking[] bookings;
        
        if (currentBookings is null)
        {
            bookings = new[] { booking };
        }
        else
        {
            var listBookings = currentBookings.ToList();
            
            listBookings.Add(booking);

            bookings = listBookings.ToArray();
        }

        _bookings[_currentlyLoggedInUser] = bookings;
    }

    public void AddProfile(Signup.SignupModel model)
    {
        _profiles[_currentlyLoggedInUser] = model;
    }
}