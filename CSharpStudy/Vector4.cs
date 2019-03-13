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

    public Vector4(float x , float y , float z , float w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    // End of Function Field
    #endregion
}