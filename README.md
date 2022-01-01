# Overview
This repo contains an very simple app to do basic dot net perf testing. It allows comparing tests like inserting into a list versus a sorted dictionary.
Just for fun, I also got the app packaged in a container and created the artifacts to deploy it to Kubernetes pods where we can also test K8s' resource limits.

# About the App
The tester app is in the `src` folder and is a dotnet 6.0 console app

## Running the App
To run it:
> Note: This is just for a quick run, the proper way is to do a release build and run it directly rather than via the dotnet cli
```
cd src
dotnet run -- -t InsertToListTest
```

Running it with no parameters will show you all the parameters that are available.
The app doesn't have good error handling yet if you give it a bad test name.

The list of test names can be found in `src/Tests`, for now you need to include the `Test` part of the name, so `-t InsertToList` won't work, only `-t InsertToListTest`

## Code
`Program.cs` - does all the generic work. It runs the test's `PreTest`, `Test` and `PostTest` methods while measuring time before and after the `Test`. It users reflection to instantiate the test(s) provides via the `-t` input argument and runs it/them  as many times as specified via the `-r` input argument. Lastly, this also has the extra code to calculate the 90th percentile. This is done by keeping a separate list of tests and durations, sorted by duration ascending.

`Options.cs` - contains all the apps command line options. The mechanism to provide command line options uses the [CommandLineParser nuget package](https://www.nuget.org/packages/CommandLineParser).

`TestResults.cs` - POCO class to capture the results of running a test, used to generate the per-test summary stats that calculate the average and 90p duration.

`Tests/*` - contains all the different tests like inserting to a list and inserting to a pre-allocated list.

# About the Container
The `docker` folder has the Dockerfile and build script (`build.sh`) to build the container.
The Docker file follows the standard pattern per [Docker's ASP.NET Core application sample](https://docs.docker.com/samples/dotnetcore/) tweaked to vanilla dotnet core instead of aspnet core.

# About the K8s Pods
The `k8s` folder has the yaml specs for a bunch of different pods that do a one-time execution of a single test.
These are pods and not deployments, replica sets or anything else since they aren't meant to be running continuously, rather just once and finish.

Identical [K8s resource limits]() are used for all pods to ensure they all execute under the same conditions.
Only the "special" pods that are meant to demonstrate the impact of changing these resource limits have differente values (explained below).

## Deploying to K8s

To deploy them:
```
cd k8s
kubectl apply -f dotnet-perf-test-s.yaml
```

Or if you want to do it with a single command

```
find dotnet-perf*.yaml -exec kubectl apply -f {} \;
```

Then you'll see all pods:
```
kubectl get pods
```

> Note: Those pods with `oom` in their name will fail either with `OOMKilled` or `Error` status.
> Note2: If you set up the cluster per the guidance below, the pods will require more memory than the 1 starting node has. The pods will stay in `Pending` state while the cluster autoscales and adds another node or until the pods that do get allocated finish and more memory is available to start the remaining pods.

# Provisioning the K8s cluster
>Note: This section is under development, TODO: add more text and export the ARM template from AKS.

Provision via AKS in Azure.

1. Enable Autoscale
1. Use Standard_DS3_v2 nodes - 4 Cores, 14 GB memory
1. Other features?

# Test Results
## Comparing Insert Performance
TODO: Rather than creating pod yaml files for each, want to try out kustomize to see if I can do this more easily.

## Impact of Lowering CPU Resources
This one is straight forward - less CPU time, slower performance.

| Container Name | Items Inserted | CPU Resource | Test Name | Runs | Average Duration (in Seconds) | 90th Percentile Duration (in Seconds) |
| ============== | ============== | ============ | ========= | ==== | ============================= | ===================================== |
| M | 1M | 0.25 CPU | InsertToListTest | 200 | 0.029922860999999988 | 0.0849627 |
| M-Slow | 1M | 0.10 CPU | InsertToListTest | 200 | 0.08596047849999998 | 0.1028228 |

## Impact of Lowering Memory Resources
Lowering memory to such a point that there isn't enough memory for the `List<T>` causes the container to crash. The odd thing about this one is that it behaves differently for the `m` container versus the `l` container.
* `m` gets a K8s status of `OOMKilled`
* `m` gets a K8s status of `Error` and the logs show a dotnet exception of `System.OutOfMemoryException`

TODO: Understand why this discrepancy exists.

# Next Steps
1. Understand why m-oom gets an OOMKilled error from Kubernetes vs l-oom gets a dotnet OOM.
1. Understand why xl doesn't work...
1. Use Customization to generate a bunch of these for every test type.
1. Improve instructions to provision AKS cluster and export ARM template

