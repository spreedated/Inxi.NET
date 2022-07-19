## Inxi.NET main instance

This library has its own main instance class which you can use to let the system run Inxi to grab hardware information and pass it to their appropriate properties. This is the master class of system information.

Such operation can be established by making a new object reference to the `Inxi` class like this:
```vb
Dim InxiInstance As New Inxi()
```
You can also selectively parse hardware by using this overload:
```vb
Dim InxiInstance As New Inxi(InxiParseFlags)
```
You can then use this instance to get information about hardware. You can either call `InxiInstance.Hardware` directly, or make an object reference to it, making you write less.
```vb
Dim HardwareInfo As HardwareInfo = InxiInstance.Hardware
```
`HardwareInfo` has sections for each hardware listed on the API documentation.
