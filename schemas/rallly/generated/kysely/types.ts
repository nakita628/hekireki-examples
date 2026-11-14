import type { ColumnType } from 'kysely'

export type Generated<T> =
  T extends ColumnType<infer S, infer I, infer U>
    ? ColumnType<S, I | undefined, U>
    : ColumnType<T, T | undefined, T>

export type Timestamp = ColumnType<Date, Date | string, Date | string>

export type ApiUsageResource = 'poll'

export type SubscriptionStatus =
  | 'incomplete'
  | 'incomplete_expired'
  | 'active'
  | 'paused'
  | 'trialing'
  | 'past_due'
  | 'canceled'
  | 'unpaid'

export type SubscriptionInterval = 'month' | 'year'

export type NonprofitApplicationStatus = 'approved' | 'rejected' | 'failed'

export type QueuedEmailStatus = 'pending' | 'sent' | 'skipped' | 'failed'

export type ScheduledEventStatus = 'confirmed' | 'canceled' | 'unconfirmed'

export type ScheduledEventInviteStatus = 'pending' | 'accepted' | 'declined' | 'tentative'

export type CredentialType = 'OAUTH'

export type LicenseType = 'PLUS' | 'ORGANIZATION' | 'ENTERPRISE'

export type LicenseStatus = 'ACTIVE' | 'REVOKED'

export type PollStatus = 'open' | 'closed' | 'scheduled' | 'canceled'

export type PollKind = 'date' | 'time'

export type PollClosedReason = 'auto' | 'manual'

export type VoteType = 'yes' | 'no' | 'ifNeedBe'

export type SpaceMemberRole = 'ADMIN' | 'MEMBER'

export type SpaceType = 'personal' | 'work'

export type SpaceTier = 'hobby' | 'pro'

export type WebhookDeliveryStatus = 'pending' | 'in_flight' | 'succeeded' | 'failed' | 'exhausted'

export type TimeFormat = 'hours12' | 'hours24'

export type UserRole = 'admin' | 'user'

export interface ApiUsage {
  space_id: string
  resource: ApiUsageResource
  period: Timestamp
  count: Generated<number>
  near_limit_warned_at: Timestamp | null
  at_limit_warned_at: Timestamp | null
  updated_at: Timestamp
}

export interface Subscription {
  id: string
  price_id: string
  quantity: Generated<number>
  subscription_item_id: string
  amount: number
  discount_percent_off: number | null
  discount_amount_off: number | null
  status: SubscriptionStatus
  active: boolean
  currency: string
  interval: SubscriptionInterval
  created_at: Generated<Timestamp>
  period_start: Timestamp
  period_end: Timestamp
  cancel_at_period_end: Generated<boolean>
  user_id: string
  space_id: string
  api_addon_item_id: string | null
  api_addon_price_id: string | null
  api_addon_amount: number | null
  api_addon_tier: string | null
  api_poll_retained_tier: string | null
  api_poll_retained_until: Timestamp | null
}

export interface PaymentMethod {
  id: string
  user_id: string
  type: string
  data: unknown
  created_at: Generated<Timestamp>
  updated_at: Timestamp
}

export interface NonprofitApplication {
  id: string
  space_id: string
  user_id: string
  organization_name: string
  website: string
  email_domain: string
  status: NonprofitApplicationStatus
  reason: string | null
  model: string | null
  created_at: Generated<Timestamp>
}

export interface QueuedEmail {
  id: string
  kind: string
  subject_id: string
  batch_id: string
  user_id: string
  status: Generated<QueuedEmailStatus>
  send_after: Timestamp | null
  attempts: Generated<number>
  claimed_at: Timestamp | null
  sent_at: Timestamp | null
  last_error: string | null
  created_at: Generated<Timestamp>
  updated_at: Timestamp
}

export interface EventType {
  id: string
  space_id: string
  host_id: string
  name: string
  duration_minutes: number
  capacity: number | null
  description: string | null
  location: unknown
  created_at: Generated<Timestamp>
  updated_at: Timestamp
  deleted: Generated<boolean>
  deleted_at: Timestamp | null
}

