using Microsoft.Maui.Storage;
using SavorySweets.Project.Controllers;
using SavorySweets.Project.Models;

namespace SavorySweets.Project.Views;

public partial class SettingsPage : ContentPage
{
    private readonly UserController _userController;
    private User? _currentUser;
    private string _profileImagePath = "";

    public SettingsPage()
    {
        InitializeComponent();
        _userController = new UserController();
        LoadUserData();
    }

    private void LoadUserData()
    {
        _currentUser = _userController.GetLoggedInUser();

        if (_currentUser != null)
        {
            firstNameEntry.Text = _currentUser.FirstName;
            lastNameEntry.Text = _currentUser.LastName;
            emailEntry.Text = _currentUser.Email;
            darkModeSwitch.IsToggled = _currentUser.IsDarkMode;
            _profileImagePath = _currentUser.ProfileImagePath;

            profileImage.Source = string.IsNullOrEmpty(_profileImagePath)
                ? "default_profile.png"
                : _profileImagePath;
        }
        else
        {
            // No user is signed in — show default image and clear fields
            profileImage.Source = "default_profile.png";
            firstNameEntry.Text = "";
            lastNameEntry.Text = "";
            emailEntry.Text = "";
            darkModeSwitch.IsToggled = false;
        }
    }

    private async void OnChangePhotoClicked(object sender, EventArgs e)
    {
        try
        {
            var result = await FilePicker.Default.PickAsync(new PickOptions
            {
                PickerTitle = "Select a profile picture",
                FileTypes = FilePickerFileType.Images
            });

            if (result != null)
            {
                _profileImagePath = result.FullPath;
                profileImage.Source = ImageSource.FromFile(_profileImagePath);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Unable to load image: {ex.Message}", "OK");
        }
    }

    private async void OnSaveSettingsClicked(object sender, EventArgs e)
    {
        if (_currentUser == null)
            return;

        _currentUser.FirstName = firstNameEntry.Text;
        _currentUser.LastName = lastNameEntry.Text;
        _currentUser.Email = emailEntry.Text;
        _currentUser.IsDarkMode = darkModeSwitch.IsToggled;
        _currentUser.ProfileImagePath = _profileImagePath;

        App.Current.UserAppTheme = _currentUser.IsDarkMode ? AppTheme.Dark : AppTheme.Light;

        _userController.UpdateUserInfo(_currentUser);

        await DisplayAlert("Success", "Your settings have been saved!", "OK");
    }
}
