# Overview
This folder contains the assets to create a dotnet "scratchpad" in Kubernetes.
This enables the scenario where debugging is required from within Kubernetes while having a rapid iteration cycle that doesn't involve docker build and docker push.

At this point, this scratchpad is being used to debug why adding dotnet fails with OOM exception when attempting to add 1 billion small strings to a List<string>.

# Instructions
```
# Create the pod
kubectl apply -f dotnet-scratch.yaml

# Open a shell in the pod
kubectl exec -it dotnet-scratch sh
```

From within the pod:
```
# Install vim so that we can tweak the program
apt update && apt install -y vim

# Create a dotnet project
cd ~
mkdir code
cd code
dotnet new console

exit
```

Back again on the machine with this repo and kubectl:
```
# Copy Program.cs
kubectl cp Program.cs dotnet-scratch:root/code/Program.cs

# Open a shell in the pod again
kubectl exec -it dotnet-scratch sh
```

From within the pod:
```
cd ~/code

# Run and wait for it to fail with OOM
dotnet run
```

To clean up (run from the host, not the pod):
```
kubectl delete pod dotnet-scratch
```
