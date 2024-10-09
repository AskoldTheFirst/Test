import { Box, Button, FormControl, FormLabel, TextField, Typography } from "@mui/material";
import { useState } from "react";
import agent from "../../Biz/agent";

export default function AboutPage() {
  const [email, setEmail] = useState<string>('');
  const [message, setMessage] = useState<string>('');
  const [emailError, setEmailError] = useState<string>('');
  const [messageError, setMessageError] = useState<string>('');

  function sendHandler() {
    if (email.length === 0) {
      setEmailError('Email should not be empty.');
      return;
    }

    const emailPattern = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
    const isValid = emailPattern.test(email);
    if (!isValid) {
      setEmailError('Email address is not valid.');
      return;
    }

    if (message.length === 0) {
      setMessageError('Message should not be empty.');
      return;
    }

    agent.App.postMessage(email, message)
      .then(() => {
        window.alert('Thank you! Your message was successfully sent.');
        setEmail('');
        setMessage('');
      });
  }

  return (
    <>
      {/* TODO - How to center without <center>? */}
      <center>
        <Box sx={{ width: '460px', marginTop: 8, textAlign: 'center', alignContent: 'center', alignItems: 'center', alignSelf: 'center' }}>
          <Typography variant="h6">
            This is a simple application for testing. Please check it out and if you
            have any questions or ideas, please send them here:
          </Typography>
          <FormControl sx={{ textAlign: 'left', marginTop: 4 }}>
            <FormLabel htmlFor="email">Email:</FormLabel>
            <TextField
              autoFocus
              fullWidth
              sx={{ ariaLabel: 'email', width: '440px' }}
              error={emailError.length > 0}
              helperText={emailError}
              size="small"
              value={email}
              onChange={(e) => {
                if (emailError.length > 0)
                  setEmailError('');
                setEmail(e.target.value);
              }}
            />
          </FormControl>

          <FormControl sx={{ textAlign: 'left', marginTop: 2 }}>
            <FormLabel htmlFor="message">Message:</FormLabel>
            <TextField
              fullWidth
              sx={{ ariaLabel: 'message', width: '440px' }}
              error={messageError.length > 0}
              helperText={messageError}
              size="small"
              multiline={true}
              rows={7}
              value={message}
              onChange={(e) => {
                if (messageError.length > 0)
                  setMessageError('');
                setMessage(e.target.value);
              }}
            />
          </FormControl>
          {/* TODO - How to left? */}
          <Button onClick={sendHandler} sx={{ border: 1, marginTop: 2, alignContent: 'start', alignItems: 'start', alignSelf: 'start', textAlign: 'left' }}>Send</Button>
        </Box>
      </center>
    </>
  );
}
