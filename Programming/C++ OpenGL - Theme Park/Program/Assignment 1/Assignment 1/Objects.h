#include <stdio.h>
#include <stdlib.h>
#include <GL/glut.h>
#include <math.h>
#include <GL/gl.h>

#include <iostream>

using namespace std;

//Circle Theorem
int th = 30;
GLfloat PI = 3.14;

//Trigonometry
#define Cos(th) cos(PI/180*(th))
#define Sin(th) sin(PI/180*(th))
#define Tan(th) tan(PI/180*(th))

 #define SW 400
 #define SH 400
 int selected = 0;

//Camera
float xpos = 0, ypos = 0, zpos = 0, xrot = 0, yrot = 0,zrot = 0,  angle=0.0;
float lastx, lasty;

//Rotation
GLfloat rotateFerrisWheel = 0;
GLfloat rotateSupportBeam = 0;
GLfloat rotateCarriage = 0;
GLfloat rotateMerryGoRound = 0;
GLfloat rotateTeapot = 0;
GLfloat rotateTeacup = 0;
GLfloat rotateTeacups = 0;
GLfloat rotateLight = 0;
GLfloat rotateLightMoon = 180;

//Colours
GLfloat FerrisWheelColour = 0;
GLfloat MerryGoRoundColour = 0;
GLfloat TeaCupsColour = 0;

GLfloat aspectRatio = 0;

//Horse animation
bool MaxHeight = false;
GLfloat horseHeight = -1.025;

//Menu option booleans
bool Camera = true;
bool CameraMouse = false;
bool TopView = false;
bool FrontView = false;
bool BackView = false;
bool RightSideView = false;
bool LeftSideView = false;
bool FerrisWheel = false;
bool MerryGoRound = false;
bool TeaCups = false;
bool Winter = false;
bool snowing = false;
bool HideHUD = false;

bool FerrisWheelClicked = false;
bool MerryGoRoundClicked = false;
bool TeaCupsClicked = false;
bool SunMoonClicked = false;

bool FerrisWheelColourB = false;
bool MerryGoRoundColourB = false;
bool TeaCupsColourB = false;

static GLdouble ex, ey, ez, upx, upy, upz;

typedef GLfloat point3[3];
GLfloat w[3] = {0,0,0};

int board[3][3];   /*  amount of color for each square  */
float Xl=0.0, Xr=5.0, Yb=0.0, Yt=5.0, Zn=0.0, Zf=10.0;

//Fog
GLfloat Density;
GLfloat fogColor[4] = {0.5, 0.5, 0.5, 1.0};
bool Fog = false;

//Normal Calculations
GLfloat* CalculateNormals(GLfloat* u, GLfloat* v)
{
	w[0] = (v[1]*u[2]-v[2]*u[1]);
	w[1] = (v[2]*u[0]-v[0]*u[2]);
	w[2] = (v[0]*u[1]-v[1]*u[0]);

	return w;
}

