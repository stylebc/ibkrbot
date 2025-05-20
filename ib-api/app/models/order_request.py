from pydantic import BaseModel

class OrderRequest(BaseModel):
    symbol: str
    side: str  # BUY or SELL
    quantity: int
    orderType: str = "MKT"
    limitPrice: float | None = None
