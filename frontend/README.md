# Ecommerce Frontend

> React SPA for the full-stack e-commerce platform. Consumes the [ASP.NET Core backend API](../backend/README.md).

![Vite](https://img.shields.io/badge/Vite-6-646CFF?logo=vite)
![React](https://img.shields.io/badge/React-17-61DAFB?logo=react)
![Tailwind CSS](https://img.shields.io/badge/Tailwind_CSS-v4-06B6D4?logo=tailwindcss)
![Redux Toolkit](https://img.shields.io/badge/Redux_Toolkit-latest-764ABC?logo=redux)
![License](https://img.shields.io/badge/license-MIT-green)

---

## Tech Stack

| Layer | Technology |
|---|---|
| Build | Vite 6 + Tailwind CSS v4 |
| UI | React 17 |
| Styling | Tailwind CSS v4 |
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
npm run dev
```

App runs at `http://localhost:5173`.

---

## Scripts

| Command | Description |
|---|---|
| `npm run dev` | Start development server (Vite) |
| `npm run build` | Production build |
| `npm run preview` | Preview production build locally |

---

## Project Structure

```
src/
├── components/
│   ├── layout/     # Header, Footer, Home, Loader
│   ├── product/    # Product, ProductDetail
│   └── products/   # Products grid wrapper
├── App.css         # Tailwind imports + custom styles (star ratings, scrollbar)
├── App.jsx         # Root component and route definitions
└── index.jsx       # Entry point
```

## Styling

This project uses **Tailwind CSS v4** with utilities-first approach:

- **Config**: `@theme` block in [src/App.css](src/App.css) defines custom colors (`--color-brand`, `--color-navy`)
- **No `tailwind.config.js` needed** — v4 scans content automatically and config lives in CSS
- **Custom utilities**: Star-rating display (FontAwesome width-overlay technique) and scrollbar styling in App.css
- **Bootstrap removed**: Migrated from CRA + React Bootstrap to Vite + Tailwind (2025 migration)

---

## License

MIT
