using Confirma.Exceptions;
using Confirma.Formatters;
using Godot;

namespace Confirma.Extensions;

public static class ConfirmVectorExtensions
{
    #region ConfirmEqualApprox
    public static Vector2 ConfirmEqualApprox(
        this Vector2 vector,
        Vector2 expected,
        float tolerance = 0.0001f,
        string? message = null
    )
    {
        if (vector.IsEqualApprox(expected, tolerance))
        {
            return vector;
        }

        throw new ConfirmAssertException(
            "Expected {1} to be equal {2}.",
            nameof(ConfirmEqualApprox),
            new VectorFormatter(),
            vector,
            expected,
            message
        );
    }

    public static Vector3 ConfirmEqualApprox(
        this Vector3 vector,
        Vector3 expected,
        float tolerance = 0.0001f,
        string? message = null
    )
    {
        if (vector.IsEqualApprox(expected, tolerance))
        {
            return vector;
        }

        throw new ConfirmAssertException(
            "Expected {1} to be equal {2}.",
            nameof(ConfirmEqualApprox),
            new VectorFormatter(),
            vector,
            expected,
            message
        );
    }

    public static Vector4 ConfirmEqualApprox(
        this Vector4 vector,
        Vector4 expected,
        float tolerance = 0.0001f,
        string? message = null
    )
    {
        if (vector.IsEqualApprox(expected, tolerance))
        {
            return vector;
        }

        throw new ConfirmAssertException(
            "Expected {1} to be equal {2}.",
            nameof(ConfirmEqualApprox),
            new VectorFormatter(),
            vector,
            expected,
            message
        );
    }
    #endregion ConfirmEqualApprox

    #region ConfirmNotEqualApprox
    public static Vector2 ConfirmNotEqualApprox(
        this Vector2 vector,
        Vector2 expected,
        float tolerance = 0.0001f,
        string? message = null
    )
    {
        if (!vector.IsEqualApprox(expected, tolerance))
        {
            return vector;
        }

        throw new ConfirmAssertException(
            "Expected {1} not to be equal {2}.",
            nameof(ConfirmNotEqualApprox),
            new VectorFormatter(),
            vector,
            expected,
            message
        );
    }

    public static Vector3 ConfirmNotEqualApprox(
        this Vector3 vector,
        Vector3 expected,
        float tolerance = 0.0001f,
        string? message = null
    )
    {
        if (!vector.IsEqualApprox(expected, tolerance))
        {
            return vector;
        }

        throw new ConfirmAssertException(
            "Expected {1} not to be equal {2}.",
            nameof(ConfirmNotEqualApprox),
            new VectorFormatter(),
            vector,
            expected,
            message
        );
    }

    public static Vector4 ConfirmNotEqualApprox(
        this Vector4 vector,
        Vector4 expected,
        float tolerance = 0.0001f,
        string? message = null
    )
    {
        if (!vector.IsEqualApprox(expected, tolerance))
        {
            return vector;
        }

        throw new ConfirmAssertException(
            "Expected {1} not to be equal {2}.",
            nameof(ConfirmNotEqualApprox),
            new VectorFormatter(),
            vector,
            expected,
            message
        );
    }
    #endregion ConfirmNotEqualApprox

    #region ConfirmLessThan
    public static Vector2 ConfirmLessThan(
        this Vector2 vector,
        Vector2 expected,
        string? message = null
    )
    {
        if (vector < expected)
        {
            return vector;
        }

        throw new ConfirmAssertException(
            "Expected {1} to be less than {2}.",
            nameof(ConfirmLessThan),
            new VectorFormatter(),
            vector,
            expected,
            message
        );
    }

    public static Vector3 ConfirmLessThan(
        this Vector3 vector,
        Vector3 expected,
        string? message = null
    )
    {
        if (vector < expected)
        {
            return vector;
        }

        throw new ConfirmAssertException(
            "Expected {1} to be less than {2}.",
            nameof(ConfirmLessThan),
            new VectorFormatter(),
            vector,
            expected,
            message
        );
    }

    public static Vector4 ConfirmLessThan(
        this Vector4 vector,
        Vector4 expected,
        string? message = null
    )
    {
        if (vector < expected)
        {
            return vector;
        }

        throw new ConfirmAssertException(
            "Expected {1} to be less than {2}.",
            nameof(ConfirmLessThan),
            new VectorFormatter(),
            vector,
            expected,
            message
        );
    }
    #endregion ConfirmLessThan

    #region ConfirmLessThanOrEqual
    public static Vector2 ConfirmLessThanOrEqual(
        this Vector2 vector,
        Vector2 expected,
        string? message = null
    )
    {
        if (vector <= expected)
        {
            return vector;
        }

        throw new ConfirmAssertException(
            "Expected {1} to be less than or equal {2}.",
            nameof(ConfirmLessThanOrEqual),
            new VectorFormatter(),
            vector,
            expected,
            message
        );
    }

    public static Vector3 ConfirmLessThanOrEqual(
        this Vector3 vector,
        Vector3 expected,
        string? message = null
    )
    {
        if (vector <= expected)
        {
            return vector;
        }

        throw new ConfirmAssertException(
            "Expected {1} to be less than or equal {2}.",
            nameof(ConfirmLessThanOrEqual),
            new VectorFormatter(),
            vector,
            expected,
            message
        );
    }

    public static Vector4 ConfirmLessThanOrEqual(
        this Vector4 vector,
        Vector4 expected,
        string? message = null
    )
    {
        if (vector <= expected)
        {
            return vector;
        }

        throw new ConfirmAssertException(
            "Expected {1} to be less than or equal {2}.",
            nameof(ConfirmLessThanOrEqual),
            new VectorFormatter(),
            vector,
            expected,
            message
        );
    }
    #endregion ConfirmLessThanOrEqual

