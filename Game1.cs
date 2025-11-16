using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace fistMonogame;

public class Game1 : Game
{
    // idle knight
    Texture2D playerIdleTexture;
    Rectangle playerIdleDestRectangle;
    Rectangle playerIdleSourceRectangle;

    // knight roll
    Texture2D playerRollTexture;
    Rectangle playerRollDestRectangle;
    Rectangle playerRollSourceRectangle;

    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    // var para a idle animation
    float elapsed;
    float delay = 180f;
    int spriteWidth = 100;
    int spriteHeight = 64;
    int idleFrames = 0;
    int rollFrames = 0;


    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        playerIdleDestRectangle = new Rectangle(100, 100, 100 , 64);
        playerRollDestRectangle = new Rectangle(200, 200, 100, 64);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        // TODO: use this.Content to load your game content here
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        playerIdleTexture = Content.Load<Texture2D>("player/Idle_KG_1");
        playerRollTexture = Content.Load<Texture2D>("player/Rolling_KG_1");

    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

        if (elapsed >= delay)
        {
            if (idleFrames >= 3 || rollFrames >= 9)
            {
                idleFrames = 0;
                rollFrames = 0;
            }
            else
            {
                idleFrames++;
                rollFrames++;
            }
            elapsed = 0;
        }

        playerIdleSourceRectangle = new Rectangle(playerIdleTexture.Width / 4 * idleFrames, 0, spriteWidth, spriteHeight);
        playerRollSourceRectangle = new Rectangle(playerRollTexture.Width / 10 * rollFrames, 0, spriteWidth, spriteHeight);
        // por algum motivo não funciona como platerIdleTexture.Width ou Height nos dois ultimos parametros, apenas valores hard coded
        // provavelmente pelo modo em que estao sendo carregadas no LoadContent (?)
        // encontrar uma maneira melhor? (talvez haja)
        
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();

        _spriteBatch.Draw(playerIdleTexture, playerIdleDestRectangle, playerIdleSourceRectangle, Color.White);
        _spriteBatch.Draw(playerRollTexture, playerRollDestRectangle, playerRollSourceRectangle, Color.White);

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
