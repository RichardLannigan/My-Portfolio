#include <cstdlib>
#include <GL\glut.h>
#include <ctime>

GLfloat t[3][3]={{-1,0,-0.5},{1,0,-0.5},{0,0,1}};

GLfloat heightWaterT;

void drawTriangles(GLfloat* a, GLfloat* b, GLfloat* c)
{	
	glNormal3f(0,1,0);
	glVertex3fv(a);
	glVertex3fv(b);
	glVertex3fv(c);
}

void subdivideTriangles(GLfloat a[], GLfloat b[], GLfloat c[], int m)
{
	if(m>0)
	{
		GLfloat v0[3], v1[3], v2[3];
		GLfloat centre[3];
		int i = 0;

		srand(time(NULL));

		for(i = 0; i<3; i++) if(i!= 1) v0[i] = (a[i]+b[i])/2;
		for(i = 0; i<3; i++) if(i!= 1) v1[i] = (b[i]+c[i])/2;
		for(i = 0; i<3; i++) if(i!= 1) v2[i] = (c[i]+a[i])/2;

		centre[i] = ((v0[i]+v1[i]+v2[i])/3);
		
		v0[1] = heightWaterT = rand() % (200/100);
		v1[1] = heightWaterT = rand() % (200/100);
		srand(time(NULL));
		v2[1] = heightWaterT = rand() % (200/100);
		centre[1] = heightWaterT = rand() % (200/100);

		glColor3f(1,1,0);
        subdivideTriangles(a, v0, v1, m-1);
		glColor3f(1,1,0);
        subdivideTriangles(c, v1, v2, m-1);
		glColor3f(1,1,0);
        subdivideTriangles(b, v2, v0, m-1);
	}
	else(drawTriangles(a,b,c));
}