import { Box, FormControl, FormLabel, TextField, Typography } from "@mui/material";
import { LoadingButton } from '@mui/lab';
import { FieldValues, useForm } from "react-hook-form";
import { Link, useNavigate } from "react-router-dom";

export default function Login() {
    const navigate = useNavigate();
    const { register, handleSubmit, formState: { isSubmitting, errors } } = useForm({
        mode: 'onSubmit'
      });

      async function submitForm(data: FieldValues) {
        try {
        //   await dispatch(signInUser(data));
        //   navigate(location.state?.from || '/catalog');
        } catch (error) {
          console.log(error);
        }
      }

    return (
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
                //sx={{ ariaLabel: 'email' }}
                {...register('username', { required: 'Username is required.' })}
                error={!!errors.username}
                helperText={errors?.username?.message}
              />
            </FormControl>
            <FormControl>
              <TextField
                placeholder="••••••"
                type="password"
                autoFocus
                fullWidth
                {...register('password', { required: 'Password is required.' })}
                error={!!errors.password}
                helperText={errors?.password?.message}
              />
            </FormControl>

            <LoadingButton
              loading={isSubmitting}
              type="submit"
              fullWidth
              variant="contained"
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
    );
}