# Schema docs

![ER diagram](er.svg)

<details>
<summary>The whole schema as SQL</summary>

```sql
CREATE SCHEMA IF NOT EXISTS "public";

CREATE TYPE "WorkspaceType" AS ENUM ('USER', 'TEAM');

CREATE TYPE "ReqType" AS ENUM ('REST', 'GQL');

CREATE TYPE "TeamAccessRole" AS ENUM ('OWNER', 'VIEWER', 'EDITOR');

CREATE TYPE "MockServerAction" AS ENUM ('CREATED', 'DELETED', 'ACTIVATED', 'DEACTIVATED');

CREATE TABLE "Team" (
    "id" TEXT NOT NULL,
    "name" TEXT NOT NULL,

    CONSTRAINT "Team_pkey" PRIMARY KEY ("id")
);

CREATE TABLE "TeamMember" (
    "id" TEXT NOT NULL,
    "role" "TeamAccessRole" NOT NULL,
    "userUid" TEXT NOT NULL,
    "teamID" TEXT NOT NULL,

    CONSTRAINT "TeamMember_pkey" PRIMARY KEY ("id")
);

CREATE TABLE "TeamInvitation" (
    "id" TEXT NOT NULL,
    "teamID" TEXT NOT NULL,
    "creatorUid" TEXT NOT NULL,
    "inviteeEmail" TEXT NOT NULL,
    "inviteeRole" "TeamAccessRole" NOT NULL,

    CONSTRAINT "TeamInvitation_pkey" PRIMARY KEY ("id")
);

CREATE TABLE "TeamCollection" (
    "id" TEXT NOT NULL,
    "parentID" TEXT,
    "teamID" TEXT NOT NULL,
    "title" TEXT NOT NULL,
    "orderIndex" INTEGER NOT NULL,
    "data" JSONB,
    "createdOn" TIMESTAMPTZ(3) NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "updatedOn" TIMESTAMPTZ(3) NOT NULL,

    CONSTRAINT "TeamCollection_pkey" PRIMARY KEY ("id")
);

CREATE TABLE "TeamRequest" (
    "id" TEXT NOT NULL,
    "collectionID" TEXT NOT NULL,
    "teamID" TEXT NOT NULL,
    "title" TEXT NOT NULL,
    "request" JSONB NOT NULL,
    "mockExamples" JSONB,
    "orderIndex" INTEGER NOT NULL,
    "createdOn" TIMESTAMPTZ(3) NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "updatedOn" TIMESTAMPTZ(3) NOT NULL,

    CONSTRAINT "TeamRequest_pkey" PRIMARY KEY ("id")
);

CREATE TABLE "TeamEnvironment" (
    "id" TEXT NOT NULL,
    "teamID" TEXT NOT NULL,
    "name" TEXT NOT NULL,
    "variables" JSONB NOT NULL,

    CONSTRAINT "TeamEnvironment_pkey" PRIMARY KEY ("id")
);

CREATE TABLE "User" (
    "uid" TEXT NOT NULL,
    "displayName" TEXT,
    "email" TEXT,
    "photoURL" TEXT,
    "isAdmin" BOOLEAN NOT NULL DEFAULT false,
    "refreshToken" TEXT,
    "currentRESTSession" JSONB,
    "currentGQLSession" JSONB,
    "createdOn" TIMESTAMPTZ(3) NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "lastLoggedOn" TIMESTAMPTZ(3),
    "lastActiveOn" TIMESTAMPTZ(3),

    CONSTRAINT "User_pkey" PRIMARY KEY ("uid")
);

CREATE TABLE "Account" (
    "id" TEXT NOT NULL,
    "userId" TEXT NOT NULL,
    "provider" TEXT NOT NULL,
    "providerAccountId" TEXT NOT NULL,
    "providerRefreshToken" TEXT,
    "providerAccessToken" TEXT,
    "providerScope" TEXT,
    "loggedIn" TIMESTAMPTZ(3) NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT "Account_pkey" PRIMARY KEY ("id")
);

CREATE TABLE "VerificationToken" (
    "deviceIdentifier" TEXT NOT NULL,
    "token" TEXT NOT NULL,
    "userUid" TEXT NOT NULL,
    "expiresOn" TIMESTAMPTZ(3) NOT NULL
);

CREATE TABLE "UserSettings" (
    "id" TEXT NOT NULL,
    "userUid" TEXT NOT NULL,
    "properties" JSONB NOT NULL,
    "updatedOn" TIMESTAMPTZ(3) NOT NULL,

    CONSTRAINT "UserSettings_pkey" PRIMARY KEY ("id")
);

CREATE TABLE "UserHistory" (
    "id" TEXT NOT NULL,
    "userUid" TEXT NOT NULL,
    "reqType" "ReqType" NOT NULL,
    "request" JSONB NOT NULL,
    "responseMetadata" JSONB NOT NULL,
    "isStarred" BOOLEAN NOT NULL,
    "executedOn" TIMESTAMPTZ(3) NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT "UserHistory_pkey" PRIMARY KEY ("id")
);

CREATE TABLE "UserEnvironment" (
    "id" TEXT NOT NULL,
    "userUid" TEXT NOT NULL,
    "name" TEXT,
    "variables" JSONB NOT NULL,
    "isGlobal" BOOLEAN NOT NULL,

    CONSTRAINT "UserEnvironment_pkey" PRIMARY KEY ("id")
);

CREATE TABLE "InvitedUsers" (
    "adminUid" TEXT NOT NULL,
    "adminEmail" TEXT NOT NULL,
    "inviteeEmail" TEXT NOT NULL,
    "invitedOn" TIMESTAMPTZ(3) NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE "UserRequest" (
    "id" TEXT NOT NULL,
    "collectionID" TEXT NOT NULL,
    "userUid" TEXT NOT NULL,
    "title" TEXT NOT NULL,
    "request" JSONB NOT NULL,
    "type" "ReqType" NOT NULL,
    "mockExamples" JSONB,
    "orderIndex" INTEGER NOT NULL,
    "createdOn" TIMESTAMPTZ(3) NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "updatedOn" TIMESTAMPTZ(3) NOT NULL,

    CONSTRAINT "UserRequest_pkey" PRIMARY KEY ("id")
);

CREATE TABLE "UserCollection" (
    "id" TEXT NOT NULL,
    "parentID" TEXT,
    "userUid" TEXT NOT NULL,
    "title" TEXT NOT NULL,
    "type" "ReqType" NOT NULL,
    "orderIndex" INTEGER NOT NULL,
    "data" JSONB,
    "createdOn" TIMESTAMPTZ(3) NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "updatedOn" TIMESTAMPTZ(3) NOT NULL,

    CONSTRAINT "UserCollection_pkey" PRIMARY KEY ("id")
);

CREATE TABLE "MockServer" (
    "id" TEXT NOT NULL,
    "name" TEXT NOT NULL,
    "subdomain" TEXT NOT NULL,
    "creatorUid" TEXT,
    "collectionID" TEXT NOT NULL,
    "workspaceType" "WorkspaceType" NOT NULL,
    "workspaceID" TEXT NOT NULL,
    "delayInMs" INTEGER NOT NULL DEFAULT 0,
    "isPublic" BOOLEAN NOT NULL DEFAULT true,
    "isActive" BOOLEAN NOT NULL DEFAULT true,
    "hitCount" INTEGER NOT NULL DEFAULT 0,
    "lastHitAt" TIMESTAMPTZ(3),
    "createdOn" TIMESTAMPTZ(3) NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "updatedOn" TIMESTAMPTZ(3) NOT NULL,
    "deletedAt" TIMESTAMPTZ(3),

    CONSTRAINT "MockServer_pkey" PRIMARY KEY ("id")
);

CREATE TABLE "MockServerLog" (
    "id" TEXT NOT NULL,
    "mockServerID" TEXT NOT NULL,
    "requestMethod" TEXT NOT NULL,
    "requestPath" TEXT NOT NULL,
    "requestHeaders" JSONB NOT NULL,
    "requestBody" JSONB,
    "requestQuery" JSONB,
    "responseStatus" INTEGER NOT NULL,
    "responseHeaders" JSONB NOT NULL,
    "responseBody" JSONB,
    "responseTime" INTEGER NOT NULL,
    "ipAddress" TEXT,
    "userAgent" TEXT,
    "executedAt" TIMESTAMPTZ(3) NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT "MockServerLog_pkey" PRIMARY KEY ("id")
);

CREATE TABLE "MockServerActivity" (
    "id" TEXT NOT NULL,
    "mockServerID" TEXT NOT NULL,
    "action" "MockServerAction" NOT NULL,
    "performedBy" TEXT,
    "performedAt" TIMESTAMPTZ(3) NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT "MockServerActivity_pkey" PRIMARY KEY ("id")
);

CREATE TABLE "Shortcode" (
    "id" TEXT NOT NULL,
    "request" JSONB NOT NULL,
    "creatorUid" TEXT,
    "embedProperties" JSONB,
    "createdOn" TIMESTAMPTZ(3) NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "updatedOn" TIMESTAMPTZ(3) NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT "Shortcode_pkey" PRIMARY KEY ("id")
);

CREATE TABLE "InfraConfig" (
    "id" TEXT NOT NULL,
    "name" TEXT NOT NULL,
    "value" TEXT,
    "isEncrypted" BOOLEAN NOT NULL DEFAULT false,
    "lastSyncedEnvFileValue" TEXT,
    "createdOn" TIMESTAMPTZ(3) NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "updatedOn" TIMESTAMPTZ(3) NOT NULL,

    CONSTRAINT "InfraConfig_pkey" PRIMARY KEY ("id")
);

CREATE TABLE "PersonalAccessToken" (
    "id" TEXT NOT NULL,
    "userUid" TEXT NOT NULL,
    "label" TEXT NOT NULL,
    "token" TEXT NOT NULL,
    "expiresOn" TIMESTAMPTZ(3),
    "createdOn" TIMESTAMPTZ(3) NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "updatedOn" TIMESTAMPTZ(3) NOT NULL,

    CONSTRAINT "PersonalAccessToken_pkey" PRIMARY KEY ("id")
);

CREATE TABLE "InfraToken" (
    "id" TEXT NOT NULL,
    "creatorUid" TEXT NOT NULL,
    "label" TEXT NOT NULL,
    "token" TEXT NOT NULL,
    "expiresOn" TIMESTAMPTZ(3),
    "createdOn" TIMESTAMPTZ(3) NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "updatedOn" TIMESTAMPTZ(3) NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT "InfraToken_pkey" PRIMARY KEY ("id")
);

CREATE TABLE "PublishedDocs" (
    "id" TEXT NOT NULL,
    "slug" TEXT NOT NULL,
    "title" TEXT NOT NULL,
    "collectionID" TEXT NOT NULL,
    "creatorUid" TEXT NOT NULL,
    "version" TEXT NOT NULL,
    "autoSync" BOOLEAN NOT NULL,
    "documentTree" JSONB,
    "workspaceType" "WorkspaceType" NOT NULL,
    "workspaceID" TEXT NOT NULL,
    "environmentID" TEXT,
    "environmentName" TEXT,
    "environmentVariables" JSONB,
    "metadata" JSONB,
    "createdOn" TIMESTAMPTZ(3) NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "updatedOn" TIMESTAMPTZ(3) NOT NULL,

    CONSTRAINT "PublishedDocs_pkey" PRIMARY KEY ("id")
);

CREATE UNIQUE INDEX "TeamMember_teamID_userUid_key" ON "TeamMember"("teamID", "userUid");

CREATE INDEX "TeamInvitation_teamID_idx" ON "TeamInvitation"("teamID");

CREATE UNIQUE INDEX "TeamInvitation_teamID_inviteeEmail_key" ON "TeamInvitation"("teamID", "inviteeEmail");

CREATE UNIQUE INDEX "TeamCollection_teamID_parentID_orderIndex_key" ON "TeamCollection"("teamID", "parentID", "orderIndex");

CREATE UNIQUE INDEX "TeamRequest_teamID_collectionID_orderIndex_key" ON "TeamRequest"("teamID", "collectionID", "orderIndex");

CREATE UNIQUE INDEX "User_email_key" ON "User"("email");

CREATE UNIQUE INDEX "Account_provider_providerAccountId_key" ON "Account"("provider", "providerAccountId");

CREATE UNIQUE INDEX "VerificationToken_token_key" ON "VerificationToken"("token");

CREATE UNIQUE INDEX "VerificationToken_deviceIdentifier_token_key" ON "VerificationToken"("deviceIdentifier", "token");

CREATE UNIQUE INDEX "UserSettings_userUid_key" ON "UserSettings"("userUid");

CREATE UNIQUE INDEX "InvitedUsers_inviteeEmail_key" ON "InvitedUsers"("inviteeEmail");

CREATE UNIQUE INDEX "UserRequest_userUid_collectionID_orderIndex_key" ON "UserRequest"("userUid", "collectionID", "orderIndex");

CREATE UNIQUE INDEX "UserCollection_userUid_parentID_orderIndex_key" ON "UserCollection"("userUid", "parentID", "orderIndex");

CREATE UNIQUE INDEX "MockServer_subdomain_key" ON "MockServer"("subdomain");

CREATE INDEX "MockServerLog_mockServerID_idx" ON "MockServerLog"("mockServerID");

CREATE INDEX "MockServerLog_mockServerID_executedAt_idx" ON "MockServerLog"("mockServerID", "executedAt");

CREATE INDEX "MockServerActivity_mockServerID_idx" ON "MockServerActivity"("mockServerID");

CREATE UNIQUE INDEX "Shortcode_id_key" ON "Shortcode"("id");

CREATE UNIQUE INDEX "Shortcode_id_creatorUid_key" ON "Shortcode"("id", "creatorUid");

CREATE UNIQUE INDEX "InfraConfig_name_key" ON "InfraConfig"("name");

CREATE UNIQUE INDEX "PersonalAccessToken_token_key" ON "PersonalAccessToken"("token");

CREATE UNIQUE INDEX "InfraToken_token_key" ON "InfraToken"("token");

CREATE INDEX "PublishedDocs_collectionID_idx" ON "PublishedDocs"("collectionID");

CREATE UNIQUE INDEX "PublishedDocs_slug_version_key" ON "PublishedDocs"("slug", "version");

ALTER TABLE "TeamMember" ADD CONSTRAINT "TeamMember_teamID_fkey" FOREIGN KEY ("teamID") REFERENCES "Team"("id") ON DELETE CASCADE ON UPDATE CASCADE;

ALTER TABLE "TeamInvitation" ADD CONSTRAINT "TeamInvitation_teamID_fkey" FOREIGN KEY ("teamID") REFERENCES "Team"("id") ON DELETE CASCADE ON UPDATE CASCADE;

ALTER TABLE "TeamCollection" ADD CONSTRAINT "TeamCollection_parentID_fkey" FOREIGN KEY ("parentID") REFERENCES "TeamCollection"("id") ON DELETE CASCADE ON UPDATE CASCADE;

ALTER TABLE "TeamCollection" ADD CONSTRAINT "TeamCollection_teamID_fkey" FOREIGN KEY ("teamID") REFERENCES "Team"("id") ON DELETE CASCADE ON UPDATE CASCADE;

ALTER TABLE "TeamRequest" ADD CONSTRAINT "TeamRequest_collectionID_fkey" FOREIGN KEY ("collectionID") REFERENCES "TeamCollection"("id") ON DELETE CASCADE ON UPDATE CASCADE;

ALTER TABLE "TeamRequest" ADD CONSTRAINT "TeamRequest_teamID_fkey" FOREIGN KEY ("teamID") REFERENCES "Team"("id") ON DELETE CASCADE ON UPDATE CASCADE;

ALTER TABLE "TeamEnvironment" ADD CONSTRAINT "TeamEnvironment_teamID_fkey" FOREIGN KEY ("teamID") REFERENCES "Team"("id") ON DELETE CASCADE ON UPDATE CASCADE;

ALTER TABLE "Account" ADD CONSTRAINT "Account_userId_fkey" FOREIGN KEY ("userId") REFERENCES "User"("uid") ON DELETE CASCADE ON UPDATE CASCADE;

ALTER TABLE "VerificationToken" ADD CONSTRAINT "VerificationToken_userUid_fkey" FOREIGN KEY ("userUid") REFERENCES "User"("uid") ON DELETE CASCADE ON UPDATE CASCADE;

ALTER TABLE "UserSettings" ADD CONSTRAINT "UserSettings_userUid_fkey" FOREIGN KEY ("userUid") REFERENCES "User"("uid") ON DELETE CASCADE ON UPDATE CASCADE;

ALTER TABLE "UserHistory" ADD CONSTRAINT "UserHistory_userUid_fkey" FOREIGN KEY ("userUid") REFERENCES "User"("uid") ON DELETE CASCADE ON UPDATE CASCADE;

ALTER TABLE "UserEnvironment" ADD CONSTRAINT "UserEnvironment_userUid_fkey" FOREIGN KEY ("userUid") REFERENCES "User"("uid") ON DELETE CASCADE ON UPDATE CASCADE;

ALTER TABLE "InvitedUsers" ADD CONSTRAINT "InvitedUsers_adminUid_fkey" FOREIGN KEY ("adminUid") REFERENCES "User"("uid") ON DELETE CASCADE ON UPDATE CASCADE;

ALTER TABLE "UserRequest" ADD CONSTRAINT "UserRequest_collectionID_fkey" FOREIGN KEY ("collectionID") REFERENCES "UserCollection"("id") ON DELETE CASCADE ON UPDATE CASCADE;

ALTER TABLE "UserRequest" ADD CONSTRAINT "UserRequest_userUid_fkey" FOREIGN KEY ("userUid") REFERENCES "User"("uid") ON DELETE CASCADE ON UPDATE CASCADE;

ALTER TABLE "UserCollection" ADD CONSTRAINT "UserCollection_parentID_fkey" FOREIGN KEY ("parentID") REFERENCES "UserCollection"("id") ON DELETE CASCADE ON UPDATE CASCADE;

ALTER TABLE "UserCollection" ADD CONSTRAINT "UserCollection_userUid_fkey" FOREIGN KEY ("userUid") REFERENCES "User"("uid") ON DELETE CASCADE ON UPDATE CASCADE;

ALTER TABLE "MockServer" ADD CONSTRAINT "MockServer_creatorUid_fkey" FOREIGN KEY ("creatorUid") REFERENCES "User"("uid") ON DELETE SET NULL ON UPDATE CASCADE;

ALTER TABLE "MockServerLog" ADD CONSTRAINT "MockServerLog_mockServerID_fkey" FOREIGN KEY ("mockServerID") REFERENCES "MockServer"("id") ON DELETE CASCADE ON UPDATE CASCADE;

ALTER TABLE "MockServerActivity" ADD CONSTRAINT "MockServerActivity_mockServerID_fkey" FOREIGN KEY ("mockServerID") REFERENCES "MockServer"("id") ON DELETE CASCADE ON UPDATE CASCADE;

ALTER TABLE "Shortcode" ADD CONSTRAINT "Shortcode_creatorUid_fkey" FOREIGN KEY ("creatorUid") REFERENCES "User"("uid") ON DELETE SET NULL ON UPDATE CASCADE;

ALTER TABLE "PersonalAccessToken" ADD CONSTRAINT "PersonalAccessToken_userUid_fkey" FOREIGN KEY ("userUid") REFERENCES "User"("uid") ON DELETE CASCADE ON UPDATE CASCADE;
```

</details>

## Contents

