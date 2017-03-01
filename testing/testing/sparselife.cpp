#include "stdafx.h"
#include "sparselife.h"

#include <iostream>
#include <chrono>
#include <cassert>
#include <unordered_set>
#include <string>
#include <thread>
#include <set>

#define _USE_HASH_SET 1

#if _USE_HASH_SET
struct GridKeyHash
{
	inline std::size_t operator()(const std::pair<uint16_t, uint16_t> & v) const
	{
		return static_cast<size_t>((v.first * 0x0F0F) ^ v.second);
	}
};
using GridKey = std::pair<uint16_t, uint16_t>;
using GridSet = std::unordered_set<GridKey, GridKeyHash>;
#define GRID_SET_RESERVE(grid, cnt) (grid.reserve(cnt))
#else
using GridKey = std::pair<uint16_t, uint16_t>;
using GridSet = std::set<GridKey>;
#define GRID_SET_RESERVE(grid, cnt) (void)grid;
#endif

struct GridTimer
{
	using TimeType = std::chrono::milliseconds;
	using ClockType = std::chrono::high_resolution_clock;

	GridTimer(std::string label)
	{
		m_label = std::move(label);
		m_then = ClockType::now();
	}

	~GridTimer()
	{
		auto dif = std::chrono::duration_cast<TimeType>(ClockType::now() - m_then);
		std::cout << m_label << " took " << dif.count() << " ?\n";
	}

	std::string m_label;
	std::chrono::time_point<ClockType> m_then;
};

struct Grid
{
	GridSet m_cellMap;
	int m_width;
	int m_height;

	Grid(int width, int height) 
		: m_width(width)
		, m_height(height) 
	{
	}

	~Grid()
	{
		GridSet().swap(m_cellMap);
	}

	GridKey MakeKeySafe(int x, int y) const
	{
		const int theX = (x + m_width) % m_width;
		const int theY = (y + m_height) % m_height;
		return std::make_pair(theX, theY);
	}

	GridKey MakeKey(int x, int y) const
	{
		return std::make_pair(x, y);
	}

	bool FindCell(int x, int y) const
	{
		int theX = (x + m_width) % m_width;
		int theY = (y + m_height) % m_height;
		const auto& it = m_cellMap.find(MakeKey(theX, theY));
		return it != m_cellMap.end();			
	}

	int NumNeighbors(int x, int y) const
	{
		int num = FindCell(x - 1, y - 1) ? 1 : 0;
		num += FindCell(x - 1, y + 0) ? 1 : 0;
		num += FindCell(x - 1, y + 1) ? 1 : 0;
		num += FindCell(x + 0, y - 1) ? 1 : 0;
		num += FindCell(x + 0, y + 1) ? 1 : 0;
		num += FindCell(x + 1, y - 1) ? 1 : 0;
		num += FindCell(x + 1, y + 0) ? 1 : 0;
		num += FindCell(x + 1, y + 1) ? 1 : 0;
		return num;
	}

	void SetCell(int x, int y)
	{
		m_cellMap.insert(MakeKey(x, y));
	}

	void PopulateWithArray(const int* values, size_t width, size_t height)
	{
		assert(width == m_width);
		assert(height == m_height);

		for (int x = 0; x < width; ++x)
		{
			for (int y = 0; y < height; ++y)
			{
				const int* value = values + ((height * y) + x);
				if (*value)
				{
					m_cellMap.insert(MakeKeySafe(x, y));
				}
			}
		}
	}

	void PopulateRandomlyUntilCount(int count)
	{
		GRID_SET_RESERVE(m_cellMap, count);
		for (int x = 0; x < m_width; ++x)
		{
			for (int y = 0; y < m_height; ++y)
			{
				if ((std::rand() % 16) == 0)
				{
					m_cellMap.insert(MakeKeySafe(x + 0, y + 0));
					m_cellMap.insert(MakeKeySafe(x + 1, y + 0));
					m_cellMap.insert(MakeKeySafe(x + 1, y + 1));
				}
				if ((std::rand() % 8) == 0)
				{
					m_cellMap.insert(MakeKeySafe(x, y));
				}
				if (m_cellMap.size() >= count)
				{
					return;
				}
			}
		}
	}

	Grid NextTurn()
	{
		GridSet targets;
		GRID_SET_RESERVE(targets, m_cellMap.size() * 9);

		for (GridKey key : m_cellMap)
		{
			int y = key.first;
			int x = key.second;

			targets.insert(MakeKeySafe(x - 1, y - 1));
			targets.insert(MakeKeySafe(x - 1, y + 0));
			targets.insert(MakeKeySafe(x - 1, y + 1));
			targets.insert(MakeKeySafe(x + 1, y - 1));
			targets.insert(MakeKeySafe(x + 1, y + 0));
			targets.insert(MakeKeySafe(x + 1, y + 1));
			targets.insert(MakeKeySafe(x + 0, y - 1));
			targets.insert(MakeKeySafe(x + 0, y + 0));
			targets.insert(MakeKeySafe(x + 0, y + 1));
		}

		Grid next(m_width, m_height);
		for (GridKey key : targets)
		{
			int y = key.first;
			int x = key.second;

			bool target = FindCell(x, y);
			int num = NumNeighbors(x, y);

			if (!target)
			{
				// Any dead cell with exactly three live neighbors becomes a live cell, as if by reproduction.
				if (num == 3)
				{
					next.SetCell(x, y);
				}
			}
			else if (num == 2 || num == 3)
			{
				// Any live cell with fewer than two live neighbors dies, as if caused by under population.
				// Any live cell with more than three live neighbors dies, as if by overpopulation.
				next.SetCell(x, y);
			}
		}
		return std::move(next);
	}