//Basic shapes.
void Cube()
{
	point3 cube_vertices[8] = {{-1,-1,-1},{ 1,-1,-1},
		         			   { 1, 1,-1},{-1, 1,-1},
		         			   {-1,-1, 1},{ 1,-1, 1},
							   { 1, 1, 1},{-1, 1, 1}};

point3 cube_colors[8] = {{1,1,1},{1,0,0},
						 {1,1,0},{0,1,0},
						 {0,1,1},{1,0,1},
						 {1,1,1},{0,1,1}};

glVertexPointer(3,GL_FLOAT,0,cube_vertices);
glColorPointer(3,GL_FLOAT,0,cube_colors);

	glEnable(GL_NORMALIZE);
	//Front
	glNormal3f(0,0,1);
	//CalculateNormals(cube_vertices[5],cube_vertices[6]);
	//glNormal3f(w[0],w[1],w[2]);
	glBegin(GL_QUADS);
		glArrayElement(4);
		glArrayElement(5);
		glArrayElement(6);
		glArrayElement(7);
	glEnd();  

	//Back
	glNormal3f(0,0,-1);
	//CalculateNormals(cube_vertices[2],cube_vertices[3]);
	//glNormal3f(w[0],w[1],w[2]);
	glBegin(GL_QUADS); 
		glArrayElement(0);
		glArrayElement(1);
		glArrayElement(2);
		glArrayElement(3);
	glEnd();  

	//Top
	glNormal3f(0,1,0);
	//CalculateNormals(cube_vertices[2],cube_vertices[3]);
	//glNormal3f(w[0],w[1],w[2]);
	glBegin(GL_QUADS); 
		glArrayElement(2);
		glArrayElement(3);
		glArrayElement(7);
		glArrayElement(6);
	glEnd(); 

	//Bottom
	glNormal3f(0,-1,0);
	//CalculateNormals(cube_vertices[5],cube_vertices[4]);
	//glNormal3f(w[0],w[1],w[2]);
	glBegin(GL_QUADS);
		glArrayElement(0);
		glArrayElement(1);
		glArrayElement(5);
		glArrayElement(4);
	glEnd();   

	//Right
	glNormal3f(1,0,0);
	//CalculateNormals(cube_vertices[1],cube_vertices[2]);
	//glNormal3f(w[0],w[1],w[2]);
	glBegin(GL_QUADS);
		glArrayElement(1);
		glArrayElement(2);
		glArrayElement(6);
		glArrayElement(5);
	glEnd(); 

	//Left
	glNormal3f(-1,0,0);
	//CalculateNormals(cube_vertices[0],cube_vertices[4]);
	//glNormal3f(w[0],w[1],w[2]);
	glBegin(GL_QUADS);
		glArrayElement(0);
		glArrayElement(4);
		glArrayElement(7);
		glArrayElement(3);
	glEnd();
}
void Prism()
{
	point3 cube_vertices[6] = {{-1,-1, 1},{ 0, 1, 1},{ 1,-1, 1},
							   {-1,-1, 0},{ 0, 1, 0},{ 1,-1, 0}};

point3 cube_colors[8] = {{1,1,1},{1,0,0},
						 {1,1,0},{0,1,0},
						 {0,1,1},{1,0,1},
						 {1,1,1},{0,1,1}};

glVertexPointer(3,GL_FLOAT,0,cube_vertices);
glColorPointer(3,GL_FLOAT,0,cube_colors);

	//Front
	glNormal3f(0,0,3);
	glBegin(GL_TRIANGLES);
		glArrayElement(0);
		glArrayElement(1);
		glArrayElement(2);
	glEnd();  

	//Back
	glNormal3f(0,0,-3);
	glBegin(GL_TRIANGLES); 
		glArrayElement(3);
		glArrayElement(4);
		glArrayElement(5);
	glEnd();  

	//Bottom
	glNormal3f(0,-3,0);
	glBegin(GL_QUADS);
		glArrayElement(0);
		glArrayElement(2);
		glArrayElement(5);
		glArrayElement(3);
	glEnd();   

	//Right
	glNormal3f(3,3,0);
	glBegin(GL_QUADS);
		glArrayElement(1);
		glArrayElement(2);
		glArrayElement(5);
		glArrayElement(4);
	glEnd(); 

	//Left
	glNormal3f(-3,3,0);
	glBegin(GL_QUADS);
		glArrayElement(0);
		glArrayElement(1);
		glArrayElement(4);
		glArrayElement(3);
	glEnd();
}
void Square()
{
	glBegin(GL_QUADS);
		glVertex3f(0,0,0);
		glVertex3f(0,1,0);
		glVertex3f(1,1,0);
		glVertex3f(1,0,0);
		glVertex3f(0,0,0);
	glEnd();
}
void Circle()
{
	glBegin(GL_TRIANGLE_FAN);
	glNormal3f(0,1,0);
	for (int i = 0; i <= 360; i += 5) //Rotate by 5 degrees until i = 360.
	{
		glVertex3f(Cos(i),0,Sin(i));
	}
	glEnd();
}
void Tube()
{
	glBegin(GL_QUAD_STRIP);
    for (int i =  0; i <= 360; i += 5 ) 
	{
		glNormal3f(Cos(i),0,Sin(i));
		glVertex3f(Cos(i),+1,Sin(i));
		glVertex3f(Cos(i),-1,Sin(i));
    }
    glEnd();

}
void Cylinder()
{
	glColor3f(0.1,0.1,0.1);
	for (int i = 1; i >= -1; i -= 2) 
	{
		glBegin(GL_TRIANGLE_FAN);
		if (i == 1)
		{
			glNormal3f(0,1,0); //Defines normal for top of cylinder.
		}
		else
		{
			glNormal3f(0,-1,0); //Defines normal for bottom of cylinder.
		}

		glVertex3f(0,i,0);

		for (int j = 0; j <= 360; j += 18) //Rotate by 18 degrees until j = 360.
		{
			glVertex3f(Cos(j),i,Sin(j));
		}
	  glEnd();
	}
	glBegin(GL_QUAD_STRIP);
    for (int i =  0; i <= 360; i += 18 ) //Rotate by 18 degrees until i = 360.
	{
		glNormal3f(Cos(i),0.5,Sin(i));
		glVertex3f(Cos(i),+1,Sin(i));
		glVertex3f(Cos(i),-1,Sin(i));
    }
    glEnd();

}
void Pole()
{
	glPushMatrix();
		glScalef(0.05,1,0.05);
		Cylinder();
	glPopMatrix();
}
void Cone()
{
	glBegin(GL_TRIANGLES);
	for (int i = 0; i <= 360; i += 5) //Rotate by 5 degrees until i = 360.
	{
		glVertex3f(0,1,0);
		glNormal3f(Cos(i),0.5,Sin(i)); //I used 0.5 for the y value because of the sloping shape of the cone.
		glVertex3f(Cos(i),0,Sin(i));
		glVertex3f(Cos(i+5),0,Sin(i+5));
	}
	glEnd();
}
void TrianglePoles()
{
	glPushMatrix();
		glRotatef(-30,0,0,1);
		Pole();
	glPopMatrix();

	glPushMatrix();
		glTranslatef(0.5,-0.85,0);
		glRotatef(-90,0,0,1);
		Pole();
	glPopMatrix();

	glPushMatrix();
		glTranslatef(0.95,0,0);
		glRotatef(-150,0,0,1);
		Pole();
	glPopMatrix();
}

