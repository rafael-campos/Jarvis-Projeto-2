// Load Wi-Fi library
#include <ESP8266WiFi.h>
IPAddress local_IP(192, 168, 4, 199); //ESP static ip
IPAddress gateway(192, 168, 4, 1);
IPAddress subnet(255, 255, 255, 0);  //Subnet mask
IPAddress primaryDNS(8, 8, 8, 8); 
IPAddress secondaryDNS(8, 8, 4, 4);

// Pakai WiFi Siapa?
const char* ssid = "STARK";
const char* password = "Xks47sd@";

// Set web server port number to 8888
WiFiServer server(8888);

// Variable to store the HTTP request
String header;

// Auxiliar variables to store the current output state
String output1State = "off";
String output2State = "off";
String output3State = "off";
String output4State = "off";

// Assign output variables to GPIO pins
const int output1 = 5; //GPIO5 = D3
const int output2 = 14; //GPIO14 = D5
const int output3 = 12; //GPIO12 = D6
const int output4 = 13; //GPIO13 = D7

// Current time
unsigned long currentTime = millis();
// Previous time
unsigned long previousTime = 0; 
// Define timeout time in milliseconds (example: 2000ms = 2s)
const long timeoutTime = 2000;

// Define Authentication
const char* base64Encoding = "SmFydmlzODA6MTIzNDU2";//Jarvis80 senha:123456


void setup() {
  Serial.begin(115200);
  // Initialize the output variables as outputs
  pinMode(output1, OUTPUT);
  pinMode(output2, OUTPUT);
  pinMode(output3, OUTPUT);
  pinMode(output4, OUTPUT);
  // Set outputs to LOW
  digitalWrite(output1, HIGH);
  digitalWrite(output2, HIGH);
  digitalWrite(output3, HIGH);
  digitalWrite(output4, HIGH);

  // Connect to Wi-Fi network with SSID and password
  Serial.print("Mengubungkan ke ");
  Serial.println(ssid);
  WiFi.config(local_IP, gateway, subnet, primaryDNS, secondaryDNS);
  WiFi.begin(ssid, password);
  while (WiFi.status() != WL_CONNECTED) {
    delay(500);
    Serial.print(".");
  }
  // Print local IP address and start web server6
  Serial.println("");
  Serial.println("WiFi terhubung.");
  Serial.println("Web server dijalankan. Menunggu alamat IP dari ESP...");
  Serial.print("Silahkan Akses ke: http://"); Serial.print(WiFi.localIP()); Serial.println(":8888");
  server.begin();
}

