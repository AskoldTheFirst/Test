import { useEffect, useState } from "react";
import agent from "../../Biz/agent";
import { TestResultDto } from "../../Biz/DTOs/TestResultDto";
import { Box, Button, Container, Typography } from "@mui/material";
import { useSelector } from "react-redux";
import { RootState } from "../../App/configureStore";
import { useNavigate } from "react-router-dom";

export default function TestResultPage() {
  const [result, setResult] = useState<TestResultDto>();
  const { test } = useSelector((state: RootState) => state.test);
  const navigate = useNavigate();

  useEffect(() => {
    if (test !== null)
      agent.Test.result(test.testId).then((result) => setResult(result));
    else
      navigate('/home');
  }, []);

  if (test === null) return <></>;

  return (
    <Container>
      <Box sx={{ marginTop: 10, textAlign: 'center' }}>
        <Typography variant="h6">Congratulate!</Typography>
        <Typography variant="h6" sx={{fontSize: '16px', marginTop: 1}}>
          You have passed through the {test.technologyName} quiz
        </Typography>
        <Typography variant="h6" sx={{fontSize: '16px'}}>with the score {result?.score}.</Typography>
      </Box>

      <Box sx={{ marginTop: 10, textAlign: 'center' }}>
        <Typography variant="h6" sx={{fontSize: '14px'}}>Now you can go back and start another quiz again:</Typography>
        <Button sx={{border: 0}} size="small" onClick={() => navigate('/home')}>Home</Button>
        <Typography variant="h6" sx={{fontSize: '14px', marginTop: 2}}>
          Or watch the statistics of all test results:
        </Typography>
        <Button sx={{border: 0}} size="small" onClick={() => navigate('/statistics')}>Statistics</Button>
      </Box>
    </Container>
  );
}
