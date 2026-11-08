import {
  boolean,
  foreignKey,
  index,
  integer,
  jsonb,
  numeric,
  pgEnum,
  pgTable,
  text,
  timestamp,
  uniqueIndex,
} from 'drizzle-orm/pg-core'
import { defineRelations, sql } from 'drizzle-orm'
import cuid from 'cuid'

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

export const userRoleEnum = pgEnum('UserRole', ['admin', 'user'])

export const exerciseAttributeNameEnumEnum = pgEnum('ExerciseAttributeNameEnum', [
  'TYPE',
  'PRIMARY_MUSCLE',
  'SECONDARY_MUSCLE',
  'EQUIPMENT',
  'MECHANICS_TYPE',
])

export const exerciseAttributeValueEnumEnum = pgEnum('ExerciseAttributeValueEnum', [
  'BODYWEIGHT',
  'STRENGTH',
  'POWERLIFTING',
  'CALISTHENIC',
  'PLYOMETRICS',
  'STRETCHING',
  'STRONGMAN',
  'CARDIO',
  'STABILIZATION',
  'POWER',
  'RESISTANCE',
  'CROSSFIT',
  'WEIGHTLIFTING',
  'BICEPS',
  'SHOULDERS',
  'CHEST',
  'BACK',
  'GLUTES',
  'TRICEPS',
  'HAMSTRINGS',
  'QUADRICEPS',
  'FOREARMS',
  'CALVES',
  'TRAPS',
  'ABDOMINALS',
  'NECK',
  'LATS',
  'ADDUCTORS',
  'ABDUCTORS',
  'OBLIQUES',
  'GROIN',
  'FULL_BODY',
  'ROTATOR_CUFF',
  'HIP_FLEXOR',
  'ACHILLES_TENDON',
  'FINGERS',
  'DUMBBELL',
  'KETTLEBELLS',
  'BARBELL',
  'SMITH_MACHINE',
  'BODY_ONLY',
  'OTHER',
  'BANDS',
  'EZ_BAR',
  'MACHINE',
  'DESK',
  'PULLUP_BAR',
  'NONE',
  'CABLE',
  'MEDICINE_BALL',
  'SWISS_BALL',
  'FOAM_ROLL',
  'WEIGHT_PLATE',
  'TRX',
  'BOX',
  'ROPES',
  'SPIN_BIKE',
  'STEP',
  'BOSU',
  'TYRE',
  'SANDBAG',
  'POLE',
  'BENCH',
  'WALL',
  'BAR',
  'RACK',
  'CAR',
  'SLED',
  'CHAIN',
  'SKIERG',
  'ROPE',
  'NA',
  'ISOLATION',
  'COMPOUND',
])

export const workoutSetTypeEnum = pgEnum('WorkoutSetType', [
  'TIME',
  'WEIGHT',
  'REPS',
  'BODYWEIGHT',
  'NA',
])

export const workoutSetUnitEnum = pgEnum('WorkoutSetUnit', ['kg', 'lbs'])

export const subscriptionStatusEnum = pgEnum('SubscriptionStatus', [
  'ACTIVE',
  'TRIAL',
  'CANCELLED',
  'EXPIRED',
  'PAUSED',
])

export const platformEnum = pgEnum('Platform', ['WEB', 'IOS', 'ANDROID'])

export const paymentProcessorEnum = pgEnum('PaymentProcessor', [
  'STRIPE',
  'PAYPAL',
  'LEMONSQUEEZY',
  'PADDLE',
  'APPLE_PAY',
  'GOOGLE_PAY',
  'REVENUECAT',
  'NONE',
  'OTHER',
])

export const programLevelEnum = pgEnum('ProgramLevel', [
  'BEGINNER',
  'INTERMEDIATE',
  'ADVANCED',
  'EXPERT',
])

export const programVisibilityEnum = pgEnum('ProgramVisibility', ['DRAFT', 'PUBLISHED', 'ARCHIVED'])