void loop(){
  WiFiClient client = server.available();

  if (client) {                             
    currentTime = millis();
    previousTime = currentTime;
    Serial.println("Novo Cliente.");         
    String currentLine = "";               
    while (client.connected() && currentTime - previousTime <= timeoutTime) { 
      currentTime = millis();
      if (client.available()) {             
        char c = client.read();             
        Serial.write(c);                    
        header += c;
        if (c == '\n') {                    
          if (currentLine.length() == 0) {            
            if (header.indexOf(base64Encoding)>=0)
            {           
              
              client.println("HTTP/1.1 200 OK");
              client.println("Content-type:text/html");
              client.println("Conexão: fechada");
              client.println();
              
              // Btn OnOf TV = D3
              if (header.indexOf("GET /5/on") >= 0) {
                Serial.println("GPIO5/D3 on");
                output1State = "on";
                digitalWrite(output1, LOW);
              }              
              else if (header.indexOf("GET /5/off") >= 0) {
                Serial.println("GPIO5/D3 off");
                output1State = "off";
                digitalWrite(output1, HIGH);
              } 

              //Btn OnOf lampada luz do quarto
              else if (header.indexOf("GET /14/on") >= 0) {
                Serial.println("GPIO14/D5 on");
                output2State = "on";
                digitalWrite(output2, LOW);
              }              
              else if (header.indexOf("GET /14/off") >= 0) {
                Serial.println("GPIO14/D5 off");
                output2State = "off";
                digitalWrite(output2, HIGH);
              }
              
              //lampada 2 GPIO12 = D6 para simular porta
              else if (header.indexOf("GET /12/on") >= 0) {
                Serial.println("GPIO12/D6 on");
                output3State = "on";
                digitalWrite(output3, LOW);
              }              
              else if (header.indexOf("GET /12/off") >= 0) {
                Serial.println("GPIO12/D6 off");
                output3State = "off";
                digitalWrite(output3, HIGH);
              } 

              //lampada 3 GPIO13 = D7
              else if (header.indexOf("GET /13/on") >= 0) {
                Serial.println("GPIO13/D7 on");
                output4State = "on";
                digitalWrite(output4, LOW);
              }              
              else if (header.indexOf("GET /13/off") >= 0) {
                Serial.println("GPIO13/D7 off");
                output4State = "off";
                digitalWrite(output4, HIGH);
              } 
              
              
              // Display the HTML web page
              client.println("<!DOCTYPE html><html>");
              client.println("<head><meta name=\"viewport\" content=\"width=device-width, initial-scale=1\">");
              client.println("<link rel=\"icon\" href=\"data:,\">");
              client.println("<style>html { font-family: Helvetica; display: inline-block; margin: 0px auto; text-align: center;}");
              client.println(".button { background-color: #4CAF50; border: none; color: white; padding: 16px 40px;");
              client.println("text-decoration: none; font-size: 30px; margin: 2px; cursor: pointer;}");
              client.println(".button2 {background-color: #555555;}</style></head>");
              
              // Tampilan Display Heading Web
              client.println("<body><h1>Jarvis Web Server</h1>");
              client.println("<p><i>By LeAdr</i></p>");
              
              // para relê da tv GPIO5/D3  
              client.println("<hr>GPIO5/D3 - State " + output1State + "</hr>");
              // If the output1State is off, it displays the ON button       
              if (output1State=="off") {
                client.println("<p><a href=\"/5/on\"><button class=\"button\">ON</button></a></p>");
              } else {
                client.println("<p><a href=\"/5/off\"><button class=\"button button2\">OFF</button></a></p>");
              } 
                 
              // para relê lampada 1 GPIO14/D5  
              client.println("<p>GPIO14/D5 - State " + output2State + "</p>");    
              if (output2State=="off") {
                client.println("<p><a href=\"/14/on\"><button class=\"button\">ON</button></a></p>");
              } else {
                client.println("<p><a href=\"/14/off\"><button class=\"button button2\">OFF</button></a></p>");
              }

              // para lampada 2 GPIO12/D6  
              client.println("<p>GPIO12/D6 - State " + output3State + "</p>");       
              if (output3State=="off") {
                client.println("<p><a href=\"/12/on\"><button class=\"button\">ON</button></a></p>");
              } else {
                client.println("<p><a href=\"/12/off\"><button class=\"button button2\">OFF</button></a></p>");
              }

              // para lampada 3 GPIO13/D7  
              client.println("<p>GPIO13/D7 - State " + output4State + "</p>");
              // If the output2State is off, it displays the ON button       
              if (output4State=="off") {
                client.println("<p><a href=\"/13/on\"><button class=\"button\">ON</button></a></p>");
              } else {
                client.println("<p><a href=\"/13/off\"><button class=\"button button2\">OFF</button></a></p>");
              }
              
              client.println("</body></html>");
              
              // The HTTP response ends with another blank line
              client.println();
              // Break out of the while loop
              break;
            }
            else{
              client.println("HTTP/1.1 401 Unauthorized");
              client.println("WWW-Authenticate: Basic realm=\"Secure\"");
              client.println("Content-Type: text/html");
              client.println();
              client.println("<html>Authentication failed</html>");
            }
          } else {
            currentLine = "";
          }
        } else if (c != '\r') { 
          currentLine += c;    
        }
      }
    header = "";
    client.stop();
    Serial.println("Client disconnected.");
    Serial.println("");
  }
}
