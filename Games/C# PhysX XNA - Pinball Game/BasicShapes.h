#pragma once

#include <iostream>
#include <iomanip>
#include "PhysicsEngine.h"
#include "PhysxJoints.h"

namespace PhysicsEngine
{
	using namespace std;

	//Convex shape vertices
	static const PxVec3 prism_verts[] = {PxVec3(0,0,0),PxVec3(15,0,0),PxVec3(15,0,15),PxVec3(15,2,15),PxVec3(15,2,0),PxVec3(0,2,0)};
	static const PxVec3 sidePanel_verts[] = {PxVec3(0,0,9.75),PxVec3(0,0,-140),PxVec3(0,36,-140),PxVec3(0,18,9.75), 
											 PxVec3(4,18,9.75),PxVec3(4,36,-140),PxVec3(4,0,-140),PxVec3(4,0,9.75)};
	static const PxVec3 reLoadSlide_verts[] = {PxVec3(0,0,0),PxVec3(0,0,-15),PxVec3(50,0,0),
											   PxVec3(50,2,0),PxVec3(0,2,-15),PxVec3(0,2,0)};
	static const PxVec3 rightSlide_verts[] = {PxVec3(0,0,0),PxVec3(0,0,-15),PxVec3(-7,0,0),
											   PxVec3(-7,2,0),PxVec3(0,2,-15),PxVec3(0,2,0)};
	static const PxVec3 leftSlide_verts[] = {PxVec3(0,0,0),PxVec3(0,0,-15),PxVec3(14,0,0),
											   PxVec3(14,2,0),PxVec3(0,2,-15),PxVec3(0,2,0)};
	static const PxVec3 flipper_verts[] = {PxVec3(0,0,0),PxVec3(0,0,-2),PxVec3(1,0,-12),PxVec3(2,0,-2),PxVec3(2,0,0),
										   PxVec3(2,2,0),PxVec3(2,2,-2),PxVec3(1,2,-12),PxVec3(0,2,-2),PxVec3(0,2,0)};

	//Simple shapes
	class Plane : public Actor
	{
		PxVec3 normal;
		PxReal distance;

	public:
		//A plane with default paramters: XZ plane centred at (0,0,0)
		Plane(PxVec3 _normal=PxVec3(0.f, 1.f, 0.f), PxReal _distance=0.f,
			const PxVec3& color=PxVec3(.5f,.5f,.5f)) 
			: Actor(PxTransform(PxIdentity), color), normal(_normal), distance(_distance)
		{
		}

		virtual void Create()
		{
			PxRigidStatic* plane = PxCreatePlane(*GetPhysics(), PxPlane(normal, distance), *GetDefaultMaterial());
			actor = plane;
			actor->userData = &color; //Pass colour parameter to renderer
		}
	};
	class Box : public Actor
	{
		PxVec3 dimensions;
		PxReal density;
		PxShape* shape;

	public:
		Box(PxTransform pose=PxTransform(PxIdentity), PxVec3 _dimensions=PxVec3(.5f,.5f,.5f), PxReal _density=1.f,
			const PxVec3& _color=PxVec3(.9f,0.f,0.f)) 
			: Actor(pose, _color), dimensions(_dimensions), density(_density)
		{ 
		}

		virtual void Create()
		{
			PxRigidDynamic* box = GetPhysics()->createRigidDynamic(pose);
			shape = box->createShape(PxBoxGeometry(dimensions), *GetDefaultMaterial());
			PxRigidBodyExt::setMassAndUpdateInertia(*box, density);
			actor = box;
			actor->userData = &color; //Pass colour parameter to renderer
		}

		PxRigidDynamic* Get() 
		{
			return (PxRigidDynamic*)actor; 
		}

		//Get a single shape
		PxShape* GetShape()
		{
			return shape;
		}
	};
	class Sphere : public Actor
	{
		PxReal radius;
		PxReal density;
		
