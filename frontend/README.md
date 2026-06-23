# Ecommerce Frontend

> React SPA for the full-stack e-commerce platform. Consumes the [ASP.NET Core backend API](../backend/README.md).

![React](https://img.shields.io/badge/React-17-61DAFB?logo=react)
![Redux Toolkit](https://img.shields.io/badge/Redux_Toolkit-latest-764ABC?logo=redux)
![React Router](https://img.shields.io/badge/React_Router-v6-CA4245?logo=reactrouter)
![License](https://img.shields.io/badge/license-MIT-green)

---

## Tech Stack

| Layer | Technology |
|---|---|
| UI | React 17 + React Bootstrap / Bootstrap 5 |
| Routing | React Router v6 |
| State | Redux Toolkit |
| HTTP | Axios |
| Payments | Stripe.js |
| SEO | React Helmet |

---

## Getting Started

### Prerequisites

- Node.js 18+
- Backend API running (see [backend setup](../backend/README.md))

### Run locally

```bash
npm install
npm start
```

App runs at `http://localhost:3000`.

---

## Scripts

| Command | Description |
|---|---|
| `npm start` | Start development server |
| `npm run build` | Production build |
| `npm test` | Run tests |

---

## Project Structure

```
src/
├── components/     # Reusable UI components
│   └── layout/     # Shell, nav, footer
├── App.jsx         # Root component and route definitions
└── index.jsx       # Entry point
```

---

## License

MIT
