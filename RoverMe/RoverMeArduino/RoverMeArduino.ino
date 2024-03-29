/*
* created by Jonathan Abraham, http://github.com/jobam
* RoverMe V1.0
* 2015
*/

int motor1Pin1 = 3; // pin 2 on L293D IC
int motor1Pin2 = 4; // pin 7 on L293D IC
int enable1Pin = 6; // pin 1 on L293D IC
int motor2Pin1 = 8; // pin 10 on L293D IC
int motor2Pin2 = 9; // pin 15 on L293D IC
int enable2Pin = 11; // pin 9 on L293D IC
int state;
int flag = 0;        //makes sure that the serial only prints once the state
int stateStop = 0;
void setup() {
	// sets the pins as outputs:
	pinMode(motor1Pin1, OUTPUT);
	pinMode(motor1Pin2, OUTPUT);
	pinMode(enable1Pin, OUTPUT);
	pinMode(motor2Pin1, OUTPUT);
	pinMode(motor2Pin2, OUTPUT);
	pinMode(enable2Pin, OUTPUT);
	// sets enable1Pin and enable2Pin high so that motor can turn on:
	digitalWrite(enable1Pin, HIGH);
	digitalWrite(enable2Pin, HIGH);
	// initialize serial communication at 9600 bits per second:
	Serial.begin(9600);
}

void loop() {
	//if some data is sent, reads it and saves in state
	if (Serial.available() > 0) {
		state = Serial.read();
		flag = 0;
	}
	// if the state is '1' the DC motor will go forward
	if (state == '1') {
		digitalWrite(motor1Pin1, HIGH);
		digitalWrite(motor1Pin2, LOW);
		digitalWrite(motor2Pin1, LOW);
		digitalWrite(motor2Pin2, HIGH);
		if (flag == 0) {
			Serial.println("Go Forward!");
			flag = 1;
		}
	}

	// if the state is '2' the motor will turn left
	else if (state == '2') {
		digitalWrite(motor1Pin1, HIGH);
		digitalWrite(motor1Pin2, LOW);
		digitalWrite(motor2Pin1, LOW);
		digitalWrite(motor2Pin2, LOW);
		if (flag == 0) {
			Serial.println("Turn LEFT");
			flag = 1;
		}
		delay(1500);
		state = 3;
		stateStop = 1;
	}
	// if the state is '3' the motor will Stop
	else if (state == '3' || stateStop == 1) {
		digitalWrite(motor1Pin1, LOW);
		digitalWrite(motor1Pin2, LOW);
		digitalWrite(motor2Pin1, LOW);
		digitalWrite(motor2Pin2, LOW);
		if (flag == 0) {
			Serial.println("STOP!");
			flag = 1;
		}
		stateStop = 0;
	}
	// if the state is '4' the motor will turn right
	else if (state == '4') {
		digitalWrite(motor1Pin1, LOW);
		digitalWrite(motor1Pin2, LOW);
		digitalWrite(motor2Pin1, LOW);
		digitalWrite(motor2Pin2, HIGH);
		if (flag == 0) {
			Serial.println("Turn RIGHT");
			flag = 1;
		}
		delay(1500);
		state = 3;
		stateStop = 1;
	}
	// if the state is '5' the motor will Reverse
	else if (state == '5') {
		digitalWrite(motor1Pin1, LOW);
		digitalWrite(motor1Pin2, HIGH);
		digitalWrite(motor2Pin1, HIGH);
		digitalWrite(motor2Pin2, LOW);
		if (flag == 0) {
			Serial.println("Reverse!");
			flag = 1;
		}
	}
	//For debugging purpose
	//Serial.println(state);
}