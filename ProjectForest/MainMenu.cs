using SFML.Graphics;

using Latte.Core.Type;
using Latte.UI;
using Latte.UI.Elements;
using Latte.Application;

using ProjectForest.UI;


namespace ProjectForest;




public sealed class MainMenu : Section
{
    public MainMenuButtonWidget PlayButton { get; }
    public MainMenuButtonWidget SettingsButton { get; }
    public MainMenuButtonWidget CreditsButton { get; }
    public MainMenuButtonWidget ExitButton { get; }

    public TextElement VersionText { get; }





    public MainMenu()
    {
        PlayButton = new MainMenuButtonWidget(null, null, "Play") { AlignmentMargin = new Vec2f(0, 60) };
        SettingsButton = new MainMenuButtonWidget(null, null, "Settings") { AlignmentMargin = new Vec2f(0, 123) };
        CreditsButton = new MainMenuButtonWidget(null, null, "Credits") { AlignmentMargin = new Vec2f(0, 180) };
        ExitButton = new MainMenuButtonWidget(null, null, "Exit") { AlignmentMargin = new Vec2f(0, 243) };

        PlayButton.MouseClickEvent += OnPlayButtonClick;
        SettingsButton.MouseClickEvent += OnSettingsButtonClick;
        CreditsButton.MouseClickEvent += OnCreditsButtonClick;
        ExitButton.MouseClickEvent += OnExitButtonClick;


        VersionText = new TextElement(null, null, 12, "dev")
        {
            Alignment = Alignment.BottomRight,
            AlignmentMargin = new Vec2f(-5, -5)
        };


        AddElements(PlayButton, SettingsButton, CreditsButton, ExitButton, VersionText);
    }




    private void OnPlayButtonClick(object? _, EventArgs __)
    {

    }


    private void OnSettingsButtonClick(object? _, EventArgs __)
    {

    }


    private void OnCreditsButtonClick(object? _, EventArgs __)
    {

    }


    private void OnExitButtonClick(object? _, EventArgs __)
    {
        App.Quit();
    }
}
