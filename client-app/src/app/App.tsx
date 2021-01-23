import NavBar from "./layout/NavBar";
import React, { useEffect, useState } from "react";
import axios, { AxiosResponse } from "axios";
import {
  Button,
  Container,
  Form,
  Grid,
  Label,
  Message,
  Segment,
} from "semantic-ui-react";

function App() {
  axios.defaults.baseURL = process.env.REACT_APP_AXIOS_BASEURL;
  const [tfn, setTfn] = useState("");
  const [isValidInput, setIsValidInput] = useState(false);
  const [submitting, setSubmitting] = useState(false);
  const [message, setMessage] = useState("");
  const [validationResult, setValidationResult] = useState(false);

  useEffect(() => {
    if (tfn.length > 0 && tfn.length !== 8 && tfn.length !== 9)
      setIsValidInput(true);
    else setIsValidInput(false);
  }, [setIsValidInput, tfn.length]);

  // Adding delay to show loader when submitting
  const sleep = (ms: number) => (response: AxiosResponse) =>
    new Promise<AxiosResponse>((resolve) =>
      setTimeout(() => resolve(response), ms)
    );

  function handleSubmit() {
    setSubmitting(true);
    setMessage(`Processing [${tfn}]`);
    axios
      .post<AxiosResponse>(`/tfn/${tfn}`)
      .then(sleep(1000))
      .then((response) => {
        if (response.data.toString() === "Valid") {
          setValidationResult(true);
          setMessage("Congratulations, the provided TFN is Valid.");
        } else if (response.data.toString() === "Invalid") {
          setValidationResult(false);
          setMessage("Sorry, the provided TFN is Invalid.");
        } else if (response.data.toString() === "Linked") {
          setValidationResult(false);
          setMessage("Multiple attempts for similar values are not allowed!!!");
        }
        setSubmitting(false);
      })
      .catch(() => {
        setTfn("");
        setSubmitting(false);
        setMessage("Oops, something went wrong");
      });
  }

  return (
    <>
      <NavBar />
      <Container style={{ marginTop: "5em" }}>
        <Grid textAlign="center">
          <Grid.Column computer={6} mobile={16}>
            <Segment clearing textAlign="left" raised>
              <Label as="a" color="blue" ribbon="right">
                TFN validation tool
              </Label>
              <Form
                onSubmit={handleSubmit}
                success={validationResult}
                error={!validationResult}
              >
                <Form.Input
                  onChange={(e) => {
                    setMessage("");
                    setTfn(
                      e.target.value.length > 9
                        ? e.target.value.substr(0, 9)
                        : e.target.value
                    );
                  }}
                  type="Number"
                  label="Tax File Number"
                  placeholder="Please enter TFN here"
                  error={isValidInput}
                  value={tfn}
                  disabled={submitting}
                />
                {message && (
                  <Message
                    success={!submitting && validationResult}
                    error={!submitting && !validationResult}
                    content={message}
                  />
                )}
                <Button
                  disabled={isValidInput || tfn.length === 0 || submitting}
                  floated="right"
                  loading={submitting}
                  positive={submitting}
                >
                  Validate
                </Button>
              </Form>
            </Segment>
          </Grid.Column>
        </Grid>
      </Container>
    </>
  );
}

export default App;
