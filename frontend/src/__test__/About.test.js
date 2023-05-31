import About from "../Pages/About";
import '@testing-library/jest-dom'
import { render, screen } from '@testing-library/react';

it('renders a joke', () => {
    render(<About />);
    const titleText = screen.getByText(/About/i);
    expect(titleText).toBeInTheDocument();
  });