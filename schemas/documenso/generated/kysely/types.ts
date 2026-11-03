import type { ColumnType } from 'kysely'

export type Generated<T> =
  T extends ColumnType<infer S, infer I, infer U>
    ? ColumnType<S, I | undefined, U>
    : ColumnType<T, T | undefined, T>

export type Timestamp = ColumnType<Date, Date | string, Date | string>

export type IdentityProvider = 'DOCUMENSO' | 'GOOGLE' | 'OIDC'

export type Role = 'ADMIN' | 'USER'

export type UserSecurityAuditLogType =
  | 'ACCOUNT_PROFILE_UPDATE'
  | 'ACCOUNT_SSO_LINK'
  | 'ACCOUNT_SSO_UNLINK'
  | 'ORGANISATION_SSO_LINK'
  | 'ORGANISATION_SSO_UNLINK'
  | 'AUTH_2FA_DISABLE'
  | 'AUTH_2FA_ENABLE'
  | 'PASSKEY_CREATED'
  | 'PASSKEY_DELETED'
  | 'PASSKEY_UPDATED'
  | 'PASSWORD_RESET'
  | 'PASSWORD_UPDATE'
  | 'SESSION_REVOKED'
  | 'SIGN_OUT'
  | 'SIGN_IN'
  | 'SIGN_IN_FAIL'
  | 'SIGN_IN_2FA_FAIL'
  | 'SIGN_IN_PASSKEY_FAIL'

export type WebhookTriggerEvents =
  | 'DOCUMENT_CREATED'
  | 'DOCUMENT_SENT'
  | 'DOCUMENT_OPENED'
  | 'DOCUMENT_SIGNED'
  | 'DOCUMENT_COMPLETED'
  | 'DOCUMENT_REJECTED'
  | 'DOCUMENT_CANCELLED'
  | 'RECIPIENT_EXPIRED'
  | 'DOCUMENT_RECIPIENT_COMPLETED'
  | 'DOCUMENT_REMINDER_SENT'
  | 'TEMPLATE_CREATED'
  | 'TEMPLATE_UPDATED'
  | 'TEMPLATE_DELETED'
  | 'TEMPLATE_USED'

export type WebhookCallStatus = 'SUCCESS' | 'FAILED'

export type ApiTokenAlgorithm = 'SHA512'

export type SubscriptionStatus = 'ACTIVE' | 'PAST_DUE' | 'INACTIVE'

export type DocumentStatus = 'DRAFT' | 'PENDING' | 'COMPLETED' | 'REJECTED' | 'CANCELLED'

export type DocumentSource = 'DOCUMENT' | 'TEMPLATE' | 'TEMPLATE_DIRECT_LINK'

export type DocumentVisibility = 'EVERYONE' | 'MANAGER_AND_ABOVE' | 'ADMIN'

export type FolderType = 'DOCUMENT' | 'TEMPLATE'

export type EnvelopeType = 'DOCUMENT' | 'TEMPLATE'

export type DocumentDataType = 'S3_PATH' | 'BYTES' | 'BYTES_64'

export type DocumentSigningOrder = 'PARALLEL' | 'SEQUENTIAL'

export type DocumentDistributionMethod = 'EMAIL' | 'NONE'

export type ReadStatus = 'NOT_OPENED' | 'OPENED'

export type SendStatus = 'NOT_SENT' | 'SENT'

export type SigningStatus = 'NOT_SIGNED' | 'SIGNED' | 'REJECTED'

export type RecipientRole = 'CC' | 'SIGNER' | 'VIEWER' | 'APPROVER' | 'ASSISTANT'

export type FieldType =
  | 'SIGNATURE'
  | 'FREE_SIGNATURE'
  | 'INITIALS'
  | 'NAME'
  | 'EMAIL'
  | 'DATE'
  | 'TEXT'
  | 'NUMBER'
  | 'RADIO'
  | 'CHECKBOX'
  | 'DROPDOWN'

export type OrganisationType = 'PERSONAL' | 'ORGANISATION'

export type OrganisationGroupType = 'INTERNAL_ORGANISATION' | 'INTERNAL_TEAM' | 'CUSTOM'

export type OrganisationMemberRole = 'ADMIN' | 'MANAGER' | 'MEMBER'

export type TeamMemberRole = 'ADMIN' | 'MANAGER' | 'MEMBER'