export interface ScheduledEvent {
  id: string
  user_id: string
  space_id: string
  event_type_id: string | null
  sheet_slot_id: string | null
  title: string
  description: string | null
  location: unknown
  conferencing: unknown
  capacity: number | null
  created_at: Generated<Timestamp>
  updated_at: Timestamp
  status: Generated<ScheduledEventStatus>
  time_zone: string | null
  start: Timestamp
  end: Timestamp
  all_day: Generated<boolean>
  deleted_at: Timestamp | null
  sequence: Generated<number>
  uid: string
  hide_attendees: Generated<boolean>
}

export interface RescheduledEventDate {
  id: string
  scheduled_event_id: string
  start: Timestamp
  end: Timestamp
  all_day: Generated<boolean>
  created_at: Generated<Timestamp>
}

export interface ScheduledEventInvite {
  id: string
  uid: string
  scheduled_event_id: string
  invitee_name: string
  invitee_email: string
  invitee_id: string | null
  invitee_time_zone: string | null
  invitee_locale: string | null
  status: Generated<ScheduledEventInviteStatus>
  created_at: Generated<Timestamp>
  updated_at: Timestamp
}

export interface InstanceSettings {
  id: Generated<number>
  disable_user_registration: Generated<boolean>
  instance_id: Generated<string>
  app_name: string | null
  primary_color: string | null
  primary_color_dark: string | null
  logo: string | null
  logo_dark: string | null
  logo_icon: string | null
  hide_attribution: boolean | null
  footer_links: Generated<unknown>
  created_at: Generated<Timestamp>
  updated_at: Generated<Timestamp>
}

export interface Credential {
  id: string
  user_id: string
  provider: string
  provider_account_id: string
  type: CredentialType
  secret: string
  scopes: string[]
  expires_at: Timestamp | null
  created_at: Generated<Timestamp>
  updated_at: Timestamp
}

export interface CalendarConnection {
  id: string
  user_id: string
  provider: string
  integration_id: string
  provider_account_id: string
  email: string
  display_name: string | null
  created_at: Generated<Timestamp>
  updated_at: Timestamp
  credential_id: string
}

export interface ProviderCalendar {
  id: string
  calendar_connection_id: string
  provider_calendar_id: string
  name: string
  primary: Generated<boolean>
  time_zone: string | null
  selected: Generated<boolean>
  deleted: Generated<boolean>
  writable: Generated<boolean>
  provider_data: unknown
  last_synced_at: Generated<Timestamp>
  created_at: Generated<Timestamp>
  updated_at: Timestamp
}

export interface ConferencingConnection {
  id: string
  user_id: string
  provider: string
  integration_id: string
  provider_account_id: string
  email: string
  display_name: string | null
  created_at: Generated<Timestamp>
  updated_at: Timestamp
  credential_id: string
}

export interface License {
  id: string
  license_key: string
  idempotency_key: string | null
  version: number | null
  type: LicenseType
  seats: number | null
  issued_at: Generated<Timestamp>
  expires_at: Timestamp | null
  licensee_email: string | null
  licensee_name: string | null
  white_label_addon: Generated<boolean>
  status: Generated<LicenseStatus>
}

export interface LicenseValidation {
  id: string
  license_id: string
  ip_address: string | null
  fingerprint: string | null
  validated_at: Generated<Timestamp>
  user_agent: string | null
}

export interface InstanceLicense {
  id: string
  license_key: string
  version: number | null
  type: LicenseType
  seats: number | null
  issued_at: Generated<Timestamp>
  expires_at: Timestamp | null
  licensee_email: string | null
  licensee_name: string | null
  white_label_addon: Generated<boolean>
  status: Generated<LicenseStatus>
}

export interface UserNotificationPreferences {
  id: string
  user_id: string
  prefs: Generated<unknown>
  created_at: Generated<Timestamp>
  updated_at: Timestamp
}

