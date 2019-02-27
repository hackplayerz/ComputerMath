using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Main Processor
/// </summary>
class Program
{
    static void Main(string[] args)
    {
        Matrix44 newMatrix1 = Matrix44.UnitMatrix;
        Matrix44 newMatrix2 = Matrix44.UnitMatrix;

        newMatrix1.PrintAllNode();

        Matrix44.MultiplyByMatrix( newMatrix1 , newMatrix2 ).PrintAllNode();
    }
}

/// <summary>
/// Class of Matrix 4 by 4
/// </summary>
class Matrix44
{
    #region Variable Field

    private double[,] _matrix = new double[ 4 , 4 ];

    // End of Variable Field
    #endregion 

    #region Initializer

    /// <summary>
    /// Initialize new Empty Matrix
    /// </summary>
    public Matrix44()
    {
        for ( int line = 0; line < 4; line++ )
        {
            for ( int column = 0; column < 4; column++ )
            {
                _matrix[ line , column ] = 0;
            }
        }
    }

    /// <summary>
    /// Initialize to Scale Matrix
    /// </summary>
    /// <param name="sx"> matrix(0,0) data </param>
    /// <param name="sy"> matrix(1,1) data </param>
    /// <param name="sz"> matrix(2,2) data </param>
    public Matrix44(double sx , double sy , double sz)
    {
        for ( int i = 0; i < 4; i++ )
        {
            for ( int j = 0; j < 4; j++ )
            {
                if ( ( i - j ) != 0 )
                {
                    _matrix[ i , j ] = 0;
                }
                else
                {
                    switch(i * j)
                    {
                        case 0:
                            _matrix[ i , j ] = sx;
                            break;

                        case 1:
                            _matrix[ i , j ] = sy;
                            break;

                        case 4:
                            _matrix[ i , j ] = sz;
                            break;

                        case 9:
                            _matrix[ i , j ] = 1;
                            break;
                    }
                }
            }
        }
    }
    /// <summary>
    /// Initialize to Rotation Matrix
    /// </summary>
    /// <param name="rotation">rotate value</param>
    /// <param name="axis">Coordinate axes to Rotate</param>
    public Matrix44(float rotation,char axis)
    {
        switch( axis )
        {
            case 'x': // X Rotate
                _matrix[ 0 , 0 ] = 1f;
                _matrix[ 1 , 1 ] = Math.Cos( rotation );
                _matrix[ 2 , 2 ] = Math.Cos( rotation );
                _matrix[ 3 , 3 ] = 1f;
                _matrix[ 2 , 1 ] = Math.Sin( rotation );
                _matrix[ 1 , 2 ] = -Math.Sin( rotation );
                break;

            case 'y': // Y rotate
                _matrix[ 0 , 0 ] = Math.Cos( rotation );
                _matrix[ 1 , 1 ] = 1f;
                _matrix[ 2 , 2 ] = Math.Cos( rotation );
                _matrix[ 3 , 3 ] = 1f;
                _matrix[ 2 , 0 ] = -Math.Sin( rotation );
                _matrix[ 0 , 2 ] = Math.Sin( rotation );
                break;

            case 'z': // Z Rotate
                _matrix[ 0 , 0 ] = Math.Cos( rotation );
                _matrix[ 0 , 1 ] = -Math.Sin( rotation);
                _matrix[ 1 , 0 ] = Math.Sin( rotation );
                _matrix[ 1 , 1 ] = Math.Cos( rotation );
                _matrix[ 2 , 2 ] = 1f;
                _matrix[ 3 , 3 ] = 1f;
                break;

            default:
                Console.WriteLine( "Wrong Input" );
                return;
        }
    }

    /// <summary>
    /// Initialize horizontal move Matrix
    /// </summary>
    /// <param name="movePosition">moving Position</param>
    public Matrix44(Vector4 movePosition)
    {
        for(int i=0;i<4;i++ )
        {
            for(int j = 0; j<4;j++ )
            {
                if((i-j) == 0)
                {
                    _matrix[ i , j ] = 1f;
                }
                else
                {
                    _matrix[ i , j ] = 0;
                }
            }
        }
        _matrix[ 0 , 3 ] = movePosition.X;
        _matrix[ 1 , 3 ] = movePosition.Y;
        _matrix[ 2 , 3 ] = movePosition.Z;
    }

