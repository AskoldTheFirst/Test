import {
  Box,
  Button,
  Grid,
  TextField,
  Typography,
} from "@mui/material";
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
          
            <Typography variant="h6" textAlign='center' sx={{marginRight: 1}}>You can leave here some information about yourself and your contacts which will be available to other users.</Typography>
            
            {/* <Stack direction='row'>
              <Typography variant="h6" sx={{marginLeft: 1}}>User:</Typography>
              <Typography variant="h6" sx={{marginLeft: 3}}>{user?.login}</Typography>
            </Stack> */}
            
            <Typography sx={{ marginTop: 6 }}>About:</Typography>
            <TextField
              id="about"
              multiline
              rows={6}
              fullWidth
              value={about}
              onChange={(e) => setAbout(e.target.value)}
            />
            <Typography sx={{ marginTop: 4 }}>Contacts:</Typography>
            <TextField
              id="contact"
              multiline
              rows={3}
              fullWidth
              value={contacts}
              onChange={(e) => setContacts(e.target.value)}
            />
            <br />
            <Button sx={{ marginTop: 4, border: 1 }} onClick={saveProfile}>
              Save
            </Button>
          </Box>
        </Box>
      </Grid>
    </Grid>
  );
}
