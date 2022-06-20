using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace OSUpdate
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                SqlConnection connection = new SqlConnection("Data Source=192.168.1.106,1433;Network Library=DBMSSOCN; Initial Catalog=OSBİLİSİM;User Id=Admin; Password=1; MultipleActiveResultSets=True;");
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                SqlCommand üründurum = new SqlCommand("select version,updateversion,versiyon_aciklama,updateprogramlink from version", connection);
                WebClient client = new WebClient();
                string güncelversiyon = "";
                Console.WriteLine("OS BİLİŞİM OTAMATİK GÜNCELLEME SİSTEMİ");
                Console.WriteLine("**************************************");
                if (File.Exists("OSBilişim.exe"))
                {
                    FileVersionInfo myFileVersionInfo = FileVersionInfo.GetVersionInfo(@"OSBilişim.exe");
                    Console.WriteLine("Uygulama: " + myFileVersionInfo.FileDescription + '\n' + "Version numarası: " + myFileVersionInfo.FileVersion);
                    Console.WriteLine();
                    FileVersionInfo updateprogramversion = FileVersionInfo.GetVersionInfo(@"OSUpdate.exe");
                    Console.WriteLine("Uygulama: " + updateprogramversion.FileDescription + '\n' + "Version numarası: " + updateprogramversion.FileVersion);
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("Görünüşe göre OSBilişim uygulamanız mevcut değil.\nOSBilişim uygulaması indiriliyor lütfen bekleyiniz.");
                    File.WriteAllBytes(@"OSBilişim.exe", new WebClient().DownloadData("http://192.168.1.106/Update/OSBilişim.exe"));
                    System.Threading.Thread.Sleep(2000);
                    Console.Clear();
                    FileVersionInfo myFileVersionInfo = FileVersionInfo.GetVersionInfo(@"OSBilişim.exe");
                    Console.WriteLine("Uygulama: " + myFileVersionInfo.FileDescription + '\n' + "Version numarası: " + myFileVersionInfo.FileVersion);
                    Console.WriteLine();
                    FileVersionInfo updateprogramversion = FileVersionInfo.GetVersionInfo(@"OSUpdate.exe");
                    Console.WriteLine("Uygulama: " + updateprogramversion.FileDescription + '\n' + "Version numarası: " + updateprogramversion.FileVersion);
                    Console.WriteLine();
                }
                SqlDataReader uygulamasürümsorgulama;
                uygulamasürümsorgulama = üründurum.ExecuteReader();                                                                                                                                                                                                                                                                                     
                if (uygulamasürümsorgulama.Read())
                {
                   string updatesürüm = ((string)uygulamasürümsorgulama["updateversion"]);
                    FileVersionInfo updateprogramversion = FileVersionInfo.GetVersionInfo(@"OSUpdate.exe");
                    if (Convert.ToInt16(updatesürüm) >= Convert.ToInt16(updateprogramversion.FileVersion))
                    {
                        Console.WriteLine("Güncelleme programı güncellemeniz gerekmektedir.\nEn son sürümü kullanmak için lütfen indirilen bağlantıyı bulunduğunuz klasöre kopyalayınız.");
                        Console.WriteLine("İndirdiğiniz uygulamanın adını OSUpdate olarak ayarlayınız, uygulama isminin sonuda 1-2-3 gibi tanımlar olmamalıdır.");
                        Process.Start("http://192.168.1.106/Update/OSUpdate.exe");
                        System.Threading.Thread.Sleep(1000);
                        Console.WriteLine("Uygulamayı kapatmak için bir tuşa basın...");
                        Console.ReadKey();
                    }
                    else
                    {
                        güncelversiyon = ((string)uygulamasürümsorgulama["version"]);
                        FileVersionInfo myFileVersionInfo = FileVersionInfo.GetVersionInfo(@"OSBilişim.exe");
                        if (Convert.ToInt16(güncelversiyon) >= Convert.ToInt16(myFileVersionInfo.FileVersion))
                        {
                            tekrartusla:
                            Console.Write("Uygulamayı güncellemek için 'e' güncellememek için 'h' tuşuna basınız: ");
                            string cevap = Console.ReadLine();
                            
                            if (cevap == "Evet" || cevap == "evet" || cevap == "e" || cevap == "EVET" || cevap == "E")
                            {
                                Console.WriteLine();
                                Console.WriteLine("Versiyon açıklaması:");
                                Console.WriteLine();
                                Console.WriteLine(((string)uygulamasürümsorgulama["versiyon_aciklama"]));
                                Console.WriteLine();
                                connection.Close();
                                System.Threading.Thread.Sleep(8000);
                                try
                                {
                                    File.WriteAllBytes(@"OSBilişim.exe", new WebClient().DownloadData("http://192.168.1.106/Update/OSBilişim.exe"));
                                    Console.WriteLine("Uygulamanız başarılı şekilde güncellenmiştir. Program otamatik olarak başlatılacaktır lütfen bekleyiniz.");
                                    Process.Start("OSBilişim.exe");
                                    System.Threading.Thread.Sleep(1000);
                                    Console.WriteLine("Uygulamayı kapatmak için bir tuşa basın...");
                                    Console.ReadKey();
                                }
                                catch (Exception hata)
                                {
                                    Console.WriteLine("Uygulama güncellenirken bir hata oluştu lütfen tekrar deneyiniz.\nHata kodu: " + hata.Message);
                                    Console.WriteLine("Uygulamayı kapatmak için bir tuşa basın...");
                                    Console.ReadKey();
                                }
                            }
                            else if (cevap == "h" || cevap == "hayır" || cevap == "HAYIR" || cevap == "Hayır" || cevap == "H")
                            {
                                Console.WriteLine("Uygulama isteğiniz üzere güncellenmeyecektir, uygulamanın eski sürümü kullanılmamaktadır. Uygulamayı mecburen güncellemeniz gerekmektedir.");
                                Console.WriteLine("Uygulamayı kapatmak için bir tuşa basın...");
                                Console.ReadKey();
                            }
                            else
                            {
                                Console.WriteLine("Böyle bir tuş ataması mevcut değildir.");
                                goto tekrartusla;
                            }
                        }
                        else
                        {
                            Console.WriteLine("OSBİLİŞİM, OSUPDATE uygulamarı güncel sürüme sahip. Güncelleme geldiğinde görüşmek üzere.");
                            Console.WriteLine("Uygulamayı kapatmak için bir tuşa basın...");
                            Console.ReadKey();
                        }
                    }
                }
            }
            catch (Exception hata)
            {
                Console.WriteLine("Uygulama güncellenirken bir hata oluştu lütfen tekrar deneyiniz.\nHata kodu: " + hata.Message);
                Console.WriteLine("Uygulamayı kapatmak için bir tuşa basın...");
                Console.ReadKey();
            }
        }
    }
}