//Ferris Wheel - parts
void SupportBeam()
{
	glPushMatrix();
		glRotatef(90,1,0,0);
		glScalef(1,0.5,1);
		Pole();
	glPopMatrix();
}
void Carriage()
{
	glPushMatrix();
		glScalef(0.15,0.35,0.4);
		glTranslatef(0,-0.75,0);
		Cube();
	glPopMatrix();
}
void Stand()
{
	glPushMatrix();
		glTranslatef(0.5,0.875,0.5);
		TrianglePoles();
		glTranslatef(0,0,-1);
		TrianglePoles();
	glPopMatrix();

	glPushMatrix();
		glRotatef(90,1,0,0);
		glScalef(0.125,0.65,0.15);
		Cylinder();
	glPopMatrix();

	glPushMatrix();
		glRotatef(90,1,0,0);
		glTranslatef(0.98,0,-1.75);
		glScalef(0.125,0.65,0.15);
		Cylinder();
	glPopMatrix();

	glPushMatrix();
		glRotatef(90,1,0,0);
		glTranslatef(1.95,0,0);
		glScalef(0.125,0.65,0.15);
		Cylinder();
	glPopMatrix();

}
void WheelSection()
{
	//Create a singl, slanted f object for a section of the wheel.
	glPushMatrix();
		Pole();
		glRotatef(72,0,0,1);
		glScalef(1,0.6,1);
		glTranslatef(0.9,-0.5,0);
		Pole();
	glPopMatrix();

	glPushMatrix();
		glRotatef(72,0,0,1);
		glScalef(1,0.3,1);
		glTranslatef(0,-1,0);
		Pole();
	glPopMatrix();
}
void Wheel()
{
	//Duplicates the WheelSection() 10 times, to create the wheel.
	for (int i = 0; i < 360; i += 36) //i increments by 36 because I wanted 10 sections to the wheel and 360/10 = 36.
	{
		glPushMatrix();
			glRotatef(i,0,0,1); //Rotate WheelSection() by i.
			glTranslatef(0,1,0); //Translate WheelSection() into postion.
			WheelSection(); //Display WheelSection().
		glPopMatrix();
	}
}
//Complete object
void Ferris_Wheel()
{
	//Wheel (Back)
	glPushMatrix();
		glRotatef(rotateFerrisWheel,0,0,1);
		glColor3f(1,0,0);
		Wheel();
	glPopMatrix();

	//Wheel (Front)
	glPushMatrix();	
		glRotatef(rotateFerrisWheel,0,0,1);
		glTranslatef(0,0,1);
		Wheel();
	glPopMatrix();

	//Stand
	glPushMatrix();	
		glTranslatef(-1.95,-2.6,0.5);
		glScalef(2,1.5,2);
		Stand();
	glPopMatrix();

	glTranslatef(0,0,0.5);
	for (int i = 0; i < 360; i += 36)
	{
		glPushMatrix();
			glRotatef(rotateSupportBeam,0,0,1);
			glRotatef(i,0,0,1); //Rotates SupportBeam() by i.
			glTranslatef(0,1.9,0); //Translates SupportBeam() into position.
			SupportBeam(); //Display SupportBeam().
			glRotatef(-i,0,0,1); //Rotates Carriage() by i.
			glRotatef(rotateCarriage,0,0,1); //Translates Carriage() into position.
			Carriage(); //Display Carriage().
		glPopMatrix();
	}
}

