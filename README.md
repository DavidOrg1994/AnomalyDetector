# Anomaly Detector - GitHub Suspicious Behavior Notifier

Anomaly Detector is a C# web application built with .NET 8 that monitors and notifies suspicious behavior in a GitHub organization. This application integrates with GitHub using a webhook and detects specific behaviors deemed suspicious. When suspicious behavior is detected, it notifies the user through a console message.

## Prerequisites

Before running the application, make sure you have the following in place:

1. **.NET 8 SDK**: You need to have the .NET 8 SDK installed on your machine.

2. **GitHub Organization**

3. **Ngrok**: Ngrok is used to create a tunnel to your local development environment.

## Setup Instructions

Follow these steps to set up and run the Anomaly Detector application:

1. **Clone the Repository**: Clone this GitHub repository to your local machine.

2. **Configure Ngrok**:
    - Start Ngrok and create a tunnel to your local environment with the following command:
      ```bash
      ngrok http 5000
      ```
3. **Setup GitHub Webhook**
    
     Create a new webhook and configure it as follows:
    - Payload URL: https://<your-ngrok-subdomain.ngrok.io>/AnomalyDetector
    - Content type: application/json
    - Events: Select the events you want to monitor (e.g., "Pushes," "Repositories," "Teams").

4. **Build and Run**:
    - Build and run the application 
    - The application will start listening for incoming webhook events.

5. **Test Suspicious Behavior**:
    - To test the application, perform actions in your GitHub organization that match the suspicious behaviors defined in the exercise.

6. **Check Console Output**:
    - If suspicious behavior is detected, the details will be printed to the console.
