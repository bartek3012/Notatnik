//using JacekMatulewski.WpfUtils;
using Microsoft.Win32;
//using System.Collections;
//using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Input; //obsługa skrótów klawiszowych
using JacekMatulewski.WpfUtils;
using System.Resources;
using System.Windows.Documents;

namespace Notatnik
{
    public partial class MainWindow : Window
    {
        private bool czy_tekst_zmieniony;
        private OpenFileDialog openFileDialog;
        private SaveFileDialog saveFileDialog;
        private string sciezkaPliku = null;
        public MainWindow()
        {
            //System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
            InitializeComponent();
            openFileDialog = new OpenFileDialog();
            openFileDialog.Title = Properties.Resources.WybierzPlikTekstowy;
            openFileDialog.Filter = Properties.Resources.FiltryOkienDialogowych;
            openFileDialog.DefaultExt = "txt";
            openFileDialog.FilterIndex = 1;

            saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = openFileDialog.Filter;
            saveFileDialog.Title = Properties.Resources.WybierzPlikDoZapisu;
            saveFileDialog.DefaultExt = "txt";
            saveFileDialog.FilterIndex = 1;
            czy_tekst_zmieniony = false;

  
        }

        private void MenuItem_Otworz_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(sciezkaPliku)) //nie wiem po co jest ten if
            {
                openFileDialog.InitialDirectory = Path.GetDirectoryName(sciezkaPliku);
                openFileDialog.FileName = Path.GetFileName(sciezkaPliku);
            }
            bool? wynik = openFileDialog.ShowDialog();
            if (wynik.HasValue && wynik.Value)
            {
                sciezkaPliku = openFileDialog.FileName;
                //string x = File.ReadAllText(sciezkaPliku).ToString();
                //textBox.Text = "Elo ziomus\n"+x;
                textBox.Text = File.ReadAllText(sciezkaPliku);
                statusBarText.Text = Path.GetFileName(sciezkaPliku);
                czy_tekst_zmieniony = false;
            }

        }

        private void MenuItem_Zapisz_jako_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(sciezkaPliku)) //nie wiem po co jest ten if
            {
                saveFileDialog.InitialDirectory = Path.GetDirectoryName(sciezkaPliku);
                saveFileDialog.FileName = Path.GetFileName(sciezkaPliku);
            }
            bool? wynik = saveFileDialog.ShowDialog();
            if (wynik.HasValue && wynik.Value)
            {
                sciezkaPliku = saveFileDialog.FileName;
                File.WriteAllText(sciezkaPliku, textBox.Text);
                statusBarText.Text = Path.GetFileName(sciezkaPliku);
                czy_tekst_zmieniony = false;
            }
        }

        private void MenuItem_Zapisz_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(sciezkaPliku))
            {
                File.WriteAllText(sciezkaPliku, textBox.Text);
                czy_tekst_zmieniony = false;
            }
            else
            {
                MenuItem_Zapisz_jako_Click(sender, e);
            }
        }

        private void MenuItem_Zamknij_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void textBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            czy_tekst_zmieniony = true;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            bool anuluj;
            zapytajOZapisanieTekstuDoPliku(sender, out anuluj);
            e.Cancel = anuluj;
        }

        private void zapytajOZapisanieTekstuDoPliku(object sender, out bool anuluj)
        {
            anuluj = false;
            if (czy_tekst_zmieniony == true)
            {
                MessageBoxResult decyzja =
                 MessageBox.Show("Czy zapisać zmiany w edytowanym dokumencie?", this.Title, MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

                switch (decyzja)
                {
                    case MessageBoxResult.Yes:
                        {
                            MenuItem_Zapisz_Click(sender, null);
                            break;
                        }
                    case MessageBoxResult.No:
                        {
                            break;
                        }
                    case MessageBoxResult.Cancel:
                    default:
                        {
                            anuluj = true; ;
                            break;
                        }
                }
            }
        }

        private void MenuItem_Nowy_Click(object sender, RoutedEventArgs e)
        {
            bool anuluj;
            zapytajOZapisanieTekstuDoPliku(sender, out anuluj);
            if (anuluj == false)
            {
                textBox.Clear();
            }
            textBox.Background = Brushes.White;
            textBox.Foreground = Brushes.Black;
        }

        private void MenuItem_Cofnij_Click(object sender, RoutedEventArgs e)
        {
            textBox.Undo();
        }

        private void MenuItem_Wytnij_Click(object sender, RoutedEventArgs e)
        {
            textBox.Cut();
        }

        private void MenuItem_Kopiuj_Click(object sender, RoutedEventArgs e)
        {
            textBox.Copy();
        }

        private void MenuItem_Wklej_Click(object sender, RoutedEventArgs e)
        {
            textBox.Paste();
        }

        private void MenuItem_Usun_Click(object sender, RoutedEventArgs e)
        {
            textBox.SelectedText = "";
        }

        private void MenuItem_ZaznaczWszytsko_Click(object sender, RoutedEventArgs e)
        {
            textBox.SelectAll();
        }

        private void MenuItem_DotaGodzina_Click(object sender, RoutedEventArgs e)
        {
            textBox.SelectedText = System.DateTime.Now.ToString();
        }

        private void MenuItem_Powtorz_Click(object sender, RoutedEventArgs e)
        {
            textBox.Redo();
        }

        private void MenuItem_ZawijajWiersze_Click(object sender, RoutedEventArgs e)
        {
            bool czyZaznaczone = ZawijajWiersze.IsChecked;
            if (czyZaznaczone == true)
            {
                textBox.TextWrapping = TextWrapping.Wrap;
            }
            else
            {
                textBox.TextWrapping = TextWrapping.NoWrap;
            }
        }
        private void MenuItem_PasekNarzedzi_Click(object sender, RoutedEventArgs e)
        {
            bool czyZaznaczone = ((System.Windows.Controls.MenuItem)sender).IsChecked;
            if (czyZaznaczone == true)
            {
                toolBar.Visibility = Visibility.Visible;
            }
            else
            {
                toolBar.Visibility = Visibility.Collapsed;
            }
        }
        private void MenuItem_PasekStanu_Click(object sender, RoutedEventArgs e)
        {
            bool czyZaznaczone = (sender as MenuItem).IsChecked;

            statusBar.Visibility = czyZaznaczone ? Visibility.Visible : Visibility.Collapsed;

        }

        private void MenuItem_Drukuj_Click(object sender, RoutedEventArgs e)
        {
            Printing.PrintText(textBox.Text);
            //string text = textBox.Text;

        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            switch(e.Key)
            {
                case Key.F5:
                    {
                        MenuItem_DotaGodzina_Click(sender, null);
                        break;
                    }
            }
            if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                switch (e.Key)
                {
                    case Key.N:
                        {
                            MenuItem_Nowy_Click(sender, null);
                            break;
                        }
                    case Key.O:
                        {
                            MenuItem_Otworz_Click(sender, null);
                            break;
                        }
                    case Key.S:
                        {
                            MenuItem_Zapisz_Click(sender, null);
                            break;
                        }
                    case Key.P:
                        {
                            MenuItem_Drukuj_Click(sender, null);
                            break;
                        }
                }
            }
        }

        private void MenuItem_KolorTlaClick(object sender, RoutedEventArgs e)
        {
            string name = ((MenuItem)sender).Header.ToString();

            switch (name)
            {
                case "Zielony":
                    {
                        textBox.Background = Brushes.Green;
                        textBox.Foreground = Brushes.White;
                        break;
                    }
                case "Zółty":
                    {
                        textBox.Background = Brushes.Yellow;
                        textBox.Foreground = Brushes.Black;
                        break;
                    }
                case "Niebieski":
                    {
                        textBox.Background = Brushes.Blue;
                        textBox.Foreground = Brushes.White;
                        break;
                    }
                case "Czerwony":
                    {
                        textBox.Background = Brushes.Red;
                        textBox.Foreground = Brushes.White;
                        break;
                    }
                case "Czarny":
                    {
                        textBox.Background = Brushes.Black;
                        textBox.Foreground = Brushes.White;
                        break;
                    }
                case "Fioletowy":
                    {
                        textBox.Background = Brushes.Purple;
                        textBox.Foreground = Brushes.White;
                        break;
                    }
                case "Pomarańczowy":
                    {
                        textBox.Background = Brushes.Orange;
                        textBox.Foreground = Brushes.White;
                        break;
                    }
                case "Szary":
                    {
                        textBox.Background = Brushes.LightGray;
                        textBox.Foreground = Brushes.Black;
                        break;
                    }
                case "Biały":
                default:
                    {
                        textBox.Background = Brushes.White;
                        textBox.Foreground = Brushes.Black;
                        break;
                    }
            }

        }
    }
}
