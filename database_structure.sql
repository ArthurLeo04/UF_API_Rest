--
-- PostgreSQL database dump
--

-- Dumped from database version 16.2
-- Dumped by pg_dump version 16.2

-- Started on 2024-04-29 11:23:58

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
-- TOC entry 2 (class 3079 OID 16414)
-- Name: uuid-ossp; Type: EXTENSION; Schema: -; Owner: -
--

CREATE EXTENSION IF NOT EXISTS "uuid-ossp" WITH SCHEMA public;


--
-- TOC entry 4915 (class 0 OID 0)
-- Dependencies: 2
-- Name: EXTENSION "uuid-ossp"; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION "uuid-ossp" IS 'generate universally unique identifiers (UUIDs)';


SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 219 (class 1259 OID 16477)
-- Name: achievements; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.achievements (
    id uuid DEFAULT public.uuid_generate_v4() NOT NULL,
    nom text NOT NULL,
    description text NOT NULL,
    image text NOT NULL
);


ALTER TABLE public.achievements OWNER TO postgres;

--
-- TOC entry 222 (class 1259 OID 16564)
-- Name: friend_requests; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.friend_requests (
    user1 uuid NOT NULL,
    user2 uuid NOT NULL
);


ALTER TABLE public.friend_requests OWNER TO postgres;

--
-- TOC entry 221 (class 1259 OID 16549)
-- Name: friends; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.friends (
    user1 uuid NOT NULL,
    user2 uuid NOT NULL
);


ALTER TABLE public.friends OWNER TO postgres;

--
-- TOC entry 218 (class 1259 OID 16449)
-- Name: ranks; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.ranks (
    rank text NOT NULL
);


ALTER TABLE public.ranks OWNER TO postgres;

--
-- TOC entry 216 (class 1259 OID 16425)
-- Name: roles; Type: TABLE; Schema: public; Owner: me
--

CREATE TABLE public.roles (
    role text NOT NULL
);


ALTER TABLE public.roles OWNER TO me;

--
-- TOC entry 220 (class 1259 OID 16487)
-- Name: user_achievements; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.user_achievements (
    id_user uuid NOT NULL,
    id_achievement uuid NOT NULL
);


ALTER TABLE public.user_achievements OWNER TO postgres;

--
-- TOC entry 217 (class 1259 OID 16429)
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
-- TOC entry 4905 (class 0 OID 16477)
-- Dependencies: 219
-- Data for Name: achievements; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.achievements (id, nom, description, image) VALUES ('d9a7275a-e51e-4870-ab01-6272d1fd3989', 'Beginner Luck', 'Shoot your first opponent', 'https://url/to/achievement/image');
INSERT INTO public.achievements (id, nom, description, image) VALUES ('39cb884f-016a-4722-bc32-6cf3c319c3ea', 'The first of a long series', 'Die for the first time', 'https://url/to/achievement/image');
INSERT INTO public.achievements (id, nom, description, image) VALUES ('fe8afe63-5fc2-4d33-90e6-4444d04e1595', 'Rampage', 'Shoot a total of 10 opponents', 'https://url/to/achievement/image');
INSERT INTO public.achievements (id, nom, description, image) VALUES ('ebe36b9f-11c8-41e5-8189-b3e14450d510', 'Immortal', 'Die a total of 10 times', 'https://url/to/achievement/image');


--
-- TOC entry 4908 (class 0 OID 16564)
-- Dependencies: 222
-- Data for Name: friend_requests; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 4907 (class 0 OID 16549)
-- Dependencies: 221
-- Data for Name: friends; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 4904 (class 0 OID 16449)
-- Dependencies: 218
-- Data for Name: ranks; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.ranks (rank) VALUES ('Bronze');
INSERT INTO public.ranks (rank) VALUES ('Silver');
INSERT INTO public.ranks (rank) VALUES ('Gold');
INSERT INTO public.ranks (rank) VALUES ('Platinum');
INSERT INTO public.ranks (rank) VALUES ('Diamond');


--
-- TOC entry 4902 (class 0 OID 16425)
-- Dependencies: 216
-- Data for Name: roles; Type: TABLE DATA; Schema: public; Owner: me
--

INSERT INTO public.roles (role) VALUES ('client');
INSERT INTO public.roles (role) VALUES ('server');


--
-- TOC entry 4906 (class 0 OID 16487)
-- Dependencies: 220
-- Data for Name: user_achievements; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 4903 (class 0 OID 16429)
-- Dependencies: 217
-- Data for Name: users; Type: TABLE DATA; Schema: public; Owner: me
--

INSERT INTO public.users (id, email, username, password, salt, kill_count, death_count, rank, role) VALUES ('6c4eff44-c07b-438e-8971-2e1405dc26ad', 'user1@email.com', 'user1', 'password1', 'salt1', 0, 0, 'Bronze', 'client');
INSERT INTO public.users (id, email, username, password, salt, kill_count, death_count, rank, role) VALUES ('dc7572a6-ff84-4380-a313-f869e21e1855', 'user2@email.com', 'user2', 'password2', 'salt2', 0, 0, 'Bronze', 'client');
INSERT INTO public.users (id, email, username, password, salt, kill_count, death_count, rank, role) VALUES ('95b3f4f9-7944-4a83-a70d-8e59618129d0', 'user3@email.com', 'user3', 'password3', 'salt3', 0, 0, 'Bronze', 'client');
INSERT INTO public.users (id, email, username, password, salt, kill_count, death_count, rank, role) VALUES ('7fecce26-2582-4fee-8613-f3707150824b', 'user4@email.com', 'user4', 'password4', 'salt4', 0, 0, 'Bronze', 'client');