    #region ConfirmGreaterThan
    public static Vector2 ConfirmGreaterThan(
        this Vector2 vector,
        Vector2 expected,
        string? message = null
    )
    {
        if (vector > expected)
        {
            return vector;
        }

        throw new ConfirmAssertException(
            "Expected {1} to be greater than {2}.",
            nameof(ConfirmGreaterThan),
            new VectorFormatter(),
            vector,
            expected,
            message
        );
    }

    public static Vector3 ConfirmGreaterThan(
        this Vector3 vector,
        Vector3 expected,
        string? message = null
    )
    {
        if (vector > expected)
        {
            return vector;
        }

        throw new ConfirmAssertException(
            "Expected {1} to be greater than {2}.",
            nameof(ConfirmGreaterThan),
            new VectorFormatter(),
            vector,
            expected,
            message
        );
    }

    public static Vector4 ConfirmGreaterThan(
        this Vector4 vector,
        Vector4 expected,
        string? message = null
    )
    {
        if (vector > expected)
        {
            return vector;
        }

        throw new ConfirmAssertException(
            "Expected {1} to be greater than {2}.",
            nameof(ConfirmGreaterThan),
            new VectorFormatter(),
            vector,
            expected,
            message
        );
    }
    #endregion ConfirmGreaterThan

    #region ConfirmGreaterThanOrEqual
    public static Vector2 ConfirmGreaterThanOrEqual(
        this Vector2 vector,
        Vector2 expected,
        string? message = null
    )
    {
        if (vector >= expected)
        {
            return vector;
        }

        throw new ConfirmAssertException(
            "Expected {1} to be greater than or equal {2}.",
            nameof(ConfirmGreaterThanOrEqual),
            new VectorFormatter(),
            vector,
            expected,
            message
        );
    }

    public static Vector3 ConfirmGreaterThanOrEqual(
        this Vector3 vector,
        Vector3 expected,
        string? message = null
    )
    {
        if (vector >= expected)
        {
            return vector;
        }

        throw new ConfirmAssertException(
            "Expected {1} to be greater than or equal {2}.",
            nameof(ConfirmGreaterThanOrEqual),
            new VectorFormatter(),
            vector,
            expected,
            message
        );
    }

    public static Vector4 ConfirmGreaterThanOrEqual(
        this Vector4 vector,
        Vector4 expected,
        string? message = null
    )
    {
        if (vector >= expected)
        {
            return vector;
        }

        throw new ConfirmAssertException(
            "Expected {1} to be greater than or equal {2}.",
            nameof(ConfirmGreaterThanOrEqual),
            new VectorFormatter(),
            vector,
            expected,
            message
        );
    }
    #endregion ConfirmGreaterThanOrEqual

    #region ConfirmBetween
    public static Vector2 ConfirmBetween(
        this Vector2 vector,
        Vector2 min,
        Vector2 max,
        string? message = null
    )
    {
        if (vector >= min && vector <= max)
        {
            return vector;
        }

        throw new ConfirmAssertException(
            $"Expected {new VectorFormatter().Format(vector)} to be between "
            + "{1} and {2}.",
            nameof(ConfirmBetween),
            new VectorFormatter(),
            min,
            max,
            message
        );
    }

    public static Vector3 ConfirmBetween(
        this Vector3 vector,
        Vector3 min,
        Vector3 max,
        string? message = null
    )
    {
        if (vector >= min && vector <= max)
        {
            return vector;
        }

        throw new ConfirmAssertException(
            $"Expected {new VectorFormatter().Format(vector)} to be between "
            + "{1} and {2}.",
            nameof(ConfirmBetween),
            new VectorFormatter(),
            min,
            max,
            message
        );
    }

    public static Vector4 ConfirmBetween(
        this Vector4 vector,
        Vector4 min,
        Vector4 max,
        string? message = null
    )
    {
        if (vector >= min && vector <= max)
        {
            return vector;
        }

        throw new ConfirmAssertException(
            $"Expected {new VectorFormatter().Format(vector)} to be between "
            + "{1} and {2}.",
            nameof(ConfirmBetween),
            new VectorFormatter(),
            min,
            max,
            message
        );
    }
    #endregion ConfirmBetween

    #region ConfirmNotBetween
    public static Vector2 ConfirmNotBetween(
        this Vector2 vector,
        Vector2 min,
        Vector2 max,
        string? message = null
    )
    {
        if (vector < min || vector > max)
        {
            return vector;
        }

        throw new ConfirmAssertException(
            $"Expected {new VectorFormatter().Format(vector)} not to be between "
            + "{1} and {2}.",
            nameof(ConfirmNotBetween),
            new VectorFormatter(),
            min,
            max,
            message
        );
    }

    public static Vector3 ConfirmNotBetween(
        this Vector3 vector,
        Vector3 min,
        Vector3 max,
        string? message = null
    )
    {
        if (vector < min || vector > max)
        {
            return vector;
        }

        throw new ConfirmAssertException(
            $"Expected {new VectorFormatter().Format(vector)} not to be between "
            + "{1} and {2}.",
            nameof(ConfirmNotBetween),
            new VectorFormatter(),
            min,
            max,
            message
        );
    }

    public static Vector4 ConfirmNotBetween(
        this Vector4 vector,
        Vector4 min,
        Vector4 max,
        string? message = null
    )
    {
        if (vector < min || vector > max)
        {
            return vector;
        }

        throw new ConfirmAssertException(
            $"Expected {new VectorFormatter().Format(vector)} not to be between "
            + "{1} and {2}.",
            nameof(ConfirmNotBetween),
            new VectorFormatter(),
            min,
            max,
            message
        );
    }
    #endregion ConfirmNotBetween
}
