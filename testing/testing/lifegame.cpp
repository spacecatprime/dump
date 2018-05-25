#include "stdafx.h"
#include "lifegame.h"

/*
The universe of the Game of Life is an infinite two-dimensional orthogonal grid of square cells, each of which is in one of two possible states, 
alive or dead, or "populated" or "unpopulated" (the difference may seem minor, except when viewing it as an early model of human/urban behaviour 
simulation or how one views a blank space on a grid). Every cell interacts with its eight neighbours, which are the cells that are horizontally, 
vertically, or diagonally adjacent. At each step in time, the following transitions occur:

Any live cell with fewer than two live neighbours dies, as if caused by underpopulation.
Any live cell with two or three live neighbours lives on to the next generation.
Any live cell with more than three live neighbours dies, as if by overpopulation.
Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.

The initial pattern constitutes the seed of the system. The first generation is created by applying the above rules simultaneously to every cell in 
the seed—births and deaths occur simultaneously, and the discrete moment at which this happens is sometimes called a tick (in other words, each 
generation is a pure function of the preceding one). The rules continue to be applied repeatedly to create further generations.



The Game of Life is a two-dimensional orthogonal grid of square cells, each of which is in one of two possible states, alive or dead. 
Every cell interacts with its eight neighbors, which are the cells that are horizontally, vertically, or diagonally adjacent. 
At each step in time, the following transitions occur:
    Any live cell with fewer than two live neighbors dies, as if caused by underpopulation.
    Any live cell with two or three live neighbors lives on to the next generation.
    Any live cell with more than three live neighbors dies, as if by overpopulation.
    Any dead cell with exactly three live neighbors becomes a live cell, as if by reproduction.

*/

#include <conio.h>

#define _EARLY_OUT 0
#define _FIND_MODULUS 1
#define _DO_CHECK 1

#if _FIND_MODULUS

// D: 59
// R: 18.252 or 19.414

bool FindCell(LifeGame::GridArray& grid, int x, int y, int width, int height)
{
	int theX = (x + width) % width;
	int theY = (y + height) % height;
	return grid.at((theY * width) + theX);
}

#else

// D: 55
// R: 13.338 or 13.897

bool FindCell(LifeGame::GridArray& grid, int x, int y, int width, int height)
{
	int theX = x;
	if (x >= width)
	{
		theX = 0;
	}
	else if (x < 0)
	{
		theX = width - 1;
	}

	int theY = y;
	if (y >= height)
	{
		theY = 0;
	}
	else if (y < 0)
	{
		theY = height - 1;
	}
	return grid.at((theY * width) + theX);
}
#endif

#if _EARLY_OUT

int NumNeighbors(LifeGame::GridArray& grid, int x, int y, int width, int height)
{
	int num = FindCell(grid, x - 1, y - 1, width, height) ? 1 : 0;
	num += FindCell(grid, x - 1, y + 0, width, height) ? 1 : 0;
	num += FindCell(grid, x - 1, y + 1, width, height) ? 1 : 0;
	if (num > 3) return num;
	num += FindCell(grid, x + 0, y - 1, width, height) ? 1 : 0;
	if (num > 3) return num;
	num += FindCell(grid, x + 0, y + 1, width, height) ? 1 : 0;
	if (num > 3) return num;
	num += FindCell(grid, x + 1, y - 1, width, height) ? 1 : 0;
	if (num > 3) return num;
	num += FindCell(grid, x + 1, y + 0, width, height) ? 1 : 0;
	if (num > 3) return num;
	num += FindCell(grid, x + 1, y + 1, width, height) ? 1 : 0;
	return num;
}

#else

int NumNeighbors(LifeGame::GridArray& grid, int x, int y, int width, int height)
{
	int num = FindCell(grid, x - 1, y - 1, width, height) ? 1 : 0;
	num += FindCell(grid, x - 1, y + 0, width, height) ? 1 : 0;
	num += FindCell(grid, x - 1, y + 1, width, height) ? 1 : 0;
	num += FindCell(grid, x + 0, y - 1, width, height) ? 1 : 0;
	num += FindCell(grid, x + 0, y + 1, width, height) ? 1 : 0;
	num += FindCell(grid, x + 1, y - 1, width, height) ? 1 : 0;
	num += FindCell(grid, x + 1, y + 0, width, height) ? 1 : 0;
	num += FindCell(grid, x + 1, y + 1, width, height) ? 1 : 0;
	return num;
}