export type OrganisationMemberInviteStatus = 'ACCEPTED' | 'PENDING' | 'DECLINED'

export type TemplateType = 'PUBLIC' | 'PRIVATE' | 'ORGANISATION'

export type BackgroundJobStatus = 'PENDING' | 'PROCESSING' | 'COMPLETED' | 'FAILED'

export type BackgroundJobTaskStatus = 'PENDING' | 'COMPLETED' | 'FAILED'

export type EmailDomainStatus = 'PENDING' | 'ACTIVE'

export type EmailTransportType = 'SMTP_AUTH' | 'SMTP_API' | 'RESEND' | 'MAILCHANNELS'

export interface User {
  id: Generated<number>
  name: string | null
  email: string
  emailVerified: Timestamp | null
  password: string | null
  source: string | null
  signature: string | null
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
  lastSignedIn: Generated<Timestamp>
  roles: Generated<Role[]>
  identityProvider: Generated<IdentityProvider>
  avatarImageId: string | null
  disabled: Generated<boolean>
  twoFactorSecret: string | null
  twoFactorEnabled: Generated<boolean>
  twoFactorBackupCodes: string | null
}

export interface TeamProfile {
  id: string
  enabled: Generated<boolean>
  teamId: number
  bio: string | null
}

export interface UserSecurityAuditLog {
  id: Generated<number>
  userId: number
  createdAt: Generated<Timestamp>
  type: UserSecurityAuditLogType
  userAgent: string | null
  ipAddress: string | null
}

export interface PasswordResetToken {
  id: Generated<number>
  token: string
  createdAt: Generated<Timestamp>
  expiry: Timestamp
  userId: number
}

export interface Passkey {
  id: string
  userId: number
  name: string
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
  lastUsedAt: Timestamp | null
  credentialId: Buffer
  credentialPublicKey: Buffer
  counter: bigint
  credentialDeviceType: string
  credentialBackedUp: boolean
  transports: string[]
}

export interface AnonymousVerificationToken {
  id: string
  token: string
  expiresAt: Timestamp
  createdAt: Generated<Timestamp>
}

export interface VerificationToken {
  id: Generated<number>
  secondaryId: string
  identifier: string
  token: string
  completed: Generated<boolean>
  expires: Timestamp
  createdAt: Generated<Timestamp>
  metadata: unknown
  userId: number
}

export interface Webhook {
  id: string
  webhookUrl: string
  eventTriggers: WebhookTriggerEvents[]
  secret: string | null
  enabled: Generated<boolean>
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
  userId: number
  teamId: number
}

export interface WebhookCall {
  id: string
  status: WebhookCallStatus
  url: string
  event: WebhookTriggerEvents
  requestBody: unknown
  responseCode: number
  responseHeaders: unknown
  responseBody: unknown
  createdAt: Generated<Timestamp>
  webhookId: string
}

export interface ApiToken {
  id: Generated<number>
  name: string
  token: string
  algorithm: Generated<ApiTokenAlgorithm>
  expires: Timestamp | null
  createdAt: Generated<Timestamp>
  lastUsedAt: Timestamp | null
  userId: number | null
  teamId: number
}

export interface Subscription {
  id: Generated<number>
  status: Generated<SubscriptionStatus>
  planId: string
  priceId: string
  periodEnd: Timestamp | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  cancelAtPeriodEnd: Generated<boolean>
  customerId: string
  organisationId: string
}

export interface SubscriptionClaim {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  name: string
  locked: Generated<boolean>
  teamCount: number
  memberCount: number
  envelopeItemCount: number
  recipientCount: number
  flags: unknown
  documentRateLimits: unknown
  documentQuota: number | null
  emailRateLimits: unknown
  emailQuota: number | null
  apiRateLimits: unknown
  apiQuota: number | null
  emailTransportId: string | null
}

export interface OrganisationClaim {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  originalSubscriptionClaimId: string | null
  teamCount: number
  memberCount: number
  envelopeItemCount: number
  recipientCount: number
  flags: unknown
  documentRateLimits: unknown
  documentQuota: number | null
  emailRateLimits: unknown
  emailQuota: number | null
  apiRateLimits: unknown
  apiQuota: number | null
  emailTransportId: string | null
}

export interface OrganisationMonthlyStat {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  organisationId: string
  period: string
  documentCount: Generated<number>
  emailCount: Generated<number>
  apiCount: Generated<number>
  emailReports: Generated<number>
}

