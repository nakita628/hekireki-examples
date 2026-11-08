import type { ColumnType } from 'kysely'

export type Generated<T> =
  T extends ColumnType<infer S, infer I, infer U>
    ? ColumnType<S, I | undefined, U>
    : ColumnType<T, T | undefined, T>

export type Timestamp = ColumnType<Date, Date | string, Date | string>

export type UserRole = 'admin' | 'user'

export type ExerciseAttributeNameEnum =
  | 'TYPE'
  | 'PRIMARY_MUSCLE'
  | 'SECONDARY_MUSCLE'
  | 'EQUIPMENT'
  | 'MECHANICS_TYPE'

export type ExerciseAttributeValueEnum =
  | 'BODYWEIGHT'
  | 'STRENGTH'
  | 'POWERLIFTING'
  | 'CALISTHENIC'
  | 'PLYOMETRICS'
  | 'STRETCHING'
  | 'STRONGMAN'
  | 'CARDIO'
  | 'STABILIZATION'
  | 'POWER'
  | 'RESISTANCE'
  | 'CROSSFIT'
  | 'WEIGHTLIFTING'
  | 'BICEPS'
  | 'SHOULDERS'
  | 'CHEST'
  | 'BACK'
  | 'GLUTES'
  | 'TRICEPS'
  | 'HAMSTRINGS'
  | 'QUADRICEPS'
  | 'FOREARMS'
  | 'CALVES'
  | 'TRAPS'
  | 'ABDOMINALS'
  | 'NECK'
  | 'LATS'
  | 'ADDUCTORS'
  | 'ABDUCTORS'
  | 'OBLIQUES'
  | 'GROIN'
  | 'FULL_BODY'
  | 'ROTATOR_CUFF'
  | 'HIP_FLEXOR'
  | 'ACHILLES_TENDON'
  | 'FINGERS'
  | 'DUMBBELL'
  | 'KETTLEBELLS'
  | 'BARBELL'
  | 'SMITH_MACHINE'
  | 'BODY_ONLY'
  | 'OTHER'
  | 'BANDS'
  | 'EZ_BAR'
  | 'MACHINE'
  | 'DESK'
  | 'PULLUP_BAR'
  | 'NONE'
  | 'CABLE'
  | 'MEDICINE_BALL'
  | 'SWISS_BALL'
  | 'FOAM_ROLL'
  | 'WEIGHT_PLATE'
  | 'TRX'
  | 'BOX'
  | 'ROPES'
  | 'SPIN_BIKE'
  | 'STEP'
  | 'BOSU'
  | 'TYRE'
  | 'SANDBAG'
  | 'POLE'
  | 'BENCH'
  | 'WALL'
  | 'BAR'
  | 'RACK'
  | 'CAR'
  | 'SLED'
  | 'CHAIN'
  | 'SKIERG'
  | 'ROPE'
  | 'NA'
  | 'ISOLATION'
  | 'COMPOUND'

export type WorkoutSetType = 'TIME' | 'WEIGHT' | 'REPS' | 'BODYWEIGHT' | 'NA'

export type WorkoutSetUnit = 'kg' | 'lbs'

export type SubscriptionStatus = 'ACTIVE' | 'TRIAL' | 'CANCELLED' | 'EXPIRED' | 'PAUSED'

export type Platform = 'WEB' | 'IOS' | 'ANDROID'

export type PaymentProcessor =
  | 'STRIPE'
  | 'PAYPAL'
  | 'LEMONSQUEEZY'
  | 'PADDLE'
  | 'APPLE_PAY'
  | 'GOOGLE_PAY'
  | 'REVENUECAT'
  | 'NONE'
  | 'OTHER'

export type ProgramLevel = 'BEGINNER' | 'INTERMEDIATE' | 'ADVANCED' | 'EXPERT'

export type ProgramVisibility = 'DRAFT' | 'PUBLISHED' | 'ARCHIVED'

