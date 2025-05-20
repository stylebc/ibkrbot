# Use official Python slim image
FROM python:3.11-slim

# Set working directory inside the container
WORKDIR /app

# Install required system dependencies (for ib_insync + TWS comms)
RUN apt-get update && apt-get install -y \
    curl gnupg2 libglib2.0-0 libnss3 libxss1 libasound2 libatk1.0-0 libatk-bridge2.0-0 libcups2 libxcomposite1 libxrandr2 libxdamage1 libxfixes3 \
    && rm -rf /var/lib/apt/lists/*

# Copy dependency file and install packages
COPY requirements.txt .
RUN pip install --no-cache-dir -r requirements.txt

# Copy the full app directory
COPY app/ ./app/

# Set environment variable for FastAPI
ENV PYTHONUNBUFFERED=1

# Expose the FastAPI port
EXPOSE 8000

# Start the app using Uvicorn
CMD ["uvicorn", "app.main:app", "--host", "0.0.0.0", "--port", "8000"]
