#include <stdio.h>
#include <stdlib.h>
#include <GL/glut.h>
#include <math.h>
#include <GL/gl.h>

#include <iostream>
#include <windows.h>

#ifdef __APPLE__
#include <OpenGL/OpenGL.h>
#include <GLUT/glut.h>
#else
#include <GL/glut.h>
#endif

#include "imageloader.h"
#include "Objects.h"
#include "Subdivision and Recursion.h"
#include "Water.h"
#include "Triangles.h"

using namespace std;

//Defining Images
Image* imageGrass;
Image* imageSnow;
Image* imageWelcome;
Image* imageText;

void HorseAnimation();

void init(void)
{ 
	glEnable(GL_DEPTH_TEST);
	//glLineWidth(3);			// Width of the drawing line	   
	glClearColor(0.53,0.8,1.0,1);
	glShadeModel (GL_SMOOTH);
	glEnable(GL_NORMALIZE);

	int i, j;
   for (i = 0; i < 3; i++) 
      for (j = 0; j < 3; j ++)
         board[i][j] = 0;
   glClearColor (1.0, 1.0, 1.0, 1.0);

	//Assigning images
	imageGrass = loadBMP("grass.bmp");
	imageSnow = loadBMP("snow.bmp");
	imageWelcome = loadBMP("Welcome.bmp");
	imageText = loadBMP("Text.bmp");

	glEnable(GL_LIGHTING); //Turn on lighting.
	glShadeModel (GL_SMOOTH);

	glEnableClientState(GL_VERTEX_ARRAY);	// Enable Vertex_Array facilities.
	glEnableClientState(GL_COLOR_ARRAY);
}

//Camera and set views
void camera (void)
{
    glRotatef(xrot,1.0,0.0,0.0);  //Rotate camera on the x-axis (left and right).
    glRotatef(yrot,0.0,1.0,0.0);  //Rotate camera on the y-axis (up and down).
    glTranslated(-xpos,-ypos,-zpos); //Translate the screen to the position of camera.
}
void MouseMovement(int x, int y)
{
	if (CameraMouse == true)
	{
		int diffx=x-lastx; //Check the difference between the current x and the last x position.
		int diffy=y-lasty; //Check the difference between the current y and the last y position.
		lastx=x; //Set lastx to the current x position.
		lasty=y; //Set lasty to the current y position.
		xrot += (float) diffy; //Set the xrot to xrot with the addition of the difference in the y position.
		yrot += (float) diffx; //Set the xrot to yrot with the addition of the difference in the x position.
	}
}
void CameraView()
{
	if (Camera == true)
	{
		camera();
	}

	if (Camera == false)
	{
		if (TopView == true)
		{
			glTranslatef(0,-10,-30);
			glRotatef(90,1,0,0);		
		}
		if (FrontView == true)
		{
			glTranslatef(0.5,0,0);
		}
		if (BackView == true)
		{
			glTranslatef(0,-1.5,-31);
			glRotatef(180,0,1,0);			
		}
		if (RightSideView == true)
		{
			glTranslatef(-15,-1.5,-18);
			glRotatef(-90,0,1,0);		
		}
		if (LeftSideView == true)
		{
			glTranslatef(7.5,-1.5,-18);
			glRotatef(90,0,1,0);
		}
		if (FerrisWheel == true)
		{
			glTranslatef(0,-2,15);
		}
		if (MerryGoRound == true)
		{
			glRotatef(-65,0,1,0);
			glTranslatef(0,-1,12);
		}
		if (TeaCups == true)
		{
			glRotatef(90,0,1,0);
			glTranslatef(0,-1,15);
		}
	}
}

//Subdivision and Recursion objects
void Mountains()
{
	glPushMatrix();
		glScalef(7.5,7.5,7.5);
		glRotatef(34,0,1,0);
		divide_triangle3(v[0], v[1], v[2], n);
	glPopMatrix();
}
void MountainRange()
{
	glTranslatef(-30,0,15);
	glPushMatrix();
		Mountains();
		glTranslatef(0,0,-12);
		Mountains();
		glTranslatef(0,0,-12);
		Mountains();
		glTranslatef(0,0,-10);
		Mountains();

		glRotatef(-90,0,1,0);
		glTranslatef(0,0,-10);
		Mountains();
		glTranslatef(0,0,-12);
		Mountains();
		glTranslatef(0,0,-12);
		Mountains();
		glTranslatef(0,0,-10);
		Mountains();

		glRotatef(-90,0,1,0);
		glTranslatef(0,0,-10);
		Mountains();
		glTranslatef(0,0,-12);
		Mountains();
		glTranslatef(0,0,-12);
		Mountains();
		glTranslatef(0,0,-10);
		Mountains();

		glRotatef(-90,0,1,0);
		glTranslatef(0,0,-10);
		Mountains();
		glTranslatef(0,0,-12);
		Mountains();
		glTranslatef(0,0,-12);
		Mountains();
		glTranslatef(0,0,-10);
		Mountains();
	glPopMatrix();
}
void Rocks()
{
	glPushMatrix();
		glTranslatef(-21.5,-0.5,20);
		for (int i = 0; i < 4; i++)
		{
		glTranslatef(2,0,0);
		glPushMatrix();
			for (int i = 0; i < 4; i++)
			{
				glTranslatef(0,0,-2);
				glPushMatrix();
					glTranslatef(3,0.5,-3);
					divide_triangle3(v[0], v[1], v[2], n);
				glPopMatrix();
			}
		glPopMatrix();
	}
	glPopMatrix();
}
//Water
void WaterSubdivision()
{
	glBegin(GL_QUADS);
		subdivideQuads(terrain[0],terrain[1],terrain[2],terrain[3],3);///////////////////////////////////////////////////
	glEnd();
}
void Water()
{
	glPushMatrix();
		glScalef(5,1,5);
		glRotatef(90,1,0,0);
		glTranslatef(-0.5,-0.5,0);
		Square();
	glPopMatrix();
}
//Triangles not used (not working)
void Triangles()
{
	glBegin(GL_TRIANGLE_STRIP);
		subdivideTriangles(v[0],v[1],v[2],3);///////////////////////////////////////////////////
	glEnd();
}