	static void PrintIt(Grid& grid)
	{
		// too big?
		if (grid.m_height > 80 || grid.m_width > 80)
		{
			return;
		}
		size_t col = 0;
		for (int i = 0; i < grid.m_width * grid.m_height; ++i)
		{
			if (col == grid.m_width)
			{
				printf_s("\n");
				col = 0;
			}

			int y = i / grid.m_width;
			int x = i % grid.m_width;

			if (grid.FindCell(x, y))
			{
				printf_s("*");
			}
			else
			{
				printf_s("'");
			}
			col++;
		}
		printf_s("\n\n");
	}
};

///////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////
#define _STABLE_POPULATION 1
#if _STABLE_POPULATION

sparselife::sparselife()
{
}

sparselife::~sparselife()
{
}

bool sparselife::RunTurn()
{
	const size_t bigWidth = 1024 * 31;
	const size_t bigHeight = 1024 * 31;
	Grid grid(bigWidth, bigHeight);

	{
		GridTimer bar("PopulateEveryOtherCol");
		grid.PopulateRandomlyUntilCount(1024 * 128);
	}
	{
		GridTimer foo("NextTurn");
		grid.NextTurn();
	}

	return true;
}

#else

// https://en.wikipedia.org/wiki/Conway's_Game_of_Life#/media/File:Game_of_life_toad.gif
static const size_t kWidth = 6;
static const size_t kHeight = 6;
static const int s_stillLife[kWidth * kHeight] =
{
	1, 0, 0, 0, 0, 1,
	1, 0, 0, 0, 0, 1,
	0, 0, 0, 0, 0, 0,
	0, 0, 0, 0, 0, 0,
	0, 0, 0, 0, 0, 0,
	0, 0, 0, 0, 0, 0,
};
static const int s_gridOne[kWidth * kHeight] =
{
	0, 0, 0, 0, 0, 0,
	0, 0, 0, 0, 0, 0,
	0, 0, 1, 1, 1, 0,
	0, 1, 1, 1, 0, 0,
	0, 0, 0, 0, 0, 0,
	0, 0, 0, 0, 0, 0,
};
static const int s_gridTwo[kWidth * kHeight] =
{
	0, 0, 0, 0, 0, 0,
	0, 0, 0, 1, 0, 0,
	0, 1, 0, 0, 1, 0,
	0, 1, 0, 0, 1, 0,
	0, 0, 1, 0, 0, 0,
	0, 0, 0, 0, 0, 0,
};

static const int k_grid0[] = {
	1,0,0,0,0,0,
	1,0,0,0,0,0,
	1,0,0,0,0,0,
	0,0,0,0,0,0,
	0,0,0,0,0,0,
	0,0,0,1,1,1
};

static const int k_grid1[] = {
	1,0,0,0,1,0,
	1,1,0,0,0,1,
	0,0,0,0,0,0,
	0,0,0,0,0,0,
	0,0,0,0,1,0,
	0,0,0,0,1,1
};

static const int k_grid2[] = {
	0,1,0,0,1,0,
	1,1,0,0,0,1,
	1,0,0,0,0,0,
	0,0,0,0,0,0,
	0,0,0,0,1,1,
	0,0,0,1,1,0
};

static const int k_grid3[] = {
	0,1,1,1,1,0,
	0,1,0,0,0,1,
	1,1,0,0,0,1,
	0,0,0,0,0,1,
	0,0,0,1,1,1,
	0,0,0,1,0,0
};

static int kThisGrid = 0;

sparselife::sparselife()
{
}

sparselife::~sparselife()
{
}

bool sparselife::RunTurn()
{
	Grid grid(kWidth, kHeight);
	const int* kNextGrid = nullptr;
	switch (kThisGrid)
	{
	case 0:
		grid.PopulateWithArray(s_stillLife + 0, kWidth, kHeight);
		kNextGrid = s_stillLife + 0;
		break;
	case 1:
		grid.PopulateWithArray(s_gridOne + 0, kWidth, kHeight);
		kNextGrid = s_gridTwo + 0;
		break;
	case 2:
		grid.PopulateWithArray(k_grid0 + 0, kWidth, kHeight);
		kNextGrid = k_grid1 + 0;
		break;
	case 3:
		grid.PopulateWithArray(k_grid1 + 0, kWidth, kHeight);
		kNextGrid = k_grid2 + 0;
		break;
	case 4:
		grid.PopulateWithArray(k_grid2 + 0, kWidth, kHeight);
		kNextGrid = k_grid3 + 0;
		break;
	default:
		kNextGrid = nullptr;
		assert(false);
		break;
	}
	kThisGrid++;

	Grid compareTo = std::move(grid.NextTurn());
	Grid::PrintIt(compareTo);
	if (kNextGrid)
	{
		Grid target(kWidth, kHeight);
		target.PopulateWithArray(kNextGrid, kWidth, kHeight);
		Grid::PrintIt(target);
		return target.m_cellMap == compareTo.m_cellMap;
	}
	return false;
}

#endif