export interface User {
  id: string
  firstName: Generated<string>
  lastName: Generated<string>
  name: string
  email: string
  emailVerified: boolean
  image: string | null
  locale: Generated<string | null>
  createdAt: Timestamp
  updatedAt: Timestamp
  role: Generated<UserRole | null>
  banned: Generated<boolean | null>
  banReason: string | null
  banExpires: Timestamp | null
  isPremium: Generated<boolean | null>
  onboardingPreferences: unknown
}

export interface UserFavoriteExercise {
  id: string
  userId: string
  exerciseId: string
  updatedAt: Timestamp
}

export interface Session {
  id: string
  expiresAt: Timestamp
  token: string
  createdAt: Timestamp
  updatedAt: Timestamp
  ipAddress: string | null
  userAgent: string | null
  userId: string
  impersonatedBy: string | null
}

export interface Account {
  id: string
  accountId: string
  providerId: string
  userId: string
  accessToken: string | null
  refreshToken: string | null
  idToken: string | null
  accessTokenExpiresAt: Timestamp | null
  refreshTokenExpiresAt: Timestamp | null
  scope: string | null
  password: string | null
  createdAt: Timestamp
  updatedAt: Timestamp
}

export interface Verification {
  id: string
  identifier: string
  value: string
  expiresAt: Timestamp
  createdAt: Timestamp | null
  updatedAt: Timestamp | null
}

