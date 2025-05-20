from fastapi import APIRouter, Query
from app.models.order_request import OrderRequest
from app.models.order_status_response import OrderStatusResponse
from app.services.ib_client import ib
from ib_insync import Stock, MarketOrder, LimitOrder
import time
import asyncio

router = APIRouter()

@router.post("/place-order", response_model=OrderStatusResponse)
async def place_order(req: OrderRequest):
    contract = Stock(req.symbol, "SMART", "USD")
    await ib.qualifyContractsAsync(contract)

    order = (
        MarketOrder(req.side, req.quantity)
        if req.orderType == "MKT"
        else LimitOrder(req.side, req.quantity, req.limitPrice)
    )

    trade = ib.placeOrder(contract, order)
    return OrderStatusResponse(
        orderId= trade.order.orderId, 
        status= trade.orderStatus.status,
        filled=trade.orderStatus.filled,
        avgFillPrice=trade.orderStatus.avgFillPrice,
        message=None
    )

@router.get("/order-status", response_model=OrderStatusResponse)
async def order_status(orderId: int = Query(...), timeout: int = 10):
    try:
        trade = next((t for t in ib.trades() if t.order.orderId == orderId), None)
        if trade is None:
            return OrderStatusResponse(
                orderId=orderId,
                status="Unknown",
                filled=0,
                avgFillPrice=0,
                message="Trade not found"
            )

        start_time = time.time()
        while not trade.isDone():
            if time.time() - start_time > timeout:
                return OrderStatusResponse(
                    orderId=orderId,
                    status=trade.orderStatus.status,
                    filled=trade.orderStatus.filled,
                    avgFillPrice=trade.orderStatus.avgFillPrice,
                    message="Timeout reached — partial result"
                )
            await asyncio.sleep(1)

        return OrderStatusResponse(
            orderId=trade.order.orderId,
            status=trade.orderStatus.status,
            filled=trade.orderStatus.filled,
            avgFillPrice=trade.orderStatus.avgFillPrice
        )
    except Exception as e:
        return OrderStatusResponse(
            orderId=orderId,
            status="Error",
            filled=0,
            avgFillPrice=0,
            message=str(e)
        )
