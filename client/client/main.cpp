
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

	char messageC[500];
	WATF message;


	Client *client = new Client();

	client->setConfigtServer(Ip, port);

	client->listenmsg();
	while(true){

		cin >> message.MSG;
		client->sendMessage(message);

	}
	_getch();
	return 0;
}
