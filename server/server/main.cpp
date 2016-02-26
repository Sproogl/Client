
#define _WINSOCK_DEPRECATED_NO_WARNINGS
#include <iostream>
#include <string>
#include <sstream>

#include <winsock2.h>
#include <windows.h>
#include <thread>
#include <string.h>
#pragma comment (lib, "ws2_32.lib")
#pragma comment (lib, "mswsock.lib")





struct WATF
{
	byte type;
	int ID_SRC;
	int ID_DEST;
	int MSG_LEN;
	char messange[500];

	WATF()

	{
		type = 111;
		ID_SRC =111;
		ID_DEST = 111;
		MSG_LEN = 111;

	}



};














int main(int argc, char* argv[])
{
	SOCKET		s, new_conn , send_sock;
	WSADATA		ws;
	sockaddr_in	sock_bind, new_ca;
	int			new_len, i = 0;
	char		buff[20];
	WATF wtf;
	WATF *buffer = new WATF();
	// Init 
	bool flag = true;
	std::cout << sizeof(WATF);

	if (FAILED(WSAStartup(0x0002, &ws))) return E_FAIL;

	// Создаём сокет
	if (INVALID_SOCKET == (s = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP)))

		// Выполняем привязку
		ZeroMemory(&sock_bind, sizeof(sock_bind));
	sock_bind.sin_family = AF_INET;
	sock_bind.sin_addr.S_un.S_addr = htonl(INADDR_ANY);		

	sock_bind.sin_port = htons(1332);				// Порт 1234
	printf("Wait for incoming connections...\n");
	if (SOCKET_ERROR == bind(s, (sockaddr*)&sock_bind, sizeof(sock_bind)))
	{
		return E_FAIL;
	}

	// Устанавливаем "слушающий" режим для сокета
	if (FAILED(listen(s, 10)))
	{
		return E_FAIL;
	}
	while (1)
	{

		ZeroMemory(&new_ca, sizeof(new_ca));
		new_len = sizeof(new_ca);
		if (flag)
		{ 
			
				if (FAILED(send_sock = accept(s, (sockaddr*)&new_ca, &new_len)))
				{

					return 0;
				}
				flag = false;

				if (FAILED(new_conn = accept(s, (sockaddr*)&new_ca, &new_len)))
				{

					return 0;
				}
		
		
		}
		else
		{
			if (FAILED(new_conn = accept(s, (sockaddr*)&new_ca, &new_len)))
			{

				return 0;
			}
			

		}
		// Получаем сообщение от клиента
		int msg_len;


		if (FAILED(msg_len = recv(new_conn, (char*)&wtf, sizeof(struct WATF), 0)))
			return 0;
		std::cout << "ID DEST : " << wtf.ID_DEST << std::endl << "ID SRC : " << wtf.ID_SRC << std::endl << "MSG LEN :" << wtf.ID_SRC << std::endl << "MESSANGE : " << wtf.messange << std::endl;
		wtf.type = 101;
		send(send_sock, (char*)&wtf, sizeof(struct WATF), 0);
		closesocket(new_conn);

	}

	printf("\n\nDone!");

	return 0;
}
