#pragma once

#include "PhysicsEngine.h"
#include <iostream>
#include <iomanip>
#include "PinballMachine.h"
#ifndef PX_PHYSICS_NX_SCENEDESC
#define PX_PHYSICS_NX_SCENEDESC
#endif

namespace PhysicsEngine
{
	using namespace std;

	static int seconds;
	static int minutes;
	static int timer;
	static int lives = 3;
	static int roundScore = 0;
	static int totalScore = 0;
	static float scoreMultiplier = 1;
	static float firstRoundScore = 0;
	static float secondRoundScore = 0;
	static float thirdRoundScore = 0;
	static int roundNumber = 1;

	static PxVec3 currentVel;
	static PxReal currentMagnitude;
	static PxVec3 newVelocity;

	static bool hasHit;
	static bool gameStarted = false;
	static bool roundStarted = false;
	static bool roundOver = false;
	static bool death = false;
	static bool firstRun = true;
	static bool hitBouncePad = false;
	static bool hitBounceTopPad = false;

	Sphere* pinball;

	void CollisionTests(Sphere *pinball, Box *bonusPad1, Box *bonusPad2, Box *bonusPad3);
	void Scoring();

	//----Triggers----//

	//Trigger
	class CustomTrigger : public Actor
	{
		PxVec3 dimensions;
		PxReal density;
		PxShape* shape;
		int triggerType;

	public:
		CustomTrigger(PxTransform pose=PxTransform(PxIdentity), PxVec3 _dimensions=PxVec3(.5f,.5f,.5f), PxReal _density=1.f,
			const PxVec3& _color=PxVec3(.9f,0.f,0.f), int _triggerType = 0) 
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
	void CreateTriggers(CustomTrigger *trigger);
	//A customised collision class, implementing various callbacks
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
							if (pinball->Get()->getGlobalPose().p.z < -137)
								pinball->Get()->addForce(PxVec3(newVelocity)*200);
							if (pinball->Get()->getGlobalPose().p.z > -132 && pinball->Get()->getGlobalPose().p.z < -128)
							{
								scoreMultiplier += 1;
								pairs[i].triggerShape->setLocalPose(PxTransform(PxVec3(300,10,10)));
							}
							if (pinball->Get()->getGlobalPose().p.z > -123 && pinball->Get()->getGlobalPose().p.z < -117)
								pinball->Get()->addForce(PxVec3(newVelocity)*200);
							if (pinball->Get()->getGlobalPose().p.z > -107 && pinball->Get()->getGlobalPose().p.z < -103)
							{
								scoreMultiplier += 1;
								pairs[i].triggerShape->setLocalPose(PxTransform(PxVec3(300,10,10)));
							}
							if (pinball->Get()->getGlobalPose().p.z > -30 )
							{
								if (roundStarted == true)
								{
									counter--;
									lives--;
									death = true;
									roundOver = true;
								}
							}
							hasHit = true;
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

	//----Triggers End----//

	//Main scene
	class MyScene : public Scene
	{
#pragma region
		Plane* plane;
		Box* box;
		SidePanel *leftSidePanel;
		SidePanel *rightSidePanel;
		Capsule *capsule;
		Box *PlungerRailLeft;
		Box *PlungerRailRight;
		Capsule *capsuleTest;
		
		//----Cabinet----//
		Cabinet *pinballMachine;
		Box *lid;

		//----Moving parts----//
		SpringPlunger* springPlunger;

		//Right flipper
		Flipper *rightFlipperPaddle;
		Capsule *rightFlipperPin;
		//Left flipper
		Flipper *leftFlipperPaddle;
		Capsule *leftFlipperPin;

		//Spinners
		CapsuleDynamic *rightSpinnerPin;
		CapsuleDynamic *rightSpinnerArm;
		CapsuleDynamic *leftSpinnerPin;
		CapsuleDynamic *leftSpinnerArm;

		//Scoring
		MySimulationEventCallback *my_callback;

		//Triggers
		CustomTrigger *livesTrigger;
		CustomTrigger *bumperTrigger1;
		CustomTrigger *bumperTrigger2;
		CustomTrigger *bumperTrigger3;
		CustomTrigger *collectableTrigger1;
		CustomTrigger *collectableTrigger2;
		CustomTrigger *collectableTrigger3;
		CustomTrigger *collectableTrigger4;

