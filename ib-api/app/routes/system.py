from fastapi import APIRouter
from app.services.ib_client import ib, connect_to_ib

router = APIRouter()

@router.get("/status")
async def status():
    return {"connected": ib.isConnected()}

@router.post("/reconnect")
async def reconnect():
    try:
        if ib.isConnected():
            ib.disconnect()
        await connect_to_ib()
        return {"connected": ib.isConnected()}
    except Exception as e:
        return {"connected": False, "error": str(e)}
