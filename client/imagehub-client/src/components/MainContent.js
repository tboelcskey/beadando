import React from "react";
import Feed from "./Feed";
import Profile from "./Profile";
import Upload from "./Upload";
import MenuAppBar from "./MenuAppBar.js";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";
import Search from "./Search";

export default function MainContent(props) {
  let data = props.userData;
  return (
    <Router>
      <div>
        <MenuAppBar />
        <Switch>
          <Route exact path="/" component={Feed} />
          <Route exact path="/feed" component={Feed} />
          <Route exact path="/profile" render={(props) => <Profile {...props} userData={data}/>} />
          <Route exact path="/upload" component={Upload} />
          <Route exact path="/search" component={Search} />
        </Switch>
      </div>
    </Router>
  );
}