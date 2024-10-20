import { Box, Button, Container, FormControl, FormLabel, TextField, Typography } from "@mui/material";
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
      <Container sx={{ display: 'flex', justifyContent: 'center' }}>
        <Box sx={{ width: '460px', marginTop: 8, textAlign: 'center' }}>
          <Typography variant="h6" sx={{ fontSize: '11pt' }}>
            This is just an example of a simplified way to test online.
          </Typography>
          <Typography variant="h6" sx={{ fontSize: '11pt' }}>
            If you want to have it in some more sophisticated, customized, or domain-oriented way, please contact me and share your requirements, ideas, thoughts, etc.
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

          <div style={{ marginTop: '24px', marginRight: '10px', textAlign: 'end' }}>
            <Button onClick={sendHandler} sx={{ border: 1 }}>Send</Button>
          </div>

          <div style={{ marginTop: '60px', marginRight: '10px', textAlign: 'end' }}>
            <Typography sx={{ fontSize: '10px' }}>
              or email to:
            </Typography>
            <a href="mailto: vkramar.biz@gmail.com" style={{ fontSize: '10px' }}>
              vkramar.biz@gmail.com
            </a>
          </div>
        </Box>
      </Container>
    </>
  );
}
