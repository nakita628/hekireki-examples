import type { ColumnType } from 'kysely'

export type Generated<T> =
  T extends ColumnType<infer S, infer I, infer U>
    ? ColumnType<S, I | undefined, U>
    : ColumnType<T, T | undefined, T>

export type Timestamp = ColumnType<Date, Date | string, Date | string>

export type NotificationType = 'LIKE' | 'REPLY' | 'MENTION' | 'MISC' | 'SYSTEM'

export type Tags = 'POPULAR' | 'NEWEST'

export type Difficulty = 'BEGINNER' | 'EASY' | 'MEDIUM' | 'HARD' | 'EXTREME' | 'EVENT'

export type RoleTypes = 'USER' | 'ADMIN' | 'MODERATOR' | 'CREATOR' | 'SUPPORTER' | 'CONTRIBUTOR'

export type UserStatus = 'BANNED' | 'ACTIVE'

export type ReportStatus = 'PENDING' | 'CLEARED' | 'DISMISSED'

export type ChallengeStatus = 'DECLINED' | 'BANNED' | 'PENDING' | 'ACTIVE'

export type VoteType = 'CHALLENGE' | 'COMMENT' | 'SUBMISSION' | 'SHAREDSOLUTION'

export type ReportType = 'CHALLENGE' | 'USER' | 'COMMENT' | 'SOLUTION'

export type IssueType =
  | 'DEROGATORY'
  | 'OTHER'
  | 'UNCLEAR'
  | 'BULLYING'
  | 'SPAM'
  | 'HATESPEECH'
  | 'THREAT'

export type CommentRoot = 'SOLUTION' | 'CHALLENGE'

export interface Account {
  id: string
  userId: string
  type: string
  provider: string
  providerAccountId: string
  refresh_token: string | null
  access_token: string | null
  expires_at: number | null
  token_type: string | null
  scope: string | null
  id_token: string | null
  session_state: string | null
}

export interface Session {
  id: string
  sessionToken: string
  userId: string
  expires: Timestamp
}

export interface Role {
  id: string
  role: RoleTypes
}

export interface UserLink {
  id: string
  url: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface User {
  id: string
  name: string
  email: string
  emailVerified: Timestamp | null
  image: string | null
  bio: Generated<string>
  status: Generated<UserStatus>
  banReason: string | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface VerificationToken {
  identifier: string
  token: string
  expires: Timestamp
}

export interface Tag {
  id: string
  tag: Tags
}

export interface Challenge {
  id: Generated<number>
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  difficulty: Difficulty
  name: string
  slug: string
  description: string
  shortDescription: string
  code: string
  tests: string
  status: Generated<ChallengeStatus>
  tsconfig: unknown
  userId: string
}

export interface Vote {
  id: Generated<number>
  userId: string
  rootType: VoteType
  challengeId: number | null
  commentId: number | null
  submissionId: number | null
  sharedSolutionId: number | null
}

export interface SharedSolution {
  id: Generated<number>
  createdAt: Generated<Timestamp>
  isPinned: Generated<boolean>
  title: string
  description: string
  userId: string | null
  challengeId: number | null
}

export interface Submission {
  id: Generated<number>
  code: string
  createdAt: Generated<Timestamp>
  isSuccessful: boolean
  userId: string
  challengeId: number
}

export interface Bookmark {
  id: Generated<number>
  userId: string
  challengeId: number | null
  createdAt: Generated<Timestamp | null>
}

export interface ReportIssue {
  id: Generated<number>
  reportId: number
  type: IssueType
}

export interface Report {
  id: Generated<number>
  text: string
  type: ReportType
  status: ReportStatus
  reporterId: string
  userId: string | null
  challengeId: number | null
  commentId: number | null
  solutionId: number | null
  moderatorId: string | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface Comment {
  id: Generated<number>
  text: string
  visible: Generated<boolean>
  parentId: number | null
  userId: string
  rootType: Generated<CommentRoot>
  rootChallengeId: number | null
  rootSolutionId: number | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface ImageUpload {
  id: string
  userId: string
  url: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface TrackChallenge {
  id: Generated<number>
  orderId: number
  challengeId: number
  trackId: number | null
}

export interface Track {
  id: Generated<number>
  name: string
  slug: string
  description: string
  visible: Generated<boolean>
  isComingSoon: Generated<boolean>
}

export interface BetaTokens {
  id: Generated<number>
  token: string
  userId: string | null
}

export interface Donation {
  id: Generated<number>
  userId: string
  amount: number
  stripeCheckoutSessionId: string
  createdAt: Generated<Timestamp | null>
}

export interface Notification {
  id: Generated<number>
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  isRead: Generated<boolean>
  type: NotificationType
  toUserId: string
  fromUserId: string
  commentId: number | null
  content: string | null
  blurb: string | null
  url: string
  challengeId: number | null
  sharedSolutionId: number | null
}

export interface ShortURL {
  id: Generated<number>
  originalUrl: string
  shortUrlSlug: string
  createdAt: Generated<Timestamp>
  expiresAt: Timestamp | null
  clicks: Generated<number>
  userId: string | null
}

export interface RoleToUser {
  A: string
  B: string
}

export interface UserToUserLink {
  A: string
  B: string
}

export interface TrackToUser {
  A: number
  B: string
}

export interface ChallengeToTag {
  A: number
  B: string
}

export interface DB {
  Account: Account
  Session: Session
  Role: Role
  UserLink: UserLink
  User: User
  VerificationToken: VerificationToken
  Tag: Tag
  Challenge: Challenge
  Vote: Vote
  SharedSolution: SharedSolution
  Submission: Submission
  Bookmark: Bookmark
  ReportIssue: ReportIssue
  Report: Report
  Comment: Comment
  ImageUpload: ImageUpload
  TrackChallenge: TrackChallenge
  Track: Track
  BetaTokens: BetaTokens
  Donation: Donation
  Notification: Notification
  ShortURL: ShortURL
  _RoleToUser: RoleToUser
  _UserToUserLink: UserToUserLink
  _TrackToUser: TrackToUser
  _ChallengeToTag: ChallengeToTag
}
