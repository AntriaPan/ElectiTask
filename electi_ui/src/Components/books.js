// Import necessary components and libraries
import React, { useState, useEffect } from 'react';
import AppBar from '@mui/material/AppBar';
import Button from '@mui/material/Button';
import WavingHandIcon from '@mui/icons-material/WavingHand';
import Card from '@mui/material/Card';
import CardActions from '@mui/material/CardActions';
import CardContent from '@mui/material/CardContent';
import CardMedia from '@mui/material/CardMedia';
import CssBaseline from '@mui/material/CssBaseline';
import Grid from '@mui/material/Grid';
import Stack from '@mui/material/Stack';
import Box from '@mui/material/Box';
import Toolbar from '@mui/material/Toolbar';
import Typography from '@mui/material/Typography';
import Container from '@mui/material/Container';
import Link from '@mui/material/Link';
import { createTheme, ThemeProvider } from '@mui/material/styles';
import TextField from '@mui/material/TextField';
import { Dialog, DialogActions, DialogContent, DialogContentText, DialogTitle } from '@mui/material';
import "react-toastify/dist/ReactToastify.css";
import { ToastContainer, toast } from 'react-toastify';
import {useNavigate} from 'react-router-dom';
import { v4 as uuid } from "uuid";
import axios from "axios";


const defaultTheme = createTheme();

