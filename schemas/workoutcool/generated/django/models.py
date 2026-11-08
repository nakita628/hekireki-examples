from datetime import datetime
from datetime import timezone as dt_timezone
from typing import Any, TypeVar

from django.conf import settings
from django.contrib.postgres.fields import ArrayField
from django.db import models
from django.db.backends.base.base import BaseDatabaseWrapper
from django.utils import timezone


class UtcDateTimeField(models.DateTimeField):  # type: ignore[type-arg]
    def get_db_prep_value(self, value: Any, connection: BaseDatabaseWrapper, prepared: bool = False) -> Any:
        if not prepared:
            value = self.get_prep_value(value)
        if not isinstance(value, datetime):
            return super().get_db_prep_value(value, connection, prepared=True)
        if timezone.is_naive(value):
            value = timezone.make_aware(value, timezone.get_default_timezone())
        value = value.astimezone(dt_timezone.utc)
        if connection.vendor == "sqlite":
            return value.isoformat(timespec="milliseconds")
        return connection.ops.adapt_datetimefield_value(value.replace(tzinfo=None))

    def from_db_value(self, value: datetime | None, expression: Any, connection: BaseDatabaseWrapper) -> datetime | None:
        if value is None:
            return None
        if timezone.is_naive(value):
            value = value.replace(tzinfo=dt_timezone.utc)
        return value if settings.USE_TZ else timezone.make_naive(value, timezone.get_default_timezone())


_M = TypeVar("_M", bound=models.Model)


class AutoNowQuerySet(models.QuerySet[_M]):
    def update(self, **kwargs: Any) -> int:
        for field in self.model._meta.concrete_fields:
            if getattr(field, "auto_now", False):
                kwargs.setdefault(field.name, getattr(field, "stamp", timezone.now)())
        return super().update(**kwargs)


class UserRole(models.TextChoices):
    admin = "admin"
    user = "user"


class ExercisePrivacy(models.TextChoices):
    PUBLIC = "PUBLIC"
    PRIVATE = "PRIVATE"


class ExerciseAttributeNameEnum(models.TextChoices):
    TYPE = "TYPE"
    PRIMARY_MUSCLE = "PRIMARY_MUSCLE"
    SECONDARY_MUSCLE = "SECONDARY_MUSCLE"
    EQUIPMENT = "EQUIPMENT"
    MECHANICS_TYPE = "MECHANICS_TYPE"


class ExerciseAttributeValueEnum(models.TextChoices):
    BODYWEIGHT = "BODYWEIGHT"
    STRENGTH = "STRENGTH"
    POWERLIFTING = "POWERLIFTING"
    CALISTHENIC = "CALISTHENIC"
    PLYOMETRICS = "PLYOMETRICS"
    STRETCHING = "STRETCHING"
    STRONGMAN = "STRONGMAN"
    CARDIO = "CARDIO"
    STABILIZATION = "STABILIZATION"
    POWER = "POWER"
    RESISTANCE = "RESISTANCE"
    CROSSFIT = "CROSSFIT"
    WEIGHTLIFTING = "WEIGHTLIFTING"
    BICEPS = "BICEPS"
    SHOULDERS = "SHOULDERS"
    CHEST = "CHEST"
    BACK = "BACK"
    GLUTES = "GLUTES"
    TRICEPS = "TRICEPS"
    HAMSTRINGS = "HAMSTRINGS"
    QUADRICEPS = "QUADRICEPS"
    FOREARMS = "FOREARMS"
    CALVES = "CALVES"
    TRAPS = "TRAPS"
    ABDOMINALS = "ABDOMINALS"
    NECK = "NECK"
    LATS = "LATS"
    ADDUCTORS = "ADDUCTORS"
    ABDUCTORS = "ABDUCTORS"
    OBLIQUES = "OBLIQUES"
    GROIN = "GROIN"
    FULL_BODY = "FULL_BODY"
    ROTATOR_CUFF = "ROTATOR_CUFF"
    HIP_FLEXOR = "HIP_FLEXOR"
    ACHILLES_TENDON = "ACHILLES_TENDON"
    FINGERS = "FINGERS"
    DUMBBELL = "DUMBBELL"
    KETTLEBELLS = "KETTLEBELLS"
    BARBELL = "BARBELL"
    SMITH_MACHINE = "SMITH_MACHINE"
    BODY_ONLY = "BODY_ONLY"
    OTHER = "OTHER"
    BANDS = "BANDS"
    EZ_BAR = "EZ_BAR"
    MACHINE = "MACHINE"
    DESK = "DESK"
    PULLUP_BAR = "PULLUP_BAR"
    NONE = "NONE"
    CABLE = "CABLE"
    MEDICINE_BALL = "MEDICINE_BALL"
    SWISS_BALL = "SWISS_BALL"
    FOAM_ROLL = "FOAM_ROLL"
    WEIGHT_PLATE = "WEIGHT_PLATE"
    TRX = "TRX"
    BOX = "BOX"
    ROPES = "ROPES"
    SPIN_BIKE = "SPIN_BIKE"
    STEP = "STEP"
    BOSU = "BOSU"
    TYRE = "TYRE"
    SANDBAG = "SANDBAG"
    POLE = "POLE"
    BENCH = "BENCH"
    WALL = "WALL"
    BAR = "BAR"
    RACK = "RACK"
    CAR = "CAR"
    SLED = "SLED"
    CHAIN = "CHAIN"
    SKIERG = "SKIERG"
    ROPE = "ROPE"
    NA = "NA"
    ISOLATION = "ISOLATION"
    COMPOUND = "COMPOUND"