	public:
		Sphere(PxTransform pose=PxTransform(PxIdentity), PxReal _radius = PxReal(1), PxReal _density=0.1f,
			const PxVec3& _color=PxVec3(.0f,0.0f,0.9f)) 
			: Actor(pose, _color), radius(_radius), density(_density)
		{ 
		}

		virtual void Create()
		{
			PxRigidDynamic* sphere = GetPhysics()->createRigidDynamic(pose);
			PxShape* shape = sphere->createShape(PxSphereGeometry(radius), *GetDefaultMaterial());
			PxRigidBodyExt::setMassAndUpdateInertia(*sphere, density);
			actor = sphere;
			actor->userData = &color; //Pass colour parameter to renderer
		}

		PxRigidDynamic* Get() 
		{
			return (PxRigidDynamic*)actor; 
		}
	};
	class Capsule : public Actor
	{
		PxVec2 dimensions;
		PxReal density;
		PxShape* shape;
	public:
		Capsule(PxTransform pose=PxTransform(PxIdentity), PxReal _density=1.f,
			const PxVec3& _color=PxVec3(.9f,0.f,0.f)) 
			: Actor(pose, _color), density(_density)
		{
		}

		virtual void Create()
		{
			PxRigidStatic* capsule = GetPhysics()->createRigidStatic(pose);
			shape = capsule->createShape(PxCapsuleGeometry(2,2),*GetDefaultMaterial());
			actor = capsule;
			actor->userData = &color; //Pass color parameter to renderer
		}

		PxRigidStatic* Get() 
		{
			return (PxRigidStatic*)actor; 
		}
	};
	class CapsuleDynamic : public Actor
	{
		float radius;
		float height;
		PxReal density;
		PxShape* shape;
	public:
		CapsuleDynamic(PxTransform pose=PxTransform(PxIdentity), float _radius=(0.5f), float _height=(1), PxReal _density=1.f,
			const PxVec3& _color=PxVec3(.9f,0.f,0.f)) 
			: Actor(pose, _color), radius(_radius), height(_height), density(_density)
		{
		}

		virtual void Create()
		{
			PxRigidDynamic* capsule = GetPhysics()->createRigidDynamic(pose);
			shape = capsule->createShape(PxCapsuleGeometry(radius,height),*GetDefaultMaterial());
			PxRigidBodyExt::setMassAndUpdateInertia(*capsule, density);
			actor = capsule;
			actor->userData = &color; //Pass color parameter to renderer
		}

		PxRigidDynamic* Get() 
		{
			return (PxRigidDynamic*)actor; 
		}

		//Get a single shape
		PxShape* GetShape()
		{
			return shape;
		}
	};

	//Convex shapes
	class SidePanel : public Actor
	{
		PxReal density;

	public:
		//Constructor
		SidePanel(PxTransform pose=PxTransform(PxIdentity), PxReal _density=1.f,
			const PxVec3& _color=PxVec3(.9f,0.f,0.f))
			: Actor(pose, _color), density(_density)
		{
		}

		//Mesh cooking (preparation)
		PxConvexMesh* CookMesh()
		{
			PxConvexMeshDesc convexDesc;
			convexDesc.points.count     = sizeof(sidePanel_verts)/sizeof(PxVec3);
			convexDesc.points.stride    = sizeof(PxVec3);
			convexDesc.points.data      = sidePanel_verts;
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
			PxRigidStatic* sidePanel = GetPhysics()->createRigidStatic(pose);
			sidePanel->createShape(PxConvexMeshGeometry(CookMesh()), *GetDefaultMaterial());
			actor = sidePanel;
			actor->userData = &color; //Pass color parameter to renderer
		}

		PxRigidDynamic* Get() 
		{
			return (PxRigidDynamic*)actor; 
		}
	};
	class ReLoadSlide : public Actor
	{
		PxReal density;

	public:
		//Constructor
		ReLoadSlide(PxTransform pose=PxTransform(PxIdentity), PxReal _density=1.f,
			const PxVec3& _color=PxVec3(.9f,0.f,0.f))
			: Actor(pose, _color), density(_density)
		{
		}