//Textures
void GroundTexture()
{
	if (Winter == false)
	{
		glTexImage2D(GL_TEXTURE_2D, 0, GL_RGB, imageGrass->width, 
		imageGrass->height, 0, GL_RGB, GL_UNSIGNED_BYTE, imageGrass->pixels);
	}
	else
	{
		glTexImage2D(GL_TEXTURE_2D, 0, GL_RGB, imageSnow->width, 
		imageSnow->height, 0, GL_RGB, GL_UNSIGNED_BYTE, imageSnow->pixels);
	}
	glBegin(GL_POLYGON);
		glTexCoord2f(0.0f, 0.0f);
		glNormal3f(0.0f, 3.0f, 0.0f);
		glVertex3f(-30.0f, 0.0f, 30.0f);
		glTexCoord2f(0.0f, 1.0f);
		glNormal3f(0.0f, 3.0f, 0.0f);
		glVertex3f(-30.0f, 0.0f, -30.0f);
		glTexCoord2f(1.0f, 1.0f);
		glNormal3f(0.0f, 3.0f, 0.0f);
		glVertex3f(30.0f, 0.0f, -30.0f);
		glTexCoord2f(1.0f, 0.0f);
		glNormal3f(0.0f, 3.0f, 0.0f);
		glVertex3f(30.0f, 0.0f, 30.0f);
	glEnd();
}
void Welcome()
{
	glTexImage2D(GL_TEXTURE_2D, 0, GL_RGB, imageWelcome->width, 
	imageWelcome->height, 0, GL_RGB, GL_UNSIGNED_BYTE, imageWelcome->pixels);

	glTranslatef(-0.75,1.3,12.65);
	glScalef(0.15,0.15,0.15);
	glBegin(GL_POLYGON);
		glNormal3f(0.0f, 0.0f, 1.0f);
		glTexCoord2f(0.0f, 0.0f);
		glVertex3f(0.0f, 0.0f, 0.0f);
		glTexCoord2f(0.0f, 1.0f);
		glVertex3f(0.0f, 2.5f, 0.0f);
		glTexCoord2f(1.0f, 1.0f);
		glVertex3f(10.0f, 2.5f, 0.0f);
		glTexCoord2f(1.0f, 0.0f);
		glVertex3f(10.0f, 0.0f, 0.0f);
	glEnd();
}
void Text()
{
	//Contains a list of all the controls and postioned at the start,
	//so the user will know the controls straight away.
	glTexImage2D(GL_TEXTURE_2D, 0, GL_RGB, imageText->width, 
	imageText->height, 0, GL_RGB, GL_UNSIGNED_BYTE, imageText->pixels);

	glTranslatef(-6,-6,0);
	glRotatef(30,0,1,0);
	glBegin(GL_QUADS);
		glNormal3f(0.0f, 0.0f, 1.0f);
		glTexCoord2f(0.0f, 0.0f);
		glVertex3f(0.0f, 0.0f, 0.0f);
		glTexCoord2f(0.0f, 1.0f);
		glVertex3f(0.0f, 7.0f, 0.0f);
		glTexCoord2f(1.0f, 1.0f);
		glVertex3f(4.0f, 7.0f, 0.0f);
		glTexCoord2f(1.0f, 0.0f);
		glVertex3f(4.0f, 0.0f, 0.0f);
	glEnd();
}

//Defining HUD
void ShowText2D(const char *MyText, int x, int y, float Red, float Green, float Blue, void *font)
{
	// Switch to 2D Ortho. projection
	glMatrixMode(GL_PROJECTION);
	glPushMatrix();				// Save Current Proj. Matrix
		glLoadIdentity();			// clear
		gluOrtho2D(0.0,400.0,0.0,300.0);	// New 2D Ortho Proj
		glMatrixMode(GL_MODELVIEW);
			glPushMatrix();				// Save Current ModelView Matrix
				glLoadIdentity();			// clear
				//Draw
				glColor3f(1,0,0);
				glRasterPos2i(x,y);
				while(*MyText)
				{
					glutBitmapCharacter(font, *MyText);
					++MyText;   // increment pointer to next char in the string.
				 }
				//End of Drawing
			glPopMatrix();				// restore ModelView
		glMatrixMode(GL_PROJECTION);
	glPopMatrix();				// Restore Proj. Matrix
	glMatrixMode(GL_MODELVIEW);	// Return to ModelView mode (good practice..0

}
void HUD()
{
	if (HideHUD == false)
	{
		GLvoid* g_pFont	= GLUT_BITMAP_HELVETICA_18; //GLUT_BITMAP_8_BY_13;

		glEnable(GL_COLOR_MATERIAL); //Allows HUD text to be coloured.

		if (rotateLight < 180)
		{
			ShowText2D("Day", 10, 10, 1, 0, 0, g_pFont);
		}
		else
		{
			ShowText2D("Night", 10, 10, 1, 0, 0, g_pFont);
		}
		ShowText2D("Controls: Move - W, A, S, D, E, C", 2, 290, 1, 0, 0, g_pFont);
		ShowText2D("Look  -  I , J , K, L  or use Mouse.", 28, 280, 1, 0, 0, g_pFont);
		ShowText2D("Animate rides - R", 28, 270, 1, 0, 0, g_pFont);
		ShowText2D("Simulate daytime - T", 28, 260, 1, 0, 0, g_pFont);
		ShowText2D("Right click mouse button to display menu.", 2, 250, 1, 0, 0, g_pFont);
		ShowText2D("Disable lighting - F. Enable lighting - O", 2, 240, 1, 0, 0, g_pFont);
		ShowText2D("Press Q to quit.", 2, 230, 1, 0, 0, g_pFont);
		if (Camera == true)
		{
			ShowText2D("Free Fly Camera", 350, 290, 1, 0, 0, g_pFont);
		}
		if (TopView == true)
		{
			ShowText2D("Top View", 350, 290, 1, 0, 0, g_pFont);
		}
		if (FrontView == true)
		{
			ShowText2D("Front View", 350, 290, 1, 0, 0, g_pFont);
		}
		if (BackView == true)
		{
			ShowText2D("Back View", 350, 290, 1, 0, 0, g_pFont);
		}
		if (LeftSideView == true)
		{
			ShowText2D("Left View", 350, 290, 1, 0, 0, g_pFont);
		}
		if (RightSideView == true)
		{
			ShowText2D("Right View", 350, 290, 1, 0, 0, g_pFont);
		}
		if (FerrisWheel == true)
		{
			ShowText2D("Ferris Wheel View", 350, 290, 1, 0, 0, g_pFont);
		}
		if (MerryGoRound == true)
		{
			ShowText2D("Merry-Go-Round View", 330, 290, 1, 0, 0, g_pFont);
		}
		if (TeaCups == true)
		{
			ShowText2D("Tea Cups View", 350, 290, 1, 0, 0, g_pFont);
		}

		glDisable(GL_COLOR_MATERIAL); //Disables material colour so lighting can take effect.
	}
}

