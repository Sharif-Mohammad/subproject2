CREATE OR REPLACE FUNCTION framework.structured_string_search(
	p_title text,
	p_plot text,
	p_characters text,
	p_person_names text,
	p_user_id character varying,
	p_offset integer DEFAULT 0,
	p_limit integer DEFAULT 100)
    RETURNS TABLE(movieid character varying, primarytitle character varying) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
BEGIN
    -- Log search history for the user
    INSERT INTO framework.search_history (user_id, search_query, search_type, created_at)
    VALUES (p_user_id, CONCAT('Title: ', p_title, ', Plot: ', p_plot, ', Characters: ', p_characters, ', People: ', p_person_names), 'structured_movie', CURRENT_TIMESTAMP);
    
    -- Perform the structured search with pagination
    RETURN QUERY
    SELECT m.movie_id as movieid, m.title as primarytitle
    FROM movie_data.movies m
    LEFT JOIN movie_data.movie_cast c ON m.movie_id = c.movie_id
    LEFT JOIN movie_data.people p ON c.person_id = p.person_id
    WHERE (p_title IS NULL OR m.title ILIKE '%' || p_title || '%')
      AND (p_plot IS NULL OR m.plot ILIKE '%' || p_plot || '%')
      AND (p_characters IS NULL OR c.character ILIKE '%' || p_characters || '%')
      AND (p_person_names IS NULL OR p.name ILIKE '%' || p_person_names || '%')
    ORDER BY m.movie_id -- Optional: Order by movie_id or another relevant column
    LIMIT p_limit       -- Limit the number of rows returned
    OFFSET p_offset;    -- Skip the specified number of rows
END;
$BODY$;





-- DROP FUNCTION IF EXISTS framework.string_search(character varying, text, integer, integer);

CREATE OR REPLACE FUNCTION framework.string_search(
	p_user_id character varying,
	p_search_string text,
	p_offset integer DEFAULT 0,
	p_limit integer DEFAULT 100)
    RETURNS TABLE(movieid character varying, primarytitle character varying) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
BEGIN
    -- First, log the search history for the user
    INSERT INTO framework.search_history (user_id, search_query, search_type, created_at)
    VALUES (p_user_id, p_search_string, 'movie', CURRENT_TIMESTAMP);
    
    -- Return the movies where the search string is found in the title or description
    RETURN QUERY
    SELECT m.movie_id AS movieid, m.title AS primarytitle
    FROM movie_data.movies m
    WHERE m.title ILIKE '%' || p_search_string || '%'  -- Case-insensitive match on title
       OR m.plot ILIKE '%' || p_search_string || '%'
    ORDER BY m.movie_id -- Optional: Specify how to order results
    LIMIT p_limit        -- Limit the number of results
    OFFSET p_offset;     -- Skip the number of results specified by offset
END;
$BODY$;



CREATE OR REPLACE FUNCTION framework.find_movie_title(
    p_title TEXT,
    p_offset INTEGER DEFAULT 0,
    p_limit INTEGER DEFAULT 100
) RETURNS TABLE(movieid VARCHAR, primarytitle VARCHAR) AS $$
BEGIN
    RETURN QUERY
    SELECT m.movie_id as movieid, m.title as primarytitle
    FROM movie_data.movies m
    WHERE m.title ILIKE '%' || p_title || '%'
    ORDER BY m.movie_id
    LIMIT p_limit OFFSET p_offset;
END;
$$ LANGUAGE plpgsql;



CREATE OR REPLACE FUNCTION framework.find_co_players(
    p_actor_name TEXT,
    p_offset INTEGER DEFAULT 0,
    p_limit INTEGER DEFAULT 100
) RETURNS TABLE(personid VARCHAR, personname VARCHAR, frequency INT) AS $$
BEGIN
    RETURN QUERY
    WITH actor_movies AS (
        SELECT c1.movie_id
        FROM movie_data.people p1
        JOIN movie_data.movie_cast c1 ON p1.person_id = c1.person_id
        WHERE p1.name ILIKE '%' || p_actor_name || '%'
    )
    SELECT p2.person_id as personid, p2.name as personname, COUNT(*)::int AS frequency
    FROM actor_movies am
    JOIN movie_data.movie_cast c2 ON am.movie_id = c2.movie_id
    JOIN movie_data.people p2 ON p2.person_id = c2.person_id
    WHERE p2.name NOT ILIKE p_actor_name
    GROUP BY p2.person_id, p2.name
    ORDER BY frequency DESC
    LIMIT p_limit OFFSET p_offset;
END;
$$ LANGUAGE plpgsql;



CREATE OR REPLACE FUNCTION framework.get_actors_by_popularity(
    p_movie_id VARCHAR,
    p_offset INTEGER DEFAULT 0,
    p_limit INTEGER DEFAULT 100
) RETURNS TABLE (
    id VARCHAR,
    name VARCHAR,
    weightedrating NUMERIC,
    totalvotes BIGINT
) AS $$
BEGIN
    RETURN QUERY
    SELECT
        nr.person_id as id,
        nr.name  ,
        nr.weighted_rating as weightedrating,
        nr.total_votes as totalvotes
    FROM
        movie_data.people tp
    JOIN
        framework.name_ratings nr
    ON
        tp.person_id = nr.person_id
    JOIN 
        movie_data.movie_cast mc
    ON
        mc.person_id = nr.person_id
    WHERE
        mc.movie_id = p_movie_id
    ORDER BY
        nr.weighted_rating DESC
    LIMIT p_limit OFFSET p_offset;  -- Limit and offset for pagination
