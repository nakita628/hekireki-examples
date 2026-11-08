package workoutcool.models

import org.jetbrains.exposed.v1.core.Schema
import org.jetbrains.exposed.v1.core.Table

object PrismaSchema {
    val schemas: List<Schema> = emptyList()
    val enumTypes: List<String> =
        listOf(
            "CREATE TYPE \"UserRole\" AS ENUM ('admin', 'user')",
            "CREATE TYPE \"ExercisePrivacy\" AS ENUM ('PUBLIC', 'PRIVATE')",
            "CREATE TYPE \"ExerciseAttributeNameEnum\" AS ENUM ('TYPE', 'PRIMARY_MUSCLE', 'SECONDARY_MUSCLE', 'EQUIPMENT', 'MECHANICS_TYPE')",
            "CREATE TYPE \"ExerciseAttributeValueEnum\" AS ENUM ('BODYWEIGHT', 'STRENGTH', 'POWERLIFTING', 'CALISTHENIC', 'PLYOMETRICS', 'STRETCHING', 'STRONGMAN', 'CARDIO', 'STABILIZATION', 'POWER', 'RESISTANCE', 'CROSSFIT', 'WEIGHTLIFTING', 'BICEPS', 'SHOULDERS', 'CHEST', 'BACK', 'GLUTES', 'TRICEPS', 'HAMSTRINGS', 'QUADRICEPS', 'FOREARMS', 'CALVES', 'TRAPS', 'ABDOMINALS', 'NECK', 'LATS', 'ADDUCTORS', 'ABDUCTORS', 'OBLIQUES', 'GROIN', 'FULL_BODY', 'ROTATOR_CUFF', 'HIP_FLEXOR', 'ACHILLES_TENDON', 'FINGERS', 'DUMBBELL', 'KETTLEBELLS', 'BARBELL', 'SMITH_MACHINE', 'BODY_ONLY', 'OTHER', 'BANDS', 'EZ_BAR', 'MACHINE', 'DESK', 'PULLUP_BAR', 'NONE', 'CABLE', 'MEDICINE_BALL', 'SWISS_BALL', 'FOAM_ROLL', 'WEIGHT_PLATE', 'TRX', 'BOX', 'ROPES', 'SPIN_BIKE', 'STEP', 'BOSU', 'TYRE', 'SANDBAG', 'POLE', 'BENCH', 'WALL', 'BAR', 'RACK', 'CAR', 'SLED', 'CHAIN', 'SKIERG', 'ROPE', 'NA', 'ISOLATION', 'COMPOUND')",
            "CREATE TYPE \"WorkoutSetType\" AS ENUM ('TIME', 'WEIGHT', 'REPS', 'BODYWEIGHT', 'NA')",
            "CREATE TYPE \"WorkoutSetUnit\" AS ENUM ('kg', 'lbs')",
            "CREATE TYPE \"SubscriptionStatus\" AS ENUM ('ACTIVE', 'TRIAL', 'CANCELLED', 'EXPIRED', 'PAUSED')",
            "CREATE TYPE \"Platform\" AS ENUM ('WEB', 'IOS', 'ANDROID')",
            "CREATE TYPE \"PaymentProcessor\" AS ENUM ('STRIPE', 'PAYPAL', 'LEMONSQUEEZY', 'PADDLE', 'APPLE_PAY', 'GOOGLE_PAY', 'REVENUECAT', 'NONE', 'OTHER')",
            "CREATE TYPE \"ProgramLevel\" AS ENUM ('BEGINNER', 'INTERMEDIATE', 'ADVANCED', 'EXPERT')",
            "CREATE TYPE \"ProgramVisibility\" AS ENUM ('DRAFT', 'PUBLISHED', 'ARCHIVED')",
        )
    val tables: List<Table> =
        listOf(
            UserTable,
            UserFavoriteExerciseTable,
            SessionTable,
            AccountTable,
            VerificationTable,
            FeedbacksTable,
            ExerciseTable,
            ExerciseAttributeNameTable,
            ExerciseAttributeValueTable,
            ExerciseAttributeTable,
            WorkoutSessionTable,
            WorkoutSessionExerciseTable,
            WorkoutSetTable,
            SubscriptionPlanTable,
            PlanProviderMappingTable,
            SubscriptionTable,
            LicenseTable,
            RevenueCatWebhookEventTable,
            ProgramTable,
            ProgramCoachTable,
            ProgramWeekTable,
            ProgramSessionTable,
            ProgramSessionExerciseTable,
            ProgramSuggestedSetTable,
            UserProgramEnrollmentTable,
            UserSessionProgressTable,
        )
}