//Defining Colours
void colourRed(GLfloat* mat_diffuse, GLfloat* mat_specular, GLfloat* mat_ambient)//Sets mat_diffuse RGB values to create a specific colour.
{
	//Every value set to appropriate, even if it was already that value e.g. changing from red to blue colours in display, 
	//the green value wouldn't have to be set, but I still set it because otherwise I would have to keep track of what the colour was before.

	mat_diffuse[0] = 1.0; //Sets first mat_diffuse element (red) to 1.
	mat_diffuse[1] = 0.0; //Sets second mat_diffuse element (green) to 0.
	mat_diffuse[2] = 0.0; //Sets third mat_diffuse element (blue) to 0.

	mat_specular[0] = 1.0;
	mat_specular[1] = 0.0;
	mat_specular[2] = 0.0;

	mat_ambient[0] = 1.0;
	mat_ambient[1] = 0.0;
	mat_ambient[2] = 0.0;

	//Updates material properties
	glMaterialfv(GL_FRONT, GL_DIFFUSE, mat_diffuse);
	glMaterialfv(GL_FRONT, GL_SPECULAR, mat_specular);
	glMaterialfv(GL_FRONT, GL_AMBIENT, mat_ambient);
}
void colourGreen(GLfloat* mat_diffuse, GLfloat* mat_specular, GLfloat* mat_ambient)//Sets mat_diffuse RGB values to create a specific colour.
{
	mat_diffuse[0] = 0.0;
	mat_diffuse[1] = 1.0;
	mat_diffuse[2] = 0.0;

	mat_specular[0] = 0.0;
	mat_specular[1] = 1.0;
	mat_specular[2] = 0.0;

	mat_ambient[0] = 0.0;
	mat_ambient[1] = 1.0;
	mat_ambient[2] = 0.0;

	//Updates material properties
	glMaterialfv(GL_FRONT, GL_DIFFUSE, mat_diffuse);
	glMaterialfv(GL_FRONT, GL_SPECULAR, mat_specular);
	glMaterialfv(GL_FRONT, GL_AMBIENT, mat_ambient);
}
void colourDarkGreen(GLfloat* mat_diffuse, GLfloat* mat_specular, GLfloat* mat_ambient)//Sets mat_diffuse RGB values to create a specific colour.
{
	mat_diffuse[0] = 0.0;
	mat_diffuse[1] = 0.4;
	mat_diffuse[2] = 0.0;

	mat_specular[0] = 0.0;
	mat_specular[1] = 0.0;
	mat_specular[2] = 0.0;

	mat_ambient[0] = 0.0;
	mat_ambient[1] = 0.4;
	mat_ambient[2] = 0.0;

	//Updates material properties
	glMaterialfv(GL_FRONT, GL_DIFFUSE, mat_diffuse);
	glMaterialfv(GL_FRONT, GL_SPECULAR, mat_specular);
	glMaterialfv(GL_FRONT, GL_AMBIENT, mat_ambient);
}
void colourBlue(GLfloat* mat_diffuse, GLfloat* mat_specular, GLfloat* mat_ambient)//Sets mat_diffuse RGB values to create a specific colour.
{
	mat_diffuse[0] = 0.0;
	mat_diffuse[1] = 0.0;
	mat_diffuse[2] = 1.0;

	mat_specular[0] = 0.0;
	mat_specular[1] = 0.0;
	mat_specular[2] = 1.0;

	mat_ambient[0] = 0.0;
	mat_ambient[1] = 0.0;
	mat_ambient[2] = 1.0;

	//Updates material properties
	glMaterialfv(GL_FRONT, GL_DIFFUSE, mat_diffuse);
	glMaterialfv(GL_FRONT, GL_SPECULAR, mat_specular);
	glMaterialfv(GL_FRONT, GL_AMBIENT, mat_ambient);
}
void colourYellow(GLfloat* mat_diffuse, GLfloat* mat_specular, GLfloat* mat_ambient) 
{
	mat_diffuse[0] = 1;
	mat_diffuse[1] = 1;
	mat_diffuse[2] = 0;

	mat_specular[0] = 1.0;
	mat_specular[1] = 1.0;
	mat_specular[2] = 0.0;

	mat_ambient[0] = 1.0;
	mat_ambient[1] = 1.0;
	mat_ambient[2] = 0.0;

	//Updates material properties
	glMaterialfv(GL_FRONT, GL_DIFFUSE, mat_diffuse);
	glMaterialfv(GL_FRONT, GL_SPECULAR, mat_specular);
	glMaterialfv(GL_FRONT, GL_AMBIENT, mat_ambient);
}
void colourGrey(GLfloat* mat_diffuse, GLfloat* mat_specular, GLfloat* mat_ambient)
{
	mat_diffuse[0] = 0.75;
	mat_diffuse[1] = 0.75;
	mat_diffuse[2] = 0.75;

	mat_specular[0] = 0.75;
	mat_specular[1] = 0.75;
	mat_specular[2] = 0.75;

	mat_ambient[0] = 0.75;
	mat_ambient[1] = 0.75;
	mat_ambient[2] = 0.75;

	//Updates material properties
	glMaterialfv(GL_FRONT, GL_DIFFUSE, mat_diffuse);
	glMaterialfv(GL_FRONT, GL_SPECULAR, mat_specular);
	glMaterialfv(GL_FRONT, GL_AMBIENT, mat_ambient);
}
void colourBrown(GLfloat* mat_diffuse, GLfloat* mat_specular, GLfloat* mat_ambient)
{
	mat_diffuse[0] = 0.545;
	mat_diffuse[1] = 0.2705;
	mat_diffuse[2] = 0.0745;

	//Wood isn't shiny so mat_specular is set to 0 (no shine).
	mat_specular[0] = 0;
	mat_specular[1] = 0;
	mat_specular[2] = 0;

	mat_ambient[0] = 0.545;
	mat_ambient[1] = 0.2705;
	mat_ambient[2] = 0.0745;

	//Updates material properties
	glMaterialfv(GL_FRONT, GL_DIFFUSE, mat_diffuse);
	glMaterialfv(GL_FRONT, GL_SPECULAR, mat_specular);
	glMaterialfv(GL_FRONT, GL_AMBIENT, mat_ambient);
}
void colourDarkGrey(GLfloat* mat_diffuse, GLfloat* mat_specular, GLfloat* mat_ambient)
{
	mat_diffuse[0] = 0.1;
	mat_diffuse[1] = 0.1;
	mat_diffuse[2] = 0.1;

	mat_specular[0] = 0.1;
	mat_specular[1] = 0.1;
	mat_specular[2] = 0.1;

	mat_ambient[0] = 0.1;
	mat_ambient[1] = 0.1;
	mat_ambient[2] = 0.1;

	//Updates material properties
	glMaterialfv(GL_FRONT, GL_DIFFUSE, mat_diffuse);
	glMaterialfv(GL_FRONT, GL_SPECULAR, mat_specular);
	glMaterialfv(GL_FRONT, GL_AMBIENT, mat_ambient);
}
void colourWhite(GLfloat* mat_diffuse, GLfloat* mat_specular, GLfloat* mat_ambient)
{
	mat_diffuse[0] = 1;
	mat_diffuse[1] = 1;
	mat_diffuse[2] = 1;

	mat_specular[0] = 1;
	mat_specular[1] = 1;
	mat_specular[2] = 1;

	mat_ambient[0] = 1;
	mat_ambient[1] = 1;
	mat_ambient[2] = 1;

	//Updates material properties
	glMaterialfv(GL_FRONT, GL_DIFFUSE, mat_diffuse);
	glMaterialfv(GL_FRONT, GL_SPECULAR, mat_specular);
	glMaterialfv(GL_FRONT, GL_AMBIENT, mat_ambient);
}
void colourBlack(GLfloat* mat_diffuse, GLfloat* mat_specular, GLfloat* mat_ambient)
{
	mat_diffuse[0] = 0;
	mat_diffuse[1] = 0;
	mat_diffuse[2] = 0;

	mat_specular[0] = 0.1;
	mat_specular[1] = 0.1;
	mat_specular[2] = 0.1;

	mat_ambient[0] = 0.1;
	mat_ambient[1] = 0.1;
	mat_ambient[2] = 0.1;

	//Updates material properties
	glMaterialfv(GL_FRONT, GL_DIFFUSE, mat_diffuse);
	glMaterialfv(GL_FRONT, GL_SPECULAR, mat_specular);
	glMaterialfv(GL_FRONT, GL_AMBIENT, mat_ambient);
}
void colourLightBlue(GLfloat* mat_diffuse, GLfloat* mat_specular, GLfloat* mat_ambient)
{
	mat_diffuse[0] = 0.22;
	mat_diffuse[1] = 0.345;
	mat_diffuse[2] = 0.475;

	mat_specular[0] = 0.22;
	mat_specular[1] = 0.345;
	mat_specular[2] = 0.475;

	mat_ambient[0] = 0.22;
	mat_ambient[1] = 0.345;
	mat_ambient[2] = 0.475;

	//Updates material properties
	glMaterialfv(GL_FRONT, GL_DIFFUSE, mat_diffuse);
	glMaterialfv(GL_FRONT, GL_SPECULAR, mat_specular);
	glMaterialfv(GL_FRONT, GL_AMBIENT, mat_ambient);
}

