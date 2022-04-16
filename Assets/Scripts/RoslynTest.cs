using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using UnityEngine;
using UnityEngine.Assertions;

public class RoslynTest : MonoBehaviour {
    async void OnEnable() {
        var result = await CSharpScript.EvaluateAsync<double>("System.Math.Pow(2,3)");
        Debug.LogFormat("res is {0}", result);
        Assert.AreEqual(result, 8);

        var vec = await CSharpScript.EvaluateAsync<Vector3>(
            "new Vector3(1, 2, 3)",
            ScriptOptions.Default.WithImports("UnityEngine")
                .AddReferences(typeof(UnityEngine.Vector3).Assembly)
        );
        Debug.LogFormat("vector = {0}", vec);
        Assert.AreApproximatelyEqual(vec.x, 1);
        Assert.AreApproximatelyEqual(vec.y, 2);
        Assert.AreApproximatelyEqual(vec.z, 3);

        var prevPos = Vector3.zero;
        transform.position = prevPos;
        await CSharpScript.EvaluateAsync<Vector3>(
            "transform.position = new Vector3(2, 2, 2)",
            ScriptOptions.Default.WithImports("UnityEngine")
                .AddReferences(typeof(System.Object).Assembly, typeof(UnityEngine.Vector3).Assembly),
            globals: new Globals {transform = transform}
        );
        var position = transform.position;
        Assert.AreApproximatelyEqual(position.x, 2);
        Assert.AreApproximatelyEqual(position.y, 2);
        Assert.AreApproximatelyEqual(position.z, 2);
        Debug.LogFormat("transform pos is {0} was {1}", position, prevPos);
    }
}

public class Globals {
    public Transform transform;
}