from pydantic import BaseModel

class OrderStatusResponse(BaseModel):
    orderId: int
    status: str
    filled: float
    avgFillPrice: float
    message: str | None = None