//Ride colour selectors
void FerrisWheelColourSelector(GLfloat* mat_diffuse, GLfloat* mat_specular, GLfloat* mat_ambient)
{
	if (FerrisWheelColour == 0)
		{
			//Calls the "colourRed" function, passes the mat_diffuse GLfloat and sets the Ferris Wheel to its default colour (red).
			colourRed(mat_diffuse, mat_specular, mat_ambient); 
		}
		if (FerrisWheelColour == 1)
		{
			//Calls the "colourRed" function, passes the mat_diffuse GLfloat and sets the object to red.
			colourRed(mat_diffuse, mat_specular, mat_ambient); 
		}
		if (FerrisWheelColour == 2)
		{
			//Calls the "colourGreen" function, passes the mat_diffuse GLfloat and sets the object to green.
			colourGreen(mat_diffuse, mat_specular, mat_ambient);
		}
		if (FerrisWheelColour == 3)
		{
			//Calls the "colourBlue" function, passes the mat_diffuse GLfloat and sets the object to blue.
			colourBlue(mat_diffuse, mat_specular, mat_ambient);
		}
}
void MerryGoRoundColourSelector(GLfloat* mat_diffuse, GLfloat* mat_specular, GLfloat* mat_ambient)
{
	if (MerryGoRoundColour == 0)
		{
			//Calls the "colourRed" function, passes the mat_diffuse GLfloat and sets the Merry-Go-Round to its default colour (blue).
			colourBlue(mat_diffuse, mat_specular, mat_ambient); 
		}
		if (MerryGoRoundColour == 1)
		{
			//Calls the "colourRed" function, passes the mat_diffuse GLfloat and sets the object's colour to red.
			colourRed(mat_diffuse, mat_specular, mat_ambient); 
		}
		if (MerryGoRoundColour == 2)
		{
			//Calls the "colourGreen" function, passes the mat_diffuse GLfloat and sets the object's colour to green.
			colourGreen(mat_diffuse, mat_specular, mat_ambient);
		}
		if (MerryGoRoundColour == 3)
		{
			//Calls the "colourBlue" function, passes the mat_diffuse GLfloat and sets the object's colour to blue.
			colourBlue(mat_diffuse, mat_specular, mat_ambient);
		}
}
void TeaCupsColourSelector(GLfloat* mat_diffuse, GLfloat* mat_specular, GLfloat* mat_ambient)
{
	if (TeaCupsColour == 0)
		{
			//Calls the "colourRed" function, passes the mat_diffuse GLfloat and sets the Teacups to its default colour (yellow).
			colourYellow(mat_diffuse, mat_specular, mat_ambient); 
		}
		if (TeaCupsColour == 1)
		{
			//Calls the "colourRed" function, passes the mat_diffuse GLfloat and sets the object's colour to red.
			colourRed(mat_diffuse, mat_specular, mat_ambient); 
		}
		if (TeaCupsColour == 2)
		{
			//Calls the "colourGreen" function, passes the mat_diffuse GLfloat and sets the object's colour to green.
			colourGreen(mat_diffuse, mat_specular, mat_ambient);
		}
		if (TeaCupsColour == 3)
		{
			//Calls the "colourBlue" function, passes the mat_diffuse GLfloat and sets the object's colour to blue.
			colourBlue(mat_diffuse, mat_specular, mat_ambient);
		}
}

//Objects with multiple colours
void Trees(GLfloat* mat_diffuse, GLfloat* mat_specular, GLfloat* mat_ambient)
{
	for (int i = 0; i < 4; i++)
	{
		glTranslatef(2,0,0);
		glPushMatrix();
			for (int i = 0; i < 4; i++)
			{
				glTranslatef(0,0,-2);
				glPushMatrix();
					glTranslatef(3,0.5,-3);
					if (Winter == false)
					{
						colourDarkGreen(mat_diffuse,mat_specular,mat_ambient);
					}
					else
					{
							colourWhite(mat_diffuse,mat_specular,mat_ambient);
					}
					glScalef(0.5,1,0.5);
					Cone();
					
					colourBrown(mat_diffuse,mat_specular,mat_ambient);
					glScalef(0.1,0.5,0.1);
					glTranslatef(0,-0.4,0);
					Tube();
				glPopMatrix();
			}
		glPopMatrix();
	}
}
void LightBulbs(GLfloat* mat_Yellow_emission, GLfloat* mat_Brown_emission)
{
	glMaterialfv(GL_FRONT, GL_EMISSION, mat_Yellow_emission);
	glPushMatrix();
		glTranslatef(34.79,4.92,1.7);
		glutSolidSphere(0.25,10,10);
		glTranslatef(-24.58,0,0);
		glutSolidSphere(0.25,10,10);
		glTranslatef(10,-3.92,-3);
		glScalef(0.5,0.5,0.5);
		glutSolidSphere(0.25,10,10);
		glMaterialfv(GL_FRONT, GL_EMISSION, mat_Brown_emission);
		glScalef(0.25,0.25,0.25);
		glRotatef(30,0,1,0);
		glTranslatef(0,0,-1);
		Cube();
	glPopMatrix();
}
void Merry_Go_Round(GLfloat* mat_diffuse, GLfloat* mat_specular, GLfloat* mat_ambient)
{
	MerryGoRoundColourSelector(mat_diffuse, mat_specular, mat_ambient);
	glTranslatef(-6,-0.25,0);
	//Centre
	glPushMatrix();
		glTranslatef(0,-0.625,0);
		glScalef(1,1.1,1);
		Cylinder();
	glPopMatrix();

	//Top
	glPushMatrix();
		glTranslatef(0,0.5,0);
		glScalef(2.5,0.25,2.5);
		Tube();
		glTranslatef(0,-1,0);
		Circle();
	glPopMatrix();

	if (Winter == true)
	{
		colourWhite(mat_diffuse,mat_specular,mat_ambient);
	}
	glPushMatrix();
		glTranslatef(0,0.5,0);
		glScalef(2.5,1,2.5);
		Cone();
	glPopMatrix();

	//Base
	glPushMatrix();
		glTranslatef(0,-1.75,0);
		glScalef(2.5,0.25,2.5);
		Circle();
	glPopMatrix();

	MerryGoRoundColourSelector(mat_diffuse, mat_specular, mat_ambient);
	glPushMatrix();
		glTranslatef(0,-2,0);
		glScalef(2.5,0.25,2.5);
		Tube();
	glPopMatrix();

	//Horse
	glPushMatrix();
		glTranslatef(0,0.6,0);
		glTranslatef(0,horseHeight,0);
		glRotatef(rotateMerryGoRound,0,1,0);
		glScalef(1.5,1,1.5);
		Horses();
	glPopMatrix();

	//Poles
	glPushMatrix();
		glTranslatef(0,-0.675,0);
		glRotatef(22.5,0,1,0);
		glRotatef(rotateMerryGoRound,0,1,0);
		glScalef(2,1.1,2);
		Poles();
	glPopMatrix();
}
void Teacups(GLfloat* mat_diffuse, GLfloat* mat_specular, GLfloat* mat_ambient)
{
	//Teapot
	glPushMatrix();
		glRotatef(rotateTeapot,0,1,0);
		glutSolidTeapot(1);
	glPopMatrix();

	//Base
	glPushMatrix();
		glTranslatef(0,-0.95,0);
		glScalef(3,0.25,3);
		Tube();
		glTranslatef(0,1,0);
		if (Winter == true)
		{
			colourWhite(mat_diffuse,mat_specular,mat_ambient);
		}
		Circle();
	glPopMatrix();
	
	TeaCupsColourSelector(mat_diffuse, mat_specular, mat_ambient);
	glPushMatrix();
		glTranslatef(0,-0.45,0);
		glRotatef(rotateTeacup,0,1,0);
		glScalef(2.25,1,2.25);
		Teacup();
	glPopMatrix();
}