END;
$$ LANGUAGE plpgsql;




CREATE OR REPLACE FUNCTION framework.get_similar_movies(
    p_movie_id VARCHAR,
    p_offset INTEGER DEFAULT 0,
    p_limit INTEGER DEFAULT 20
) RETURNS TABLE (
    similarmovieid VARCHAR,
    similarmovietitle VARCHAR,
    sharedgenrecount BIGINT,
    sharedcastcount BIGINT
) AS $$
BEGIN
    RETURN QUERY
    WITH movie_genres AS (
        SELECT genre_id
        FROM movie_data.movie_genres
        WHERE movie_id = p_movie_id
    ),
    movie_cast AS (
        SELECT person_id
        FROM movie_data.movie_cast
        WHERE movie_id = p_movie_id
    ),
    similar_movies AS (
        SELECT 
            m.movie_id AS similarmovieid,
            m.title AS similarmovietitle,
            COUNT(DISTINCT mg.genre_id) AS sharedgenrecount,
            COUNT(DISTINCT mc.person_id) AS sharedcastcount
        FROM 
            movie_data.movies m
        JOIN 
            movie_genres mg ON mg.genre_id IN (SELECT genre_id FROM movie_data.movie_genres WHERE movie_id = m.movie_id)
        JOIN 
            movie_cast mc ON mc.person_id IN (SELECT person_id FROM movie_data.movie_cast WHERE movie_id = m.movie_id)
        WHERE 
            m.movie_id <> p_movie_id  -- Exclude the original movie from results
        GROUP BY 
            m.movie_id
        HAVING 
            COUNT(DISTINCT mg.genre_id) > 0 OR COUNT(DISTINCT mc.person_id) > 0  -- Only include movies with shared genres or cast
    )
    SELECT 
        sm.similarmovieid,
        sm.similarmovietitle,
        sm.sharedgenrecount,
        sm.sharedcastcount
    FROM 
        similar_movies sm
    ORDER BY 
        (sm.sharedgenrecount + sm.sharedcastcount) DESC  -- Order by total similarities
    LIMIT p_limit OFFSET p_offset;  -- Apply limit and offset for pagination
END;
$$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION framework.exact_match_query(
    p_keywords TEXT[],
    p_offset INTEGER DEFAULT 0,
    p_limit INTEGER DEFAULT 10
) RETURNS TABLE(movieid VARCHAR, primarytitle VARCHAR) AS $$
BEGIN
    RETURN QUERY
    SELECT m.movie_id AS movieid, m.title AS primarytitle
    FROM movie_data.movies m
    JOIN 
        wi.wi w	
    ON 
        w.tconst = m.movie_id
    WHERE w.word = ANY(p_keywords)
    GROUP BY m.movie_id
    HAVING COUNT(DISTINCT w.word) = array_length(p_keywords, 1)
    LIMIT p_limit OFFSET p_offset;  -- Add pagination limit and offset
END;
$$ LANGUAGE plpgsql;



CREATE OR REPLACE FUNCTION framework.rate(
    p_user_id VARCHAR,       -- ID of the user rating the movie
    p_movie_id VARCHAR,  -- ID of the movie (tconst)
    p_rating INT ,        -- Rating value (1 to 10)
	p_review TEXT
) RETURNS VOID AS $$
DECLARE
    v_existing_rating INT;
    v_old_average DECIMAL(3, 1);
    v_rating_count INT;
BEGIN
    -- Ensure the rating is between 1 and 10
    IF p_rating < 1 OR p_rating > 10 THEN
        RAISE EXCEPTION 'Rating must be between 1 and 10';
    END IF;
    
    -- Check if the user has already rated this movie
    SELECT rating INTO v_existing_rating
    FROM framework.user_ratings
    WHERE user_id = p_user_id AND movie_id = p_movie_id;
    
    -- Fetch the current average rating and rating count of the movie
    SELECT averagerating, numvotes INTO v_old_average, v_rating_count
    FROM movie_data.ratings r
    WHERE r.movie_id = p_movie_id;
    
    IF v_existing_rating IS NULL THEN
        -- New rating by the user for this movie
        
        -- Insert new rating record
        INSERT INTO framework.user_ratings (user_id, movie_id, rating,review, created_at,updated_at)
        VALUES (p_user_id, p_movie_id, p_rating,p_review, CURRENT_TIMESTAMP,CURRENT_TIMESTAMP);
        
        -- Update movie average rating and count
        UPDATE movie_data.ratings
        SET averagerating = ((v_old_average * v_rating_count) + p_rating) / (v_rating_count + 1),
            numvotes = v_rating_count + 1
        WHERE movie_id = p_movie_id;

    ELSE
        -- User has already rated this movie, so update their rating
        
        -- Update the existing rating record
        UPDATE framework.user_ratings
        SET rating = p_rating,review = p_review, updated_at = CURRENT_TIMESTAMP
        WHERE user_id = p_user_id AND movie_id = p_movie_id;

        -- Adjust the average rating (recalculate by removing the old rating and adding the new one)
        UPDATE movie_data.ratings
        SET averagerating = ((v_old_average * v_rating_count) - v_existing_rating + p_rating) / v_rating_count
        WHERE movie_id = p_movie_id;
    END IF;
    
    
END;
$$ LANGUAGE plpgsql;

