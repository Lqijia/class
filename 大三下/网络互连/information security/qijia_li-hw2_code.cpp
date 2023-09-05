#include<iostream>
using namespace std;

#include<stdlib.h>
#include<string.h>

//菜单打印函数 
void print_menu()
{
	system("cls");
	cout<<"This is a program about CAESAR cipher,and it has the following three functions"<<endl;
	cout<<"Caesar password encryption（1）"<<endl;
	cout<<"Caesar code decryption（2）"<<endl;
	cout<<"Brute force decryption（3）"<<endl;
	cout<<"Exit the program（4）"<<endl;
	cout<<"Please enter the feature you want to use（1/2/3/4）：";
} 

// 加密函数，plaintext是明文，shift是偏移量
string encrypt(string plaintext, int shift) 
{
    string ciphertext = "";
    // 遍历明文中的每个字符
    for (int i = 0; i < plaintext.length(); i++) 
	{
        char c = plaintext[i];
        // 如果字符是字母，则进行加密操作
        if (isalpha(c))
		{
            // 将字母转换为大写
            c = toupper(c);
            // 将字母的ASCII码值减去65，以便于在0到25之间进行移位
            c = ((c - 65 + shift) % 26) + 65;
            // 将加密后的字符添加到密文字符串中
        }
        ciphertext += c;
    }
    // 返回加密后的密文字符串
    return ciphertext;
}

// 解密函数，ciphertext是密文，shift是偏移量
string decrypt(string ciphertext, int shift) 
{
    string plaintext = "";
    // 遍历密文中的每个字符
    for (int i = 0; i < ciphertext.length(); i++) 
	{
        char c = ciphertext[i];
        // 如果字符是字母，则进行解密操作
        if (isalpha(c)) 
		{
            // 将字母转换为大写
            c = toupper(c);
            // 将字母的ASCII码值减去65，以便于在0到25之间进行移位
            c = ((c - 65 - shift + 26) % 26) + 65;
            // 将解密后的字符添加到明文字符串中
        }
        plaintext += c;
    }
    // 返回解密后的明文字符串
    return plaintext;
}

// 枚举所有可能的密钥，并打印出对应的明文

void brute_force_decryption(string ciphertext)
{
    // 循环遍历密钥范围（0到25）
    for (int all_possible_key=1;all_possible_key<=26;all_possible_key++) 
	{
        // 解密密文
        string plaintext = decrypt(ciphertext, all_possible_key);
        // 打印明文和对应的密钥
        cout << "Key " << all_possible_key << ": " << plaintext << endl;
    }
}

int main()
{
	int choice=0;
	int key=0; 
	string plaintext="";
	string ciphertext="";
	
	string encrypted_message=""; 
	string decrypted_message="";
	
	while(1)
	{
		print_menu();
		cin>>choice;
		switch(choice)
			{
				case 1:
					system("cls");
					
					cout<<"input plaintext and a key:";
					cin>>plaintext>>key;
					// 使用加密函数加密明文
					encrypted_message = encrypt(plaintext, key);
					//打印结果 
    				cout << "Original message: " << plaintext << endl;
    				cout << "Encrypted message: " << encrypted_message << endl;
    				
    				system("pause");
    				break;
				case 2:
					system("cls");
					
					cout<<"input ciphertext and a key:";
					cin>>ciphertext>>key;
					// 使用解密函数解密密文
    				decrypted_message = decrypt(ciphertext, key);
    				//打印结果 
    				cout << "Original message: " << ciphertext << endl;
				    cout << "Decrypted message: " << decrypted_message << endl;
				    
				    system("pause");
				    break;
				case 3:
					system("cls");
					
					cout<<"input ciphertext:";
					
					//清除 回车符 对getline() 函数的影响 
					cin.ignore(); 
					getline(cin, ciphertext);

					brute_force_decryption(ciphertext);
					
					system("pause");
				    break;
				case 4:
					return 0;
				default:
					cout<<"输入错误，请重新输入。"<<endl;	
			}
	}
	
}
