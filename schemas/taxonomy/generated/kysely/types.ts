import type { ColumnType } from 'kysely'

export type Generated<T> =
  T extends ColumnType<infer S, infer I, infer U>
    ? ColumnType<S, I | undefined, U>
    : ColumnType<T, T | undefined, T>

export type Timestamp = ColumnType<Date, Date | string, Date | string>

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
  created_at: Generated<Timestamp>
  updated_at: Generated<Timestamp>
}

export interface Session {
  id: string
  sessionToken: string
  userId: string
  expires: Timestamp
}

export interface User {
  id: string
  name: string | null
  email: string | null
  emailVerified: Timestamp | null
  image: string | null
  created_at: Generated<Timestamp>
  updated_at: Generated<Timestamp>
  stripe_customer_id: string | null
  stripe_subscription_id: string | null
  stripe_price_id: string | null
  stripe_current_period_end: Timestamp | null
}

export interface VerificationToken {
  identifier: string
  token: string
  expires: Timestamp
}

export interface Post {
  id: string
  title: string
  content: unknown
  published: Generated<boolean>
  created_at: Generated<Timestamp>
  updated_at: Generated<Timestamp>
  authorId: string
}

export interface DB {
  accounts: Account
  sessions: Session
  users: User
  verification_tokens: VerificationToken
  posts: Post
}
