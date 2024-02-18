# TradeStatistics

This repository contains a test task for an interview.

## Installation Instructions

1. **Create a network and run:**
   
   Make sure docker and git installed on your PC
     
   Open CMD and navigate to the directory where you wish store the project 
   Execute the following commands:
	
   - clone repository 
   ```bash
   git clone https://github.com/georgevolkov/TradeStatistics.git
   ```
   - open project directory
   ```bash
   cd TradeStatistics
   ```
   - create docker network 
   ```bash
   docker network create trade_statistics_network
   ```
   - build docker images     
   ```bash
   docker compose build
   ```
   - run builded images
   ```bash
   docker compose up -d
   ```

2. Access the Application:

Open http://localhost:8050/ in your web browser.


Notes:

1. Create refacoting
2. Add reconnection logic for websockets
3. Add exception handling
4. Add logging
5. Create database to store data if container wiil restart to keep calculations