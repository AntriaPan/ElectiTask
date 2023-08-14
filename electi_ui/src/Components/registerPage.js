// Import required components and libraries
import * as React from "react";
import Avatar from "@mui/material/Avatar";
import Button from "@mui/material/Button";
import CssBaseline from "@mui/material/CssBaseline";
import TextField from "@mui/material/TextField";
import Link from "@mui/material/Link";
import Grid from "@mui/material/Grid";
import Box from "@mui/material/Box";
import LockOutlinedIcon from "@mui/icons-material/LockOutlined";
import Typography from "@mui/material/Typography";
import Container from "@mui/material/Container";
import { createTheme, ThemeProvider } from "@mui/material/styles";
import "react-toastify/dist/ReactToastify.css";
import { ToastContainer, toast } from 'react-toastify';
import axios from "axios";
import {useNavigate} from 'react-router-dom';

const defaultTheme = createTheme();

// Define the SignUp component
export default function SignUp() {
  const navigate = useNavigate();

  // Function to handle form submission
  const handleSubmit = (event) => {
    event.preventDefault();
    const data = new FormData(event.currentTarget);
    
    // Extract input values from form data
    let emailInput = data.get("email");
    let passwordlInput = data.get("password");
    let  usernameInput = data.get("username");
    // Check if all required fields are provided
    if (!data.get("email") || !data.get("password") || !data.get("username")) {
      // Display an error toast if any field is missing
      toast.error("Please fill all the details !", {
        position: toast.POSITION.TOP_RIGHT,
      });
      return;
    }
    
    // Send a POST request to register a new user
    axios({
      method: "POST",
      url: `http://localhost:5283/api/Users/register`,
      data: {
        username: usernameInput,
        password: passwordlInput,
        email: emailInput
      },
      config: {
        headers: {
          Accept: "application/json",
          "Content-Type": "application/json",
        },
      },
    })
      .then((response) => {
        // Display a success toast and navigate after a delay
        console.log(response);
        if(response.status === 200){
          toast.success("Succesful account creation", {
            position: toast.POSITION.TOP_RIGHT,
          });
          setTimeout(() => {
            navigate('/'); // Navigate again after 5 seconds
          }, 6000); // 5000 milliseconds = 5 seconds      
        }
        else {
            // Display an error toast for unsuccessful registration
            toast.error("Unsuccesful Account Creation! The username should be unique.", {
            position: toast.POSITION.TOP_RIGHT,
          });
        }
      })
      .catch((error) => {
        // Display an error toast for network or username duplication issues
        toast.error("Unsuccesful Account Creation! The username should be unique.", {
            position: toast.POSITION.TOP_RIGHT,
          });      
        
    });
  };

  // Return JSX for rendering the component
  return (
    <ThemeProvider theme={defaultTheme}>
      <Container component="main" maxWidth="xs">
      <ToastContainer />
        <CssBaseline />
        <Box
          sx={{
            marginTop: 8,
            display: "flex",
            flexDirection: "column",
            alignItems: "center",
          }}
        >
          <Avatar sx={{ m: 1, bgcolor: "secondary.main" }}>
            <LockOutlinedIcon />
          </Avatar>
          <Typography component="h1" variant="h5">
            Sign up
          </Typography>
          <Box
            component="form"
            noValidate
            onSubmit={handleSubmit}
            sx={{ mt: 3 }}
          >
            <Grid container spacing={2}>
              <Grid item xs={12} >
                <TextField
                  autoComplete="given-name"
                  name="username"
                  required
                  fullWidth
                  id="username"
                  label="User Name"
                  autoFocus
                />
              </Grid>
              <Grid item xs={12}>
                <TextField
                  required
                  fullWidth
                  id="email"
                  label="Email Address"
                  name="email"
                  autoComplete="email"
                />
              </Grid>
              <Grid item xs={12}>
                <TextField
                  required
                  fullWidth
                  name="password"
                  label="Password"
                  type="password"
                  id="password"
                  autoComplete="new-password"
                />
              </Grid>
            </Grid>
            <Button
              type="submit"
              fullWidth
              variant="contained"
              sx={{ mt: 3, mb: 2 }}
            >
              Sign Up
            </Button>
            <Grid container justifyContent="flex-end">
              <Grid item>
                <Link href="./" variant="body2">
                  Already have an account? Sign in
                </Link>
              </Grid>
            </Grid>
          </Box>
        </Box>
      </Container>
    </ThemeProvider>
  );
}


