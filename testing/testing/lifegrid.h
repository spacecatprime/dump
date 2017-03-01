#pragma once

#include <array>

class lifegrid
{
public:
	lifegrid();
	~lifegrid();

	bool Run();

#if 1
	static const size_t k_Stride = 6;
	static const size_t k_Height = 6;
#else
	static const size_t k_Stride = 1024 * 12;
	static const size_t k_Height = 1024 * 12;
#endif

	using GridArray = std::array<bool, k_Stride * k_Height>;
};

