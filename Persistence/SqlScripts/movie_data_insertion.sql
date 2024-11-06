

INSERT INTO movie_data.movies 
    (movie_id, title,category, release_year, runtime_minutes, plot, is_adult, poster_url, box_office, production, created_at, updated_at)
SELECT 
    imdb.tconst, 
    imdb.originaltitle, 
	imdb.titletype,
     NULLIF(CAST(NULLIF(TRIM(imdb.startyear), '') AS INTEGER), 0),  -- Handle empty strings and trim whitespace
    imdb.runtimeminutes, 
    omdb.plot, 
    imdb.isadult, 
    omdb.poster, 
    omdb.boxoffice, 
    omdb.production, 
    CURRENT_TIMESTAMP, 
    CURRENT_TIMESTAMP
FROM 
    imdb_db.title_basics AS imdb
LEFT JOIN 
    omdb_db.omdb_data AS omdb 
ON 
    imdb.tconst = omdb.tconst;


-- Make sure imdb and omdb added first

INSERT INTO movie_data.genres (name)
SELECT DISTINCT genre
FROM (
    SELECT UNNEST(STRING_TO_ARRAY(TRIM(imdb.genres), ',')) AS genre
    FROM imdb_db.title_basics as imdb
) AS subquery
ON CONFLICT (name) DO NOTHING;



INSERT INTO movie_data.people (person_id, name, birth_year, created_at, updated_at)
SELECT nconst, primaryname,
     NULLIF(CAST(NULLIF(TRIM(birthyear), '') AS INTEGER), 0),  CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
FROM imdb_db.name_basics;



-- First, extract and match known movie titles and nconsts
WITH person_movies AS (
    SELECT
        nb.nconst AS person_id,
        nb.primaryname,
        UNNEST(string_to_array(nb.knownfortitles, ',')) AS movie_id
    FROM
        imdb_db.name_basics nb
    WHERE
        nb.knownfortitles IS NOT NULL
        AND nb.knownfortitles != ''
),
-- Join with title_principals to get the character information
person_characters AS (
    SELECT
        pm.movie_id,
        pm.person_id,
        tp.characters,
        tp.ordering
    FROM
        person_movies pm
    JOIN
        imdb_db.title_principals tp
    ON
        pm.movie_id = tp.tconst
        AND pm.person_id = tp.nconst
)

-- Now insert the data into movie_data.movie_cast
INSERT INTO movie_data.movie_cast (movie_id, person_id, "character", ordering)
SELECT
    pc.movie_id,       -- movie_id from person_characters
    pc.person_id,      -- person_id (nconst)
    COALESCE(pc.characters, 'Unknown'), -- Use the characters from title_principals, default to 'Unknown' if null
    pc.ordering        -- ordering from title_principals
FROM
    person_characters pc
-- Skip conflicts (don't insert if movie_id and person_id already exist)
ON CONFLICT (movie_id, person_id) DO NOTHING;



INSERT INTO movie_data.ratings (movie_id, averagerating, numvotes)
SELECT tconst, averagerating, numvotes
	FROM imdb_db.title_ratings;



-- First, extract and match known movie titles and genres
WITH genre_movies AS (
    SELECT
        tb.tconst AS movie_id,   -- Use tconst as movie_id (assuming tconst is the movie identifier)
        UNNEST(string_to_array(tb.genres, ',')) AS genre
    FROM
        imdb_db.title_basics tb
    WHERE
        tb.genres IS NOT NULL
        AND tb.genres != ''
)

-- Now insert the data into movie_data.movie_genres
INSERT INTO movie_data.movie_genres (movie_id, genre_id)
SELECT
    gm.movie_id,      -- movie_id from genre_movies
    g.genre_id        -- genre_id from movie_data.genres
FROM
    movie_data.genres g
    JOIN genre_movies gm ON g.name = gm.genre  -- Join to match genre names to genre_ids
-- Skip conflicts (don't insert if movie_id and genre_id already exist)
ON CONFLICT (movie_id, genre_id) DO NOTHING;


-- Build professions table

INSERT INTO movie_data.professions (profession_name)
SELECT DISTINCT unnest(string_to_array(nb.primaryprofession, ','))
FROM imdb_db.name_basics nb
-- Skip conflicts (don't insert if movie_id and genre_id already exist)
ON CONFLICT (profession_name) DO NOTHING;
;


-- Build professions relation with people table
INSERT INTO movie_data.person_professions (person_id, profession_id)
SELECT p.person_id, pr.profession_id
FROM movie_data.people p
JOIN imdb_db.name_basics nb on nb.nconst = p.person_id
CROSS JOIN LATERAL unnest(string_to_array(nb.primaryprofession, ',')) AS profession
JOIN movie_data.professions pr ON pr.profession_name = profession
-- Skip conflicts (don't insert if person_id and profession_id already exist)
ON CONFLICT (person_id, profession_id) DO NOTHING;


