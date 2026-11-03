import type { ColumnType } from 'kysely'

export type Generated<T> =
  T extends ColumnType<infer S, infer I, infer U>
    ? ColumnType<S, I | undefined, U>
    : ColumnType<T, T | undefined, T>

export type Timestamp = ColumnType<Date, Date | string, Date | string>

export interface User {
  user_id: string
  username: string
  password: string
  role: string
  logo_url: string | null
  display_name: string | null
  two_factor_required: Generated<boolean>
  created_at: Generated<Timestamp | null>
  updated_at: ColumnType<Date | null, Date | string, Date | string | null>
  deleted_at: Timestamp | null
}

export interface ApiKey {
  api_key_id: string
  user_id: string
  name: string
  key_hash: string
  key_prefix: string
  last_used_at: Timestamp | null
  created_at: Generated<Timestamp | null>
}

export interface Session {
  session_id: string
  website_id: string
  browser: string | null
  os: string | null
  device: string | null
  screen: string | null
  language: string | null
  country: string | null
  region: string | null
  city: string | null
  distinct_id: string | null
  created_at: Generated<Timestamp | null>
}

export interface SessionLink {
  website_id: string
  session_id: string
  distinct_id: string
  created_at: Generated<Timestamp | null>
}

export interface Website {
  website_id: string
  name: string
  domain: string | null
  reset_at: Timestamp | null
  user_id: string | null
  team_id: string | null
  created_by: string | null
  created_at: Generated<Timestamp | null>
  updated_at: ColumnType<Date | null, Date | string, Date | string | null>
  deleted_at: Timestamp | null
  recorder_enabled: Generated<boolean>
  replay_config: unknown
}

export interface WebsiteEvent {
  event_id: string
  website_id: string
  session_id: string
  visit_id: string
  created_at: Generated<Timestamp | null>
  url_path: string
  url_query: string | null
  utm_source: string | null
  utm_medium: string | null
  utm_campaign: string | null
  utm_content: string | null
  utm_term: string | null
  referrer_path: string | null
  referrer_query: string | null
  referrer_domain: string | null
  page_title: string | null
  gclid: string | null
  fbclid: string | null
  msclkid: string | null
  ttclid: string | null
  li_fat_id: string | null
  twclid: string | null
  event_type: Generated<number>
  event_name: string | null
  tag: string | null
  hostname: string | null
  lcp: string | null
  inp: string | null
  cls: string | null
  fcp: string | null
  ttfb: string | null
}

export interface EventData {
  event_data_id: string
  website_id: string
  website_event_id: string
  data_key: string
  string_value: string | null
  number_value: string | null
  date_value: Timestamp | null
  data_type: number
  created_at: Generated<Timestamp | null>
}

export interface SessionData {
  session_data_id: string
  website_id: string
  session_id: string
  data_key: string
  string_value: string | null
  number_value: string | null
  date_value: Timestamp | null
  data_type: number
  distinct_id: string | null
  created_at: Generated<Timestamp | null>
}

export interface Team {
  team_id: string
  name: string
  access_code: string | null
  logo_url: string | null
  two_factor_required: Generated<boolean>
  created_at: Generated<Timestamp | null>
  updated_at: ColumnType<Date | null, Date | string, Date | string | null>
  deleted_at: Timestamp | null
}

export interface TeamUser {
  team_user_id: string
  team_id: string
  user_id: string
  role: string
  created_at: Generated<Timestamp | null>
  updated_at: ColumnType<Date | null, Date | string, Date | string | null>
}

export interface Report {
  report_id: string
  user_id: string
  website_id: string
  type: string
  name: string
  description: string
  parameters: unknown
  created_at: Generated<Timestamp | null>
  updated_at: ColumnType<Date | null, Date | string, Date | string | null>
}

export interface Annotation {
  annotation_id: string
  website_id: string
  user_id: string | null
  date: Timestamp
  all_day: Generated<boolean>
  note: string
  created_at: Generated<Timestamp | null>
  updated_at: ColumnType<Date | null, Date | string, Date | string | null>
}

