#include<iostream>
using namespace std;

#include<stdlib.h>
#include<string.h>

//�˵���ӡ���� 
void print_menu()
{
	system("cls");
	cout<<"This is a program about CAESAR cipher,and it has the following three functions"<<endl;
	cout<<"Caesar password encryption��1��"<<endl;
	cout<<"Caesar code decryption��2��"<<endl;
	cout<<"Brute force decryption��3��"<<endl;
	cout<<"Exit the program��4��"<<endl;
	cout<<"Please enter the feature you want to use��1/2/3/4����";
} 

// ���ܺ�����plaintext�����ģ�shift��ƫ����
string encrypt(string plaintext, int shift) 
{
    string ciphertext = "";
    // ���������е�ÿ���ַ�
    for (int i = 0; i < plaintext.length(); i++) 
	{
        char c = plaintext[i];
        // ����ַ�����ĸ������м��ܲ���
        if (isalpha(c))
		{
            // ����ĸת��Ϊ��д
            c = toupper(c);
            // ����ĸ��ASCII��ֵ��ȥ65���Ա�����0��25֮�������λ
            c = ((c - 65 + shift) % 26) + 65;
            // �����ܺ���ַ���ӵ������ַ�����
        }
        ciphertext += c;
    }
    // ���ؼ��ܺ�������ַ���
    return ciphertext;
}

// ���ܺ�����ciphertext�����ģ�shift��ƫ����
string decrypt(string ciphertext, int shift) 
{
    string plaintext = "";
    // ���������е�ÿ���ַ�
    for (int i = 0; i < ciphertext.length(); i++) 
	{
        char c = ciphertext[i];
        // ����ַ�����ĸ������н��ܲ���
        if (isalpha(c)) 
		{
            // ����ĸת��Ϊ��д
            c = toupper(c);
            // ����ĸ��ASCII��ֵ��ȥ65���Ա�����0��25֮�������λ
            c = ((c - 65 - shift + 26) % 26) + 65;
            // �����ܺ���ַ���ӵ������ַ�����
        }
        plaintext += c;
    }
    // ���ؽ��ܺ�������ַ���
    return plaintext;
}

// ö�����п��ܵ���Կ������ӡ����Ӧ������

void brute_force_decryption(string ciphertext)
{
    // ѭ��������Կ��Χ��0��25��
    for (int all_possible_key=1;all_possible_key<=26;all_possible_key++) 
	{
        // ��������
        string plaintext = decrypt(ciphertext, all_possible_key);
        // ��ӡ���ĺͶ�Ӧ����Կ
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
					// ʹ�ü��ܺ�����������
					encrypted_message = encrypt(plaintext, key);
					//��ӡ��� 
    				cout << "Original message: " << plaintext << endl;
    				cout << "Encrypted message: " << encrypted_message << endl;
    				
    				system("pause");
    				break;
				case 2:
					system("cls");
					
					cout<<"input ciphertext and a key:";
					cin>>ciphertext>>key;
					// ʹ�ý��ܺ�����������
    				decrypted_message = decrypt(ciphertext, key);
    				//��ӡ��� 
    				cout << "Original message: " << ciphertext << endl;
				    cout << "Decrypted message: " << decrypted_message << endl;
				    
				    system("pause");
				    break;
				case 3:
					system("cls");
					
					cout<<"input ciphertext:";
					
					//��� �س��� ��getline() ������Ӱ�� 
					cin.ignore(); 
					getline(cin, ciphertext);

					brute_force_decryption(ciphertext);
					
					system("pause");
				    break;
				case 4:
					return 0;
				default:
					cout<<"����������������롣"<<endl;	
			}
	}
	
}
