import React from "react";
import {Route, Switch} from "react-router-dom";

import asyncComponent from "util/asyncComponent";

const App = ({match}) => (
  <div className="gx-main-content-wrapper">
    <Switch>
      <Route path={`${match.url}`} exact component={asyncComponent(() => import('./dashboard/index'))}/>
      <Route path={`${match.url}category`} exact component={asyncComponent(() => import('./category/index'))}/>
      <Route path={`${match.url}subcategory`} exact component={asyncComponent(() => import('./subcategory/index'))}/>
    </Switch>
  </div>
);

export default App;
