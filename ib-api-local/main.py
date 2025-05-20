from fastapi import FastAPI
from ib_insync import *
import asyncio
import os

util.startLoop()
ib = IB()
app = FastAPI()

@app.on_event("startup")
async def connect():
    print("🔌 Connecting to IB Gateway...")
    await ib.connectAsync('127.0.0.1', 4002, clientId=1)
    print("✅ Connected:", ib.isConnected())

@app.get("/historical")
async def get_historical(symbol: str = "AAPL", barSize: str = "1 day", duration: str = "1 M"):
    try:
        contract = Stock(symbol, 'SMART', 'USD')
        bars = await ib.reqHistoricalDataAsync(
            contract,
            endDateTime='',
            durationStr=duration,
            barSizeSetting=barSize,
            whatToShow='TRADES',
            useRTH=True,
            formatDate=1
        )
        return [{"time": b.date.strftime("%Y-%m-%d"), "close": b.close} for b in bars]
    except Exception as e:
        return {"error": str(e)}
