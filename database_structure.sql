--
-- PostgreSQL database dump
--

-- Dumped from database version 16.2
-- Dumped by pg_dump version 16.2

-- Started on 2024-04-23 16:55:14

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
-- TOC entry 4895 (class 0 OID 0)
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
    role text NOT NULL
);


ALTER TABLE public.users OWNER TO me;

--
-- TOC entry 4887 (class 0 OID 16477)
-- Dependencies: 219
-- Data for Name: achievements; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 4886 (class 0 OID 16449)
-- Dependencies: 218
-- Data for Name: ranks; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.ranks (rank) VALUES ('Bronze');
INSERT INTO public.ranks (rank) VALUES ('Silver');
INSERT INTO public.ranks (rank) VALUES ('Gold');
INSERT INTO public.ranks (rank) VALUES ('Platinum');
INSERT INTO public.ranks (rank) VALUES ('Diamond');


--
-- TOC entry 4884 (class 0 OID 16425)
-- Dependencies: 216
-- Data for Name: roles; Type: TABLE DATA; Schema: public; Owner: me
--

INSERT INTO public.roles (role) VALUES ('client');
INSERT INTO public.roles (role) VALUES ('server');


--
-- TOC entry 4888 (class 0 OID 16487)
-- Dependencies: 220
-- Data for Name: user_achievements; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 4885 (class 0 OID 16429)
-- Dependencies: 217
-- Data for Name: users; Type: TABLE DATA; Schema: public; Owner: me
--



--
-- TOC entry 4732 (class 2606 OID 16483)
-- Name: achievements achievements_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.achievements
    ADD CONSTRAINT achievements_pkey PRIMARY KEY (id);


--
-- TOC entry 4734 (class 2606 OID 16485)
-- Name: achievements id; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.achievements
    ADD CONSTRAINT id UNIQUE (id);


--
-- TOC entry 4730 (class 2606 OID 16455)
-- Name: ranks ranks_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.ranks
    ADD CONSTRAINT ranks_pkey PRIMARY KEY (rank);


--
-- TOC entry 4720 (class 2606 OID 16467)
-- Name: roles roles_name_key; Type: CONSTRAINT; Schema: public; Owner: me
--

ALTER TABLE ONLY public.roles
    ADD CONSTRAINT roles_name_key UNIQUE (role);


--
-- TOC entry 4722 (class 2606 OID 16471)
-- Name: roles roles_pkey; Type: CONSTRAINT; Schema: public; Owner: me
--

ALTER TABLE ONLY public.roles
    ADD CONSTRAINT roles_pkey PRIMARY KEY (role);


--
-- TOC entry 4736 (class 2606 OID 16491)
-- Name: user_achievements user_achievements_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.user_achievements
    ADD CONSTRAINT user_achievements_pkey PRIMARY KEY (id_user, id_achievement);


--
-- TOC entry 4724 (class 2606 OID 16439)
-- Name: users users_email_key; Type: CONSTRAINT; Schema: public; Owner: me
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_email_key UNIQUE (email);


--
-- TOC entry 4726 (class 2606 OID 16441)
-- Name: users users_pkey; Type: CONSTRAINT; Schema: public; Owner: me
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_pkey PRIMARY KEY (id);


--
-- TOC entry 4728 (class 2606 OID 16443)
-- Name: users users_username_key; Type: CONSTRAINT; Schema: public; Owner: me
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_username_key UNIQUE (username);


--
-- TOC entry 4739 (class 2606 OID 16497)
-- Name: user_achievements id_achievement_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.user_achievements
    ADD CONSTRAINT id_achievement_fkey FOREIGN KEY (id_achievement) REFERENCES public.achievements(id) NOT VALID;


--
-- TOC entry 4740 (class 2606 OID 16492)
-- Name: user_achievements id_user_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.user_achievements
    ADD CONSTRAINT id_user_fkey FOREIGN KEY (id_user) REFERENCES public.users(id) NOT VALID;


--
-- TOC entry 4737 (class 2606 OID 16461)
-- Name: users users_rank_fkey; Type: FK CONSTRAINT; Schema: public; Owner: me
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_rank_fkey FOREIGN KEY (rank) REFERENCES public.ranks(rank) NOT VALID;


--
-- TOC entry 4738 (class 2606 OID 16472)
-- Name: users users_role_fkey; Type: FK CONSTRAINT; Schema: public; Owner: me
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_role_fkey FOREIGN KEY (role) REFERENCES public.roles(role) NOT VALID;


--
-- TOC entry 4894 (class 0 OID 0)
-- Dependencies: 6
-- Name: SCHEMA public; Type: ACL; Schema: -; Owner: pg_database_owner
--

GRANT ALL ON SCHEMA public TO me;


-- Completed on 2024-04-23 16:55:17

--
-- PostgreSQL database dump complete
--