export interface Account {
  id: string
  createdAt: Generated<Timestamp>
  userId: number
  type: string
  provider: string
  providerAccountId: string
  refresh_token: string | null
  access_token: string | null
  expires_at: number | null
  created_at: number | null
  ext_expires_in: number | null
  token_type: string | null
  scope: string | null
  id_token: string | null
  session_state: string | null
  password: string | null
}

export interface Session {
  id: string
  sessionToken: string
  userId: number
  ipAddress: string | null
  userAgent: string | null
  expiresAt: Timestamp
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  isEmbed: Generated<boolean>
}

export interface AuthHandoff {
  id: string
  type: string
  payload: string
  createdAt: Generated<Timestamp>
  expiresAt: Timestamp
}

export interface Folder {
  id: string
  name: string
  userId: number
  teamId: number
  pinned: Generated<boolean>
  parentId: string | null
  createdAt: Generated<Timestamp>
  updatedAt: Generated<Timestamp>
  visibility: Generated<DocumentVisibility>
  type: FolderType
}

export interface Envelope {
  id: string
  secondaryId: string
  externalId: string | null
  type: EnvelopeType
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  completedAt: Timestamp | null
  deletedAt: Timestamp | null
  title: string
  status: Generated<DocumentStatus>
  source: DocumentSource
  qrToken: string | null
  signatureLevel: string
  internalVersion: number
  useLegacyFieldInsertion: Generated<boolean>
  authOptions: unknown
  formValues: unknown
  visibility: Generated<DocumentVisibility>
  templateType: Generated<TemplateType>
  publicTitle: Generated<string>
  publicDescription: Generated<string>
  templateId: number | null
  userId: number
  teamId: number
  folderId: string | null
  documentMetaId: string
}

export interface EnvelopeItem {
  id: string
  title: string
  order: number
  documentDataId: string
  envelopeId: string
}

export interface DocumentAuditLog {
  id: string
  envelopeId: string | null
  createdAt: Generated<Timestamp>
  type: string
  data: unknown
  name: string | null
  email: string | null
  userId: number | null
  userAgent: string | null
  ipAddress: string | null
}

export interface DocumentData {
  id: string
  type: DocumentDataType
  data: string
  initialData: string
}

export interface DocumentMeta {
  id: string
  subject: string | null
  message: string | null
  timezone: Generated<string | null>
  dateFormat: Generated<string | null>
  redirectUrl: string | null
  signingOrder: Generated<DocumentSigningOrder>
  allowDictateNextSigner: Generated<boolean>
  typedSignatureEnabled: Generated<boolean>
  uploadSignatureEnabled: Generated<boolean>
  drawSignatureEnabled: Generated<boolean>
  language: Generated<string>
  distributionMethod: Generated<DocumentDistributionMethod>
  emailSettings: unknown
  emailReplyTo: string | null
  emailId: string | null
  envelopeExpirationPeriod: unknown
  reminderSettings: unknown
}

export interface EnvelopeAttachment {
  id: string
  type: string
  label: string
  data: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  envelopeId: string
}

export interface Recipient {
  id: Generated<number>
  envelopeId: string
  email: string
  name: Generated<string>
  token: string
  documentDeletedAt: Timestamp | null
  expired: Timestamp | null
  expiresAt: Timestamp | null
  expirationNotifiedAt: Timestamp | null
  sentAt: Timestamp | null
  signedAt: Timestamp | null
  lastReminderSentAt: Timestamp | null
  nextReminderAt: Timestamp | null
  reminderCount: Generated<number>
  authOptions: unknown
  signingOrder: number | null
  rejectionReason: string | null
  role: Generated<RecipientRole>
  readStatus: Generated<ReadStatus>
  signingStatus: Generated<SigningStatus>
  sendStatus: Generated<SendStatus>
}

export interface Field {
  id: Generated<number>
  secondaryId: string
  envelopeId: string
  envelopeItemId: string
  recipientId: number
  type: FieldType
  page: number
  positionX: Generated<string>
  positionY: Generated<string>
  width: Generated<string>
  height: Generated<string>
  customText: string
  inserted: boolean
  fieldMeta: unknown
}

export interface Signature {
  id: Generated<number>
  created: Generated<Timestamp>
  recipientId: number
  fieldId: number
  signatureImageAsBase64: string | null
  typedSignature: string | null
}

