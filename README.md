# CacheServer
The purpose of this Visual Studio 2013 project is to provide a simple program that will give similar functionality to a 
memcached server in the Linux world.  The program depends on the TCPServerApp class in a separate repo.

This is a WPF server application.  Default IP address and port bindings are used when booting the program.  

Launch program with the argument PORT=XXXX,YYYY where XXXX and YYYY are additional ports.  Use as many as necessary.
Launch program with the argument IPADDRESS=XXXX,YYYY where XXXX and YYYY are valid IP Addresses.  Use as many as necessary.

When IPADDRESS and PORT arguments are used, every combination of Port and IP Address will be bound (including the defaults)

This program has only been tested using the 127.0.0.1 IP Address and separately, a 192.XX.XX.XX address.  This hasn't been tested to bind to multiple addresses at the same time.
