import { Box, Button, FormControl, FormLabel, Grid, TextField, Typography } from "@mui/material";
import { useEffect, useState } from "react";
import agent from "../../Biz/agent";

export default function ProfilePage() {
  const [about, setAbout] = useState<string>("");
  const [contacts, setContacts] = useState<string>("");


  useEffect(() => {
    agent.Profile.get().then((data) => {
      setAbout(data.about);
      setContacts(data.contacts);
    });
  }, []);

  async function saveProfile() {
    await agent.Profile.save({
      about: about,
      contacts: contacts,
    });
  }

  return (
    <Grid container sx={{ marginTop: 6 }}>
      <Grid item xl={3} lg={3} md={3} sm={1} xs={1} />
      <Grid item xl={6} lg={6} md={6} sm={10} xs={10}>
        <Box>
          <Box sx={{ maxWidth: "440px" }}>

            <Typography variant="h6" textAlign='center' sx={{ marginRight: 1, fontSize: '11pt' }}>
              You can leave here some information about yourself, and your contacts which will be available to other users.
            </Typography>

            <FormControl sx={{ textAlign: 'left', marginTop: 5 }}>
              <FormLabel htmlFor="about">About:</FormLabel>
              <TextField
                autoFocus
                id="about"
                sx={{ ariaLabel: 'email', width: '440px' }}
                multiline
                rows={6}
                fullWidth
                size='small'
                value={about}
                onChange={(e) => setAbout(e.target.value)}
              />
            </FormControl>

            <FormControl sx={{ textAlign: 'left', marginTop: 4 }}>
              <FormLabel htmlFor="contact">Contacts:</FormLabel>
              <TextField
                id="contact"
                sx={{ ariaLabel: 'email', width: '440px' }}
                multiline
                rows={3}
                fullWidth
                size='small'
                value={contacts}
                onChange={(e) => setContacts(e.target.value)}
              />
            </FormControl>

            <div style={{ textAlign: 'end' }}>
              <Button sx={{ marginTop: 4, border: 1 }} onClick={saveProfile}>
                Save
              </Button>
            </div>
          </Box>
        </Box>
      </Grid>
    </Grid>
  );
}