export interface Poll {
  id: string
  created_at: Generated<Timestamp>
  updated_at: Timestamp
  deadline: Timestamp | null
  title: string
  description: string | null
  location: string | null
  conferencing: unknown
  user_id: string | null
  time_zone: string | null
  status: Generated<PollStatus>
  closed_reason: PollClosedReason | null
  kind: Generated<PollKind>
  deleted: Generated<boolean>
  deleted_at: Timestamp | null
  event_id: string | null
  scheduled_event_id: string | null
  hide_participants: Generated<boolean>
  hide_scores: Generated<boolean>
  disable_comments: Generated<boolean>
  require_participant_email: Generated<boolean>
  allow_tentative_votes: Generated<boolean>
  muted: Generated<boolean>
  space_id: string | null
}

export interface Participant {
  id: string
  name: string
  email: string | null
  note: string | null
  token: string
  user_id: string | null
  poll_id: string
  locale: string | null
  time_zone: string | null
  created_at: Generated<Timestamp>
  updated_at: ColumnType<Date | null, Date | string, Date | string | null>
}

export interface PollInvite {
  id: string
  poll_id: string
  email: string
  token: string
  opened_at: Timestamp | null
  reminded_at: Timestamp | null
  revoked_at: Timestamp | null
  participant_id: string | null
  created_at: Generated<Timestamp>
  updated_at: Timestamp
}

export interface PollActivity {
  id: string
  poll_id: string
  type: string
  user_id: string | null
  participant_id: string | null
  invite_id: string | null
  option_id: string | null
  payload: Generated<unknown>
  created_at: Generated<Timestamp>
}

export interface Option {
  id: string
  start_time: Timestamp
  duration_minutes: Generated<number>
  poll_id: string
  created_at: Generated<Timestamp>
}

export interface Vote {
  id: string
  participant_id: string
  option_id: string
  poll_id: string
  type: Generated<VoteType>
  created_at: Generated<Timestamp>
  updated_at: ColumnType<Date | null, Date | string, Date | string | null>
}

export interface Comment {
  id: string
  content: string
  poll_id: string
  author_name: string
  user_id: string | null
  created_at: Generated<Timestamp>
  updated_at: ColumnType<Date | null, Date | string, Date | string | null>
}

export interface RegisteredInstance {
  id: string
  instance_id: string
  version: string
  first_seen_at: Generated<Timestamp>
  last_seen_at: Generated<Timestamp>
}

export interface Sheet {
  id: string
  space_id: string
  host_id: string
  title: string
  description: string | null
  url_id: string
  created_at: Generated<Timestamp>
  updated_at: Timestamp
  deleted: Generated<boolean>
  deleted_at: Timestamp | null
}

export interface SheetSlot {
  id: string
  sheet_id: string
  event_type_id: string
  start_time: Timestamp
  created_at: Generated<Timestamp>
  updated_at: Timestamp
}

export interface Space {
  id: string
  name: string
  image: string | null
  owner_id: string
  tier: Generated<SpaceTier>
  space_type: SpaceType | null
  industry: string | null
  primary_color: string | null
  show_branding: Generated<boolean>
  hide_attribution: Generated<boolean>
  shared: Generated<boolean>
  nonprofit_discount_granted_at: Timestamp | null
  api_poll_quota_override: number | null
  created_at: Generated<Timestamp>
  updated_at: Timestamp
}

export interface SpaceMember {
  id: string
  space_id: string
  user_id: string
  created_at: Generated<Timestamp>
  updated_at: Timestamp
  role: Generated<SpaceMemberRole>
  last_selected_at: Generated<Timestamp>
}

export interface SpaceMemberInvite {
  id: string
  space_id: string
  email: string
  created_at: Generated<Timestamp>
  updated_at: Timestamp
  role: Generated<SpaceMemberRole>
  inviter_id: string
}