//Defining Lights
void Light0()
{
	GLfloat light_ambient[]= {0.0, 0.0, 0.0, 1.0}; //Black
	GLfloat light_diffuse_White[]= {1, 1, 1, 1.0}; //White
	GLfloat light_diffuse_Yellow[]= {1, 0.9, 0.2, 1.0}; //Yellow
	GLfloat light_specular_White[]= {1.0, 1.0, 1.0, 1.0}; //White
	GLfloat light_specular_Yellow[]= {1.0, 0.9, 0.2, 1.0}; //Yellow
	GLfloat light_position[] = {35*Cos(rotateLight), 35*Sin(rotateLight), 30, 0};

	glLightfv(GL_LIGHT0, GL_POSITION, light_position);
	//glLightfv(GL_LIGHT0, GL_AMBIENT, light_ambient);
	//Sun rise
	if (rotateLight < 30)
	{
		glLightfv(GL_LIGHT0, GL_DIFFUSE, light_diffuse_Yellow);
		glLightfv(GL_LIGHT0, GL_SPECULAR, light_specular_Yellow);
	}
	//Daytime
	if (rotateLight > 30 && 150)
	{
		glLightfv(GL_LIGHT0, GL_DIFFUSE, light_diffuse_White);
		glLightfv(GL_LIGHT0, GL_SPECULAR, light_specular_White);
	}
	//Sun set
	if (rotateLight > 150)
	{
		glLightfv(GL_LIGHT0, GL_DIFFUSE, light_diffuse_Yellow);
		glLightfv(GL_LIGHT0, GL_SPECULAR, light_specular_Yellow);
	}
}
void Light1()
{
	GLfloat light1_ambient[] = { 0.2, 0.2, 0.2, 1.0 };
	GLfloat light1_diffuse[] = { 1.0, 1.0, 1.0, 1.0 };
	GLfloat light1_specular[] = { 1.0, 1.0, 1.0, 1.0 };
	GLfloat light1_position[] = { -5, 1, 5, 1.0 };
	GLfloat spot1_direction[] = { 5, 0, -5};

	//glLightfv(GL_LIGHT1, GL_AMBIENT, light1_ambient);
	glLightfv(GL_LIGHT1, GL_DIFFUSE, light1_diffuse);
	glLightfv(GL_LIGHT1, GL_SPECULAR, light1_specular);
	glLightfv(GL_LIGHT1, GL_POSITION, light1_position);
	//glLightf(GL_LIGHT1, GL_CONSTANT_ATTENUATION, 1.5);
	//glLightf(GL_LIGHT1, GL_LINEAR_ATTENUATION, 0.5);
	//glLightf(GL_LIGHT1, GL_QUADRATIC_ATTENUATION, 0.2);

	glLightf(GL_LIGHT1, GL_SPOT_CUTOFF, 45); //Act like spotlights i.e. cone like coverage.
	glLightfv(GL_LIGHT1, GL_SPOT_DIRECTION, spot1_direction);
	glLightf(GL_LIGHT1, GL_SPOT_EXPONENT, 4.0);
}
void Light2()
{
	GLfloat light2_ambient[] = { 0.2, 0.2, 0.2, 1.0 };
	GLfloat light2_diffuse[] = { 1.0, 1.0, 1.0, 1.0 };
	GLfloat light2_specular[] = { 1.0, 1.0, 1.0, 1.0 };
	GLfloat light2_position[] = { 70, 1, 30, 1.0 };
	GLfloat spot2_direction[] = { 0.1, 0, -5};

	//glLightfv(GL_LIGHT2, GL_AMBIENT, light2_ambient);
	glLightfv(GL_LIGHT2, GL_DIFFUSE, light2_diffuse);
	glLightfv(GL_LIGHT2, GL_SPECULAR, light2_specular);
	glLightfv(GL_LIGHT2, GL_POSITION, light2_position);
	//glLightf(GL_LIGHT2, GL_CONSTANT_ATTENUATION, 1.5);
	//glLightf(GL_LIGHT2, GL_LINEAR_ATTENUATION, 0.5);
	//glLightf(GL_LIGHT2, GL_QUADRATIC_ATTENUATION, 0.2);

	glLightf(GL_LIGHT2, GL_SPOT_CUTOFF, 45); //Act like spotlights i.e. cone like coverage.
	glLightfv(GL_LIGHT2, GL_SPOT_DIRECTION, spot2_direction);
	glLightf(GL_LIGHT2, GL_SPOT_EXPONENT, 4.0);
}
void Light3()
{
	GLfloat light3_ambient[] = { 0.2, 0.2, 0.2, 1.0 };
	GLfloat light3_diffuse[] = { 1.0, 1.0, 1.0, 1.0 };
	GLfloat light3_specular[] = { 1.0, 1.0, 1.0, 1.0 };
	GLfloat light3_position[] = { 1, 5, 2, 1.0 };
	GLfloat spot3_direction[] = { 1, 0, 1};

	//glLightfv(GL_LIGHT3, GL_AMBIENT, light3_ambient);
	glLightfv(GL_LIGHT3, GL_DIFFUSE, light3_diffuse);
	glLightfv(GL_LIGHT3, GL_SPECULAR, light3_specular);
	glLightfv(GL_LIGHT3, GL_POSITION, light3_position);
	glLightf(GL_LIGHT3, GL_CONSTANT_ATTENUATION, 0.25);
	glLightf(GL_LIGHT3, GL_LINEAR_ATTENUATION, 0.25);
	glLightf(GL_LIGHT3, GL_QUADRATIC_ATTENUATION, 0.1);

	glLightf(GL_LIGHT3, GL_SPOT_CUTOFF, 180); //Act like lamps i.e. 360 coverage.
	glLightfv(GL_LIGHT3, GL_SPOT_DIRECTION, spot3_direction);
	glLightf(GL_LIGHT3, GL_SPOT_EXPONENT, 4.0);
}

//Picking
void FerrisWheelPicker(GLenum mode)
{
	glPushMatrix();
		glTranslatef(0,0,-7);
		if (FerrisWheelClicked == true)
		{
			glMaterialf(GL_FRONT, GL_DIFFUSE, (1,1,1,1));
			rotateFerrisWheel += 1;
			rotateSupportBeam += 1;
			rotateCarriage -= 1;
		}
		Ferris_Wheel();
	glPopMatrix();
}
void MerryGoRoundPicker(GLenum mode, GLfloat* mat_diffuse, GLfloat* mat_specular, GLfloat* mat_ambient)
{
	glPushMatrix();
		if (MerryGoRoundClicked == true)
		{
			glMaterialf(GL_FRONT, GL_DIFFUSE, (1,1,1,1));
			rotateMerryGoRound += 0.75;
			HorseAnimation();
		}
		Merry_Go_Round(mat_diffuse, mat_specular, mat_ambient);
	glPopMatrix();
}
void TeaCupsPicker(GLenum mode, GLfloat* mat_diffuse, GLfloat* mat_specular, GLfloat* mat_ambient)
{
	glPushMatrix();
		if (TeaCupsClicked == true)
		{
			glMaterialf(GL_FRONT, GL_DIFFUSE, (1,1,1,1));
			rotateTeapot += 1;
			rotateTeacup -= 0.5;
			rotateTeacups += 2;
		}
		Teacups(mat_diffuse, mat_specular, mat_ambient);
	glPopMatrix();
}
void SunMoonPicker(GLenum mode, GLfloat mat_Grey_emission[], GLfloat mat_Yellow_emission[])
{
	glPushMatrix();
		if (SunMoonClicked == true)
		{
			if (rotateLight < 360)
			{
				rotateLight += 0.25;
				rotateLightMoon += 0.25;
			}
			else
			{
				rotateLight = 0;
				rotateLightMoon = 180;
			}
		}
		//Not using (GL_EMISSION) for the sun and the moon means that the side that is facing the scene will be black,
		//because a directional light has an infinite beam of light, so it can’t be positioned in front of the sun and the moon.
	glMaterialfv(GL_FRONT, GL_EMISSION, mat_Yellow_emission);
	Sun();
	glMaterialfv(GL_FRONT, GL_EMISSION, mat_Grey_emission);
	Moon();
	glPopMatrix();
}