export const user = pgTable(
  'user',
  {
    id: text('id').primaryKey(),
    firstName: text('firstName').notNull().default(''),
    lastName: text('lastName').notNull().default(''),
    name: text('name').notNull(),
    email: text('email').notNull(),
    emailVerified: boolean('emailVerified').notNull(),
    image: text('image'),
    locale: text('locale').default('fr'),
    createdAt: timestamp('createdAt', { precision: 3 }).notNull(),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull(),
    role: userRoleEnum('role').default('user'),
    banned: boolean('banned').default(false),
    banReason: text('banReason'),
    banExpires: timestamp('banExpires', { precision: 3 }),
    isPremium: boolean('isPremium').default(false),
    onboardingPreferences: jsonb('onboardingPreferences'),
  },
  (table) => [uniqueIndex('user_email_key').on(table.email)],
)

export const userFavoriteExercises = pgTable(
  'user_favorite_exercises',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    userId: text('userId').notNull(),
    exerciseId: text('exerciseId').notNull(),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    uniqueIndex('user_favorite_exercises_userId_exerciseId_key').on(table.userId, table.exerciseId),
    foreignKey({
      name: 'user_favorite_exercises_userId_fkey',
      columns: [table.userId],
      foreignColumns: [user.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'user_favorite_exercises_exerciseId_fkey',
      columns: [table.exerciseId],
      foreignColumns: [exercises.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const session = pgTable(
  'session',
  {
    id: text('id').primaryKey(),
    expiresAt: timestamp('expiresAt', { precision: 3 }).notNull(),
    token: text('token').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 }).notNull(),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull(),
    ipAddress: text('ipAddress'),
    userAgent: text('userAgent'),
    userId: text('userId').notNull(),
    impersonatedBy: text('impersonatedBy'),
  },
  (table) => [
    uniqueIndex('session_token_key').on(table.token),
    foreignKey({ name: 'session_userId_fkey', columns: [table.userId], foreignColumns: [user.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const account = pgTable(
  'account',
  {
    id: text('id').primaryKey(),
    accountId: text('accountId').notNull(),
    providerId: text('providerId').notNull(),
    userId: text('userId').notNull(),
    accessToken: text('accessToken'),
    refreshToken: text('refreshToken'),
    idToken: text('idToken'),
    accessTokenExpiresAt: timestamp('accessTokenExpiresAt', { precision: 3 }),
    refreshTokenExpiresAt: timestamp('refreshTokenExpiresAt', { precision: 3 }),
    scope: text('scope'),
    password: text('password'),
    createdAt: timestamp('createdAt', { precision: 3 }).notNull(),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull(),
  },
  (table) => [
    foreignKey({ name: 'account_userId_fkey', columns: [table.userId], foreignColumns: [user.id] })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const verification = pgTable('verification', {
  id: text('id').primaryKey(),
  identifier: text('identifier').notNull(),
  value: text('value').notNull(),
  expiresAt: timestamp('expiresAt', { precision: 3 }).notNull(),
  createdAt: timestamp('createdAt', { precision: 3 }),
  updatedAt: timestamp('updatedAt', { precision: 3 }),
})

export const feedbacks = pgTable(
  'feedbacks',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    review: integer('review').notNull(),
    message: text('message').notNull(),
    email: text('email'),
    userId: text('userId'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    foreignKey({
      name: 'feedbacks_userId_fkey',
      columns: [table.userId],
      foreignColumns: [user.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const exercises = pgTable(
  'exercises',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    name: text('name').notNull(),
    nameEn: text('nameEn'),
    description: text('description'),
    descriptionEn: text('descriptionEn'),
    fullVideoUrl: text('fullVideoUrl'),
    fullVideoImageUrl: text('fullVideoImageUrl'),
    introduction: text('introduction'),
    introductionEn: text('introductionEn'),
    slug: text('slug'),
    slugEn: text('slugEn'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    uniqueIndex('exercises_slug_key').on(table.slug),
    uniqueIndex('exercises_slugEn_key').on(table.slugEn),
  ],
)

export const exerciseAttributeNames = pgTable(
  'exercise_attribute_names',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    name: exerciseAttributeNameEnumEnum('name').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [uniqueIndex('exercise_attribute_names_name_key').on(table.name)],
)

export const exerciseAttributeValues = pgTable(
  'exercise_attribute_values',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    attributeNameId: text('attributeNameId').notNull(),
    value: exerciseAttributeValueEnumEnum('value').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    uniqueIndex('exercise_attribute_values_attributeNameId_value_key').on(
      table.attributeNameId,
      table.value,
    ),
    foreignKey({
      name: 'exercise_attribute_values_attributeNameId_fkey',
      columns: [table.attributeNameId],
      foreignColumns: [exerciseAttributeNames.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const exerciseAttributes = pgTable(
  'exercise_attributes',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    exerciseId: text('exerciseId').notNull(),
    attributeNameId: text('attributeNameId').notNull(),
    attributeValueId: text('attributeValueId').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    uniqueIndex('exercise_attributes_exerciseId_attributeNameId_attributeVal_key').on(
      table.exerciseId,
      table.attributeNameId,
      table.attributeValueId,
    ),
    foreignKey({
      name: 'exercise_attributes_exerciseId_fkey',
      columns: [table.exerciseId],
      foreignColumns: [exercises.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'exercise_attributes_attributeNameId_fkey',
      columns: [table.attributeNameId],
      foreignColumns: [exerciseAttributeNames.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({
      name: 'exercise_attributes_attributeValueId_fkey',
      columns: [table.attributeValueId],
      foreignColumns: [exerciseAttributeValues.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const workoutSessions = pgTable(
  'workout_sessions',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    userId: text('userId').notNull(),
    startedAt: timestamp('startedAt', { precision: 3 }).notNull(),
    endedAt: timestamp('endedAt', { precision: 3 }),
    duration: integer('duration'),
    muscles: exerciseAttributeValueEnumEnum('muscles').array().default([]),
    rating: integer('rating'),
    ratingComment: text('ratingComment'),
  },
  (table) => [
    foreignKey({
      name: 'workout_sessions_userId_fkey',
      columns: [table.userId],
      foreignColumns: [user.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const workoutSessionExercises = pgTable(
  'workout_session_exercises',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    workoutSessionId: text('workoutSessionId').notNull(),
    exerciseId: text('exerciseId').notNull(),
    order: integer('order').notNull(),
  },
  (table) => [
    foreignKey({
      name: 'workout_session_exercises_workoutSessionId_fkey',
      columns: [table.workoutSessionId],
      foreignColumns: [workoutSessions.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'workout_session_exercises_exerciseId_fkey',
      columns: [table.exerciseId],
      foreignColumns: [exercises.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const workoutSets = pgTable(
  'workout_sets',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    workoutSessionExerciseId: text('workoutSessionExerciseId').notNull(),
    setIndex: integer('setIndex').notNull(),
    type: workoutSetTypeEnum('type').notNull(),
    types: workoutSetTypeEnum('types').array().default([]),
    valuesInt: integer('valuesInt').array().default([]),
    valuesSec: integer('valuesSec').array().default([]),
    units: workoutSetUnitEnum('units').array().default([]),
    completed: boolean('completed').notNull().default(false),
  },
  (table) => [
    foreignKey({
      name: 'workout_sets_workoutSessionExerciseId_fkey',
      columns: [table.workoutSessionExerciseId],
      foreignColumns: [workoutSessionExercises.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const subscriptionPlans = pgTable('subscription_plans', {
  id: text('id')
    .primaryKey()
    .$defaultFn(() => cuid()),
  priceMonthly: numeric('priceMonthly', { precision: 10, scale: 2 }),
  priceYearly: numeric('priceYearly', { precision: 10, scale: 2 }),
  currency: text('currency').notNull().default('EUR'),
  interval: text('interval').notNull().default('month'),
  isActive: boolean('isActive').notNull().default(true),
  availableRegions: text('availableRegions').array().default([]),
  createdAt: timestamp('createdAt', { precision: 3 })
    .notNull()
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow),
  updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
})

export const planProviderMappings = pgTable(
  'plan_provider_mappings',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    planId: text('planId').notNull(),
    provider: paymentProcessorEnum('provider').notNull(),
    externalId: text('externalId').notNull(),
    region: text('region'),
    metadata: jsonb('metadata'),
    isActive: boolean('isActive').notNull().default(true),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('plan_provider_mappings_provider_externalId_idx').on(table.provider, table.externalId),
    uniqueIndex('plan_provider_mappings_planId_provider_region_key').on(
      table.planId,
      table.provider,
      table.region,
    ),
    foreignKey({
      name: 'plan_provider_mappings_planId_fkey',
      columns: [table.planId],
      foreignColumns: [subscriptionPlans.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const subscriptions = pgTable(
  'subscriptions',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    userId: text('userId').notNull(),
    planId: text('planId').notNull(),
    revenueCatUserId: text('revenueCatUserId'),
    status: subscriptionStatusEnum('status').notNull(),
    startedAt: timestamp('startedAt', { precision: 3 }).notNull(),
    currentPeriodEnd: timestamp('currentPeriodEnd', { precision: 3 }),
    cancelledAt: timestamp('cancelledAt', { precision: 3 }),
    platform: platformEnum('platform'),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    uniqueIndex('subscriptions_userId_platform_key').on(table.userId, table.platform),
    foreignKey({
      name: 'subscriptions_userId_fkey',
      columns: [table.userId],
      foreignColumns: [user.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({
      name: 'subscriptions_planId_fkey',
      columns: [table.planId],
      foreignColumns: [subscriptionPlans.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const licenses = pgTable(
  'licenses',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    key: text('key').notNull(),
    userId: text('userId'),
    validFrom: timestamp('validFrom', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    validUntil: timestamp('validUntil', { precision: 3 }),
    maxUsers: integer('maxUsers').default(1),
    features: jsonb('features'),
    activatedAt: timestamp('activatedAt', { precision: 3 }),
    lastCheckedAt: timestamp('lastCheckedAt', { precision: 3 }),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    uniqueIndex('licenses_key_key').on(table.key),
    foreignKey({ name: 'licenses_userId_fkey', columns: [table.userId], foreignColumns: [user.id] })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const revenuecatWebhookEvents = pgTable(
  'revenuecat_webhook_events',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    eventType: text('eventType').notNull(),
    eventTimestamp: timestamp('eventTimestamp', { precision: 3 }).notNull(),
    appUserId: text('appUserId').notNull(),
    environment: text('environment').notNull(),
    productId: text('productId'),
    transactionId: text('transactionId'),
    originalTransactionId: text('originalTransactionId'),
    entitlementIds: text('entitlementIds'),
    processed: boolean('processed').notNull().default(false),
    processingError: text('processingError'),
    retryCount: integer('retryCount').notNull().default(0),
    rawEventData: jsonb('rawEventData').notNull(),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('revenuecat_webhook_events_appUserId_idx').on(table.appUserId),
    index('revenuecat_webhook_events_eventType_idx').on(table.eventType),
    index('revenuecat_webhook_events_processed_idx').on(table.processed),
    index('revenuecat_webhook_events_eventTimestamp_idx').on(table.eventTimestamp),
  ],
)

export const programs = pgTable(
  'programs',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    slug: text('slug').notNull(),
    slugEn: text('slugEn').notNull(),
    slugEs: text('slugEs').notNull(),
    slugPt: text('slugPt').notNull(),
    slugRu: text('slugRu').notNull(),
    slugZhCn: text('slugZhCn').notNull(),
    title: text('title').notNull(),
    titleEn: text('titleEn').notNull(),
    titleEs: text('titleEs').notNull(),
    titlePt: text('titlePt').notNull(),
    titleRu: text('titleRu').notNull(),
    titleZhCn: text('titleZhCn').notNull(),
    description: text('description').notNull(),
    descriptionEn: text('descriptionEn').notNull(),
    descriptionEs: text('descriptionEs').notNull(),
    descriptionPt: text('descriptionPt').notNull(),
    descriptionRu: text('descriptionRu').notNull(),
    descriptionZhCn: text('descriptionZhCn').notNull(),
    category: text('category').notNull(),
    image: text('image').notNull(),
    level: programLevelEnum('level').notNull(),
    type: exerciseAttributeValueEnumEnum('type').notNull(),
    durationWeeks: integer('durationWeeks').notNull().default(4),
    sessionsPerWeek: integer('sessionsPerWeek').notNull().default(3),
    sessionDurationMin: integer('sessionDurationMin').notNull().default(30),
    equipment: exerciseAttributeValueEnumEnum('equipment').array().default([]),
    isPremium: boolean('isPremium').notNull().default(true),
    isActive: boolean('isActive').notNull().default(true),
    visibility: programVisibilityEnum('visibility').notNull().default('DRAFT'),
    participantCount: integer('participantCount').notNull().default(0),
    createdAt: timestamp('createdAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updatedAt', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    uniqueIndex('programs_slug_key').on(table.slug),
    uniqueIndex('programs_slugEn_key').on(table.slugEn),
    uniqueIndex('programs_slugEs_key').on(table.slugEs),
    uniqueIndex('programs_slugPt_key').on(table.slugPt),
    uniqueIndex('programs_slugRu_key').on(table.slugRu),
    uniqueIndex('programs_slugZhCn_key').on(table.slugZhCn),
  ],
)

export const programCoaches = pgTable(
  'program_coaches',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    programId: text('programId').notNull(),
    name: text('name').notNull(),
    image: text('image').notNull(),
    order: integer('order').notNull().default(0),
  },
  (table) => [
    foreignKey({
      name: 'program_coaches_programId_fkey',
      columns: [table.programId],
      foreignColumns: [programs.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const programWeeks = pgTable(
  'program_weeks',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    programId: text('programId').notNull(),
    weekNumber: integer('weekNumber').notNull(),
    title: text('title').notNull(),
    titleEn: text('titleEn').notNull(),
    titleEs: text('titleEs').notNull(),
    titlePt: text('titlePt').notNull(),
    titleRu: text('titleRu').notNull(),
    titleZhCn: text('titleZhCn').notNull(),
    description: text('description').notNull(),
    descriptionEn: text('descriptionEn').notNull(),
    descriptionEs: text('descriptionEs').notNull(),
    descriptionPt: text('descriptionPt').notNull(),
    descriptionRu: text('descriptionRu').notNull(),
    descriptionZhCn: text('descriptionZhCn').notNull(),
  },
  (table) => [
    uniqueIndex('program_weeks_programId_weekNumber_key').on(table.programId, table.weekNumber),
    foreignKey({
      name: 'program_weeks_programId_fkey',
      columns: [table.programId],
      foreignColumns: [programs.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const programSessions = pgTable(
  'program_sessions',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    weekId: text('weekId').notNull(),
    sessionNumber: integer('sessionNumber').notNull(),
    title: text('title').notNull(),
    titleEn: text('titleEn').notNull(),
    titleEs: text('titleEs').notNull(),
    titlePt: text('titlePt').notNull(),
    titleRu: text('titleRu').notNull(),
    titleZhCn: text('titleZhCn').notNull(),
    slug: text('slug').notNull(),
    slugEn: text('slugEn').notNull(),
    slugEs: text('slugEs').notNull(),
    slugPt: text('slugPt').notNull(),
    slugRu: text('slugRu').notNull(),
    slugZhCn: text('slugZhCn').notNull(),
    description: text('description').notNull(),
    descriptionEn: text('descriptionEn').notNull(),
    descriptionEs: text('descriptionEs').notNull(),
    descriptionPt: text('descriptionPt').notNull(),
    descriptionRu: text('descriptionRu').notNull(),
    descriptionZhCn: text('descriptionZhCn').notNull(),
    equipment: exerciseAttributeValueEnumEnum('equipment').array().default([]),
    estimatedMinutes: integer('estimatedMinutes').notNull(),
    isPremium: boolean('isPremium').notNull().default(true),
  },
  (table) => [
    uniqueIndex('program_sessions_weekId_sessionNumber_key').on(table.weekId, table.sessionNumber),
    uniqueIndex('program_sessions_weekId_slug_key').on(table.weekId, table.slug),
    uniqueIndex('program_sessions_weekId_slugEn_key').on(table.weekId, table.slugEn),
    uniqueIndex('program_sessions_weekId_slugEs_key').on(table.weekId, table.slugEs),
    uniqueIndex('program_sessions_weekId_slugPt_key').on(table.weekId, table.slugPt),
    uniqueIndex('program_sessions_weekId_slugRu_key').on(table.weekId, table.slugRu),
    uniqueIndex('program_sessions_weekId_slugZhCn_key').on(table.weekId, table.slugZhCn),
    foreignKey({
      name: 'program_sessions_weekId_fkey',
      columns: [table.weekId],
      foreignColumns: [programWeeks.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const programSessionExercises = pgTable(
  'program_session_exercises',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    sessionId: text('sessionId').notNull(),
    exerciseId: text('exerciseId').notNull(),
    order: integer('order').notNull(),
    instructions: text('instructions').notNull(),
    instructionsEn: text('instructionsEn').notNull(),
    instructionsEs: text('instructionsEs').notNull(),
    instructionsPt: text('instructionsPt').notNull(),
    instructionsRu: text('instructionsRu').notNull(),
    instructionsZhCn: text('instructionsZhCn').notNull(),
  },
  (table) => [
    uniqueIndex('program_session_exercises_sessionId_order_key').on(table.sessionId, table.order),
    foreignKey({
      name: 'program_session_exercises_sessionId_fkey',
      columns: [table.sessionId],
      foreignColumns: [programSessions.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'program_session_exercises_exerciseId_fkey',
      columns: [table.exerciseId],
      foreignColumns: [exercises.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const programSuggestedSets = pgTable(
  'program_suggested_sets',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    programSessionExerciseId: text('programSessionExerciseId').notNull(),
    setIndex: integer('setIndex').notNull(),
    types: workoutSetTypeEnum('types').array().default([]),
    valuesInt: integer('valuesInt').array().default([]),
    valuesSec: integer('valuesSec').array().default([]),
    units: workoutSetUnitEnum('units').array().default([]),
  },
  (table) => [
    uniqueIndex('program_suggested_sets_programSessionExerciseId_setIndex_key').on(
      table.programSessionExerciseId,
      table.setIndex,
    ),
    foreignKey({
      name: 'program_suggested_sets_programSessionExerciseId_fkey',
      columns: [table.programSessionExerciseId],
      foreignColumns: [programSessionExercises.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const userProgramEnrollments = pgTable(
  'user_program_enrollments',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    userId: text('userId').notNull(),
    programId: text('programId').notNull(),
    enrolledAt: timestamp('enrolledAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    currentWeek: integer('currentWeek').notNull().default(1),
    currentSession: integer('currentSession').notNull().default(1),
    completedSessions: integer('completedSessions').notNull().default(0),
    isActive: boolean('isActive').notNull().default(true),
    completedAt: timestamp('completedAt', { precision: 3 }),
  },
  (table) => [
    uniqueIndex('user_program_enrollments_userId_programId_key').on(table.userId, table.programId),
    foreignKey({
      name: 'user_program_enrollments_userId_fkey',
      columns: [table.userId],
      foreignColumns: [user.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'user_program_enrollments_programId_fkey',
      columns: [table.programId],
      foreignColumns: [programs.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const userSessionProgress = pgTable(
  'user_session_progress',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    enrollmentId: text('enrollmentId').notNull(),
    sessionId: text('sessionId').notNull(),
    startedAt: timestamp('startedAt', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    completedAt: timestamp('completedAt', { precision: 3 }),
    workoutSessionId: text('workoutSessionId'),
  },
  (table) => [
    uniqueIndex('user_session_progress_workoutSessionId_key').on(table.workoutSessionId),
    uniqueIndex('user_session_progress_enrollmentId_sessionId_key').on(
      table.enrollmentId,
      table.sessionId,
    ),
    foreignKey({
      name: 'user_session_progress_enrollmentId_fkey',
      columns: [table.enrollmentId],
      foreignColumns: [userProgramEnrollments.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'user_session_progress_sessionId_fkey',
      columns: [table.sessionId],
      foreignColumns: [programSessions.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({
      name: 'user_session_progress_workoutSessionId_fkey',
      columns: [table.workoutSessionId],
      foreignColumns: [workoutSessions.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const relations = defineRelations(
  {
    user,
    userFavoriteExercises,
    session,
    account,
    verification,
    feedbacks,
    exercises,
    exerciseAttributeNames,
    exerciseAttributeValues,
    exerciseAttributes,
    workoutSessions,
    workoutSessionExercises,
    workoutSets,
    subscriptionPlans,
    planProviderMappings,
    subscriptions,
    licenses,
    revenuecatWebhookEvents,
    programs,
    programCoaches,
    programWeeks,
    programSessions,
    programSessionExercises,
    programSuggestedSets,
    userProgramEnrollments,
    userSessionProgress,
  },
  (r) => ({
    user: {
      sessions: r.many.session(),
      accounts: r.many.account(),
      feedbacks: r.many.feedbacks(),
      WorkoutSession: r.many.workoutSessions(),
      FavoriteExercises: r.many.userFavoriteExercises(),
      subscriptions: r.many.subscriptions(),
      licenses: r.many.licenses(),
      programEnrollments: r.many.userProgramEnrollments(),
    },
    userFavoriteExercises: {
      user: r.one.user({ from: r.userFavoriteExercises.userId, to: r.user.id, optional: false }),
      exercise: r.one.exercises({
        from: r.userFavoriteExercises.exerciseId,
        to: r.exercises.id,
        optional: false,
      }),
    },
    session: { user: r.one.user({ from: r.session.userId, to: r.user.id, optional: false }) },
    account: { user: r.one.user({ from: r.account.userId, to: r.user.id, optional: false }) },
    feedbacks: { user: r.one.user({ from: r.feedbacks.userId, to: r.user.id }) },
    exercises: {
      attributes: r.many.exerciseAttributes(),
      WorkoutSessionExercise: r.many.workoutSessionExercises(),
      ProgramSessionExercise: r.many.programSessionExercises(),
      favoritesByUsers: r.many.userFavoriteExercises(),
    },
    exerciseAttributeNames: {
      values: r.many.exerciseAttributeValues(),
      attributes: r.many.exerciseAttributes(),
    },
    exerciseAttributeValues: {
      attributeName: r.one.exerciseAttributeNames({
        from: r.exerciseAttributeValues.attributeNameId,
        to: r.exerciseAttributeNames.id,
        optional: false,
      }),
      attributes: r.many.exerciseAttributes(),
    },
    exerciseAttributes: {
      exercise: r.one.exercises({
        from: r.exerciseAttributes.exerciseId,
        to: r.exercises.id,
        optional: false,
      }),
      attributeName: r.one.exerciseAttributeNames({
        from: r.exerciseAttributes.attributeNameId,
        to: r.exerciseAttributeNames.id,
        optional: false,
      }),
      attributeValue: r.one.exerciseAttributeValues({
        from: r.exerciseAttributes.attributeValueId,
        to: r.exerciseAttributeValues.id,
        optional: false,
      }),
    },
    workoutSessions: {
      user: r.one.user({ from: r.workoutSessions.userId, to: r.user.id, optional: false }),
      exercises: r.many.workoutSessionExercises(),
      userSessionProgress: r.one.userSessionProgress({
        from: r.workoutSessions.id,
        to: r.userSessionProgress.workoutSessionId,
      }),
    },
    workoutSessionExercises: {
      workoutSession: r.one.workoutSessions({
        from: r.workoutSessionExercises.workoutSessionId,
        to: r.workoutSessions.id,
        optional: false,
      }),
      exercise: r.one.exercises({
        from: r.workoutSessionExercises.exerciseId,
        to: r.exercises.id,
        optional: false,
      }),
      sets: r.many.workoutSets(),
    },
    workoutSets: {
      workoutSessionExercise: r.one.workoutSessionExercises({
        from: r.workoutSets.workoutSessionExerciseId,
        to: r.workoutSessionExercises.id,
        optional: false,
      }),
    },
    subscriptionPlans: {
      subscriptions: r.many.subscriptions(),
      providerMappings: r.many.planProviderMappings(),
    },
    planProviderMappings: {
      plan: r.one.subscriptionPlans({
        from: r.planProviderMappings.planId,
        to: r.subscriptionPlans.id,
        optional: false,
      }),
    },
    subscriptions: {
      user: r.one.user({ from: r.subscriptions.userId, to: r.user.id, optional: false }),
      plan: r.one.subscriptionPlans({
        from: r.subscriptions.planId,
        to: r.subscriptionPlans.id,
        optional: false,
      }),
    },
    licenses: { user: r.one.user({ from: r.licenses.userId, to: r.user.id }) },
    programs: {
      weeks: r.many.programWeeks(),
      enrollments: r.many.userProgramEnrollments(),
      coaches: r.many.programCoaches(),
    },
    programCoaches: {
      program: r.one.programs({
        from: r.programCoaches.programId,
        to: r.programs.id,
        optional: false,
      }),
    },
    programWeeks: {
      program: r.one.programs({
        from: r.programWeeks.programId,
        to: r.programs.id,
        optional: false,
      }),
      sessions: r.many.programSessions(),
    },
    programSessions: {
      week: r.one.programWeeks({
        from: r.programSessions.weekId,
        to: r.programWeeks.id,
        optional: false,
      }),
      exercises: r.many.programSessionExercises(),
      userProgress: r.many.userSessionProgress(),
    },
    programSessionExercises: {
      session: r.one.programSessions({
        from: r.programSessionExercises.sessionId,
        to: r.programSessions.id,
        optional: false,
      }),
      exercise: r.one.exercises({
        from: r.programSessionExercises.exerciseId,
        to: r.exercises.id,
        optional: false,
      }),
      suggestedSets: r.many.programSuggestedSets(),
    },
    programSuggestedSets: {
      programSessionExercise: r.one.programSessionExercises({
        from: r.programSuggestedSets.programSessionExerciseId,
        to: r.programSessionExercises.id,
        optional: false,
      }),
    },
    userProgramEnrollments: {
      user: r.one.user({ from: r.userProgramEnrollments.userId, to: r.user.id, optional: false }),
      program: r.one.programs({
        from: r.userProgramEnrollments.programId,
        to: r.programs.id,
        optional: false,
      }),
      sessionProgress: r.many.userSessionProgress(),
    },
    userSessionProgress: {
      enrollment: r.one.userProgramEnrollments({
        from: r.userSessionProgress.enrollmentId,
        to: r.userProgramEnrollments.id,
        optional: false,
      }),
      session: r.one.programSessions({
        from: r.userSessionProgress.sessionId,
        to: r.programSessions.id,
        optional: false,
      }),
      workoutSession: r.one.workoutSessions({
        from: r.userSessionProgress.workoutSessionId,
        to: r.workoutSessions.id,
      }),
    },
  }),
)
