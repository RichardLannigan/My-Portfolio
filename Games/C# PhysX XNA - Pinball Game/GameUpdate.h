#pragma once

#include "PhysicsEngine.h"
#include <iostream>
#include <iomanip>
#include "PinballMachine.h"

namespace PhysicsEngine
{
	using namespace std;

	static float scoreMultiplier = 1;

	static bool hitBouncePad = false;
	static bool hitBounceTopPad = false;

	void CollisionTests(Sphere *pinball, Box *bonusPad1, Box *bonusPad2, Box *bonusPad3)
	{
		if (pinball->Get()->getGlobalPose().p.x <= bonusPad1->pose.p.x + 3
				&& pinball->Get()->getGlobalPose().p.x >= bonusPad1->pose.p.x)
			{
				if (pinball->Get()->getGlobalPose().p.z <= bonusPad1->pose.p.z + 6
					&& pinball->Get()->getGlobalPose().p.z >= bonusPad1->pose.p.z - 6)
				{
					pinball->Get()->addForce(PxVec3(1,0,0)*10,PxForceMode::eFORCE,true);
					if (hitBouncePad == false)
						scoreMultiplier += 0.5;
					hitBouncePad = true;
				}
				else
					hitBouncePad = false;
			}

			if (pinball->Get()->getGlobalPose().p.x <= bonusPad2->pose.p.x + 3
				&& pinball->Get()->getGlobalPose().p.x >= bonusPad2->pose.p.x)
			{
				if (pinball->Get()->getGlobalPose().p.z <= bonusPad2->pose.p.z + 6
					&& pinball->Get()->getGlobalPose().p.z >= bonusPad2->pose.p.z - 6)
				{
					pinball->Get()->addForce(PxVec3(1,0,0)*10,PxForceMode::eFORCE,true);
					if (hitBounceTopPad == false)
						scoreMultiplier += 0.5;
					hitBounceTopPad = true;
				}
				else
					hitBounceTopPad = false;
			}

			if (pinball->Get()->getGlobalPose().p.x >= bonusPad3->pose.p.x - 3
				&& pinball->Get()->getGlobalPose().p.x <= bonusPad3->pose.p.x)
			{
				if (pinball->Get()->getGlobalPose().p.z <= bonusPad3->pose.p.z + 6
					&& pinball->Get()->getGlobalPose().p.z >= bonusPad3->pose.p.z - 6)
				{
					pinball->Get()->addForce(PxVec3(1,0,0)*-10,PxForceMode::eFORCE,true);
					if (hitBouncePad == false)
						scoreMultiplier += 0.5;
					hitBouncePad = true;
				}
				else
					hitBouncePad = false;
			}
	}
}