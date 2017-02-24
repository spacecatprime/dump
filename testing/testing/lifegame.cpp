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
*/

// TODO a sparse grid cell impl
// TODO find a simpler cell accessor modulus formula

#include <conio.h>

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
			// Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.
			if (num == 3)
			{
				nextPage[i] = true;
			}
		}
		else if (num < 2)
		{
			// Any live cell with fewer than two live neighbours dies, as if caused by underpopulation.
			nextPage[i] = false;
		}
		else if (num > 3)
		{
			// Any live cell with more than three live neighbours dies, as if by overpopulation.
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

LifeGame::LifeGame()
{
	m_gridOne = s_gridOne;
	m_gridTwo = s_gridTwo;
	m_onPageOne = false;
}

LifeGame::~LifeGame()
{
}

bool LifeGame::Run()
{
	GridArray& thisPage = (m_onPageOne) ? m_gridTwo : m_gridOne;
	GridArray& nextPage = (m_onPageOne) ? m_gridOne : m_gridTwo;
	NextTurn(thisPage, LifeGame::k_Stride, thisPage.max_size(), nextPage);

	m_onPageOne ^= true;

	if (thisPage == m_gridOne)
	{
		return m_gridTwo == s_gridTwo;
	}
	else if (thisPage == m_gridTwo)
	{
		return m_gridOne == s_gridOne;
	}

	return false;
}