		//Bonus pads
		Box *bonusPad1;
		Box *bonusPad2;
		Box *bonusPad3;

		PxPhysics* physx;
#pragma endregion

	public:
		///A custom scene class
		void SetVisualisation()
		{
			px_scene->setVisualizationParameter(PxVisualizationParameter::eSCALE, 1.0f);
			px_scene->setVisualizationParameter(PxVisualizationParameter::eCOLLISION_SHAPES, 1.0f);  
			px_scene->setVisualizationParameter(PxVisualizationParameter::eACTOR_AXES, 1.0f);
		}

		//Custom scene initialisation
		virtual void CustomInit() 
		{
			SetVisualisation();		

			//Enabling 
			px_scene->setFlag(PxSceneFlag::eENABLE_CCD, true);

			//Initialise and set the customised event callback
			my_callback = new MySimulationEventCallback();
			px_scene->setSimulationEventCallback(my_callback);

			plane = new Plane();
			Add(*plane);
			
			//-----Pinball machine start-----//

			//Cabinet
			pinballMachine = new Cabinet(this, PxTransform(-60,6,0.1));
			Add(*pinballMachine);

			//Moving parts//

			//Pinball
			pinball = new Sphere(PxTransform(PxVec3(-32, 18.5, -30)));
			Add(*pinball);
			pinball->Get()->setRigidBodyFlag(PxRigidBodyFlag::eENABLE_CCD, true);

			//Right flipper
			rightFlipperPaddle = new Flipper(PxTransform(PxVec3(-44,18.7,-41.5),PxQuat(0.125,PxVec3(1,0,0))),5,PxVec3(0,1,0));
			Add(*rightFlipperPaddle);
			rightFlipperPin = new Capsule(PxTransform(PxVec3(-45,19.7,-40),PxQuat(0.125,PxVec3(1,0,0))*PxQuat(PxPi/2,PxVec3(0,0,1))),1,PxVec3(1,0,0));
			Add(*rightFlipperPin);
			SphericalJoint rightFlipper(rightFlipperPin->Get(),PxTransform(PxVec3(0,0,0),PxQuat(PxPi/2,PxVec3(0.f,0.f,1.f))),
				rightFlipperPaddle->Get(),PxTransform(PxVec3(1,0.95,0),PxQuat(PxPi/2,PxVec3(0.f,0.f,1.f))));
			rightFlipper.SetLimits(PxPi/2, PxPi/2);
			rightFlipperPaddle->Get()->setRigidBodyFlag(PxRigidBodyFlag::eENABLE_CCD, true);

			//Left flipper
			leftFlipperPaddle = new Flipper(PxTransform(PxVec3(-74,18.7,-41.5),PxQuat(0.125,PxVec3(1,0,0))),5,PxVec3(0,1,0));
			Add(*leftFlipperPaddle);
			leftFlipperPin = new Capsule(PxTransform(PxVec3(-75,19.7,-40),PxQuat(0.125,PxVec3(1,0,0))*PxQuat(PxPi/2,PxVec3(0,0,1))),1,PxVec3(1,0,0));
			Add(*leftFlipperPin);
			SphericalJoint leftFlipper(leftFlipperPin->Get(),PxTransform(PxVec3(0,0,0),PxQuat(PxPi/2,PxVec3(0.f,0.f,1.f))),
				leftFlipperPaddle->Get(),PxTransform(PxVec3(1,0.95,0),PxQuat(PxPi/2,PxVec3(0.f,0.f,1.f))));
			leftFlipper.SetLimits(PxPi/2, PxPi/2);
			leftFlipperPaddle->Get()->setRigidBodyFlag(PxRigidBodyFlag::eENABLE_CCD, true);

			//Right spinner
			rightSpinnerArm = new CapsuleDynamic(PxTransform(PxVec3(-50,22,-60),PxQuat(0.125,PxVec3(1,0,0))),0.75f,6,1,PxVec3(0,1,0));
			Add(*rightSpinnerArm);
			rightSpinnerPin = new CapsuleDynamic(PxTransform(PxVec3(-50,22,-60),PxQuat(0.125,PxVec3(1,0,0))*PxQuat(PxPi/2,PxVec3(0,0,1))),1,1,1,PxVec3(1,0,0));
			Add(*rightSpinnerPin);
			rightSpinnerPin->Get()->setRigidBodyFlag(PxRigidBodyFlag::eKINEMATIC, true);
			SphericalJoint rightSpinner(rightSpinnerPin->Get(),PxTransform(PxVec3(0,0,0),PxQuat(PxPi/2,PxVec3(0.f,0.f,1.f))),
				rightSpinnerArm->Get(),PxTransform(PxVec3(0,0,0),PxQuat(PxPi/2,PxVec3(1.f,0.0f,0.0f))));
			rightSpinnerArm->Get()->setRigidBodyFlag(PxRigidBodyFlag::eENABLE_CCD, true);

			//Left spinner
			leftSpinnerArm = new CapsuleDynamic(PxTransform(PxVec3(-70,22,-60),PxQuat(0.125,PxVec3(1,0,0))),0.75f,6,1,PxVec3(0,1,0));
			Add(*leftSpinnerArm);
			leftSpinnerPin = new CapsuleDynamic(PxTransform(PxVec3(-70,22,-60),PxQuat(0.125,PxVec3(1,0,0))*PxQuat(PxPi/2,PxVec3(0,0,1))),1,1,1,PxVec3(1,0,0));
			Add(*leftSpinnerPin);
			leftSpinnerPin->Get()->setRigidBodyFlag(PxRigidBodyFlag::eKINEMATIC, true);
			SphericalJoint leftSpinner(leftSpinnerPin->Get(),PxTransform(PxVec3(0,0,0),PxQuat(PxPi/2,PxVec3(0.f,0.f,1.f))),
				leftSpinnerArm->Get(),PxTransform(PxVec3(0,0,0),PxQuat(PxPi/2,PxVec3(1.f,0.0f,0.0f))));
			leftSpinnerArm->Get()->setRigidBodyFlag(PxRigidBodyFlag::eENABLE_CCD, true);

			//Plunger
			springPlunger = new SpringPlunger(this, PxTransform(PxVec3(-32,16,-14.5)));
			Add(*springPlunger);
			springPlunger->Get()->setRigidBodyFlag(PxRigidBodyFlag::eENABLE_CCD, true);

			//-----Pinball machine end-----//

			//-----Scoring start-----//

			//Triggers//

			//Trigger to detect when a pinball is lost down the drop zone.
			livesTrigger = new CustomTrigger(PxTransform(PxVec3(-42,18.4,-30),PxQuat(0.125,PxVec3(1,0,0))),PxVec3(1,1,7.5),1,PxVec3(1,0,0),1);
			Add(*livesTrigger);

			//Triggers to detect when the pinball hits one of the bumbers.
			bumperTrigger1 = new CustomTrigger(PxTransform(PxVec3(-50,30,-120),PxQuat(0.125,PxVec3(1,0,0)) * PxQuat(PxPi/4,PxVec3(0,1,0))),PxVec3(4,1.5,4),1,PxVec3(1,0,0),2);
			Add(*bumperTrigger1);
			bumperTrigger2 = new CustomTrigger(PxTransform(PxVec3(-70,30,-120),PxQuat(0.125,PxVec3(1,0,0)) * PxQuat(PxPi/4,PxVec3(0,1,0))),PxVec3(4,1.5,4),1,PxVec3(1,0,0),2);
			Add(*bumperTrigger2);
			bumperTrigger3 = new CustomTrigger(PxTransform(PxVec3(-60,31.8,-142),PxQuat(0.125,PxVec3(1,0,0)) * PxQuat(PxPi/4,PxVec3(0,1,0))),PxVec3(4,1.5,4),1,PxVec3(1,0,0),2);
			Add(*bumperTrigger3);

			//Triggers to detect when the pinball passes through a collectable.
			collectableTrigger1 = new CustomTrigger(PxTransform(PxVec3(-50,28,-105),PxQuat(0.125,PxVec3(1,0,0))),PxVec3(1,1,1),1,PxVec3(1,1,0),3);
			Add(*collectableTrigger1);
			collectableTrigger2 = new CustomTrigger(PxTransform(PxVec3(-70,28,-105),PxQuat(0.125,PxVec3(1,0,0))),PxVec3(1,1,1),1,PxVec3(1,1,0),3);
			Add(*collectableTrigger2);
			collectableTrigger3 = new CustomTrigger(PxTransform(PxVec3(-80,31,-130),PxQuat(0.125,PxVec3(1,0,0))),PxVec3(1,1,1),1,PxVec3(1,1,0),3);
			Add(*collectableTrigger3);
			collectableTrigger4 = new CustomTrigger(PxTransform(PxVec3(-60,31,-130),PxQuat(0.125,PxVec3(1,0,0))),PxVec3(1,1,1),1,PxVec3(1,1,0),3);
			Add(*collectableTrigger4);

			//Pass each trigger to function and set appropriate properties.
			CreateTriggers(livesTrigger);
			CreateTriggers(bumperTrigger1);
			CreateTriggers(bumperTrigger2);
			CreateTriggers(bumperTrigger3);
			CreateTriggers(collectableTrigger1);
			CreateTriggers(collectableTrigger2);
			CreateTriggers(collectableTrigger3);
			CreateTriggers(collectableTrigger4);

			//Bonus pads
			bonusPad1 = new Box(PxTransform(PxVec3(-90.25,28.25,-100),PxQuat(0.125, PxVec3(1,0,0))),PxVec3(1,2,6),1,PxVec3(1,0,0));
			Add(*bonusPad1);
			bonusPad1->Get()->setRigidBodyFlag(PxRigidBodyFlag::eKINEMATIC, true);
			bonusPad1->GetShape()->setFlag(PxShapeFlag::eSIMULATION_SHAPE, false);

			bonusPad2 = new Box(PxTransform(PxVec3(-90.25,25,-75),PxQuat(0.125, PxVec3(1,0,0))),PxVec3(1,2,6),1,PxVec3(1,0,0));
			Add(*bonusPad2);
			bonusPad2->Get()->setRigidBodyFlag(PxRigidBodyFlag::eKINEMATIC, true);
			bonusPad2->GetShape()->setFlag(PxShapeFlag::eSIMULATION_SHAPE, false);

			bonusPad3 = new Box(PxTransform(PxVec3(-36.75,24.35,-80),PxQuat(0.125, PxVec3(1,0,0))),PxVec3(1,0.8,6),1,PxVec3(1,0,0));
			Add(*bonusPad3);
			bonusPad3->Get()->setRigidBodyFlag(PxRigidBodyFlag::eKINEMATIC, true);
			bonusPad3->GetShape()->setFlag(PxShapeFlag::eSIMULATION_SHAPE, false);

			//-----Scoring end-----//
		}