//Merry-Go-Round - parts
void Poles()
{
	for (int j = 0; j <= 360; j += 45) 
	{
		glPushMatrix();
			glTranslatef(Cos(j),0,Sin(j));
			Pole();
		glPopMatrix();
	}
}
void Decoration()
{
	glPushMatrix();
		glTranslatef(-6,3.4,0);
		glScalef(1.25,1,1);
		for(int i = 0; i < 6; i++)
		{
			//Rotation outside "glPushMatrix()" so the pole will rotate with the equation r = i*60, where i is the pole number and r is the rotation value.
			glRotatef(60,0,1,0); //Rotate the new pole by another 60 degrees each loop. 60 degrees because 360/6 = 60, equal placement of poles.
			glPushMatrix();
				//Inside "glPushMatrix()" so transformations won't accumilate for each progressive pole.
				glTranslatef(0,0,1);
				glRotatef(-70,1,0,0); //Rotates each pole by -70 degrees to create a cone like arrangment of poles to fit the Merry_Go_Round.
				//Makes the 3rd and 6th poles longer
				if (i == 2)
				{
					glTranslatef(0,-0.3,0);
					glScalef(1,1.25,1);
				}
				if (i == 5)
				{
					glTranslatef(0,-0.3,0);
					glScalef(1,1.25,1);
				}
				Pole();
			glPopMatrix();
		}
	glPopMatrix();
}
void Horse()
{
	glPushMatrix();
		glColor3f(0,0,1);
		glScalef(0.125,0.25,0.4);
		Cube();
	glPopMatrix();
}
void Horses()
{
	//One
	glPushMatrix();	
		glTranslatef(-1.25,horseHeight,-0.5);
		glRotatef(-22.5,0,1,0);
		Horse();
	glPopMatrix();

	//Two
	glPushMatrix();
		glTranslatef(-1.25,horseHeight,0.5);
		glRotatef(22.5,0,1,0);
		Horse();
	glPopMatrix();

	//Three
	glPushMatrix();
		glTranslatef(-0.5,horseHeight,1.25);
		glRotatef(67.5,0,1,0);
		Horse();

	glPopMatrix();

	//Four
	glPushMatrix();
		glTranslatef(0.5,horseHeight,1.25);
		glRotatef(112.5,0,1,0);
		Horse();
	glPopMatrix();

	//Five
	glPushMatrix();
		glTranslatef(1.25,horseHeight,0.5);
		glRotatef(157.5,0,1,0);
		Horse();
	glPopMatrix();

	//Six
	glPushMatrix();
		glTranslatef(1.25,horseHeight,-0.5);
		glRotatef(202.5,0,1,0);
		Horse();
	glPopMatrix();

	//Seven
	glPushMatrix();
		glTranslatef(0.5,horseHeight,-1.25);
		glRotatef(247.5,0,1,0);
		Horse();
	glPopMatrix();

	//Eight
	glPushMatrix();
		glTranslatef(-0.5,horseHeight,-1.25);
		glRotatef(292.5,0,1,0);
		Horse();
	glPopMatrix();
}

