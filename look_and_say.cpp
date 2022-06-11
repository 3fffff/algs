#include<iostream>

using namespace std;

string look_and_say(int n) {

	string res = "";
	if (n == 1) return "1";
	if (n == 2) return "11";
	res = "11";
	for (int i = 3; i <= n; i++) {
		int count = 1;
		res += "_";
		string temp = "";
		for (int j = 1; j < res.length(); j++) {
			if (res[j - 1] != res[j]) {
				temp += count + '0';
				temp += res[j - 1];
				count = 1;
			}
			else count++;
		}
		res = temp;
	}
	return res;
}
int main() {
	string res = look_and_say(5);
	cout << res << '\n';
	return 0;
}