		//Custom udpate function
		virtual void CustomUpdate()
		{
			//Applies constant force to the plunger and flippers, 
			//allowing them to return to thier starting position.
			rightFlipperPaddle->Get()->addTorque(PxVec3(0,1,0.125)*1000,PxForceMode::eFORCE,true);
			leftFlipperPaddle->Get()->addTorque(PxVec3(0,1,0.125)*-1000,PxForceMode::eFORCE,true);

			//Applies torque to the spinners, allowing them to spin.
			rightSpinnerArm->Get()->addTorque(PxVec3(0,1,0.125)*-1000,PxForceMode::eFORCE,true);
			leftSpinnerArm->Get()->addTorque(PxVec3(0,1,0.125)*1000,PxForceMode::eFORCE,true);

			//To increase game pacing, a backwards force is applied to the pinball, 
			//making it move faster to the flippers. 
			pinball->Get()->addForce(PxVec3(0,0,1)*1.5,PxForceMode::eFORCE,true);

			//Calls function to handle special collision tests.
			CollisionTests(pinball, bonusPad1, bonusPad2, bonusPad3);

			//Calls function to handle scoring.
			Scoring();

			//Calculates pinball's current velocity vector and magnitude.
			currentVel = pinball->Get()->getLinearVelocity();
			currentMagnitude = sqrt((currentVel.x * currentVel.x) + (currentVel.z * currentVel.z));
			newVelocity = PxVec3(currentVel.x/currentMagnitude, 0, currentVel.z/currentMagnitude);
		}
	};

