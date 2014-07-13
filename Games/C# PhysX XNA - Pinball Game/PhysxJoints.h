#pragma once

#include "PhysicsEngine.h"
#include <iostream>
#include <iomanip>

namespace PhysicsEngine
{
	using namespace std;

	//Joint classes
	class DistanceJoint
	{
	public:
		DistanceJoint(PxRigidActor* actor0, PxTransform& localFrame0, PxRigidActor* actor1, PxTransform& localFrame1, PxReal stiffness=1.f, PxReal damping=1.f)
		{
			PxDistanceJoint* joint = PxDistanceJointCreate(*GetPhysics(), actor0, localFrame0, actor1, localFrame1);
			joint->setConstraintFlag(PxConstraintFlag::eVISUALIZATION,true);
			joint->setDistanceJointFlag(PxDistanceJointFlag::eSPRING_ENABLED, true);
			joint->setStiffness(stiffness);
			joint->setDamping(damping);
			joint->setConstraintFlag(PxConstraintFlag::eVISUALIZATION, true);
		}
	};
	class SphericalJoint
	{
		PxSphericalJoint* joint;
	public:
		SphericalJoint(PxRigidActor* actor0, PxTransform& localFrame0, PxRigidActor* actor1, PxTransform& localFrame1)
		{
			joint = PxSphericalJointCreate(*GetPhysics(), actor0, localFrame0, actor1, localFrame1);
			joint->setConstraintFlag(PxConstraintFlag::eVISUALIZATION,true);
		}

		void SetLimits(PxReal yAngle, PxReal zAngle)
		{
			joint->setLimitCone(PxJointLimitCone(yAngle,zAngle,0.01f));
			joint->setSphericalJointFlag(PxSphericalJointFlag::eLIMIT_ENABLED, true);
		}
	};
}