export interface CscCredential {
  id: string
  providerId: string
  credentialId: string
  certCache: Buffer | null
  signatureAlgorithm: string
  keyType: string
  digestAlgorithm: string
  keyLenBits: number | null
  signAlgoParams: string | null
  serviceTokenCiphertext: Buffer | null
  serviceTokenExpiresAt: Timestamp | null
  recipientId: number
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface CscSession {
  id: string
  envelopeId: string
  signingTime: Timestamp
  itemsJson: unknown
  encryptedSad: Buffer | null
  sadExpiresAt: Timestamp | null
  createdAt: Generated<Timestamp>
  recipientId: number
}

export interface DocumentShareLink {
  id: Generated<number>
  email: string
  slug: string
  envelopeId: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface Organisation {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  type: OrganisationType
  name: string
  url: string
  avatarImageId: string | null
  customerId: string | null
  organisationClaimId: string
  ownerUserId: number
  organisationGlobalSettingsId: string
  organisationAuthenticationPortalId: string
}

export interface OrganisationMember {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  userId: number
  organisationId: string
}

export interface OrganisationMemberInvite {
  id: string
  createdAt: Generated<Timestamp>
  email: string
  token: string
  status: Generated<OrganisationMemberInviteStatus>
  organisationId: string
  organisationRole: OrganisationMemberRole
}

export interface OrganisationGroup {
  id: string
  name: string | null
  type: OrganisationGroupType
  organisationRole: OrganisationMemberRole
  organisationId: string
}

export interface OrganisationGroupMember {
  id: string
  groupId: string
  organisationMemberId: string
}

export interface TeamGroup {
  id: string
  organisationGroupId: string
  teamRole: TeamMemberRole
  teamId: number
}

export interface OrganisationGlobalSettings {
  id: string
  documentVisibility: Generated<DocumentVisibility>
  documentLanguage: Generated<string>
  includeSenderDetails: Generated<boolean>
  includeSigningCertificate: Generated<boolean>
  includeAuditLog: Generated<boolean>
  documentTimezone: string | null
  documentDateFormat: Generated<string>
  delegateDocumentOwnership: Generated<boolean>
  typedSignatureEnabled: Generated<boolean>
  uploadSignatureEnabled: Generated<boolean>
  drawSignatureEnabled: Generated<boolean>
  defaultRecipients: unknown
  emailId: string | null
  emailReplyTo: string | null
  emailDocumentSettings: unknown
  brandingEnabled: Generated<boolean>
  brandingLogo: Generated<string>
  brandingUrl: Generated<string>
  brandingCompanyDetails: Generated<string>
  brandingColors: unknown
  brandingCss: Generated<string>
  envelopeExpirationPeriod: unknown
  reminderSettings: unknown
  aiFeaturesEnabled: Generated<boolean>
}

export interface TeamGlobalSettings {
  id: string
  documentVisibility: DocumentVisibility | null
  documentLanguage: string | null
  documentTimezone: string | null
  documentDateFormat: string | null
  delegateDocumentOwnership: boolean | null
  includeSenderDetails: boolean | null
  includeSigningCertificate: boolean | null
  includeAuditLog: boolean | null
  typedSignatureEnabled: boolean | null
  uploadSignatureEnabled: boolean | null
  drawSignatureEnabled: boolean | null
  defaultRecipients: unknown
  emailId: string | null
  emailReplyTo: string | null
  emailDocumentSettings: unknown
  brandingEnabled: boolean | null
  brandingLogo: string | null
  brandingUrl: string | null
  brandingCompanyDetails: string | null
  brandingColors: unknown
  brandingCss: string | null
  envelopeExpirationPeriod: unknown
  reminderSettings: unknown
  aiFeaturesEnabled: boolean | null
}

export interface Team {
  id: Generated<number>
  name: string
  url: string
  createdAt: Generated<Timestamp>
  avatarImageId: string | null
  organisationId: string
  teamGlobalSettingsId: string
}

export interface TeamEmail {
  teamId: number
  createdAt: Generated<Timestamp>
  name: string
  email: string
}

export interface TeamEmailVerification {
  teamId: number
  name: string
  email: string
  token: string
  completed: Generated<boolean>
  expiresAt: Timestamp
  createdAt: Generated<Timestamp>
}

export interface TemplateDirectLink {
  id: string
  envelopeId: string
  token: string
  createdAt: Generated<Timestamp>
  enabled: boolean
  directTemplateRecipientId: number
}

export interface SiteSettings {
  id: string
  enabled: Generated<boolean>
  data: unknown
  lastModifiedByUserId: number | null
  lastModifiedAt: Generated<Timestamp>
}

export interface BackgroundJob {
  id: string
  status: Generated<BackgroundJobStatus>
  payload: unknown
  retried: Generated<number>
  maxRetries: Generated<number>
  jobId: string
  name: string
  version: string
  submittedAt: Generated<Timestamp>
  updatedAt: Timestamp
  completedAt: Timestamp | null
  lastRetriedAt: Timestamp | null
}

export interface BackgroundJobTask {
  id: string
  name: string
  status: Generated<BackgroundJobTaskStatus>
  result: unknown
  retried: Generated<number>
  maxRetries: Generated<number>
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  completedAt: Timestamp | null
  jobId: string
}

export interface AvatarImage {
  id: string
  bytes: string
}

export interface EmailDomain {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  status: Generated<EmailDomainStatus>
  selector: string
  domain: string
  publicKey: string
  privateKey: string
  lastVerifiedAt: Timestamp | null
  organisationId: string
}

export interface OrganisationEmail {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  email: string
  emailName: string
  emailDomainId: string
  organisationId: string
}

export interface EmailTransport {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  name: string
  type: EmailTransportType
  fromName: string
  fromAddress: string
  config: string
}

export interface OrganisationAuthenticationPortal {
  id: string
  enabled: Generated<boolean>
  clientId: Generated<string>
  clientSecret: Generated<string>
  wellKnownUrl: Generated<string>
  defaultOrganisationRole: Generated<OrganisationMemberRole>
  autoProvisionUsers: Generated<boolean>
  allowedDomains: Generated<string[]>
  allowPersonalOrganisations: Generated<boolean>
}

export interface Counter {
  id: string
  value: number
}

export interface EmbedSignature {
  key: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  organisationId: string
  recipientId: number
  signature: string
}

export interface RateLimit {
  key: string
  action: string
  bucket: Timestamp
  count: Generated<number>
  createdAt: Generated<Timestamp>
}

export interface DB {
  User: User
  TeamProfile: TeamProfile
  UserSecurityAuditLog: UserSecurityAuditLog
  PasswordResetToken: PasswordResetToken
  Passkey: Passkey
  AnonymousVerificationToken: AnonymousVerificationToken
  VerificationToken: VerificationToken
  Webhook: Webhook
  WebhookCall: WebhookCall
  ApiToken: ApiToken
  Subscription: Subscription
  SubscriptionClaim: SubscriptionClaim
  OrganisationClaim: OrganisationClaim
  OrganisationMonthlyStat: OrganisationMonthlyStat
  Account: Account
  Session: Session
  AuthHandoff: AuthHandoff
  Folder: Folder
  Envelope: Envelope
  EnvelopeItem: EnvelopeItem
  DocumentAuditLog: DocumentAuditLog
  DocumentData: DocumentData
  DocumentMeta: DocumentMeta
  EnvelopeAttachment: EnvelopeAttachment
  Recipient: Recipient
  Field: Field
  Signature: Signature
  CscCredential: CscCredential
  CscSession: CscSession
  DocumentShareLink: DocumentShareLink
  Organisation: Organisation
  OrganisationMember: OrganisationMember
  OrganisationMemberInvite: OrganisationMemberInvite
  OrganisationGroup: OrganisationGroup
  OrganisationGroupMember: OrganisationGroupMember
  TeamGroup: TeamGroup
  OrganisationGlobalSettings: OrganisationGlobalSettings
  TeamGlobalSettings: TeamGlobalSettings
  Team: Team
  TeamEmail: TeamEmail
  TeamEmailVerification: TeamEmailVerification
  TemplateDirectLink: TemplateDirectLink
  SiteSettings: SiteSettings
  BackgroundJob: BackgroundJob
  BackgroundJobTask: BackgroundJobTask
  AvatarImage: AvatarImage
  EmailDomain: EmailDomain
  OrganisationEmail: OrganisationEmail
  EmailTransport: EmailTransport
  OrganisationAuthenticationPortal: OrganisationAuthenticationPortal
  Counter: Counter
  EmbedSignature: EmbedSignature
  RateLimit: RateLimit
}
