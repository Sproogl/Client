
#define _WINSOCK_DEPRECATED_NO_WARNINGS
#define _CRT_SECURE_NO_WARNINGS

#include <iostream>
#include <string>
#include <sstream>
#include <stdio.h>
#include <winsock2.h>
#include <windows.h>
#include <thread>
#include <string.h>
#include <map> 
#include "Server.h"
#pragma comment (lib, "ws2_32.lib")
#pragma comment (lib, "mswsock.lib")







int hashing(char ip[50])
{
	std::string ID = ip;
	std::hash<std::string> hash_fn;
	unsigned int ID_hash = hash_fn(ID);
	std::cout << ID_hash << '\n';
	return ID_hash;
}




int main(int argc, char* argv[])
{
	Server serv;
	serv.startServer();
	return 0;
}
