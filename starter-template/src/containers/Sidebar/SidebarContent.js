import React from "react";
import { Menu } from "antd";
import { Link } from "react-router-dom";

import CustomScrollbars from "util/CustomScrollbars";
import SidebarLogo from "./SidebarLogo";
import UserProfile from "./UserProfile";
import AppsNavigation from "./AppsNavigation";
import {
  NAV_STYLE_NO_HEADER_EXPANDED_SIDEBAR,
  NAV_STYLE_NO_HEADER_MINI_SIDEBAR,
  THEME_TYPE_LITE,
} from "../../constants/ThemeSetting";
import IntlMessages from "../../util/IntlMessages";
import { useSelector } from "react-redux";
import SubMenu from "antd/lib/menu/SubMenu";

const SidebarContent = ({ sidebarCollapsed, setSidebarCollapsed }) => {
  const { navStyle, themeType } = useSelector(({ settings }) => settings);
  const pathname = useSelector(({ common }) => common.pathname);

  const getNoHeaderClass = (navStyle) => {
    if (
      navStyle === NAV_STYLE_NO_HEADER_MINI_SIDEBAR ||
      navStyle === NAV_STYLE_NO_HEADER_EXPANDED_SIDEBAR
    ) {
      return "gx-no-header-notifications";
    }
    return "";
  };

  const getNavStyleSubMenuClass = (navStyle) => {
    if (navStyle === NAV_STYLE_NO_HEADER_MINI_SIDEBAR) {
      return "gx-no-header-submenu-popup";
    }
    return "";
  };
  const selectedKeys = pathname.substr(1);
  const defaultOpenKeys = selectedKeys.split("/")[1];

  return (
    <>
      <SidebarLogo
        sidebarCollapsed={sidebarCollapsed}
        setSidebarCollapsed={setSidebarCollapsed}
      />
      <div className="gx-sidebar-content">
        <div
          className={`gx-sidebar-notifications ${getNoHeaderClass(navStyle)}`}
        >
          {/* <UserProfile /> */}
          {/* <AppsNavigation /> */}
          <b>เมนู</b>
        </div>
        <CustomScrollbars className="gx-layout-sider-scrollbar">
          <Menu
            defaultOpenKeys={[defaultOpenKeys]}
            selectedKeys={[selectedKeys]}
            theme={themeType === THEME_TYPE_LITE ? "lite" : "dark"}
            mode="inline"
          >
            <Menu.Item key="sample">
              <Link to="/">
                <i className="icon icon-widgets" />
                ภาพรวมระบบ
              </Link>
            </Menu.Item>
            <SubMenu
              key="dashboard"
              popupClassName={getNavStyleSubMenuClass(navStyle)}
              title={
                <span>
                  <i className="icon icon-dasbhoard" />
                  <span>
                    อะไหล่
                  </span>
                </span>
              }
            >
              <Menu.Item key="category">
                <Link to="/category">
                  <i className="icon icon-crypto" />
                  หมวดหมูอะไหล่
                </Link>
              </Menu.Item>
              <Menu.Item key="subcategory">
                <Link to="/subcategory">
                  <i className="icon icon-crm" />
                  ประเภทอะไหล่
                </Link>
              </Menu.Item>
            </SubMenu>
            
            <Menu.Item key="Mat">
              <Link to="/Material">
                <i className="icon icon-widgets" />
                รายการอะไหล่
              </Link>
            </Menu.Item>

            <Menu.Item key="Stock">
              <Link to="/stock">
                <i className="icon icon-widgets" />
                คลังอะไหล่
              </Link>
            </Menu.Item>
            <Menu.Item key="use">
              <Link to="/invoice">
                <i className="icon icon-widgets" />
                เบิกอะไหล่ออก
              </Link>
            </Menu.Item>
          </Menu>
        </CustomScrollbars>
      </div>
    </>
  );
};

export default React.memo(SidebarContent);
