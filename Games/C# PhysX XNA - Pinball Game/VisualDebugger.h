#pragma once

#include "MyPhysicsEngine.h"

namespace PhysicsEngine
{
	using namespace physx;

	void Init(const char *window_name, int width=512, int height=512);

	void Start();
}