	//Collision tests for the "Bonus Pads".
	void CollisionTests(Sphere *pinball, Box *bonusPad1, Box *bonusPad2, Box *bonusPad3)
	{
		//If the pinball hits a pad it will apply a horizontal force to it.
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

		//Collision tests for the pinball. If the pinball is near the mouth of the plunger shaft, 
		//then a forwards and sideways force will be applied to push it away.
		if (pinball->Get()->getGlobalPose().p.x > -37 && pinball->Get()->getGlobalPose().p.x < -30)
		{
			if (pinball->Get()->getGlobalPose().p.z > -95 && pinball->Get()->getGlobalPose().p.z < -90)
			{
				pinball->Get()->addForce(PxVec3(0,0,1)*-20,PxForceMode::eFORCE,true);
				pinball->Get()->addForce(PxVec3(1,0,0)*-10,PxForceMode::eFORCE,true);
				if (gameStarted == false)
				{
					cerr << "Game Started!! "  << endl;
					gameStarted = true;
				}
				roundStarted = true;
			}
		}
	}
	//Handle scoring, timer and lives.
	void Scoring()
	{
		if (firstRun == true)
			{
				cerr << "Lives: " << lives << endl;
				firstRun = false;
			}

			if (death == true)
			{
				cerr << "Lives: " << lives << endl;
				death = false;
				roundOver = true;
			}

			//Timer
			if (roundStarted == true);
			{
				timer++;
				if (timer == 60) //If 60 ticks have passed then increase timer.
				{
					timer = 0;
					if (seconds < 60) //If seconds are less then 60 then increase seconds by 1.
					{
						seconds++;
						//cerr << "Timer: " << minutes << ":" << seconds << endl;
						if (roundStarted == true && roundOver == false)
						{
							//cerr << scoreMultiplier << endl;
							//scoreMultiplier += 0.1;
							roundScore += 20;
						}
					}
					else //Otherwise increase minutes by 1 and set seconds back to 0.
					{
						minutes++;
						seconds = 0;
					}
				}
			}

			//Handles the end of a round and prints complete results onto console window.
			if (roundOver == true)
			{
				roundStarted = false;
				totalScore += roundScore * scoreMultiplier;
				cerr << "Round score: " << roundScore << endl;
				cerr << "Score multiplier: " << scoreMultiplier << endl;

				if (roundNumber == 1)
				{
					firstRoundScore = roundScore * scoreMultiplier;
					cerr << "Total round score: " << firstRoundScore << endl;
				}
				if (roundNumber == 2)
				{
					secondRoundScore = roundScore * scoreMultiplier;
					cerr << "Total round score: " << secondRoundScore << endl;
				}
				if (roundNumber == 3)
				{
					thirdRoundScore = roundScore * scoreMultiplier;
					cerr << "Total round score: " << thirdRoundScore << endl;
				}
				cerr << "Round time: " << minutes << ":" << seconds << endl;

				scoreMultiplier = 1;
				roundScore = 0;
				roundOver = false;
				if (lives == 0)
				{
					cerr << endl;
					cerr << endl;
					cerr << endl;
					cerr << "GAME OVER " << endl;
					cerr << endl;
					cerr << "Total score: " << totalScore << endl;
					cerr << endl;
					cerr << endl;
					cerr << "Total time: " << minutes << ":" << seconds << endl;
					cerr << endl;
					cerr << endl;
					cerr << firstRoundScore << endl;
					cerr << endl;
					cerr << secondRoundScore << endl;
					cerr << endl;
					cerr << thirdRoundScore << endl;
				}
				roundNumber++;
			}
	}
	//Allows each trigger to be passed in and set up.
	void CreateTriggers(CustomTrigger *trigger)
	{
		trigger->Get()->setRigidBodyFlag(PxRigidBodyFlag::eKINEMATIC, true);
		trigger->GetShape()->setFlag(PxShapeFlag::eSIMULATION_SHAPE, false);
		trigger->GetShape()->setFlag(PxShapeFlag::eTRIGGER_SHAPE, true);
	}
}