void display()
{
	///////////////////////////////////----Lighting and materials----///////////////////////////////////
	
	//Material properties
	GLfloat mat_specular[] = {0,0,0,0};
	GLfloat mat_diffuse[] = {0,0,0,0};
	GLfloat mat_ambient[] = {0,0,0,0};
	GLfloat mat_shininess[] = { 10 };

	//Material - emission
	GLfloat mat_Grey_emission[]= {0.2,0.2,0.2,1}; //Grey
	GLfloat mat_Yellow_emission[]= {1,1,0,0}; //Yellow
	GLfloat mat_Brown_emission[]= {0.545,0.2705,0.0745,0}; //Brown
	GLfloat mat_White_emission[]= {1,1,1,1}; //White
	GLfloat no_mat_emission[]= {0,0,0,0}; //Black (off)
	
	//Setting material properties
	glMaterialfv(GL_FRONT, GL_SPECULAR, mat_specular);
	glMaterialfv(GL_FRONT, GL_SHININESS, mat_shininess);
	glMaterialfv(GL_FRONT, GL_DIFFUSE, mat_diffuse);
	glMaterialfv(GL_FRONT, GL_AMBIENT, mat_ambient);
	//glMaterialfv(GL_FRONT, GL_SHININESS, mat_shininess);

	//Initialising lights
	Light0();
	Light1();
	Light2();
	Light3();

	//Setting lights and clear (sky) colour for day and night.
	//Day
	if (rotateLight < 180)
	{
		if (Fog == false)
		{
			glClearColor(0.53,0.8,1.0,1); //Clear colour set to blue to simulate the sky.
		}
		else
		{
			glClearColor(0.75,0.75,0.75,1); //Clear colour set to grey to simulate an overcast day.
		}
		glEnable(GL_LIGHT0);
		glDisable(GL_LIGHT1);
		glDisable(GL_LIGHT2);
		glDisable(GL_LIGHT3);
	}
	//Night
	else
	{
		glClearColor(0,0,0,0); //Clear colour set to black to simulate night.
		glEnable(GL_LIGHT1);
		glEnable(GL_LIGHT2);
		glEnable(GL_LIGHT3);
		glDisable(GL_LIGHT0);
	}

	///////////////////////////////////----Modelling----///////////////////////////////////

	// Model/View Transformation
	glMatrixMode(GL_MODELVIEW); // Set matrix mode
	glLoadIdentity(); 		  // Clear
	glClear(GL_COLOR_BUFFER_BIT|GL_DEPTH_BUFFER_BIT);

	//Set camera views
	CameraView();

	//Sun and Moon
	glPushName(4);
		SunMoonPicker(GL_RENDER, mat_Grey_emission, mat_Yellow_emission);
	glPopName();
	glMaterialfv(GL_FRONT, GL_EMISSION, no_mat_emission);

	glTranslatef(0,1.8,-15.2); //Translate all objects so camera is at the welcome banner.

	//Ferris Wheel
	FerrisWheelColourSelector(mat_diffuse, mat_specular, mat_ambient);
	glPushName(1);
		FerrisWheelPicker(GL_RENDER);
	glPopName();

	//Merry-Go-Round
	glPushName(2);
		MerryGoRoundPicker(GL_RENDER, mat_diffuse, mat_specular, mat_ambient);
		glTranslatef(-6,-0.25,0);
	glPopName();
	
	//Paths
	if (Winter == true)
	{
		colourWhite(mat_diffuse, mat_specular, mat_ambient); //Sets the path's colour to white to suggest snow is covering them.
	}
	else
	{
		colourGrey(mat_diffuse, mat_specular, mat_ambient); //Sets the path's colour to grey, its default.
	}
	glTranslatef(6,-2.3,0);
	Paths();

	//Ferris Wheel path.
	glPushMatrix();
		glTranslatef(-2.75,0,-3.5);
		glColor3f(0,0,0);
		SquarePath();
	glPopMatrix();

	//Merr Go Round path.
	glPushMatrix();
		glTranslatef(-6,0.06,0);
		glColor3f(0,0,0);
		glRotatef(270,1,0,0);
		gluDisk(gluNewQuadric(),3,4,20,5);
	glPopMatrix();

	//Teacups
	glPushMatrix();
		TeaCupsColourSelector(mat_diffuse, mat_specular, mat_ambient);
		glColor3f(0,0,0);
		glTranslatef(6,1.25,0);
		glPushName(3);
			TeaCupsPicker(GL_RENDER, mat_diffuse, mat_specular, mat_ambient);
		glPopName();
	glPopMatrix();

	//Merry-Go_Round decoration.
	colourYellow(mat_diffuse, mat_specular, mat_ambient);
	Decoration();

	//Benches
	colourBrown(mat_diffuse, mat_specular, mat_ambient);
	glPushMatrix();
		glTranslatef(-0.75,-0.25,-2);
		glScalef(0.25,0.25,0.35);
		Bench();
		glTranslatef(0,0,11);
		Bench();
		glTranslatef(6,0,0);
		glRotatef(180,0,1,0);
		Bench();
		glTranslatef(0,0,11);
		Bench();
	glPopMatrix();

	//Tables
	glPushMatrix();
		MultipleTables();
	glPopMatrix();

	//Fences
	glPushMatrix();
		glTranslatef(0,3,0);
		Fence();
	glPopMatrix();

	//Rocks
	Rocks();
	Trees(mat_diffuse,mat_specular,mat_ambient);

	//Texture signs
	WelcomeSign();
	glPushMatrix();
		glTranslatef(-2,0,1);
		TextSign();
	glPopMatrix();

	//Mountains
	colourGrey(mat_diffuse, mat_specular, mat_ambient);
	MountainRange();

	//Spotlights
	glMaterialfv(GL_FRONT, GL_EMISSION, mat_Grey_emission);
	glPushMatrix();
		glTranslatef(35,0,2);
		SpotLight();
		glTranslatef(-25,0,0);
		glRotatef(-70,0,1,0);
		SpotLight();
	glPopMatrix();
	//Spotlight bulbs
	LightBulbs(mat_Yellow_emission, mat_Brown_emission);

	//Stars
	if (rotateLight > 180) //If it's day time then don't display stars.
	{
		glMaterialfv(GL_FRONT, GL_EMISSION, mat_White_emission);
		Stars();
	}
	glMaterialfv(GL_FRONT, GL_EMISSION, no_mat_emission);

	//Rubbish bins and lamposts
	colourBlack(mat_diffuse, mat_specular, mat_ambient);
	RubbishBins();
	LampPosts();
	glMaterialfv(GL_FRONT, GL_EMISSION, mat_Yellow_emission);
	LampPostLights();
	glMaterialfv(GL_FRONT, GL_EMISSION, no_mat_emission);

	//Swimming Pool
	glPushMatrix();
		colourWhite(mat_diffuse, mat_specular, mat_ambient);
		glTranslatef(16,0.3,-10);
		SwimmingPool();
		colourLightBlue(mat_diffuse, mat_specular, mat_ambient);
		glTranslatef(0,0,2.65);
		Water();
		glTranslatef(0,-0.65,0);
		WaterSubdivision();
		//Triangles();
	glPopMatrix();

	///////////////////////////////////----Texturing----///////////////////////////////////
	colourWhite(mat_diffuse, mat_specular, mat_ambient);
	glTranslatef(22,0,-14);
	glEnable(GL_TEXTURE_2D);	

	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);

	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_REPEAT);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_REPEAT);

	//MipsMaps
	//glTexParameterf( GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR_MIPMAP_LINEAR );
    //glTexParameterf( GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR_MIPMAP_LINEAR );
	//gluBuild2DMipmaps( GL_TEXTURE_2D, 3, 24, 24, GL_RGB, GL_UNSIGNED_BYTE, imageGrass ); 

	//glTexEnvf(GL_TEXTURE_ENV, GL_TEXTURE_ENV_MODE, GL_REPLACE); 
	glTexEnvi(GL_TEXTURE_ENV,GL_TEXTURE_ENV_MODE,GL_MODULATE); //Enable lighting to affect textures.
	
	GroundTexture();
	glTranslatef(0,0,-0.5);
	Welcome();
	glTranslatef(1,-2,2.1);
	Text();

	glDisable(GL_TEXTURE_2D);

	//HUD
	colourRed(mat_diffuse, mat_specular, mat_ambient);
	HUD();

	glutSwapBuffers(); //Swap the buffers

	glFlush();
}