export interface SpaceApiKey {
  id: string
  space_id: string
  name: string
  prefix: string
  hashed_key: string
  last_used_at: Timestamp | null
  expires_at: Timestamp | null
  revoked_at: Timestamp | null
  created_at: Generated<Timestamp>
  updated_at: Timestamp
}

export interface SpaceWebhook {
  id: string
  space_id: string
  url: string
  secret: string
  events: string[]
  enabled: Generated<boolean>
  version: string
  cursor: Generated<Timestamp>
  last_delivered_at: Timestamp | null
  last_failed_at: Timestamp | null
  consecutive_failures: Generated<number>
  created_at: Generated<Timestamp>
  updated_at: Timestamp
}

export interface WebhookDelivery {
  id: string
  webhook_id: string
  activity_id: string
  event_type: string
  payload: unknown
  status: Generated<WebhookDeliveryStatus>
  attempts: Generated<number>
  next_attempt_at: Generated<Timestamp>
  last_response_status: number | null
  last_error: string | null
  created_at: Generated<Timestamp>
  updated_at: Timestamp
}

export interface Account {
  id: string
  user_id: string
  provider: string
  provider_account_id: string
  refresh_token: string | null
  access_token: string | null
  expires_at: number | null
  scope: string | null
  id_token: string | null
  access_token_expires_at: Timestamp | null
  refresh_token_expires_at: Timestamp | null
  password: string | null
  created_at: Generated<Timestamp>
  updated_at: Timestamp
}

export interface User {
  id: string
  name: string
  email: string
  email_verified: boolean | null
  image: string | null
  time_zone: string | null
  week_start: number | null
  time_format: TimeFormat | null
  locale: string | null
  created_at: Generated<Timestamp>
  updated_at: ColumnType<Date | null, Date | string, Date | string | null>
  customer_id: string | null
  banned: Generated<boolean>
  banned_at: Timestamp | null
  ban_reason: string | null
  ban_expires: Timestamp | null
  role: Generated<UserRole>
  last_login_method: string | null
  anonymous: Generated<boolean>
  last_seen_at: Generated<Timestamp>
  deleted_at: Timestamp | null
  job_title: string | null
  review_requested_at: Timestamp | null
  default_destination_calendar_id: string | null
}

export interface Session {
  id: string
  expires_at: Timestamp
  token: string
  created_at: Generated<Timestamp>
  updated_at: Timestamp
  ip_address: string | null
  user_agent: string | null
  user_id: string
  impersonated_by: string | null
}

export interface Verification {
  id: string
  identifier: string
  value: string
  expires_at: Timestamp
  created_at: Generated<Timestamp>
  updated_at: Generated<Timestamp>
}

export interface DB {
  api_usage: ApiUsage
  subscriptions: Subscription
  payment_methods: PaymentMethod
  nonprofit_applications: NonprofitApplication
  queued_emails: QueuedEmail
  event_types: EventType
  scheduled_events: ScheduledEvent
  rescheduled_event_dates: RescheduledEventDate
  scheduled_event_invites: ScheduledEventInvite
  instance_settings: InstanceSettings
  credentials: Credential
  calendar_connections: CalendarConnection
  provider_calendars: ProviderCalendar
  conferencing_connections: ConferencingConnection
  licenses: License
  license_validations: LicenseValidation
  instance_licenses: InstanceLicense
  user_notification_preferences: UserNotificationPreferences
  polls: Poll
  participants: Participant
  poll_invites: PollInvite
  poll_activities: PollActivity
  options: Option
  votes: Vote
  comments: Comment
  registered_instances: RegisteredInstance
  sheets: Sheet
  sheet_slots: SheetSlot
  spaces: Space
  space_members: SpaceMember
  space_member_invites: SpaceMemberInvite
  space_api_keys: SpaceApiKey
  space_webhooks: SpaceWebhook
  webhook_deliveries: WebhookDelivery
  accounts: Account
  users: User
  sessions: Session
  verifications: Verification
}
