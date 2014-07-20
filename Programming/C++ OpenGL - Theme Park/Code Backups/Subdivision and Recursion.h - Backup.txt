#include <stdio.h> 
#include <GL/glut.h> 


//Initial triangle 
GLfloat v[3][3]={{-1,0,-0.5},{1,0,-0.5},{0,0,1}};

GLfloat centre1[3];
GLfloat centre2[3];
GLfloat centre3[3];
GLfloat centre4[3];

/*GLfloat height1 = 0.75;
GLfloat height2 = 0.75;
GLfloat height3 = 0.75;
GLfloat height4 = 1.5;*/

GLfloat height1 = rand() % (200/100)+0.5;
GLfloat height2 = rand() % (200/100)+0.5;
GLfloat height3 = rand() % (200/100)+0.5;
GLfloat height4 = rand() % (200/100)+0.5;

int n = 1;
//int n = rand() % 2 + 1; // number of recursive steps 

void triangle3( GLfloat *a, GLfloat *b, GLfloat *c)
//Display one triangle  
{
	glBegin(GL_TRIANGLES);
      glVertex3fv(a); 
      glVertex3fv(b);  
      glVertex3fv(c);
    glEnd();
}


void divide_triangle3(GLfloat *a, GLfloat *b, GLfloat *c,int m)
{
/* triangle subdivision using vertex numbers */
	glEnable(GL_NORMALIZE);
    GLfloat v0[3], v1[3], v2[3];
    int j;
    if(m>0)
     {
		for(j=0; j<3; j++)
		{
			v0[j]=((a[j]+b[j])/2);
			v1[j]=((a[j]+c[j])/2);
			v2[j]=((b[j]+c[j])/2);
		}

		for (int i = 0; i < 3; i++)
		{
			//GLfloat height1 = rand() % (200/100)+0.5;
			if (i == 1) //When i is equal to 1, the y value is being calculated, so a random height can be assigned.
			{
				centre1[i] = ((v2[i]+b[i]+v0[i])/3)+height1;//Calculate the middle point between the three points and add a random value to the height.
			}
			else
			{
				centre1[i] = (v2[i]+b[i]+v0[i])/3;//Calculate the middle point between the three points.
			}
		}

		for (int i = 0; i < 3; i++)
		{
			//GLfloat height2 = rand() % (200/100)+0.5;
			if (i == 1)
			{
				centre2[i] = ((v0[i]+a[i]+v1[i])/3)+height2;
			}
			else
			{
				centre2[i] = (v0[i]+a[i]+v1[i])/3;
			}
		}

		for (int i = 0; i < 3; i++)
		{
			//GLfloat height3 = rand() % (200/100)+0.5;
			if (i == 1)
			{
				centre3[i] = ((v1[i]+c[i]+v2[i])/3)+height3;
			}
			else
			{
				centre3[i] = (v1[i]+c[i]+v2[i])/3;
			}
		}

		for (int i = 0; i < 3; i++)
		{
			//GLfloat height4 = rand() % (200/100)+0.5;
			if (i == 1)
			{
				centre4[i] = ((v0[i]+v1[i]+v2[i])/3)+height4;
			}
			else
			{
				centre4[i] = (v0[i]+v1[i]+v2[i])/3;
			}
		}
		
		//Base
		glColor3f(1,1,0);
        divide_triangle3(a, v0, v1, m-1);
		glColor3f(1,1,0);
        divide_triangle3(c, v1, v2, m-1);
		glColor3f(1,1,0);
        divide_triangle3(b, v2, v0, m-1);

		//First tetrahedron
		glColor3f(0,0,1);
		glNormal3f(-1,1,-1);
		divide_triangle3(b, centre1, v0, m-1);
		glColor3f(0,0,1);
		glNormal3f(1,1,-1);
		divide_triangle3(b, centre1, v2, m-1);
		glColor3f(0,0,1);
		glNormal3f(0,1,1);
		divide_triangle3(v0, centre1, v2, m-1);

		//Second tetrahedron
		glColor3f(1,0,0);
		glNormal3f(-1,1,-1);
		divide_triangle3(a, centre2, v0, m-1);
		glColor3f(1,0,0);
		glNormal3f(0,1,1);
		divide_triangle3(a, centre2, v1, m-1);
		glColor3f(1,0,0);
		glNormal3f(1,1,-1);
		divide_triangle3(v0, centre2, v1, m-1);

		//Third tetrahedron
		glColor3f(0,1,0);
		glNormal3f(1,1,-1);
		divide_triangle3(c, centre3, v2, m-1);
		glColor3f(0,1,0);
		glNormal3f(0,1,1);
		divide_triangle3(c, centre3, v1, m-1);
		glColor3f(0,1,0);
		glNormal3f(-1,1,-1);
		divide_triangle3(v2, centre3, v1, m-1);

		//Fourth tetrahedron
		glColor3f(1,1,0);
		glNormal3f(1,1,-1);
		divide_triangle3(v0, centre4, v2, m-1);
		glColor3f(1,1,0);
		glNormal3f(-1,1,-1);
		divide_triangle3(v0, centre4, v1, m-1);
		glColor3f(1,1,0);
		glNormal3f(0,1,1);
		divide_triangle3(v2, centre4, v1, m-1);
    }
    else(triangle3(a,b,c)); //draw triangle at end of recursion 
 
}

void tetrahedron(int m)
{
	glColor3f(1.0,0.0,0.0);
	divide_triangle3(v[0], v[1], v[2], n);
	glColor3f(0.0,1.0,0.0);
	divide_triangle3(v[3], v[2], v[1], n);
	glColor3f(0.0,0.0,1.0);
	divide_triangle3(v[0], v[3], v[1], n);
	glColor3f(1.0,1.0,0.0);
	divide_triangle3(v[0], v[2], v[3], n);
}