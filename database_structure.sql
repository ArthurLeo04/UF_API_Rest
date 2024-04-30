--
-- PostgreSQL database dump
--

-- Dumped from database version 16.2
-- Dumped by pg_dump version 16.2

-- Started on 2024-04-30 13:41:48

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 6 (class 2615 OID 16579)
-- Name: public; Type: SCHEMA; Schema: -; Owner: postgres
--

-- *not* creating schema, since initdb creates it


ALTER SCHEMA public OWNER TO postgres;

--
-- TOC entry 4914 (class 0 OID 0)
-- Dependencies: 6
-- Name: SCHEMA public; Type: COMMENT; Schema: -; Owner: postgres
--

COMMENT ON SCHEMA public IS '';


--
-- TOC entry 2 (class 3079 OID 16580)
-- Name: uuid-ossp; Type: EXTENSION; Schema: -; Owner: -
--

CREATE EXTENSION IF NOT EXISTS "uuid-ossp" WITH SCHEMA public;


--
-- TOC entry 4916 (class 0 OID 0)
-- Dependencies: 2
-- Name: EXTENSION "uuid-ossp"; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION "uuid-ossp" IS 'generate universally unique identifiers (UUIDs)';


SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 216 (class 1259 OID 16591)
-- Name: achievements; Type: TABLE; Schema: public; Owner: me
--

CREATE TABLE public.achievements (
    id uuid DEFAULT public.uuid_generate_v4() NOT NULL,
    nom text NOT NULL,
    description text NOT NULL,
    image text NOT NULL
);


ALTER TABLE public.achievements OWNER TO me;

--
-- TOC entry 217 (class 1259 OID 16597)
-- Name: friend_requests; Type: TABLE; Schema: public; Owner: me
--

CREATE TABLE public.friend_requests (
    sender uuid NOT NULL,
    receiver uuid NOT NULL
);


ALTER TABLE public.friend_requests OWNER TO me;

--
-- TOC entry 218 (class 1259 OID 16600)
-- Name: friends; Type: TABLE; Schema: public; Owner: me
--

CREATE TABLE public.friends (
    user1 uuid NOT NULL,
    user2 uuid NOT NULL
);


ALTER TABLE public.friends OWNER TO me;

--
-- TOC entry 219 (class 1259 OID 16603)
-- Name: ranks; Type: TABLE; Schema: public; Owner: me
--

CREATE TABLE public.ranks (
    rank text NOT NULL
);


ALTER TABLE public.ranks OWNER TO me;

--
-- TOC entry 220 (class 1259 OID 16608)
-- Name: roles; Type: TABLE; Schema: public; Owner: me
--

CREATE TABLE public.roles (
    role text NOT NULL
);


ALTER TABLE public.roles OWNER TO me;

--
-- TOC entry 221 (class 1259 OID 16613)
-- Name: user_achievements; Type: TABLE; Schema: public; Owner: me
--

CREATE TABLE public.user_achievements (
    id_user uuid NOT NULL,
    id_achievement uuid NOT NULL
);


ALTER TABLE public.user_achievements OWNER TO me;

--
-- TOC entry 222 (class 1259 OID 16616)
-- Name: users; Type: TABLE; Schema: public; Owner: me
--

CREATE TABLE public.users (
    id uuid DEFAULT public.uuid_generate_v4() NOT NULL,
    email character varying(100) NOT NULL,
    username character varying(50) NOT NULL,
    password character varying(255) NOT NULL,
    salt character varying(50) NOT NULL,
    kill_count integer DEFAULT 0 NOT NULL,
    death_count integer DEFAULT 0 NOT NULL,
    rank text DEFAULT 'Bronze'::text NOT NULL,
    role text DEFAULT 'client'::text NOT NULL,
    kd_ratio numeric GENERATED ALWAYS AS (
CASE
    WHEN ((kill_count = 0) AND (death_count = 0)) THEN (0)::numeric
    WHEN (death_count = 0) THEN (1)::numeric
    ELSE ((kill_count)::numeric / (NULLIF(death_count, 0))::numeric)
END) STORED
);


