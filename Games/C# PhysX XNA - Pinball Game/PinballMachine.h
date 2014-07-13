#pragma once

#include <iostream>
#include <iomanip>
#include "PhysicsEngine.h"
#include "BasicShapes.h"

namespace PhysicsEngine
{
	using namespace std;

	//Player input objects
	class SpringPlunger : public Actor
	{
		PxVec3 dimensions;
		PxReal density;
		Box *base;

	public:
		SpringPlunger(Scene* scene, PxTransform pose=PxTransform(PxIdentity), PxVec3 _dimensions=PxVec3(0,0,0), PxReal _density=1.f,
			const PxVec3& _color=PxVec3(0,1,0)) 
			: Actor(pose, _color), dimensions(_dimensions), density(_density)
		{ 
			PxRigidDynamic* plunger = GetPhysics()->createRigidDynamic(pose);
			PxShape* head1 = plunger->createShape(PxBoxGeometry(PxVec3(0.5,1,1.6)), *GetDefaultMaterial(), PxTransform(PxVec3(-1.5f,0.45,-3.5),PxQuat(0.125,PxVec3(1,0,0))));
			PxShape* head2 = plunger->createShape(PxBoxGeometry(PxVec3(0.5,1,1.6)), *GetDefaultMaterial(), PxTransform(PxVec3(1.5f,0.45,-3.5),PxQuat(0.125,PxVec3(1,0,0))));
			PxShape* pole = plunger->createShape(PxBoxGeometry(PxVec3(2,1,5)), *GetDefaultMaterial());
			base = new Box(PxTransform(PxVec3(-32,18.5,-30),PxQuat(0.125,PxVec3(1,0,0))),PxVec3(2,0.5,0.5),100,PxVec3(1,0,0));
			scene->Add(*base);
			base->GetShape()->setFlag(PxShapeFlag::eSIMULATION_SHAPE, false);
			base->Get()->setRigidBodyFlag(PxRigidBodyFlag::eKINEMATIC, true);
			pole->setLocalPose(PxTransform(PxVec3(0,-0.35,3),PxQuat(0.125,PxVec3(1,0,0))));
			actor = plunger;
			actor->userData = &color; //Pass colour parameter to renderer
			DistanceJoint spring1(base->Get(),PxTransform(PxVec3(0,-1,0.5)),plunger,PxTransform(PxVec3(0,0,-8)),30.0f,3.5f);
		}

		virtual void Create()
		{

		}

		PxRigidDynamic* Get() 
		{
			return (PxRigidDynamic*)actor; 
		}
	};
	class Flipper : public Actor
	{
		PxReal density;

	public:
		//constructor
		Flipper(PxTransform pose=PxTransform(PxIdentity), PxReal _density=1.f,
			const PxVec3& _color=PxVec3(.9f,0.f,0.f))
			: Actor(pose, _color), density(_density)
		{
		}

		//Mesh cooking (preparation)
		PxConvexMesh* CookMesh()
		{
			PxConvexMeshDesc convexDesc;
			convexDesc.points.count     = sizeof(flipper_verts)/sizeof(PxVec3);
			convexDesc.points.stride    = sizeof(PxVec3);
			convexDesc.points.data      = flipper_verts;
			convexDesc.flags            = PxConvexFlag::eCOMPUTE_CONVEX;
			convexDesc.vertexLimit      = 256;

			PxDefaultMemoryOutputStream stream;

			if(!GetCooking()->cookConvexMesh(convexDesc, stream))
				throw new Exception("Pyramid::CookMesh, cooking failed.");

			PxDefaultMemoryInputData input(stream.getData(), stream.getSize());

			return GetPhysics()->createConvexMesh(input);
		}

		virtual void Create()
		{
			PxRigidDynamic* flipper = GetPhysics()->createRigidDynamic(pose);
			flipper->createShape(PxConvexMeshGeometry(CookMesh()), *GetDefaultMaterial());
			PxRigidBodyExt::setMassAndUpdateInertia(*flipper, density);
			actor = flipper;
			actor->userData = &color; //Pass colour parameter to renderer
		}

		PxRigidDynamic* Get() 
		{
			return (PxRigidDynamic*)actor; 
		}
	};

