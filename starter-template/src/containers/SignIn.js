import React, { useEffect, useState } from "react";
import { Button, Card, Form, Input, Row, message } from "antd";
import axios from "axios";
import { API_URL, authUser } from "../constanst";

const SignIn = () => {
  // const [user,setUser] =  useState({ username : "" , pass : ""});

  useEffect(() => {});

  const onFinishFailed = (errorInfo) => {};

  const onFinish = (values) => {
    console.log(values);
    const newData = {
      username: values.username,
      password: values.password,
    };
    console.log("newData", newData);
    axios.post(API_URL+"/api/Auth/login", newData).then((res) => {
      console.log("res", res.data);

      if (res.status === 200) {
        console.log(res.data.token);
        localStorage.setItem("user", res.data);
        console.log("AUTH : "+authUser);
        window.location.assign("/");
      } else {
        /// call api error
        window.alert(res.data.message);
      }
    });
  };

  return (
    <Row
      type="flex"
      justify="center"
      align="middle"
      style={{ minHeight: "100vh" }}
      className="container"
    >
      <Card>
        <Form
          initialValues={{ remember: true }}
          name="basic"
          onFinish={onFinish}
          onFinishFailed={onFinishFailed}
          className="gx-signin-form gx-form-row0"
        >
          <Form.Item
            // initialValue="demo@example.com"
            rules={[
              { required: true, message: "The input is not valid E-mail!" },
            ]}
            name="username"
          >
            <Input placeholder="Email" />
          </Form.Item>
          <Form.Item
            // initialValue="demo#123"
            rules={[{ required: true, message: "Please input your Password!" }]}
            name="password"
          >
            <Input type="password" placeholder="Password" />
          </Form.Item>
          {/* <Form.Item>
                <Checkbox><IntlMessages id="appModule.iAccept"/></Checkbox>
                <span className="gx-signup-form-forgot gx-link"><IntlMessages
                  id="appModule.termAndCondition"/></span>
              </Form.Item> */}
          <Form.Item className="align-center">
            <Row 
              type="flex"
              justify="center"
              align="middle"
              className="container">
                
            <Button type="primary" className="gx-mb-0" htmlType="submit">
              เข้าสู่ระบบ
            </Button>
            </Row>
          </Form.Item>
        </Form>
      </Card>
    </Row>
  );
};

export default SignIn;
