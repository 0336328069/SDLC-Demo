@echo off
echo Setting up SDLC Frontend (Next.js 15)...

cd src\frontend

echo.
echo 1. Installing dependencies...
npm install

echo.
echo 2. Starting development server...
echo Frontend will be available at: http://localhost:3000
echo.
npm run dev

pause 