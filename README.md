# Overview


# About the App & Source Code
The tester app is in the `src` folder and is a dotnet 6.0 app that 

To run it:
> Note: This is just for a quick run, the proper way is to do a release build and run it directly rather than via the dotnet cli
```
cd src
dotnet run -- -t InsertToListTest
```

Running it with no parameters will show you all the parameters that are available.
The app doesn't have good error handling yet if you give it a bad test name.



Src
 Options
 Program
 TestResults
 Tests - Bunch of tests
   They all have the same K8s resources limits
	- RAM
	- CPU
   Except for variations intentionally meant to demonstrate the impact of changing resources.
        - Memory - OOM
        - CPU - slower

# About the Docker Container
 Docker file
 To build just run from ./docker/build and then docker push gsacavdm/dotnet-perf-test

# About K8s the Pods
 Has different pods to do a one-time execution.
 They are all running just a single tests

 Kubectl apply -f dotnet-perf-test-s.yaml

 Or if you want to do it with a single command
 
 find dotnet-perf*.yaml -exec kubectl apply -f {} \;

 Then you'll see all pods



# Next Steps
1. Understand why m-oom gets an OOMKilled error from Kubernetes vs l-oom gets a botnet OOM.
1. Understand why xl doesn't work...
1. Use Customization to generate a bunch of these for every test type.
1. Improve instructions to provision AKS cluster and export ARM template


# Provisioning the K8s cluster
Provision via AKS in Azure.

1. Enable Autoscale
1. Use Standard_DS3_v2 nodes - 4 Cores, 14 GB memory
1. Other features?

PENDING - Document this better. Export ARM template

# Test Results
App and infra aside, this section explains the findings from the different tests.

TBD
