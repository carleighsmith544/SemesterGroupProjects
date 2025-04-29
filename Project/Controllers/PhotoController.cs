using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;


namespace SavorySweets.Project.Controllers
{
    public class PhotoController
    {
        public async Task<string?> PickPhotoAsync()
        {
            try
            {
                var result = await FilePicker.PickAsync(new PickOptions
                {
                    PickerTitle = "Select a photo"
                });

                if (result != null)
                {
                    return result.FullPath;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Photo picking failed: {ex.Message}");
            }

            return null;
        }
    }
}
