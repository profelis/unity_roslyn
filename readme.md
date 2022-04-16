# WARNING: this is just POC (proof of concept), not properly tested!


## Unity runtime scripting using Roslyn compiler

just example how to use Roslyn scripting with Unity

- Simpliest example

```cs
var result = await CSharpScript.EvaluateAsync<double>("System.Math.Pow(2,3)");
Debug.LogFormat("res is {0}", result);
Assert.AreEqual(result, 8);
```

- Vector3 usage

```cs
var vec = await CSharpScript.EvaluateAsync<Vector3>(
    "new Vector3(1, 2, 3)",
    ScriptOptions.Default.WithImports("UnityEngine")
        .AddReferences(typeof(UnityEngine.Vector3).Assembly)
);
Debug.LogFormat("vector = {0}", vec);
```

- Parameterize a script

```cs
var prevPos = Vector3.zero;
transform.position = prevPos;
await CSharpScript.EvaluateAsync<Vector3>(
    "transform.position = new Vector3(2, 2, 2)",
    ScriptOptions.Default.WithImports("UnityEngine")
        .AddReferences(typeof(UnityEngine.Vector3).Assembly),
    globals: new Globals {transform = transform}
);
var position = transform.position;
Debug.LogFormat("transform pos is {0} was {1}", position, prevPos);
```

## Extra setup

- Install required dlls into Assets/Plugins folder (reed Assets/Plugins/readme.md for more info)

### Useful links
- https://github.com/dotnet/roslyn/blob/main/docs/wiki/Scripting-API-Samples.md

- https://worldofzero.com/videos/runtime-c-scripting-embedding-the-net-roslyn-compiler-in-unity/

- https://docs.unity3d.com/2022.1/Documentation/Manual/roslyn-analyzers.html

- https://forum.unity.com/threads/released-roslyn-c-runtime-c-compiler.651505/