	//Pinball machine
	class Cabinet : public Actor
	{
		PxVec3 dimensions;
		PxReal density;

	public:
		Cabinet(Scene *scene, PxTransform pose=PxTransform(PxIdentity), PxVec3 _dimensions=PxVec3(3.5,1.5,0.25), PxReal _density=1.f,
			const PxVec3& _color=PxVec3(0.2,0.2,0.2)) 
			: Actor(pose, _color), dimensions(_dimensions), density(_density)
		{ 
			SidePanel *leftSidePanel = new SidePanel(PxTransform(-94,0,-1.65),1, PxVec3(0,0,0));
			scene->Add(*leftSidePanel);
			SidePanel *rightSidePanel = new SidePanel(PxTransform(-30,0,-1.65),1, PxVec3(0,0,0));
			scene->Add(*rightSidePanel);

			ReLoadSlide *reLoadSlide = new ReLoadSlide(PxTransform(PxVec3(-84,16,-18.45), PxQuat(0.125,PxVec3(1,0,0))),1,PxVec3(0.2,0.2,0.2));
			scene->Add(*reLoadSlide);

			Prism *rightBouncePad = new Prism(PxTransform(PxVec3(-45,31.5,-142), PxQuat(0.125,PxVec3(1,0,0))),1,PxVec3(1,1,1));
			scene->Add(*rightBouncePad);
			Prism *leftBouncePad = new Prism(PxTransform(PxVec3(-75,33.5,-142), PxQuat(0.125,PxVec3(1,0,0)) * PxQuat(PxPi,PxVec3(0,0,1))),1,PxVec3(0.65,0.65,0.65));
			scene->Add(*leftBouncePad);

			RightSlide *rightSlide = new RightSlide(PxTransform(PxVec3(-37,18.6,-43.5), PxQuat(0.125,PxVec3(1,0,0))),1,PxVec3(0.2,0.2,0.2));
			scene->Add(*rightSlide);

			LeftSlide *leftSlide = new LeftSlide(PxTransform(PxVec3(-90,19,-43.5), PxQuat(0.125,PxVec3(1,0,0))),1,PxVec3(0.2,0.2,0.2));
			scene->Add(*leftSlide);
		}

