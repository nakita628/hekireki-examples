import {
  boolean,
  doublePrecision,
  foreignKey,
  integer,
  pgTable,
  primaryKey,
  serial,
  text,
  timestamp,
  uniqueIndex,
} from 'drizzle-orm/pg-core'
import { defineRelations, sql } from 'drizzle-orm'

const utcNow = (() => {
  let now: Date | undefined
  return () => {
    if (now === undefined) {
      now = new Date()
      queueMicrotask(() => {
        now = undefined
      })
    }
    return now
  }
})()

export const user = pgTable(
  'User',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    email: text('email'),
    username: text('username'),
    isAdmin: boolean('isAdmin').notNull().default(false),
    paymentProcessorUserId: text('paymentProcessorUserId'),
    subscriptionStatus: text('subscriptionStatus'),
    subscriptionPlan: text('subscriptionPlan'),
    datePaid: timestamp('datePaid', { precision: 3 }),
    credits: integer('credits').notNull().default(3),
  },
  (table) => [
    uniqueIndex('User_email_key').on(table.email),
    uniqueIndex('User_username_key').on(table.username),
    uniqueIndex('User_paymentProcessorUserId_key').on(table.paymentProcessorUserId),
  ],
)

export const processedWebhookEvent = pgTable(
  'ProcessedWebhookEvent',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    paymentProcessorEventId: text('paymentProcessorEventId').notNull(),
    eventType: text('eventType').notNull(),
  },
  (table) => [
    uniqueIndex('ProcessedWebhookEvent_paymentProcessorEventId_key').on(
      table.paymentProcessorEventId,
    ),
  ],
)

export const gptResponse = pgTable(
  'GptResponse',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
    userId: text('userId').notNull(),
    content: text('content').notNull(),
  },
  (table) => [
    foreignKey({
      name: 'GptResponse_userId_fkey',
      columns: [table.userId],
      foreignColumns: [user.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const task = pgTable(
  'Task',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    userId: text('userId').notNull(),
    description: text('description').notNull(),
    time: text('time').notNull().default('1'),
    isDone: boolean('isDone').notNull().default(false),
  },
  (table) => [
    foreignKey({ name: 'Task_userId_fkey', columns: [table.userId], foreignColumns: [user.id] })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const file = pgTable(
  'File',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    userId: text('userId').notNull(),
    name: text('name').notNull(),
    type: text('type').notNull(),
    s3Key: text('s3Key').notNull(),
  },
  (table) => [
    foreignKey({ name: 'File_userId_fkey', columns: [table.userId], foreignColumns: [user.id] })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const dailyStats = pgTable(
  'DailyStats',
  {
    id: serial('id').primaryKey(),
    date: timestamp('date', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    totalViews: integer('totalViews').notNull().default(0),
    prevDayViewsChangePercent: text('prevDayViewsChangePercent').notNull().default('0'),
    userCount: integer('userCount').notNull().default(0),
    paidUserCount: integer('paidUserCount').notNull().default(0),
    userDelta: integer('userDelta').notNull().default(0),
    paidUserDelta: integer('paidUserDelta').notNull().default(0),
    totalRevenue: doublePrecision('totalRevenue').notNull().default(0),
    totalProfit: doublePrecision('totalProfit').notNull().default(0),
  },
  (table) => [uniqueIndex('DailyStats_date_key').on(table.date)],
)

export const pageViewSource = pgTable(
  'PageViewSource',
  {
    name: text('name').notNull(),
    date: timestamp('date', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    dailyStatsId: integer('dailyStatsId'),
    visitors: integer('visitors').notNull(),
  },
  (table) => [
    primaryKey({ name: 'PageViewSource_pkey', columns: [table.date, table.name] }),
    foreignKey({
      name: 'PageViewSource_dailyStatsId_fkey',
      columns: [table.dailyStatsId],
      foreignColumns: [dailyStats.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const logs = pgTable('Logs', {
  id: serial('id').primaryKey(),
  createdAt: timestamp('createdAt', { precision: 3 })
    .notNull()
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow),
  message: text('message').notNull(),
  level: text('level').notNull(),
})

export const contactFormMessage = pgTable(
  'ContactFormMessage',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    userId: text('userId').notNull(),
    content: text('content').notNull(),
    isRead: boolean('isRead').notNull().default(false),
    repliedAt: timestamp('repliedAt', { precision: 3 }),
  },
  (table) => [
    foreignKey({
      name: 'ContactFormMessage_userId_fkey',
      columns: [table.userId],
      foreignColumns: [user.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const relations = defineRelations(
  {
    user,
    processedWebhookEvent,
    gptResponse,
    task,
    file,
    dailyStats,
    pageViewSource,
    logs,
    contactFormMessage,
  },
  (r) => ({
    user: {
      gptResponses: r.many.gptResponse(),
      contactFormMessages: r.many.contactFormMessage(),
      tasks: r.many.task(),
      files: r.many.file(),
    },
    gptResponse: {
      user: r.one.user({ from: r.gptResponse.userId, to: r.user.id, optional: false }),
    },
    task: { user: r.one.user({ from: r.task.userId, to: r.user.id, optional: false }) },
    file: { user: r.one.user({ from: r.file.userId, to: r.user.id, optional: false }) },
    dailyStats: { sources: r.many.pageViewSource() },
    pageViewSource: {
      dailyStats: r.one.dailyStats({ from: r.pageViewSource.dailyStatsId, to: r.dailyStats.id }),
    },
    contactFormMessage: {
      user: r.one.user({ from: r.contactFormMessage.userId, to: r.user.id, optional: false }),
    },
  }),
)