//Teacups - parts
void Handle()
{
	glPushMatrix();
		glTranslatef(0,1.75,0);
		glRotatef(90,0,0,1);
		glScalef(0.5,1,0.25);
		Cylinder();
	glPopMatrix();

	glPushMatrix();
		glTranslatef(-0.9,3.9,0);
		glScalef(0.25,2.6,0.275);
		Cylinder();
		glTranslatef(2,1,0);
	glPopMatrix();

	glPushMatrix();
		glTranslatef(0,6,0);
		glRotatef(90,0,0,1);
		glScalef(0.5,1,0.25);
		Cylinder();
	glPopMatrix();
}
void singleTeacup()
{
	glRotatef(rotateTeacups,0,1,0);
	glPushMatrix();
		glTranslatef(0,0.1,0);
		glScalef(0.2,0.35,0.2);
		Tube();
		glScalef(0.8,1,0.8);
		Tube();
		glTranslatef(0,1,0);
		glRotatef(270,1,0,0);
		gluDisk(gluNewQuadric(),1,1.275,20,5);
	glPopMatrix();

	glPushMatrix();
		glTranslatef(-0.3,-0.4,0);
		glScalef(0.125,0.125,0.125);
		Handle();
	glPopMatrix();
}
void Teacup()
{
	for (int j = 0; j <= 360; j += 60) 
	{
		glPushMatrix();
			glTranslatef(Cos(j),0,Sin(j));
			singleTeacup();
		glPopMatrix();
	}
}

//Space
void Sun()
{
	glPushMatrix();
		glTranslatef(6,0,0);
		glTranslatef(35*Cos(rotateLight),35*Sin(rotateLight),30);
		glutSolidSphere(2,20,20);
	glPopMatrix();
}
void Moon()
{
	glPushMatrix();
		glTranslatef(35*Cos(rotateLightMoon),35*Sin(rotateLightMoon),30);
		glutSolidSphere(2,20,20);
	glPopMatrix();
}
void Stars()
{
	glPushMatrix();
	glTranslatef(-50,40,50);
		for (int i = 0; i < 15; i++)
		{
		glTranslatef(10,0,0);
		glPushMatrix();
			for (int i = 0; i < 15; i++)
			{
				glTranslatef(0,0,-10);
				glutSolidSphere(0.5,20,20);
			}
		glPopMatrix();
	}
	glPopMatrix();
}

//Paths
void singlePath()
{
	glBegin(GL_QUADS);
		glNormal3f(0,6.5,0);
		glVertex3f(0,0,0);
		glVertex3f(0,0,-6.5);
		glVertex3f(1,0,-6.5);
		glVertex3f(1,0,0);
	glEnd();

}
void Paths()
{
	glPushMatrix();
		glTranslatef(-0.5,0.002,15.55);
		glScalef(1,1,3);
		singlePath();
	glPopMatrix();

	glPushMatrix();
		glTranslatef(3.5,0.001,0.5);
		glRotatef(90,0,1,0);
		singlePath();
	glPopMatrix();
}
void SquarePath()
{
	glPushMatrix();
		glTranslatef(-1,0.001,0);
		singlePath();
		glTranslatef(6.5,0,0);
		singlePath();
	glPopMatrix();

	glPushMatrix();
		glTranslatef(6.5,0.001,0);
		glRotatef(90,0,1,0);
		singlePath();
	glPopMatrix();

	glPushMatrix();
		glTranslatef(6,0.001,-5.5);
		glRotatef(90,0,1,0);
		singlePath();
	glPopMatrix();
}