| Model                                             | Table                 | Description |
| ------------------------------------------------- | --------------------- | ----------- |
| [Team](#model-Team)                               | `Team`                |             |
| [TeamMember](#model-TeamMember)                   | `TeamMember`          |             |
| [TeamInvitation](#model-TeamInvitation)           | `TeamInvitation`      |             |
| [TeamCollection](#model-TeamCollection)           | `TeamCollection`      |             |
| [TeamRequest](#model-TeamRequest)                 | `TeamRequest`         |             |
| [TeamEnvironment](#model-TeamEnvironment)         | `TeamEnvironment`     |             |
| [User](#model-User)                               | `User`                |             |
| [Account](#model-Account)                         | `Account`             |             |
| [VerificationToken](#model-VerificationToken)     | `VerificationToken`   |             |
| [UserSettings](#model-UserSettings)               | `UserSettings`        |             |
| [UserHistory](#model-UserHistory)                 | `UserHistory`         |             |
| [UserEnvironment](#model-UserEnvironment)         | `UserEnvironment`     |             |
| [InvitedUsers](#model-InvitedUsers)               | `InvitedUsers`        |             |
| [UserRequest](#model-UserRequest)                 | `UserRequest`         |             |
| [UserCollection](#model-UserCollection)           | `UserCollection`      |             |
| [MockServer](#model-MockServer)                   | `MockServer`          |             |
| [MockServerLog](#model-MockServerLog)             | `MockServerLog`       |             |
| [MockServerActivity](#model-MockServerActivity)   | `MockServerActivity`  |             |
| [Shortcode](#model-Shortcode)                     | `Shortcode`           |             |
| [InfraConfig](#model-InfraConfig)                 | `InfraConfig`         |             |
| [PersonalAccessToken](#model-PersonalAccessToken) | `PersonalAccessToken` |             |
| [InfraToken](#model-InfraToken)                   | `InfraToken`          |             |
| [PublishedDocs](#model-PublishedDocs)             | `PublishedDocs`       |             |

| Enum                                       | Name in the database | Description |
| ------------------------------------------ | -------------------- | ----------- |
| [WorkspaceType](#enum-WorkspaceType)       | `WorkspaceType`      |             |
| [ReqType](#enum-ReqType)                   | `ReqType`            |             |
| [TeamAccessRole](#enum-TeamAccessRole)     | `TeamAccessRole`     |             |
| [MockServerAction](#enum-MockServerAction) | `MockServerAction`   |             |

## Models

<a id="model-Team"></a>

### Team

![Team and the models it relates to](tables/Team.svg)

| Field  | Type     | Constraints | Default   | Description |
| ------ | -------- | ----------- | --------- | ----------- |
| `id`   | `String` | PK NOT NULL | `cuid(1)` |             |
| `name` | `String` | NOT NULL    |           |             |

#### Referenced by

- `id` ← [TeamMember.teamID](#model-TeamMember) (one-to-many, on delete Cascade)
- `id` ← [TeamInvitation.teamID](#model-TeamInvitation) (one-to-many, on delete Cascade)
- `id` ← [TeamCollection.teamID](#model-TeamCollection) (one-to-many, on delete Cascade)
- `id` ← [TeamRequest.teamID](#model-TeamRequest) (one-to-many, on delete Cascade)
- `id` ← [TeamEnvironment.teamID](#model-TeamEnvironment) (one-to-many, on delete Cascade)

#### Definition

```sql
CREATE TABLE "Team" (
    "id" TEXT NOT NULL,
    "name" TEXT NOT NULL,

    CONSTRAINT "Team_pkey" PRIMARY KEY ("id")
);
```

<details>
<summary>Queries and their SQL</summary>

**findMany**: Every row

```ts
await prisma.team.findMany();
```

```sql
SELECT
  "public"."Team"."id",
  "public"."Team"."name"
FROM
  "public"."Team"
WHERE
  1 = 1
OFFSET
  $1
-- params: ["0"]
```

**count**: How many rows there are

```ts
await prisma.team.count();
```

```sql
SELECT
  COUNT(*) AS "_count$_all"
FROM
  (
    SELECT
      "public"."Team"."id"
    FROM
      "public"."Team"
    WHERE
      1 = 1
    OFFSET
      $1
  ) AS "sub"
-- params: ["0"]
```

**findMany**: Every row with its relations

```ts
await prisma.team.findMany({
  include: {
    collections: true,
    environments: true,
    invitations: true,
    members: true,
    requests: true,
  },
});
```

```sql
SELECT
  "public"."Team"."id",
  "public"."Team"."name"
FROM
  "public"."Team"
WHERE
  1 = 1
OFFSET
  $1
-- params: ["0"]

SELECT
  "public"."TeamCollection"."id",
  "public"."TeamCollection"."parentID",
  "public"."TeamCollection"."teamID",
  "public"."TeamCollection"."title",
  "public"."TeamCollection"."orderIndex",
  "public"."TeamCollection"."data",
  "public"."TeamCollection"."createdOn",
  "public"."TeamCollection"."updatedOn"
FROM
  "public"."TeamCollection"
WHERE
  "public"."TeamCollection"."teamID" IN ($1)
OFFSET
  $2
-- params: ["<id of the rows above>","0"]

SELECT
  "public"."TeamEnvironment"."id",
  "public"."TeamEnvironment"."teamID",
  "public"."TeamEnvironment"."name",
  "public"."TeamEnvironment"."variables"
FROM
  "public"."TeamEnvironment"
WHERE
  "public"."TeamEnvironment"."teamID" IN ($1)
OFFSET
  $2
-- params: ["<id of the rows above>","0"]

SELECT
  "public"."TeamInvitation"."id",
  "public"."TeamInvitation"."teamID",
  "public"."TeamInvitation"."creatorUid",
  "public"."TeamInvitation"."inviteeEmail",
  "public"."TeamInvitation"."inviteeRole"::text
FROM
  "public"."TeamInvitation"
WHERE
  "public"."TeamInvitation"."teamID" IN ($1)
OFFSET
  $2
-- params: ["<id of the rows above>","0"]

SELECT
  "public"."TeamMember"."id",
  "public"."TeamMember"."role"::text,
  "public"."TeamMember"."userUid",
  "public"."TeamMember"."teamID"
FROM
  "public"."TeamMember"
WHERE
  "public"."TeamMember"."teamID" IN ($1)
OFFSET
  $2
-- params: ["<id of the rows above>","0"]

SELECT
  "public"."TeamRequest"."id",
  "public"."TeamRequest"."collectionID",
  "public"."TeamRequest"."teamID",
  "public"."TeamRequest"."title",
  "public"."TeamRequest"."request",
  "public"."TeamRequest"."mockExamples",
  "public"."TeamRequest"."orderIndex",
  "public"."TeamRequest"."createdOn",
  "public"."TeamRequest"."updatedOn"
FROM
  "public"."TeamRequest"
WHERE
  "public"."TeamRequest"."teamID" IN ($1)
OFFSET
  $2
-- params: ["<id of the rows above>","0"]
```

**findUnique**: One row by what is unique to it

```ts
await prisma.team.findUnique({ where: { id: "example" } });
```

```sql
SELECT
  "public"."Team"."id",
  "public"."Team"."name"
FROM
  "public"."Team"
WHERE
  (
    "public"."Team"."id" = $1
    AND 1 = 1
  )
LIMIT
  $2
OFFSET
  $3
-- params: ["example","1","0"]
```

**create**: Create a row

```ts
await prisma.team.create({ data: { name: "example" } });
```

```sql
INSERT INTO
  "public"."Team" ("id", "name")
VALUES
  ($1, $2)
RETURNING
  "public"."Team"."id",
  "public"."Team"."name"
-- params: ["<cuid(1)>","example"]
```

**update**: Update a row

```ts
await prisma.team.update({ where: { id: "example" }, data: { name: "example" } });
```

```sql
UPDATE "public"."Team"
SET
  "name" = $1
WHERE
  (
    "public"."Team"."id" = $2
    AND 1 = 1
  )
RETURNING
  "public"."Team"."id",
  "public"."Team"."name"
-- params: ["example","example"]
```

**delete**: Delete a row

```ts
await prisma.team.delete({ where: { id: "example" } });
```

```sql
DELETE FROM "public"."Team"
WHERE
  (
    "public"."Team"."id" = $1
    AND 1 = 1
  )
RETURNING
  "public"."Team"."id",
  "public"."Team"."name"
-- params: ["example"]
```

</details>

<a id="model-TeamMember"></a>

### TeamMember

![TeamMember and the models it relates to](tables/TeamMember.svg)

| Field     | Type                                     | Constraints                       | Default   | Description |
| --------- | ---------------------------------------- | --------------------------------- | --------- | ----------- |
| `id`      | `String`                                 | PK NOT NULL                       | `uuid(4)` |             |
| `role`    | [`TeamAccessRole`](#enum-TeamAccessRole) | NOT NULL                          |           |             |
| `userUid` | `String`                                 | NOT NULL                          |           |             |
| `teamID`  | `String`                                 | NOT NULL → [Team.id](#model-Team) |           |             |

#### Indexes

- `@@unique([teamID, userUid])`

#### References

- `teamID` → [Team.id](#model-Team) (many-to-one, on delete Cascade)

#### Definition

```sql
CREATE TABLE "TeamMember" (
    "id" TEXT NOT NULL,
    "role" "TeamAccessRole" NOT NULL,
    "userUid" TEXT NOT NULL,
    "teamID" TEXT NOT NULL,

    CONSTRAINT "TeamMember_pkey" PRIMARY KEY ("id")
);

CREATE UNIQUE INDEX "TeamMember_teamID_userUid_key" ON "TeamMember"("teamID", "userUid");

ALTER TABLE "TeamMember" ADD CONSTRAINT "TeamMember_teamID_fkey" FOREIGN KEY ("teamID") REFERENCES "Team"("id") ON DELETE CASCADE ON UPDATE CASCADE;
```

<details>
<summary>Queries and their SQL</summary>

**findMany**: Every row

```ts
await prisma.teamMember.findMany();
```

```sql
SELECT
  "public"."TeamMember"."id",
  "public"."TeamMember"."role"::text,
  "public"."TeamMember"."userUid",
  "public"."TeamMember"."teamID"
FROM
  "public"."TeamMember"
WHERE
  1 = 1
OFFSET
  $1
-- params: ["0"]
```

**count**: How many rows there are

```ts
await prisma.teamMember.count();
```

```sql
SELECT
  COUNT(*) AS "_count$_all"
FROM
  (
    SELECT
      "public"."TeamMember"."id"
    FROM
      "public"."TeamMember"
    WHERE
      1 = 1
    OFFSET
      $1
  ) AS "sub"
-- params: ["0"]
```

**findMany**: Every row with its relations

```ts
await prisma.teamMember.findMany({ include: { team: true } });
```

```sql
SELECT
  "public"."TeamMember"."id",
  "public"."TeamMember"."role"::text,
  "public"."TeamMember"."userUid",
  "public"."TeamMember"."teamID"
FROM
  "public"."TeamMember"
WHERE
  1 = 1
OFFSET
  $1
-- params: ["0"]

SELECT
  "public"."Team"."id",
  "public"."Team"."name"
FROM
  "public"."Team"
WHERE
  "public"."Team"."id" IN ($1)
OFFSET
  $2
-- params: ["<teamID of the rows above>","0"]
```

**findUnique**: One row by what is unique to it

```ts
await prisma.teamMember.findUnique({ where: { id: "example" } });
```

```sql
SELECT
  "public"."TeamMember"."id",
  "public"."TeamMember"."role"::text,
  "public"."TeamMember"."userUid",
  "public"."TeamMember"."teamID"
FROM
  "public"."TeamMember"
WHERE
  (
    "public"."TeamMember"."id" = $1
    AND 1 = 1
  )
LIMIT
  $2
OFFSET
  $3
-- params: ["example","1","0"]
```

**create**: Create a row

```ts
await prisma.teamMember.create({ data: { role: "OWNER", userUid: "example", teamID: "example" } });
```

```sql
INSERT INTO
  "public"."TeamMember" ("id", "role", "userUid", "teamID")
VALUES
  (
    $1,
    CAST($2::text AS "public"."TeamAccessRole"),
    $3,
    $4
  )
RETURNING
  "public"."TeamMember"."id",
  "public"."TeamMember"."role"::text,
  "public"."TeamMember"."userUid",
  "public"."TeamMember"."teamID"
-- params: ["<uuid(4)>","OWNER","example","example"]
```

**update**: Update a row

```ts
await prisma.teamMember.update({ where: { id: "example" }, data: { role: "OWNER" } });
```

```sql
UPDATE "public"."TeamMember"
SET
  "role" = CAST($1::text AS "public"."TeamAccessRole")
WHERE
  (
    "public"."TeamMember"."id" = $2
    AND 1 = 1
  )
RETURNING
  "public"."TeamMember"."id",
  "public"."TeamMember"."role"::text,
  "public"."TeamMember"."userUid",
  "public"."TeamMember"."teamID"
-- params: ["OWNER","example"]
```

**delete**: Delete a row

```ts
await prisma.teamMember.delete({ where: { id: "example" } });
```

```sql
DELETE FROM "public"."TeamMember"
WHERE
  (
    "public"."TeamMember"."id" = $1
    AND 1 = 1
  )
RETURNING
  "public"."TeamMember"."id",
  "public"."TeamMember"."role"::text,
  "public"."TeamMember"."userUid",
  "public"."TeamMember"."teamID"
-- params: ["example"]
```

</details>

<a id="model-TeamInvitation"></a>

### TeamInvitation

![TeamInvitation and the models it relates to](tables/TeamInvitation.svg)

| Field          | Type                                     | Constraints                       | Default   | Description |
| -------------- | ---------------------------------------- | --------------------------------- | --------- | ----------- |
| `id`           | `String`                                 | PK NOT NULL                       | `cuid(1)` |             |
| `teamID`       | `String`                                 | NOT NULL → [Team.id](#model-Team) |           |             |
| `creatorUid`   | `String`                                 | NOT NULL                          |           |             |
| `inviteeEmail` | `String`                                 | NOT NULL                          |           |             |
| `inviteeRole`  | [`TeamAccessRole`](#enum-TeamAccessRole) | NOT NULL                          |           |             |

#### Indexes

- `@@index([teamID])`
- `@@unique([teamID, inviteeEmail])`

#### References

- `teamID` → [Team.id](#model-Team) (many-to-one, on delete Cascade)

#### Definition

```sql
CREATE TABLE "TeamInvitation" (
    "id" TEXT NOT NULL,
    "teamID" TEXT NOT NULL,
    "creatorUid" TEXT NOT NULL,
    "inviteeEmail" TEXT NOT NULL,
    "inviteeRole" "TeamAccessRole" NOT NULL,

    CONSTRAINT "TeamInvitation_pkey" PRIMARY KEY ("id")
);

CREATE INDEX "TeamInvitation_teamID_idx" ON "TeamInvitation"("teamID");

CREATE UNIQUE INDEX "TeamInvitation_teamID_inviteeEmail_key" ON "TeamInvitation"("teamID", "inviteeEmail");

ALTER TABLE "TeamInvitation" ADD CONSTRAINT "TeamInvitation_teamID_fkey" FOREIGN KEY ("teamID") REFERENCES "Team"("id") ON DELETE CASCADE ON UPDATE CASCADE;
```

<details>
<summary>Queries and their SQL</summary>

**findMany**: Every row

```ts
await prisma.teamInvitation.findMany();
```

```sql
SELECT
  "public"."TeamInvitation"."id",
  "public"."TeamInvitation"."teamID",
  "public"."TeamInvitation"."creatorUid",
  "public"."TeamInvitation"."inviteeEmail",
  "public"."TeamInvitation"."inviteeRole"::text
FROM
  "public"."TeamInvitation"
WHERE
  1 = 1
OFFSET
  $1
-- params: ["0"]
```

**count**: How many rows there are

```ts
await prisma.teamInvitation.count();
```

```sql
SELECT
  COUNT(*) AS "_count$_all"
FROM
  (
    SELECT
      "public"."TeamInvitation"."id"
    FROM
      "public"."TeamInvitation"
    WHERE
      1 = 1
    OFFSET
      $1
  ) AS "sub"
-- params: ["0"]
```

**findMany**: Every row with its relations

```ts
await prisma.teamInvitation.findMany({ include: { team: true } });
```

```sql
SELECT
  "public"."TeamInvitation"."id",
  "public"."TeamInvitation"."teamID",
  "public"."TeamInvitation"."creatorUid",
  "public"."TeamInvitation"."inviteeEmail",
  "public"."TeamInvitation"."inviteeRole"::text
FROM
  "public"."TeamInvitation"
WHERE
  1 = 1
OFFSET
  $1
-- params: ["0"]

SELECT
  "public"."Team"."id",
  "public"."Team"."name"
FROM
  "public"."Team"
WHERE
  "public"."Team"."id" IN ($1)
OFFSET
  $2
-- params: ["<teamID of the rows above>","0"]
```

**findUnique**: One row by what is unique to it

```ts
await prisma.teamInvitation.findUnique({ where: { id: "example" } });
```

```sql
SELECT
  "public"."TeamInvitation"."id",
  "public"."TeamInvitation"."teamID",
  "public"."TeamInvitation"."creatorUid",
  "public"."TeamInvitation"."inviteeEmail",
  "public"."TeamInvitation"."inviteeRole"::text
FROM
  "public"."TeamInvitation"
WHERE
  (
    "public"."TeamInvitation"."id" = $1
    AND 1 = 1
  )
LIMIT
  $2
OFFSET
  $3
-- params: ["example","1","0"]
```

**create**: Create a row

```ts
await prisma.teamInvitation.create({
  data: { teamID: "example", creatorUid: "example", inviteeEmail: "example", inviteeRole: "OWNER" },
});
```

```sql
INSERT INTO
  "public"."TeamInvitation" (
    "id",
    "teamID",
    "creatorUid",
    "inviteeEmail",
    "inviteeRole"
  )
VALUES
  (
    $1,
    $2,
    $3,
    $4,
    CAST($5::text AS "public"."TeamAccessRole")
  )
RETURNING
  "public"."TeamInvitation"."id",
  "public"."TeamInvitation"."teamID",
  "public"."TeamInvitation"."creatorUid",
  "public"."TeamInvitation"."inviteeEmail",
  "public"."TeamInvitation"."inviteeRole"::text
-- params: ["<cuid(1)>","example","example","example","OWNER"]
```

**update**: Update a row

```ts
await prisma.teamInvitation.update({ where: { id: "example" }, data: { creatorUid: "example" } });
```

```sql
UPDATE "public"."TeamInvitation"
SET
  "creatorUid" = $1
WHERE
  (
    "public"."TeamInvitation"."id" = $2
    AND 1 = 1
  )
RETURNING
  "public"."TeamInvitation"."id",
  "public"."TeamInvitation"."teamID",
  "public"."TeamInvitation"."creatorUid",
  "public"."TeamInvitation"."inviteeEmail",
  "public"."TeamInvitation"."inviteeRole"::text
-- params: ["example","example"]
```

**delete**: Delete a row

```ts
await prisma.teamInvitation.delete({ where: { id: "example" } });
```

```sql
DELETE FROM "public"."TeamInvitation"
WHERE
  (
    "public"."TeamInvitation"."id" = $1
    AND 1 = 1
  )
RETURNING
  "public"."TeamInvitation"."id",
  "public"."TeamInvitation"."teamID",
  "public"."TeamInvitation"."creatorUid",
  "public"."TeamInvitation"."inviteeEmail",
  "public"."TeamInvitation"."inviteeRole"::text
-- params: ["example"]
```

</details>

<a id="model-TeamCollection"></a>

### TeamCollection

![TeamCollection and the models it relates to](tables/TeamCollection.svg)

| Field        | Type                            | Constraints                                       | Default      | Description |
| ------------ | ------------------------------- | ------------------------------------------------- | ------------ | ----------- |
| `id`         | `String`                        | PK NOT NULL                                       | `cuid(1)`    |             |
| `parentID`   | `String`                        | NULL → [TeamCollection.id](#model-TeamCollection) |              |             |
| `teamID`     | `String`                        | NOT NULL → [Team.id](#model-Team)                 |              |             |
| `title`      | `String`                        | NOT NULL                                          |              |             |
| `orderIndex` | `Int`                           | NOT NULL                                          |              |             |
| `data`       | `Json`                          | NULL                                              |              |             |
| `createdOn`  | `DateTime` `@db.Timestamptz(3)` | NOT NULL                                          | `now()`      |             |
| `updatedOn`  | `DateTime` `@db.Timestamptz(3)` | NOT NULL                                          | `@updatedAt` |             |

#### Indexes

- `@@unique([teamID, parentID, orderIndex])`

#### References

- `parentID` → [TeamCollection.id](#model-TeamCollection) (many-to-one, on delete Cascade)
- `teamID` → [Team.id](#model-Team) (many-to-one, on delete Cascade)

#### Referenced by

- `id` ← [TeamCollection.parentID](#model-TeamCollection) (one-to-many, on delete Cascade)
- `id` ← [TeamRequest.collectionID](#model-TeamRequest) (one-to-many, on delete Cascade)

#### Definition

```sql
CREATE TABLE "TeamCollection" (
    "id" TEXT NOT NULL,
    "parentID" TEXT,
    "teamID" TEXT NOT NULL,
    "title" TEXT NOT NULL,
    "orderIndex" INTEGER NOT NULL,
    "data" JSONB,
    "createdOn" TIMESTAMPTZ(3) NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "updatedOn" TIMESTAMPTZ(3) NOT NULL,

    CONSTRAINT "TeamCollection_pkey" PRIMARY KEY ("id")
);

CREATE UNIQUE INDEX "TeamCollection_teamID_parentID_orderIndex_key" ON "TeamCollection"("teamID", "parentID", "orderIndex");

ALTER TABLE "TeamCollection" ADD CONSTRAINT "TeamCollection_parentID_fkey" FOREIGN KEY ("parentID") REFERENCES "TeamCollection"("id") ON DELETE CASCADE ON UPDATE CASCADE;

ALTER TABLE "TeamCollection" ADD CONSTRAINT "TeamCollection_teamID_fkey" FOREIGN KEY ("teamID") REFERENCES "Team"("id") ON DELETE CASCADE ON UPDATE CASCADE;
```

<details>
<summary>Queries and their SQL</summary>

**findMany**: Every row

```ts
await prisma.teamCollection.findMany();
```

```sql
SELECT
  "public"."TeamCollection"."id",
  "public"."TeamCollection"."parentID",
  "public"."TeamCollection"."teamID",
  "public"."TeamCollection"."title",
  "public"."TeamCollection"."orderIndex",
  "public"."TeamCollection"."data",
  "public"."TeamCollection"."createdOn",
  "public"."TeamCollection"."updatedOn"
FROM
  "public"."TeamCollection"
WHERE
  1 = 1
OFFSET
  $1
-- params: ["0"]
```

**count**: How many rows there are

```ts
await prisma.teamCollection.count();
```

```sql
SELECT
  COUNT(*) AS "_count$_all"
FROM
  (
    SELECT
      "public"."TeamCollection"."id"
    FROM
      "public"."TeamCollection"
    WHERE
      1 = 1
    OFFSET
      $1
  ) AS "sub"
-- params: ["0"]
```

**findMany**: Every row with its relations

```ts
await prisma.teamCollection.findMany({
  include: { parent: true, children: true, team: true, requests: true },
});
```

```sql
SELECT
  "public"."TeamCollection"."id",
  "public"."TeamCollection"."parentID",
  "public"."TeamCollection"."teamID",
  "public"."TeamCollection"."title",
  "public"."TeamCollection"."orderIndex",
  "public"."TeamCollection"."data",
  "public"."TeamCollection"."createdOn",
  "public"."TeamCollection"."updatedOn"
FROM
  "public"."TeamCollection"
WHERE
  1 = 1
OFFSET
  $1
-- params: ["0"]

SELECT
  "public"."TeamCollection"."id",
  "public"."TeamCollection"."parentID",
  "public"."TeamCollection"."teamID",
  "public"."TeamCollection"."title",
  "public"."TeamCollection"."orderIndex",
  "public"."TeamCollection"."data",
  "public"."TeamCollection"."createdOn",
  "public"."TeamCollection"."updatedOn"
FROM
  "public"."TeamCollection"
WHERE
  "public"."TeamCollection"."id" IN ($1)
OFFSET
  $2
-- params: ["<parentID of the rows above>","0"]

SELECT
  "public"."TeamCollection"."id",
  "public"."TeamCollection"."parentID",
  "public"."TeamCollection"."teamID",
  "public"."TeamCollection"."title",
  "public"."TeamCollection"."orderIndex",
  "public"."TeamCollection"."data",
  "public"."TeamCollection"."createdOn",
  "public"."TeamCollection"."updatedOn"
FROM
  "public"."TeamCollection"
WHERE
  "public"."TeamCollection"."parentID" IN ($1)
OFFSET
  $2
-- params: ["<id of the rows above>","0"]

SELECT
  "public"."Team"."id",
  "public"."Team"."name"
FROM
  "public"."Team"
WHERE
  "public"."Team"."id" IN ($1)
OFFSET
  $2
-- params: ["<teamID of the rows above>","0"]

SELECT
  "public"."TeamRequest"."id",
  "public"."TeamRequest"."collectionID",
  "public"."TeamRequest"."teamID",
  "public"."TeamRequest"."title",
  "public"."TeamRequest"."request",
  "public"."TeamRequest"."mockExamples",
  "public"."TeamRequest"."orderIndex",
  "public"."TeamRequest"."createdOn",
  "public"."TeamRequest"."updatedOn"
FROM
  "public"."TeamRequest"
WHERE
  "public"."TeamRequest"."collectionID" IN ($1)
OFFSET
  $2
-- params: ["<id of the rows above>","0"]
```

**findUnique**: One row by what is unique to it

```ts
await prisma.teamCollection.findUnique({ where: { id: "example" } });
```

```sql
SELECT
  "public"."TeamCollection"."id",
  "public"."TeamCollection"."parentID",
  "public"."TeamCollection"."teamID",
  "public"."TeamCollection"."title",
  "public"."TeamCollection"."orderIndex",
  "public"."TeamCollection"."data",
  "public"."TeamCollection"."createdOn",
  "public"."TeamCollection"."updatedOn"
FROM
  "public"."TeamCollection"
WHERE
  (
    "public"."TeamCollection"."id" = $1
    AND 1 = 1
  )
LIMIT
  $2
OFFSET
  $3
-- params: ["example","1","0"]
```

**create**: Create a row

```ts
await prisma.teamCollection.create({
  data: { teamID: "example", title: "example", orderIndex: 1 },
});
```

```sql
INSERT INTO
  "public"."TeamCollection" (
    "id",
    "teamID",
    "title",
    "orderIndex",
    "createdOn",
    "updatedOn"
  )
VALUES
  ($1, $2, $3, $4, $5, $6)
RETURNING
  "public"."TeamCollection"."id",
  "public"."TeamCollection"."parentID",
  "public"."TeamCollection"."teamID",
  "public"."TeamCollection"."title",
  "public"."TeamCollection"."orderIndex",
  "public"."TeamCollection"."data",
  "public"."TeamCollection"."createdOn",
  "public"."TeamCollection"."updatedOn"
-- params: ["<cuid(1)>","example","example","1","<now()>","<now()>"]
```

**update**: Update a row

```ts
await prisma.teamCollection.update({ where: { id: "example" }, data: { title: "example" } });
```

```sql
UPDATE "public"."TeamCollection"
SET
  "title" = $1,
  "updatedOn" = $2
WHERE
  (
    "public"."TeamCollection"."id" = $3
    AND 1 = 1
  )
RETURNING
  "public"."TeamCollection"."id",
  "public"."TeamCollection"."parentID",
  "public"."TeamCollection"."teamID",
  "public"."TeamCollection"."title",
  "public"."TeamCollection"."orderIndex",
  "public"."TeamCollection"."data",
  "public"."TeamCollection"."createdOn",
  "public"."TeamCollection"."updatedOn"
-- params: ["example",{"prisma__type":"generatorCall","prisma__value":{"name":"now","args":[],"returnType":{"type":"DateTime"}}},"example"]
```

**delete**: Delete a row

```ts
await prisma.teamCollection.delete({ where: { id: "example" } });
```

```sql
DELETE FROM "public"."TeamCollection"
WHERE
  (
    "public"."TeamCollection"."id" = $1
    AND 1 = 1
  )
RETURNING
  "public"."TeamCollection"."id",
  "public"."TeamCollection"."parentID",
  "public"."TeamCollection"."teamID",
  "public"."TeamCollection"."title",
  "public"."TeamCollection"."orderIndex",
  "public"."TeamCollection"."data",
  "public"."TeamCollection"."createdOn",
  "public"."TeamCollection"."updatedOn"
-- params: ["example"]
```

</details>

<a id="model-TeamRequest"></a>

### TeamRequest

![TeamRequest and the models it relates to](tables/TeamRequest.svg)

| Field          | Type                            | Constraints                                           | Default      | Description |
| -------------- | ------------------------------- | ----------------------------------------------------- | ------------ | ----------- |
| `id`           | `String`                        | PK NOT NULL                                           | `cuid(1)`    |             |
| `collectionID` | `String`                        | NOT NULL → [TeamCollection.id](#model-TeamCollection) |              |             |
| `teamID`       | `String`                        | NOT NULL → [Team.id](#model-Team)                     |              |             |
| `title`        | `String`                        | NOT NULL                                              |              |             |
| `request`      | `Json`                          | NOT NULL                                              |              |             |
| `mockExamples` | `Json`                          | NULL                                                  |              |             |
| `orderIndex`   | `Int`                           | NOT NULL                                              |              |             |
| `createdOn`    | `DateTime` `@db.Timestamptz(3)` | NOT NULL                                              | `now()`      |             |
| `updatedOn`    | `DateTime` `@db.Timestamptz(3)` | NOT NULL                                              | `@updatedAt` |             |

#### Indexes

- `@@unique([teamID, collectionID, orderIndex])`

#### References

- `collectionID` → [TeamCollection.id](#model-TeamCollection) (many-to-one, on delete Cascade)
- `teamID` → [Team.id](#model-Team) (many-to-one, on delete Cascade)

#### Definition

```sql
CREATE TABLE "TeamRequest" (
    "id" TEXT NOT NULL,
    "collectionID" TEXT NOT NULL,
    "teamID" TEXT NOT NULL,
    "title" TEXT NOT NULL,
    "request" JSONB NOT NULL,
    "mockExamples" JSONB,
    "orderIndex" INTEGER NOT NULL,
    "createdOn" TIMESTAMPTZ(3) NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "updatedOn" TIMESTAMPTZ(3) NOT NULL,

    CONSTRAINT "TeamRequest_pkey" PRIMARY KEY ("id")
);

CREATE UNIQUE INDEX "TeamRequest_teamID_collectionID_orderIndex_key" ON "TeamRequest"("teamID", "collectionID", "orderIndex");

ALTER TABLE "TeamRequest" ADD CONSTRAINT "TeamRequest_collectionID_fkey" FOREIGN KEY ("collectionID") REFERENCES "TeamCollection"("id") ON DELETE CASCADE ON UPDATE CASCADE;

ALTER TABLE "TeamRequest" ADD CONSTRAINT "TeamRequest_teamID_fkey" FOREIGN KEY ("teamID") REFERENCES "Team"("id") ON DELETE CASCADE ON UPDATE CASCADE;
```

<details>
<summary>Queries and their SQL</summary>

**findMany**: Every row

```ts
await prisma.teamRequest.findMany();
```

```sql
SELECT
  "public"."TeamRequest"."id",
  "public"."TeamRequest"."collectionID",
  "public"."TeamRequest"."teamID",
  "public"."TeamRequest"."title",
  "public"."TeamRequest"."request",
  "public"."TeamRequest"."mockExamples",
  "public"."TeamRequest"."orderIndex",
  "public"."TeamRequest"."createdOn",
  "public"."TeamRequest"."updatedOn"
FROM
  "public"."TeamRequest"
WHERE
  1 = 1
OFFSET
  $1
-- params: ["0"]
```

**count**: How many rows there are

```ts
await prisma.teamRequest.count();
```

```sql
SELECT
  COUNT(*) AS "_count$_all"
FROM
  (
    SELECT
      "public"."TeamRequest"."id"
    FROM
      "public"."TeamRequest"
    WHERE
      1 = 1
    OFFSET
      $1
  ) AS "sub"
-- params: ["0"]
```

**findMany**: Every row with its relations

```ts
await prisma.teamRequest.findMany({ include: { collection: true, team: true } });
```

```sql
SELECT
  "public"."TeamRequest"."id",
  "public"."TeamRequest"."collectionID",
  "public"."TeamRequest"."teamID",
  "public"."TeamRequest"."title",
  "public"."TeamRequest"."request",
  "public"."TeamRequest"."mockExamples",
  "public"."TeamRequest"."orderIndex",
  "public"."TeamRequest"."createdOn",
  "public"."TeamRequest"."updatedOn"
FROM
  "public"."TeamRequest"
WHERE
  1 = 1
OFFSET
  $1
-- params: ["0"]

SELECT
  "public"."TeamCollection"."id",
  "public"."TeamCollection"."parentID",
  "public"."TeamCollection"."teamID",
  "public"."TeamCollection"."title",
  "public"."TeamCollection"."orderIndex",
  "public"."TeamCollection"."data",
  "public"."TeamCollection"."createdOn",
  "public"."TeamCollection"."updatedOn"
FROM
  "public"."TeamCollection"
WHERE
  "public"."TeamCollection"."id" IN ($1)
OFFSET
  $2
-- params: ["<collectionID of the rows above>","0"]

SELECT
  "public"."Team"."id",
  "public"."Team"."name"
FROM
  "public"."Team"
WHERE
  "public"."Team"."id" IN ($1)
OFFSET
  $2
-- params: ["<teamID of the rows above>","0"]
```

**findUnique**: One row by what is unique to it

```ts
await prisma.teamRequest.findUnique({ where: { id: "example" } });
```

```sql
SELECT
  "public"."TeamRequest"."id",
  "public"."TeamRequest"."collectionID",
  "public"."TeamRequest"."teamID",
  "public"."TeamRequest"."title",
  "public"."TeamRequest"."request",
  "public"."TeamRequest"."mockExamples",
  "public"."TeamRequest"."orderIndex",
  "public"."TeamRequest"."createdOn",
  "public"."TeamRequest"."updatedOn"
FROM
  "public"."TeamRequest"
WHERE
  (
    "public"."TeamRequest"."id" = $1
    AND 1 = 1
  )
LIMIT
  $2
OFFSET
  $3
-- params: ["example","1","0"]
```

**create**: Create a row

```ts
await prisma.teamRequest.create({
  data: {
    collectionID: "example",
    teamID: "example",
    title: "example",
    request: {},
    orderIndex: 1,
  },
});
```

```sql
INSERT INTO
  "public"."TeamRequest" (
    "id",
    "collectionID",
    "teamID",
    "title",
    "request",
    "orderIndex",
    "createdOn",
    "updatedOn"
  )
VALUES
  ($1, $2, $3, $4, $5, $6, $7, $8)
RETURNING
  "public"."TeamRequest"."id",
  "public"."TeamRequest"."collectionID",
  "public"."TeamRequest"."teamID",
  "public"."TeamRequest"."title",
  "public"."TeamRequest"."request",
  "public"."TeamRequest"."mockExamples",
  "public"."TeamRequest"."orderIndex",
  "public"."TeamRequest"."createdOn",
  "public"."TeamRequest"."updatedOn"
-- params: ["<cuid(1)>","example","example","example","{}","1","<now()>","<now()>"]
```

**update**: Update a row

```ts
await prisma.teamRequest.update({ where: { id: "example" }, data: { title: "example" } });
```

```sql
UPDATE "public"."TeamRequest"
SET
  "title" = $1,
  "updatedOn" = $2
WHERE
  (
    "public"."TeamRequest"."id" = $3
    AND 1 = 1
  )
RETURNING
  "public"."TeamRequest"."id",
  "public"."TeamRequest"."collectionID",
  "public"."TeamRequest"."teamID",
  "public"."TeamRequest"."title",
  "public"."TeamRequest"."request",
  "public"."TeamRequest"."mockExamples",
  "public"."TeamRequest"."orderIndex",
  "public"."TeamRequest"."createdOn",
  "public"."TeamRequest"."updatedOn"
-- params: ["example",{"prisma__type":"generatorCall","prisma__value":{"name":"now","args":[],"returnType":{"type":"DateTime"}}},"example"]
```

**delete**: Delete a row

```ts
await prisma.teamRequest.delete({ where: { id: "example" } });
```

```sql
DELETE FROM "public"."TeamRequest"
WHERE
  (
    "public"."TeamRequest"."id" = $1
    AND 1 = 1
  )
RETURNING
  "public"."TeamRequest"."id",
  "public"."TeamRequest"."collectionID",
  "public"."TeamRequest"."teamID",
  "public"."TeamRequest"."title",
  "public"."TeamRequest"."request",
  "public"."TeamRequest"."mockExamples",
  "public"."TeamRequest"."orderIndex",
  "public"."TeamRequest"."createdOn",
  "public"."TeamRequest"."updatedOn"
-- params: ["example"]
```

</details>

<a id="model-TeamEnvironment"></a>

### TeamEnvironment

![TeamEnvironment and the models it relates to](tables/TeamEnvironment.svg)

| Field       | Type     | Constraints                       | Default   | Description |
| ----------- | -------- | --------------------------------- | --------- | ----------- |
| `id`        | `String` | PK NOT NULL                       | `cuid(1)` |             |
| `teamID`    | `String` | NOT NULL → [Team.id](#model-Team) |           |             |
| `name`      | `String` | NOT NULL                          |           |             |
| `variables` | `Json`   | NOT NULL                          |           |             |

#### References

- `teamID` → [Team.id](#model-Team) (many-to-one, on delete Cascade)

#### Definition

```sql
CREATE TABLE "TeamEnvironment" (
    "id" TEXT NOT NULL,
    "teamID" TEXT NOT NULL,
    "name" TEXT NOT NULL,
    "variables" JSONB NOT NULL,

    CONSTRAINT "TeamEnvironment_pkey" PRIMARY KEY ("id")
);

ALTER TABLE "TeamEnvironment" ADD CONSTRAINT "TeamEnvironment_teamID_fkey" FOREIGN KEY ("teamID") REFERENCES "Team"("id") ON DELETE CASCADE ON UPDATE CASCADE;
```

<details>
<summary>Queries and their SQL</summary>

**findMany**: Every row

```ts
await prisma.teamEnvironment.findMany();
```

```sql
SELECT
  "public"."TeamEnvironment"."id",
  "public"."TeamEnvironment"."teamID",
  "public"."TeamEnvironment"."name",
  "public"."TeamEnvironment"."variables"
FROM
  "public"."TeamEnvironment"
WHERE
  1 = 1
OFFSET
  $1
-- params: ["0"]
```

**count**: How many rows there are

```ts
await prisma.teamEnvironment.count();
```

```sql
SELECT
  COUNT(*) AS "_count$_all"
FROM
  (
    SELECT
      "public"."TeamEnvironment"."id"
    FROM
      "public"."TeamEnvironment"
    WHERE
      1 = 1
    OFFSET
      $1
  ) AS "sub"
-- params: ["0"]
```

**findMany**: Every row with its relations

```ts
await prisma.teamEnvironment.findMany({ include: { team: true } });
```

```sql
SELECT
  "public"."TeamEnvironment"."id",
  "public"."TeamEnvironment"."teamID",
  "public"."TeamEnvironment"."name",
  "public"."TeamEnvironment"."variables"
FROM
  "public"."TeamEnvironment"
WHERE
  1 = 1
OFFSET
  $1
-- params: ["0"]

SELECT
  "public"."Team"."id",
  "public"."Team"."name"
FROM
  "public"."Team"
WHERE
  "public"."Team"."id" IN ($1)
OFFSET
  $2
-- params: ["<teamID of the rows above>","0"]
```

**findUnique**: One row by what is unique to it

```ts
await prisma.teamEnvironment.findUnique({ where: { id: "example" } });
```

```sql
SELECT
  "public"."TeamEnvironment"."id",
  "public"."TeamEnvironment"."teamID",
  "public"."TeamEnvironment"."name",
  "public"."TeamEnvironment"."variables"
FROM
  "public"."TeamEnvironment"
WHERE
  (
    "public"."TeamEnvironment"."id" = $1
    AND 1 = 1
  )
LIMIT
  $2
OFFSET
  $3
-- params: ["example","1","0"]
```

**create**: Create a row

```ts
await prisma.teamEnvironment.create({
  data: { teamID: "example", name: "example", variables: {} },
});
```

```sql
INSERT INTO
  "public"."TeamEnvironment" ("id", "teamID", "name", "variables")
VALUES
  ($1, $2, $3, $4)
RETURNING
  "public"."TeamEnvironment"."id",
  "public"."TeamEnvironment"."teamID",
  "public"."TeamEnvironment"."name",
  "public"."TeamEnvironment"."variables"
-- params: ["<cuid(1)>","example","example","{}"]
```

**update**: Update a row

```ts
await prisma.teamEnvironment.update({ where: { id: "example" }, data: { name: "example" } });
```

```sql
UPDATE "public"."TeamEnvironment"
SET
  "name" = $1
WHERE
  (
    "public"."TeamEnvironment"."id" = $2
    AND 1 = 1
  )
RETURNING
  "public"."TeamEnvironment"."id",
  "public"."TeamEnvironment"."teamID",
  "public"."TeamEnvironment"."name",
  "public"."TeamEnvironment"."variables"
-- params: ["example","example"]
```

**delete**: Delete a row

```ts
await prisma.teamEnvironment.delete({ where: { id: "example" } });
```

```sql
DELETE FROM "public"."TeamEnvironment"
WHERE
  (
    "public"."TeamEnvironment"."id" = $1
    AND 1 = 1
  )
RETURNING
  "public"."TeamEnvironment"."id",
  "public"."TeamEnvironment"."teamID",
  "public"."TeamEnvironment"."name",
  "public"."TeamEnvironment"."variables"
-- params: ["example"]
```

</details>

<a id="model-User"></a>

### User

![User and the models it relates to](tables/User.svg)

| Field                | Type                            | Constraints | Default   | Description |
| -------------------- | ------------------------------- | ----------- | --------- | ----------- |
| `uid`                | `String`                        | PK NOT NULL | `cuid(1)` |             |
| `displayName`        | `String`                        | NULL        |           |             |
| `email`              | `String`                        | UNIQUE NULL |           |             |
| `photoURL`           | `String`                        | NULL        |           |             |
| `isAdmin`            | `Boolean`                       | NOT NULL    | `false`   |             |
| `refreshToken`       | `String`                        | NULL        |           |             |
| `currentRESTSession` | `Json`                          | NULL        |           |             |
| `currentGQLSession`  | `Json`                          | NULL        |           |             |
| `createdOn`          | `DateTime` `@db.Timestamptz(3)` | NOT NULL    | `now()`   |             |
| `lastLoggedOn`       | `DateTime` `@db.Timestamptz(3)` | NULL        |           |             |
| `lastActiveOn`       | `DateTime` `@db.Timestamptz(3)` | NULL        |           |             |

#### Referenced by

- `uid` ← [Account.userId](#model-Account) (one-to-many, on delete Cascade)
- `uid` ← [VerificationToken.userUid](#model-VerificationToken) (one-to-many, on delete Cascade)
- `uid` ← [UserSettings.userUid](#model-UserSettings) (one-to-one, on delete Cascade)
- `uid` ← [UserHistory.userUid](#model-UserHistory) (one-to-many, on delete Cascade)
- `uid` ← [UserEnvironment.userUid](#model-UserEnvironment) (one-to-many, on delete Cascade)
- `uid` ← [InvitedUsers.adminUid](#model-InvitedUsers) (one-to-many, on delete Cascade)
- `uid` ← [UserRequest.userUid](#model-UserRequest) (one-to-many, on delete Cascade)
- `uid` ← [UserCollection.userUid](#model-UserCollection) (one-to-many, on delete Cascade)
- `uid` ← [MockServer.creatorUid](#model-MockServer) (one-to-many, on delete SetNull)
- `uid` ← [Shortcode.creatorUid](#model-Shortcode) (one-to-many)
- `uid` ← [PersonalAccessToken.userUid](#model-PersonalAccessToken) (one-to-many, on delete Cascade)

#### Definition

```sql
CREATE TABLE "User" (
    "uid" TEXT NOT NULL,
    "displayName" TEXT,
    "email" TEXT,
    "photoURL" TEXT,
    "isAdmin" BOOLEAN NOT NULL DEFAULT false,
    "refreshToken" TEXT,
    "currentRESTSession" JSONB,
    "currentGQLSession" JSONB,
    "createdOn" TIMESTAMPTZ(3) NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "lastLoggedOn" TIMESTAMPTZ(3),
    "lastActiveOn" TIMESTAMPTZ(3),

    CONSTRAINT "User_pkey" PRIMARY KEY ("uid")
);

CREATE UNIQUE INDEX "User_email_key" ON "User"("email");
```

<details>
<summary>Queries and their SQL</summary>

**findMany**: Every row

```ts
await prisma.user.findMany();
```

```sql
SELECT
  "public"."User"."uid",
  "public"."User"."displayName",
  "public"."User"."email",
  "public"."User"."photoURL",
  "public"."User"."isAdmin",
  "public"."User"."refreshToken",
  "public"."User"."currentRESTSession",
  "public"."User"."currentGQLSession",
  "public"."User"."createdOn",
  "public"."User"."lastLoggedOn",
  "public"."User"."lastActiveOn"
FROM
  "public"."User"
WHERE
  1 = 1
OFFSET
  $1
-- params: ["0"]
```

**count**: How many rows there are

```ts
await prisma.user.count();
```

```sql
SELECT
  COUNT(*) AS "_count$_all"
FROM
  (
    SELECT
      "public"."User"."uid"
    FROM
      "public"."User"
    WHERE
      1 = 1
    OFFSET
      $1
  ) AS "sub"
-- params: ["0"]
```

**findMany**: Every row with its relations

```ts
await prisma.user.findMany({
  include: {
    providerAccounts: true,
    invitedUsers: true,
    mockServers: true,
    personalAccessTokens: true,
    shortcodes: true,
    userCollections: true,
    environments: true,
    history: true,
    userRequests: true,
    settings: true,
    verificationTokens: true,
  },
});
```

```sql
SELECT
  "public"."User"."uid",
  "public"."User"."displayName",
  "public"."User"."email",
  "public"."User"."photoURL",
  "public"."User"."isAdmin",
  "public"."User"."refreshToken",
  "public"."User"."currentRESTSession",
  "public"."User"."currentGQLSession",
  "public"."User"."createdOn",
  "public"."User"."lastLoggedOn",
  "public"."User"."lastActiveOn"
FROM
  "public"."User"
WHERE
  1 = 1
OFFSET
  $1
-- params: ["0"]

SELECT
  "public"."Account"."id",
  "public"."Account"."userId",
  "public"."Account"."provider",
  "public"."Account"."providerAccountId",
  "public"."Account"."providerRefreshToken",
  "public"."Account"."providerAccessToken",
  "public"."Account"."providerScope",
  "public"."Account"."loggedIn"
FROM
  "public"."Account"
WHERE
  "public"."Account"."userId" IN ($1)
OFFSET
  $2
-- params: ["<uid of the rows above>","0"]

SELECT
  "public"."InvitedUsers"."inviteeEmail",
  "public"."InvitedUsers"."adminUid",
  "public"."InvitedUsers"."adminEmail",
  "public"."InvitedUsers"."invitedOn"
FROM
  "public"."InvitedUsers"
WHERE
  "public"."InvitedUsers"."adminUid" IN ($1)
OFFSET
  $2
-- params: ["<uid of the rows above>","0"]

SELECT
  "public"."MockServer"."id",
  "public"."MockServer"."name",
  "public"."MockServer"."subdomain",
  "public"."MockServer"."creatorUid",
  "public"."MockServer"."collectionID",
  "public"."MockServer"."workspaceType"::text,
  "public"."MockServer"."workspaceID",
  "public"."MockServer"."delayInMs",
  "public"."MockServer"."isPublic",
  "public"."MockServer"."isActive",
  "public"."MockServer"."hitCount",
  "public"."MockServer"."lastHitAt",
  "public"."MockServer"."createdOn",
  "public"."MockServer"."updatedOn",
  "public"."MockServer"."deletedAt"
FROM
  "public"."MockServer"
WHERE
  "public"."MockServer"."creatorUid" IN ($1)
OFFSET
  $2
-- params: ["<uid of the rows above>","0"]

SELECT
  "public"."PersonalAccessToken"."id",
  "public"."PersonalAccessToken"."userUid",
  "public"."PersonalAccessToken"."label",
  "public"."PersonalAccessToken"."token",
  "public"."PersonalAccessToken"."expiresOn",
  "public"."PersonalAccessToken"."createdOn",
  "public"."PersonalAccessToken"."updatedOn"
FROM
  "public"."PersonalAccessToken"
WHERE
  "public"."PersonalAccessToken"."userUid" IN ($1)
OFFSET
  $2
-- params: ["<uid of the rows above>","0"]

SELECT
  "public"."Shortcode"."id",
  "public"."Shortcode"."request",
  "public"."Shortcode"."creatorUid",
  "public"."Shortcode"."embedProperties",
  "public"."Shortcode"."createdOn",
  "public"."Shortcode"."updatedOn"
FROM
  "public"."Shortcode"
WHERE
  "public"."Shortcode"."creatorUid" IN ($1)
OFFSET
  $2
-- params: ["<uid of the rows above>","0"]

SELECT
  "public"."UserCollection"."id",
  "public"."UserCollection"."parentID",
  "public"."UserCollection"."userUid",
  "public"."UserCollection"."title",
  "public"."UserCollection"."type"::text,
  "public"."UserCollection"."orderIndex",
  "public"."UserCollection"."data",
  "public"."UserCollection"."createdOn",
  "public"."UserCollection"."updatedOn"
FROM
  "public"."UserCollection"
WHERE
  "public"."UserCollection"."userUid" IN ($1)
OFFSET
  $2
-- params: ["<uid of the rows above>","0"]

SELECT
  "public"."UserEnvironment"."id",
  "public"."UserEnvironment"."userUid",
  "public"."UserEnvironment"."name",
  "public"."UserEnvironment"."variables",
  "public"."UserEnvironment"."isGlobal"
FROM
  "public"."UserEnvironment"
WHERE
  "public"."UserEnvironment"."userUid" IN ($1)
OFFSET
  $2
-- params: ["<uid of the rows above>","0"]

SELECT
  "public"."UserHistory"."id",
  "public"."UserHistory"."userUid",
  "public"."UserHistory"."reqType"::text,
  "public"."UserHistory"."request",
  "public"."UserHistory"."responseMetadata",
  "public"."UserHistory"."isStarred",
  "public"."UserHistory"."executedOn"
FROM
  "public"."UserHistory"
WHERE
  "public"."UserHistory"."userUid" IN ($1)
OFFSET
  $2
-- params: ["<uid of the rows above>","0"]

SELECT
  "public"."UserRequest"."id",
  "public"."UserRequest"."collectionID",
  "public"."UserRequest"."userUid",
  "public"."UserRequest"."title",
  "public"."UserRequest"."request",
  "public"."UserRequest"."type"::text,
  "public"."UserRequest"."mockExamples",
  "public"."UserRequest"."orderIndex",
  "public"."UserRequest"."createdOn",
  "public"."UserRequest"."updatedOn"
FROM
  "public"."UserRequest"
WHERE
  "public"."UserRequest"."userUid" IN ($1)
OFFSET
  $2
-- params: ["<uid of the rows above>","0"]

SELECT
  "public"."UserSettings"."id",
  "public"."UserSettings"."userUid",
  "public"."UserSettings"."properties",
  "public"."UserSettings"."updatedOn"
FROM
  "public"."UserSettings"
WHERE
  "public"."UserSettings"."userUid" IN ($1)
OFFSET
  $2
-- params: ["<uid of the rows above>","0"]

SELECT
  "public"."VerificationToken"."token",
  "public"."VerificationToken"."deviceIdentifier",
  "public"."VerificationToken"."userUid",
  "public"."VerificationToken"."expiresOn"
FROM
  "public"."VerificationToken"
WHERE
  "public"."VerificationToken"."userUid" IN ($1)
OFFSET
  $2
-- params: ["<uid of the rows above>","0"]
```

**findUnique**: One row by what is unique to it

```ts
await prisma.user.findUnique({ where: { uid: "example" } });
```

```sql
SELECT
  "public"."User"."uid",
  "public"."User"."displayName",
  "public"."User"."email",
  "public"."User"."photoURL",
  "public"."User"."isAdmin",
  "public"."User"."refreshToken",
  "public"."User"."currentRESTSession",
  "public"."User"."currentGQLSession",
  "public"."User"."createdOn",
  "public"."User"."lastLoggedOn",
  "public"."User"."lastActiveOn"
FROM
  "public"."User"
WHERE
  (
    "public"."User"."uid" = $1
    AND 1 = 1
  )
LIMIT
  $2
OFFSET
  $3
-- params: ["example","1","0"]
```

**create**: Create a row

```ts
await prisma.user.create({ data: {} });
```

```sql
INSERT INTO
  "public"."User" ("uid", "isAdmin", "createdOn")
VALUES
  ($1, $2, $3)
RETURNING
  "public"."User"."uid",
  "public"."User"."displayName",
  "public"."User"."email",
  "public"."User"."photoURL",
  "public"."User"."isAdmin",
  "public"."User"."refreshToken",
  "public"."User"."currentRESTSession",
  "public"."User"."currentGQLSession",
  "public"."User"."createdOn",
  "public"."User"."lastLoggedOn",
  "public"."User"."lastActiveOn"
-- params: ["<cuid(1)>",false,"<now()>"]
```

**update**: Update a row

```ts
await prisma.user.update({ where: { uid: "example" }, data: { displayName: "example" } });
```

```sql
UPDATE "public"."User"
SET
  "displayName" = $1
WHERE
  (
    "public"."User"."uid" = $2
    AND 1 = 1
  )
RETURNING
  "public"."User"."uid",
  "public"."User"."displayName",
  "public"."User"."email",
  "public"."User"."photoURL",
  "public"."User"."isAdmin",
  "public"."User"."refreshToken",
  "public"."User"."currentRESTSession",
  "public"."User"."currentGQLSession",
  "public"."User"."createdOn",
  "public"."User"."lastLoggedOn",
  "public"."User"."lastActiveOn"
-- params: ["example","example"]
```

**delete**: Delete a row

```ts
await prisma.user.delete({ where: { uid: "example" } });
```

```sql
DELETE FROM "public"."User"
WHERE
  (
    "public"."User"."uid" = $1
    AND 1 = 1
  )
RETURNING
  "public"."User"."uid",
  "public"."User"."displayName",
  "public"."User"."email",
  "public"."User"."photoURL",
  "public"."User"."isAdmin",
  "public"."User"."refreshToken",
  "public"."User"."currentRESTSession",
  "public"."User"."currentGQLSession",
  "public"."User"."createdOn",
  "public"."User"."lastLoggedOn",
  "public"."User"."lastActiveOn"
-- params: ["example"]
```

</details>

<a id="model-Account"></a>

### Account

![Account and the models it relates to](tables/Account.svg)

| Field                  | Type                            | Constraints                        | Default   | Description |
| ---------------------- | ------------------------------- | ---------------------------------- | --------- | ----------- |
| `id`                   | `String`                        | PK NOT NULL                        | `cuid(1)` |             |
| `userId`               | `String`                        | NOT NULL → [User.uid](#model-User) |           |             |
| `provider`             | `String`                        | NOT NULL                           |           |             |
| `providerAccountId`    | `String`                        | NOT NULL                           |           |             |
| `providerRefreshToken` | `String`                        | NULL                               |           |             |
| `providerAccessToken`  | `String`                        | NULL                               |           |             |
| `providerScope`        | `String`                        | NULL                               |           |             |
| `loggedIn`             | `DateTime` `@db.Timestamptz(3)` | NOT NULL                           | `now()`   |             |

#### Indexes

- `@@unique([provider, providerAccountId], name: "verifyProviderAccount")`

#### References

- `userId` → [User.uid](#model-User) (many-to-one, on delete Cascade)

#### Definition

```sql
CREATE TABLE "Account" (
    "id" TEXT NOT NULL,
    "userId" TEXT NOT NULL,
    "provider" TEXT NOT NULL,
    "providerAccountId" TEXT NOT NULL,
    "providerRefreshToken" TEXT,
    "providerAccessToken" TEXT,
    "providerScope" TEXT,
    "loggedIn" TIMESTAMPTZ(3) NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT "Account_pkey" PRIMARY KEY ("id")
);

CREATE UNIQUE INDEX "Account_provider_providerAccountId_key" ON "Account"("provider", "providerAccountId");

ALTER TABLE "Account" ADD CONSTRAINT "Account_userId_fkey" FOREIGN KEY ("userId") REFERENCES "User"("uid") ON DELETE CASCADE ON UPDATE CASCADE;
```

<details>
<summary>Queries and their SQL</summary>

**findMany**: Every row

```ts
await prisma.account.findMany();
```

```sql
SELECT
  "public"."Account"."id",
  "public"."Account"."userId",
  "public"."Account"."provider",
  "public"."Account"."providerAccountId",
  "public"."Account"."providerRefreshToken",
  "public"."Account"."providerAccessToken",
  "public"."Account"."providerScope",
  "public"."Account"."loggedIn"
FROM
  "public"."Account"
WHERE
  1 = 1
OFFSET
  $1
-- params: ["0"]
```

**count**: How many rows there are

```ts
await prisma.account.count();
```

```sql
SELECT
  COUNT(*) AS "_count$_all"
FROM
  (
    SELECT
      "public"."Account"."id"
    FROM
      "public"."Account"
    WHERE
      1 = 1
    OFFSET
      $1
  ) AS "sub"
-- params: ["0"]
```

**findMany**: Every row with its relations

```ts
await prisma.account.findMany({ include: { user: true } });
```

```sql
SELECT
  "public"."Account"."id",
  "public"."Account"."userId",
  "public"."Account"."provider",
  "public"."Account"."providerAccountId",
  "public"."Account"."providerRefreshToken",
  "public"."Account"."providerAccessToken",
  "public"."Account"."providerScope",
  "public"."Account"."loggedIn"
FROM
  "public"."Account"
WHERE
  1 = 1
OFFSET
  $1
-- params: ["0"]

SELECT
  "public"."User"."uid",
  "public"."User"."displayName",
  "public"."User"."email",
  "public"."User"."photoURL",
  "public"."User"."isAdmin",
  "public"."User"."refreshToken",
  "public"."User"."currentRESTSession",
  "public"."User"."currentGQLSession",
  "public"."User"."createdOn",
  "public"."User"."lastLoggedOn",
  "public"."User"."lastActiveOn"
FROM
  "public"."User"
WHERE
  "public"."User"."uid" IN ($1)
OFFSET
  $2
-- params: ["<userId of the rows above>","0"]
```

**findUnique**: One row by what is unique to it

```ts
await prisma.account.findUnique({ where: { id: "example" } });
```

```sql
SELECT
  "public"."Account"."id",
  "public"."Account"."userId",
  "public"."Account"."provider",
  "public"."Account"."providerAccountId",
  "public"."Account"."providerRefreshToken",
  "public"."Account"."providerAccessToken",
  "public"."Account"."providerScope",
  "public"."Account"."loggedIn"
FROM
  "public"."Account"
WHERE
  (
    "public"."Account"."id" = $1
    AND 1 = 1
  )
LIMIT
  $2
OFFSET
  $3
-- params: ["example","1","0"]
```

**create**: Create a row

```ts
await prisma.account.create({
  data: { userId: "example", provider: "example", providerAccountId: "example" },
});
```

```sql
INSERT INTO
  "public"."Account" (
    "id",
    "userId",
    "provider",
    "providerAccountId",
    "loggedIn"
  )
VALUES
  ($1, $2, $3, $4, $5)
RETURNING
  "public"."Account"."id",
  "public"."Account"."userId",
  "public"."Account"."provider",
  "public"."Account"."providerAccountId",
  "public"."Account"."providerRefreshToken",
  "public"."Account"."providerAccessToken",
  "public"."Account"."providerScope",
  "public"."Account"."loggedIn"
-- params: ["<cuid(1)>","example","example","example","<now()>"]
```

**update**: Update a row

```ts
await prisma.account.update({ where: { id: "example" }, data: { provider: "example" } });
```

```sql
UPDATE "public"."Account"
SET
  "provider" = $1
WHERE
  (
    "public"."Account"."id" = $2
    AND 1 = 1
  )
RETURNING
  "public"."Account"."id",
  "public"."Account"."userId",
  "public"."Account"."provider",
  "public"."Account"."providerAccountId",
  "public"."Account"."providerRefreshToken",
  "public"."Account"."providerAccessToken",
  "public"."Account"."providerScope",
  "public"."Account"."loggedIn"
-- params: ["example","example"]
```

**delete**: Delete a row

```ts
await prisma.account.delete({ where: { id: "example" } });
```

```sql
DELETE FROM "public"."Account"
WHERE
  (
    "public"."Account"."id" = $1
    AND 1 = 1
  )
RETURNING
  "public"."Account"."id",
  "public"."Account"."userId",
  "public"."Account"."provider",
  "public"."Account"."providerAccountId",
  "public"."Account"."providerRefreshToken",
  "public"."Account"."providerAccessToken",
  "public"."Account"."providerScope",
  "public"."Account"."loggedIn"
-- params: ["example"]
```

</details>

<a id="model-VerificationToken"></a>

### VerificationToken

![VerificationToken and the models it relates to](tables/VerificationToken.svg)

| Field              | Type                            | Constraints                        | Default   | Description |
| ------------------ | ------------------------------- | ---------------------------------- | --------- | ----------- |
| `deviceIdentifier` | `String`                        | NOT NULL                           |           |             |
| `token`            | `String`                        | UNIQUE NOT NULL                    | `cuid(1)` |             |
| `userUid`          | `String`                        | NOT NULL → [User.uid](#model-User) |           |             |
| `expiresOn`        | `DateTime` `@db.Timestamptz(3)` | NOT NULL                           |           |             |

#### Indexes

- `@@unique([deviceIdentifier, token], name: "passwordless_deviceIdentifier_tokens")`

#### References

- `userUid` → [User.uid](#model-User) (many-to-one, on delete Cascade)

#### Definition

```sql
CREATE TABLE "VerificationToken" (
    "deviceIdentifier" TEXT NOT NULL,
    "token" TEXT NOT NULL,
    "userUid" TEXT NOT NULL,
    "expiresOn" TIMESTAMPTZ(3) NOT NULL
);

CREATE UNIQUE INDEX "VerificationToken_token_key" ON "VerificationToken"("token");

CREATE UNIQUE INDEX "VerificationToken_deviceIdentifier_token_key" ON "VerificationToken"("deviceIdentifier", "token");

ALTER TABLE "VerificationToken" ADD CONSTRAINT "VerificationToken_userUid_fkey" FOREIGN KEY ("userUid") REFERENCES "User"("uid") ON DELETE CASCADE ON UPDATE CASCADE;
```

<details>
<summary>Queries and their SQL</summary>

**findMany**: Every row

```ts
await prisma.verificationToken.findMany();
```

```sql
SELECT
  "public"."VerificationToken"."token",
  "public"."VerificationToken"."deviceIdentifier",
  "public"."VerificationToken"."userUid",
  "public"."VerificationToken"."expiresOn"
FROM
  "public"."VerificationToken"
WHERE
  1 = 1
OFFSET
  $1
-- params: ["0"]
```

**count**: How many rows there are

```ts
await prisma.verificationToken.count();
```

```sql
SELECT
  COUNT(*) AS "_count$_all"
FROM
  (
    SELECT
      "public"."VerificationToken"."token"
    FROM
      "public"."VerificationToken"
    WHERE
      1 = 1
    OFFSET
      $1
  ) AS "sub"
-- params: ["0"]
```

**findMany**: Every row with its relations

```ts
await prisma.verificationToken.findMany({ include: { user: true } });
```

```sql
SELECT
  "public"."VerificationToken"."token",
  "public"."VerificationToken"."deviceIdentifier",
  "public"."VerificationToken"."userUid",
  "public"."VerificationToken"."expiresOn"
FROM
  "public"."VerificationToken"
WHERE
  1 = 1
OFFSET
  $1
-- params: ["0"]

SELECT
  "public"."User"."uid",
  "public"."User"."displayName",
  "public"."User"."email",
  "public"."User"."photoURL",
  "public"."User"."isAdmin",
  "public"."User"."refreshToken",
  "public"."User"."currentRESTSession",
  "public"."User"."currentGQLSession",
  "public"."User"."createdOn",
  "public"."User"."lastLoggedOn",
  "public"."User"."lastActiveOn"
FROM
  "public"."User"
WHERE
  "public"."User"."uid" IN ($1)
OFFSET
  $2
-- params: ["<userUid of the rows above>","0"]
```

**findUnique**: One row by what is unique to it

```ts
await prisma.verificationToken.findUnique({ where: { token: "example" } });
```

```sql
SELECT
  "public"."VerificationToken"."token",
  "public"."VerificationToken"."deviceIdentifier",
  "public"."VerificationToken"."userUid",
  "public"."VerificationToken"."expiresOn"
FROM
  "public"."VerificationToken"
WHERE
  (
    "public"."VerificationToken"."token" = $1
    AND 1 = 1
  )
LIMIT
  $2
OFFSET
  $3
-- params: ["example","1","0"]
```

**create**: Create a row

```ts
await prisma.verificationToken.create({
  data: {
    deviceIdentifier: "example",
    userUid: "example",
    expiresOn: new Date("2026-01-01T00:00:00.000Z"),
  },
});
```

```sql
INSERT INTO
  "public"."VerificationToken" (
    "deviceIdentifier",
    "token",
    "userUid",
    "expiresOn"
  )
VALUES
  ($1, $2, $3, $4)
RETURNING
  "public"."VerificationToken"."token",
  "public"."VerificationToken"."deviceIdentifier",
  "public"."VerificationToken"."userUid",
  "public"."VerificationToken"."expiresOn"
-- params: ["example","<cuid(1)>","example","2026-01-01T00:00:00.000Z"]
```

**update**: Update a row

```ts
await prisma.verificationToken.update({
  where: { token: "example" },
  data: { deviceIdentifier: "example" },
});
```

```sql
UPDATE "public"."VerificationToken"
SET
  "deviceIdentifier" = $1
WHERE
  (
    "public"."VerificationToken"."token" = $2
    AND 1 = 1
  )
RETURNING
  "public"."VerificationToken"."token",
  "public"."VerificationToken"."deviceIdentifier",
  "public"."VerificationToken"."userUid",
  "public"."VerificationToken"."expiresOn"
-- params: ["example","example"]
```

**delete**: Delete a row

```ts
await prisma.verificationToken.delete({ where: { token: "example" } });
```

```sql
DELETE FROM "public"."VerificationToken"
WHERE
  (
    "public"."VerificationToken"."token" = $1
    AND 1 = 1
  )
RETURNING
  "public"."VerificationToken"."token",
  "public"."VerificationToken"."deviceIdentifier",
  "public"."VerificationToken"."userUid",
  "public"."VerificationToken"."expiresOn"
-- params: ["example"]
```

</details>

<a id="model-UserSettings"></a>

### UserSettings

![UserSettings and the models it relates to](tables/UserSettings.svg)

| Field        | Type                            | Constraints                               | Default      | Description |
| ------------ | ------------------------------- | ----------------------------------------- | ------------ | ----------- |
| `id`         | `String`                        | PK NOT NULL                               | `cuid(1)`    |             |
| `userUid`    | `String`                        | UNIQUE NOT NULL → [User.uid](#model-User) |              |             |
| `properties` | `Json`                          | NOT NULL                                  |              |             |
| `updatedOn`  | `DateTime` `@db.Timestamptz(3)` | NOT NULL                                  | `@updatedAt` |             |

#### References

- `userUid` → [User.uid](#model-User) (one-to-one, on delete Cascade)

#### Definition

```sql
CREATE TABLE "UserSettings" (
    "id" TEXT NOT NULL,
    "userUid" TEXT NOT NULL,
    "properties" JSONB NOT NULL,
    "updatedOn" TIMESTAMPTZ(3) NOT NULL,

    CONSTRAINT "UserSettings_pkey" PRIMARY KEY ("id")
);

CREATE UNIQUE INDEX "UserSettings_userUid_key" ON "UserSettings"("userUid");

ALTER TABLE "UserSettings" ADD CONSTRAINT "UserSettings_userUid_fkey" FOREIGN KEY ("userUid") REFERENCES "User"("uid") ON DELETE CASCADE ON UPDATE CASCADE;
```

<details>
<summary>Queries and their SQL</summary>

**findMany**: Every row

```ts
await prisma.userSettings.findMany();
```

```sql
SELECT
  "public"."UserSettings"."id",
  "public"."UserSettings"."userUid",
  "public"."UserSettings"."properties",
  "public"."UserSettings"."updatedOn"
FROM
  "public"."UserSettings"
WHERE
  1 = 1
OFFSET
  $1
-- params: ["0"]
```

**count**: How many rows there are

```ts
await prisma.userSettings.count();
```

```sql
SELECT
  COUNT(*) AS "_count$_all"
FROM
  (
    SELECT
      "public"."UserSettings"."id"
    FROM
      "public"."UserSettings"
    WHERE
      1 = 1
    OFFSET
      $1
  ) AS "sub"
-- params: ["0"]
```

**findMany**: Every row with its relations

```ts
await prisma.userSettings.findMany({ include: { user: true } });
```

```sql
SELECT
  "public"."UserSettings"."id",
  "public"."UserSettings"."userUid",
  "public"."UserSettings"."properties",
  "public"."UserSettings"."updatedOn"
FROM
  "public"."UserSettings"
WHERE
  1 = 1
OFFSET
  $1
-- params: ["0"]

SELECT
  "public"."User"."uid",
  "public"."User"."displayName",
  "public"."User"."email",
  "public"."User"."photoURL",
  "public"."User"."isAdmin",
  "public"."User"."refreshToken",
  "public"."User"."currentRESTSession",
  "public"."User"."currentGQLSession",
  "public"."User"."createdOn",
  "public"."User"."lastLoggedOn",
  "public"."User"."lastActiveOn"
FROM
  "public"."User"
WHERE
  "public"."User"."uid" IN ($1)
OFFSET
  $2
-- params: ["<userUid of the rows above>","0"]
```

**findUnique**: One row by what is unique to it

```ts
await prisma.userSettings.findUnique({ where: { id: "example" } });
```

```sql
SELECT
  "public"."UserSettings"."id",
  "public"."UserSettings"."userUid",
  "public"."UserSettings"."properties",
  "public"."UserSettings"."updatedOn"
FROM
  "public"."UserSettings"
WHERE
  (
    "public"."UserSettings"."id" = $1
    AND 1 = 1
  )
LIMIT
  $2
OFFSET
  $3
-- params: ["example","1","0"]
```

**create**: Create a row

```ts
await prisma.userSettings.create({ data: { userUid: "example", properties: {} } });
```

```sql
INSERT INTO
  "public"."UserSettings" ("id", "userUid", "properties", "updatedOn")
VALUES
  ($1, $2, $3, $4)
RETURNING
  "public"."UserSettings"."id",
  "public"."UserSettings"."userUid",
  "public"."UserSettings"."properties",
  "public"."UserSettings"."updatedOn"
-- params: ["<cuid(1)>","example","{}","<now()>"]
```

**update**: Update a row

```ts
await prisma.userSettings.update({ where: { id: "example" }, data: { properties: {} } });
```

```sql
UPDATE "public"."UserSettings"
SET
  "properties" = $1,
  "updatedOn" = $2
WHERE
  (
    "public"."UserSettings"."id" = $3
    AND 1 = 1
  )
RETURNING
  "public"."UserSettings"."id",
  "public"."UserSettings"."userUid",
  "public"."UserSettings"."properties",
  "public"."UserSettings"."updatedOn"
-- params: ["{}",{"prisma__type":"generatorCall","prisma__value":{"name":"now","args":[],"returnType":{"type":"DateTime"}}},"example"]
```

**delete**: Delete a row

```ts
await prisma.userSettings.delete({ where: { id: "example" } });
```

```sql
DELETE FROM "public"."UserSettings"
WHERE
  (
    "public"."UserSettings"."id" = $1
    AND 1 = 1
  )
RETURNING
  "public"."UserSettings"."id",
  "public"."UserSettings"."userUid",
  "public"."UserSettings"."properties",
  "public"."UserSettings"."updatedOn"
-- params: ["example"]
```

</details>

<a id="model-UserHistory"></a>

### UserHistory

![UserHistory and the models it relates to](tables/UserHistory.svg)

| Field              | Type                            | Constraints                        | Default   | Description |
| ------------------ | ------------------------------- | ---------------------------------- | --------- | ----------- |
| `id`               | `String`                        | PK NOT NULL                        | `cuid(1)` |             |
| `userUid`          | `String`                        | NOT NULL → [User.uid](#model-User) |           |             |
| `reqType`          | [`ReqType`](#enum-ReqType)      | NOT NULL                           |           |             |
| `request`          | `Json`                          | NOT NULL                           |           |             |
| `responseMetadata` | `Json`                          | NOT NULL                           |           |             |
| `isStarred`        | `Boolean`                       | NOT NULL                           |           |             |
| `executedOn`       | `DateTime` `@db.Timestamptz(3)` | NOT NULL                           | `now()`   |             |

#### References

- `userUid` → [User.uid](#model-User) (many-to-one, on delete Cascade)

#### Definition

```sql
CREATE TABLE "UserHistory" (
    "id" TEXT NOT NULL,
    "userUid" TEXT NOT NULL,
    "reqType" "ReqType" NOT NULL,
    "request" JSONB NOT NULL,
    "responseMetadata" JSONB NOT NULL,
    "isStarred" BOOLEAN NOT NULL,
    "executedOn" TIMESTAMPTZ(3) NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT "UserHistory_pkey" PRIMARY KEY ("id")
);

ALTER TABLE "UserHistory" ADD CONSTRAINT "UserHistory_userUid_fkey" FOREIGN KEY ("userUid") REFERENCES "User"("uid") ON DELETE CASCADE ON UPDATE CASCADE;
```

<details>
<summary>Queries and their SQL</summary>

**findMany**: Every row

```ts
await prisma.userHistory.findMany();
```

```sql
SELECT
  "public"."UserHistory"."id",
  "public"."UserHistory"."userUid",
  "public"."UserHistory"."reqType"::text,
  "public"."UserHistory"."request",
  "public"."UserHistory"."responseMetadata",
  "public"."UserHistory"."isStarred",
  "public"."UserHistory"."executedOn"
FROM
  "public"."UserHistory"
WHERE
  1 = 1
OFFSET
  $1
-- params: ["0"]
```

**count**: How many rows there are

```ts
await prisma.userHistory.count();
```

```sql
SELECT
  COUNT(*) AS "_count$_all"
FROM
  (
    SELECT
      "public"."UserHistory"."id"
    FROM
      "public"."UserHistory"
    WHERE
      1 = 1
    OFFSET
      $1
  ) AS "sub"
-- params: ["0"]
```

**findMany**: Every row with its relations

```ts
await prisma.userHistory.findMany({ include: { user: true } });
```

```sql
SELECT
  "public"."UserHistory"."id",
  "public"."UserHistory"."userUid",
  "public"."UserHistory"."reqType"::text,
  "public"."UserHistory"."request",
  "public"."UserHistory"."responseMetadata",
  "public"."UserHistory"."isStarred",
  "public"."UserHistory"."executedOn"
FROM
  "public"."UserHistory"
WHERE
  1 = 1
OFFSET
  $1
-- params: ["0"]

SELECT
  "public"."User"."uid",
  "public"."User"."displayName",
  "public"."User"."email",
  "public"."User"."photoURL",
  "public"."User"."isAdmin",
  "public"."User"."refreshToken",
  "public"."User"."currentRESTSession",
  "public"."User"."currentGQLSession",
  "public"."User"."createdOn",
  "public"."User"."lastLoggedOn",
  "public"."User"."lastActiveOn"
FROM
  "public"."User"
WHERE
  "public"."User"."uid" IN ($1)
OFFSET
  $2
-- params: ["<userUid of the rows above>","0"]
```

**findUnique**: One row by what is unique to it

```ts
await prisma.userHistory.findUnique({ where: { id: "example" } });
```

```sql
SELECT
  "public"."UserHistory"."id",
  "public"."UserHistory"."userUid",
  "public"."UserHistory"."reqType"::text,
  "public"."UserHistory"."request",
  "public"."UserHistory"."responseMetadata",
  "public"."UserHistory"."isStarred",
  "public"."UserHistory"."executedOn"
FROM
  "public"."UserHistory"
WHERE
  (
    "public"."UserHistory"."id" = $1
    AND 1 = 1
  )
LIMIT
  $2
OFFSET
  $3
-- params: ["example","1","0"]
```

**create**: Create a row

```ts
await prisma.userHistory.create({
  data: { userUid: "example", reqType: "REST", request: {}, responseMetadata: {}, isStarred: true },
});
```

```sql
INSERT INTO
  "public"."UserHistory" (
    "id",
    "userUid",
    "reqType",
    "request",
    "responseMetadata",
    "isStarred",
    "executedOn"
  )
VALUES
  (
    $1,
    $2,
    CAST($3::text AS "public"."ReqType"),
    $4,
    $5,
    $6,
    $7
  )
RETURNING
  "public"."UserHistory"."id",
  "public"."UserHistory"."userUid",
  "public"."UserHistory"."reqType"::text,
  "public"."UserHistory"."request",
  "public"."UserHistory"."responseMetadata",
  "public"."UserHistory"."isStarred",
  "public"."UserHistory"."executedOn"
-- params: ["<cuid(1)>","example","REST","{}","{}",true,"<now()>"]
```

**update**: Update a row

```ts
await prisma.userHistory.update({ where: { id: "example" }, data: { reqType: "REST" } });
```

```sql
UPDATE "public"."UserHistory"
SET
  "reqType" = CAST($1::text AS "public"."ReqType")
WHERE
  (
    "public"."UserHistory"."id" = $2
    AND 1 = 1
  )
RETURNING
  "public"."UserHistory"."id",
  "public"."UserHistory"."userUid",
  "public"."UserHistory"."reqType"::text,
  "public"."UserHistory"."request",
  "public"."UserHistory"."responseMetadata",
  "public"."UserHistory"."isStarred",
  "public"."UserHistory"."executedOn"
-- params: ["REST","example"]
```

**delete**: Delete a row

```ts
await prisma.userHistory.delete({ where: { id: "example" } });
```

```sql
DELETE FROM "public"."UserHistory"
WHERE
  (
    "public"."UserHistory"."id" = $1
    AND 1 = 1
  )
RETURNING
  "public"."UserHistory"."id",
  "public"."UserHistory"."userUid",
  "public"."UserHistory"."reqType"::text,
  "public"."UserHistory"."request",
  "public"."UserHistory"."responseMetadata",
  "public"."UserHistory"."isStarred",
  "public"."UserHistory"."executedOn"
-- params: ["example"]
```

</details>

<a id="model-UserEnvironment"></a>

### UserEnvironment

![UserEnvironment and the models it relates to](tables/UserEnvironment.svg)

| Field       | Type      | Constraints                        | Default   | Description |
| ----------- | --------- | ---------------------------------- | --------- | ----------- |
| `id`        | `String`  | PK NOT NULL                        | `cuid(1)` |             |
| `userUid`   | `String`  | NOT NULL → [User.uid](#model-User) |           |             |
| `name`      | `String`  | NULL                               |           |             |
| `variables` | `Json`    | NOT NULL                           |           |             |
| `isGlobal`  | `Boolean` | NOT NULL                           |           |             |

#### References

- `userUid` → [User.uid](#model-User) (many-to-one, on delete Cascade)

#### Definition

```sql
CREATE TABLE "UserEnvironment" (
    "id" TEXT NOT NULL,
    "userUid" TEXT NOT NULL,
    "name" TEXT,
    "variables" JSONB NOT NULL,
    "isGlobal" BOOLEAN NOT NULL,

    CONSTRAINT "UserEnvironment_pkey" PRIMARY KEY ("id")
);

ALTER TABLE "UserEnvironment" ADD CONSTRAINT "UserEnvironment_userUid_fkey" FOREIGN KEY ("userUid") REFERENCES "User"("uid") ON DELETE CASCADE ON UPDATE CASCADE;
```

<details>
<summary>Queries and their SQL</summary>

**findMany**: Every row

```ts
await prisma.userEnvironment.findMany();
```

```sql
SELECT
  "public"."UserEnvironment"."id",
  "public"."UserEnvironment"."userUid",
  "public"."UserEnvironment"."name",
  "public"."UserEnvironment"."variables",
  "public"."UserEnvironment"."isGlobal"
FROM
  "public"."UserEnvironment"
WHERE
  1 = 1
OFFSET
  $1
-- params: ["0"]
```

**count**: How many rows there are

```ts
await prisma.userEnvironment.count();
```

```sql
SELECT
  COUNT(*) AS "_count$_all"
FROM
  (
    SELECT
      "public"."UserEnvironment"."id"
    FROM
      "public"."UserEnvironment"
    WHERE
      1 = 1
    OFFSET
      $1
  ) AS "sub"
-- params: ["0"]
```

**findMany**: Every row with its relations

```ts
await prisma.userEnvironment.findMany({ include: { user: true } });
```

```sql
SELECT
  "public"."UserEnvironment"."id",
  "public"."UserEnvironment"."userUid",
  "public"."UserEnvironment"."name",
  "public"."UserEnvironment"."variables",
  "public"."UserEnvironment"."isGlobal"
FROM
  "public"."UserEnvironment"
WHERE
  1 = 1
OFFSET
  $1
-- params: ["0"]

SELECT
  "public"."User"."uid",
  "public"."User"."displayName",
  "public"."User"."email",
  "public"."User"."photoURL",
  "public"."User"."isAdmin",
  "public"."User"."refreshToken",
  "public"."User"."currentRESTSession",
  "public"."User"."currentGQLSession",
  "public"."User"."createdOn",
  "public"."User"."lastLoggedOn",
  "public"."User"."lastActiveOn"
FROM
  "public"."User"
WHERE
  "public"."User"."uid" IN ($1)
OFFSET
  $2
-- params: ["<userUid of the rows above>","0"]
```

**findUnique**: One row by what is unique to it

```ts
await prisma.userEnvironment.findUnique({ where: { id: "example" } });
```

```sql
SELECT
  "public"."UserEnvironment"."id",
  "public"."UserEnvironment"."userUid",
  "public"."UserEnvironment"."name",
  "public"."UserEnvironment"."variables",
  "public"."UserEnvironment"."isGlobal"
FROM
  "public"."UserEnvironment"
WHERE
  (
    "public"."UserEnvironment"."id" = $1
    AND 1 = 1
  )
LIMIT
  $2
OFFSET
  $3
-- params: ["example","1","0"]
```

**create**: Create a row

```ts
await prisma.userEnvironment.create({
  data: { userUid: "example", variables: {}, isGlobal: true },
});
```

```sql
INSERT INTO
  "public"."UserEnvironment" ("id", "userUid", "variables", "isGlobal")
VALUES
  ($1, $2, $3, $4)
RETURNING
  "public"."UserEnvironment"."id",
  "public"."UserEnvironment"."userUid",
  "public"."UserEnvironment"."name",
  "public"."UserEnvironment"."variables",
  "public"."UserEnvironment"."isGlobal"
-- params: ["<cuid(1)>","example","{}",true]
```

**update**: Update a row

```ts
await prisma.userEnvironment.update({ where: { id: "example" }, data: { name: "example" } });
```

```sql
UPDATE "public"."UserEnvironment"
SET
  "name" = $1
WHERE
  (
    "public"."UserEnvironment"."id" = $2
    AND 1 = 1
  )
RETURNING
  "public"."UserEnvironment"."id",
  "public"."UserEnvironment"."userUid",
  "public"."UserEnvironment"."name",
  "public"."UserEnvironment"."variables",
  "public"."UserEnvironment"."isGlobal"
-- params: ["example","example"]
```

**delete**: Delete a row

```ts
await prisma.userEnvironment.delete({ where: { id: "example" } });
```

```sql
DELETE FROM "public"."UserEnvironment"
WHERE
  (
    "public"."UserEnvironment"."id" = $1
    AND 1 = 1
  )
RETURNING
  "public"."UserEnvironment"."id",
  "public"."UserEnvironment"."userUid",
  "public"."UserEnvironment"."name",
  "public"."UserEnvironment"."variables",
  "public"."UserEnvironment"."isGlobal"
-- params: ["example"]
```

</details>

<a id="model-InvitedUsers"></a>

### InvitedUsers

![InvitedUsers and the models it relates to](tables/InvitedUsers.svg)

| Field          | Type                            | Constraints                        | Default | Description |
| -------------- | ------------------------------- | ---------------------------------- | ------- | ----------- |
| `adminUid`     | `String`                        | NOT NULL → [User.uid](#model-User) |         |             |
| `adminEmail`   | `String`                        | NOT NULL                           |         |             |
| `inviteeEmail` | `String`                        | UNIQUE NOT NULL                    |         |             |
| `invitedOn`    | `DateTime` `@db.Timestamptz(3)` | NOT NULL                           | `now()` |             |

#### References

- `adminUid` → [User.uid](#model-User) (many-to-one, on delete Cascade)

#### Definition

```sql
CREATE TABLE "InvitedUsers" (
    "adminUid" TEXT NOT NULL,
    "adminEmail" TEXT NOT NULL,
    "inviteeEmail" TEXT NOT NULL,
    "invitedOn" TIMESTAMPTZ(3) NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE UNIQUE INDEX "InvitedUsers_inviteeEmail_key" ON "InvitedUsers"("inviteeEmail");

ALTER TABLE "InvitedUsers" ADD CONSTRAINT "InvitedUsers_adminUid_fkey" FOREIGN KEY ("adminUid") REFERENCES "User"("uid") ON DELETE CASCADE ON UPDATE CASCADE;
```

<details>
<summary>Queries and their SQL</summary>

**findMany**: Every row

```ts
await prisma.invitedUsers.findMany();
```

```sql
SELECT
  "public"."InvitedUsers"."inviteeEmail",
  "public"."InvitedUsers"."adminUid",
  "public"."InvitedUsers"."adminEmail",
  "public"."InvitedUsers"."invitedOn"
FROM
  "public"."InvitedUsers"
WHERE
  1 = 1
OFFSET
  $1
-- params: ["0"]
```

**count**: How many rows there are

```ts
await prisma.invitedUsers.count();
```

```sql
SELECT
  COUNT(*) AS "_count$_all"
FROM
  (
    SELECT
      "public"."InvitedUsers"."inviteeEmail"
    FROM
      "public"."InvitedUsers"
    WHERE
      1 = 1
    OFFSET
      $1
  ) AS "sub"
-- params: ["0"]
```

**findMany**: Every row with its relations

```ts
await prisma.invitedUsers.findMany({ include: { user: true } });
```

```sql
SELECT
  "public"."InvitedUsers"."inviteeEmail",
  "public"."InvitedUsers"."adminUid",
  "public"."InvitedUsers"."adminEmail",
  "public"."InvitedUsers"."invitedOn"
FROM
  "public"."InvitedUsers"
WHERE
  1 = 1
OFFSET
  $1
-- params: ["0"]

SELECT
  "public"."User"."uid",
  "public"."User"."displayName",
  "public"."User"."email",
  "public"."User"."photoURL",
  "public"."User"."isAdmin",
  "public"."User"."refreshToken",
  "public"."User"."currentRESTSession",
  "public"."User"."currentGQLSession",
  "public"."User"."createdOn",
  "public"."User"."lastLoggedOn",
  "public"."User"."lastActiveOn"
FROM
  "public"."User"
WHERE
  "public"."User"."uid" IN ($1)
OFFSET
  $2
-- params: ["<adminUid of the rows above>","0"]
```

**findUnique**: One row by what is unique to it

```ts
await prisma.invitedUsers.findUnique({ where: { inviteeEmail: "example" } });
```

```sql
SELECT
  "public"."InvitedUsers"."inviteeEmail",
  "public"."InvitedUsers"."adminUid",
  "public"."InvitedUsers"."adminEmail",
  "public"."InvitedUsers"."invitedOn"
FROM
  "public"."InvitedUsers"
WHERE
  (
    "public"."InvitedUsers"."inviteeEmail" = $1
    AND 1 = 1
  )
LIMIT
  $2
OFFSET
  $3
-- params: ["example","1","0"]
```

**create**: Create a row

```ts
await prisma.invitedUsers.create({
  data: { adminUid: "example", adminEmail: "example", inviteeEmail: "example" },
});
```

```sql
INSERT INTO
  "public"."InvitedUsers" (
    "adminUid",
    "adminEmail",
    "inviteeEmail",
    "invitedOn"
  )
VALUES
  ($1, $2, $3, $4)
RETURNING
  "public"."InvitedUsers"."inviteeEmail",
  "public"."InvitedUsers"."adminUid",
  "public"."InvitedUsers"."adminEmail",
  "public"."InvitedUsers"."invitedOn"
-- params: ["example","example","example","<now()>"]
```

**update**: Update a row

```ts
await prisma.invitedUsers.update({
  where: { inviteeEmail: "example" },
  data: { adminEmail: "example" },
});
```

```sql
UPDATE "public"."InvitedUsers"
SET
  "adminEmail" = $1
WHERE
  (
    "public"."InvitedUsers"."inviteeEmail" = $2
    AND 1 = 1
  )
RETURNING
  "public"."InvitedUsers"."inviteeEmail",
  "public"."InvitedUsers"."adminUid",
  "public"."InvitedUsers"."adminEmail",
  "public"."InvitedUsers"."invitedOn"
-- params: ["example","example"]
```

**delete**: Delete a row

```ts
await prisma.invitedUsers.delete({ where: { inviteeEmail: "example" } });
```

```sql
DELETE FROM "public"."InvitedUsers"
WHERE
  (
    "public"."InvitedUsers"."inviteeEmail" = $1
    AND 1 = 1
  )
RETURNING
  "public"."InvitedUsers"."inviteeEmail",
  "public"."InvitedUsers"."adminUid",
  "public"."InvitedUsers"."adminEmail",
  "public"."InvitedUsers"."invitedOn"
-- params: ["example"]
```

</details>

<a id="model-UserRequest"></a>

### UserRequest

![UserRequest and the models it relates to](tables/UserRequest.svg)

| Field          | Type                            | Constraints                                           | Default      | Description |
| -------------- | ------------------------------- | ----------------------------------------------------- | ------------ | ----------- |
| `id`           | `String`                        | PK NOT NULL                                           | `cuid(1)`    |             |
| `collectionID` | `String`                        | NOT NULL → [UserCollection.id](#model-UserCollection) |              |             |
| `userUid`      | `String`                        | NOT NULL → [User.uid](#model-User)                    |              |             |
| `title`        | `String`                        | NOT NULL                                              |              |             |
| `request`      | `Json`                          | NOT NULL                                              |              |             |
| `type`         | [`ReqType`](#enum-ReqType)      | NOT NULL                                              |              |             |
| `mockExamples` | `Json`                          | NULL                                                  |              |             |
| `orderIndex`   | `Int`                           | NOT NULL                                              |              |             |
| `createdOn`    | `DateTime` `@db.Timestamptz(3)` | NOT NULL                                              | `now()`      |             |
| `updatedOn`    | `DateTime` `@db.Timestamptz(3)` | NOT NULL                                              | `@updatedAt` |             |

#### Indexes

- `@@unique([userUid, collectionID, orderIndex])`

#### References

- `collectionID` → [UserCollection.id](#model-UserCollection) (many-to-one, on delete Cascade)
- `userUid` → [User.uid](#model-User) (many-to-one, on delete Cascade)

#### Definition

```sql
CREATE TABLE "UserRequest" (
    "id" TEXT NOT NULL,
    "collectionID" TEXT NOT NULL,
    "userUid" TEXT NOT NULL,
    "title" TEXT NOT NULL,
    "request" JSONB NOT NULL,
    "type" "ReqType" NOT NULL,
    "mockExamples" JSONB,
    "orderIndex" INTEGER NOT NULL,
    "createdOn" TIMESTAMPTZ(3) NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "updatedOn" TIMESTAMPTZ(3) NOT NULL,

    CONSTRAINT "UserRequest_pkey" PRIMARY KEY ("id")
);

CREATE UNIQUE INDEX "UserRequest_userUid_collectionID_orderIndex_key" ON "UserRequest"("userUid", "collectionID", "orderIndex");

ALTER TABLE "UserRequest" ADD CONSTRAINT "UserRequest_collectionID_fkey" FOREIGN KEY ("collectionID") REFERENCES "UserCollection"("id") ON DELETE CASCADE ON UPDATE CASCADE;

ALTER TABLE "UserRequest" ADD CONSTRAINT "UserRequest_userUid_fkey" FOREIGN KEY ("userUid") REFERENCES "User"("uid") ON DELETE CASCADE ON UPDATE CASCADE;
```

<details>
<summary>Queries and their SQL</summary>

**findMany**: Every row

```ts
await prisma.userRequest.findMany();
```

```sql
SELECT
  "public"."UserRequest"."id",
  "public"."UserRequest"."collectionID",
  "public"."UserRequest"."userUid",
  "public"."UserRequest"."title",
  "public"."UserRequest"."request",
  "public"."UserRequest"."type"::text,
  "public"."UserRequest"."mockExamples",
  "public"."UserRequest"."orderIndex",
  "public"."UserRequest"."createdOn",
  "public"."UserRequest"."updatedOn"
FROM
  "public"."UserRequest"
WHERE
  1 = 1
OFFSET
  $1
-- params: ["0"]
```

**count**: How many rows there are

```ts
await prisma.userRequest.count();
```

```sql
SELECT
  COUNT(*) AS "_count$_all"
FROM
  (
    SELECT
      "public"."UserRequest"."id"
    FROM
      "public"."UserRequest"
    WHERE
      1 = 1
    OFFSET
      $1
  ) AS "sub"
-- params: ["0"]
```

**findMany**: Every row with its relations

```ts
await prisma.userRequest.findMany({ include: { userCollection: true, user: true } });
```

```sql
SELECT
  "public"."UserRequest"."id",
  "public"."UserRequest"."collectionID",
  "public"."UserRequest"."userUid",
  "public"."UserRequest"."title",
  "public"."UserRequest"."request",
  "public"."UserRequest"."type"::text,
  "public"."UserRequest"."mockExamples",
  "public"."UserRequest"."orderIndex",
  "public"."UserRequest"."createdOn",
  "public"."UserRequest"."updatedOn"
FROM
  "public"."UserRequest"
WHERE
  1 = 1
OFFSET
  $1
-- params: ["0"]

SELECT
  "public"."UserCollection"."id",
  "public"."UserCollection"."parentID",
  "public"."UserCollection"."userUid",
  "public"."UserCollection"."title",
  "public"."UserCollection"."type"::text,
  "public"."UserCollection"."orderIndex",
  "public"."UserCollection"."data",
  "public"."UserCollection"."createdOn",
  "public"."UserCollection"."updatedOn"
FROM
  "public"."UserCollection"
WHERE
  "public"."UserCollection"."id" IN ($1)
OFFSET
  $2
-- params: ["<collectionID of the rows above>","0"]

SELECT
  "public"."User"."uid",
  "public"."User"."displayName",
  "public"."User"."email",
  "public"."User"."photoURL",
  "public"."User"."isAdmin",
  "public"."User"."refreshToken",
  "public"."User"."currentRESTSession",
  "public"."User"."currentGQLSession",
  "public"."User"."createdOn",
  "public"."User"."lastLoggedOn",
  "public"."User"."lastActiveOn"
FROM
  "public"."User"
WHERE
  "public"."User"."uid" IN ($1)
OFFSET
  $2
-- params: ["<userUid of the rows above>","0"]
```

**findUnique**: One row by what is unique to it

```ts
await prisma.userRequest.findUnique({ where: { id: "example" } });
```

```sql
SELECT
  "public"."UserRequest"."id",
  "public"."UserRequest"."collectionID",
  "public"."UserRequest"."userUid",
  "public"."UserRequest"."title",
  "public"."UserRequest"."request",
  "public"."UserRequest"."type"::text,
  "public"."UserRequest"."mockExamples",
  "public"."UserRequest"."orderIndex",
  "public"."UserRequest"."createdOn",
  "public"."UserRequest"."updatedOn"
FROM
  "public"."UserRequest"
WHERE
  (
    "public"."UserRequest"."id" = $1
    AND 1 = 1
  )
LIMIT
  $2
OFFSET
  $3
-- params: ["example","1","0"]
```

**create**: Create a row

```ts
await prisma.userRequest.create({
  data: {
    collectionID: "example",
    userUid: "example",
    title: "example",
    request: {},
    type: "REST",
    orderIndex: 1,
  },
});
```

```sql
INSERT INTO
  "public"."UserRequest" (
    "id",
    "collectionID",
    "userUid",
    "title",
    "request",
    "type",
    "orderIndex",
    "createdOn",
    "updatedOn"
  )
VALUES
  (
    $1,
    $2,
    $3,
    $4,
    $5,
    CAST($6::text AS "public"."ReqType"),
    $7,
    $8,
    $9
  )
RETURNING
  "public"."UserRequest"."id",
  "public"."UserRequest"."collectionID",
  "public"."UserRequest"."userUid",
  "public"."UserRequest"."title",
  "public"."UserRequest"."request",
  "public"."UserRequest"."type"::text,
  "public"."UserRequest"."mockExamples",
  "public"."UserRequest"."orderIndex",
  "public"."UserRequest"."createdOn",
  "public"."UserRequest"."updatedOn"
-- params: ["<cuid(1)>","example","example","example","{}","REST","1","<now()>","<now()>"]
```

**update**: Update a row

```ts
await prisma.userRequest.update({ where: { id: "example" }, data: { title: "example" } });
```

```sql
UPDATE "public"."UserRequest"
SET
  "title" = $1,
  "updatedOn" = $2
WHERE
  (
    "public"."UserRequest"."id" = $3
    AND 1 = 1
  )
RETURNING
  "public"."UserRequest"."id",
  "public"."UserRequest"."collectionID",
  "public"."UserRequest"."userUid",
  "public"."UserRequest"."title",
  "public"."UserRequest"."request",
  "public"."UserRequest"."type"::text,
  "public"."UserRequest"."mockExamples",
  "public"."UserRequest"."orderIndex",
  "public"."UserRequest"."createdOn",
  "public"."UserRequest"."updatedOn"
-- params: ["example",{"prisma__type":"generatorCall","prisma__value":{"name":"now","args":[],"returnType":{"type":"DateTime"}}},"example"]
```

**delete**: Delete a row

```ts
await prisma.userRequest.delete({ where: { id: "example" } });
```

```sql
DELETE FROM "public"."UserRequest"
WHERE
  (
    "public"."UserRequest"."id" = $1
    AND 1 = 1
  )
RETURNING
  "public"."UserRequest"."id",
  "public"."UserRequest"."collectionID",
  "public"."UserRequest"."userUid",
  "public"."UserRequest"."title",
  "public"."UserRequest"."request",
  "public"."UserRequest"."type"::text,
  "public"."UserRequest"."mockExamples",
  "public"."UserRequest"."orderIndex",
  "public"."UserRequest"."createdOn",
  "public"."UserRequest"."updatedOn"
-- params: ["example"]
```

</details>

<a id="model-UserCollection"></a>

### UserCollection

![UserCollection and the models it relates to](tables/UserCollection.svg)

| Field        | Type                            | Constraints                                       | Default      | Description |
| ------------ | ------------------------------- | ------------------------------------------------- | ------------ | ----------- |
| `id`         | `String`                        | PK NOT NULL                                       | `cuid(1)`    |             |
| `parentID`   | `String`                        | NULL → [UserCollection.id](#model-UserCollection) |              |             |
| `userUid`    | `String`                        | NOT NULL → [User.uid](#model-User)                |              |             |
| `title`      | `String`                        | NOT NULL                                          |              |             |
| `type`       | [`ReqType`](#enum-ReqType)      | NOT NULL                                          |              |             |
| `orderIndex` | `Int`                           | NOT NULL                                          |              |             |
| `data`       | `Json`                          | NULL                                              |              |             |
| `createdOn`  | `DateTime` `@db.Timestamptz(3)` | NOT NULL                                          | `now()`      |             |
| `updatedOn`  | `DateTime` `@db.Timestamptz(3)` | NOT NULL                                          | `@updatedAt` |             |

#### Indexes

- `@@unique([userUid, parentID, orderIndex])`

#### References

- `parentID` → [UserCollection.id](#model-UserCollection) (many-to-one, on delete Cascade)
- `userUid` → [User.uid](#model-User) (many-to-one, on delete Cascade)

#### Referenced by

- `id` ← [UserRequest.collectionID](#model-UserRequest) (one-to-many, on delete Cascade)
- `id` ← [UserCollection.parentID](#model-UserCollection) (one-to-many, on delete Cascade)

#### Definition

```sql
CREATE TABLE "UserCollection" (
    "id" TEXT NOT NULL,
    "parentID" TEXT,
    "userUid" TEXT NOT NULL,
    "title" TEXT NOT NULL,
    "type" "ReqType" NOT NULL,
    "orderIndex" INTEGER NOT NULL,
    "data" JSONB,
    "createdOn" TIMESTAMPTZ(3) NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "updatedOn" TIMESTAMPTZ(3) NOT NULL,

    CONSTRAINT "UserCollection_pkey" PRIMARY KEY ("id")
);

CREATE UNIQUE INDEX "UserCollection_userUid_parentID_orderIndex_key" ON "UserCollection"("userUid", "parentID", "orderIndex");

ALTER TABLE "UserCollection" ADD CONSTRAINT "UserCollection_parentID_fkey" FOREIGN KEY ("parentID") REFERENCES "UserCollection"("id") ON DELETE CASCADE ON UPDATE CASCADE;

ALTER TABLE "UserCollection" ADD CONSTRAINT "UserCollection_userUid_fkey" FOREIGN KEY ("userUid") REFERENCES "User"("uid") ON DELETE CASCADE ON UPDATE CASCADE;
```

<details>
<summary>Queries and their SQL</summary>

**findMany**: Every row

```ts
await prisma.userCollection.findMany();
```

```sql
SELECT
  "public"."UserCollection"."id",
  "public"."UserCollection"."parentID",
  "public"."UserCollection"."userUid",
  "public"."UserCollection"."title",
  "public"."UserCollection"."type"::text,
  "public"."UserCollection"."orderIndex",
  "public"."UserCollection"."data",
  "public"."UserCollection"."createdOn",
  "public"."UserCollection"."updatedOn"
FROM
  "public"."UserCollection"
WHERE
  1 = 1
OFFSET
  $1
-- params: ["0"]
```

**count**: How many rows there are

```ts
await prisma.userCollection.count();
```

```sql
SELECT
  COUNT(*) AS "_count$_all"
FROM
  (
    SELECT
      "public"."UserCollection"."id"
    FROM
      "public"."UserCollection"
    WHERE
      1 = 1
    OFFSET
      $1
  ) AS "sub"
-- params: ["0"]
```

**findMany**: Every row with its relations

```ts
await prisma.userCollection.findMany({
  include: { parent: true, children: true, user: true, requests: true },
});
```

```sql
SELECT
  "public"."UserCollection"."id",
  "public"."UserCollection"."parentID",
  "public"."UserCollection"."userUid",
  "public"."UserCollection"."title",
  "public"."UserCollection"."type"::text,
  "public"."UserCollection"."orderIndex",
  "public"."UserCollection"."data",
  "public"."UserCollection"."createdOn",
  "public"."UserCollection"."updatedOn"
FROM
  "public"."UserCollection"
WHERE
  1 = 1
OFFSET
  $1
-- params: ["0"]

SELECT
  "public"."UserCollection"."id",
  "public"."UserCollection"."parentID",
  "public"."UserCollection"."userUid",
  "public"."UserCollection"."title",
  "public"."UserCollection"."type"::text,
  "public"."UserCollection"."orderIndex",
  "public"."UserCollection"."data",
  "public"."UserCollection"."createdOn",
  "public"."UserCollection"."updatedOn"
FROM
  "public"."UserCollection"
WHERE
  "public"."UserCollection"."id" IN ($1)
OFFSET
  $2
-- params: ["<parentID of the rows above>","0"]

SELECT
  "public"."UserCollection"."id",
  "public"."UserCollection"."parentID",
  "public"."UserCollection"."userUid",
  "public"."UserCollection"."title",
  "public"."UserCollection"."type"::text,
  "public"."UserCollection"."orderIndex",
  "public"."UserCollection"."data",
  "public"."UserCollection"."createdOn",
  "public"."UserCollection"."updatedOn"
FROM
  "public"."UserCollection"
WHERE
  "public"."UserCollection"."parentID" IN ($1)
OFFSET
  $2
-- params: ["<id of the rows above>","0"]

SELECT
  "public"."User"."uid",
  "public"."User"."displayName",
  "public"."User"."email",
  "public"."User"."photoURL",
  "public"."User"."isAdmin",
  "public"."User"."refreshToken",
  "public"."User"."currentRESTSession",
  "public"."User"."currentGQLSession",
  "public"."User"."createdOn",
  "public"."User"."lastLoggedOn",
  "public"."User"."lastActiveOn"
FROM
  "public"."User"
WHERE
  "public"."User"."uid" IN ($1)
OFFSET
  $2
-- params: ["<userUid of the rows above>","0"]

SELECT
  "public"."UserRequest"."id",
  "public"."UserRequest"."collectionID",
  "public"."UserRequest"."userUid",
  "public"."UserRequest"."title",
  "public"."UserRequest"."request",
  "public"."UserRequest"."type"::text,
  "public"."UserRequest"."mockExamples",
  "public"."UserRequest"."orderIndex",
  "public"."UserRequest"."createdOn",
  "public"."UserRequest"."updatedOn"
FROM
  "public"."UserRequest"
WHERE
  "public"."UserRequest"."collectionID" IN ($1)
OFFSET
  $2
-- params: ["<id of the rows above>","0"]
```

**findUnique**: One row by what is unique to it

```ts
await prisma.userCollection.findUnique({ where: { id: "example" } });
```

```sql
SELECT
  "public"."UserCollection"."id",
  "public"."UserCollection"."parentID",
  "public"."UserCollection"."userUid",
  "public"."UserCollection"."title",
  "public"."UserCollection"."type"::text,
  "public"."UserCollection"."orderIndex",
  "public"."UserCollection"."data",
  "public"."UserCollection"."createdOn",
  "public"."UserCollection"."updatedOn"
FROM
  "public"."UserCollection"
WHERE
  (
    "public"."UserCollection"."id" = $1
    AND 1 = 1
  )
LIMIT
  $2
OFFSET
  $3
-- params: ["example","1","0"]
```

**create**: Create a row

```ts
await prisma.userCollection.create({
  data: { userUid: "example", title: "example", type: "REST", orderIndex: 1 },
});
```

```sql
INSERT INTO
  "public"."UserCollection" (
    "id",
    "userUid",
    "title",
    "type",
    "orderIndex",
    "createdOn",
    "updatedOn"
  )
VALUES
  (
    $1,
    $2,
    $3,
    CAST($4::text AS "public"."ReqType"),
    $5,
    $6,
    $7
  )
RETURNING
  "public"."UserCollection"."id",
  "public"."UserCollection"."parentID",
  "public"."UserCollection"."userUid",
  "public"."UserCollection"."title",
  "public"."UserCollection"."type"::text,
  "public"."UserCollection"."orderIndex",
  "public"."UserCollection"."data",
  "public"."UserCollection"."createdOn",
  "public"."UserCollection"."updatedOn"
-- params: ["<cuid(1)>","example","example","REST","1","<now()>","<now()>"]
```

**update**: Update a row

```ts
await prisma.userCollection.update({ where: { id: "example" }, data: { title: "example" } });
```

```sql
UPDATE "public"."UserCollection"
SET
  "title" = $1,
  "updatedOn" = $2
WHERE
  (
    "public"."UserCollection"."id" = $3
    AND 1 = 1
  )
RETURNING
  "public"."UserCollection"."id",
  "public"."UserCollection"."parentID",
  "public"."UserCollection"."userUid",
  "public"."UserCollection"."title",
  "public"."UserCollection"."type"::text,
  "public"."UserCollection"."orderIndex",
  "public"."UserCollection"."data",
  "public"."UserCollection"."createdOn",
  "public"."UserCollection"."updatedOn"
-- params: ["example",{"prisma__type":"generatorCall","prisma__value":{"name":"now","args":[],"returnType":{"type":"DateTime"}}},"example"]
```

**delete**: Delete a row

```ts
await prisma.userCollection.delete({ where: { id: "example" } });
```

```sql
DELETE FROM "public"."UserCollection"
WHERE
  (
    "public"."UserCollection"."id" = $1
    AND 1 = 1
  )
RETURNING
  "public"."UserCollection"."id",
  "public"."UserCollection"."parentID",
  "public"."UserCollection"."userUid",
  "public"."UserCollection"."title",
  "public"."UserCollection"."type"::text,
  "public"."UserCollection"."orderIndex",
  "public"."UserCollection"."data",
  "public"."UserCollection"."createdOn",
  "public"."UserCollection"."updatedOn"
-- params: ["example"]
```

</details>

<a id="model-MockServer"></a>

### MockServer

![MockServer and the models it relates to](tables/MockServer.svg)

| Field           | Type                                   | Constraints                    | Default      | Description |
| --------------- | -------------------------------------- | ------------------------------ | ------------ | ----------- |
| `id`            | `String`                               | PK NOT NULL                    | `cuid(1)`    |             |
| `name`          | `String`                               | NOT NULL                       |              |             |
| `subdomain`     | `String`                               | UNIQUE NOT NULL                |              |             |
| `creatorUid`    | `String`                               | NULL → [User.uid](#model-User) |              |             |
| `collectionID`  | `String`                               | NOT NULL                       |              |             |
| `workspaceType` | [`WorkspaceType`](#enum-WorkspaceType) | NOT NULL                       |              |             |
| `workspaceID`   | `String`                               | NOT NULL                       |              |             |
| `delayInMs`     | `Int`                                  | NOT NULL                       | `0`          |             |
| `isPublic`      | `Boolean`                              | NOT NULL                       | `true`       |             |
| `isActive`      | `Boolean`                              | NOT NULL                       | `true`       |             |
| `hitCount`      | `Int`                                  | NOT NULL                       | `0`          |             |
| `lastHitAt`     | `DateTime` `@db.Timestamptz(3)`        | NULL                           |              |             |
| `createdOn`     | `DateTime` `@db.Timestamptz(3)`        | NOT NULL                       | `now()`      |             |
| `updatedOn`     | `DateTime` `@db.Timestamptz(3)`        | NOT NULL                       | `@updatedAt` |             |
| `deletedAt`     | `DateTime` `@db.Timestamptz(3)`        | NULL                           |              |             |

#### References

- `creatorUid` → [User.uid](#model-User) (many-to-one, on delete SetNull)

#### Referenced by

- `id` ← [MockServerLog.mockServerID](#model-MockServerLog) (one-to-many, on delete Cascade)
- `id` ← [MockServerActivity.mockServerID](#model-MockServerActivity) (one-to-many, on delete Cascade)

#### Definition

```sql
CREATE TABLE "MockServer" (
    "id" TEXT NOT NULL,
    "name" TEXT NOT NULL,
    "subdomain" TEXT NOT NULL,
    "creatorUid" TEXT,
    "collectionID" TEXT NOT NULL,
    "workspaceType" "WorkspaceType" NOT NULL,
    "workspaceID" TEXT NOT NULL,
    "delayInMs" INTEGER NOT NULL DEFAULT 0,
    "isPublic" BOOLEAN NOT NULL DEFAULT true,
    "isActive" BOOLEAN NOT NULL DEFAULT true,
    "hitCount" INTEGER NOT NULL DEFAULT 0,
    "lastHitAt" TIMESTAMPTZ(3),
    "createdOn" TIMESTAMPTZ(3) NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "updatedOn" TIMESTAMPTZ(3) NOT NULL,
    "deletedAt" TIMESTAMPTZ(3),

    CONSTRAINT "MockServer_pkey" PRIMARY KEY ("id")
);

CREATE UNIQUE INDEX "MockServer_subdomain_key" ON "MockServer"("subdomain");

ALTER TABLE "MockServer" ADD CONSTRAINT "MockServer_creatorUid_fkey" FOREIGN KEY ("creatorUid") REFERENCES "User"("uid") ON DELETE SET NULL ON UPDATE CASCADE;
```

<details>
<summary>Queries and their SQL</summary>

**findMany**: Every row

```ts
await prisma.mockServer.findMany();
```

```sql
SELECT
  "public"."MockServer"."id",
  "public"."MockServer"."name",
  "public"."MockServer"."subdomain",
  "public"."MockServer"."creatorUid",
  "public"."MockServer"."collectionID",
  "public"."MockServer"."workspaceType"::text,
  "public"."MockServer"."workspaceID",
  "public"."MockServer"."delayInMs",
  "public"."MockServer"."isPublic",
  "public"."MockServer"."isActive",
  "public"."MockServer"."hitCount",
  "public"."MockServer"."lastHitAt",
  "public"."MockServer"."createdOn",
  "public"."MockServer"."updatedOn",
  "public"."MockServer"."deletedAt"
FROM
  "public"."MockServer"
WHERE
  1 = 1
OFFSET
  $1
-- params: ["0"]
```

**count**: How many rows there are

```ts
await prisma.mockServer.count();
```

```sql
SELECT
  COUNT(*) AS "_count$_all"
FROM
  (
    SELECT
      "public"."MockServer"."id"
    FROM
      "public"."MockServer"
    WHERE
      1 = 1
    OFFSET
      $1
  ) AS "sub"
-- params: ["0"]
```

**findMany**: Every row with its relations

```ts
await prisma.mockServer.findMany({
  include: { user: true, requestLogs: true, activityHistory: true },
});
```

```sql
SELECT
  "public"."MockServer"."id",
  "public"."MockServer"."name",
  "public"."MockServer"."subdomain",
  "public"."MockServer"."creatorUid",
  "public"."MockServer"."collectionID",
  "public"."MockServer"."workspaceType"::text,
  "public"."MockServer"."workspaceID",
  "public"."MockServer"."delayInMs",
  "public"."MockServer"."isPublic",
  "public"."MockServer"."isActive",
  "public"."MockServer"."hitCount",
  "public"."MockServer"."lastHitAt",
  "public"."MockServer"."createdOn",
  "public"."MockServer"."updatedOn",
  "public"."MockServer"."deletedAt"
FROM
  "public"."MockServer"
WHERE
  1 = 1
OFFSET
  $1
-- params: ["0"]

SELECT
  "public"."User"."uid",
  "public"."User"."displayName",
  "public"."User"."email",
  "public"."User"."photoURL",
  "public"."User"."isAdmin",
  "public"."User"."refreshToken",
  "public"."User"."currentRESTSession",
  "public"."User"."currentGQLSession",
  "public"."User"."createdOn",
  "public"."User"."lastLoggedOn",
  "public"."User"."lastActiveOn"
FROM
  "public"."User"
WHERE
  "public"."User"."uid" IN ($1)
OFFSET
  $2
-- params: ["<creatorUid of the rows above>","0"]

SELECT
  "public"."MockServerLog"."id",
  "public"."MockServerLog"."mockServerID",
  "public"."MockServerLog"."requestMethod",
  "public"."MockServerLog"."requestPath",
  "public"."MockServerLog"."requestHeaders",
  "public"."MockServerLog"."requestBody",
  "public"."MockServerLog"."requestQuery",
  "public"."MockServerLog"."responseStatus",
  "public"."MockServerLog"."responseHeaders",
  "public"."MockServerLog"."responseBody",
  "public"."MockServerLog"."responseTime",
  "public"."MockServerLog"."ipAddress",
  "public"."MockServerLog"."userAgent",
  "public"."MockServerLog"."executedAt"
FROM
  "public"."MockServerLog"
WHERE
  "public"."MockServerLog"."mockServerID" IN ($1)
OFFSET
  $2
-- params: ["<id of the rows above>","0"]

SELECT
  "public"."MockServerActivity"."id",
  "public"."MockServerActivity"."mockServerID",
  "public"."MockServerActivity"."action"::text,
  "public"."MockServerActivity"."performedBy",
  "public"."MockServerActivity"."performedAt"
FROM
  "public"."MockServerActivity"
WHERE
  "public"."MockServerActivity"."mockServerID" IN ($1)
OFFSET
  $2
-- params: ["<id of the rows above>","0"]
```

**findUnique**: One row by what is unique to it

```ts
await prisma.mockServer.findUnique({ where: { id: "example" } });
```

```sql
SELECT
  "public"."MockServer"."id",
  "public"."MockServer"."name",
  "public"."MockServer"."subdomain",
  "public"."MockServer"."creatorUid",
  "public"."MockServer"."collectionID",
  "public"."MockServer"."workspaceType"::text,
  "public"."MockServer"."workspaceID",
  "public"."MockServer"."delayInMs",
  "public"."MockServer"."isPublic",
  "public"."MockServer"."isActive",
  "public"."MockServer"."hitCount",
  "public"."MockServer"."lastHitAt",
  "public"."MockServer"."createdOn",
  "public"."MockServer"."updatedOn",
  "public"."MockServer"."deletedAt"
FROM
  "public"."MockServer"
WHERE
  (
    "public"."MockServer"."id" = $1
    AND 1 = 1
  )
LIMIT
  $2
OFFSET
  $3
-- params: ["example","1","0"]
```

**create**: Create a row

```ts
await prisma.mockServer.create({
  data: {
    name: "example",
    subdomain: "example",
    collectionID: "example",
    workspaceType: "USER",
    workspaceID: "example",
  },
});
```

```sql
INSERT INTO
  "public"."MockServer" (
    "id",
    "name",
    "subdomain",
    "collectionID",
    "workspaceType",
    "workspaceID",
    "delayInMs",
    "isPublic",
    "isActive",
    "hitCount",
    "createdOn",
    "updatedOn"
  )
VALUES
  (
    $1,
    $2,
    $3,
    $4,
    CAST($5::text AS "public"."WorkspaceType"),
    $6,
    $7,
    $8,
    $9,
    $10,
    $11,
    $12
  )
RETURNING
  "public"."MockServer"."id",
  "public"."MockServer"."name",
  "public"."MockServer"."subdomain",
  "public"."MockServer"."creatorUid",
  "public"."MockServer"."collectionID",
  "public"."MockServer"."workspaceType"::text,
  "public"."MockServer"."workspaceID",
  "public"."MockServer"."delayInMs",
  "public"."MockServer"."isPublic",
  "public"."MockServer"."isActive",
  "public"."MockServer"."hitCount",
  "public"."MockServer"."lastHitAt",
  "public"."MockServer"."createdOn",
  "public"."MockServer"."updatedOn",
  "public"."MockServer"."deletedAt"
-- params: ["<cuid(1)>","example","example","example","USER","example","0",true,true,"0","<now()>","<now()>"]
```

**update**: Update a row

```ts
await prisma.mockServer.update({ where: { id: "example" }, data: { name: "example" } });
```

```sql
UPDATE "public"."MockServer"
SET
  "name" = $1,
  "updatedOn" = $2
WHERE
  (
    "public"."MockServer"."id" = $3
    AND 1 = 1
  )
RETURNING
  "public"."MockServer"."id",
  "public"."MockServer"."name",
  "public"."MockServer"."subdomain",
  "public"."MockServer"."creatorUid",
  "public"."MockServer"."collectionID",
  "public"."MockServer"."workspaceType"::text,
  "public"."MockServer"."workspaceID",
  "public"."MockServer"."delayInMs",
  "public"."MockServer"."isPublic",
  "public"."MockServer"."isActive",
  "public"."MockServer"."hitCount",
  "public"."MockServer"."lastHitAt",
  "public"."MockServer"."createdOn",
  "public"."MockServer"."updatedOn",
  "public"."MockServer"."deletedAt"
-- params: ["example",{"prisma__type":"generatorCall","prisma__value":{"name":"now","args":[],"returnType":{"type":"DateTime"}}},"example"]
```

**delete**: Delete a row

```ts
await prisma.mockServer.delete({ where: { id: "example" } });
```

```sql
DELETE FROM "public"."MockServer"
WHERE
  (
    "public"."MockServer"."id" = $1
    AND 1 = 1
  )
RETURNING
  "public"."MockServer"."id",
  "public"."MockServer"."name",
  "public"."MockServer"."subdomain",
  "public"."MockServer"."creatorUid",
  "public"."MockServer"."collectionID",
  "public"."MockServer"."workspaceType"::text,
  "public"."MockServer"."workspaceID",
  "public"."MockServer"."delayInMs",
  "public"."MockServer"."isPublic",
  "public"."MockServer"."isActive",
  "public"."MockServer"."hitCount",
  "public"."MockServer"."lastHitAt",
  "public"."MockServer"."createdOn",
  "public"."MockServer"."updatedOn",
  "public"."MockServer"."deletedAt"
-- params: ["example"]
```

</details>

<a id="model-MockServerLog"></a>

### MockServerLog

![MockServerLog and the models it relates to](tables/MockServerLog.svg)

| Field             | Type                            | Constraints                                   | Default   | Description |
| ----------------- | ------------------------------- | --------------------------------------------- | --------- | ----------- |
| `id`              | `String`                        | PK NOT NULL                                   | `cuid(1)` |             |
| `mockServerID`    | `String`                        | NOT NULL → [MockServer.id](#model-MockServer) |           |             |
| `requestMethod`   | `String`                        | NOT NULL                                      |           |             |
| `requestPath`     | `String`                        | NOT NULL                                      |           |             |
| `requestHeaders`  | `Json`                          | NOT NULL                                      |           |             |
| `requestBody`     | `Json`                          | NULL                                          |           |             |
| `requestQuery`    | `Json`                          | NULL                                          |           |             |
| `responseStatus`  | `Int`                           | NOT NULL                                      |           |             |
| `responseHeaders` | `Json`                          | NOT NULL                                      |           |             |
| `responseBody`    | `Json`                          | NULL                                          |           |             |
| `responseTime`    | `Int`                           | NOT NULL                                      |           |             |
| `ipAddress`       | `String`                        | NULL                                          |           |             |
| `userAgent`       | `String`                        | NULL                                          |           |             |
| `executedAt`      | `DateTime` `@db.Timestamptz(3)` | NOT NULL                                      | `now()`   |             |

#### Indexes

- `@@index([mockServerID])`
- `@@index([mockServerID, executedAt])`

#### References

- `mockServerID` → [MockServer.id](#model-MockServer) (many-to-one, on delete Cascade)

#### Definition

```sql
CREATE TABLE "MockServerLog" (
    "id" TEXT NOT NULL,
    "mockServerID" TEXT NOT NULL,
    "requestMethod" TEXT NOT NULL,
    "requestPath" TEXT NOT NULL,
    "requestHeaders" JSONB NOT NULL,
    "requestBody" JSONB,
    "requestQuery" JSONB,
    "responseStatus" INTEGER NOT NULL,
    "responseHeaders" JSONB NOT NULL,
    "responseBody" JSONB,
    "responseTime" INTEGER NOT NULL,
    "ipAddress" TEXT,
    "userAgent" TEXT,
    "executedAt" TIMESTAMPTZ(3) NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT "MockServerLog_pkey" PRIMARY KEY ("id")
);

CREATE INDEX "MockServerLog_mockServerID_idx" ON "MockServerLog"("mockServerID");

CREATE INDEX "MockServerLog_mockServerID_executedAt_idx" ON "MockServerLog"("mockServerID", "executedAt");

ALTER TABLE "MockServerLog" ADD CONSTRAINT "MockServerLog_mockServerID_fkey" FOREIGN KEY ("mockServerID") REFERENCES "MockServer"("id") ON DELETE CASCADE ON UPDATE CASCADE;
```

<details>
<summary>Queries and their SQL</summary>

**findMany**: Every row

```ts
await prisma.mockServerLog.findMany();
```

```sql
SELECT
  "public"."MockServerLog"."id",
  "public"."MockServerLog"."mockServerID",
  "public"."MockServerLog"."requestMethod",
  "public"."MockServerLog"."requestPath",
  "public"."MockServerLog"."requestHeaders",
  "public"."MockServerLog"."requestBody",
  "public"."MockServerLog"."requestQuery",
  "public"."MockServerLog"."responseStatus",
  "public"."MockServerLog"."responseHeaders",
  "public"."MockServerLog"."responseBody",
  "public"."MockServerLog"."responseTime",
  "public"."MockServerLog"."ipAddress",
  "public"."MockServerLog"."userAgent",
  "public"."MockServerLog"."executedAt"
FROM
  "public"."MockServerLog"
WHERE
  1 = 1
OFFSET
  $1
-- params: ["0"]
```

**count**: How many rows there are

```ts
await prisma.mockServerLog.count();
```

```sql
SELECT
  COUNT(*) AS "_count$_all"
FROM
  (
    SELECT
      "public"."MockServerLog"."id"
    FROM
      "public"."MockServerLog"
    WHERE
      1 = 1
    OFFSET
      $1
  ) AS "sub"
-- params: ["0"]
```

**findMany**: Every row with its relations

```ts
await prisma.mockServerLog.findMany({ include: { mockServer: true } });
```

```sql
SELECT
  "public"."MockServerLog"."id",
  "public"."MockServerLog"."mockServerID",
  "public"."MockServerLog"."requestMethod",
  "public"."MockServerLog"."requestPath",
  "public"."MockServerLog"."requestHeaders",
  "public"."MockServerLog"."requestBody",
  "public"."MockServerLog"."requestQuery",
  "public"."MockServerLog"."responseStatus",
  "public"."MockServerLog"."responseHeaders",
  "public"."MockServerLog"."responseBody",
  "public"."MockServerLog"."responseTime",
  "public"."MockServerLog"."ipAddress",
  "public"."MockServerLog"."userAgent",
  "public"."MockServerLog"."executedAt"
FROM
  "public"."MockServerLog"
WHERE
  1 = 1
OFFSET
  $1
-- params: ["0"]

SELECT
  "public"."MockServer"."id",
  "public"."MockServer"."name",
  "public"."MockServer"."subdomain",
  "public"."MockServer"."creatorUid",
  "public"."MockServer"."collectionID",
  "public"."MockServer"."workspaceType"::text,
  "public"."MockServer"."workspaceID",
  "public"."MockServer"."delayInMs",
  "public"."MockServer"."isPublic",
  "public"."MockServer"."isActive",
  "public"."MockServer"."hitCount",
  "public"."MockServer"."lastHitAt",
  "public"."MockServer"."createdOn",
  "public"."MockServer"."updatedOn",
  "public"."MockServer"."deletedAt"
FROM
  "public"."MockServer"
WHERE
  "public"."MockServer"."id" IN ($1)
OFFSET
  $2
-- params: ["<mockServerID of the rows above>","0"]
```

**findUnique**: One row by what is unique to it

```ts
await prisma.mockServerLog.findUnique({ where: { id: "example" } });
```

```sql
SELECT
  "public"."MockServerLog"."id",
  "public"."MockServerLog"."mockServerID",
  "public"."MockServerLog"."requestMethod",
  "public"."MockServerLog"."requestPath",
  "public"."MockServerLog"."requestHeaders",
  "public"."MockServerLog"."requestBody",
  "public"."MockServerLog"."requestQuery",
  "public"."MockServerLog"."responseStatus",
  "public"."MockServerLog"."responseHeaders",
  "public"."MockServerLog"."responseBody",
  "public"."MockServerLog"."responseTime",
  "public"."MockServerLog"."ipAddress",
  "public"."MockServerLog"."userAgent",
  "public"."MockServerLog"."executedAt"
FROM
  "public"."MockServerLog"
WHERE
  (
    "public"."MockServerLog"."id" = $1
    AND 1 = 1
  )
LIMIT
  $2
OFFSET
  $3
-- params: ["example","1","0"]
```

**create**: Create a row

```ts
await prisma.mockServerLog.create({
  data: {
    mockServerID: "example",
    requestMethod: "example",
    requestPath: "example",
    requestHeaders: {},
    responseStatus: 1,
    responseHeaders: {},
    responseTime: 1,
  },
});
```

```sql
INSERT INTO
  "public"."MockServerLog" (
    "id",
    "mockServerID",
    "requestMethod",
    "requestPath",
    "requestHeaders",
    "responseStatus",
    "responseHeaders",
    "responseTime",
    "executedAt"
  )
VALUES
  ($1, $2, $3, $4, $5, $6, $7, $8, $9)
RETURNING
  "public"."MockServerLog"."id",
  "public"."MockServerLog"."mockServerID",
  "public"."MockServerLog"."requestMethod",
  "public"."MockServerLog"."requestPath",
  "public"."MockServerLog"."requestHeaders",
  "public"."MockServerLog"."requestBody",
  "public"."MockServerLog"."requestQuery",
  "public"."MockServerLog"."responseStatus",
  "public"."MockServerLog"."responseHeaders",
  "public"."MockServerLog"."responseBody",
  "public"."MockServerLog"."responseTime",
  "public"."MockServerLog"."ipAddress",
  "public"."MockServerLog"."userAgent",
  "public"."MockServerLog"."executedAt"
-- params: ["<cuid(1)>","example","example","example","{}","1","{}","1","<now()>"]
```

**update**: Update a row

```ts
await prisma.mockServerLog.update({ where: { id: "example" }, data: { requestMethod: "example" } });
```

```sql
UPDATE "public"."MockServerLog"
SET
  "requestMethod" = $1
WHERE
  (
    "public"."MockServerLog"."id" = $2
    AND 1 = 1
  )
RETURNING
  "public"."MockServerLog"."id",
  "public"."MockServerLog"."mockServerID",
  "public"."MockServerLog"."requestMethod",
  "public"."MockServerLog"."requestPath",
  "public"."MockServerLog"."requestHeaders",
  "public"."MockServerLog"."requestBody",
  "public"."MockServerLog"."requestQuery",
  "public"."MockServerLog"."responseStatus",
  "public"."MockServerLog"."responseHeaders",
  "public"."MockServerLog"."responseBody",
  "public"."MockServerLog"."responseTime",
  "public"."MockServerLog"."ipAddress",
  "public"."MockServerLog"."userAgent",
  "public"."MockServerLog"."executedAt"
-- params: ["example","example"]
```

**delete**: Delete a row

```ts
await prisma.mockServerLog.delete({ where: { id: "example" } });
```

```sql
DELETE FROM "public"."MockServerLog"
WHERE
  (
    "public"."MockServerLog"."id" = $1
    AND 1 = 1
  )
RETURNING
  "public"."MockServerLog"."id",
  "public"."MockServerLog"."mockServerID",
  "public"."MockServerLog"."requestMethod",
  "public"."MockServerLog"."requestPath",
  "public"."MockServerLog"."requestHeaders",
  "public"."MockServerLog"."requestBody",
  "public"."MockServerLog"."requestQuery",
  "public"."MockServerLog"."responseStatus",
  "public"."MockServerLog"."responseHeaders",
  "public"."MockServerLog"."responseBody",
  "public"."MockServerLog"."responseTime",
  "public"."MockServerLog"."ipAddress",
  "public"."MockServerLog"."userAgent",
  "public"."MockServerLog"."executedAt"
-- params: ["example"]
```

</details>

<a id="model-MockServerActivity"></a>

### MockServerActivity

![MockServerActivity and the models it relates to](tables/MockServerActivity.svg)

| Field          | Type                                         | Constraints                                   | Default   | Description |
| -------------- | -------------------------------------------- | --------------------------------------------- | --------- | ----------- |
| `id`           | `String`                                     | PK NOT NULL                                   | `cuid(1)` |             |
| `mockServerID` | `String`                                     | NOT NULL → [MockServer.id](#model-MockServer) |           |             |
| `action`       | [`MockServerAction`](#enum-MockServerAction) | NOT NULL                                      |           |             |
| `performedBy`  | `String`                                     | NULL                                          |           |             |
| `performedAt`  | `DateTime` `@db.Timestamptz(3)`              | NOT NULL                                      | `now()`   |             |

#### Indexes

- `@@index([mockServerID])`

#### References

- `mockServerID` → [MockServer.id](#model-MockServer) (many-to-one, on delete Cascade)

#### Definition

```sql
CREATE TABLE "MockServerActivity" (
    "id" TEXT NOT NULL,
    "mockServerID" TEXT NOT NULL,
    "action" "MockServerAction" NOT NULL,
    "performedBy" TEXT,
    "performedAt" TIMESTAMPTZ(3) NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT "MockServerActivity_pkey" PRIMARY KEY ("id")
);

CREATE INDEX "MockServerActivity_mockServerID_idx" ON "MockServerActivity"("mockServerID");

ALTER TABLE "MockServerActivity" ADD CONSTRAINT "MockServerActivity_mockServerID_fkey" FOREIGN KEY ("mockServerID") REFERENCES "MockServer"("id") ON DELETE CASCADE ON UPDATE CASCADE;
```

<details>
<summary>Queries and their SQL</summary>

**findMany**: Every row

```ts
await prisma.mockServerActivity.findMany();
```

```sql
SELECT
  "public"."MockServerActivity"."id",
  "public"."MockServerActivity"."mockServerID",
  "public"."MockServerActivity"."action"::text,
  "public"."MockServerActivity"."performedBy",
  "public"."MockServerActivity"."performedAt"
FROM
  "public"."MockServerActivity"
WHERE
  1 = 1
OFFSET
  $1
-- params: ["0"]
```

**count**: How many rows there are

```ts
await prisma.mockServerActivity.count();
```

```sql
SELECT
  COUNT(*) AS "_count$_all"
FROM
  (
    SELECT
      "public"."MockServerActivity"."id"
    FROM
      "public"."MockServerActivity"
    WHERE
      1 = 1
    OFFSET
      $1
  ) AS "sub"
-- params: ["0"]
```

**findMany**: Every row with its relations

```ts
await prisma.mockServerActivity.findMany({ include: { mockServer: true } });
```

```sql
SELECT
  "public"."MockServerActivity"."id",
  "public"."MockServerActivity"."mockServerID",
  "public"."MockServerActivity"."action"::text,
  "public"."MockServerActivity"."performedBy",
  "public"."MockServerActivity"."performedAt"
FROM
  "public"."MockServerActivity"
WHERE
  1 = 1
OFFSET
  $1
-- params: ["0"]

SELECT
  "public"."MockServer"."id",
  "public"."MockServer"."name",
  "public"."MockServer"."subdomain",
  "public"."MockServer"."creatorUid",
  "public"."MockServer"."collectionID",
  "public"."MockServer"."workspaceType"::text,
  "public"."MockServer"."workspaceID",
  "public"."MockServer"."delayInMs",
  "public"."MockServer"."isPublic",
  "public"."MockServer"."isActive",
  "public"."MockServer"."hitCount",
  "public"."MockServer"."lastHitAt",
  "public"."MockServer"."createdOn",
  "public"."MockServer"."updatedOn",
  "public"."MockServer"."deletedAt"
FROM
  "public"."MockServer"
WHERE
  "public"."MockServer"."id" IN ($1)
OFFSET
  $2
-- params: ["<mockServerID of the rows above>","0"]
```

**findUnique**: One row by what is unique to it

```ts
await prisma.mockServerActivity.findUnique({ where: { id: "example" } });
```

```sql
SELECT
  "public"."MockServerActivity"."id",
  "public"."MockServerActivity"."mockServerID",
  "public"."MockServerActivity"."action"::text,
  "public"."MockServerActivity"."performedBy",
  "public"."MockServerActivity"."performedAt"
FROM
  "public"."MockServerActivity"
WHERE
  (
    "public"."MockServerActivity"."id" = $1
    AND 1 = 1
  )
LIMIT
  $2
OFFSET
  $3
-- params: ["example","1","0"]
```

**create**: Create a row

```ts
await prisma.mockServerActivity.create({ data: { mockServerID: "example", action: "CREATED" } });
```

```sql
INSERT INTO
  "public"."MockServerActivity" ("id", "mockServerID", "action", "performedAt")
VALUES
  (
    $1,
    $2,
    CAST($3::text AS "public"."MockServerAction"),
    $4
  )
RETURNING
  "public"."MockServerActivity"."id",
  "public"."MockServerActivity"."mockServerID",
  "public"."MockServerActivity"."action"::text,
  "public"."MockServerActivity"."performedBy",
  "public"."MockServerActivity"."performedAt"
-- params: ["<cuid(1)>","example","CREATED","<now()>"]
```

**update**: Update a row

```ts
await prisma.mockServerActivity.update({ where: { id: "example" }, data: { action: "CREATED" } });
```

```sql
UPDATE "public"."MockServerActivity"
SET
  "action" = CAST($1::text AS "public"."MockServerAction")
WHERE
  (
    "public"."MockServerActivity"."id" = $2
    AND 1 = 1
  )
RETURNING
  "public"."MockServerActivity"."id",
  "public"."MockServerActivity"."mockServerID",
  "public"."MockServerActivity"."action"::text,
  "public"."MockServerActivity"."performedBy",
  "public"."MockServerActivity"."performedAt"
-- params: ["CREATED","example"]
```

**delete**: Delete a row

```ts
await prisma.mockServerActivity.delete({ where: { id: "example" } });
```

```sql
DELETE FROM "public"."MockServerActivity"
WHERE
  (
    "public"."MockServerActivity"."id" = $1
    AND 1 = 1
  )
RETURNING
  "public"."MockServerActivity"."id",
  "public"."MockServerActivity"."mockServerID",
  "public"."MockServerActivity"."action"::text,
  "public"."MockServerActivity"."performedBy",
  "public"."MockServerActivity"."performedAt"
-- params: ["example"]
```

</details>

<a id="model-Shortcode"></a>

### Shortcode

![Shortcode and the models it relates to](tables/Shortcode.svg)

| Field             | Type                            | Constraints                    | Default              | Description |
| ----------------- | ------------------------------- | ------------------------------ | -------------------- | ----------- |
| `id`              | `String`                        | PK NOT NULL                    |                      |             |
| `request`         | `Json`                          | NOT NULL                       |                      |             |
| `creatorUid`      | `String`                        | NULL → [User.uid](#model-User) |                      |             |
| `embedProperties` | `Json`                          | NULL                           |                      |             |
| `createdOn`       | `DateTime` `@db.Timestamptz(3)` | NOT NULL                       | `now()`              |             |
| `updatedOn`       | `DateTime` `@db.Timestamptz(3)` | NOT NULL                       | `now()` `@updatedAt` |             |

#### Indexes

- `@@unique([id, creatorUid], name: "creator_uid_shortcode_unique")`

#### References

- `creatorUid` → [User.uid](#model-User) (many-to-one)

#### Definition

```sql
CREATE TABLE "Shortcode" (
    "id" TEXT NOT NULL,
    "request" JSONB NOT NULL,
    "creatorUid" TEXT,
    "embedProperties" JSONB,
    "createdOn" TIMESTAMPTZ(3) NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "updatedOn" TIMESTAMPTZ(3) NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT "Shortcode_pkey" PRIMARY KEY ("id")
);

CREATE UNIQUE INDEX "Shortcode_id_key" ON "Shortcode"("id");

CREATE UNIQUE INDEX "Shortcode_id_creatorUid_key" ON "Shortcode"("id", "creatorUid");

ALTER TABLE "Shortcode" ADD CONSTRAINT "Shortcode_creatorUid_fkey" FOREIGN KEY ("creatorUid") REFERENCES "User"("uid") ON DELETE SET NULL ON UPDATE CASCADE;
```

<details>
<summary>Queries and their SQL</summary>

**findMany**: Every row

```ts
await prisma.shortcode.findMany();
```

```sql
SELECT
  "public"."Shortcode"."id",
  "public"."Shortcode"."request",
  "public"."Shortcode"."creatorUid",
  "public"."Shortcode"."embedProperties",
  "public"."Shortcode"."createdOn",
  "public"."Shortcode"."updatedOn"
FROM
  "public"."Shortcode"
WHERE
  1 = 1
OFFSET
  $1
-- params: ["0"]
```

**count**: How many rows there are

```ts
await prisma.shortcode.count();
```

```sql
SELECT
  COUNT(*) AS "_count$_all"
FROM
  (
    SELECT
      "public"."Shortcode"."id"
    FROM
      "public"."Shortcode"
    WHERE
      1 = 1
    OFFSET
      $1
  ) AS "sub"
-- params: ["0"]
```

**findMany**: Every row with its relations

```ts
await prisma.shortcode.findMany({ include: { user: true } });
```

```sql
SELECT
  "public"."Shortcode"."id",
  "public"."Shortcode"."request",
  "public"."Shortcode"."creatorUid",
  "public"."Shortcode"."embedProperties",
  "public"."Shortcode"."createdOn",
  "public"."Shortcode"."updatedOn"
FROM
  "public"."Shortcode"
WHERE
  1 = 1
OFFSET
  $1
-- params: ["0"]

SELECT
  "public"."User"."uid",
  "public"."User"."displayName",
  "public"."User"."email",
  "public"."User"."photoURL",
  "public"."User"."isAdmin",
  "public"."User"."refreshToken",
  "public"."User"."currentRESTSession",
  "public"."User"."currentGQLSession",
  "public"."User"."createdOn",
  "public"."User"."lastLoggedOn",
  "public"."User"."lastActiveOn"
FROM
  "public"."User"
WHERE
  "public"."User"."uid" IN ($1)
OFFSET
  $2
-- params: ["<creatorUid of the rows above>","0"]
```

**findUnique**: One row by what is unique to it

```ts
await prisma.shortcode.findUnique({ where: { id: "example" } });
```

```sql
SELECT
  "public"."Shortcode"."id",
  "public"."Shortcode"."request",
  "public"."Shortcode"."creatorUid",
  "public"."Shortcode"."embedProperties",
  "public"."Shortcode"."createdOn",
  "public"."Shortcode"."updatedOn"
FROM
  "public"."Shortcode"
WHERE
  (
    "public"."Shortcode"."id" = $1
    AND 1 = 1
  )
LIMIT
  $2
OFFSET
  $3
-- params: ["example","1","0"]
```

**create**: Create a row

```ts
await prisma.shortcode.create({ data: { id: "example", request: {} } });
```

```sql
INSERT INTO
  "public"."Shortcode" ("id", "request", "createdOn", "updatedOn")
VALUES
  ($1, $2, $3, $4)
RETURNING
  "public"."Shortcode"."id",
  "public"."Shortcode"."request",
  "public"."Shortcode"."creatorUid",
  "public"."Shortcode"."embedProperties",
  "public"."Shortcode"."createdOn",
  "public"."Shortcode"."updatedOn"
-- params: ["example","{}","<now()>","<now()>"]
```

**update**: Update a row

```ts
await prisma.shortcode.update({ where: { id: "example" }, data: { request: {} } });
```

```sql
UPDATE "public"."Shortcode"
SET
  "request" = $1,
  "updatedOn" = $2
WHERE
  (
    "public"."Shortcode"."id" = $3
    AND 1 = 1
  )
RETURNING
  "public"."Shortcode"."id",
  "public"."Shortcode"."request",
  "public"."Shortcode"."creatorUid",
  "public"."Shortcode"."embedProperties",
  "public"."Shortcode"."createdOn",
  "public"."Shortcode"."updatedOn"
-- params: ["{}",{"prisma__type":"generatorCall","prisma__value":{"name":"now","args":[],"returnType":{"type":"DateTime"}}},"example"]
```

**delete**: Delete a row

```ts
await prisma.shortcode.delete({ where: { id: "example" } });
```

```sql
DELETE FROM "public"."Shortcode"
WHERE
  (
    "public"."Shortcode"."id" = $1
    AND 1 = 1
  )
RETURNING
  "public"."Shortcode"."id",
  "public"."Shortcode"."request",
  "public"."Shortcode"."creatorUid",
  "public"."Shortcode"."embedProperties",
  "public"."Shortcode"."createdOn",
  "public"."Shortcode"."updatedOn"
-- params: ["example"]
```

</details>

<a id="model-InfraConfig"></a>

### InfraConfig

| Field                    | Type                            | Constraints     | Default      | Description |
| ------------------------ | ------------------------------- | --------------- | ------------ | ----------- |
| `id`                     | `String`                        | PK NOT NULL     | `cuid(1)`    |             |
| `name`                   | `String`                        | UNIQUE NOT NULL |              |             |
| `value`                  | `String`                        | NULL            |              |             |
| `isEncrypted`            | `Boolean`                       | NOT NULL        | `false`      |             |
| `lastSyncedEnvFileValue` | `String`                        | NULL            |              |             |
| `createdOn`              | `DateTime` `@db.Timestamptz(3)` | NOT NULL        | `now()`      |             |
| `updatedOn`              | `DateTime` `@db.Timestamptz(3)` | NOT NULL        | `@updatedAt` |             |

#### Definition

```sql
CREATE TABLE "InfraConfig" (
    "id" TEXT NOT NULL,
    "name" TEXT NOT NULL,
    "value" TEXT,
    "isEncrypted" BOOLEAN NOT NULL DEFAULT false,
    "lastSyncedEnvFileValue" TEXT,
    "createdOn" TIMESTAMPTZ(3) NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "updatedOn" TIMESTAMPTZ(3) NOT NULL,

    CONSTRAINT "InfraConfig_pkey" PRIMARY KEY ("id")
);

CREATE UNIQUE INDEX "InfraConfig_name_key" ON "InfraConfig"("name");
```

<details>
<summary>Queries and their SQL</summary>

**findMany**: Every row

```ts
await prisma.infraConfig.findMany();
```

```sql
SELECT
  "public"."InfraConfig"."id",
  "public"."InfraConfig"."name",
  "public"."InfraConfig"."value",
  "public"."InfraConfig"."isEncrypted",
  "public"."InfraConfig"."lastSyncedEnvFileValue",
  "public"."InfraConfig"."createdOn",
  "public"."InfraConfig"."updatedOn"
FROM
  "public"."InfraConfig"
WHERE
  1 = 1
OFFSET
  $1
-- params: ["0"]
```

**count**: How many rows there are

```ts
await prisma.infraConfig.count();
```

```sql
SELECT
  COUNT(*) AS "_count$_all"
FROM
  (
    SELECT
      "public"."InfraConfig"."id"
    FROM
      "public"."InfraConfig"
    WHERE
      1 = 1
    OFFSET
      $1
  ) AS "sub"
-- params: ["0"]
```

**findUnique**: One row by what is unique to it

```ts
await prisma.infraConfig.findUnique({ where: { id: "example" } });
```

```sql
SELECT
  "public"."InfraConfig"."id",
  "public"."InfraConfig"."name",
  "public"."InfraConfig"."value",
  "public"."InfraConfig"."isEncrypted",
  "public"."InfraConfig"."lastSyncedEnvFileValue",
  "public"."InfraConfig"."createdOn",
  "public"."InfraConfig"."updatedOn"
FROM
  "public"."InfraConfig"
WHERE
  (
    "public"."InfraConfig"."id" = $1
    AND 1 = 1
  )
LIMIT
  $2
OFFSET
  $3
-- params: ["example","1","0"]
```

**create**: Create a row

```ts
await prisma.infraConfig.create({ data: { name: "example" } });
```

```sql
INSERT INTO
  "public"."InfraConfig" (
    "id",
    "name",
    "isEncrypted",
    "createdOn",
    "updatedOn"
  )
VALUES
  ($1, $2, $3, $4, $5)
RETURNING
  "public"."InfraConfig"."id",
  "public"."InfraConfig"."name",
  "public"."InfraConfig"."value",
  "public"."InfraConfig"."isEncrypted",
  "public"."InfraConfig"."lastSyncedEnvFileValue",
  "public"."InfraConfig"."createdOn",
  "public"."InfraConfig"."updatedOn"
-- params: ["<cuid(1)>","example",false,"<now()>","<now()>"]
```

**update**: Update a row

```ts
await prisma.infraConfig.update({ where: { id: "example" }, data: { name: "example" } });
```

```sql
UPDATE "public"."InfraConfig"
SET
  "name" = $1,
  "updatedOn" = $2
WHERE
  (
    "public"."InfraConfig"."id" = $3
    AND 1 = 1
  )
RETURNING
  "public"."InfraConfig"."id",
  "public"."InfraConfig"."name",
  "public"."InfraConfig"."value",
  "public"."InfraConfig"."isEncrypted",
  "public"."InfraConfig"."lastSyncedEnvFileValue",
  "public"."InfraConfig"."createdOn",
  "public"."InfraConfig"."updatedOn"
-- params: ["example",{"prisma__type":"generatorCall","prisma__value":{"name":"now","args":[],"returnType":{"type":"DateTime"}}},"example"]
```

**delete**: Delete a row

```ts
await prisma.infraConfig.delete({ where: { id: "example" } });
```

```sql
DELETE FROM "public"."InfraConfig"
WHERE
  (
    "public"."InfraConfig"."id" = $1
    AND 1 = 1
  )
RETURNING
  "public"."InfraConfig"."id",
  "public"."InfraConfig"."name",
  "public"."InfraConfig"."value",
  "public"."InfraConfig"."isEncrypted",
  "public"."InfraConfig"."lastSyncedEnvFileValue",
  "public"."InfraConfig"."createdOn",
  "public"."InfraConfig"."updatedOn"
-- params: ["example"]
```

</details>

<a id="model-PersonalAccessToken"></a>

### PersonalAccessToken

![PersonalAccessToken and the models it relates to](tables/PersonalAccessToken.svg)

| Field       | Type                            | Constraints                        | Default      | Description |
| ----------- | ------------------------------- | ---------------------------------- | ------------ | ----------- |
| `id`        | `String`                        | PK NOT NULL                        | `cuid(1)`    |             |
| `userUid`   | `String`                        | NOT NULL → [User.uid](#model-User) |              |             |
| `label`     | `String`                        | NOT NULL                           |              |             |
| `token`     | `String`                        | UNIQUE NOT NULL                    | `uuid(4)`    |             |
| `expiresOn` | `DateTime` `@db.Timestamptz(3)` | NULL                               |              |             |
| `createdOn` | `DateTime` `@db.Timestamptz(3)` | NOT NULL                           | `now()`      |             |
| `updatedOn` | `DateTime` `@db.Timestamptz(3)` | NOT NULL                           | `@updatedAt` |             |

#### References

- `userUid` → [User.uid](#model-User) (many-to-one, on delete Cascade)

#### Definition

```sql
CREATE TABLE "PersonalAccessToken" (
    "id" TEXT NOT NULL,
    "userUid" TEXT NOT NULL,
    "label" TEXT NOT NULL,
    "token" TEXT NOT NULL,
    "expiresOn" TIMESTAMPTZ(3),
    "createdOn" TIMESTAMPTZ(3) NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "updatedOn" TIMESTAMPTZ(3) NOT NULL,

    CONSTRAINT "PersonalAccessToken_pkey" PRIMARY KEY ("id")
);

CREATE UNIQUE INDEX "PersonalAccessToken_token_key" ON "PersonalAccessToken"("token");

ALTER TABLE "PersonalAccessToken" ADD CONSTRAINT "PersonalAccessToken_userUid_fkey" FOREIGN KEY ("userUid") REFERENCES "User"("uid") ON DELETE CASCADE ON UPDATE CASCADE;
```

<details>
<summary>Queries and their SQL</summary>

**findMany**: Every row

```ts
await prisma.personalAccessToken.findMany();
```

```sql
SELECT
  "public"."PersonalAccessToken"."id",
  "public"."PersonalAccessToken"."userUid",
  "public"."PersonalAccessToken"."label",
  "public"."PersonalAccessToken"."token",
  "public"."PersonalAccessToken"."expiresOn",
  "public"."PersonalAccessToken"."createdOn",
  "public"."PersonalAccessToken"."updatedOn"
FROM
  "public"."PersonalAccessToken"
WHERE
  1 = 1
OFFSET
  $1
-- params: ["0"]
```

**count**: How many rows there are

```ts
await prisma.personalAccessToken.count();
```

```sql
SELECT
  COUNT(*) AS "_count$_all"
FROM
  (
    SELECT
      "public"."PersonalAccessToken"."id"
    FROM
      "public"."PersonalAccessToken"
    WHERE
      1 = 1
    OFFSET
      $1
  ) AS "sub"
-- params: ["0"]
```

**findMany**: Every row with its relations

```ts
await prisma.personalAccessToken.findMany({ include: { user: true } });
```

```sql
SELECT
  "public"."PersonalAccessToken"."id",
  "public"."PersonalAccessToken"."userUid",
  "public"."PersonalAccessToken"."label",
  "public"."PersonalAccessToken"."token",
  "public"."PersonalAccessToken"."expiresOn",
  "public"."PersonalAccessToken"."createdOn",
  "public"."PersonalAccessToken"."updatedOn"
FROM
  "public"."PersonalAccessToken"
WHERE
  1 = 1
OFFSET
  $1
-- params: ["0"]

SELECT
  "public"."User"."uid",
  "public"."User"."displayName",
  "public"."User"."email",
  "public"."User"."photoURL",
  "public"."User"."isAdmin",
  "public"."User"."refreshToken",
  "public"."User"."currentRESTSession",
  "public"."User"."currentGQLSession",
  "public"."User"."createdOn",
  "public"."User"."lastLoggedOn",
  "public"."User"."lastActiveOn"
FROM
  "public"."User"
WHERE
  "public"."User"."uid" IN ($1)
OFFSET
  $2
-- params: ["<userUid of the rows above>","0"]
```

**findUnique**: One row by what is unique to it

```ts
await prisma.personalAccessToken.findUnique({ where: { id: "example" } });
```

```sql
SELECT
  "public"."PersonalAccessToken"."id",
  "public"."PersonalAccessToken"."userUid",
  "public"."PersonalAccessToken"."label",
  "public"."PersonalAccessToken"."token",
  "public"."PersonalAccessToken"."expiresOn",
  "public"."PersonalAccessToken"."createdOn",
  "public"."PersonalAccessToken"."updatedOn"
FROM
  "public"."PersonalAccessToken"
WHERE
  (
    "public"."PersonalAccessToken"."id" = $1
    AND 1 = 1
  )
LIMIT
  $2
OFFSET
  $3
-- params: ["example","1","0"]
```

**create**: Create a row

```ts
await prisma.personalAccessToken.create({ data: { userUid: "example", label: "example" } });
```

```sql
INSERT INTO
  "public"."PersonalAccessToken" (
    "id",
    "userUid",
    "label",
    "token",
    "createdOn",
    "updatedOn"
  )
VALUES
  ($1, $2, $3, $4, $5, $6)
RETURNING
  "public"."PersonalAccessToken"."id",
  "public"."PersonalAccessToken"."userUid",
  "public"."PersonalAccessToken"."label",
  "public"."PersonalAccessToken"."token",
  "public"."PersonalAccessToken"."expiresOn",
  "public"."PersonalAccessToken"."createdOn",
  "public"."PersonalAccessToken"."updatedOn"
-- params: ["<cuid(1)>","example","example","<uuid(4)>","<now()>","<now()>"]
```

**update**: Update a row

```ts
await prisma.personalAccessToken.update({ where: { id: "example" }, data: { label: "example" } });
```

```sql
UPDATE "public"."PersonalAccessToken"
SET
  "label" = $1,
  "updatedOn" = $2
WHERE
  (
    "public"."PersonalAccessToken"."id" = $3
    AND 1 = 1
  )
RETURNING
  "public"."PersonalAccessToken"."id",
  "public"."PersonalAccessToken"."userUid",
  "public"."PersonalAccessToken"."label",
  "public"."PersonalAccessToken"."token",
  "public"."PersonalAccessToken"."expiresOn",
  "public"."PersonalAccessToken"."createdOn",
  "public"."PersonalAccessToken"."updatedOn"
-- params: ["example",{"prisma__type":"generatorCall","prisma__value":{"name":"now","args":[],"returnType":{"type":"DateTime"}}},"example"]
```

**delete**: Delete a row

```ts
await prisma.personalAccessToken.delete({ where: { id: "example" } });
```

```sql
DELETE FROM "public"."PersonalAccessToken"
WHERE
  (
    "public"."PersonalAccessToken"."id" = $1
    AND 1 = 1
  )
RETURNING
  "public"."PersonalAccessToken"."id",
  "public"."PersonalAccessToken"."userUid",
  "public"."PersonalAccessToken"."label",
  "public"."PersonalAccessToken"."token",
  "public"."PersonalAccessToken"."expiresOn",
  "public"."PersonalAccessToken"."createdOn",
  "public"."PersonalAccessToken"."updatedOn"
-- params: ["example"]
```

</details>

<a id="model-InfraToken"></a>

### InfraToken

| Field        | Type                            | Constraints     | Default   | Description |
| ------------ | ------------------------------- | --------------- | --------- | ----------- |
| `id`         | `String`                        | PK NOT NULL     | `cuid(1)` |             |
| `creatorUid` | `String`                        | NOT NULL        |           |             |
| `label`      | `String`                        | NOT NULL        |           |             |
| `token`      | `String`                        | UNIQUE NOT NULL | `uuid(4)` |             |
| `expiresOn`  | `DateTime` `@db.Timestamptz(3)` | NULL            |           |             |
| `createdOn`  | `DateTime` `@db.Timestamptz(3)` | NOT NULL        | `now()`   |             |
| `updatedOn`  | `DateTime` `@db.Timestamptz(3)` | NOT NULL        | `now()`   |             |

#### Definition

```sql
CREATE TABLE "InfraToken" (
    "id" TEXT NOT NULL,
    "creatorUid" TEXT NOT NULL,
    "label" TEXT NOT NULL,
    "token" TEXT NOT NULL,
    "expiresOn" TIMESTAMPTZ(3),
    "createdOn" TIMESTAMPTZ(3) NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "updatedOn" TIMESTAMPTZ(3) NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT "InfraToken_pkey" PRIMARY KEY ("id")
);

CREATE UNIQUE INDEX "InfraToken_token_key" ON "InfraToken"("token");
```

<details>
<summary>Queries and their SQL</summary>

**findMany**: Every row

```ts
await prisma.infraToken.findMany();
```

```sql
SELECT
  "public"."InfraToken"."id",
  "public"."InfraToken"."creatorUid",
  "public"."InfraToken"."label",
  "public"."InfraToken"."token",
  "public"."InfraToken"."expiresOn",
  "public"."InfraToken"."createdOn",
  "public"."InfraToken"."updatedOn"
FROM
  "public"."InfraToken"
WHERE
  1 = 1
OFFSET
  $1
-- params: ["0"]
```

**count**: How many rows there are

```ts
await prisma.infraToken.count();
```

```sql
SELECT
  COUNT(*) AS "_count$_all"
FROM
  (
    SELECT
      "public"."InfraToken"."id"
    FROM
      "public"."InfraToken"
    WHERE
      1 = 1
    OFFSET
      $1
  ) AS "sub"
-- params: ["0"]
```

**findUnique**: One row by what is unique to it

```ts
await prisma.infraToken.findUnique({ where: { id: "example" } });
```

```sql
SELECT
  "public"."InfraToken"."id",
  "public"."InfraToken"."creatorUid",
  "public"."InfraToken"."label",
  "public"."InfraToken"."token",
  "public"."InfraToken"."expiresOn",
  "public"."InfraToken"."createdOn",
  "public"."InfraToken"."updatedOn"
FROM
  "public"."InfraToken"
WHERE
  (
    "public"."InfraToken"."id" = $1
    AND 1 = 1
  )
LIMIT
  $2
OFFSET
  $3
-- params: ["example","1","0"]
```

**create**: Create a row

```ts
await prisma.infraToken.create({ data: { creatorUid: "example", label: "example" } });
```

```sql
INSERT INTO
  "public"."InfraToken" (
    "id",
    "creatorUid",
    "label",
    "token",
    "createdOn",
    "updatedOn"
  )
VALUES
  ($1, $2, $3, $4, $5, $6)
RETURNING
  "public"."InfraToken"."id",
  "public"."InfraToken"."creatorUid",
  "public"."InfraToken"."label",
  "public"."InfraToken"."token",
  "public"."InfraToken"."expiresOn",
  "public"."InfraToken"."createdOn",
  "public"."InfraToken"."updatedOn"
-- params: ["<cuid(1)>","example","example","<uuid(4)>","<now()>","<now()>"]
```

**update**: Update a row

```ts
await prisma.infraToken.update({ where: { id: "example" }, data: { creatorUid: "example" } });
```

```sql
UPDATE "public"."InfraToken"
SET
  "creatorUid" = $1
WHERE
  (
    "public"."InfraToken"."id" = $2
    AND 1 = 1
  )
RETURNING
  "public"."InfraToken"."id",
  "public"."InfraToken"."creatorUid",
  "public"."InfraToken"."label",
  "public"."InfraToken"."token",
  "public"."InfraToken"."expiresOn",
  "public"."InfraToken"."createdOn",
  "public"."InfraToken"."updatedOn"
-- params: ["example","example"]
```

**delete**: Delete a row

```ts
await prisma.infraToken.delete({ where: { id: "example" } });
```

```sql
DELETE FROM "public"."InfraToken"
WHERE
  (
    "public"."InfraToken"."id" = $1
    AND 1 = 1
  )
RETURNING
  "public"."InfraToken"."id",
  "public"."InfraToken"."creatorUid",
  "public"."InfraToken"."label",
  "public"."InfraToken"."token",
  "public"."InfraToken"."expiresOn",
  "public"."InfraToken"."createdOn",
  "public"."InfraToken"."updatedOn"
-- params: ["example"]
```

</details>

<a id="model-PublishedDocs"></a>

### PublishedDocs

| Field                  | Type                                   | Constraints | Default      | Description |
| ---------------------- | -------------------------------------- | ----------- | ------------ | ----------- |
| `id`                   | `String`                               | PK NOT NULL | `cuid(1)`    |             |
| `slug`                 | `String`                               | NOT NULL    |              |             |
| `title`                | `String`                               | NOT NULL    |              |             |
| `collectionID`         | `String`                               | NOT NULL    |              |             |
| `creatorUid`           | `String`                               | NOT NULL    |              |             |
| `version`              | `String`                               | NOT NULL    |              |             |
| `autoSync`             | `Boolean`                              | NOT NULL    |              |             |
| `documentTree`         | `Json`                                 | NULL        |              |             |
| `workspaceType`        | [`WorkspaceType`](#enum-WorkspaceType) | NOT NULL    |              |             |
| `workspaceID`          | `String`                               | NOT NULL    |              |             |
| `environmentID`        | `String`                               | NULL        |              |             |
| `environmentName`      | `String`                               | NULL        |              |             |
| `environmentVariables` | `Json`                                 | NULL        |              |             |
| `metadata`             | `Json`                                 | NULL        |              |             |
| `createdOn`            | `DateTime` `@db.Timestamptz(3)`        | NOT NULL    | `now()`      |             |
| `updatedOn`            | `DateTime` `@db.Timestamptz(3)`        | NOT NULL    | `@updatedAt` |             |

#### Indexes

- `@@index([collectionID])`
- `@@unique([slug, version])`

#### Definition

```sql
CREATE TABLE "PublishedDocs" (
    "id" TEXT NOT NULL,
    "slug" TEXT NOT NULL,
    "title" TEXT NOT NULL,
    "collectionID" TEXT NOT NULL,
    "creatorUid" TEXT NOT NULL,
    "version" TEXT NOT NULL,
    "autoSync" BOOLEAN NOT NULL,
    "documentTree" JSONB,
    "workspaceType" "WorkspaceType" NOT NULL,
    "workspaceID" TEXT NOT NULL,
    "environmentID" TEXT,
    "environmentName" TEXT,
    "environmentVariables" JSONB,
    "metadata" JSONB,
    "createdOn" TIMESTAMPTZ(3) NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "updatedOn" TIMESTAMPTZ(3) NOT NULL,

    CONSTRAINT "PublishedDocs_pkey" PRIMARY KEY ("id")
);

CREATE INDEX "PublishedDocs_collectionID_idx" ON "PublishedDocs"("collectionID");

CREATE UNIQUE INDEX "PublishedDocs_slug_version_key" ON "PublishedDocs"("slug", "version");
```

<details>
<summary>Queries and their SQL</summary>

**findMany**: Every row

```ts
await prisma.publishedDocs.findMany();
```

```sql
SELECT
  "public"."PublishedDocs"."id",
  "public"."PublishedDocs"."slug",
  "public"."PublishedDocs"."title",
  "public"."PublishedDocs"."collectionID",
  "public"."PublishedDocs"."creatorUid",
  "public"."PublishedDocs"."version",
  "public"."PublishedDocs"."autoSync",
  "public"."PublishedDocs"."documentTree",
  "public"."PublishedDocs"."workspaceType"::text,
  "public"."PublishedDocs"."workspaceID",
  "public"."PublishedDocs"."environmentID",
  "public"."PublishedDocs"."environmentName",
  "public"."PublishedDocs"."environmentVariables",
  "public"."PublishedDocs"."metadata",
  "public"."PublishedDocs"."createdOn",
  "public"."PublishedDocs"."updatedOn"
FROM
  "public"."PublishedDocs"
WHERE
  1 = 1
OFFSET
  $1
-- params: ["0"]
```

**count**: How many rows there are

```ts
await prisma.publishedDocs.count();
```

```sql
SELECT
  COUNT(*) AS "_count$_all"
FROM
  (
    SELECT
      "public"."PublishedDocs"."id"
    FROM
      "public"."PublishedDocs"
    WHERE
      1 = 1
    OFFSET
      $1
  ) AS "sub"
-- params: ["0"]
```

**findUnique**: One row by what is unique to it

```ts
await prisma.publishedDocs.findUnique({ where: { id: "example" } });
```

```sql
SELECT
  "public"."PublishedDocs"."id",
  "public"."PublishedDocs"."slug",
  "public"."PublishedDocs"."title",
  "public"."PublishedDocs"."collectionID",
  "public"."PublishedDocs"."creatorUid",
  "public"."PublishedDocs"."version",
  "public"."PublishedDocs"."autoSync",
  "public"."PublishedDocs"."documentTree",
  "public"."PublishedDocs"."workspaceType"::text,
  "public"."PublishedDocs"."workspaceID",
  "public"."PublishedDocs"."environmentID",
  "public"."PublishedDocs"."environmentName",
  "public"."PublishedDocs"."environmentVariables",
  "public"."PublishedDocs"."metadata",
  "public"."PublishedDocs"."createdOn",
  "public"."PublishedDocs"."updatedOn"
FROM
  "public"."PublishedDocs"
WHERE
  (
    "public"."PublishedDocs"."id" = $1
    AND 1 = 1
  )
LIMIT
  $2
OFFSET
  $3
-- params: ["example","1","0"]
```

**create**: Create a row

```ts
await prisma.publishedDocs.create({
  data: {
    slug: "example",
    title: "example",
    collectionID: "example",
    creatorUid: "example",
    version: "example",
    autoSync: true,
    workspaceType: "USER",
    workspaceID: "example",
  },
});
```

```sql
INSERT INTO
  "public"."PublishedDocs" (
    "id",
    "slug",
    "title",
    "collectionID",
    "creatorUid",
    "version",
    "autoSync",
    "workspaceType",
    "workspaceID",
    "createdOn",
    "updatedOn"
  )
VALUES
  (
    $1,
    $2,
    $3,
    $4,
    $5,
    $6,
    $7,
    CAST($8::text AS "public"."WorkspaceType"),
    $9,
    $10,
    $11
  )
RETURNING
  "public"."PublishedDocs"."id",
  "public"."PublishedDocs"."slug",
  "public"."PublishedDocs"."title",
  "public"."PublishedDocs"."collectionID",
  "public"."PublishedDocs"."creatorUid",
  "public"."PublishedDocs"."version",
  "public"."PublishedDocs"."autoSync",
  "public"."PublishedDocs"."documentTree",
  "public"."PublishedDocs"."workspaceType"::text,
  "public"."PublishedDocs"."workspaceID",
  "public"."PublishedDocs"."environmentID",
  "public"."PublishedDocs"."environmentName",
  "public"."PublishedDocs"."environmentVariables",
  "public"."PublishedDocs"."metadata",
  "public"."PublishedDocs"."createdOn",
  "public"."PublishedDocs"."updatedOn"
-- params: ["<cuid(1)>","example","example","example","example","example",true,"USER","example","<now()>","<now()>"]
```

**update**: Update a row

```ts
await prisma.publishedDocs.update({ where: { id: "example" }, data: { slug: "example" } });
```

```sql
UPDATE "public"."PublishedDocs"
SET
  "slug" = $1,
  "updatedOn" = $2
WHERE
  (
    "public"."PublishedDocs"."id" = $3
    AND 1 = 1
  )
RETURNING
  "public"."PublishedDocs"."id",
  "public"."PublishedDocs"."slug",
  "public"."PublishedDocs"."title",
  "public"."PublishedDocs"."collectionID",
  "public"."PublishedDocs"."creatorUid",
  "public"."PublishedDocs"."version",
  "public"."PublishedDocs"."autoSync",
  "public"."PublishedDocs"."documentTree",
  "public"."PublishedDocs"."workspaceType"::text,
  "public"."PublishedDocs"."workspaceID",
  "public"."PublishedDocs"."environmentID",
  "public"."PublishedDocs"."environmentName",
  "public"."PublishedDocs"."environmentVariables",
  "public"."PublishedDocs"."metadata",
  "public"."PublishedDocs"."createdOn",
  "public"."PublishedDocs"."updatedOn"
-- params: ["example",{"prisma__type":"generatorCall","prisma__value":{"name":"now","args":[],"returnType":{"type":"DateTime"}}},"example"]
```

**delete**: Delete a row

```ts
await prisma.publishedDocs.delete({ where: { id: "example" } });
```

```sql
DELETE FROM "public"."PublishedDocs"
WHERE
  (
    "public"."PublishedDocs"."id" = $1
    AND 1 = 1
  )
RETURNING
  "public"."PublishedDocs"."id",
  "public"."PublishedDocs"."slug",
  "public"."PublishedDocs"."title",
  "public"."PublishedDocs"."collectionID",
  "public"."PublishedDocs"."creatorUid",
  "public"."PublishedDocs"."version",
  "public"."PublishedDocs"."autoSync",
  "public"."PublishedDocs"."documentTree",
  "public"."PublishedDocs"."workspaceType"::text,
  "public"."PublishedDocs"."workspaceID",
  "public"."PublishedDocs"."environmentID",
  "public"."PublishedDocs"."environmentName",
  "public"."PublishedDocs"."environmentVariables",
  "public"."PublishedDocs"."metadata",
  "public"."PublishedDocs"."createdOn",
  "public"."PublishedDocs"."updatedOn"
-- params: ["example"]
```

</details>

## Enums

<a id="enum-WorkspaceType"></a>

### WorkspaceType

- `USER`
- `TEAM`

#### Definition

```sql
CREATE TYPE "WorkspaceType" AS ENUM ('USER', 'TEAM');
```

<a id="enum-ReqType"></a>

### ReqType

- `REST`
- `GQL`

#### Definition

```sql
CREATE TYPE "ReqType" AS ENUM ('REST', 'GQL');
```

<a id="enum-TeamAccessRole"></a>

### TeamAccessRole

- `OWNER`
- `VIEWER`
- `EDITOR`

#### Definition

```sql
CREATE TYPE "TeamAccessRole" AS ENUM ('OWNER', 'VIEWER', 'EDITOR');
```

<a id="enum-MockServerAction"></a>

### MockServerAction

- `CREATED`
- `DELETED`
- `ACTIVATED`
- `DEACTIVATED`

#### Definition

```sql
CREATE TYPE "MockServerAction" AS ENUM ('CREATED', 'DELETED', 'ACTIVATED', 'DEACTIVATED');
```