void reshape (int w, int h)
{
	glViewport (0, 0, (GLsizei) w, (GLsizei) h); // Viewport mapping
	//Projection transformation
	glMatrixMode(GL_PROJECTION); // Set matrix mode	
	glOrtho(0, (GLsizei) w, 0, (GLsizei) h, -1, 1);	
	glLoadIdentity(); // Clear
	//gluPerspective(110, 1, 0.1, 100); 	// define and adjust projection
	aspectRatio =  (GLfloat)w / (GLfloat)h;
	gluPerspective (60, aspectRatio, 1.0, 200.0); //set the perspective (angle of sight, width, height, depth)
	glMatrixMode(GL_MODELVIEW);	// reset the matrix mode
}

//Merry-Go-Round horse animation
void HorseAnimation()
{
	
	if (MaxHeight == false)
	{
		horseHeight += 0.0125;
		if (horseHeight > -0.3)
		{
			MaxHeight = true;
		}
	}
	if (MaxHeight == true)
	{
		horseHeight -= 0.0125;
		if (horseHeight < -1.025)
		{
			MaxHeight = false;
		}
	}
}

//Interaction
void keyboard (unsigned char key, int x, int y) 
{
    switch(key) {
	
	//Move

	//Forward
	case 'w':
    float xrotrad, yrotrad, zrotrad;
    yrotrad = (yrot / 180 * 3.141592654f);
    xrotrad = (xrot / 180 * 3.141592654f); 
    xpos += float(sin(yrotrad));
    zpos -= float(cos(yrotrad));
    ypos -= float(sin(xrotrad));
    break;

	//Back
	case 's':
    xrotrad, yrotrad;
    yrotrad = (yrot / 180 * 3.141592654f);
    xrotrad = (xrot / 180 * 3.141592654f); 
    xpos -= float(sin(yrotrad));
    zpos += float(cos(yrotrad));
    ypos += float(sin(xrotrad));
    break;

	//Left
	case 'a':
		yrotrad;
		yrotrad = (yrot / 180 * 3.141592654f);
		xpos -= float(cos(yrotrad)) * 0.4;
		zpos -= float(sin(yrotrad)) * 0.4;
	break;

	//Right
	case 'd':
		yrotrad;
		yrotrad = (yrot / 180 * 3.141592654f);
		xpos += float(cos(yrotrad)) * 0.4;
		zpos += float(sin(yrotrad)) * 0.4;
	break;

	//Up
	case 'e':
		yrotrad;
		zrotrad = (zrot / 180 * 3.141592654f);
		ypos += float(cos(zrotrad)) * 0.2;
		zpos += float(sin(zrotrad)) * 0.2;
	break;

	//Down
	case 'c':
		zrotrad;
		zrotrad = (zrot / 180 * 3.141592654f);
		ypos -= float(cos(zrotrad)) * 0.2;
		zpos -= float(sin(zrotrad)) * 0.2;
	break;

	//Look

	//Down
	case 'k':
		xrot += 5;
		if (xrot >360) {xrot -= 360;}
    break;

	//Up
	case 'i':
		xrot -= 5;
		if (xrot < -360) {xrot += 360;}
    break;

	//Right
	case 'l':
		yrot += 5;
		if (yrot >360) 
		{yrot -= 360;}
    break;

	//Left
	case 'j':
		yrot -= 5;
		if (yrot < -360) {yrot += 360;}
    break;

	//Animation
	case 'r':
		rotateFerrisWheel += 2;
		rotateSupportBeam += 2;
		rotateCarriage -= 2;
		rotateMerryGoRound += 2;
		HorseAnimation();
		rotateTeapot += 2;
		rotateTeacup -= 1;
		rotateTeacups += 2;
	break;

	case 't':
		if (rotateLight < 360)
		{
			rotateLight += 1;
			rotateLightMoon += 1;
		}
		else
		{
			rotateLight = 0;
			rotateLightMoon = 180;
		}
	break;

	case 'o':
		glEnable(GL_LIGHTING);
	break;

	case 'f':
		glDisable(GL_LIGHTING);
	break;

	case '1':
		if (FerrisWheelColourB == true || FerrisWheel == true)
		{
			FerrisWheelColour = 1;
		}
		if (MerryGoRoundColourB == true || MerryGoRound == true)
		{
			MerryGoRoundColour = 1;
		}
		if (TeaCupsColourB == true || TeaCups == true)
		{
			TeaCupsColour = 1;
		}
	break;

	case '2':
		if (FerrisWheelColourB == true || FerrisWheel == true)
		{
			FerrisWheelColour = 2;
		}
		if (MerryGoRoundColourB == true || MerryGoRound == true)
		{
			MerryGoRoundColour = 2;
		}
		if (TeaCupsColourB == true || TeaCups == true)
		{
			TeaCupsColour = 2;
		}
	break;

	case '3':
		if (FerrisWheelColourB == true || FerrisWheel == true)
		{
			FerrisWheelColour = 3;
		}
		if (MerryGoRoundColourB == true || MerryGoRound == true)
		{
			MerryGoRoundColour = 3;
		}
		if (TeaCupsColourB == true || TeaCups == true)
		{
			TeaCupsColour = 3;
		}
	break;

	//Quit
	case 'q':
		exit(1);
		//glutLeaveGameMode();
	break;

    if (key==27)
    {
	 exit(0);
    }
	}
}
void HandleSelectedMenuItem(int id)
{
  switch (id)
    {
		case 1: //Camera
			Camera = true;
			TopView = false;
			FrontView = false;
			BackView = false;
			RightSideView = false;
			LeftSideView = false;	
			FerrisWheel = false;
			MerryGoRound = false;
			TeaCups = false;
		break; 
		case 2: //Top view
			Camera = false;
			FrontView = false;
			BackView = false;
			RightSideView = false;
			LeftSideView = false;	
			TopView = true;
			FerrisWheel = false;
			MerryGoRound = false;
			TeaCups = false;
		break; 
		case 3: //Front view
			Camera = false;
			TopView = false;
			BackView = false;
			RightSideView = false;
			LeftSideView = false;	
			FrontView = true;
			FerrisWheel = false;
			MerryGoRound = false;
			TeaCups = false;
		break; 
		case 4: //Back view
			Camera = false;
			TopView = false;
			FrontView = false;
			RightSideView = false;
			LeftSideView = false;	
			BackView = true;
			FerrisWheel = false;
			MerryGoRound = false;
			TeaCups = false;
		break;
		case 5: //Right side view
			Camera = false;
			TopView = false;
			FrontView = false;
			BackView = false;
			LeftSideView = false;	
			RightSideView = true;
			FerrisWheel = false;
			MerryGoRound = false;
			TeaCups = false;
		break;
		case 6: //Left side view
			Camera = false;
			TopView = false;
			FrontView = false;
			BackView = false;
			RightSideView = false;	
			LeftSideView = true;
			FerrisWheel = false;
			MerryGoRound = false;
			TeaCups = false;
		break;
		case 7: //Ferris Wheel view
			Camera = false;
			TopView = false;
			FrontView = false;
			BackView = false;
			RightSideView = false;	
			LeftSideView = false;
			FerrisWheel = true;
			MerryGoRound = false;
			TeaCups = false;
		break;
		case 8: //Merry Go Round view
			Camera = false;
			TopView = false;
			FrontView = false;
			BackView = false;
			RightSideView = false;	
			LeftSideView = false;
			FerrisWheel = false;
			MerryGoRound = true;
			TeaCups = false;
		break;
		case 9: //Teacups view
			Camera = false;
			TopView = false;
			FrontView = false;
			BackView = false;
			RightSideView = false;	
			LeftSideView = false;
			FerrisWheel = false;
			MerryGoRound = false;
			TeaCups = true;
		break;

		case 10: //Wire frame mode
			glPolygonMode(GL_FRONT_AND_BACK, GL_LINE);
		break;
		case 11: //Solid fill mode
			glPolygonMode(GL_FRONT_AND_BACK, GL_FILL);
		break;

		case 12: //Winter Wonderland enabled
			Winter = true;
			//snowing = true;
		break; 
		case 13: //Winter Wonderland disabled
			Winter = false;
			//snowing = false;
		break; 

		case 14: //Camera controlled by mouse - enabled.
			CameraMouse = true;
		break; 
		case 15: //Camera controlled by mouse - disabled.
			CameraMouse = false;
		break; 

		case 16: //Hide HUD
			HideHUD = true;
		break; 
		case 17: //Show HUD
			HideHUD = false;
		break; 

		case 18: //Mid day
			rotateLight = 90;
			rotateLightMoon = 270;
		break; 
		case 19: //Mid night
			rotateLight = 270;
			rotateLightMoon = 90;
		break; 

		case 20: //Dawn
			rotateLight = 0;
			rotateLightMoon = 180;
		break; 

		case 21: //Enable Fog
			glEnable (GL_FOG); //Enable fog
			Density = 0.3;
			glFogi (GL_FOG_MODE, GL_EXP2); //set the fog mode to GL_EXP2
			glFogfv (GL_FOG_COLOR, fogColor); //set the fog color to our color chosen above
			glFogf (GL_FOG_DENSITY, Density); //set the density to the value above
			glHint (GL_FOG_HINT, GL_NICEST); // set the fog to look the nicest, may slow down on older cards
			Fog = true;
		break;

		case 22: //Enable Mist
			glEnable (GL_FOG); //Enable fog
			Density = 0.1;
			glFogi (GL_FOG_MODE, GL_EXP2); //set the fog mode to GL_EXP2
			glFogfv (GL_FOG_COLOR, fogColor); //set the fog color to our color chosen above
			glFogf (GL_FOG_DENSITY, Density); //set the density to the value above
			glHint (GL_FOG_HINT, GL_NICEST); // set the fog to look the nicest, may slow down on older cards
			Fog = true;
		break;

		case 23: //Disable fog and mist (clear day)
			glDisable (GL_FOG); //Disable fog
			Fog = false;
		break;

		case 24: //Reset ride colours to default
			FerrisWheelColour = 0;
			MerryGoRoundColour = 0;
			TeaCupsColour = 0;
		break; 

		case 25: //Turn off rides and stop sun and moon
			FerrisWheelClicked = false;
			MerryGoRoundClicked = false;
			TeaCupsClicked = false;
			SunMoonClicked = false;
		break; 

		case 26: //Close program
			exit(1); //Close program
		break; 
    }
   // Almost any menu selection requires a redraw
  glutPostRedisplay();
}
void Menu()
{
	int menu;
	menu = glutCreateMenu(HandleSelectedMenuItem); //Create & define handler
	glutAddMenuEntry ("Free Fly Camera", 1);
	glutAddMenuEntry ("Top View", 2);
	glutAddMenuEntry ("Front View", 3);
	glutAddMenuEntry ("Back View", 4);
	glutAddMenuEntry ("Right Side View", 5);
	glutAddMenuEntry ("Left Side View", 6);
	glutAddMenuEntry ("Ferris Wheel View", 7);
	glutAddMenuEntry ("Merry Go Round View", 8);
	glutAddMenuEntry ("Tea Cups View", 9);
	glutAddMenuEntry ("Wire Frame Mode", 10);
	glutAddMenuEntry ("Solid Fill Mode", 11);
	glutAddMenuEntry ("Winter wonderland", 12);
	glutAddMenuEntry ("Summer", 13);
	glutAddMenuEntry ("Mouse Camera", 14);
	glutAddMenuEntry ("Keyboard Camera", 15);
	glutAddMenuEntry ("Hide HUD", 16);
	glutAddMenuEntry ("Show HUD", 17);
	glutAddMenuEntry ("Mid Day", 18);
	glutAddMenuEntry ("Mid Night", 19);
	glutAddMenuEntry ("Dawn", 20);
	glutAddMenuEntry ("Foggy Day", 21);
	glutAddMenuEntry ("Misty Day", 22);
	glutAddMenuEntry ("Clear Day", 23);
	glutAddMenuEntry ("Reset colours", 24);
	glutAddMenuEntry ("Stop animation", 25);
	glutAddMenuEntry ("Exit", 26);
	glutAttachMenu (GLUT_RIGHT_BUTTON); // Attach menu to Right-Button of the mouse
}

