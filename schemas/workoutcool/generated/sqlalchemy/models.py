from sqlalchemy import ARRAY, Dialect, Enum, ForeignKey, Index, Integer, MetaData, Numeric, Text, TypeDecorator, text
from sqlalchemy.dialects.postgresql import JSONB, TIMESTAMP
from sqlalchemy.orm import DeclarativeBase, Mapped, mapped_column, relationship
from typing import Any, Optional
from decimal import Decimal as DecimalType
from datetime import datetime, timezone


class UtcDateTime(TypeDecorator[datetime]):
    impl = TIMESTAMP
    cache_ok = True

    def process_bind_param(self, value: Optional[datetime], dialect: Dialect) -> Optional[datetime]:
        if value is None or value.tzinfo is None:
            return value
        return value.astimezone(timezone.utc).replace(tzinfo=None)

    def process_result_value(self, value: Optional[datetime], dialect: Dialect) -> Optional[datetime]:
        return None if value is None else value.replace(tzinfo=timezone.utc)


def utc_now() -> datetime:
    return datetime.now(timezone.utc)


class Base(DeclarativeBase):
    metadata = MetaData(naming_convention={"pk": "%(table_name)s_pkey"})
    type_annotation_map = {datetime: UtcDateTime(precision=3), str: Text}