class WorkoutSetType(models.TextChoices):
    TIME = "TIME"
    WEIGHT = "WEIGHT"
    REPS = "REPS"
    BODYWEIGHT = "BODYWEIGHT"
    NA = "NA"


class WorkoutSetUnit(models.TextChoices):
    kg = "kg"
    lbs = "lbs"


class SubscriptionStatus(models.TextChoices):
    ACTIVE = "ACTIVE"
    TRIAL = "TRIAL"
    CANCELLED = "CANCELLED"
    EXPIRED = "EXPIRED"
    PAUSED = "PAUSED"


class Platform(models.TextChoices):
    WEB = "WEB"
    IOS = "IOS"
    ANDROID = "ANDROID"


class PaymentProcessor(models.TextChoices):
    STRIPE = "STRIPE"
    PAYPAL = "PAYPAL"
    LEMONSQUEEZY = "LEMONSQUEEZY"
    PADDLE = "PADDLE"
    APPLE_PAY = "APPLE_PAY"
    GOOGLE_PAY = "GOOGLE_PAY"
    REVENUECAT = "REVENUECAT"
    NONE = "NONE"
    OTHER = "OTHER"


class ProgramLevel(models.TextChoices):
    BEGINNER = "BEGINNER"
    INTERMEDIATE = "INTERMEDIATE"
    ADVANCED = "ADVANCED"
    EXPERT = "EXPERT"


class ProgramVisibility(models.TextChoices):
    DRAFT = "DRAFT"
    PUBLISHED = "PUBLISHED"
    ARCHIVED = "ARCHIVED"


class User(models.Model):
    id = models.TextField(primary_key=True, default=None)
    first_name = models.TextField(db_column="firstName", default="")
    last_name = models.TextField(db_column="lastName", default="")
    name = models.TextField()
    email = models.TextField(unique=True)
    email_verified = models.BooleanField(db_column="emailVerified")
    image = models.TextField(null=True)
    locale = models.TextField(null=True, default="fr")
    created_at = UtcDateTimeField(db_column="createdAt")
    updated_at = UtcDateTimeField(db_column="updatedAt")
    role = models.TextField(choices=UserRole.choices, null=True, default=UserRole.user)
    banned = models.BooleanField(null=True, default=False)
    ban_reason = models.TextField(null=True, db_column="banReason")
    ban_expires = UtcDateTimeField(null=True, db_column="banExpires")
    is_premium = models.BooleanField(null=True, db_column="isPremium", default=False)
    onboarding_preferences = models.JSONField(null=True, db_column="onboardingPreferences")

    class Meta:
        db_table = "user"