//Benches and tables
void BenchStrutt()
{
	glPushMatrix();
		glScalef(0.125,0.0625,1.2);
		Cube();
	glPopMatrix();
}
void BenchFrame()
{
	glPushMatrix();
		glTranslatef(-0.6,2,1.25);
		glRotatef(90,1,0,0);
		BenchStrutt();
	glPopMatrix();

	glPushMatrix();
		glTranslatef(0,2,1.25);
		glRotatef(90,0,1,0);
		glScalef(0.5,2,0.5);
		BenchStrutt();
	glPopMatrix();

	glPushMatrix();
		glTranslatef(0.475,1.4,1.25);
		glRotatef(90,1,0,0);
		glScalef(1,1,0.5);
		BenchStrutt();
	glPopMatrix();
}
void BenchStruttsX3()
{
	glPushMatrix();
		glTranslatef(0,2,0);
		BenchStrutt();
	glPopMatrix();

	glPushMatrix();
		glTranslatef(-0.4,2,0);
		BenchStrutt();
	glPopMatrix();

	glPushMatrix();
		glTranslatef(0.4,2,0);
		BenchStrutt();
	glPopMatrix();
}
void BenchStruttsX2()
{
	glPushMatrix();
		BenchStrutt();
	glPopMatrix();

	glPushMatrix();
		glTranslatef(0.4,0,0);
		BenchStrutt();
	glPopMatrix();

}
void Bench()
{
	glPushMatrix();
		BenchStruttsX3();
	glPopMatrix();

	glPushMatrix();
		glTranslatef(-0.6,2.5,0);
		glRotatef(90,0,0,1);
		BenchStruttsX2();
	glPopMatrix();

	glPushMatrix();
		BenchFrame();
		glTranslatef(0,0,-2.5);
		BenchFrame();
	glPopMatrix();
}
void TableLegs()
{
	//Cross beams
	glPushMatrix();
		glTranslatef(1.8,1.85,1.75);
		glRotatef(90,0,1,0);
		glRotatef(90,0,0,1);
		glScalef(1,1,1.3);
		BenchStrutt();
	glPopMatrix();

	glPushMatrix();
		glTranslatef(1.8,1,1.75);
		glRotatef(90,0,1,0);
		glRotatef(90,0,0,1);
		glScalef(1,1,2);
		BenchStrutt();
	glPopMatrix();

	//Legs
	glPushMatrix();
		glTranslatef(0,1,1.75);
		glRotatef(90,1,0,0);
		glRotatef(-45,0,1,0);
		BenchStrutt();
		glTranslatef(0,0,0);
	glPopMatrix();

	glPushMatrix();
		glTranslatef(3.5,0.94,1.75);
		glRotatef(90,1,0,0);
		glRotatef(45,0,1,0);
		BenchStrutt();
		glTranslatef(0,0,0);
	glPopMatrix();
}
void Table()
{
	glTranslatef(0,-0.05,0);
	//Table
	glPushMatrix();
		glTranslatef(0,2,0);
		glScalef(1.5,1,1.75);
		for (int i = 0; i < 7; i++)
		{
			glTranslatef(0.3,0,0);
			BenchStrutt();
		}
	glPopMatrix();

	//Benches
	glPushMatrix();
		glTranslatef(0,-1,0);
		glScalef(1,1,1.75);
		BenchStruttsX3();
		glTranslatef(3.5,0,0);
		BenchStruttsX3();
	glPopMatrix();

	//Legs
	glPushMatrix();
		TableLegs();
		glTranslatef(0,0,-3.5);
		TableLegs();
	glPopMatrix();
}
void MultipleTables()
{
	glPushMatrix();
		glScalef(0.25,0.25,0.35);
		glTranslatef(10,0,15);
		Table();
		glTranslatef(0,0,7.5);
		Table();
		glTranslatef(0,0,7.5);
		Table();
		glTranslatef(10,0,0);
		Table();
		glTranslatef(0,0,-7.5);
		Table();
		glTranslatef(0,0,-7.5);
		Table();
	glPopMatrix();
}