class User(Base):
    __tablename__ = "user"

    id: Mapped[str] = mapped_column(primary_key=True)
    first_name: Mapped[str] = mapped_column("firstName", default="", server_default=text("''"))
    last_name: Mapped[str] = mapped_column("lastName", default="", server_default=text("''"))
    name: Mapped[str]
    email: Mapped[str]
    email_verified: Mapped[bool] = mapped_column("emailVerified")
    image: Mapped[Optional[str]]
    locale: Mapped[Optional[str]] = mapped_column(default="fr", server_default=text("'fr'"))
    created_at: Mapped[datetime] = mapped_column("createdAt")
    updated_at: Mapped[datetime] = mapped_column("updatedAt")
    role: Mapped[Optional[str]] = mapped_column(Enum("admin", "user", name="UserRole"), default="user", server_default=text("'user'"))
    banned: Mapped[Optional[bool]] = mapped_column(default=False, server_default=text("false"))
    ban_reason: Mapped[Optional[str]] = mapped_column("banReason")
    ban_expires: Mapped[Optional[datetime]] = mapped_column("banExpires")
    is_premium: Mapped[Optional[bool]] = mapped_column("isPremium", default=False, server_default=text("false"))
    onboarding_preferences: Mapped[Optional[dict[str, Any]]] = mapped_column("onboardingPreferences", JSONB(none_as_null=True))

    __table_args__ = (
        Index("user_email_key", "email", unique=True),
    )

    sessions: Mapped[list["Session"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    accounts: Mapped[list["Account"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    feedbacks: Mapped[list["Feedbacks"]] = relationship(back_populates="user")
    workout_session: Mapped[list["WorkoutSession"]] = relationship(passive_deletes="all", back_populates="user")
    favorite_exercises: Mapped[list["UserFavoriteExercise"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    subscriptions: Mapped[list["Subscription"]] = relationship(passive_deletes="all", back_populates="user")
    licenses: Mapped[list["License"]] = relationship(back_populates="user")
    program_enrollments: Mapped[list["UserProgramEnrollment"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")

class UserFavoriteExercise(Base):
    __tablename__ = "user_favorite_exercises"

    id: Mapped[str] = mapped_column(primary_key=True)
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("user.id", ondelete="CASCADE", onupdate="CASCADE", name="user_favorite_exercises_userId_fkey"))
    exercise_id: Mapped[str] = mapped_column("exerciseId", ForeignKey("exercises.id", ondelete="CASCADE", onupdate="CASCADE", name="user_favorite_exercises_exerciseId_fkey"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("user_favorite_exercises_userId_exerciseId_key", "userId", "exerciseId", unique=True),
    )

    user: Mapped["User"] = relationship(back_populates="favorite_exercises")
    exercise: Mapped["Exercise"] = relationship(back_populates="favorites_by_users")

class Session(Base):
    __tablename__ = "session"

    id: Mapped[str] = mapped_column(primary_key=True)
    expires_at: Mapped[datetime] = mapped_column("expiresAt")
    token: Mapped[str]
    created_at: Mapped[datetime] = mapped_column("createdAt")
    updated_at: Mapped[datetime] = mapped_column("updatedAt")
    ip_address: Mapped[Optional[str]] = mapped_column("ipAddress")
    user_agent: Mapped[Optional[str]] = mapped_column("userAgent")
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("user.id", ondelete="CASCADE", onupdate="CASCADE", name="session_userId_fkey"))
    impersonated_by: Mapped[Optional[str]] = mapped_column("impersonatedBy")

    __table_args__ = (
        Index("session_token_key", "token", unique=True),
    )

    user: Mapped["User"] = relationship(back_populates="sessions")

class Account(Base):
    __tablename__ = "account"

    id: Mapped[str] = mapped_column(primary_key=True)
    account_id: Mapped[str] = mapped_column("accountId")
    provider_id: Mapped[str] = mapped_column("providerId")
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("user.id", ondelete="CASCADE", onupdate="CASCADE", name="account_userId_fkey"))
    access_token: Mapped[Optional[str]] = mapped_column("accessToken")
    refresh_token: Mapped[Optional[str]] = mapped_column("refreshToken")
    id_token: Mapped[Optional[str]] = mapped_column("idToken")
    access_token_expires_at: Mapped[Optional[datetime]] = mapped_column("accessTokenExpiresAt")
    refresh_token_expires_at: Mapped[Optional[datetime]] = mapped_column("refreshTokenExpiresAt")
    scope: Mapped[Optional[str]]
    password: Mapped[Optional[str]]
    created_at: Mapped[datetime] = mapped_column("createdAt")
    updated_at: Mapped[datetime] = mapped_column("updatedAt")

    user: Mapped["User"] = relationship(back_populates="accounts")

class Verification(Base):
    __tablename__ = "verification"

    id: Mapped[str] = mapped_column(primary_key=True)
    identifier: Mapped[str]
    value: Mapped[str]
    expires_at: Mapped[datetime] = mapped_column("expiresAt")
    created_at: Mapped[Optional[datetime]] = mapped_column("createdAt")
    updated_at: Mapped[Optional[datetime]] = mapped_column("updatedAt")

class Feedbacks(Base):
    __tablename__ = "feedbacks"

    id: Mapped[str] = mapped_column(primary_key=True)
    review: Mapped[int]
    message: Mapped[str]
    email: Mapped[Optional[str]]
    user_id: Mapped[Optional[str]] = mapped_column("userId", ForeignKey("user.id", ondelete="SET NULL", onupdate="CASCADE", name="feedbacks_userId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    user: Mapped[Optional["User"]] = relationship(back_populates="feedbacks")

class Exercise(Base):
    __tablename__ = "exercises"

    id: Mapped[str] = mapped_column(primary_key=True)
    name: Mapped[str]
    name_en: Mapped[Optional[str]] = mapped_column("nameEn")
    description: Mapped[Optional[str]]
    description_en: Mapped[Optional[str]] = mapped_column("descriptionEn")
    full_video_url: Mapped[Optional[str]] = mapped_column("fullVideoUrl")
    full_video_image_url: Mapped[Optional[str]] = mapped_column("fullVideoImageUrl")
    introduction: Mapped[Optional[str]]
    introduction_en: Mapped[Optional[str]] = mapped_column("introductionEn")
    slug: Mapped[Optional[str]]
    slug_en: Mapped[Optional[str]] = mapped_column("slugEn")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("exercises_slug_key", "slug", unique=True),
        Index("exercises_slugEn_key", "slugEn", unique=True),
    )

    attributes: Mapped[list["ExerciseAttribute"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="exercise")
    workout_session_exercise: Mapped[list["WorkoutSessionExercise"]] = relationship(passive_deletes="all", back_populates="exercise")
    program_session_exercise: Mapped[list["ProgramSessionExercise"]] = relationship(passive_deletes="all", back_populates="exercise")
    favorites_by_users: Mapped[list["UserFavoriteExercise"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="exercise")

class ExerciseAttributeName(Base):
    __tablename__ = "exercise_attribute_names"

    id: Mapped[str] = mapped_column(primary_key=True)
    name: Mapped[str] = mapped_column(Enum("TYPE", "PRIMARY_MUSCLE", "SECONDARY_MUSCLE", "EQUIPMENT", "MECHANICS_TYPE", name="ExerciseAttributeNameEnum"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("exercise_attribute_names_name_key", "name", unique=True),
    )

    values: Mapped[list["ExerciseAttributeValue"]] = relationship(passive_deletes="all", back_populates="attribute_name")
    attributes: Mapped[list["ExerciseAttribute"]] = relationship(passive_deletes="all", back_populates="attribute_name")

class ExerciseAttributeValue(Base):
    __tablename__ = "exercise_attribute_values"

    id: Mapped[str] = mapped_column(primary_key=True)
    attribute_name_id: Mapped[str] = mapped_column("attributeNameId", ForeignKey("exercise_attribute_names.id", ondelete="RESTRICT", onupdate="CASCADE", name="exercise_attribute_values_attributeNameId_fkey"))
    value: Mapped[str] = mapped_column(Enum("BODYWEIGHT", "STRENGTH", "POWERLIFTING", "CALISTHENIC", "PLYOMETRICS", "STRETCHING", "STRONGMAN", "CARDIO", "STABILIZATION", "POWER", "RESISTANCE", "CROSSFIT", "WEIGHTLIFTING", "BICEPS", "SHOULDERS", "CHEST", "BACK", "GLUTES", "TRICEPS", "HAMSTRINGS", "QUADRICEPS", "FOREARMS", "CALVES", "TRAPS", "ABDOMINALS", "NECK", "LATS", "ADDUCTORS", "ABDUCTORS", "OBLIQUES", "GROIN", "FULL_BODY", "ROTATOR_CUFF", "HIP_FLEXOR", "ACHILLES_TENDON", "FINGERS", "DUMBBELL", "KETTLEBELLS", "BARBELL", "SMITH_MACHINE", "BODY_ONLY", "OTHER", "BANDS", "EZ_BAR", "MACHINE", "DESK", "PULLUP_BAR", "NONE", "CABLE", "MEDICINE_BALL", "SWISS_BALL", "FOAM_ROLL", "WEIGHT_PLATE", "TRX", "BOX", "ROPES", "SPIN_BIKE", "STEP", "BOSU", "TYRE", "SANDBAG", "POLE", "BENCH", "WALL", "BAR", "RACK", "CAR", "SLED", "CHAIN", "SKIERG", "ROPE", "NA", "ISOLATION", "COMPOUND", name="ExerciseAttributeValueEnum"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("exercise_attribute_values_attributeNameId_value_key", "attributeNameId", "value", unique=True),
    )

    attribute_name: Mapped["ExerciseAttributeName"] = relationship(back_populates="values")
    attributes: Mapped[list["ExerciseAttribute"]] = relationship(passive_deletes="all", back_populates="attribute_value")

class ExerciseAttribute(Base):
    __tablename__ = "exercise_attributes"

    id: Mapped[str] = mapped_column(primary_key=True)
    exercise_id: Mapped[str] = mapped_column("exerciseId", ForeignKey("exercises.id", ondelete="CASCADE", onupdate="CASCADE", name="exercise_attributes_exerciseId_fkey"))
    attribute_name_id: Mapped[str] = mapped_column("attributeNameId", ForeignKey("exercise_attribute_names.id", ondelete="RESTRICT", onupdate="CASCADE", name="exercise_attributes_attributeNameId_fkey"))
    attribute_value_id: Mapped[str] = mapped_column("attributeValueId", ForeignKey("exercise_attribute_values.id", ondelete="RESTRICT", onupdate="CASCADE", name="exercise_attributes_attributeValueId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("exercise_attributes_exerciseId_attributeNameId_attributeVal_key", "exerciseId", "attributeNameId", "attributeValueId", unique=True),
    )

    exercise: Mapped["Exercise"] = relationship(back_populates="attributes")
    attribute_name: Mapped["ExerciseAttributeName"] = relationship(back_populates="attributes")
    attribute_value: Mapped["ExerciseAttributeValue"] = relationship(back_populates="attributes")

class WorkoutSession(Base):
    __tablename__ = "workout_sessions"

    id: Mapped[str] = mapped_column(primary_key=True)
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("user.id", ondelete="RESTRICT", onupdate="CASCADE", name="workout_sessions_userId_fkey"))
    started_at: Mapped[datetime] = mapped_column("startedAt")
    ended_at: Mapped[Optional[datetime]] = mapped_column("endedAt")
    duration: Mapped[Optional[int]]
    muscles: Mapped[list[str]] = mapped_column(ARRAY(Enum("BODYWEIGHT", "STRENGTH", "POWERLIFTING", "CALISTHENIC", "PLYOMETRICS", "STRETCHING", "STRONGMAN", "CARDIO", "STABILIZATION", "POWER", "RESISTANCE", "CROSSFIT", "WEIGHTLIFTING", "BICEPS", "SHOULDERS", "CHEST", "BACK", "GLUTES", "TRICEPS", "HAMSTRINGS", "QUADRICEPS", "FOREARMS", "CALVES", "TRAPS", "ABDOMINALS", "NECK", "LATS", "ADDUCTORS", "ABDUCTORS", "OBLIQUES", "GROIN", "FULL_BODY", "ROTATOR_CUFF", "HIP_FLEXOR", "ACHILLES_TENDON", "FINGERS", "DUMBBELL", "KETTLEBELLS", "BARBELL", "SMITH_MACHINE", "BODY_ONLY", "OTHER", "BANDS", "EZ_BAR", "MACHINE", "DESK", "PULLUP_BAR", "NONE", "CABLE", "MEDICINE_BALL", "SWISS_BALL", "FOAM_ROLL", "WEIGHT_PLATE", "TRX", "BOX", "ROPES", "SPIN_BIKE", "STEP", "BOSU", "TYRE", "SANDBAG", "POLE", "BENCH", "WALL", "BAR", "RACK", "CAR", "SLED", "CHAIN", "SKIERG", "ROPE", "NA", "ISOLATION", "COMPOUND", name="ExerciseAttributeValueEnum")), nullable=True, default=lambda: [], server_default=text("ARRAY[]::\"ExerciseAttributeValueEnum\"[]"))
    rating: Mapped[Optional[int]]
    rating_comment: Mapped[Optional[str]] = mapped_column("ratingComment")

    user: Mapped["User"] = relationship(back_populates="workout_session")
    exercises: Mapped[list["WorkoutSessionExercise"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="workout_session")
    user_session_progress: Mapped[Optional["UserSessionProgress"]] = relationship(back_populates="workout_session")

class WorkoutSessionExercise(Base):
    __tablename__ = "workout_session_exercises"

    id: Mapped[str] = mapped_column(primary_key=True)
    workout_session_id: Mapped[str] = mapped_column("workoutSessionId", ForeignKey("workout_sessions.id", ondelete="CASCADE", onupdate="CASCADE", name="workout_session_exercises_workoutSessionId_fkey"))
    exercise_id: Mapped[str] = mapped_column("exerciseId", ForeignKey("exercises.id", ondelete="RESTRICT", onupdate="CASCADE", name="workout_session_exercises_exerciseId_fkey"))
    order: Mapped[int]

    workout_session: Mapped["WorkoutSession"] = relationship(back_populates="exercises")
    exercise: Mapped["Exercise"] = relationship(back_populates="workout_session_exercise")
    sets: Mapped[list["WorkoutSet"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="workout_session_exercise")

class WorkoutSet(Base):
    __tablename__ = "workout_sets"

    id: Mapped[str] = mapped_column(primary_key=True)
    workout_session_exercise_id: Mapped[str] = mapped_column("workoutSessionExerciseId", ForeignKey("workout_session_exercises.id", ondelete="CASCADE", onupdate="CASCADE", name="workout_sets_workoutSessionExerciseId_fkey"))
    set_index: Mapped[int] = mapped_column("setIndex")
    type: Mapped[str] = mapped_column(Enum("TIME", "WEIGHT", "REPS", "BODYWEIGHT", "NA", name="WorkoutSetType"))
    types: Mapped[list[str]] = mapped_column(ARRAY(Enum("TIME", "WEIGHT", "REPS", "BODYWEIGHT", "NA", name="WorkoutSetType")), nullable=True, default=lambda: [], server_default=text("ARRAY[]::\"WorkoutSetType\"[]"))
    values_int: Mapped[list[int]] = mapped_column("valuesInt", ARRAY(Integer), nullable=True, default=lambda: [], server_default=text("ARRAY[]::integer[]"))
    values_sec: Mapped[list[int]] = mapped_column("valuesSec", ARRAY(Integer), nullable=True, default=lambda: [], server_default=text("ARRAY[]::integer[]"))
    units: Mapped[list[str]] = mapped_column(ARRAY(Enum("kg", "lbs", name="WorkoutSetUnit")), nullable=True, default=lambda: [], server_default=text("ARRAY[]::\"WorkoutSetUnit\"[]"))
    completed: Mapped[bool] = mapped_column(default=False, server_default=text("false"))

    workout_session_exercise: Mapped["WorkoutSessionExercise"] = relationship(back_populates="sets")

class SubscriptionPlan(Base):
    __tablename__ = "subscription_plans"

    id: Mapped[str] = mapped_column(primary_key=True)
    price_monthly: Mapped[Optional[DecimalType]] = mapped_column("priceMonthly", Numeric(precision=10, scale=2))
    price_yearly: Mapped[Optional[DecimalType]] = mapped_column("priceYearly", Numeric(precision=10, scale=2))
    currency: Mapped[str] = mapped_column(default="EUR", server_default=text("'EUR'"))
    interval: Mapped[str] = mapped_column(default="month", server_default=text("'month'"))
    is_active: Mapped[bool] = mapped_column("isActive", default=True, server_default=text("true"))
    available_regions: Mapped[list[str]] = mapped_column("availableRegions", ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    subscriptions: Mapped[list["Subscription"]] = relationship(passive_deletes="all", back_populates="plan")
    provider_mappings: Mapped[list["PlanProviderMapping"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="plan")

class PlanProviderMapping(Base):
    __tablename__ = "plan_provider_mappings"

    id: Mapped[str] = mapped_column(primary_key=True)
    plan_id: Mapped[str] = mapped_column("planId", ForeignKey("subscription_plans.id", ondelete="CASCADE", onupdate="CASCADE", name="plan_provider_mappings_planId_fkey"))
    provider: Mapped[str] = mapped_column(Enum("STRIPE", "PAYPAL", "LEMONSQUEEZY", "PADDLE", "APPLE_PAY", "GOOGLE_PAY", "REVENUECAT", "NONE", "OTHER", name="PaymentProcessor"))
    external_id: Mapped[str] = mapped_column("externalId")
    region: Mapped[Optional[str]]
    metadata_: Mapped[Optional[dict[str, Any]]] = mapped_column("metadata", JSONB(none_as_null=True))
    is_active: Mapped[bool] = mapped_column("isActive", default=True, server_default=text("true"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("plan_provider_mappings_provider_externalId_idx", "provider", "externalId"),
        Index("plan_provider_mappings_planId_provider_region_key", "planId", "provider", "region", unique=True),
    )

    plan: Mapped["SubscriptionPlan"] = relationship(back_populates="provider_mappings")

class Subscription(Base):
    __tablename__ = "subscriptions"

    id: Mapped[str] = mapped_column(primary_key=True)
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("user.id", ondelete="RESTRICT", onupdate="CASCADE", name="subscriptions_userId_fkey"))
    plan_id: Mapped[str] = mapped_column("planId", ForeignKey("subscription_plans.id", ondelete="RESTRICT", onupdate="CASCADE", name="subscriptions_planId_fkey"))
    revenue_cat_user_id: Mapped[Optional[str]] = mapped_column("revenueCatUserId")
    status: Mapped[str] = mapped_column(Enum("ACTIVE", "TRIAL", "CANCELLED", "EXPIRED", "PAUSED", name="SubscriptionStatus"))
    started_at: Mapped[datetime] = mapped_column("startedAt")
    current_period_end: Mapped[Optional[datetime]] = mapped_column("currentPeriodEnd")
    cancelled_at: Mapped[Optional[datetime]] = mapped_column("cancelledAt")
    platform: Mapped[Optional[str]] = mapped_column(Enum("WEB", "IOS", "ANDROID", name="Platform"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("subscriptions_userId_platform_key", "userId", "platform", unique=True),
    )

    user: Mapped["User"] = relationship(back_populates="subscriptions")
    plan: Mapped["SubscriptionPlan"] = relationship(back_populates="subscriptions")

class License(Base):
    __tablename__ = "licenses"

    id: Mapped[str] = mapped_column(primary_key=True)
    key: Mapped[str]
    user_id: Mapped[Optional[str]] = mapped_column("userId", ForeignKey("user.id", ondelete="SET NULL", onupdate="CASCADE", name="licenses_userId_fkey"))
    valid_from: Mapped[datetime] = mapped_column("validFrom", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    valid_until: Mapped[Optional[datetime]] = mapped_column("validUntil")
    max_users: Mapped[Optional[int]] = mapped_column("maxUsers", default=1, server_default=text("1"))
    features: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    activated_at: Mapped[Optional[datetime]] = mapped_column("activatedAt")
    last_checked_at: Mapped[Optional[datetime]] = mapped_column("lastCheckedAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("licenses_key_key", "key", unique=True),
    )

    user: Mapped[Optional["User"]] = relationship(back_populates="licenses")

class RevenueCatWebhookEvent(Base):
    __tablename__ = "revenuecat_webhook_events"

    id: Mapped[str] = mapped_column(primary_key=True)
    event_type: Mapped[str] = mapped_column("eventType")
    event_timestamp: Mapped[datetime] = mapped_column("eventTimestamp")
    app_user_id: Mapped[str] = mapped_column("appUserId")
    environment: Mapped[str]
    product_id: Mapped[Optional[str]] = mapped_column("productId")
    transaction_id: Mapped[Optional[str]] = mapped_column("transactionId")
    original_transaction_id: Mapped[Optional[str]] = mapped_column("originalTransactionId")
    entitlement_ids: Mapped[Optional[str]] = mapped_column("entitlementIds")
    processed: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    processing_error: Mapped[Optional[str]] = mapped_column("processingError")
    retry_count: Mapped[int] = mapped_column("retryCount", default=0, server_default=text("0"))
    raw_event_data: Mapped[dict[str, Any]] = mapped_column("rawEventData", JSONB)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("revenuecat_webhook_events_appUserId_idx", "appUserId"),
        Index("revenuecat_webhook_events_eventType_idx", "eventType"),
        Index("revenuecat_webhook_events_processed_idx", "processed"),
        Index("revenuecat_webhook_events_eventTimestamp_idx", "eventTimestamp"),
    )

class Program(Base):
    __tablename__ = "programs"

    id: Mapped[str] = mapped_column(primary_key=True)
    slug: Mapped[str]
    slug_en: Mapped[str] = mapped_column("slugEn")
    slug_es: Mapped[str] = mapped_column("slugEs")
    slug_pt: Mapped[str] = mapped_column("slugPt")
    slug_ru: Mapped[str] = mapped_column("slugRu")
    slug_zh_cn: Mapped[str] = mapped_column("slugZhCn")
    title: Mapped[str]
    title_en: Mapped[str] = mapped_column("titleEn")
    title_es: Mapped[str] = mapped_column("titleEs")
    title_pt: Mapped[str] = mapped_column("titlePt")
    title_ru: Mapped[str] = mapped_column("titleRu")
    title_zh_cn: Mapped[str] = mapped_column("titleZhCn")
    description: Mapped[str]
    description_en: Mapped[str] = mapped_column("descriptionEn")
    description_es: Mapped[str] = mapped_column("descriptionEs")
    description_pt: Mapped[str] = mapped_column("descriptionPt")
    description_ru: Mapped[str] = mapped_column("descriptionRu")
    description_zh_cn: Mapped[str] = mapped_column("descriptionZhCn")
    category: Mapped[str]
    image: Mapped[str]
    level: Mapped[str] = mapped_column(Enum("BEGINNER", "INTERMEDIATE", "ADVANCED", "EXPERT", name="ProgramLevel"))
    type: Mapped[str] = mapped_column(Enum("BODYWEIGHT", "STRENGTH", "POWERLIFTING", "CALISTHENIC", "PLYOMETRICS", "STRETCHING", "STRONGMAN", "CARDIO", "STABILIZATION", "POWER", "RESISTANCE", "CROSSFIT", "WEIGHTLIFTING", "BICEPS", "SHOULDERS", "CHEST", "BACK", "GLUTES", "TRICEPS", "HAMSTRINGS", "QUADRICEPS", "FOREARMS", "CALVES", "TRAPS", "ABDOMINALS", "NECK", "LATS", "ADDUCTORS", "ABDUCTORS", "OBLIQUES", "GROIN", "FULL_BODY", "ROTATOR_CUFF", "HIP_FLEXOR", "ACHILLES_TENDON", "FINGERS", "DUMBBELL", "KETTLEBELLS", "BARBELL", "SMITH_MACHINE", "BODY_ONLY", "OTHER", "BANDS", "EZ_BAR", "MACHINE", "DESK", "PULLUP_BAR", "NONE", "CABLE", "MEDICINE_BALL", "SWISS_BALL", "FOAM_ROLL", "WEIGHT_PLATE", "TRX", "BOX", "ROPES", "SPIN_BIKE", "STEP", "BOSU", "TYRE", "SANDBAG", "POLE", "BENCH", "WALL", "BAR", "RACK", "CAR", "SLED", "CHAIN", "SKIERG", "ROPE", "NA", "ISOLATION", "COMPOUND", name="ExerciseAttributeValueEnum"))
    duration_weeks: Mapped[int] = mapped_column("durationWeeks", default=4, server_default=text("4"))
    sessions_per_week: Mapped[int] = mapped_column("sessionsPerWeek", default=3, server_default=text("3"))
    session_duration_min: Mapped[int] = mapped_column("sessionDurationMin", default=30, server_default=text("30"))
    equipment: Mapped[list[str]] = mapped_column(ARRAY(Enum("BODYWEIGHT", "STRENGTH", "POWERLIFTING", "CALISTHENIC", "PLYOMETRICS", "STRETCHING", "STRONGMAN", "CARDIO", "STABILIZATION", "POWER", "RESISTANCE", "CROSSFIT", "WEIGHTLIFTING", "BICEPS", "SHOULDERS", "CHEST", "BACK", "GLUTES", "TRICEPS", "HAMSTRINGS", "QUADRICEPS", "FOREARMS", "CALVES", "TRAPS", "ABDOMINALS", "NECK", "LATS", "ADDUCTORS", "ABDUCTORS", "OBLIQUES", "GROIN", "FULL_BODY", "ROTATOR_CUFF", "HIP_FLEXOR", "ACHILLES_TENDON", "FINGERS", "DUMBBELL", "KETTLEBELLS", "BARBELL", "SMITH_MACHINE", "BODY_ONLY", "OTHER", "BANDS", "EZ_BAR", "MACHINE", "DESK", "PULLUP_BAR", "NONE", "CABLE", "MEDICINE_BALL", "SWISS_BALL", "FOAM_ROLL", "WEIGHT_PLATE", "TRX", "BOX", "ROPES", "SPIN_BIKE", "STEP", "BOSU", "TYRE", "SANDBAG", "POLE", "BENCH", "WALL", "BAR", "RACK", "CAR", "SLED", "CHAIN", "SKIERG", "ROPE", "NA", "ISOLATION", "COMPOUND", name="ExerciseAttributeValueEnum")), nullable=True, default=lambda: [], server_default=text("ARRAY[]::\"ExerciseAttributeValueEnum\"[]"))
    is_premium: Mapped[bool] = mapped_column("isPremium", default=True, server_default=text("true"))
    is_active: Mapped[bool] = mapped_column("isActive", default=True, server_default=text("true"))
    visibility: Mapped[str] = mapped_column(Enum("DRAFT", "PUBLISHED", "ARCHIVED", name="ProgramVisibility"), default="DRAFT", server_default=text("'DRAFT'"))
    participant_count: Mapped[int] = mapped_column("participantCount", default=0, server_default=text("0"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("programs_slug_key", "slug", unique=True),
        Index("programs_slugEn_key", "slugEn", unique=True),
        Index("programs_slugEs_key", "slugEs", unique=True),
        Index("programs_slugPt_key", "slugPt", unique=True),
        Index("programs_slugRu_key", "slugRu", unique=True),
        Index("programs_slugZhCn_key", "slugZhCn", unique=True),
    )

    weeks: Mapped[list["ProgramWeek"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="program")
    enrollments: Mapped[list["UserProgramEnrollment"]] = relationship(passive_deletes="all", back_populates="program")
    coaches: Mapped[list["ProgramCoach"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="program")

class ProgramCoach(Base):
    __tablename__ = "program_coaches"

    id: Mapped[str] = mapped_column(primary_key=True)
    program_id: Mapped[str] = mapped_column("programId", ForeignKey("programs.id", ondelete="CASCADE", onupdate="CASCADE", name="program_coaches_programId_fkey"))
    name: Mapped[str]
    image: Mapped[str]
    order: Mapped[int] = mapped_column(default=0, server_default=text("0"))

    program: Mapped["Program"] = relationship(back_populates="coaches")

class ProgramWeek(Base):
    __tablename__ = "program_weeks"

    id: Mapped[str] = mapped_column(primary_key=True)
    program_id: Mapped[str] = mapped_column("programId", ForeignKey("programs.id", ondelete="CASCADE", onupdate="CASCADE", name="program_weeks_programId_fkey"))
    week_number: Mapped[int] = mapped_column("weekNumber")
    title: Mapped[str]
    title_en: Mapped[str] = mapped_column("titleEn")
    title_es: Mapped[str] = mapped_column("titleEs")
    title_pt: Mapped[str] = mapped_column("titlePt")
    title_ru: Mapped[str] = mapped_column("titleRu")
    title_zh_cn: Mapped[str] = mapped_column("titleZhCn")
    description: Mapped[str]
    description_en: Mapped[str] = mapped_column("descriptionEn")
    description_es: Mapped[str] = mapped_column("descriptionEs")
    description_pt: Mapped[str] = mapped_column("descriptionPt")
    description_ru: Mapped[str] = mapped_column("descriptionRu")
    description_zh_cn: Mapped[str] = mapped_column("descriptionZhCn")

    __table_args__ = (
        Index("program_weeks_programId_weekNumber_key", "programId", "weekNumber", unique=True),
    )

    program: Mapped["Program"] = relationship(back_populates="weeks")
    sessions: Mapped[list["ProgramSession"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="week")

class ProgramSession(Base):
    __tablename__ = "program_sessions"

    id: Mapped[str] = mapped_column(primary_key=True)
    week_id: Mapped[str] = mapped_column("weekId", ForeignKey("program_weeks.id", ondelete="CASCADE", onupdate="CASCADE", name="program_sessions_weekId_fkey"))
    session_number: Mapped[int] = mapped_column("sessionNumber")
    title: Mapped[str]
    title_en: Mapped[str] = mapped_column("titleEn")
    title_es: Mapped[str] = mapped_column("titleEs")
    title_pt: Mapped[str] = mapped_column("titlePt")
    title_ru: Mapped[str] = mapped_column("titleRu")
    title_zh_cn: Mapped[str] = mapped_column("titleZhCn")
    slug: Mapped[str]
    slug_en: Mapped[str] = mapped_column("slugEn")
    slug_es: Mapped[str] = mapped_column("slugEs")
    slug_pt: Mapped[str] = mapped_column("slugPt")
    slug_ru: Mapped[str] = mapped_column("slugRu")
    slug_zh_cn: Mapped[str] = mapped_column("slugZhCn")
    description: Mapped[str]
    description_en: Mapped[str] = mapped_column("descriptionEn")
    description_es: Mapped[str] = mapped_column("descriptionEs")
    description_pt: Mapped[str] = mapped_column("descriptionPt")
    description_ru: Mapped[str] = mapped_column("descriptionRu")
    description_zh_cn: Mapped[str] = mapped_column("descriptionZhCn")
    equipment: Mapped[list[str]] = mapped_column(ARRAY(Enum("BODYWEIGHT", "STRENGTH", "POWERLIFTING", "CALISTHENIC", "PLYOMETRICS", "STRETCHING", "STRONGMAN", "CARDIO", "STABILIZATION", "POWER", "RESISTANCE", "CROSSFIT", "WEIGHTLIFTING", "BICEPS", "SHOULDERS", "CHEST", "BACK", "GLUTES", "TRICEPS", "HAMSTRINGS", "QUADRICEPS", "FOREARMS", "CALVES", "TRAPS", "ABDOMINALS", "NECK", "LATS", "ADDUCTORS", "ABDUCTORS", "OBLIQUES", "GROIN", "FULL_BODY", "ROTATOR_CUFF", "HIP_FLEXOR", "ACHILLES_TENDON", "FINGERS", "DUMBBELL", "KETTLEBELLS", "BARBELL", "SMITH_MACHINE", "BODY_ONLY", "OTHER", "BANDS", "EZ_BAR", "MACHINE", "DESK", "PULLUP_BAR", "NONE", "CABLE", "MEDICINE_BALL", "SWISS_BALL", "FOAM_ROLL", "WEIGHT_PLATE", "TRX", "BOX", "ROPES", "SPIN_BIKE", "STEP", "BOSU", "TYRE", "SANDBAG", "POLE", "BENCH", "WALL", "BAR", "RACK", "CAR", "SLED", "CHAIN", "SKIERG", "ROPE", "NA", "ISOLATION", "COMPOUND", name="ExerciseAttributeValueEnum")), nullable=True, default=lambda: [], server_default=text("ARRAY[]::\"ExerciseAttributeValueEnum\"[]"))
    estimated_minutes: Mapped[int] = mapped_column("estimatedMinutes")
    is_premium: Mapped[bool] = mapped_column("isPremium", default=True, server_default=text("true"))

    __table_args__ = (
        Index("program_sessions_weekId_sessionNumber_key", "weekId", "sessionNumber", unique=True),
        Index("program_sessions_weekId_slug_key", "weekId", "slug", unique=True),
        Index("program_sessions_weekId_slugEn_key", "weekId", "slugEn", unique=True),
        Index("program_sessions_weekId_slugEs_key", "weekId", "slugEs", unique=True),
        Index("program_sessions_weekId_slugPt_key", "weekId", "slugPt", unique=True),
        Index("program_sessions_weekId_slugRu_key", "weekId", "slugRu", unique=True),
        Index("program_sessions_weekId_slugZhCn_key", "weekId", "slugZhCn", unique=True),
    )

    week: Mapped["ProgramWeek"] = relationship(back_populates="sessions")
    exercises: Mapped[list["ProgramSessionExercise"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="session")
    user_progress: Mapped[list["UserSessionProgress"]] = relationship(passive_deletes="all", back_populates="session")

class ProgramSessionExercise(Base):
    __tablename__ = "program_session_exercises"

    id: Mapped[str] = mapped_column(primary_key=True)
    session_id: Mapped[str] = mapped_column("sessionId", ForeignKey("program_sessions.id", ondelete="CASCADE", onupdate="CASCADE", name="program_session_exercises_sessionId_fkey"))
    exercise_id: Mapped[str] = mapped_column("exerciseId", ForeignKey("exercises.id", ondelete="RESTRICT", onupdate="CASCADE", name="program_session_exercises_exerciseId_fkey"))
    order: Mapped[int]
    instructions: Mapped[str]
    instructions_en: Mapped[str] = mapped_column("instructionsEn")
    instructions_es: Mapped[str] = mapped_column("instructionsEs")
    instructions_pt: Mapped[str] = mapped_column("instructionsPt")
    instructions_ru: Mapped[str] = mapped_column("instructionsRu")
    instructions_zh_cn: Mapped[str] = mapped_column("instructionsZhCn")

    __table_args__ = (
        Index("program_session_exercises_sessionId_order_key", "sessionId", "order", unique=True),
    )

    session: Mapped["ProgramSession"] = relationship(back_populates="exercises")
    exercise: Mapped["Exercise"] = relationship(back_populates="program_session_exercise")
    suggested_sets: Mapped[list["ProgramSuggestedSet"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="program_session_exercise")

class ProgramSuggestedSet(Base):
    __tablename__ = "program_suggested_sets"

    id: Mapped[str] = mapped_column(primary_key=True)
    program_session_exercise_id: Mapped[str] = mapped_column("programSessionExerciseId", ForeignKey("program_session_exercises.id", ondelete="CASCADE", onupdate="CASCADE", name="program_suggested_sets_programSessionExerciseId_fkey"))
    set_index: Mapped[int] = mapped_column("setIndex")
    types: Mapped[list[str]] = mapped_column(ARRAY(Enum("TIME", "WEIGHT", "REPS", "BODYWEIGHT", "NA", name="WorkoutSetType")), nullable=True, default=lambda: [], server_default=text("ARRAY[]::\"WorkoutSetType\"[]"))
    values_int: Mapped[list[int]] = mapped_column("valuesInt", ARRAY(Integer), nullable=True, default=lambda: [], server_default=text("ARRAY[]::integer[]"))
    values_sec: Mapped[list[int]] = mapped_column("valuesSec", ARRAY(Integer), nullable=True, default=lambda: [], server_default=text("ARRAY[]::integer[]"))
    units: Mapped[list[str]] = mapped_column(ARRAY(Enum("kg", "lbs", name="WorkoutSetUnit")), nullable=True, default=lambda: [], server_default=text("ARRAY[]::\"WorkoutSetUnit\"[]"))

    __table_args__ = (
        Index("program_suggested_sets_programSessionExerciseId_setIndex_key", "programSessionExerciseId", "setIndex", unique=True),
    )

    program_session_exercise: Mapped["ProgramSessionExercise"] = relationship(back_populates="suggested_sets")

class UserProgramEnrollment(Base):
    __tablename__ = "user_program_enrollments"

    id: Mapped[str] = mapped_column(primary_key=True)
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("user.id", ondelete="CASCADE", onupdate="CASCADE", name="user_program_enrollments_userId_fkey"))
    program_id: Mapped[str] = mapped_column("programId", ForeignKey("programs.id", ondelete="RESTRICT", onupdate="CASCADE", name="user_program_enrollments_programId_fkey"))
    enrolled_at: Mapped[datetime] = mapped_column("enrolledAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    current_week: Mapped[int] = mapped_column("currentWeek", default=1, server_default=text("1"))
    current_session: Mapped[int] = mapped_column("currentSession", default=1, server_default=text("1"))
    completed_sessions: Mapped[int] = mapped_column("completedSessions", default=0, server_default=text("0"))
    is_active: Mapped[bool] = mapped_column("isActive", default=True, server_default=text("true"))
    completed_at: Mapped[Optional[datetime]] = mapped_column("completedAt")

    __table_args__ = (
        Index("user_program_enrollments_userId_programId_key", "userId", "programId", unique=True),
    )

    user: Mapped["User"] = relationship(back_populates="program_enrollments")
    program: Mapped["Program"] = relationship(back_populates="enrollments")
    session_progress: Mapped[list["UserSessionProgress"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="enrollment")

class UserSessionProgress(Base):
    __tablename__ = "user_session_progress"

    id: Mapped[str] = mapped_column(primary_key=True)
    enrollment_id: Mapped[str] = mapped_column("enrollmentId", ForeignKey("user_program_enrollments.id", ondelete="CASCADE", onupdate="CASCADE", name="user_session_progress_enrollmentId_fkey"))
    session_id: Mapped[str] = mapped_column("sessionId", ForeignKey("program_sessions.id", ondelete="RESTRICT", onupdate="CASCADE", name="user_session_progress_sessionId_fkey"))
    started_at: Mapped[datetime] = mapped_column("startedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    completed_at: Mapped[Optional[datetime]] = mapped_column("completedAt")
    workout_session_id: Mapped[Optional[str]] = mapped_column("workoutSessionId", ForeignKey("workout_sessions.id", ondelete="SET NULL", onupdate="CASCADE", name="user_session_progress_workoutSessionId_fkey"))

    __table_args__ = (
        Index("user_session_progress_workoutSessionId_key", "workoutSessionId", unique=True),
        Index("user_session_progress_enrollmentId_sessionId_key", "enrollmentId", "sessionId", unique=True),
    )

    enrollment: Mapped["UserProgramEnrollment"] = relationship(back_populates="session_progress")
    session: Mapped["ProgramSession"] = relationship(back_populates="user_progress")
    workout_session: Mapped[Optional["WorkoutSession"]] = relationship(back_populates="user_session_progress")
