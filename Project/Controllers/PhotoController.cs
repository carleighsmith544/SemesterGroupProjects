namespace SavorySweets.Project.Controllers
{
    public class PhotoController
    {
        //returns the full file path of the selected photo, or null if selection failed

        public async Task<string?> PickPhotoAsync()
        {
            try
            {
                //opens the file picker 
                var result = await FilePicker.PickAsync(new PickOptions
                {
                    PickerTitle = "Select a photo"
                });

                //if the user selected a file, return its full path
                if (result != null)
                {
                    return result.FullPath;
                }
            }
            catch (Exception ex)
            {
                //return null if no photo was selected or an error occurred
                Console.WriteLine($"Photo picking failed: {ex.Message}");
            }

            return null;
        }
    }
}
