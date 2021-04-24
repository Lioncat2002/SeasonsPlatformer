# A monogame platformer using Monogame Extended and TiledSharp
I just made this code public coz I was just too lazy to make a video.
Also I am getting busier by day so maybe there won't be any video for a while.
The code is messy coz I wrote it in a hurry
The main part is the collision check in the update method which enables the platform movement
```cs
 var initpos = player.pos;
            var Ypos = player.pos.Y;
            player.Update(gameTime);
            foreach (var rect in collisionObjects)
            {
                if (rect.Intersects(new Rectangle(player.playerBounds.X+2,player.playerBounds.Y-1,4,4)))//This one checks for x axis collision
                {
                    player.pos.X = initpos.X;
                    player.isIdle = true;
                    player.isFalling = false;
                }
                
                if(rect.Contains(new Point(player.playerBounds.X+4,player.playerBounds.Y+9)))//The y axis collision
                {
                    
                    player.isFalling = false;
                    player.pos.Y = initpos.Y;
                }
                else
                {
                    player.isFalling = true;
                }
            }
            ```
You can check out the jump part very easily after this ;)
