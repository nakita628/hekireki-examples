export interface User {
  id: string
  name: string | null
  email: string
  emailVerified: string | null
  image: string | null
}

export interface Account {
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
  sessionToken: string
  userId: string
  expires: string
}

export interface VerificationToken {
  identifier: string
  token: string
  expires: string
}

export interface Authenticator {
  credentialID: string
  userId: string
  providerAccountId: string
  credentialPublicKey: string
  counter: number
  credentialDeviceType: string
  credentialBackedUp: number
  transports: string | null
}

export interface DB {
  User: User
  Account: Account
  Session: Session
  VerificationToken: VerificationToken
  Authenticator: Authenticator
}
