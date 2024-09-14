import { Alert, AlertTitle, Box, FormControl, FormLabel, List, ListItem, ListItemText, Stack, TextField, Typography } from "@mui/material";
import { LoadingButton } from '@mui/lab';
import { FieldValues, useForm } from "react-hook-form";
import { useNavigate } from "react-router-dom";
import { styled } from '@mui/material/styles';
import MuiCard from '@mui/material/Card';
import agent from "../../Biz/agent";
import { useState } from "react";

export default function Login() {
  const navigate = useNavigate();
  const { register, handleSubmit, setError, formState: { isSubmitting, errors } } = useForm({
    mode: 'onSubmit'
  });
  const [validationErrors, setValidationErrors] = useState<string[]>([]);

  async function submitForm(data: FieldValues) {
    agent.Account.register(data).then((data) => {
      console.log("REG: " + data);
      setValidationErrors([]);
      navigate('/login');
    })
      .catch(error => handleApiErrors(error));
  }

  function handleApiErrors(errors: any) {
    if (errors) {
      errors.forEach((error: string) => {
        if (error.includes('Password')) {
          setError('password', { message: error });
        } else if (error.includes('Email')) {
          setError('email', { message: error });
        } else if (error.includes('Username')) {
          setError('username', { message: error });
        }
      });
    }
  }

  const Card = styled(MuiCard)(({ theme }) => ({
    display: 'flex',
    flexDirection: 'column',
    alignSelf: 'center',
    width: '100%',
    padding: theme.spacing(4),
    gap: theme.spacing(2),
    margin: 'auto',
    [theme.breakpoints.up('sm')]: {
      maxWidth: '450px',
    },
    boxShadow:
      'hsla(220, 30%, 5%, 0.05) 0px 5px 15px 0px, hsla(220, 25%, 10%, 0.05) 0px 15px 35px -5px',
    ...theme.applyStyles('dark', {
      boxShadow:
        'hsla(220, 30%, 5%, 0.5) 0px 5px 15px 0px, hsla(220, 25%, 10%, 0.08) 0px 15px 35px -5px',
    }),
  }));

  return (
    <>
      <Card variant="outlined" sx={{marginTop: 10}}>
        <Typography
          component="h1"
          variant="h4"
          sx={{ width: '100%', fontSize: 'clamp(2rem, 10vw, 2.15rem)' }}
        >
          Register
        </Typography>
        <Box
          component="form"
          onSubmit={handleSubmit(submitForm)}
          noValidate
          sx={{
            display: 'flex',
            flexDirection: 'column',
            width: '100%',
            gap: 2,
          }}
        >
          <FormControl>
            <FormLabel htmlFor="username">Username</FormLabel>
            <TextField
              id="userName"
              autoFocus
              fullWidth
              sx={{ ariaLabel: 'username' }}
              {...register('username', { required: 'Username is required.' })}
              error={!!errors.username}
              helperText={errors.username?.message?.toString()}
            />
          </FormControl>

          <FormControl>
            <FormLabel htmlFor="email">Email</FormLabel>
            <TextField
              fullWidth
              sx={{ ariaLabel: 'email' }}
              {...register('email', {
                required: 'Email is required.',
                pattern: {
                  value: /^\S+@\S+\.\S+$/,
                  message: 'Not a valid email address.'
                }})}
              error={!!errors.email}
              helperText={errors?.email?.message?.toString()}
            />
          </FormControl>

          <FormControl>
            <FormLabel htmlFor="password">Password</FormLabel>
            <TextField
              placeholder="••••••••"
              type="password"
              fullWidth
              sx={{ ariaLabel: 'password' }}
              {...register('password', {
                required: 'Password is required.',
                pattern: {
                  value: /(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,}/,
                  message: 'Must contain at least one number and one uppercase and lowercase letter, and at least 8 or more characters'
                }})}
              error={!!errors.password}
              helperText={errors?.password?.message?.toString()}
            />
          </FormControl>
          {
            validationErrors.length > 0 &&
            <Alert severity="error">
              <AlertTitle>Validation Errors</AlertTitle>
              <List>
                {validationErrors.map(error => (
                  <ListItem key={error}>
                    <ListItemText>{error}</ListItemText>
                  </ListItem>
                ))}
              </List>
            </Alert>
          }

          <LoadingButton
            loading={isSubmitting}
            type="submit"
            fullWidth
            variant="contained"
            sx={{marginTop: 1}}
          >
            Register
          </LoadingButton>
        </Box>
      </Card>
    </>
  );
}