		//Mesh cooking (preparation)
		PxConvexMesh* CookMesh()
		{
			PxConvexMeshDesc convexDesc;
			convexDesc.points.count     = sizeof(reLoadSlide_verts)/sizeof(PxVec3);
			convexDesc.points.stride    = sizeof(PxVec3);
			convexDesc.points.data      = reLoadSlide_verts;
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
			PxRigidStatic* reLoadSlide = GetPhysics()->createRigidStatic(pose);
			reLoadSlide->createShape(PxConvexMeshGeometry(CookMesh()), *GetDefaultMaterial());
			actor = reLoadSlide;
			actor->userData = &color; //Pass color parameter to renderer
		}

		PxRigidDynamic* Get() 
		{
			return (PxRigidDynamic*)actor; 
		}
	};
	class RightSlide : public Actor
	{
		PxReal density;

	public:
		//Constructor
		RightSlide(PxTransform pose=PxTransform(PxIdentity), PxReal _density=1.f,
			const PxVec3& _color=PxVec3(.9f,0.f,0.f))
			: Actor(pose, _color), density(_density)
		{
		}

		//Mesh cooking (preparation)
		PxConvexMesh* CookMesh()
		{
			PxConvexMeshDesc convexDesc;
			convexDesc.points.count     = sizeof(rightSlide_verts)/sizeof(PxVec3);
			convexDesc.points.stride    = sizeof(PxVec3);
			convexDesc.points.data      = rightSlide_verts;
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
			PxRigidStatic* Slide = GetPhysics()->createRigidStatic(pose);
			Slide->createShape(PxConvexMeshGeometry(CookMesh()), *GetDefaultMaterial());
			actor = Slide;
			actor->userData = &color; //Pass color parameter to renderer
		}

		PxRigidDynamic* Get() 
		{
			return (PxRigidDynamic*)actor; 
		}
	};
	class LeftSlide : public Actor
	{
		PxReal density;

	public:
		//Constructor
		LeftSlide(PxTransform pose=PxTransform(PxIdentity), PxReal _density=1.f,
			const PxVec3& _color=PxVec3(.9f,0.f,0.f))
			: Actor(pose, _color), density(_density)
		{
		}

		//Mesh cooking (preparation)
		PxConvexMesh* CookMesh()
		{
			PxConvexMeshDesc convexDesc;
			convexDesc.points.count     = sizeof(leftSlide_verts)/sizeof(PxVec3);
			convexDesc.points.stride    = sizeof(PxVec3);
			convexDesc.points.data      = leftSlide_verts;
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
			PxRigidStatic* Slide = GetPhysics()->createRigidStatic(pose);
			Slide->createShape(PxConvexMeshGeometry(CookMesh()), *GetDefaultMaterial());
			actor = Slide;
			actor->userData = &color; //Pass color parameter to renderer
		}

		PxRigidDynamic* Get() 
		{
			return (PxRigidDynamic*)actor; 
		}
	};
	class Prism : public Actor
	{
		PxReal density;

	public:
		//Constructor
		Prism(PxTransform pose=PxTransform(PxIdentity), PxReal _density=1.f,
			const PxVec3& _color=PxVec3(.9f,0.f,0.f))
			: Actor(pose, _color), density(_density)
		{
		}

		//Mesh cooking (preparation)
		PxConvexMesh* CookMesh()
		{
			PxConvexMeshDesc convexDesc;
			convexDesc.points.count     = sizeof(prism_verts)/sizeof(PxVec3);
			convexDesc.points.stride    = sizeof(PxVec3);
			convexDesc.points.data      = prism_verts;
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
			PxRigidStatic* prism = GetPhysics()->createRigidStatic(pose);
			prism->createShape(PxConvexMeshGeometry(CookMesh()), *GetDefaultMaterial());
			actor = prism;
			actor->userData = &color; //Pass color parameter to renderer
		}

		PxRigidDynamic* Get() 
		{
			return (PxRigidDynamic*)actor; 
		}
	};
}