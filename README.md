# TradeStatistics

This repository contains a test task for an interview.

## Installation Instructions

1. **Create a network and run:**

   Open CMD and navigate to the directory containing `TradeStatisticsAPI.sln`.

   Execute the following commands:
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

Access the Application:

Open http://localhost:8050/ in your web browser.


notes:

1. Create refacoting
2. Add reconnection logic for websockets
3. Add exception handling
4. Add logging
5. Create database to store data if container wiil restart to keep calculations