ALTER TABLE public.users OWNER TO me;

--
-- TOC entry 4902 (class 0 OID 16591)
-- Dependencies: 216
-- Data for Name: achievements; Type: TABLE DATA; Schema: public; Owner: me
--

INSERT INTO public.achievements (id, nom, description, image) VALUES ('d9a7275a-e51e-4870-ab01-6272d1fd3989', 'Beginner Luck', 'Shoot your first opponent', 'https://url/to/achievement/image');
INSERT INTO public.achievements (id, nom, description, image) VALUES ('39cb884f-016a-4722-bc32-6cf3c319c3ea', 'The first of a long series', 'Die for the first time', 'https://url/to/achievement/image');
INSERT INTO public.achievements (id, nom, description, image) VALUES ('fe8afe63-5fc2-4d33-90e6-4444d04e1595', 'Rampage', 'Shoot a total of 10 opponents', 'https://url/to/achievement/image');
INSERT INTO public.achievements (id, nom, description, image) VALUES ('ebe36b9f-11c8-41e5-8189-b3e14450d510', 'Immortal', 'Die a total of 10 times', 'https://url/to/achievement/image');


--
-- TOC entry 4903 (class 0 OID 16597)
-- Dependencies: 217
-- Data for Name: friend_requests; Type: TABLE DATA; Schema: public; Owner: me
--

INSERT INTO public.friend_requests (sender, receiver) VALUES ('3257e378-751d-4946-ad4d-38e1df83947f', 'd4df66d9-1874-4219-87ec-35faf7a0828c');
INSERT INTO public.friend_requests (sender, receiver) VALUES ('3257e378-751d-4946-ad4d-38e1df83947f', 'b067a476-09e6-4869-98b7-f50f7d7d5604');
INSERT INTO public.friend_requests (sender, receiver) VALUES ('2aeea279-a2ac-4f3e-b1cf-7e6ae4cb45dd', '3257e378-751d-4946-ad4d-38e1df83947f');


--
-- TOC entry 4904 (class 0 OID 16600)
-- Dependencies: 218
-- Data for Name: friends; Type: TABLE DATA; Schema: public; Owner: me
--



--
-- TOC entry 4905 (class 0 OID 16603)
-- Dependencies: 219
-- Data for Name: ranks; Type: TABLE DATA; Schema: public; Owner: me
--

INSERT INTO public.ranks (rank) VALUES ('Bronze');
INSERT INTO public.ranks (rank) VALUES ('Silver');
INSERT INTO public.ranks (rank) VALUES ('Gold');
INSERT INTO public.ranks (rank) VALUES ('Platinum');
INSERT INTO public.ranks (rank) VALUES ('Diamond');


--
-- TOC entry 4906 (class 0 OID 16608)
-- Dependencies: 220
-- Data for Name: roles; Type: TABLE DATA; Schema: public; Owner: me
--

INSERT INTO public.roles (role) VALUES ('client');
INSERT INTO public.roles (role) VALUES ('server');


--
-- TOC entry 4907 (class 0 OID 16613)
-- Dependencies: 221
-- Data for Name: user_achievements; Type: TABLE DATA; Schema: public; Owner: me
--



--
-- TOC entry 4908 (class 0 OID 16616)
-- Dependencies: 222
-- Data for Name: users; Type: TABLE DATA; Schema: public; Owner: me
--

