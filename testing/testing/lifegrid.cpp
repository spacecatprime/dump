#include "stdafx.h"
#include "lifegrid.h"

#include <conio.h>

namespace LifeGrid
{
	bool FindCell(lifegrid::GridArray& grid, int x, int y, int width, int height)
	{
		int theX = (x + width) % width;
		int theY = (y + height) % height;
		return grid.at((theY * width) + theX);
	}

	int NumNeighbors(lifegrid::GridArray& grid, int x, int y, int width, int height)
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

    lifegrid::GridArray NextTurn(lifegrid::GridArray& grid, size_t stride, size_t total)
    {
        lifegrid::GridArray nextPage;
        size_t height = total / stride;
        for (int i = 0; i < total; ++i)
        {
            int y = static_cast<int>(i / stride);
            int x = i % stride;
            int num = NumNeighbors(grid, x, y, static_cast<int>(stride), static_cast<int>(height));

            if (num == 3)
            {
                // Any dead cell with exactly three live neighbors becomes a live cell, as if by reproduction.
                nextPage[i] = true;
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
            else
            {
                // Any live cell with two or three live neighbors lives on to the next generation.
                nextPage[i] = FindCell(grid, x, y, static_cast<int>(stride), static_cast<int>(height));
            }
        }
        return std::move(nextPage);
    }

    void PrintArray(const lifegrid::GridArray& grid, size_t stride)
	{
		size_t col = 0;
		for (const auto it : grid)
		{
			if (col == stride)
			{
				printf_s("\n");
				col = 0;
			}
			if (it == 0)
			{
				printf_s("`");
			}
			else
			{
				printf_s("*");
			}
			col++;
		}
		printf_s("\n\n");
	}

	static const lifegrid::GridArray stillLife =
	{
		1, 0, 0, 0, 0, 1,
		1, 0, 0, 0, 0, 1,
		0, 0, 0, 0, 0, 0,
		0, 0, 0, 0, 0, 0,
		0, 0, 0, 0, 0, 0,
		0, 0, 0, 0, 0, 0,
	};

	static const lifegrid::GridArray k_grid0 =
	{
		1,0,0,0,0,0,
		1,0,0,0,0,0,
		1,0,0,0,0,0,
		0,0,0,0,0,0,
		0,0,0,0,0,0,
		0,0,0,1,1,1
	};

	static const lifegrid::GridArray k_grid1 =
	{
		1,0,0,0,1,0,
		1,1,0,0,0,1,
		0,0,0,0,0,0,
		0,0,0,0,0,0,
		0,0,0,0,1,0,
		0,0,0,0,1,1
	};

	static const lifegrid::GridArray k_grid2 = {

		0,1,0,0,1,0,
		1,1,0,0,0,1,
		1,0,0,0,0,0,
		0,0,0,0,0,0,
		0,0,0,0,1,1,
		0,0,0,1,1,0
	};

	static const lifegrid::GridArray k_grid3 = {
		0,1,1,1,1,0,
		0,1,0,0,0,1,
		1,1,0,0,0,1,
		0,0,0,0,0,1,
		0,0,0,1,1,1,
		0,0,0,1,0,0
	};

	static const lifegrid::GridArray* Grids[] = { &stillLife, &k_grid0, &k_grid1, &k_grid2, &k_grid3 };

	static int s_thisGrid = 0;
	static const int k_maxGrid = 3;
}


lifegrid::lifegrid()
{
}


lifegrid::~lifegrid()
{
}

bool lifegrid::Run()
{
	if (LifeGrid::s_thisGrid >= LifeGrid::k_maxGrid)
	{
		return false;
	}

	GridArray thisArray = *(LifeGrid::Grids[LifeGrid::s_thisGrid]);
	GridArray result = LifeGrid::NextTurn(thisArray, k_Stride, thisArray.size());
	const GridArray* target = LifeGrid::Grids[LifeGrid::s_thisGrid + 1];
	LifeGrid::PrintArray(result, k_Stride);

	// is still life?
	if (LifeGrid::s_thisGrid == 0)
	{
		target = LifeGrid::Grids[LifeGrid::s_thisGrid];
	}
	LifeGrid::PrintArray(*target, k_Stride);

	LifeGrid::s_thisGrid++;
	return *target == result;
}
