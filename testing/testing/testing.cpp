// testing.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <cassert>

#include "lifegame.h"
#include "sparselife.h"
#include "lifegrid.h"

#define _GRID_TEST 1
#define _FIXED_LEN_LIFE 0
#define _SPARSE_LEN_LIFE 0

void DoAssert(int value, int expected)
{
	assert(value == expected);
}

#include <iostream>
#define N 8
void makeNextFrame(char frame[N][N])
{
    int t = sizeof(frame);
    std::cout << "Frame size " << t;
    frame[0][0] = 2;
    frame[N-1][N - 1] = 8;
}

int main()
{
    char thisFrame[N][N];
    thisFrame[0][0] = 1;
    makeNextFrame(thisFrame);
    thisFrame[0][0] = 3;

#if _GRID_TEST
{
		lifegrid lgrid;
		bool test1 = lgrid.Run();
		bool test2 = lgrid.Run();
		bool test3 = lgrid.Run();
		DoAssert(test1 + test2 + test3, 3);
	}
#endif

#if _FIXED_LEN_LIFE
	{
		LifeGame lifeGame;
		bool test1 = lifeGame.Run();
		bool test2 = lifeGame.Run();
		bool test3 = lifeGame.Run();
		DoAssert(test1 + test2 + test3, 3);
	}
#endif

#if _SPARSE_LEN_LIFE
	{
		sparselife theSparselife;
		bool test0 = theSparselife.RunTurn();
		bool test1 = theSparselife.RunTurn();
		bool test2 = theSparselife.RunTurn();
		bool test3 = theSparselife.RunTurn();
		bool test4 = theSparselife.RunTurn();
		DoAssert(test0 + test1 + test2 + test3 + test4, 5);
	}
#endif

    return 0;
}