		virtual void Create()
		{
			PxRigidStatic* cabinet = GetPhysics()->createRigidStatic(pose);
			//Front panel
			PxShape* frontPanel = cabinet->createShape(PxBoxGeometry(PxVec3(34,12,2)), *GetDefaultMaterial());
			frontPanel->setLocalPose(PxTransform(PxVec3(0.0f, 0, 10)));

			//Back panel
			PxShape* backPanel = cabinet->createShape(PxBoxGeometry(PxVec3(34,18,2)), *GetDefaultMaterial());
			backPanel->setLocalPose(PxTransform(PxVec3(0.0f, 12, -143.75)));

			//Platform
			PxShape* platform = cabinet->createShape(PxBoxGeometry(PxVec3(30,0.5,77)), *GetDefaultMaterial());
			platform->setLocalPose(PxTransform(PxVec3(0.0f, 16.0f, -70), PxQuat(0.125,PxVec3(1,0,0))));

			//Pinball track right
			PxShape* rightPinballTrackLeft = cabinet->createShape(PxBoxGeometry(PxVec3(2,1,30)), *GetDefaultMaterial());
			rightPinballTrackLeft->setLocalPose(PxTransform(PxVec3(25.0f, 16.0f, -60.0f), PxQuat(0.125,PxVec3(1,0,0))));
			PxShape* rightPinballTrackRight = cabinet->createShape(PxBoxGeometry(PxVec3(0.5,1,30)), *GetDefaultMaterial());
			rightPinballTrackRight->setLocalPose(PxTransform(PxVec3(29.5f, 16.0f, -60.0f), PxQuat(0.125,PxVec3(1,0,0))));

			//Plunger block
			PxShape* plungerBlock = cabinet->createShape(PxBoxGeometry(PxVec3(28.5,1,13.25)), *GetDefaultMaterial());
			plungerBlock->setLocalPose(PxTransform(PxVec3(-2.5, 9.25f, -5.25f), PxQuat(0.125,PxVec3(1,0,0))));

			//Bottom block
			PxShape* bottomBlock = cabinet->createShape(PxBoxGeometry(PxVec3(3,1,13)), *GetDefaultMaterial());
			bottomBlock->setLocalPose(PxTransform(PxVec3(-27, 12.35f, -30.75f), PxQuat(0.125,PxVec3(1,0,0))));

			//Capsule obstacles
			PxShape *obstacle1 = cabinet->createShape(PxCapsuleGeometry(2,2),*GetDefaultMaterial());
			obstacle1->setLocalPose(PxTransform(PxVec3(-15,18,-70),PxQuat(PxPi/2,PxVec3(0,0,1))));
			PxShape *obstacle2 = cabinet->createShape(PxCapsuleGeometry(2,2),*GetDefaultMaterial());
			obstacle2->setLocalPose(PxTransform(PxVec3(15,18,-70),PxQuat(PxPi/2,PxVec3(0,0,1))));
			PxShape *obstacle3 = cabinet->createShape(PxCapsuleGeometry(2,2),*GetDefaultMaterial());
			obstacle3->setLocalPose(PxTransform(PxVec3(0,20.5,-90),PxQuat(PxPi/2,PxVec3(0,0,1))));
			PxShape *obstacle4 = cabinet->createShape(PxCapsuleGeometry(2,2),*GetDefaultMaterial());
			obstacle4->setLocalPose(PxTransform(PxVec3(-15,23,-110),PxQuat(PxPi/2,PxVec3(0,0,1))));
			PxShape *obstacle5 = cabinet->createShape(PxCapsuleGeometry(2,2),*GetDefaultMaterial());
			obstacle5->setLocalPose(PxTransform(PxVec3(15,23,-110),PxQuat(PxPi/2,PxVec3(0,0,1))));

			//Bumbers
			PxShape *boxObstacle1 = cabinet->createShape(PxBoxGeometry(PxVec3(3,1.5,3)), *GetDefaultMaterial());
			boxObstacle1->setLocalPose(PxTransform(PxVec3(0,26.5,-141.5), PxQuat(0.125,PxVec3(1,0,0)) * PxQuat(PxPi/4,PxVec3(0,1,0))));
			PxShape *boxObstacle2 = cabinet->createShape(PxBoxGeometry(PxVec3(3,1.5,3)), *GetDefaultMaterial());
			boxObstacle2->setLocalPose(PxTransform(PxVec3(-10,25,-120), PxQuat(0.125,PxVec3(1,0,0)) * PxQuat(PxPi/4,PxVec3(0,1,0))));
			PxShape *boxObstacle3 = cabinet->createShape(PxBoxGeometry(PxVec3(3,1.5,3)), *GetDefaultMaterial());
			boxObstacle3->setLocalPose(PxTransform(PxVec3(10,25,-120), PxQuat(0.125,PxVec3(1,0,0)) * PxQuat(PxPi/4,PxVec3(0,1,0))));

			//Invisible lid
			PxShape *lid = cabinet->createShape(PxBoxGeometry(PxVec3(35,1,80)), *GetDefaultMaterial());
			lid->setLocalPose(PxTransform(PxVec3(0,19.675,-71),PxQuat(0.125,PxVec3(1,0,0))));

			PxShape *rightLowerRestrictor = cabinet->createShape(PxBoxGeometry(PxVec3(1,1,1)), *GetDefaultMaterial());
			rightLowerRestrictor->setLocalPose(PxTransform(PxVec3(15,13,-35),PxQuat(0.125,PxVec3(1,0,0))));

			PxShape *leftUpperRestrictor = cabinet->createShape(PxBoxGeometry(PxVec3(1,1,1)), *GetDefaultMaterial());
			leftUpperRestrictor->setLocalPose(PxTransform(PxVec3(-17,15,-51),PxQuat(0.125,PxVec3(1,0,0))));

			actor = cabinet;
			actor->userData = &color; //Pass colour parameter to renderer
		}

		PxRigidDynamic* Get() 
		{
			return (PxRigidDynamic*)actor; 
		}
	};
}