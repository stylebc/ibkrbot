from fastapi import APIRouter
from app.services.ib_client import ib
from ib_insync import Stock

router = APIRouter()

@router.get("/historical")
async def get_historical(symbol: str, barSize: str = "1 day", duration: str = "1 M"):
    contract = Stock(symbol, "SMART", "USD")
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
