from ib_insync import *

# Connect to IB Gateway on your Mac (default port 4001)
ib = IB()
ib.connect('127.0.0.1', 4002, clientId=1)

# Fetch historical data
contract = Stock('MSFT', 'SMART', 'USD')
bars = ib.reqHistoricalData(
    contract,
    endDateTime='',
    durationStr='1 M',
    barSizeSetting='1 day',
    whatToShow='TRADES',
    useRTH=True,
    formatDate=1
)

# Print close prices
for bar in bars:
    print(bar.date, bar.close)

ib.disconnect()