//Fences
void FencePost()
{
	glPushMatrix();
		glScalef(0.5,1.5,0.5);
		glTranslatef(0,-1,0.5);
		glScalef(1,1.5,0.5);
		Cube();
	glPopMatrix();

	glPushMatrix();
		glScalef(0.5,1,0.5);
		glTranslatef(0,1.25,0);
		glScalef(1,0.5,1);
		Prism();
	glPopMatrix();
}
void Fence()
{	
	glPushMatrix();
		glTranslatef(1,-2.6,3);
		glScalef(0.1,0.1,0.1);
		for (int i = 0; i < 50; i++)
		{
			glTranslatef(2,0,0);
			FencePost();
		}
		glScalef(49,0.35,0.25);
		glTranslatef(-1,0,0);
		Cube(); //Once all of the fence posts are displayed, two long beams created from the scaled �Cube()� are displayed on them, creating a picket fence look.
		glTranslatef(0,-6,0);
		Cube(); //Once all of the fence posts are displayed, two long beams created from the scaled �Cube()� are displayed on them, creating a picket fence look.
	glPopMatrix();

	glPushMatrix();
		glTranslatef(1,-2.6,11.9);
		glScalef(0.1,0.1,0.1);
		for (int i = 0; i < 50; i++)
		{
			glTranslatef(2,0,0);
			FencePost();
		}
		glScalef(49,0.35,0.25);
		glTranslatef(-1,0,2);
		Cube();
		glTranslatef(0,-6,0);
		Cube();
	glPopMatrix();

	glPushMatrix();
		glRotatef(90,0,1,0);
		glTranslatef(-12,-2.6,11);
		glScalef(0.1,0.1,0.1);
		for (int i = 0; i < 44; i++)
		{
			glTranslatef(2,0,0);
			FencePost();
		}
		glScalef(43,0.35,0.25);
		glTranslatef(-1,0,2);
		Cube();
		glTranslatef(0,-6,0);
		Cube();
	glPopMatrix();

	glPushMatrix();
		glRotatef(90,0,1,0);
		glTranslatef(-6.2,-2.6,1.15);
		glScalef(0.1,0.1,0.1);
		for (int i = 0; i < 15; i++)
		{
			glTranslatef(2,0,0);
			FencePost();
		}
		glScalef(14.7,0.35,0.25);
		glTranslatef(-0.9,0,0);
		Cube();
		glTranslatef(0,-6,0);
		Cube();
	glPopMatrix();

	glPushMatrix();
		glRotatef(90,0,1,0);
		glTranslatef(-12,-2.6,1.15);
		glScalef(0.1,0.1,0.1);
		for (int i = 0; i < 15; i++)
		{
			glTranslatef(2,0,0);
			FencePost();
		}
		glScalef(14.7,0.35,0.25);
		glTranslatef(-1,0,0);
		Cube();
		glTranslatef(0,-6,0);
		Cube();
	glPopMatrix();
}

//Text objects
void WelcomeSign()
{
	glPushMatrix();
		glTranslatef(-8.75,0.65,13);
		glScalef(0.05,0.65,0.05);
		Cylinder();
		glTranslatef(29,0,0);
		Cylinder();
	glPopMatrix();

	glPushMatrix();
		glTranslatef(-8,1.5,13);
		glScalef(1,0.25,0.125);
		Cube();
	glPopMatrix();
}
void TextSign()
{
	glPushMatrix();
		glTranslatef(-8,0.65,12.75);
		glScalef(0.05,0.65,0.05);
		Cylinder();
	glPopMatrix();

	glPushMatrix();
		glTranslatef(-8,0,12.75);
		glRotatef(30,0,1,0);
		glScalef(1.5,1.2,0);
		Square();
	glPopMatrix();
}
void SpotLight()
{
	glPushMatrix();
		glTranslatef(0,2.5,0);
		glScalef(0.125,2.5,0.125);
		Cylinder();
	glPopMatrix();

	glPushMatrix();
		glTranslatef(0,5,0);
		glRotatef(75,1,0,0);
		glRotatef(-35,0,0,1);
		glScalef(0.26,0.5,0.26);
		Cylinder();
	glPopMatrix();
}

