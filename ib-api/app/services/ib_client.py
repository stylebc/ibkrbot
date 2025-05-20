from ib_insync import *
import os, asyncio

util.startLoop()
ib = IB()

async def connect_to_ib():
    host = os.getenv("IB_HOST", "host.docker.internal")
    port = int(os.getenv("IB_PORT", "4002"))
    print(f"🔌 Connecting to IB Gateway at {host}:{port}")
    await ib.connectAsync(host, port, clientId=1)
    print("✅ Connected:", ib.isConnected())
    return ib




