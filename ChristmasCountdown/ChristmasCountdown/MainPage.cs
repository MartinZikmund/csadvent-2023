using Microsoft.UI.Text;
using Microsoft.UI.Xaml.Media.Imaging;

namespace ChristmasCountdown;

public sealed partial class MainPage : Page
{
    private DispatcherTimer _timer = new();
    private TextBlock _countdownText;

    public MainPage()
    {
        this
            .Background(
                new ImageBrush()
                    .ImageSource("ms-appx:///ChristmasCountdown/Assets/Background.png")
                    .Stretch(Stretch.UniformToFill))
            .Content(
                new Border()
                    .Background(new SolidColorBrush(Color.FromArgb(128, 0, 0, 0)))
                    .Child(
                        new StackPanel()
                            .Spacing(20)
                            .VerticalAlignment(VerticalAlignment.Center)
                            .HorizontalAlignment(HorizontalAlignment.Center)
                            .Children(
                                new TextBlock()
                                    .Text("Christmas Countdown")
                                    .FontSize(24)
                                    .FontWeight(FontWeights.Bold)
                                    .HorizontalAlignment(HorizontalAlignment.Center)
                                    .Foreground(Colors.White),
                                (_countdownText = new TextBlock()
                                    .FontSize(48)
                                    .FontWeight(FontWeights.Bold)
                                    .HorizontalAlignment(HorizontalAlignment.Center)
                                    .TextAlignment(Microsoft.UI.Xaml.TextAlignment.Center)
                                    .Foreground(Colors.White))
                            )));

        _timer.Interval = TimeSpan.FromSeconds(0.5);
        _timer.Tick += (s, e) => OnTick();
        _timer.Start();
        OnTick();
    }

    private void OnTick()
    {
        var now = DateTime.Now;
        var christmasDay = new DateTime(now.Year, 12, 25);
        var boxingDay = christmasDay.AddDays(1);

        if (christmasDay <= now && now < boxingDay)
        {
            _countdownText.Text = "Merry Christmas!";
        }
        else
        {
            int year = now >= boxingDay ? now.Year + 1 : now.Year;
            var timeUntilChristmas = new DateTime(year, 12, 25) - now;

            _countdownText.Text =
                $"{timeUntilChristmas.Days} days" + Environment.NewLine +
                $"{timeUntilChristmas.Hours} hours" + Environment.NewLine +
                $"{timeUntilChristmas.Minutes} minutes" + Environment.NewLine +
                $"{timeUntilChristmas.Seconds} seconds";
        }
    }
}