#endif

bool NextTurn(LifeGame::GridArray& grid, size_t stride, size_t total, LifeGame::GridArray& nextPage)
{
	size_t height = total / stride;
	for (size_t i = 0; i < total; ++i)
	{
		int y = i / stride;
		int x = i % stride;
		bool target = FindCell(grid, x, y, stride, height);
		int num = NumNeighbors(grid, x, y, stride, height);

		// assume status quo
		nextPage[i] = target;

		if (!target)
		{
			// Any dead cell with exactly three live neighbors becomes a live cell, as if by reproduction.
			if (num == 3)
			{
				nextPage[i] = true;
			}
		}
		else if (num < 2)
		{
			// Any live cell with fewer than two live neighbors dies, as if caused by under population.
			nextPage[i] = false;
		}
		else if (num > 3)
		{
			// Any live cell with more than three live neighbors dies, as if by overpopulation.
			nextPage[i] = false;
		}
	}
	return true;
}

// https://en.wikipedia.org/wiki/Conway's_Game_of_Life#/media/File:Game_of_life_toad.gif
static const LifeGame::GridArray s_gridOne =
{
	0, 0, 0, 0, 0, 0,
	0, 0, 0, 0, 0, 0,
	0, 0, 1, 1, 1, 0,
	0, 1, 1, 1, 0, 0,
	0, 0, 0, 0, 0, 0,
	0, 0, 0, 0, 0, 0,
};
static const LifeGame::GridArray s_gridTwo =
{
	0, 0, 0, 0, 0, 0,
	0, 0, 0, 1, 0, 0,
	0, 1, 0, 0, 1, 0,
	0, 1, 0, 0, 1, 0,
	0, 0, 1, 0, 0, 0,
	0, 0, 0, 0, 0, 0,
};

static const LifeGame::GridArray k_grid0 = 
{
	1,0,0,0,0,0,
	1,0,0,0,0,0,
	1,0,0,0,0,0,
	0,0,0,0,0,0,
	0,0,0,0,0,0,
	0,0,0,1,1,1
};

static const LifeGame::GridArray k_grid1 =
{
	1,0,0,0,1,0,
	1,1,0,0,0,1,
	0,0,0,0,0,0,
	0,0,0,0,0,0,
	0,0,0,0,1,0,
	0,0,0,0,1,1
};

static const LifeGame::GridArray k_grid2 = {

	0,1,0,0,1,0,
	1,1,0,0,0,1,
	1,0,0,0,0,0,
	0,0,0,0,0,0,
	0,0,0,0,1,1,
	0,0,0,1,1,0
};

static const LifeGame::GridArray k_grid3 = {
	0,1,1,1,1,0,
	0,1,0,0,0,1,
	1,1,0,0,0,1,
	0,0,0,0,0,1,
	0,0,0,1,1,1,
	0,0,0,1,0,0
};

LifeGame::LifeGame()
{
	m_gridOne = new GridArray();
	m_gridTwo = new GridArray();

#if 1
	*m_gridOne = k_grid0;
	*m_gridTwo = k_grid1;
	m_onPageOne = true;
#else
	*m_gridOne = s_gridOne;
	*m_gridTwo = s_gridTwo;
	m_onPageOne = false;
#endif
}

LifeGame::~LifeGame()
{
	delete m_gridTwo;
	m_gridTwo = nullptr;
	delete m_gridOne;
	m_gridOne = nullptr;
}

bool LifeGame::Run()
{
	GridArray* thisPage = (m_onPageOne) ? m_gridTwo : m_gridOne;
	GridArray* nextPage = (m_onPageOne) ? m_gridOne : m_gridTwo;
	NextTurn(*thisPage, LifeGame::k_Stride, thisPage->max_size(), *nextPage);

#if _DO_CHECK
	if (m_onPageOne)
	{
		m_onPageOne ^= true;
		return *m_gridTwo == s_gridTwo;
	}
	else
	{
		m_onPageOne ^= true;
		return *m_gridOne == s_gridOne;
	}
#else
	m_onPageOne ^= true;
#endif

	return false;
}
