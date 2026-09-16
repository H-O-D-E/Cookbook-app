import { Link } from "react-router"
import { Book } from "lucide-react"
import Login from "../pages/authpages/Login";

function Navbar() {
  return (
    <nav className="grid grid-cols-3 items-start px-4 pt-3 bg-base-100 h-16">
      <div className="flex justify-start">
        <Link to="/" className="btn btn-ghost text-xl">Fork-IT</Link>
      </div>
      <div />
      <div className="flex gap-3 justify-end items-center">
        <div className="nav-options">
          <Link to="/recipebooks" className="btn btn-ghost"><Book /></Link>
          <label className="toggle text-base-content">
            <input type="checkbox" value="synthwave" className="theme-controller" />
            <svg aria-label="sun" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24"><g strokeLinejoin="round" strokeLinecap="round" strokeWidth="2" fill="none" stroke="currentColor"><circle cx="12" cy="12" r="4"></circle><path d="M12 2v2"></path><path d="M12 20v2"></path><path d="m4.93 4.93 1.41 1.41"></path><path d="m17.66 17.66 1.41 1.41"></path><path d="M2 12h2"></path><path d="M20 12h2"></path><path d="m6.34 17.66-1.41 1.41"></path><path d="m19.07 4.93-1.41 1.41"></path></g></svg>
            <svg aria-label="moon" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24"><g strokeLinejoin="round" strokeLinecap="round" strokeWidth="2" fill="none" stroke="currentColor"><path d="M12 3a6 6 0 0 0 9 9 9 9 0 1 1-9-9Z"></path></g></svg>
          </label>
          <Link to="/login" className="btn btn-ghost">Sign Out</Link>
        </div>
      </div>
    </nav>
  )
}

export default Navbar;