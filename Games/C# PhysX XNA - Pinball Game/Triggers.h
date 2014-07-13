#pragma once

#include "PhysicsEngine.h"
#include <iostream>
#include <iomanip>

namespace PhysicsEngine
{
	using namespace std;

	static bool hasHit;

	//Trigger
	class TriggerBox : public Actor
	{
		PxVec3 dimensions;
		PxReal density;
		PxShape* shape;

	public:
		TriggerBox(PxTransform pose=PxTransform(PxIdentity), PxVec3 _dimensions=PxVec3(.5f,.5f,.5f), PxReal _density=1.f,
			const PxVec3& _color=PxVec3(.9f,0.f,0.f)) 
			: Actor(pose, _color), dimensions(_dimensions), density(_density)
		{ 
		}

		virtual void Create()
		{
			int triggerNumber = 0;
			PxRigidDynamic* triggerBox = GetPhysics()->createRigidDynamic(pose);
			shape = triggerBox->createShape(PxBoxGeometry(dimensions), *GetDefaultMaterial());
			PxRigidBodyExt::setMassAndUpdateInertia(*triggerBox, density);
			actor = triggerBox;
			//box->userData = &PxVec3(1,1,1);
			actor->userData = &color; //pass dcolor parameter to renderer
		}

		PxRigidDynamic* Get() 
		{
			return (PxRigidDynamic*)actor; 
		}

		//get a single shape
		PxShape* GetShape()
		{
			return shape;
		}
	};
	//A customised collision class, implemneting various callbacks
	class MySimulationEventCallback : public PxSimulationEventCallback
	{
	public:
		//an example variable that will be checked in the main simulation loop
		bool trigger;

		MySimulationEventCallback() : trigger(false) {}

		///Method called when the contact with the trigger object is detected.
		virtual void onTrigger(PxTriggerPair* pairs, PxU32 count) 
		{
			//you can read the trigger information here
			for (PxU32 i = 0; i < count; i++)
			{
				//filter out contact with the planes
				if (pairs[i].otherShape->getGeometryType() != PxGeometryType::ePLANE)
				{
					//check if eNOTIFY_TOUCH_FOUND trigger
					if (pairs[i].status & PxPairFlag::eNOTIFY_TOUCH_FOUND)
					{
						//cerr << "eNOTIFY_TOUCH_FOUND" << endl;
						trigger = true;
						if (hasHit == false)
						{
							counter--;
							lives--;
							hasHit = true;
							death = true;
							roundOver = true;
						}
					}
					//check if eNOTIFY_TOUCH_LOST trigger
					if (pairs[i].status & PxPairFlag::eNOTIFY_TOUCH_LOST)
					{
						//cerr << "eNOTIFY_TOUCH_LOST" << endl;
						trigger = false;
						if (hasHit == true)
						{
							//cerr << "counter " << counter <<endl;
							hasHit = false;
						}
					}
				}
			}
		}

		///Other types of events
		virtual void onContact(const PxContactPairHeader &pairHeader, const PxContactPair *pairs, PxU32 nbPairs) {}
		virtual void onConstraintBreak(PxConstraintInfo *constraints, PxU32 count) {}
		virtual void onWake(PxActor **actors, PxU32 count) {}
		virtual void onSleep(PxActor **actors, PxU32 count) {}
	};

	class Collectable : public Actor
	{
		PxVec3 dimensions;
		PxReal density;
		PxShape* shape;

	public:
		Collectable(PxTransform pose=PxTransform(PxIdentity), PxVec3 _dimensions=PxVec3(.5f,.5f,.5f), PxReal _density=1.f,
			const PxVec3& _color=PxVec3(.9f,0.f,0.f)) 
			: Actor(pose, _color), dimensions(_dimensions), density(_density)
		{ 
		}

