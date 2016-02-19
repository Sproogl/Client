
#define _WINSOCK_DEPRECATED_NO_WARNINGS
#include <iostream>
#include <string>
#include <sstream>
#include <conio.h>
#include <winsock2.h>
#include <windows.h>
#include "Client.h"
#pragma comment (lib, "ws2_32.lib")
#pragma comment (lib, "mswsock.lib")



int main(int argc, char* argv[])
{
	char Ip[] = { "127.0.0.1" };
	int port = 1322;
	string message("hello");
	char messageC[500];
	WATF *wtf = new WATF(10,10,10,"123456789");


	Client *client = new Client();

	if (client->connectServer(Ip, port) == false)
	{
		cout << "error conection";

	}
	else{
		client->sendMessage();
		while (true)
		{
			client->listenmsg();
			
		}
	}
	_getch();
	return 0;
}