//Picking
void processHits (GLint hits, GLuint buffer[])
{
	GLuint i;
	GLuint *ptr;

   ptr = (GLuint *) buffer;

   for (i = 0; i < hits; i++) 
	{
		ptr+=3;
		if (*ptr == 1)
		{
			FerrisWheelClicked = true;
			FerrisWheelColourB = true;
			MerryGoRoundColourB = false;
			TeaCupsColourB = false;
		}
		if (*ptr == 2)
		{
			MerryGoRoundClicked = true;
			FerrisWheelColourB = false;
			MerryGoRoundColourB = true;
			TeaCupsColourB = false;
		}
		if (*ptr == 3)
		{
			TeaCupsClicked = true;
			FerrisWheelColourB = false;
			MerryGoRoundColourB = false;
			TeaCupsColourB = true;
		}
		if (*ptr == 4)
		{
			SunMoonClicked = true;
		}
	}
}
void gl_select(int button, int state, int x, int y)
 {
 	GLuint buff[64] = {0};
 	GLint hits;
	GLint viewport[4];
 
	if (button != GLUT_LEFT_BUTTON || state != GLUT_DOWN)
      return;
   glGetIntegerv(GL_VIEWPORT, viewport);

 	glSelectBuffer(64, buff);

 	glMatrixMode(GL_PROJECTION);
 	glPushMatrix();
	 	glRenderMode(GL_SELECT);
 		glLoadIdentity();
		glInitNames();
 		gluPickMatrix((GLdouble) x, (GLdouble) (viewport[3] - y), 5.0, 5.0, viewport);
 		gluPerspective(60, aspectRatio, 0.0001, 1000.0);
 		glMatrixMode(GL_MODELVIEW);
		display();
		hits = glRenderMode(GL_RENDER);
 		glMatrixMode(GL_PROJECTION);
 	glPopMatrix();
	glFlush();

 	processHits(hits, buff); 
 	glMatrixMode(GL_MODELVIEW);
 }

int main(int argc, char** argv) 
{
	glutInit(&argc, argv);
	glutInitWindowSize(1250,600); 
	glutInitDisplayMode (GLUT_DOUBLE | GLUT_RGB | GLUT_DEPTH); //Set the display to Double buffer, with depth
	glutCreateWindow("Theme park by Richard Lannigan"); 
	init();

	//Fullscreen mode (Not Working)
	//glutGameModeString( "1024×768:32@75" );
	//glutEnterGameMode();

	glMatrixMode(GL_PROJECTION);
	glLoadIdentity();
	glMatrixMode(GL_MODELVIEW);
	glLoadIdentity();

	Menu();

	glutDisplayFunc(display);
	glutIdleFunc (display);
	glutReshapeFunc(reshape);
	glutPassiveMotionFunc(MouseMovement); //Check for mouse movement.
	glutKeyboardFunc (keyboard); //Check for keyboard press.
	glutMouseFunc(gl_select); //Check for mouse click
	glutMainLoop();
	return 0;
}