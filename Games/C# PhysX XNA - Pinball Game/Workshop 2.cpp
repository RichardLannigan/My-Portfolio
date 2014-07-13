#include <iostream>
#include "VD.h"

using namespace std;

int main()
{
	try 
	{ 
		PhysicsEngine::Init("Pinball Game", 512, 512); 
	}
	catch (Exception exc) 
	{ 
		cerr << exc.what() << endl;
		return 0; 
	}

	PhysicsEngine::Start();

	return 0;
}