class UserFavoriteExercise(models.Model):
    id = models.TextField(primary_key=True, default=None)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="favorite_exercises", db_column="userId", db_index=False)
    exercise = models.ForeignKey("Exercise", on_delete=models.CASCADE, related_name="favorites_by_users", db_column="exerciseId", db_index=False)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "user_favorite_exercises"
        constraints = [
            models.UniqueConstraint(fields=["user", "exercise"], name="user_favorite_exercises_userId_exerciseId_key"),
        ]


class Session(models.Model):
    id = models.TextField(primary_key=True, default=None)
    expires_at = UtcDateTimeField(db_column="expiresAt")
    token = models.TextField(unique=True)
    created_at = UtcDateTimeField(db_column="createdAt")
    updated_at = UtcDateTimeField(db_column="updatedAt")
    ip_address = models.TextField(null=True, db_column="ipAddress")
    user_agent = models.TextField(null=True, db_column="userAgent")
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="sessions", db_column="userId", db_index=False)
    impersonated_by = models.TextField(null=True, db_column="impersonatedBy")

    class Meta:
        db_table = "session"


class Account(models.Model):
    id = models.TextField(primary_key=True, default=None)
    account_id = models.TextField(db_column="accountId")
    provider_id = models.TextField(db_column="providerId")
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="accounts", db_column="userId", db_index=False)
    access_token = models.TextField(null=True, db_column="accessToken")
    refresh_token = models.TextField(null=True, db_column="refreshToken")
    id_token = models.TextField(null=True, db_column="idToken")
    access_token_expires_at = UtcDateTimeField(null=True, db_column="accessTokenExpiresAt")
    refresh_token_expires_at = UtcDateTimeField(null=True, db_column="refreshTokenExpiresAt")
    scope = models.TextField(null=True)
    password = models.TextField(null=True)
    created_at = UtcDateTimeField(db_column="createdAt")
    updated_at = UtcDateTimeField(db_column="updatedAt")

    class Meta:
        db_table = "account"


class Verification(models.Model):
    id = models.TextField(primary_key=True, default=None)
    identifier = models.TextField()
    value = models.TextField()
    expires_at = UtcDateTimeField(db_column="expiresAt")
    created_at = UtcDateTimeField(null=True, db_column="createdAt")
    updated_at = UtcDateTimeField(null=True, db_column="updatedAt")

    class Meta:
        db_table = "verification"


class Feedbacks(models.Model):
    id = models.TextField(primary_key=True, default=None)
    review = models.IntegerField()
    message = models.TextField()
    email = models.TextField(null=True)
    user = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="feedbacks", db_column="userId", null=True, db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "feedbacks"


class Exercise(models.Model):
    id = models.TextField(primary_key=True, default=None)
    name = models.TextField()
    name_en = models.TextField(null=True, db_column="nameEn")
    description = models.TextField(null=True)
    description_en = models.TextField(null=True, db_column="descriptionEn")
    full_video_url = models.TextField(null=True, db_column="fullVideoUrl")
    full_video_image_url = models.TextField(null=True, db_column="fullVideoImageUrl")
    introduction = models.TextField(null=True)
    introduction_en = models.TextField(null=True, db_column="introductionEn")
    slug = models.TextField(null=True, unique=True)
    slug_en = models.TextField(null=True, unique=True, db_column="slugEn")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "exercises"


class ExerciseAttributeName(models.Model):
    id = models.TextField(primary_key=True, default=None)
    name = models.TextField(choices=ExerciseAttributeNameEnum.choices, unique=True)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "exercise_attribute_names"


class ExerciseAttributeValue(models.Model):
    id = models.TextField(primary_key=True, default=None)
    attribute_name = models.ForeignKey("ExerciseAttributeName", on_delete=models.RESTRICT, related_name="values", db_column="attributeNameId", db_index=False)
    value = models.TextField(choices=ExerciseAttributeValueEnum.choices)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "exercise_attribute_values"
        constraints = [
            models.UniqueConstraint(fields=["attribute_name", "value"], name="exercise_attribute_values_attributeNameId_value_key"),
        ]


