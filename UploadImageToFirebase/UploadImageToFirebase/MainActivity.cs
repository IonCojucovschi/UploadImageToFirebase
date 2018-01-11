using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using System;
using Android.Content;
using Android.Runtime;
using Android.Graphics;
using Android.Provider;
using Java.IO;
using Firebase.Storage;

namespace UploadImageToFirebase
{
    [Activity(Label = "UploadImageToFirebase", MainLauncher = true,Theme ="@style/Theme.AppCompat.Light.NoActionBar")]
    public class MainActivity : AppCompatActivity
    {
        private Button buttonChose, buttonUpload;
        private ImageView imageView;

        private Android.Net.Uri fileNamePath;

        private const int PICK_IMAGE_REQUEST = 71;

        ProgressDialog progressDialog;

        FirebaseStorage storage;
        StorageReference storageReference;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);

            /// firebase init Storage


            //init views 
            buttonChose = FindViewById<Button>(Resource.Id.button_chose);
            buttonUpload=FindViewById<Button>(Resource.Id.button_upload);
            imageView = FindViewById<ImageView>(Resource.Id.imageView);

            ////   add events to button

            buttonChose.Click += delegate {
                ChoseImage();
            };

            buttonUpload.Click += delegate
            {
                UploadImageToFirebase();

            };

        }

        private void UploadImageToFirebase()
        {

        }

        private void ChoseImage()
        {
            Intent intent = new Intent();
            intent.SetType("image/*");
            intent.SetAction(Intent.ActionGetContent);
            StartActivityForResult(Intent.CreateChooser(intent,"Select Picture"),PICK_IMAGE_REQUEST);

        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (requestCode==PICK_IMAGE_REQUEST && resultCode==Result.Ok && data!=null && data.Data!=null)
            {
                fileNamePath = data.Data;
                try
                {
                    Bitmap bitmap = MediaStore.Images.Media.GetBitmap(ContentResolver,fileNamePath);
                    imageView.SetImageBitmap(bitmap);

                }
                catch (IOException ex)
                {
                    ex.PrintStackTrace();
                }

            }

        }

    }
}

