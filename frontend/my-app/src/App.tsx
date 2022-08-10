import axios from "axios";
import { useState } from "react";
import SearchIcon from "@mui/icons-material/Search";
import TextField from "@mui/material/TextField";
import "./App.css";
import { Box, Button, Grid, Paper, Skeleton } from "@mui/material";

function App() {
  const [filmName, setFilmName] = useState("");
  const [filmInfo, setFilmInfo] = useState<undefined | any>(undefined);

  const FILM_BASE_API_URL = "https://ghibliapi.herokuapp.com/films";
  return (
    <div>
      <div className="search-field">
        <div style={{ display: "flex", justifyContent: "center" }}>
          <h1>Studio Ghibli Movie Search</h1>
        </div>
        <div style={{ display: "flex", justifyContent: "center" }}>
          <TextField
            id="search-bar"
            className="text"
            value={filmName}
            onChange={(prop) => {
              setFilmName(prop.target.value);
            }}
            label="Enter the film name..."
            variant="outlined"
            placeholder="Search..."
            size="medium"
          />
          <Button
            onClick={() => {
              search();
            }}
          >
            <SearchIcon style={{ fill: "blue" }} />
            Search
          </Button>
        </div>
      </div>

      {filmInfo === undefined ? (
        <div></div>
      ) : (
        <div
          id="film-result"
          style={{
            maxWidth: "800px",
            margin: "0 auto",
            padding: "50px 10px 0px 10px",
          }}
        >
          <Paper sx={{ backgroundColor: "#BDF8FF" }}>
            <Grid
              container
              direction="row"
              spacing={5}
              sx={{
                justifyContent: "center",
              }}
            >
              <Grid item>
                <Box>
                  {filmInfo === undefined || filmInfo.length === 0 ? (
                    <h1> Film not found</h1>
                  ) : (
                    <div>
                      <h1>
                        {filmInfo[0].title}
                      </h1>
                      <p>
                        Description: {filmInfo[0].description}
                        <br />
                        Director: {filmInfo[0].director}
                        <br />
                        Release Year: {filmInfo[0].release_date}
                        <br />
                        Running Time: {filmInfo[0].running_time} mins
                        <br />
                        Rotten Tomato Score: {filmInfo[0].rt_score}
                      </p>
                    </div>
                  )}
                </Box>
              </Grid>
              <Grid item>
                <Box>
                  {filmInfo[0]?.image ? (
                    <img
                      height="300px"
                      width="300px"
                      alt={filmInfo[0].title}
                      src={filmInfo[0].image}
                    ></img>
                  ) : (
                    <Skeleton width={300} height={300} />
                  )}
                </Box>
              </Grid>
            </Grid>
          </Paper>
        </div>
      )}
    </div>
  );

  function search() {
    console.log(filmName);
    if (filmName === undefined || filmName === "") return;

    const name = filmName.split(" ");

    for (let i = 0; i < name.length; i++) {
      name[i] = name[i][0].toUpperCase() + name[i].substr(1);
    }

    axios
      .get(FILM_BASE_API_URL + "?title=" + name.join(" "))
      .then((res) => {
        setFilmInfo(res.data);
        console.log(res.data);
      })
      .catch(() => {
        setFilmInfo(null);
      });
  }
}

export default App;