import type { ColumnType } from 'kysely'

export type Generated<T> =
  T extends ColumnType<infer S, infer I, infer U>
    ? ColumnType<S, I | undefined, U>
    : ColumnType<T, T | undefined, T>

export type Timestamp = ColumnType<Date, Date | string, Date | string>

export type WorkspaceType = 'USER' | 'TEAM'

export type ReqType = 'REST' | 'GQL'

export type TeamAccessRole = 'OWNER' | 'VIEWER' | 'EDITOR'

export type MockServerAction = 'CREATED' | 'DELETED' | 'ACTIVATED' | 'DEACTIVATED'

export interface Team {
  id: string
  name: string
}

export interface TeamMember {
  id: string
  role: TeamAccessRole
  userUid: string
  teamID: string
}

export interface TeamInvitation {
  id: string
  teamID: string
  creatorUid: string
  inviteeEmail: string
  inviteeRole: TeamAccessRole
}

export interface TeamCollection {
  id: string
  parentID: string | null
  teamID: string
  title: string
  orderIndex: number
  data: unknown
  createdOn: Generated<Timestamp>
  updatedOn: Timestamp
}

export interface TeamRequest {
  id: string
  collectionID: string
  teamID: string
  title: string
  request: unknown
  mockExamples: unknown
  orderIndex: number
  createdOn: Generated<Timestamp>
  updatedOn: Timestamp
}

export interface TeamEnvironment {
  id: string
  teamID: string
  name: string
  variables: unknown
}

export interface User {
  uid: string
  displayName: string | null
  email: string | null
  photoURL: string | null
  isAdmin: Generated<boolean>
  refreshToken: string | null
  currentRESTSession: unknown
  currentGQLSession: unknown
  createdOn: Generated<Timestamp>
  lastLoggedOn: Timestamp | null
  lastActiveOn: Timestamp | null
}

export interface Account {
  id: string
  userId: string
  provider: string
  providerAccountId: string
  providerRefreshToken: string | null
  providerAccessToken: string | null
  providerScope: string | null
  loggedIn: Generated<Timestamp>
}

export interface VerificationToken {
  deviceIdentifier: string
  token: string
  userUid: string
  expiresOn: Timestamp
}

export interface UserSettings {
  id: string
  userUid: string
  properties: unknown
  updatedOn: Timestamp
}

export interface UserHistory {
  id: string
  userUid: string
  reqType: ReqType
  request: unknown
  responseMetadata: unknown
  isStarred: boolean
  executedOn: Generated<Timestamp>
}

export interface UserEnvironment {
  id: string
  userUid: string
  name: string | null
  variables: unknown
  isGlobal: boolean
}

export interface InvitedUsers {
  adminUid: string
  adminEmail: string
  inviteeEmail: string
  invitedOn: Generated<Timestamp>
}

export interface UserRequest {
  id: string
  collectionID: string
  userUid: string
  title: string
  request: unknown
  type: ReqType
  mockExamples: unknown
  orderIndex: number
  createdOn: Generated<Timestamp>
  updatedOn: Timestamp
}

export interface UserCollection {
  id: string
  parentID: string | null
  userUid: string
  title: string
  type: ReqType
  orderIndex: number
  data: unknown
  createdOn: Generated<Timestamp>
  updatedOn: Timestamp
}

export interface MockServer {
  id: string
  name: string
  subdomain: string
  creatorUid: string | null
  collectionID: string
  workspaceType: WorkspaceType
  workspaceID: string
  delayInMs: Generated<number>
  isPublic: Generated<boolean>
  isActive: Generated<boolean>
  hitCount: Generated<number>
  lastHitAt: Timestamp | null
  createdOn: Generated<Timestamp>
  updatedOn: Timestamp
  deletedAt: Timestamp | null
}

export interface MockServerLog {
  id: string
  mockServerID: string
  requestMethod: string
  requestPath: string
  requestHeaders: unknown
  requestBody: unknown
  requestQuery: unknown
  responseStatus: number
  responseHeaders: unknown
  responseBody: unknown
  responseTime: number
  ipAddress: string | null
  userAgent: string | null
  executedAt: Generated<Timestamp>
}

export interface MockServerActivity {
  id: string
  mockServerID: string
  action: MockServerAction
  performedBy: string | null
  performedAt: Generated<Timestamp>
}

export interface Shortcode {
  id: string
  request: unknown
  creatorUid: string | null
  embedProperties: unknown
  createdOn: Generated<Timestamp>
  updatedOn: Generated<Timestamp>
}

export interface InfraConfig {
  id: string
  name: string
  value: string | null
  isEncrypted: Generated<boolean>
  lastSyncedEnvFileValue: string | null
  createdOn: Generated<Timestamp>
  updatedOn: Timestamp
}

export interface PersonalAccessToken {
  id: string
  userUid: string
  label: string
  token: string
  expiresOn: Timestamp | null
  createdOn: Generated<Timestamp>
  updatedOn: Timestamp
}

export interface InfraToken {
  id: string
  creatorUid: string
  label: string
  token: string
  expiresOn: Timestamp | null
  createdOn: Generated<Timestamp>
  updatedOn: Generated<Timestamp>
}

export interface PublishedDocs {
  id: string
  slug: string
  title: string
  collectionID: string
  creatorUid: string
  version: string
  autoSync: boolean
  documentTree: unknown
  workspaceType: WorkspaceType
  workspaceID: string
  environmentID: string | null
  environmentName: string | null
  environmentVariables: unknown
  metadata: unknown
  createdOn: Generated<Timestamp>
  updatedOn: Timestamp
}

export interface DB {
  Team: Team
  TeamMember: TeamMember
  TeamInvitation: TeamInvitation
  TeamCollection: TeamCollection
  TeamRequest: TeamRequest
  TeamEnvironment: TeamEnvironment
  User: User
  Account: Account
  VerificationToken: VerificationToken
  UserSettings: UserSettings
  UserHistory: UserHistory
  UserEnvironment: UserEnvironment
  InvitedUsers: InvitedUsers
  UserRequest: UserRequest
  UserCollection: UserCollection
  MockServer: MockServer
  MockServerLog: MockServerLog
  MockServerActivity: MockServerActivity
  Shortcode: Shortcode
  InfraConfig: InfraConfig
  PersonalAccessToken: PersonalAccessToken
  InfraToken: InfraToken
  PublishedDocs: PublishedDocs
}