		virtual void Create()
		{
			PxRigidDynamic* collectable = GetPhysics()->createRigidDynamic(pose);
			shape = collectable->createShape(PxBoxGeometry(dimensions), *GetDefaultMaterial());
			PxRigidBodyExt::setMassAndUpdateInertia(*collectable, density);
			actor = collectable;
			//box->userData = &PxVec3(1,1,1);
			actor->userData = &color; //pass dcolor parameter to renderer
		}

		PxRigidDynamic* Get() 
		{
			return (PxRigidDynamic*)actor; 
		}

		//get a single shape
		PxShape* GetShape()
		{
			return shape;
		}
	};

	class PickUpCollectable : public PxSimulationEventCallback
	{
	public:
		//an example variable that will be checked in the main simulation loop
		bool trigger;

		PickUpCollectable() : trigger(false) {}

		///Method called when the contact with the trigger object is detected.
		virtual void onTrigger(PxTriggerPair* pairs, PxU32 count) 
		{
			//you can read the trigger information here
			for (PxU32 i = 0; i < count; i++)
			{
				//filter out contact with the planes
				if (pairs[i].otherShape->getGeometryType() != PxGeometryType::ePLANE)
				{
					//check if eNOTIFY_TOUCH_FOUND trigger
					if (pairs[i].status & PxPairFlag::eNOTIFY_TOUCH_FOUND)
					{
						cerr << "eNOTIFY_TOUCH_FOUND" << endl;
						trigger = true;
						if (hasHit == false)
						{
							counter--;
							hasHit = true;
						}
					}
					//check if eNOTIFY_TOUCH_LOST trigger
					if (pairs[i].status & PxPairFlag::eNOTIFY_TOUCH_LOST)
					{
						cerr << "eNOTIFY_TOUCH_LOST" << endl;
						trigger = false;
						if (hasHit == true)
						{
							cerr << "counter " << counter <<endl;
							hasHit = false;
						}
					}
				}
			}
		}

		///Other types of events
		virtual void onContact(const PxContactPairHeader &pairHeader, const PxContactPair *pairs, PxU32 nbPairs) {}
		virtual void onConstraintBreak(PxConstraintInfo *constraints, PxU32 count) {}
		virtual void onWake(PxActor **actors, PxU32 count) {}
		virtual void onSleep(PxActor **actors, PxU32 count) {}
	};
	class CollisionTest : public PxSimulationEventCallback
	{
	public:
		//an example variable that will be checked in the main simulation loop
		bool trigger;

		CollisionTest() : trigger(false) {}

		///Method called when the contact with the trigger object is detected.
		virtual void onTrigger(PxTriggerPair* pairs, PxU32 count) 
		{
			//you can read the trigger information here
			for (PxU32 i = 0; i < count; i++)
			{
				//filter out contact with the planes
				if (pairs[i].otherShape->getGeometryType() != PxGeometryType::ePLANE)
				{
					//check if eNOTIFY_TOUCH_FOUND trigger
					if (pairs[i].status & PxPairFlag::eNOTIFY_TOUCH_FOUND)
					{
						cerr << "eNOTIFY_TOUCH_FOUND" << endl;
						trigger = true;
						if (hasHit == false)
						{
							counter--;
							hasHit = true;
						}
					}
					//check if eNOTIFY_TOUCH_LOST trigger
					if (pairs[i].status & PxPairFlag::eNOTIFY_TOUCH_LOST)
					{
						cerr << "eNOTIFY_TOUCH_LOST" << endl;
						trigger = false;
						if (hasHit == true)
						{
							cerr << "counter " << counter <<endl;
							hasHit = false;
						}
					}
				}
			}
		}

		///Other types of events
		virtual void onContact(const PxContactPairHeader &pairHeader, const PxContactPair *pairs, PxU32 nbPairs) {}
		virtual void onConstraintBreak(PxConstraintInfo *constraints, PxU32 count) {}
		virtual void onWake(PxActor **actors, PxU32 count) {}
		virtual void onSleep(PxActor **actors, PxU32 count) {}
	};
}