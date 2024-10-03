import { Box, TextField, Typography } from "@mui/material";

export default function AboutPage() {
  return (
    <>
      <Typography>
        This is a simple application for testing. Please check it out and if you
        have any questions or ideas, please send them here:
      </Typography>
      <Box>
        <TextField id="yourEmail"></TextField>
        <TextField id="message"></TextField>
      </Box>
    </>
  );
}