    // End of Initializer
    #endregion

    #region Static Field

    /// <summary>
    /// Initialize to Unit Matrix
    /// </summary>
    public static Matrix44 UnitMatrix
    {
        get
        {
            Matrix44 eMatrix = new Matrix44();

            for ( int i = 0; i < 4; i++ )
            {
                for ( int j = 0; j < 4; j++ )
                {
                    if ( ( i - j ) == 0 )
                    {
                        eMatrix._matrix[ i , j ] = 1;
                    }
                    else
                    {
                        eMatrix._matrix[ i , j ] = 0;
                    }
                }
            }
            return eMatrix;
        }
    }

    // End of Static Field
    #endregion

    #region Calculate Function

    /// <summary>
    /// (Matrix) X (Vector4)
    /// </summary>
    /// <param name="vector">Vector Value</param>
    public void MultiplyByVector(Vector4 vector)
    {
        // Calculate Multiply Matrix with Vector4.
        for ( int x = 0; x < 4; x++ ) 
        {
            _matrix[ x , 0 ] *= vector.X;
        }
        for ( int y = 0; y < 4; y++ ) 
        {
            _matrix[ y , 1 ] *= vector.Y;
        }
        for ( int z = 0; z<4;z++ )
        {
            _matrix[ z , 2 ] *= vector.Z;
        }
        for ( int w = 0; w < 4; w++ ) 
        {
            _matrix[ w , 3 ] *= vector.W;
        }
    }

    /// <summary>
    /// (Matrix) X (Matrix)
    /// </summary>
    /// <param name="matrix"> Multiply with Matrix </param>
    public static Matrix44 MultiplyByMatrix(Matrix44 matrix1, Matrix44 matrix2)
    {
        Matrix44 calculated_matrix = new Matrix44();
        for(int parent_line = 0; parent_line < 4; parent_line++ )
        {
            for ( int parent_column = 0; parent_column < 4; parent_column++ )
            {
                for ( int line = 0; line < 4; line++ )
                {
                    // Calculate 
                    calculated_matrix._matrix[ parent_line , parent_column ] += (matrix1._matrix[ parent_line , line ] * matrix2._matrix[ line , parent_column ]);
                }

            }
            
        }

        return calculated_matrix;
    }

    // End of Calculate Function
    #endregion
    

    #region Function Field

    /// <summary>
    /// Print All node of Matrix
    /// </summary>
    public void PrintAllNode()
    {
        Console.WriteLine( "=== Print All Node ====" );
        for(int line = 0; line<4;line ++ )
        {
            for(int column = 0; column < 4;column++ )
            {
                Console.Write( "│  " + _matrix[ line , column ] + " ");
            }
            Console.WriteLine("│");
        }
        Console.WriteLine( "=======================" );
    }

    // End of Class
    #endregion
}

/// <summary>
/// Class of Dimension 4 Vector
/// </summary>
class Vector4
{
    #region Variable Field

    /*
     * { x }
     * { y }
     * { z }
     * { w }
     */
    public float X;
    public float Y;
    public float Z;
    public float W;

    // End of Variable Field
    #endregion

    #region Static Field

    public static Vector4 One
    {
        get
        {
            Vector4 vector4 = new Vector4( 1 , 1 , 1 , 1 );
            return vector4;
        }
    }

    public static Vector4 Zero
    {
        get
        {
            Vector4 vector4 = new Vector4();
            return vector4;
        }
    }

    //Static Field
    #endregion

    #region Function Field

    public Vector4()
    {
        X = 0;
        Y = 0;
        Z = 0;
        W = 0;
    }

    public Vector4(float x, float y,float z, float w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    // End of Function Field
    #endregion
}