export interface Segment {
  segment_id: string
  website_id: string
  type: string
  name: string
  parameters: unknown
  created_at: Generated<Timestamp | null>
  updated_at: ColumnType<Date | null, Date | string, Date | string | null>
}

export interface Revenue {
  revenue_id: string
  website_id: string
  session_id: string
  event_id: string
  event_name: string
  currency: string
  revenue: string | null
  created_at: Generated<Timestamp | null>
}

export interface Link {
  link_id: string
  name: string
  url: string
  slug: string
  user_id: string | null
  team_id: string | null
  created_at: Generated<Timestamp | null>
  updated_at: ColumnType<Date | null, Date | string, Date | string | null>
  deleted_at: Timestamp | null
}

export interface Pixel {
  pixel_id: string
  name: string
  slug: string
  user_id: string | null
  team_id: string | null
  created_at: Generated<Timestamp | null>
  updated_at: ColumnType<Date | null, Date | string, Date | string | null>
  deleted_at: Timestamp | null
}

export interface Board {
  board_id: string
  type: string
  name: string
  description: string
  parameters: unknown
  user_id: string | null
  team_id: string | null
  created_at: Generated<Timestamp | null>
  updated_at: ColumnType<Date | null, Date | string, Date | string | null>
}

export interface Share {
  share_id: string
  entity_id: string
  name: string
  share_type: number
  slug: string
  parameters: unknown
  created_at: Generated<Timestamp | null>
  updated_at: ColumnType<Date | null, Date | string, Date | string | null>
}

export interface SessionReplay {
  replay_id: string
  website_id: string
  session_id: string
  visit_id: string
  chunk_index: number
  events: Buffer
  event_count: number
  started_at: Timestamp
  ended_at: Timestamp
  created_at: Generated<Timestamp | null>
}

export interface SessionReplaySaved {
  saved_replay_id: string
  name: string
  website_id: string
  visit_id: string
  created_at: Generated<Timestamp | null>
  updated_at: ColumnType<Date | null, Date | string, Date | string | null>
}

export interface HeatmapEvent {
  heatmap_event_id: string
  website_id: string
  session_id: string
  visit_id: string
  url_path: string
  event_type: number
  x: number | null
  y: number | null
  page_x: number | null
  page_y: number | null
  page_w: number | null
  viewport_w: number | null
  viewport_h: number | null
  page_h: number | null
  scroll_pct: number | null
  created_at: Generated<Timestamp>
}

export interface TwoFactorAuth {
  id: string
  user_id: string
  secret: string
  is_enabled: Generated<boolean>
  created_at: Generated<Timestamp>
  updated_at: Timestamp
}

export interface TwoFactorBackupCode {
  id: string
  user_id: string
  code_hash: string
  used: Generated<boolean>
  created_at: Generated<Timestamp>
}

export interface TwoFactorOtpUsed {
  id: string
  user_id: string
  otp: string
  expires_at: Timestamp
}

export interface TwoFactorRateLimit {
  id: string
  user_id: string
  attempts: Generated<number>
  locked_until: Timestamp | null
  updated_at: Timestamp
}

export interface AppSetting {
  key: string
  value: string
}

export interface DB {
  user: User
  api_key: ApiKey
  session: Session
  session_link: SessionLink
  website: Website
  website_event: WebsiteEvent
  event_data: EventData
  session_data: SessionData
  team: Team
  team_user: TeamUser
  report: Report
  annotation: Annotation
  segment: Segment
  revenue: Revenue
  link: Link
  pixel: Pixel
  board: Board
  share: Share
  session_replay: SessionReplay
  session_replay_saved: SessionReplaySaved
  heatmap_event: HeatmapEvent
  two_factor_auth: TwoFactorAuth
  two_factor_backup_code: TwoFactorBackupCode
  two_factor_otp_used: TwoFactorOtpUsed
  two_factor_rate_limit: TwoFactorRateLimit
  app_setting: AppSetting
}