--
-- TOC entry 4742 (class 2606 OID 16483)
-- Name: achievements achievements_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.achievements
    ADD CONSTRAINT achievements_pkey PRIMARY KEY (id);


--
-- TOC entry 4750 (class 2606 OID 16568)
-- Name: friend_requests friend_requests_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.friend_requests
    ADD CONSTRAINT friend_requests_pkey PRIMARY KEY (user1, user2);


--
-- TOC entry 4748 (class 2606 OID 16553)
-- Name: friends friends_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.friends
    ADD CONSTRAINT friends_pkey PRIMARY KEY (user1, user2);


--
-- TOC entry 4744 (class 2606 OID 16485)
-- Name: achievements id; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.achievements
    ADD CONSTRAINT id UNIQUE (id);


--
-- TOC entry 4740 (class 2606 OID 16455)
-- Name: ranks ranks_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.ranks
    ADD CONSTRAINT ranks_pkey PRIMARY KEY (rank);


--
-- TOC entry 4730 (class 2606 OID 16467)
-- Name: roles roles_name_key; Type: CONSTRAINT; Schema: public; Owner: me
--

ALTER TABLE ONLY public.roles
    ADD CONSTRAINT roles_name_key UNIQUE (role);


--
-- TOC entry 4732 (class 2606 OID 16471)
-- Name: roles roles_pkey; Type: CONSTRAINT; Schema: public; Owner: me
--

ALTER TABLE ONLY public.roles
    ADD CONSTRAINT roles_pkey PRIMARY KEY (role);


--
-- TOC entry 4746 (class 2606 OID 16491)
-- Name: user_achievements user_achievements_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.user_achievements
    ADD CONSTRAINT user_achievements_pkey PRIMARY KEY (id_user, id_achievement);


--
-- TOC entry 4734 (class 2606 OID 16439)
-- Name: users users_email_key; Type: CONSTRAINT; Schema: public; Owner: me
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_email_key UNIQUE (email);


--
-- TOC entry 4736 (class 2606 OID 16441)
-- Name: users users_pkey; Type: CONSTRAINT; Schema: public; Owner: me
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_pkey PRIMARY KEY (id);


--
-- TOC entry 4738 (class 2606 OID 16443)
-- Name: users users_username_key; Type: CONSTRAINT; Schema: public; Owner: me
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_username_key UNIQUE (username);


--
-- TOC entry 4753 (class 2606 OID 16497)
-- Name: user_achievements id_achievement_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.user_achievements
    ADD CONSTRAINT id_achievement_fkey FOREIGN KEY (id_achievement) REFERENCES public.achievements(id) NOT VALID;


--
-- TOC entry 4754 (class 2606 OID 16492)
-- Name: user_achievements id_user_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.user_achievements
    ADD CONSTRAINT id_user_fkey FOREIGN KEY (id_user) REFERENCES public.users(id) NOT VALID;


--
-- TOC entry 4755 (class 2606 OID 16554)
-- Name: friends user1_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.friends
    ADD CONSTRAINT user1_fkey FOREIGN KEY (user1) REFERENCES public.users(id);


--
-- TOC entry 4757 (class 2606 OID 16569)
-- Name: friend_requests user1_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.friend_requests
    ADD CONSTRAINT user1_fkey FOREIGN KEY (user1) REFERENCES public.users(id);


--
-- TOC entry 4756 (class 2606 OID 16559)
-- Name: friends user2_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.friends
    ADD CONSTRAINT user2_fkey FOREIGN KEY (user2) REFERENCES public.users(id);


--
-- TOC entry 4758 (class 2606 OID 16574)
-- Name: friend_requests user2_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.friend_requests
    ADD CONSTRAINT user2_fkey FOREIGN KEY (user2) REFERENCES public.users(id);


--
-- TOC entry 4751 (class 2606 OID 16461)
-- Name: users users_rank_fkey; Type: FK CONSTRAINT; Schema: public; Owner: me
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_rank_fkey FOREIGN KEY (rank) REFERENCES public.ranks(rank) NOT VALID;


--
-- TOC entry 4752 (class 2606 OID 16472)
-- Name: users users_role_fkey; Type: FK CONSTRAINT; Schema: public; Owner: me
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_role_fkey FOREIGN KEY (role) REFERENCES public.roles(role) NOT VALID;


--
-- TOC entry 4914 (class 0 OID 0)
-- Dependencies: 6
-- Name: SCHEMA public; Type: ACL; Schema: -; Owner: pg_database_owner
--

GRANT ALL ON SCHEMA public TO me;


-- Completed on 2024-04-29 11:24:03

--
-- PostgreSQL database dump complete
--

