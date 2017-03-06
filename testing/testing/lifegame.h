#pragma once

#include <array>

class LifeGame
{
public:
	LifeGame();
	~LifeGame();

#if 1
	static const size_t k_Stride = 6;
	static const size_t k_Height = 6;
#else
	static const size_t k_Stride = 1024 * 12;
	static const size_t k_Height = 1024 * 12;
#endif

	bool Run();

	using GridArray = std::array<bool, k_Stride * k_Height>;

	bool m_onPageOne;
	GridArray* m_gridOne;
	GridArray* m_gridTwo;
};

