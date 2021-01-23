import React from "react";
import { Container, Menu } from "semantic-ui-react";

export default function NavBar() {
  return (
    <Menu inverted fixed="top">
      <Container>
        <Menu.Item header>
          <img src="/logo192.png" alt="logo" style={{ marginRight: "10px" }} />
          Tfnizer
        </Menu.Item>
      </Container>
    </Menu>
  );
}
