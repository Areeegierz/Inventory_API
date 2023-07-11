import { Button, Form, Input, Modal, Row } from "antd";
import axios from "axios";
import { API_URL, authUser } from "../../constanst";

const CategoryEdit = (data) => {

  const [form] = Form.useForm();
  const onFinishFailed = (errorInfo) => {};
  const onFinish = (values) => {
    const category = {
      id: values.id,
      name: values.name,
      createBy: authUser.id,
    };
    axios.post(API_URL + "/api/Category/Update", category).then((res) => {
      console.log(res);
      window.location.reload();
    });
  };
  form.setFieldsValue({id:data.data.id});
  form.setFieldsValue({name:data.data.name});
  
  return (
    
    <Form
    form={form}
    name="validateOnly"
    layout="vertical"
    autoComplete="off"
    initialValues={{ remember: true }}
    onFinish={onFinish}
    onFinishFailed={onFinishFailed}
    className="gx-signin-form gx-form-row0"
  >
    {/* <p>{JSON.stringify(data.data)}</p> */}
    <Form.Item style={{display:"none"}}
      name="id"
      rules={[
        {
          required: true,
        },
      ]}
    >
    <Input/>
  </Form.Item>
    <Form.Item
      name="name"
      label="ชื่อหมวดหมู่อะไหล่"
      rules={[
        {
          required: true,
        },
      ]}
    >
      <Input/>
    </Form.Item>
    <Form.Item className="align-center">
      <Row
        type="flex"
        justify="center"
        align="middle"
        className="container"
      >
        <Button type="primary" className="gx-mb-0" htmlType="submit">
          บันทึก
        </Button>
      </Row>
    </Form.Item>
  </Form>
  );
};

export default CategoryEdit;
