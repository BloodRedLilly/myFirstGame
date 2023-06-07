using Godot;
using System;

public partial class player : Area2D
{
	[Export]
	public int Speed {get; set;} = 400;
	
	public Vector2 ScreenSize;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ScreenSize = GetViewportRect().Size;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
{
	var velocity = Vector2.Zero; // The player's movement vector.

	if (Input.IsActionPressed("move_right"))
	{
		velocity.X += 1;
	}

	if (Input.IsActionPressed("move_left"))
	{
		velocity.X -= 1;
	}

	if (Input.IsActionPressed("move_down"))
	{
		velocity.Y += 1;
	}

	if (Input.IsActionPressed("move_up"))
	{
		velocity.Y -= 1;
	}

	var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

	if (velocity.Length() > 0)
	{
		velocity = velocity.Normalized() * Speed;
		animatedSprite2D.Play();
	}
	else
	{
		animatedSprite2D.Stop();
	}
	
	Position += velocity * (float)delta;

Position = new Vector2(
	x: Mathf.Clamp(Position.X, 0, ScreenSize.X),
	y: Mathf.Clamp(Position.Y, 0, ScreenSize.Y)
);

	var Size = Position.Y / 400;
		animatedSprite2D.Scale = new Vector2(Size, Size);
		
	if (velocity.X != 0)
{
	animatedSprite2D.Animation = "walk";
	animatedSprite2D.FlipV = false;
	// See the note below about boolean assignment.
	animatedSprite2D.FlipH = velocity.X < 0;
}
else if (velocity.Y != 0)
{
	animatedSprite2D.Animation = "up";
	animatedSprite2D.FlipV = velocity.Y > 0;
}
}
}
