#!/bin/bash
nuget restore Ocelot.sln && xbuild Ocelot.sln /p:Configuration=Debug