export interface Feedbacks {
  id: string
  review: number
  message: string
  email: string | null
  userId: string | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface Exercise {
  id: string
  name: string
  nameEn: string | null
  description: string | null
  descriptionEn: string | null
  fullVideoUrl: string | null
  fullVideoImageUrl: string | null
  introduction: string | null
  introductionEn: string | null
  slug: string | null
  slugEn: string | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface ExerciseAttributeName {
  id: string
  name: ExerciseAttributeNameEnum
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface ExerciseAttributeValue {
  id: string
  attributeNameId: string
  value: ExerciseAttributeValueEnum
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface ExerciseAttribute {
  id: string
  exerciseId: string
  attributeNameId: string
  attributeValueId: string
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface WorkoutSession {
  id: string
  userId: string
  startedAt: Timestamp
  endedAt: Timestamp | null
  duration: number | null
  muscles: Generated<ExerciseAttributeValueEnum[]>
  rating: number | null
  ratingComment: string | null
}

export interface WorkoutSessionExercise {
  id: string
  workoutSessionId: string
  exerciseId: string
  order: number
}

export interface WorkoutSet {
  id: string
  workoutSessionExerciseId: string
  setIndex: number
  type: WorkoutSetType
  types: Generated<WorkoutSetType[]>
  valuesInt: Generated<number[]>
  valuesSec: Generated<number[]>
  units: Generated<WorkoutSetUnit[]>
  completed: Generated<boolean>
}

export interface SubscriptionPlan {
  id: string
  priceMonthly: string | null
  priceYearly: string | null
  currency: Generated<string>
  interval: Generated<string>
  isActive: Generated<boolean>
  availableRegions: Generated<string[]>
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface PlanProviderMapping {
  id: string
  planId: string
  provider: PaymentProcessor
  externalId: string
  region: string | null
  metadata: unknown
  isActive: Generated<boolean>
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface Subscription {
  id: string
  userId: string
  planId: string
  revenueCatUserId: string | null
  status: SubscriptionStatus
  startedAt: Timestamp
  currentPeriodEnd: Timestamp | null
  cancelledAt: Timestamp | null
  platform: Platform | null
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface License {
  id: string
  key: string
  userId: string | null
  validFrom: Generated<Timestamp>
  validUntil: Timestamp | null
  maxUsers: Generated<number | null>
  features: unknown
  activatedAt: Timestamp | null
  lastCheckedAt: Timestamp | null
  createdAt: Generated<Timestamp>
}

export interface RevenueCatWebhookEvent {
  id: string
  eventType: string
  eventTimestamp: Timestamp
  appUserId: string
  environment: string
  productId: string | null
  transactionId: string | null
  originalTransactionId: string | null
  entitlementIds: string | null
  processed: Generated<boolean>
  processingError: string | null
  retryCount: Generated<number>
  rawEventData: unknown
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface Program {
  id: string
  slug: string
  slugEn: string
  slugEs: string
  slugPt: string
  slugRu: string
  slugZhCn: string
  title: string
  titleEn: string
  titleEs: string
  titlePt: string
  titleRu: string
  titleZhCn: string
  description: string
  descriptionEn: string
  descriptionEs: string
  descriptionPt: string
  descriptionRu: string
  descriptionZhCn: string
  category: string
  image: string
  level: ProgramLevel
  type: ExerciseAttributeValueEnum
  durationWeeks: Generated<number>
  sessionsPerWeek: Generated<number>
  sessionDurationMin: Generated<number>
  equipment: Generated<ExerciseAttributeValueEnum[]>
  isPremium: Generated<boolean>
  isActive: Generated<boolean>
  visibility: Generated<ProgramVisibility>
  participantCount: Generated<number>
  createdAt: Generated<Timestamp>
  updatedAt: Timestamp
}

export interface ProgramCoach {
  id: string
  programId: string
  name: string
  image: string
  order: Generated<number>
}

export interface ProgramWeek {
  id: string
  programId: string
  weekNumber: number
  title: string
  titleEn: string
  titleEs: string
  titlePt: string
  titleRu: string
  titleZhCn: string
  description: string
  descriptionEn: string
  descriptionEs: string
  descriptionPt: string
  descriptionRu: string
  descriptionZhCn: string
}

export interface ProgramSession {
  id: string
  weekId: string
  sessionNumber: number
  title: string
  titleEn: string
  titleEs: string
  titlePt: string
  titleRu: string
  titleZhCn: string
  slug: string
  slugEn: string
  slugEs: string
  slugPt: string
  slugRu: string
  slugZhCn: string
  description: string
  descriptionEn: string
  descriptionEs: string
  descriptionPt: string
  descriptionRu: string
  descriptionZhCn: string
  equipment: Generated<ExerciseAttributeValueEnum[]>
  estimatedMinutes: number
  isPremium: Generated<boolean>
}

export interface ProgramSessionExercise {
  id: string
  sessionId: string
  exerciseId: string
  order: number
  instructions: string
  instructionsEn: string
  instructionsEs: string
  instructionsPt: string
  instructionsRu: string
  instructionsZhCn: string
}

export interface ProgramSuggestedSet {
  id: string
  programSessionExerciseId: string
  setIndex: number
  types: Generated<WorkoutSetType[]>
  valuesInt: Generated<number[]>
  valuesSec: Generated<number[]>
  units: Generated<WorkoutSetUnit[]>
}

export interface UserProgramEnrollment {
  id: string
  userId: string
  programId: string
  enrolledAt: Generated<Timestamp>
  currentWeek: Generated<number>
  currentSession: Generated<number>
  completedSessions: Generated<number>
  isActive: Generated<boolean>
  completedAt: Timestamp | null
}

export interface UserSessionProgress {
  id: string
  enrollmentId: string
  sessionId: string
  startedAt: Generated<Timestamp>
  completedAt: Timestamp | null
  workoutSessionId: string | null
}

export interface DB {
  user: User
  user_favorite_exercises: UserFavoriteExercise
  session: Session
  account: Account
  verification: Verification
  feedbacks: Feedbacks
  exercises: Exercise
  exercise_attribute_names: ExerciseAttributeName
  exercise_attribute_values: ExerciseAttributeValue
  exercise_attributes: ExerciseAttribute
  workout_sessions: WorkoutSession
  workout_session_exercises: WorkoutSessionExercise
  workout_sets: WorkoutSet
  subscription_plans: SubscriptionPlan
  plan_provider_mappings: PlanProviderMapping
  subscriptions: Subscription
  licenses: License
  revenuecat_webhook_events: RevenueCatWebhookEvent
  programs: Program
  program_coaches: ProgramCoach
  program_weeks: ProgramWeek
  program_sessions: ProgramSession
  program_session_exercises: ProgramSessionExercise
  program_suggested_sets: ProgramSuggestedSet
  user_program_enrollments: UserProgramEnrollment
  user_session_progress: UserSessionProgress
}
