#pragma once
#pragma once

#include <array>

class LifeGame
{
public:
	LifeGame();
	~LifeGame();

	static const size_t k_Stride = 6;
	static const size_t k_Height = 6;

	bool Run();

	using GridArray = std::array<bool, k_Stride * k_Height>;

	bool m_onPageOne;
	GridArray m_gridOne;
	GridArray m_gridTwo;
};

