#include <cstdlib>
#include <GL\glut.h>
#include <ctime>

GLfloat terrain[4][3] = {{2.5, 0.0, 2.5},
				      {-2.5, 0.0, 2.5},
					  {-2.5, 0.0, -2.5},
					  {2.5, 0.0, -2.5}};

//GLfloat w[3] = {0,0,0};

GLfloat heightWater;

void drawQuad(GLfloat* a, GLfloat* b, GLfloat* c, GLfloat* d)
{
	glNormal3f(0,1,0);
	glVertex3fv(a);
	glVertex3fv(b);
	glVertex3fv(c);
	glVertex3fv(d);
}

void subdivideQuads(GLfloat a[], GLfloat b[], GLfloat c[], GLfloat d[], int m)
{
	if(m>0)
	{
		GLfloat e[3];
		GLfloat f[3];
		GLfloat g[3];
		GLfloat h[3];
		GLfloat centre[3];
		int i = 0;

		srand(time(NULL));

		for(i = 0; i<3; i++) if(i!= 1) e[i] = (a[i]+b[i])/2;
		for(i = 0; i<3; i++) if(i!= 1) f[i] = (b[i]+c[i])/2;
		for(i = 0; i<3; i++) if(i!= 1) g[i] = (c[i]+d[i])/2;
		for(i = 0; i<3; i++) if(i!= 1) h[i] = (d[i]+a[i])/2;

		centre[0] = (h[0]+f[0]+e[0]+g[0])/4;
		centre[2] = (h[2]+f[2]+e[2]+g[2])/4;
		
		e[1] = heightWater = rand() % (200/100);
		f[1] = heightWater = rand() % (200/100);
		srand(time(NULL));
		g[1] = heightWater = rand() % (200/100);
		h[1] = heightWater = rand() % (200/100);
		centre[1] = heightWater = rand() % (200/100);

		CalculateNormals(e, f);
		glNormal3f(w[0],w[1],w[2]);

		subdivideQuads(centre, h, a, e, m-1);
		subdivideQuads(e, b, f, centre, m-1);
		subdivideQuads(centre, f, c, g, m-1);
		subdivideQuads(g, d, h, centre, m-1);
	}
	else(drawQuad(a, b, c, d));
}