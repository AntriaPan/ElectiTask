// Import necessary components and libraries
import * as React from 'react';
import Avatar from '@mui/material/Avatar';
import Button from '@mui/material/Button';
import CssBaseline from '@mui/material/CssBaseline';
import TextField from '@mui/material/TextField';
import Link from '@mui/material/Link';
import Grid from '@mui/material/Grid';
import Box from '@mui/material/Box';
import LockOutlinedIcon from '@mui/icons-material/LockOutlined';
import Typography from '@mui/material/Typography';
import Container from '@mui/material/Container';
import { createTheme, ThemeProvider } from '@mui/material/styles';
import "react-toastify/dist/ReactToastify.css";
import { ToastContainer, toast } from 'react-toastify';
import axios from "axios";
import {useNavigate} from 'react-router-dom';

const defaultTheme = createTheme();

// Define the SignIn component
export default function SignIn() {
  const navigate = useNavigate();

    // Function to handle user login
    function handleLogin(username,password) {
        axios({
          method: "POST",
          url: `http://localhost:5283/api/Users/login`,
          data: {
            username: username,
            password: password,
          },
          config: {
            headers: {
              Accept: "application/json",
              "Content-Type": "application/json",
            },
          },
        })
          .then((response) => {
            // Check the response status for login success or failure
            if(response.status === 200){
                // Redirect to '/books' upon successful login
                navigate('/books');
            }
            else {
                // Display error toast message for unsuccessful login
                toast.error("Unsuccesful Login", {
                position: toast.POSITION.TOP_RIGHT,
              });
            }
          })
          .catch((error) => {
            // Display error toast message for network or wrong credentials
            toast.error("Unsuccesful Login - Wrong credentials", {
                position: toast.POSITION.TOP_RIGHT,
              });          
        });
      }

  // Function to handle form submission
  const handleSubmit = (event) => {
    event.preventDefault();
    const data = new FormData(event.currentTarget);
    let usernameInput = data.get('username');
    let passInput = data.get('password');
    console.log({
      username: usernameInput,
      password: passInput,
    });
    // Check if username and password are provided
    if (!usernameInput || !passInput) {
            // Display an error toast if inputs are missing
          toast.error("Please fill all the details !", {
          position: toast.POSITION.TOP_CENTER,
        });
        return;
      }
      // Call the handleLogin function with input values
      handleLogin(usernameInput,passInput);
  };

  // Render the component
  return (
    <ThemeProvider theme={defaultTheme}>
      <Container component="main" maxWidth="xs">
        <ToastContainer />
        <CssBaseline />
        <Box
          sx={{
            marginTop: 8,
            display: 'flex',
            flexDirection: 'column',
            alignItems: 'center',
          }}
        >
          <Avatar sx={{ m: 1, bgcolor: 'secondary.main' }}>
            <LockOutlinedIcon />
          </Avatar>
          <Typography component="h1" variant="h5">
            Sign in
          </Typography>
          <Box component="form" onSubmit={handleSubmit} noValidate sx={{ mt: 1 }}>
            <TextField
              margin="normal"
              required
              fullWidth
              id="username"
              label="Username"
              name="username"
              autoComplete="username"
              autoFocus
            />
            <TextField
              margin="normal"
              required
              fullWidth
              name="password"
              label="Password"
              type="password"
              id="password"
              autoComplete="current-password"
            />
          
            <Button
              type="submit"
              fullWidth
              variant="contained"
              sx={{ mt: 3, mb: 2 }}
            >
              Sign In
            </Button>
            <Grid container>
              <Grid item>
                <Link href="./register" variant="body2">
                  {"Don't have an account? Sign Up"}
                </Link>
              </Grid>
            </Grid>
          </Box>
        </Box>
      </Container>
    </ThemeProvider>
  );
}