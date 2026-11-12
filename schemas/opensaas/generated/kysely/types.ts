import type { ColumnType } from 'kysely'

export type Generated<T> =
  T extends ColumnType<infer S, infer I, infer U>
    ? ColumnType<S, I | undefined, U>
    : ColumnType<T, T | undefined, T>

export type Timestamp = ColumnType<Date, Date | string, Date | string>

export interface User {
  id: string
  createdAt: Generated<Timestamp>
  email: string | null
  username: string | null
  isAdmin: Generated<boolean>
  paymentProcessorUserId: string | null
  subscriptionStatus: string | null
  subscriptionPlan: string | null
  currentPeriodEnd: Timestamp | null
  cancelAtPeriodEnd: Generated<boolean>
  datePaid: Timestamp | null
  credits: Generated<number>
}

export interface ProcessedWebhookEvent {
  id: string
  createdAt: Generated<Timestamp>
  paymentProcessorEventId: string
  eventType: string
}

export interface GptResponse {
  id: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
  userId: string
  content: string
}

export interface Task {
  id: string
  createdAt: Generated<Timestamp>
  userId: string
  description: string
  time: Generated<string>
  isDone: Generated<boolean>
}

export interface File {
  id: string
  createdAt: Generated<Timestamp>
  userId: string
  name: string
  type: string
  s3Key: string
}

export interface DailyStats {
  id: Generated<number>
  date: Generated<Timestamp>
  totalViews: Generated<number>
  prevDayViewsChangePercent: Generated<string>
  userCount: Generated<number>
  paidUserCount: Generated<number>
  userDelta: Generated<number>
  paidUserDelta: Generated<number>
  totalRevenue: Generated<number>
  totalProfit: Generated<number>
}

export interface PageViewSource {
  name: string
  date: Generated<Timestamp>
  dailyStatsId: number | null
  visitors: number
}

export interface Logs {
  id: Generated<number>
  createdAt: Generated<Timestamp>
  message: string
  level: string
}

export interface ContactFormMessage {
  id: string
  createdAt: Generated<Timestamp>
  userId: string
  content: string
  isRead: Generated<boolean>
  repliedAt: Timestamp | null
}

export interface DB {
  User: User
  ProcessedWebhookEvent: ProcessedWebhookEvent
  GptResponse: GptResponse
  Task: Task
  File: File
  DailyStats: DailyStats
  PageViewSource: PageViewSource
  Logs: Logs
  ContactFormMessage: ContactFormMessage
}