class ExerciseAttribute(models.Model):
    id = models.TextField(primary_key=True, default=None)
    exercise = models.ForeignKey("Exercise", on_delete=models.CASCADE, related_name="attributes", db_column="exerciseId", db_index=False)
    attribute_name = models.ForeignKey("ExerciseAttributeName", on_delete=models.RESTRICT, related_name="attributes", db_column="attributeNameId", db_index=False)
    attribute_value = models.ForeignKey("ExerciseAttributeValue", on_delete=models.RESTRICT, related_name="attributes", db_column="attributeValueId", db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "exercise_attributes"
        constraints = [
            models.UniqueConstraint(fields=["exercise", "attribute_name", "attribute_value"], name="exercise_attributes_exerciseId_attributeNameId_attributeValueId_key"),
        ]


class WorkoutSession(models.Model):
    id = models.TextField(primary_key=True, default=None)
    user = models.ForeignKey("User", on_delete=models.RESTRICT, related_name="workout_session", db_column="userId", db_index=False)
    started_at = UtcDateTimeField(db_column="startedAt")
    ended_at = UtcDateTimeField(null=True, db_column="endedAt")
    duration = models.IntegerField(null=True)
    muscles = ArrayField(models.TextField(choices=ExerciseAttributeValueEnum.choices), default=list)
    rating = models.IntegerField(null=True)
    rating_comment = models.TextField(null=True, db_column="ratingComment")

    class Meta:
        db_table = "workout_sessions"


class WorkoutSessionExercise(models.Model):
    id = models.TextField(primary_key=True, default=None)
    workout_session = models.ForeignKey("WorkoutSession", on_delete=models.CASCADE, related_name="exercises", db_column="workoutSessionId", db_index=False)
    exercise = models.ForeignKey("Exercise", on_delete=models.RESTRICT, related_name="workout_session_exercise", db_column="exerciseId", db_index=False)
    order = models.IntegerField()

    class Meta:
        db_table = "workout_session_exercises"


class WorkoutSet(models.Model):
    id = models.TextField(primary_key=True, default=None)
    workout_session_exercise = models.ForeignKey("WorkoutSessionExercise", on_delete=models.CASCADE, related_name="sets", db_column="workoutSessionExerciseId", db_index=False)
    set_index = models.IntegerField(db_column="setIndex")
    type = models.TextField(choices=WorkoutSetType.choices)
    types = ArrayField(models.TextField(choices=WorkoutSetType.choices), default=list)
    values_int = ArrayField(models.IntegerField(), db_column="valuesInt", default=list)
    values_sec = ArrayField(models.IntegerField(), db_column="valuesSec", default=list)
    units = ArrayField(models.TextField(choices=WorkoutSetUnit.choices), default=list)
    completed = models.BooleanField(default=False)

    class Meta:
        db_table = "workout_sets"


class SubscriptionPlan(models.Model):
    id = models.TextField(primary_key=True, default=None)
    price_monthly = models.DecimalField(max_digits=10, decimal_places=2, null=True, db_column="priceMonthly")
    price_yearly = models.DecimalField(max_digits=10, decimal_places=2, null=True, db_column="priceYearly")
    currency = models.TextField(default="EUR")
    interval = models.TextField(default="month")
    is_active = models.BooleanField(db_column="isActive", default=True)
    available_regions = ArrayField(models.TextField(), db_column="availableRegions", default=list)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "subscription_plans"


class PlanProviderMapping(models.Model):
    id = models.TextField(primary_key=True, default=None)
    plan = models.ForeignKey("SubscriptionPlan", on_delete=models.CASCADE, related_name="provider_mappings", db_column="planId", db_index=False)
    provider = models.TextField(choices=PaymentProcessor.choices)
    external_id = models.TextField(db_column="externalId")
    region = models.TextField(null=True)
    metadata = models.JSONField(null=True)
    is_active = models.BooleanField(db_column="isActive", default=True)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "plan_provider_mappings"
        constraints = [
            models.UniqueConstraint(fields=["plan", "provider", "region"], name="plan_provider_mappings_planId_provider_region_key"),
        ]
        indexes = [
            models.Index(fields=["provider", "external_id"]),
        ]


class Subscription(models.Model):
    id = models.TextField(primary_key=True, default=None)
    user = models.ForeignKey("User", on_delete=models.RESTRICT, related_name="subscriptions", db_column="userId", db_index=False)
    plan = models.ForeignKey("SubscriptionPlan", on_delete=models.RESTRICT, related_name="subscriptions", db_column="planId", db_index=False)
    revenue_cat_user_id = models.TextField(null=True, db_column="revenueCatUserId")
    status = models.TextField(choices=SubscriptionStatus.choices)
    started_at = UtcDateTimeField(db_column="startedAt")
    current_period_end = UtcDateTimeField(null=True, db_column="currentPeriodEnd")
    cancelled_at = UtcDateTimeField(null=True, db_column="cancelledAt")
    platform = models.TextField(choices=Platform.choices, null=True)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "subscriptions"
        constraints = [
            models.UniqueConstraint(fields=["user", "platform"], name="subscriptions_userId_platform_key"),
        ]


class License(models.Model):
    id = models.TextField(primary_key=True, default=None)
    key = models.TextField(unique=True)
    user = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="licenses", db_column="userId", null=True, db_index=False)
    valid_from = UtcDateTimeField(db_column="validFrom", default=timezone.now)
    valid_until = UtcDateTimeField(null=True, db_column="validUntil")
    max_users = models.IntegerField(null=True, db_column="maxUsers", default=1)
    features = models.JSONField(null=True)
    activated_at = UtcDateTimeField(null=True, db_column="activatedAt")
    last_checked_at = UtcDateTimeField(null=True, db_column="lastCheckedAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "licenses"


class RevenueCatWebhookEvent(models.Model):
    id = models.TextField(primary_key=True, default=None)
    event_type = models.TextField(db_column="eventType")
    event_timestamp = UtcDateTimeField(db_column="eventTimestamp")
    app_user_id = models.TextField(db_column="appUserId")
    environment = models.TextField()
    product_id = models.TextField(null=True, db_column="productId")
    transaction_id = models.TextField(null=True, db_column="transactionId")
    original_transaction_id = models.TextField(null=True, db_column="originalTransactionId")
    entitlement_ids = models.TextField(null=True, db_column="entitlementIds")
    processed = models.BooleanField(default=False)
    processing_error = models.TextField(null=True, db_column="processingError")
    retry_count = models.IntegerField(db_column="retryCount", default=0)
    raw_event_data = models.JSONField(db_column="rawEventData")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "revenuecat_webhook_events"
        indexes = [
            models.Index(fields=["app_user_id"]),
            models.Index(fields=["event_type"]),
            models.Index(fields=["processed"]),
            models.Index(fields=["event_timestamp"]),
        ]


class Program(models.Model):
    id = models.TextField(primary_key=True, default=None)
    slug = models.TextField(unique=True)
    slug_en = models.TextField(unique=True, db_column="slugEn")
    slug_es = models.TextField(unique=True, db_column="slugEs")
    slug_pt = models.TextField(unique=True, db_column="slugPt")
    slug_ru = models.TextField(unique=True, db_column="slugRu")
    slug_zh_cn = models.TextField(unique=True, db_column="slugZhCn")
    title = models.TextField()
    title_en = models.TextField(db_column="titleEn")
    title_es = models.TextField(db_column="titleEs")
    title_pt = models.TextField(db_column="titlePt")
    title_ru = models.TextField(db_column="titleRu")
    title_zh_cn = models.TextField(db_column="titleZhCn")
    description = models.TextField()
    description_en = models.TextField(db_column="descriptionEn")
    description_es = models.TextField(db_column="descriptionEs")
    description_pt = models.TextField(db_column="descriptionPt")
    description_ru = models.TextField(db_column="descriptionRu")
    description_zh_cn = models.TextField(db_column="descriptionZhCn")
    category = models.TextField()
    image = models.TextField()
    level = models.TextField(choices=ProgramLevel.choices)
    type = models.TextField(choices=ExerciseAttributeValueEnum.choices)
    duration_weeks = models.IntegerField(db_column="durationWeeks", default=4)
    sessions_per_week = models.IntegerField(db_column="sessionsPerWeek", default=3)
    session_duration_min = models.IntegerField(db_column="sessionDurationMin", default=30)
    equipment = ArrayField(models.TextField(choices=ExerciseAttributeValueEnum.choices), default=list)
    is_premium = models.BooleanField(db_column="isPremium", default=True)
    is_active = models.BooleanField(db_column="isActive", default=True)
    visibility = models.TextField(choices=ProgramVisibility.choices, default=ProgramVisibility.DRAFT)
    participant_count = models.IntegerField(db_column="participantCount", default=0)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "programs"


class ProgramCoach(models.Model):
    id = models.TextField(primary_key=True, default=None)
    program = models.ForeignKey("Program", on_delete=models.CASCADE, related_name="coaches", db_column="programId", db_index=False)
    name = models.TextField()
    image = models.TextField()
    order = models.IntegerField(default=0)

    class Meta:
        db_table = "program_coaches"


class ProgramWeek(models.Model):
    id = models.TextField(primary_key=True, default=None)
    program = models.ForeignKey("Program", on_delete=models.CASCADE, related_name="weeks", db_column="programId", db_index=False)
    week_number = models.IntegerField(db_column="weekNumber")
    title = models.TextField()
    title_en = models.TextField(db_column="titleEn")
    title_es = models.TextField(db_column="titleEs")
    title_pt = models.TextField(db_column="titlePt")
    title_ru = models.TextField(db_column="titleRu")
    title_zh_cn = models.TextField(db_column="titleZhCn")
    description = models.TextField()
    description_en = models.TextField(db_column="descriptionEn")
    description_es = models.TextField(db_column="descriptionEs")
    description_pt = models.TextField(db_column="descriptionPt")
    description_ru = models.TextField(db_column="descriptionRu")
    description_zh_cn = models.TextField(db_column="descriptionZhCn")

    class Meta:
        db_table = "program_weeks"
        constraints = [
            models.UniqueConstraint(fields=["program", "week_number"], name="program_weeks_programId_weekNumber_key"),
        ]


class ProgramSession(models.Model):
    id = models.TextField(primary_key=True, default=None)
    week = models.ForeignKey("ProgramWeek", on_delete=models.CASCADE, related_name="sessions", db_column="weekId", db_index=False)
    session_number = models.IntegerField(db_column="sessionNumber")
    title = models.TextField()
    title_en = models.TextField(db_column="titleEn")
    title_es = models.TextField(db_column="titleEs")
    title_pt = models.TextField(db_column="titlePt")
    title_ru = models.TextField(db_column="titleRu")
    title_zh_cn = models.TextField(db_column="titleZhCn")
    slug = models.TextField()
    slug_en = models.TextField(db_column="slugEn")
    slug_es = models.TextField(db_column="slugEs")
    slug_pt = models.TextField(db_column="slugPt")
    slug_ru = models.TextField(db_column="slugRu")
    slug_zh_cn = models.TextField(db_column="slugZhCn")
    description = models.TextField()
    description_en = models.TextField(db_column="descriptionEn")
    description_es = models.TextField(db_column="descriptionEs")
    description_pt = models.TextField(db_column="descriptionPt")
    description_ru = models.TextField(db_column="descriptionRu")
    description_zh_cn = models.TextField(db_column="descriptionZhCn")
    equipment = ArrayField(models.TextField(choices=ExerciseAttributeValueEnum.choices), default=list)
    estimated_minutes = models.IntegerField(db_column="estimatedMinutes")
    is_premium = models.BooleanField(db_column="isPremium", default=True)

    class Meta:
        db_table = "program_sessions"
        constraints = [
            models.UniqueConstraint(fields=["week", "session_number"], name="program_sessions_weekId_sessionNumber_key"),
            models.UniqueConstraint(fields=["week", "slug"], name="program_sessions_weekId_slug_key"),
            models.UniqueConstraint(fields=["week", "slug_en"], name="program_sessions_weekId_slugEn_key"),
            models.UniqueConstraint(fields=["week", "slug_es"], name="program_sessions_weekId_slugEs_key"),
            models.UniqueConstraint(fields=["week", "slug_pt"], name="program_sessions_weekId_slugPt_key"),
            models.UniqueConstraint(fields=["week", "slug_ru"], name="program_sessions_weekId_slugRu_key"),
            models.UniqueConstraint(fields=["week", "slug_zh_cn"], name="program_sessions_weekId_slugZhCn_key"),
        ]


class ProgramSessionExercise(models.Model):
    id = models.TextField(primary_key=True, default=None)
    session = models.ForeignKey("ProgramSession", on_delete=models.CASCADE, related_name="exercises", db_column="sessionId", db_index=False)
    exercise = models.ForeignKey("Exercise", on_delete=models.RESTRICT, related_name="program_session_exercise", db_column="exerciseId", db_index=False)
    order = models.IntegerField()
    instructions = models.TextField()
    instructions_en = models.TextField(db_column="instructionsEn")
    instructions_es = models.TextField(db_column="instructionsEs")
    instructions_pt = models.TextField(db_column="instructionsPt")
    instructions_ru = models.TextField(db_column="instructionsRu")
    instructions_zh_cn = models.TextField(db_column="instructionsZhCn")

    class Meta:
        db_table = "program_session_exercises"
        constraints = [
            models.UniqueConstraint(fields=["session", "order"], name="program_session_exercises_sessionId_order_key"),
        ]


class ProgramSuggestedSet(models.Model):
    id = models.TextField(primary_key=True, default=None)
    program_session_exercise = models.ForeignKey("ProgramSessionExercise", on_delete=models.CASCADE, related_name="suggested_sets", db_column="programSessionExerciseId", db_index=False)
    set_index = models.IntegerField(db_column="setIndex")
    types = ArrayField(models.TextField(choices=WorkoutSetType.choices), default=list)
    values_int = ArrayField(models.IntegerField(), db_column="valuesInt", default=list)
    values_sec = ArrayField(models.IntegerField(), db_column="valuesSec", default=list)
    units = ArrayField(models.TextField(choices=WorkoutSetUnit.choices), default=list)

    class Meta:
        db_table = "program_suggested_sets"
        constraints = [
            models.UniqueConstraint(fields=["program_session_exercise", "set_index"], name="program_suggested_sets_programSessionExerciseId_setIndex_key"),
        ]


class UserProgramEnrollment(models.Model):
    id = models.TextField(primary_key=True, default=None)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="program_enrollments", db_column="userId", db_index=False)
    program = models.ForeignKey("Program", on_delete=models.RESTRICT, related_name="enrollments", db_column="programId", db_index=False)
    enrolled_at = UtcDateTimeField(db_column="enrolledAt", default=timezone.now)
    current_week = models.IntegerField(db_column="currentWeek", default=1)
    current_session = models.IntegerField(db_column="currentSession", default=1)
    completed_sessions = models.IntegerField(db_column="completedSessions", default=0)
    is_active = models.BooleanField(db_column="isActive", default=True)
    completed_at = UtcDateTimeField(null=True, db_column="completedAt")

    class Meta:
        db_table = "user_program_enrollments"
        constraints = [
            models.UniqueConstraint(fields=["user", "program"], name="user_program_enrollments_userId_programId_key"),
        ]


class UserSessionProgress(models.Model):
    id = models.TextField(primary_key=True, default=None)
    enrollment = models.ForeignKey("UserProgramEnrollment", on_delete=models.CASCADE, related_name="session_progress", db_column="enrollmentId", db_index=False)
    session = models.ForeignKey("ProgramSession", on_delete=models.RESTRICT, related_name="user_progress", db_column="sessionId", db_index=False)
    started_at = UtcDateTimeField(db_column="startedAt", default=timezone.now)
    completed_at = UtcDateTimeField(null=True, db_column="completedAt")
    workout_session = models.OneToOneField("WorkoutSession", on_delete=models.SET_NULL, related_name="user_session_progress", db_column="workoutSessionId", null=True)

    class Meta:
        db_table = "user_session_progress"
        constraints = [
            models.UniqueConstraint(fields=["enrollment", "session"], name="user_session_progress_enrollmentId_sessionId_key"),
        ]
