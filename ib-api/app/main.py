from fastapi import FastAPI
from app.routes import orders, historical, system
from app.services.ib_client import connect_to_ib

app = FastAPI()

app.include_router(orders.router)
app.include_router(historical.router)
app.include_router(system.router)

@app.on_event("startup")
async def startup():
    await connect_to_ib()


