// testing.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"

#include "lifegame.h"

int main()
{
	{
		LifeGame lifeGame;
		bool test1 = lifeGame.Run();
		bool test2 = lifeGame.Run();
		bool test3 = lifeGame.Run();
		lifeGame.~LifeGame();
	}

    return 0;
}