INSERT INTO public.users (id, email, username, password, salt, kill_count, death_count, rank, role) VALUES ('51e1b3e6-3e66-4787-bcd8-89253f3a0a1d', 'user5@email.com', 'user5', '9+g6zhWArx6WTR33mVWBuau7P/+g4bLYo3KY9Td5wVQ=', 'NEZGH2JSHbTFJvychuqX7w==', 0, 0, 'Bronze', 'client');
INSERT INTO public.users (id, email, username, password, salt, kill_count, death_count, rank, role) VALUES ('3257e378-751d-4946-ad4d-38e1df83947f', 'user1@email.com', 'user1', 'ppnCWHuzwRWqNgBPg+xSP63WOw/EBza9cS5JUhBzCyU=', 'Azd7GSuoUlsY21YL2tMZzw==', 0, 0, 'Bronze', 'client');
INSERT INTO public.users (id, email, username, password, salt, kill_count, death_count, rank, role) VALUES ('d4df66d9-1874-4219-87ec-35faf7a0828c', 'user2@email.com', 'user2', '5Qr0cRu08WN0XeR2YsDDnGYfS0EU6wlD/gqHnCMCAKk=', 'TNUqukhx5geYwQMBj7xd8A==', 0, 0, 'Bronze', 'client');
INSERT INTO public.users (id, email, username, password, salt, kill_count, death_count, rank, role) VALUES ('b067a476-09e6-4869-98b7-f50f7d7d5604', 'user3@email.com', 'user3', 'vdkMYbz4PaNJKqwSGe1JhYaXrvX2bT6xzjCktq0p42w=', '0rp+rT6IaLSOygXSkXGJaQ==', 0, 0, 'Bronze', 'client');
INSERT INTO public.users (id, email, username, password, salt, kill_count, death_count, rank, role) VALUES ('2aeea279-a2ac-4f3e-b1cf-7e6ae4cb45dd', 'user4@email.com', 'user4', '25vhxMza3OgvAAUrtWjVZ8AV+Vh3Tqv5Z9VIW6erjPI=', 'F8I16s5CAjLMajC6mEaQOw==', 0, 0, 'Bronze', 'client');


--
-- TOC entry 4730 (class 2606 OID 16628)
-- Name: achievements achievements_pkey; Type: CONSTRAINT; Schema: public; Owner: me
--

ALTER TABLE ONLY public.achievements
    ADD CONSTRAINT achievements_pkey PRIMARY KEY (id);


--
-- TOC entry 4734 (class 2606 OID 16630)
-- Name: friend_requests friend_requests_pkey; Type: CONSTRAINT; Schema: public; Owner: me
--

ALTER TABLE ONLY public.friend_requests
    ADD CONSTRAINT friend_requests_pkey PRIMARY KEY (sender, receiver);


--
-- TOC entry 4736 (class 2606 OID 16632)
-- Name: friends friends_pkey; Type: CONSTRAINT; Schema: public; Owner: me
--

ALTER TABLE ONLY public.friends
    ADD CONSTRAINT friends_pkey PRIMARY KEY (user1, user2);


--
-- TOC entry 4732 (class 2606 OID 16634)
-- Name: achievements id; Type: CONSTRAINT; Schema: public; Owner: me
--

ALTER TABLE ONLY public.achievements
    ADD CONSTRAINT id UNIQUE (id);


--
-- TOC entry 4738 (class 2606 OID 16636)
-- Name: ranks ranks_pkey; Type: CONSTRAINT; Schema: public; Owner: me
--

ALTER TABLE ONLY public.ranks
    ADD CONSTRAINT ranks_pkey PRIMARY KEY (rank);


--
-- TOC entry 4740 (class 2606 OID 16638)
-- Name: roles roles_name_key; Type: CONSTRAINT; Schema: public; Owner: me
--

ALTER TABLE ONLY public.roles
    ADD CONSTRAINT roles_name_key UNIQUE (role);


--
-- TOC entry 4742 (class 2606 OID 16640)
-- Name: roles roles_pkey; Type: CONSTRAINT; Schema: public; Owner: me
--

ALTER TABLE ONLY public.roles
    ADD CONSTRAINT roles_pkey PRIMARY KEY (role);