//Detailing
void RubbishBins()
{
	glPushMatrix();
		glTranslatef(21.2,0.25,-18);
		glScalef(0.2,0.25,0.2);
		Tube();
		glTranslatef(0,0,20);
		Tube();
	glPopMatrix();

	glPushMatrix();
		glTranslatef(26.2,0.25,-11.6);
		glScalef(0.2,0.25,0.2);
		Tube();
		glTranslatef(0,0,41);
		Tube();
	glPopMatrix();
}
void LampPosts()
{
	//One
	glPushMatrix();
		glTranslatef(21,1,-16);
		glScalef(0.075,1,0.075);
		Cylinder();
		glTranslatef(0,1.1,0);
		glScalef(3.5,0.2,3.5);
		Cone();
		glTranslatef(0,-11,0);
		glScalef(1,4,1);
		Cone();
	glPopMatrix();

	//Two
	glPushMatrix();
		glTranslatef(23.65,1,-11.5);
		glScalef(0.075,1,0.075);
		Cylinder();
		glTranslatef(0,1.1,0);
		glScalef(3.5,0.2,3.5);
		Cone();
		glTranslatef(0,-11,0);
		glScalef(1,4,1);
		Cone();
	glPopMatrix();

	//Three
	glPushMatrix();
		glTranslatef(23.65,1,-3.5);
		glScalef(0.075,1,0.075);
		Cylinder();
		glTranslatef(0,1.1,0);
		glScalef(3.5,0.2,3.5);
		Cone();
		glTranslatef(0,-11,0);
		glScalef(1,4,1);
		Cone();
	glPopMatrix();

	//Four
	glPushMatrix();
		glTranslatef(32.5,1,-3.5);
		glScalef(0.075,1,0.075);
		Cylinder();
		glTranslatef(0,1.1,0);
		glScalef(3.5,0.2,3.5);
		Cone();
		glTranslatef(0,-11,0);
		glScalef(1,4,1);
		Cone();
	glPopMatrix();

	//Five
	glPushMatrix();
		glTranslatef(32.5,1,-11.5);
		glScalef(0.075,1,0.075);
		Cylinder();
		glTranslatef(0,1.1,0);
		glScalef(3.5,0.2,3.5);
		Cone();
		glTranslatef(0,-11,0);
		glScalef(1,4,1);
		Cone();
	glPopMatrix();
}
void LampPostLights()
{
	//One
	glPushMatrix();
		glTranslatef(21,2,-16);
		glScalef(0.1,0.1,0.1);
		Cylinder();
	glPopMatrix();

	//Two
	glPushMatrix();
		glTranslatef(23.65,2,-11.5);
		glScalef(0.1,0.1,0.1);
		Cylinder();
	glPopMatrix();

	//Three
	glPushMatrix();
		glTranslatef(23.65,2,-3.5);
		glScalef(0.1,0.1,0.1);
		Cylinder();
	glPopMatrix();

	//Four
	glPushMatrix();
		glTranslatef(32.5,2,-3.5);
		glScalef(0.1,0.1,0.1);
		Cylinder();
	glPopMatrix();

	//Five
	glPushMatrix();
		glTranslatef(32.5,2,-11.5);
		glScalef(0.1,0.1,0.1);
		Cylinder();
	glPopMatrix();
}

//Pool - parts
void PoolWall()
{
	glScalef(2.75,0.5,0.25);
	Cube();
}
void Step()
{
	glTranslatef(3,0,1);
	glScalef(0.075,0.075,2);
	Cube();
}
void Steps()
{
	glTranslatef(0.45,-0.35,0);
	for (int i = 0; i < 4; i++)
	{
		glTranslatef(-0.15,0.15,0);
		glPushMatrix();
			Step();
		glPopMatrix();
	}
	glTranslatef(2.9,-0.25,3);
	glScalef(1,0.5,0.75);
	Prism();
	glTranslatef(0,0,-6.335);
	Prism();
}
//Complete object
void SwimmingPool()
{
	glPushMatrix();
		PoolWall();
	glPopMatrix();

	glPushMatrix();
		glTranslatef(0,0,5);
		PoolWall();
	glPopMatrix();

	glPushMatrix();
		glTranslatef(-2.5,0,2.5);
		glRotatef(90,0,1,0);
		PoolWall();
	glPopMatrix();

	glPushMatrix();
		glTranslatef(2.5,0,2.5);
		glRotatef(90,0,1,0);
		PoolWall();
	glPopMatrix();

	glPushMatrix();
		glTranslatef(0,0,1.5);
		Steps();
	glPopMatrix();
}