// Define a function component named Book
export default function Book() {

  // Initialize state variables using useState hook
  const navigate = useNavigate();
  const [books, setBooks] = useState([]);
  const [book, setBook] = useState();
  const [searchInput, setSearchInput] = useState('');
  const [openDialog, setOpenDialog] = useState(false);
  const [selectedBook, setSelectedBook] = useState(null);
  const [openEditDialog, setOpenEditDialog] = useState(false);
  const [editedBook, setEditedBook] = useState(null);
  const [openAddDialog, setOpenAddDialog] = useState(false);
  const [isSaveDisabled, setIsSaveDisabled] = useState(true);

  const [newBook, setNewBook] = useState({
    title: '',
    author: '',
    publishedYear: '',
    isbn: '',
  });

  // Define a function to update the disabled state of the save button
  useEffect(() => {
    setIsSaveDisabled(!newBook.title || !newBook.author || !newBook.isbn || !newBook.publishedYear);
  }, [newBook]);

  // Define functions to handle adding, editing, and deleting books
  const handleAddChange = (field, value) => {
    setNewBook((prevBook) => ({
      ...prevBook,
      [field]: value,
    }));
  };

  const handleAddSave = () => {
    if (!isSaveDisabled) {
      var suc = handleAddBook(newBook);
      if (suc === true){
      setOpenAddDialog(false);
      setNewBook({
        title: '',
        author: '',
        publishedYear: '',
        isbn: '',
      });
      window.location.reload();
    }
    }
  };
    
  const handleEditSave = () => {
    let suc = handleEditBook(editedBook);
    if (suc) {
      setOpenEditDialog(false);
    }
  };

  //Functions for controlling the modals
  const handleAddClick = () => {
    setOpenAddDialog(true);
  };

  const handleAddDialogClose = () => {
    setOpenAddDialog(false);
  };

    const handleDeleteClick = (book) => {
        setSelectedBook(book);
        console.log(book);
        setOpenDialog(true);
    };

    const handleDialogClose = () => {
        setOpenDialog(false);
        setSelectedBook(null);
    };

    const handleConfirmDelete = () => {
        handleDeleteMovie(selectedBook);
        setOpenDialog(false);
    };

    const handleEditClick = (book) => {
        setEditedBook({ ...book });
        setOpenEditDialog(true);
      };
    
      const handleEditDialogClose = () => {
        setOpenEditDialog(false);
        setEditedBook(null);
      };
  

      // Filter books based on search input
      const filteredBooks = books.filter(
        (book) =>
          book.title.toLowerCase().includes(searchInput.toLowerCase()) ||
          book.author.toLowerCase().includes(searchInput.toLowerCase())
      );
      
      // Handle validation and updating of edited book
      function handleEditBook(book) {
         // Validate ISBN: Should be 13 digits
         if (!/^\d{13}$/.test(book.isbn)) {
          toast.error("Invalid ISBN: ISBN should be 13 digits", {
            position: toast.POSITION.TOP_RIGHT,
          });
          return false;
        }

        // Validate Published Year: Should be a 4-digit number
        if (!/^\d{4}$/.test(book.publishedYear)) {
          toast.error("Invalid Published Year: Year should be a valid 4-digit number", {
            position: toast.POSITION.TOP_RIGHT,
          });
          return false;
        }
        axios({
          method: "put",
          url: `http://localhost:5283/api/Books/${book.id}`,
          data: {
            id: book.id,
            title: book.title,
            author: book.author,
            publishedYear: book.publishedYear,
            isbn: book.isbn,
          },
          config: {
            headers: {
              Accept: "application/json",
              "Content-Type": "application/json",
            },
          },
        })
          .then((response) => {
            console.log(response);
            toast.success("Book updated successfully", {
              position: toast.POSITION.TOP_RIGHT,
            });
            return true;
          })
          .catch((error) => {
            console.log("the error has occured: " + error);
            return false;
          });
    
        setBooks([...books, book]);
        return true;
      }

      // Handle validation and adding of new book
      function handleAddBook(book) {
        // Validate ISBN: Should be 13 digits
        if (!/^\d{13}$/.test(book.isbn)) {
          toast.error("Invalid ISBN: ISBN should be 13 digits", {
            position: toast.POSITION.TOP_RIGHT,
          });
          return false;
        }

        // Validate Published Year: Should be a 4-digit number
        if (!/^\d{4}$/.test(book.publishedYear)) {
          toast.error("Invalid Published Year: Year should be a valid year", {
            position: toast.POSITION.TOP_RIGHT,
          });
          return false;
        }
        const data = {
          id: uuid(),
          title: book.title,
          publishedYear: book.publishedYear,
          author: book.author,
          isbn: book.isbn,
        };
        axios({
          method: "post",
          url: "http://localhost:5283/api/Books",
          data: data,
          config: {
            headers: {
              Accept: "application/json",
              "Content-Type": "application/json",
            },
          },
        })
          .then((response) => {
            console.log(response);
            toast.success("book added successfully", {
              position: toast.POSITION.TOP_RIGHT,
            });
            return true;
          })
          .catch((error) => {
            console.log("the error has occured: " + error);
            return false;
          });
    
        setBooks([...books, data]);
        return true;
      }

      // Handle deletion of book
      function handleDeleteMovie(book) {
        axios.delete(`http://localhost:5283/api/books/${book.id}`).then(() => {
          toast.success("Book deleted successfully", {
            position: toast.POSITION.TOP_RIGHT,
          });
        });
    
        setBooks([...books.filter((x) => x.id !== book.id)]);
      }
  
      // Fetch initial book data using useEffect
      useEffect(() => {
      axios.get("http://localhost:5283/api/books").then((response) => {
        setBooks(response.data);
      });
     }, [books]);
      


  return (
    <ThemeProvider theme={defaultTheme}>
        <ToastContainer />
      <CssBaseline />
      <AppBar position="relative">
        <Toolbar>
          <WavingHandIcon sx={{ mr: 2 }} />
          <Button variant="outlined" sx={{ color: 'white', borderColor: 'white' }}  onClick={() => {
        navigate('/register')}}>
            Sign out
            </Button>
        </Toolbar>
      </AppBar>
      <main>
        <Box
          sx={{
            bgcolor: 'background.paper',
            pt: 8,
            pb: 6,
          }}
        >
          <Container maxWidth="sm">
            <Typography
              component="h1"
              variant="h2"
              align="center"
              color="text.primary"
              gutterBottom
            >
                Books
            </Typography>
            <Stack
              sx={{ pt: 4 }}
              direction="row"
              spacing={2}
              justifyContent="center"
            >
              <Button variant="contained" color="primary" onClick={handleAddClick} >
        Add a new book
      </Button>
            </Stack>
            <Stack
              sx={{ pt: 4 }}
              direction="row"
              spacing={2}
              justifyContent="center"
            >
                <TextField
      label="Searching for titles or authors"
      fullWidth
      value={searchInput}
      onChange={(e) => setSearchInput(e.target.value)}
      sx={{ marginBottom: 2 }}
    /></Stack>
          </Container>
        </Box>
        <Container sx={{ py: 8 }} maxWidth="md">
          <div>
          <Grid container spacing={4}>
          {filteredBooks.map((book) => {
            const imageKeyword = encodeURIComponent(book.title);
            const imageUrl = `https://source.unsplash.com/400x300/?${imageKeyword}`;
            
            return (
                <Grid item key={book.id} xs={12} sm={6} md={4}>
                <Card
                    sx={{ height: '100%', display: 'flex', flexDirection: 'column' }}
                >
                    <CardMedia
                    component="div"
                    sx={{
                        // 16:9
                        pt: '56.25%',
                    }}
                    image={imageUrl} 
                    />
                    <CardContent sx={{ flexGrow: 1 }}>
                    <Typography gutterBottom variant="h5" component="h2">
                        {book.title}
                    </Typography>
                    <Typography>
                        Author: {book.author}
                    </Typography>
                    <Typography>
                        Published Year: {book.publishedYear}
                    </Typography>
                    <Typography>
                        ISBN: {book.isbn}
                    </Typography>
                    </CardContent>
                    <CardActions>
                    <Button size="small" onClick={() => handleEditClick(book)}> Edit </Button>
                    <Button size="small" color="error" onClick={() => handleDeleteClick(book)}>Delete </Button>                    </CardActions>
                </Card>
                </Grid>
            );
            })}
          </Grid>
           {/* AlertDialog */}
      <Dialog open={openDialog} onClose={handleDialogClose}>
        <DialogTitle>Confirm Delete</DialogTitle>
        <DialogContent>
          <DialogContentText>
            Are you sure you want to delete the book "{selectedBook?.title}"?
          </DialogContentText>
        </DialogContent>
        <DialogActions>
          <Button onClick={handleDialogClose} color="primary">
            Keep
          </Button>
          <Button onClick={handleConfirmDelete} color="error">
            Delete
          </Button>
        </DialogActions>
      </Dialog>
       {/* Edit Dialog */}
      <Dialog open={openEditDialog} onClose={handleEditDialogClose}>
        <DialogTitle>Edit Book</DialogTitle>
        <DialogContent>
          <Box mb={2} mt={2}>
            <TextField
              label="Title"
              fullWidth
              defaultValue={editedBook?.title || ''}
              onChange={(e) => setEditedBook({ ...editedBook, title: e.target.value })}
            />
          </Box>
          <Box mb={2}>
            <TextField
              label="Author"
              fullWidth
              defaultValue={editedBook?.author || ''}
              onChange={(e) => setEditedBook({ ...editedBook, author: e.target.value })}
            />
          </Box>
          <Box mb={2}>
            <TextField
              label="Published Year"
              fullWidth
              defaultValue={editedBook?.publishedYear || ''}
              onChange={(e) => setEditedBook({ ...editedBook, publishedYear: e.target.value })}
            />
          </Box>
          <Box mb={2}>
            <TextField
              label="ISBN"
              fullWidth
              defaultValue={editedBook?.isbn || ''}
              onChange={(e) => setEditedBook({ ...editedBook, isbn: e.target.value })}
            />
          </Box>
        </DialogContent>
        <DialogActions>
          <Button onClick={handleEditDialogClose} color="error">
            Cancel
          </Button>
          <Button onClick={handleEditSave} color="primary">
            Save
          </Button>
        </DialogActions>
      </Dialog>
      {/* Add Dialog */}
      <Dialog open={openAddDialog} onClose={handleAddDialogClose}>
        <DialogTitle>Add Book</DialogTitle>
        <DialogContent>
        <DialogContentText>
            Please fill all the details for the new book.
          </DialogContentText>
        <Box mb={2} mt={2}>
            <TextField
              label="Title"
              fullWidth
              value={newBook.title}
              onChange={(e) => handleAddChange('title', e.target.value)}
            />
          </Box>
          <Box mb={2}>
            <TextField
              label="Author"
              fullWidth
              value={newBook.author}
              onChange={(e) => handleAddChange('author', e.target.value)}
            />
          </Box>
          <Box mb={2}>
            <TextField
              label="Published Year"
              fullWidth
              value={newBook.publishedYear}
              onChange={(e) => handleAddChange('publishedYear', e.target.value)}
            />
          </Box>
          <Box mb={2}>
            <TextField
              label="ISBN"
              fullWidth
              value={newBook.isbn}
              onChange={(e) => handleAddChange('isbn', e.target.value)}
            />
          </Box>
        </DialogContent>
        <DialogActions>
          <Button onClick={handleAddDialogClose} color="primary">
            Cancel
          </Button>
          <Button onClick={handleAddSave} color="primary" disabled={isSaveDisabled}>
            Save
          </Button>
        </DialogActions>
      </Dialog>
      </div>
        </Container>
      </main>
    </ThemeProvider>
  );
}