--
-- TOC entry 4744 (class 2606 OID 16642)
-- Name: user_achievements user_achievements_pkey; Type: CONSTRAINT; Schema: public; Owner: me
--

ALTER TABLE ONLY public.user_achievements
    ADD CONSTRAINT user_achievements_pkey PRIMARY KEY (id_user, id_achievement);


--
-- TOC entry 4746 (class 2606 OID 16644)
-- Name: users users_email_key; Type: CONSTRAINT; Schema: public; Owner: me
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_email_key UNIQUE (email);


--
-- TOC entry 4748 (class 2606 OID 16646)
-- Name: users users_pkey; Type: CONSTRAINT; Schema: public; Owner: me
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_pkey PRIMARY KEY (id);


--
-- TOC entry 4750 (class 2606 OID 16648)
-- Name: users users_username_key; Type: CONSTRAINT; Schema: public; Owner: me
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_username_key UNIQUE (username);


--
-- TOC entry 4755 (class 2606 OID 16649)
-- Name: user_achievements id_achievement_fkey; Type: FK CONSTRAINT; Schema: public; Owner: me
--

ALTER TABLE ONLY public.user_achievements
    ADD CONSTRAINT id_achievement_fkey FOREIGN KEY (id_achievement) REFERENCES public.achievements(id) NOT VALID;


--
-- TOC entry 4756 (class 2606 OID 16654)
-- Name: user_achievements id_user_fkey; Type: FK CONSTRAINT; Schema: public; Owner: me
--

ALTER TABLE ONLY public.user_achievements
    ADD CONSTRAINT id_user_fkey FOREIGN KEY (id_user) REFERENCES public.users(id) NOT VALID;


--
-- TOC entry 4751 (class 2606 OID 16674)
-- Name: friend_requests receiver_fkey; Type: FK CONSTRAINT; Schema: public; Owner: me
--

ALTER TABLE ONLY public.friend_requests
    ADD CONSTRAINT receiver_fkey FOREIGN KEY (receiver) REFERENCES public.users(id);


--
-- TOC entry 4752 (class 2606 OID 16664)
-- Name: friend_requests sender_fkey; Type: FK CONSTRAINT; Schema: public; Owner: me
--

ALTER TABLE ONLY public.friend_requests
    ADD CONSTRAINT sender_fkey FOREIGN KEY (sender) REFERENCES public.users(id);


--
-- TOC entry 4753 (class 2606 OID 16659)
-- Name: friends user1_fkey; Type: FK CONSTRAINT; Schema: public; Owner: me
--

ALTER TABLE ONLY public.friends
    ADD CONSTRAINT user1_fkey FOREIGN KEY (user1) REFERENCES public.users(id);


--
-- TOC entry 4754 (class 2606 OID 16669)
-- Name: friends user2_fkey; Type: FK CONSTRAINT; Schema: public; Owner: me
--

ALTER TABLE ONLY public.friends
    ADD CONSTRAINT user2_fkey FOREIGN KEY (user2) REFERENCES public.users(id);


--
-- TOC entry 4757 (class 2606 OID 16679)
-- Name: users users_rank_fkey; Type: FK CONSTRAINT; Schema: public; Owner: me
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_rank_fkey FOREIGN KEY (rank) REFERENCES public.ranks(rank) NOT VALID;


--
-- TOC entry 4758 (class 2606 OID 16684)
-- Name: users users_role_fkey; Type: FK CONSTRAINT; Schema: public; Owner: me
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_role_fkey FOREIGN KEY (role) REFERENCES public.roles(role) NOT VALID;


--
-- TOC entry 4915 (class 0 OID 0)
-- Dependencies: 6
-- Name: SCHEMA public; Type: ACL; Schema: -; Owner: postgres
--

REVOKE USAGE ON SCHEMA public FROM PUBLIC;
GRANT ALL ON SCHEMA public TO me;


-- Completed on 2024-04-30 13:41:52

--
-- PostgreSQL database dump complete
--

