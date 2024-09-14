import { Alert, AlertTitle, Box, FormControl, FormLabel, List, ListItem, ListItemText, Stack, TextField, Typography } from "@mui/material";
import { LoadingButton } from '@mui/lab';
import { FieldValues, useForm } from "react-hook-form";
import { Link, useLocation, useNavigate } from "react-router-dom";
import { styled } from '@mui/material/styles';
import MuiCard from '@mui/material/Card';
import { signInUser } from './accountSlice';
import { useAppDispatch, useAppSelector } from "../../App/configureStore";
import { useState } from "react";

export default function Login() {
  const navigate = useNavigate();
  const location = useLocation();
  const dispatch = useAppDispatch();
  let { user } = useAppSelector(state => state.account);
  const { register, handleSubmit, formState: { isSubmitting, errors } } = useForm({
    mode: 'onSubmit'
  });
  const [validationErrors, setValidationErrors] = useState<string[]>([]);

  async function submitForm(data: FieldValues) {
      await dispatch(signInUser(data))
        .then((data: any) => {
          if (data.payload?.error)
          {
            if (data.payload?.error.status === 401)
            {
              setValidationErrors(["login or password is not correct!"]);
            }
            else
            {
              setValidationErrors(["network error."]);
            }
          }
          else if (data.payload.email)
          {
            setValidationErrors([]);
            navigate('/home');
          }
        });
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
          Login
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
              autoFocus
              fullWidth
              sx={{ ariaLabel: 'username' }}
              {...register('username', { required: 'Username is required.' })}
              error={!!errors.username}
              helperText={errors?.username?.message?.toString()}
            />
          </FormControl>
          <FormControl>
            <FormLabel htmlFor="password">Password</FormLabel>
            <TextField
              placeholder="••••••"
              type="password"
              fullWidth
              sx={{ ariaLabel: 'password' }}
              {...register('password', { required: 'Password is required.' })}
              error={!!errors.password}
              helperText={errors?.password?.message?.toString()}
            />
          </FormControl>
          {
            validationErrors.length > 0 &&
            <Alert severity="error">
              <AlertTitle>Errors:</AlertTitle>
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
            Log in
          </LoadingButton>
          <Typography sx={{ textAlign: 'center' }}>
            Don&apos;t have an account?{' '}
            <span>
              <Link to="/register">
                Register
              </Link>
            </span>
          </Typography>
        </Box>
      </Card>
    </>
  );
}
