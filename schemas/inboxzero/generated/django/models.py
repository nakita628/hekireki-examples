from datetime import datetime
from datetime import timezone as dt_timezone
from typing import Any, TypeVar

from django.conf import settings
from django.contrib.postgres.fields import ArrayField
from django.contrib.postgres.indexes import GinIndex
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


class SignupIntent(models.TextChoices):
    AI_ASSISTANT = "AI_ASSISTANT"
    MAIL_CLIENT = "MAIL_CLIENT"
    BOTH = "BOTH"


class ApiKeyScope(models.TextChoices):
    STATS_READ = "STATS_READ"
    RULES_READ = "RULES_READ"
    RULES_WRITE = "RULES_WRITE"
    SETTINGS_READ = "SETTINGS_READ"
    SETTINGS_WRITE = "SETTINGS_WRITE"
    ASSISTANT_CHAT = "ASSISTANT_CHAT"
    SENDERS_UNSUBSCRIBE = "SENDERS_UNSUBSCRIBE"


class ScheduledEmailStatus(models.TextChoices):
    PENDING = "PENDING"
    PROCESSING = "PROCESSING"
    SENT = "SENT"
    BLOCKED_AUTH = "BLOCKED_AUTH"
    UNCERTAIN = "UNCERTAIN"
    FAILED = "FAILED"
    CANCELLED = "CANCELLED"


class EmailReminderStatus(models.TextChoices):
    NONE = "NONE"
    PENDING = "PENDING"
    PROCESSING = "PROCESSING"
    COMPLETED = "COMPLETED"
    CANCELLED = "CANCELLED"


class MeetingJoinRule(models.TextChoices):
    ALL = "ALL"
    EXTERNAL_ONLY = "EXTERNAL_ONLY"
    HOST_ONLY = "HOST_ONLY"
    OFF = "OFF"


class MeetingRecordingStatus(models.TextChoices):
    PENDING = "PENDING"
    SCHEDULED = "SCHEDULED"
    CANCELLING = "CANCELLING"
    JOINING = "JOINING"
    IN_WAITING_ROOM = "IN_WAITING_ROOM"
    IN_CALL = "IN_CALL"
    RECORDING = "RECORDING"
    CALL_ENDED = "CALL_ENDED"
    DONE = "DONE"
    FAILED = "FAILED"
    CANCELLED = "CANCELLED"


class MeetingProcessingStatus(models.TextChoices):
    PENDING = "PENDING"
    PROCESSING = "PROCESSING"
    COMPLETED = "COMPLETED"
    FAILED = "FAILED"


class McpAuthType(models.TextChoices):
    OAUTH = "OAUTH"
    API_TOKEN = "API_TOKEN"
    NONE = "NONE"


class MobilePushPlatform(models.TextChoices):
    ANDROID = "android"
    IOS = "ios"


class MobilePushTokenType(models.TextChoices):
    EXPO = "expo"
    APNS = "apns"
    FCM = "fcm"


class ActionType(models.TextChoices):
    ARCHIVE = "ARCHIVE"
    LABEL = "LABEL"
    REPLY = "REPLY"
    SEND_EMAIL = "SEND_EMAIL"
    FORWARD = "FORWARD"
    DRAFT_EMAIL = "DRAFT_EMAIL"
    DRAFT_MESSAGING_CHANNEL = "DRAFT_MESSAGING_CHANNEL"
    NOTIFY_MESSAGING_CHANNEL = "NOTIFY_MESSAGING_CHANNEL"
    MARK_SPAM = "MARK_SPAM"
    CALL_WEBHOOK = "CALL_WEBHOOK"
    MARK_READ = "MARK_READ"
    STAR = "STAR"
    DELETE = "DELETE"
    DIGEST = "DIGEST"
    MOVE_FOLDER = "MOVE_FOLDER"
    NOTIFY_SENDER = "NOTIFY_SENDER"
    INTEGRATION = "INTEGRATION"


class MessagingMessageStatus(models.TextChoices):
    SENT = "SENT"
    DRAFT_SENT = "DRAFT_SENT"
    DRAFT_EDITED = "DRAFT_EDITED"
    DISMISSED = "DISMISSED"
    EXPIRED = "EXPIRED"
    FAILED = "FAILED"


class Frequency(models.TextChoices):
    NEVER = "NEVER"
    DAILY = "DAILY"
    WEEKLY = "WEEKLY"


class MailLayout(models.TextChoices):
    LIST = "LIST"
    SPLIT = "SPLIT"


class MailSplitFilterKind(models.TextChoices):
    UNREAD = "UNREAD"
    STARRED = "STARRED"
    LABEL = "LABEL"
    CATEGORY = "CATEGORY"
    FROM = "FROM"
    OLDER_THAN = "OLDER_THAN"


class DraftReplyConfidence(models.TextChoices):
    ALL_EMAILS = "ALL_EMAILS"
    STANDARD = "STANDARD"
    HIGH_CONFIDENCE = "HIGH_CONFIDENCE"


class DraftEmailStatus(models.TextChoices):
    PENDING = "PENDING"
    LIKELY_SENT = "LIKELY_SENT"
    REPLIED_WITHOUT_DRAFT = "REPLIED_WITHOUT_DRAFT"
    CLEANED_UP_UNUSED = "CLEANED_UP_UNUSED"
    MISSING_FROM_PROVIDER = "MISSING_FROM_PROVIDER"
    USER_EDITED = "USER_EDITED"
    CLEANUP_SKIPPED = "CLEANUP_SKIPPED"


class ReplyMemoryKind(models.TextChoices):
    FACT = "FACT"
    PREFERENCE = "PREFERENCE"
    PROCEDURE = "PROCEDURE"


class ReplyMemoryScopeType(models.TextChoices):
    GLOBAL = "GLOBAL"
    SENDER = "SENDER"
    DOMAIN = "DOMAIN"
    TOPIC = "TOPIC"


class NewsletterStatus(models.TextChoices):
    APPROVED = "APPROVED"
    UNSUBSCRIBED = "UNSUBSCRIBED"
    AUTO_ARCHIVED = "AUTO_ARCHIVED"


class ColdEmailStatus(models.TextChoices):
    AI_LABELED_COLD = "AI_LABELED_COLD"
    USER_REJECTED_COLD = "USER_REJECTED_COLD"


class DocumentFilingStatus(models.TextChoices):
    PROCESSING = "PROCESSING"
    PENDING = "PENDING"
    FILED = "FILED"
    REJECTED = "REJECTED"
    ERROR = "ERROR"
    PREVIEW = "PREVIEW"


class AttachmentSourceType(models.TextChoices):
    FILE = "FILE"
    FOLDER = "FOLDER"


class ColdEmailSetting(models.TextChoices):
    DISABLED = "DISABLED"
    LIST = "LIST"
    LABEL = "LABEL"
    ARCHIVE_AND_LABEL = "ARCHIVE_AND_LABEL"
    ARCHIVE_AND_READ_AND_LABEL = "ARCHIVE_AND_READ_AND_LABEL"


class PremiumTier(models.TextChoices):
    BASIC_MONTHLY = "BASIC_MONTHLY"
    BASIC_ANNUALLY = "BASIC_ANNUALLY"
    PRO_MONTHLY = "PRO_MONTHLY"
    PRO_ANNUALLY = "PRO_ANNUALLY"
    STARTER_MONTHLY = "BUSINESS_MONTHLY"
    STARTER_ANNUALLY = "BUSINESS_ANNUALLY"
    PLUS_MONTHLY = "PLUS_MONTHLY"
    PLUS_ANNUALLY = "PLUS_ANNUALLY"
    PROFESSIONAL_MONTHLY = "BUSINESS_PLUS_MONTHLY"
    PROFESSIONAL_ANNUALLY = "BUSINESS_PLUS_ANNUALLY"
    COPILOT_MONTHLY = "COPILOT_MONTHLY"
    LIFETIME = "LIFETIME"


class ExecutedRuleStatus(models.TextChoices):
    APPLIED = "APPLIED"
    APPLYING = "APPLYING"
    REJECTED = "REJECTED"
    PENDING = "PENDING"
    SKIPPED = "SKIPPED"
    ERROR = "ERROR"


class ExecutedActionStatus(models.TextChoices):
    SUCCEEDED = "SUCCEEDED"
    FAILED = "FAILED"
    SKIPPED = "SKIPPED"


class GroupItemType(models.TextChoices):
    FROM = "FROM"
    SUBJECT = "SUBJECT"
    BODY = "BODY"


class CategoryFilterType(models.TextChoices):
    INCLUDE = "INCLUDE"
    EXCLUDE = "EXCLUDE"


class LogicalOperator(models.TextChoices):
    AND = "AND"
    OR = "OR"


class ThreadTrackerType(models.TextChoices):
    AWAITING = "AWAITING"
    NEEDS_REPLY = "NEEDS_REPLY"
    NEEDS_ACTION = "NEEDS_ACTION"


class ProcessorType(models.TextChoices):
    LEMON_SQUEEZY = "LEMON_SQUEEZY"
    STRIPE = "STRIPE"


class CleanAction(models.TextChoices):
    ARCHIVE = "ARCHIVE"
    MARK_READ = "MARK_READ"


class SystemType(models.TextChoices):
    TO_REPLY = "TO_REPLY"
    FYI = "FYI"
    AWAITING_REPLY = "AWAITING_REPLY"
    ACTIONED = "ACTIONED"
    COLD_EMAIL = "COLD_EMAIL"
    NEWSLETTER = "NEWSLETTER"
    MARKETING = "MARKETING"
    CALENDAR = "CALENDAR"
    RECEIPT = "RECEIPT"
    NOTIFICATION = "NOTIFICATION"
    OTP = "OTP"


class ReferralStatus(models.TextChoices):
    PENDING = "PENDING"
    COMPLETED = "COMPLETED"


class DigestStatus(models.TextChoices):
    PENDING = "PENDING"
    PROCESSING = "PROCESSING"
    SENT = "SENT"
    FAILED = "FAILED"


class ScheduledActionStatus(models.TextChoices):
    PENDING = "PENDING"
    EXECUTING = "EXECUTING"
    COMPLETED = "COMPLETED"
    FAILED = "FAILED"
    CANCELLED = "CANCELLED"


class SnoozedThreadStatus(models.TextChoices):
    PREPARING = "PREPARING"
    PENDING = "PENDING"
    EXECUTING = "EXECUTING"
    COMPLETED = "COMPLETED"
    CANCELLED = "CANCELLED"


class EmailSendOperationStatus(models.TextChoices):
    PROCESSING = "PROCESSING"
    SENT = "SENT"
    UNCERTAIN = "UNCERTAIN"


class SchedulingStatus(models.TextChoices):
    PENDING = "PENDING"
    SCHEDULED = "SCHEDULED"
    FAILED = "FAILED"


class MeetingBriefingStatus(models.TextChoices):
    PENDING = "PENDING"
    SENT = "SENT"
    FAILED = "FAILED"
    SKIPPED = "SKIPPED"


class BookingLinkLocationType(models.TextChoices):
    IN_PERSON = "IN_PERSON"
    GOOGLE_MEET = "GOOGLE_MEET"
    MICROSOFT_TEAMS = "MICROSOFT_TEAMS"
    PHONE = "PHONE"
    CUSTOM = "CUSTOM"


class BookingStatus(models.TextChoices):
    PENDING_PROVIDER_EVENT = "PENDING_PROVIDER_EVENT"
    CONFIRMED = "CONFIRMED"
    CANCELED = "CANCELED"
    FAILED = "FAILED"


class GroupItemSource(models.TextChoices):
    AI = "AI"
    USER = "USER"
    LABEL_REMOVED = "LABEL_REMOVED"
    LABEL_ADDED = "LABEL_ADDED"


class ClassificationFeedbackEventType(models.TextChoices):
    LABEL_ADDED = "LABEL_ADDED"
    LABEL_REMOVED = "LABEL_REMOVED"


class MessagingProvider(models.TextChoices):
    SLACK = "SLACK"
    TEAMS = "TEAMS"
    TELEGRAM = "TELEGRAM"


class MessagingRoutePurpose(models.TextChoices):
    RULE_NOTIFICATIONS = "RULE_NOTIFICATIONS"
    SCHEDULED_CHECK_INS = "SCHEDULED_CHECK_INS"
    MEETING_BRIEFS = "MEETING_BRIEFS"
    DOCUMENT_FILINGS = "DOCUMENT_FILINGS"
    DIGESTS = "DIGESTS"
    FOLLOW_UPS = "FOLLOW_UPS"


class MessagingRouteTargetType(models.TextChoices):
    CHANNEL = "CHANNEL"
    DIRECT_MESSAGE = "DIRECT_MESSAGE"


class AutomationJobRunStatus(models.TextChoices):
    PENDING = "PENDING"
    RUNNING = "RUNNING"
    SENT = "SENT"
    SKIPPED = "SKIPPED"
    FAILED = "FAILED"


class ConversationStatus(models.TextChoices):
    ACTIVE = "ACTIVE"
    STOPPED = "STOPPED"


class ConversationActivityKind(models.TextChoices):
    INVITED = "INVITED"
    COMMENT = "COMMENT"
    MENTION = "MENTION"


class Account(models.Model):
    password = models.TextField(null=True)
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="accounts", db_column="userId", db_index=False)
    provider = models.TextField()
    type = models.TextField(default="oidc")
    provider_account_id = models.TextField(db_column="providerAccountId")
    refresh_token = models.TextField(null=True)
    refresh_token_expires_at = UtcDateTimeField(null=True, db_column="refreshTokenExpiresAt")
    access_token = models.TextField(null=True)
    expires_at = UtcDateTimeField(null=True, default=timezone.now)
    disconnected_at = UtcDateTimeField(null=True, db_column="disconnectedAt")
    token_type = models.TextField(null=True)
    scope = models.TextField(null=True)
    id_token = models.TextField(null=True)
    session_state = models.TextField(null=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Account"
        constraints = [
            models.UniqueConstraint(fields=["provider", "provider_account_id"], name="Account_provider_providerAccountId_key"),
        ]
        indexes = [
            models.Index(fields=["user"]),
        ]


class Session(models.Model):
    id = models.TextField(primary_key=True, default=None)
    session_token = models.TextField(unique=True, db_column="sessionToken")
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="sessions", db_column="userId", db_index=False)
    expires = UtcDateTimeField()
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    email_otp = models.BooleanField(db_column="emailOtp", default=False)
    email_otp_version = models.IntegerField(db_column="emailOtpVersion", default=0)
    ip_address = models.TextField(null=True, db_column="ipAddress")
    user_agent = models.TextField(null=True, db_column="userAgent")
    active_organization = models.ForeignKey("Organization", on_delete=models.SET_NULL, related_name="sessions", db_column="activeOrganizationId", null=True, db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Session"
        indexes = [
            models.Index(fields=["user"]),
            models.Index(fields=["active_organization"]),
        ]


class User(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    name = models.TextField(null=True)
    email = models.TextField(unique=True)
    email_verified = models.BooleanField(null=True, db_column="emailVerified", default=False)
    image = models.TextField(null=True)
    completed_onboarding_at = UtcDateTimeField(null=True, db_column="completedOnboardingAt")
    signup_intent = models.TextField(choices=SignupIntent.choices, null=True, db_column="signupIntent")
    onboarding_answers = models.JSONField(null=True, db_column="onboardingAnswers")
    last_login = UtcDateTimeField(null=True, db_column="lastLogin")
    utms = models.JSONField(null=True)
    error_messages = models.JSONField(null=True, db_column="errorMessages")
    announcement_dismissed_at = UtcDateTimeField(null=True, db_column="announcementDismissedAt")
    dismissed_hints = ArrayField(models.TextField(), db_column="dismissedHints", default=list)
    survey_features = ArrayField(models.TextField(), db_column="surveyFeatures")
    survey_role = models.TextField(null=True, db_column="surveyRole")
    survey_goal = models.TextField(null=True, db_column="surveyGoal")
    survey_company_size = models.IntegerField(null=True, db_column="surveyCompanySize")
    survey_source = models.TextField(null=True, db_column="surveySource")
    survey_improvements = models.TextField(null=True, db_column="surveyImprovements")
    mcp_server_enabled = models.BooleanField(db_column="mcpServerEnabled", default=False)
    mcp_token_version = models.IntegerField(db_column="mcpTokenVersion", default=0)
    email_otp_enabled = models.BooleanField(db_column="emailOtpEnabled", default=False)
    email_otp_version = models.IntegerField(db_column="emailOtpVersion", default=0)
    ai_provider = models.TextField(null=True, db_column="aiProvider")
    ai_model = models.TextField(null=True, db_column="aiModel")
    ai_api_key = models.TextField(null=True, db_column="aiApiKey")
    decision_model_enabled = models.BooleanField(null=True, db_column="decisionModelEnabled")
    webhook_secret = models.TextField(null=True, db_column="webhookSecret")
    referral_code = models.TextField(null=True, unique=True, db_column="referralCode")
    premium = models.ForeignKey("Premium", on_delete=models.SET_NULL, related_name="users", db_column="premiumId", null=True, db_index=False)
    premium_admin = models.ForeignKey("Premium", on_delete=models.SET_NULL, related_name="admins", db_column="premiumAdminId", null=True, db_index=False)
    scim_access_disabled = models.BooleanField(db_column="scimAccessDisabled", default=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "User"


class EmailAccount(models.Model):
    id = models.TextField(primary_key=True, default=None)
    email = models.TextField(unique=True)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    name = models.TextField(null=True)
    image = models.TextField(null=True)
    sort_order = models.IntegerField(null=True, db_column="sortOrder")
    about = models.TextField(null=True)
    writing_style = models.TextField(null=True, db_column="writingStyle")
    signature = models.TextField(null=True)
    sent_signature_scan = models.JSONField(null=True, db_column="sentSignatureScan")
    sent_signature_scanned_at = UtcDateTimeField(null=True, db_column="sentSignatureScannedAt")
    include_referral_signature = models.BooleanField(db_column="includeReferralSignature", default=False)
    include_sent_with_signature = models.BooleanField(db_column="includeSentWithSignature", default=True)
    sent_message_open_tracking_enabled = models.BooleanField(db_column="sentMessageOpenTrackingEnabled", default=True)
    watch_emails_expiration_date = UtcDateTimeField(null=True, db_column="watchEmailsExpirationDate")
    watch_emails_subscription_id = models.TextField(null=True, db_column="watchEmailsSubscriptionId")
    watch_emails_subscription_history = models.JSONField(null=True, db_column="watchEmailsSubscriptionHistory")
    last_synced_history_id = models.TextField(null=True, db_column="lastSyncedHistoryId")
    behavior_profile = models.JSONField(null=True, db_column="behaviorProfile")
    persona_analysis = models.JSONField(null=True, db_column="personaAnalysis")
    role = models.TextField(null=True)
    timezone = models.TextField(null=True)
    calendar_booking_link = models.TextField(null=True, db_column="calendarBookingLink")
    draft_docs_urls = ArrayField(models.TextField(), db_column="draftDocsUrls", default=list)
    stats_email_frequency = models.TextField(choices=Frequency.choices, db_column="statsEmailFrequency", default=Frequency.WEEKLY)
    summary_email_frequency = models.TextField(choices=Frequency.choices, db_column="summaryEmailFrequency", default=Frequency.WEEKLY)
    last_summary_email_at = UtcDateTimeField(null=True, db_column="lastSummaryEmailAt")
    last_inbox_health_email_at = UtcDateTimeField(null=True, db_column="lastInboxHealthEmailAt")
    cold_email_blocker = models.TextField(choices=ColdEmailSetting.choices, null=True, db_column="coldEmailBlocker")
    cold_email_digest = models.BooleanField(db_column="coldEmailDigest", default=False)
    cold_email_prompt = models.TextField(null=True, db_column="coldEmailPrompt")
    rules_prompt = models.TextField(null=True, db_column="rulesPrompt")
    auto_categorize_senders = models.BooleanField(db_column="autoCategorizeSenders", default=False)
    multi_rule_selection_enabled = models.BooleanField(db_column="multiRuleSelectionEnabled", default=False)
    sensitive_data_policy = models.TextField(null=True, db_column="sensitiveDataPolicy")
    draft_reply_confidence = models.TextField(choices=DraftReplyConfidence.choices, db_column="draftReplyConfidence", default=DraftReplyConfidence.ALL_EMAILS)
    draft_cleanup_days = models.IntegerField(null=True, db_column="draftCleanupDays", default=14)
    allow_hidden_ai_draft_links = models.BooleanField(db_column="allowHiddenAiDraftLinks", default=False)
    rules_revision = models.IntegerField(db_column="rulesRevision", default=0)
    mail_layout = models.TextField(choices=MailLayout.choices, db_column="mailLayout", default=MailLayout.LIST)
    mail_expanded_preview = models.BooleanField(db_column="mailExpandedPreview", default=False)
    mail_bundle_inbox = models.BooleanField(null=True, db_column="mailBundleInbox")
    include_in_all_accounts = models.BooleanField(db_column="includeInAllAccounts", default=True)
    meeting_briefings_enabled = models.BooleanField(db_column="meetingBriefingsEnabled", default=False)
    meeting_briefings_minutes_before = models.IntegerField(db_column="meetingBriefingsMinutesBefore", default=240)
    meeting_briefs_send_email = models.BooleanField(db_column="meetingBriefsSendEmail", default=True)
    meeting_recorder_enabled = models.BooleanField(db_column="meetingRecorderEnabled", default=False)
    meeting_recorder_join_rule = models.TextField(choices=MeetingJoinRule.choices, db_column="meetingRecorderJoinRule", default=MeetingJoinRule.ALL)
    meeting_recorder_recap_email_enabled = models.BooleanField(db_column="meetingRecorderRecapEmailEnabled", default=True)
    meeting_recorder_follow_up_draft_enabled = models.BooleanField(db_column="meetingRecorderFollowUpDraftEnabled", default=True)
    filing_enabled = models.BooleanField(db_column="filingEnabled", default=False)
    filing_prompt = models.TextField(null=True, db_column="filingPrompt")
    filing_confirmation_send_email = models.BooleanField(db_column="filingConfirmationSendEmail", default=True)
    follow_up_awaiting_reply_days = models.FloatField(null=True, db_column="followUpAwaitingReplyDays")
    follow_up_needs_reply_days = models.FloatField(null=True, db_column="followUpNeedsReplyDays")
    follow_up_auto_draft_enabled = models.BooleanField(db_column="followUpAutoDraftEnabled", default=True)
    digest_send_email = models.BooleanField(db_column="digestSendEmail", default=True)
    learned_writing_style = models.TextField(null=True, db_column="learnedWritingStyle")
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="email_accounts", db_column="userId", db_index=False)
    account = models.OneToOneField("Account", on_delete=models.CASCADE, related_name="email_account", db_column="accountId")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "EmailAccount"
        indexes = [
            models.Index(fields=["user"]),
            models.Index(fields=["last_summary_email_at"]),
            models.Index(fields=["last_inbox_health_email_at"]),
            models.Index(fields=["watch_emails_subscription_id"]),
            GinIndex(fields=["watch_emails_subscription_history"]),
        ]


class MailSplit(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    name = models.TextField()
    order = models.IntegerField()
    match_all = models.BooleanField(db_column="matchAll", default=True)
    bundle_in_inbox = models.BooleanField(db_column="bundleInInbox", default=False)
    email_account = models.ForeignKey("EmailAccount", on_delete=models.CASCADE, related_name="mail_splits", db_column="emailAccountId", db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "MailSplit"
        constraints = [
            models.UniqueConstraint(fields=["email_account", "name"], name="MailSplit_emailAccountId_name_key"),
        ]
        indexes = [
            models.Index(fields=["email_account", "order"]),
        ]


class MailSplitFilter(models.Model):
    id = models.TextField(primary_key=True, default=None)
    kind = models.TextField(choices=MailSplitFilterKind.choices)
    value = models.TextField(null=True)
    order = models.IntegerField()
    mail_split = models.ForeignKey("MailSplit", on_delete=models.CASCADE, related_name="filters", db_column="mailSplitId", db_index=False)

    class Meta:
        db_table = "MailSplitFilter"
        constraints = [
            models.UniqueConstraint(fields=["mail_split", "order"], name="MailSplitFilter_mailSplitId_order_key"),
        ]
        indexes = [
            models.Index(fields=["mail_split"]),
            models.Index(fields=["kind", "value"]),
        ]


class Organization(models.Model):
    id = models.TextField(primary_key=True, default=None)
    name = models.TextField()
    slug = models.TextField(unique=True)
    logo = models.TextField(null=True)
    metadata = models.JSONField(null=True)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Organization"


class OrganizationRule(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    organization = models.ForeignKey("Organization", on_delete=models.CASCADE, related_name="rules", db_column="organizationId", db_index=False)
    enabled = models.BooleanField(default=True)
    name = models.TextField()
    instructions = models.TextField(null=True)
    run_on_threads = models.BooleanField(db_column="runOnThreads", default=False)
    conditional_operator = models.TextField(choices=LogicalOperator.choices, db_column="conditionalOperator", default=LogicalOperator.AND)
    from_field = models.TextField(null=True, db_column="from")
    to = models.TextField(null=True)
    subject = models.TextField(null=True)
    body = models.TextField(null=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "OrganizationRule"
        constraints = [
            models.UniqueConstraint(fields=["organization", "name"], name="OrganizationRule_organizationId_name_key"),
        ]
        indexes = [
            models.Index(fields=["organization", "enabled"]),
        ]


class OrganizationRuleAction(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    organization_rule = models.ForeignKey("OrganizationRule", on_delete=models.CASCADE, related_name="actions", db_column="organizationRuleId", db_index=False)
    type = models.TextField(choices=ActionType.choices)
    label = models.TextField(null=True)
    subject = models.TextField(null=True)
    content = models.TextField(null=True)
    to = models.TextField(null=True)
    cc = models.TextField(null=True)
    bcc = models.TextField(null=True)
    url = models.TextField(null=True)
    folder_name = models.TextField(null=True, db_column="folderName")
    delay_in_minutes = models.IntegerField(null=True, db_column="delayInMinutes")
    static_attachments = models.JSONField(null=True, db_column="staticAttachments")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "OrganizationRuleAction"
        indexes = [
            models.Index(fields=["organization_rule"]),
        ]


class Member(models.Model):
    id = models.TextField(primary_key=True, default=None)
    organization = models.ForeignKey("Organization", on_delete=models.CASCADE, related_name="members", db_column="organizationId", db_index=False)
    email_account = models.ForeignKey("EmailAccount", on_delete=models.RESTRICT, related_name="members", db_column="emailAccountId", db_index=False)
    role = models.TextField(default="member")
    allow_org_admin_analytics = models.BooleanField(db_column="allowOrgAdminAnalytics", default=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Member"
        constraints = [
            models.UniqueConstraint(fields=["organization", "email_account"], name="Member_organizationId_emailAccountId_key"),
            models.UniqueConstraint(fields=["email_account"], name="Member_emailAccountId_key"),
        ]


class Conversation(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    organization = models.ForeignKey("Organization", on_delete=models.CASCADE, related_name="conversations", db_column="organizationId", db_index=False)
    publisher = models.ForeignKey("Member", on_delete=models.CASCADE, related_name="conversations", db_column="publisherId", db_index=False)
    thread_id = models.TextField(db_column="threadId")
    status = models.TextField(choices=ConversationStatus.choices, default=ConversationStatus.ACTIVE)
    revision = models.IntegerField(default=0)
    generation = models.IntegerField(default=1)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Conversation"
        constraints = [
            models.UniqueConstraint(fields=["publisher", "thread_id"], name="Conversation_publisherId_threadId_key"),
        ]
        indexes = [
            models.Index(fields=["organization"]),
        ]


class ConversationParticipant(models.Model):
    id = models.TextField(primary_key=True, default=None)
    conversation = models.ForeignKey("Conversation", on_delete=models.CASCADE, related_name="participants", db_column="conversationId", db_index=False)
    member = models.ForeignKey("Member", on_delete=models.CASCADE, related_name="conversation_participants", db_column="memberId", db_index=False)
    generation = models.IntegerField()
    active = models.BooleanField(default=True)
    read_revision = models.IntegerField(db_column="readRevision", default=0)
    muted = models.BooleanField(default=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "ConversationParticipant"
        constraints = [
            models.UniqueConstraint(fields=["conversation", "member", "generation"], name="ConversationParticipant_conversationId_memberId_generation_key"),
        ]
        indexes = [
            models.Index(fields=["member"]),
        ]


class ConversationComment(models.Model):
    id = models.TextField(primary_key=True, default=None)
    conversation = models.ForeignKey("Conversation", on_delete=models.CASCADE, related_name="comments", db_column="conversationId", db_index=False)
    author = models.ForeignKey("Member", on_delete=models.SET_NULL, related_name="conversation_comments", db_column="authorMemberId", null=True, db_index=False)
    client_mutation_id = models.TextField(db_column="clientMutationId")
    body = models.TextField()
    revision = models.IntegerField()
    deleted_at = UtcDateTimeField(null=True, db_column="deletedAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "ConversationComment"
        constraints = [
            models.UniqueConstraint(fields=["conversation", "revision"], name="ConversationComment_conversationId_revision_key"),
            models.UniqueConstraint(fields=["conversation", "author", "client_mutation_id"], name="ConversationComment_conversationId_authorMemberId_clientMutationId_key"),
        ]


class ConversationActivity(models.Model):
    id = models.TextField(primary_key=True, default=None)
    participant = models.ForeignKey("ConversationParticipant", on_delete=models.CASCADE, related_name="activity", db_column="participantId", db_index=False)
    comment = models.ForeignKey("ConversationComment", on_delete=models.SET_NULL, related_name="activity", db_column="commentId", null=True, db_index=False)
    kind = models.TextField(choices=ConversationActivityKind.choices)
    revision = models.IntegerField()
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "ConversationActivity"
        constraints = [
            models.UniqueConstraint(fields=["participant", "kind", "revision"], name="ConversationActivity_participantId_kind_revision_key"),
        ]
        indexes = [
            models.Index(fields=["participant", "created_at", "id"]),
        ]


class Invitation(models.Model):
    id = models.TextField(primary_key=True, default=None)
    organization = models.ForeignKey("Organization", on_delete=models.CASCADE, related_name="invitations", db_column="organizationId", db_index=False)
    email = models.TextField()
    role = models.TextField(null=True)
    status = models.TextField()
    expires_at = UtcDateTimeField(db_column="expiresAt")
    inviter = models.ForeignKey("EmailAccount", on_delete=models.CASCADE, related_name="invitations", db_column="inviterId", db_index=False)

    class Meta:
        db_table = "invitation"
        indexes = [
            models.Index(fields=["organization"]),
            models.Index(fields=["inviter"]),
        ]


class Verification(models.Model):
    id = models.TextField(primary_key=True, default=None)
    identifier = models.TextField()
    value = models.TextField()
    expires_at = UtcDateTimeField(db_column="expiresAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "verification"


class SsoProvider(models.Model):
    user = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="sso_providers", db_column="userId", null=True, db_index=False)
    id = models.TextField(primary_key=True, default=None)
    issuer = models.TextField()
    oidc_config = models.TextField(null=True, db_column="oidcConfig")
    saml_config = models.TextField(null=True, db_column="samlConfig")
    email_account = models.ForeignKey("EmailAccount", on_delete=models.CASCADE, related_name="ssoproviders", db_column="emailAccountId", null=True, db_index=False)
    provider_id = models.TextField(unique=True, db_column="providerId")
    organization = models.OneToOneField("Organization", on_delete=models.CASCADE, related_name="sso_provider", db_column="organizationId", null=True)
    domain = models.TextField()

    class Meta:
        db_table = "ssoProvider"
        indexes = [
            models.Index(fields=["email_account"]),
        ]


class Digest(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    email_account = models.ForeignKey("EmailAccount", on_delete=models.CASCADE, related_name="digests", db_column="emailAccountId", db_index=False)
    sent_at = UtcDateTimeField(null=True, db_column="sentAt")
    status = models.TextField(choices=DigestStatus.choices, default=DigestStatus.PENDING)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Digest"
        indexes = [
            models.Index(fields=["email_account"]),
        ]


class DigestItem(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    message_id = models.TextField(db_column="messageId")
    thread_id = models.TextField(db_column="threadId")
    content = models.TextField()
    digest = models.ForeignKey("Digest", on_delete=models.CASCADE, related_name="items", db_column="digestId", db_index=False)
    action = models.ForeignKey("ExecutedAction", on_delete=models.CASCADE, related_name="digest_items", db_column="actionId", null=True, db_index=False)
    cold_email = models.ForeignKey("ColdEmail", on_delete=models.SET_NULL, related_name="digest_items", db_column="coldEmailId", null=True, db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "DigestItem"
        constraints = [
            models.UniqueConstraint(fields=["digest", "thread_id", "message_id"], name="DigestItem_digestId_threadId_messageId_key"),
        ]
        indexes = [
            models.Index(fields=["action"]),
            models.Index(fields=["cold_email"]),
        ]


class Schedule(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    interval_days = models.IntegerField(null=True, db_column="intervalDays")
    occurrences = models.IntegerField(null=True)
    days_of_week = models.IntegerField(null=True, db_column="daysOfWeek")
    time_of_day = UtcDateTimeField(null=True, db_column="timeOfDay")
    email_account = models.OneToOneField("EmailAccount", on_delete=models.CASCADE, related_name="digest_schedule", db_column="emailAccountId")
    last_occurrence_at = UtcDateTimeField(null=True, db_column="lastOccurrenceAt")
    next_occurrence_at = UtcDateTimeField(null=True, db_column="nextOccurrenceAt")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Schedule"
        constraints = [
            models.UniqueConstraint(fields=["email_account"], name="Schedule_emailAccountId_key"),
        ]


class Premium(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    pending_invites = ArrayField(models.TextField(), db_column="pendingInvites")
    lemon_squeezy_renews_at = UtcDateTimeField(null=True, db_column="lemonSqueezyRenewsAt")
    lemon_squeezy_customer_id = models.IntegerField(null=True, db_column="lemonSqueezyCustomerId")
    lemon_squeezy_subscription_id = models.IntegerField(null=True, db_column="lemonSqueezySubscriptionId")
    lemon_squeezy_subscription_item_id = models.IntegerField(null=True, db_column="lemonSqueezySubscriptionItemId")
    lemon_squeezy_order_id = models.IntegerField(null=True, db_column="lemonSqueezyOrderId")
    lemon_squeezy_product_id = models.IntegerField(null=True, db_column="lemonSqueezyProductId")
    lemon_squeezy_variant_id = models.IntegerField(null=True, db_column="lemonSqueezyVariantId")
    lemon_license_key = models.TextField(null=True, db_column="lemonLicenseKey")
    lemon_license_instance_id = models.TextField(null=True, db_column="lemonLicenseInstanceId")
    lemon_subscription_status = models.TextField(null=True, db_column="lemonSubscriptionStatus")
    stripe_customer_id = models.TextField(null=True, unique=True, db_column="stripeCustomerId")
    stripe_subscription_id = models.TextField(null=True, unique=True, db_column="stripeSubscriptionId")
    stripe_subscription_item_id = models.TextField(null=True, unique=True, db_column="stripeSubscriptionItemId")
    stripe_price_id = models.TextField(null=True, db_column="stripePriceId")
    stripe_product_id = models.TextField(null=True, db_column="stripeProductId")
    stripe_subscription_status = models.TextField(null=True, db_column="stripeSubscriptionStatus")
    stripe_cancel_at = UtcDateTimeField(null=True, db_column="stripeCancelAt")
    stripe_cancel_at_period_end = models.BooleanField(null=True, db_column="stripeCancelAtPeriodEnd")
    stripe_renews_at = UtcDateTimeField(null=True, db_column="stripeRenewsAt")
    stripe_trial_end = UtcDateTimeField(null=True, db_column="stripeTrialEnd")
    stripe_trial_conversion_invoice_id = models.TextField(null=True, db_column="stripeTrialConversionInvoiceId")
    stripe_trial_converted_at = UtcDateTimeField(null=True, db_column="stripeTrialConvertedAt")
    stripe_cancellation_initiated_at = UtcDateTimeField(null=True, db_column="stripeCancellationInitiatedAt")
    stripe_canceled_at = UtcDateTimeField(null=True, db_column="stripeCanceledAt")
    stripe_ended_at = UtcDateTimeField(null=True, db_column="stripeEndedAt")
    stripe_cancellation_feedback = models.TextField(null=True, db_column="stripeCancellationFeedback")
    stripe_cancellation_comment = models.TextField(null=True, db_column="stripeCancellationComment")
    stripe_ai_overage_last_invoice_id = models.TextField(null=True, db_column="stripeAiOverageLastInvoiceId")
    stripe_ai_overage_last_period_end = UtcDateTimeField(null=True, db_column="stripeAiOverageLastPeriodEnd")
    stripe_ai_overage_last_units = models.IntegerField(null=True, db_column="stripeAiOverageLastUnits")
    stripe_invoice_emails_enabled = models.BooleanField(db_column="stripeInvoiceEmailsEnabled", default=False)
    apple_original_transaction_id = models.TextField(null=True, unique=True, db_column="appleOriginalTransactionId")
    apple_latest_transaction_id = models.TextField(null=True, unique=True, db_column="appleLatestTransactionId")
    apple_product_id = models.TextField(null=True, db_column="appleProductId")
    apple_subscription_group_identifier = models.TextField(null=True, db_column="appleSubscriptionGroupIdentifier")
    apple_app_account_token = models.TextField(null=True, db_column="appleAppAccountToken")
    apple_subscription_status = models.TextField(null=True, db_column="appleSubscriptionStatus")
    apple_environment = models.TextField(null=True, db_column="appleEnvironment")
    apple_purchase_date = UtcDateTimeField(null=True, db_column="applePurchaseDate")
    apple_expires_at = UtcDateTimeField(null=True, db_column="appleExpiresAt")
    apple_revoked_at = UtcDateTimeField(null=True, db_column="appleRevokedAt")
    apple_offer_discount_type = models.TextField(null=True, db_column="appleOfferDiscountType")
    admin_grant_expires_at = UtcDateTimeField(null=True, db_column="adminGrantExpiresAt")
    admin_grant_tier = models.TextField(choices=PremiumTier.choices, null=True, db_column="adminGrantTier")
    tier = models.TextField(choices=PremiumTier.choices, null=True)
    email_accounts_access = models.IntegerField(null=True, db_column="emailAccountsAccess")
    unsubscribe_month = models.IntegerField(null=True, db_column="unsubscribeMonth")
    unsubscribe_credits = models.IntegerField(null=True, db_column="unsubscribeCredits")
    ai_month = models.IntegerField(null=True, db_column="aiMonth")
    ai_credits = models.IntegerField(null=True, db_column="aiCredits")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Premium"
        indexes = [
            models.Index(fields=["pending_invites"]),
        ]


class PendingPremiumGrant(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    email = models.TextField(unique=True)
    tier = models.TextField(choices=PremiumTier.choices)
    count = models.IntegerField(default=1)
    email_accounts_access = models.IntegerField(null=True, db_column="emailAccountsAccess")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "PendingPremiumGrant"


class VerificationToken(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    identifier = models.TextField()
    token = models.TextField(unique=True)
    expires = UtcDateTimeField()
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "VerificationToken"
        constraints = [
            models.UniqueConstraint(fields=["identifier", "token"], name="VerificationToken_identifier_token_key"),
        ]


class Label(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    gmail_label_id = models.TextField(db_column="gmailLabelId")
    name = models.TextField()
    description = models.TextField(null=True)
    enabled = models.BooleanField(default=True)
    email_account = models.ForeignKey("EmailAccount", on_delete=models.CASCADE, related_name="labels", db_column="emailAccountId", db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Label"
        constraints = [
            models.UniqueConstraint(fields=["gmail_label_id", "email_account"], name="Label_gmailLabelId_emailAccountId_key"),
            models.UniqueConstraint(fields=["name", "email_account"], name="Label_name_emailAccountId_key"),
        ]
        indexes = [
            models.Index(fields=["email_account"]),
        ]


class Rule(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    name = models.TextField()
    enabled = models.BooleanField(default=True)
    automate = models.BooleanField(default=True)
    run_on_threads = models.BooleanField(db_column="runOnThreads", default=False)
    email_account = models.ForeignKey("EmailAccount", on_delete=models.CASCADE, related_name="rules", db_column="emailAccountId", db_index=False)
    organization_rule = models.ForeignKey("OrganizationRule", on_delete=models.CASCADE, related_name="member_rules", db_column="organizationRuleId", null=True, db_index=False)
    organization_rule_member_enabled = models.BooleanField(null=True, db_column="organizationRuleMemberEnabled")
    conditional_operator = models.TextField(choices=LogicalOperator.choices, db_column="conditionalOperator", default=LogicalOperator.AND)
    instructions = models.TextField(null=True)
    group = models.OneToOneField("Group", on_delete=models.CASCADE, related_name="rule", db_column="groupId", null=True)
    from_field = models.TextField(null=True, db_column="from")
    to = models.TextField(null=True)
    subject = models.TextField(null=True)
    body = models.TextField(null=True)
    category_filter_type = models.TextField(choices=CategoryFilterType.choices, null=True, db_column="categoryFilterType")
    system_type = models.TextField(choices=SystemType.choices, null=True, db_column="systemType")
    prompt_text = models.TextField(null=True, db_column="promptText")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Rule"
        constraints = [
            models.UniqueConstraint(fields=["id", "email_account"], name="Rule_id_emailAccountId_key"),
            models.UniqueConstraint(fields=["name", "email_account"], name="Rule_name_emailAccountId_key"),
            models.UniqueConstraint(fields=["email_account", "system_type"], name="Rule_emailAccountId_systemType_key"),
            models.UniqueConstraint(fields=["email_account", "organization_rule"], name="Rule_emailAccountId_organizationRuleId_key"),
        ]
        indexes = [
            models.Index(fields=["email_account", "enabled"]),
            models.Index(fields=["organization_rule"]),
        ]


class Action(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    type = models.TextField(choices=ActionType.choices)
    rule_id = models.TextField(db_column="ruleId")
    email_account = models.ForeignKey("EmailAccount", on_delete=models.CASCADE, related_name="actions", db_column="emailAccountId", db_index=False)
    messaging_channel_id = models.TextField(null=True, db_column="messagingChannelId")
    messaging_channel_email_account_id = models.TextField(null=True, db_column="messagingChannelEmailAccountId")
    label = models.TextField(null=True)
    label_id = models.TextField(null=True, db_column="labelId")
    subject = models.TextField(null=True)
    content = models.TextField(null=True)
    to = models.TextField(null=True)
    cc = models.TextField(null=True)
    bcc = models.TextField(null=True)
    url = models.TextField(null=True)
    folder_name = models.TextField(null=True, db_column="folderName")
    folder_id = models.TextField(null=True, db_column="folderId")
    delay_in_minutes = models.IntegerField(null=True, db_column="delayInMinutes")
    static_attachments = models.JSONField(null=True, db_column="staticAttachments")
    integration_name = models.TextField(null=True, db_column="integrationName")
    integration_tool_name = models.TextField(null=True, db_column="integrationToolName")
    integration_args = models.JSONField(null=True, db_column="integrationArgs")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Action"
        indexes = [
            models.Index(fields=["rule_id"]),
            models.Index(fields=["email_account"]),
            models.Index(fields=["messaging_channel_id", "messaging_channel_email_account_id"]),
        ]


class RuleHistory(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    rule = models.ForeignKey("Rule", on_delete=models.CASCADE, related_name="history", db_column="ruleId", db_index=False)
    version = models.IntegerField()
    trigger_type = models.TextField(db_column="triggerType")
    prompt_text = models.TextField(null=True, db_column="promptText")
    name = models.TextField()
    instructions = models.TextField(null=True)
    enabled = models.BooleanField()
    automate = models.BooleanField()
    run_on_threads = models.BooleanField(db_column="runOnThreads")
    conditional_operator = models.TextField(db_column="conditionalOperator")
    from_field = models.TextField(null=True, db_column="from")
    to = models.TextField(null=True)
    subject = models.TextField(null=True)
    body = models.TextField(null=True)
    category_filter_type = models.TextField(null=True, db_column="categoryFilterType")
    system_type = models.TextField(null=True, db_column="systemType")
    actions = models.JSONField()
    category_filters = models.JSONField(null=True, db_column="categoryFilters")

    class Meta:
        db_table = "RuleHistory"
        constraints = [
            models.UniqueConstraint(fields=["rule", "version"], name="RuleHistory_ruleId_version_key"),
        ]
        indexes = [
            models.Index(fields=["rule", "created_at"]),
        ]


class ExecutedRule(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    thread_id = models.TextField(db_column="threadId")
    message_id = models.TextField(db_column="messageId")
    status = models.TextField(choices=ExecutedRuleStatus.choices)
    automated = models.BooleanField()
    reason = models.TextField(null=True)
    match_metadata = models.JSONField(null=True, db_column="matchMetadata")
    rule = models.ForeignKey("Rule", on_delete=models.SET_NULL, related_name="executed_rules", db_column="ruleId", null=True, db_index=False)
    email_account = models.ForeignKey("EmailAccount", on_delete=models.CASCADE, related_name="executed_rules", db_column="emailAccountId", db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "ExecutedRule"
        indexes = [
            models.Index(fields=["email_account", "thread_id", "message_id", "rule"]),
            models.Index(fields=["email_account", "message_id"]),
            models.Index(fields=["email_account", "status", "created_at"]),
            models.Index(fields=["email_account", "created_at"]),
            models.Index(fields=["email_account", "id"]),
            models.Index(fields=["email_account", "updated_at", "id"]),
            models.Index(fields=["rule"]),
        ]


class ExecutedAction(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    type = models.TextField(choices=ActionType.choices)
    execution_status = models.TextField(choices=ExecutedActionStatus.choices, null=True, db_column="executionStatus")
    execution_started_at = UtcDateTimeField(null=True, db_column="executionStartedAt")
    executed_at = UtcDateTimeField(null=True, db_column="executedAt")
    execution_error = models.JSONField(null=True, db_column="executionError")
    sent_message_ids = ArrayField(models.TextField(), db_column="sentMessageIds", default=list)
    executed_rule = models.ForeignKey("ExecutedRule", on_delete=models.CASCADE, related_name="action_items", db_column="executedRuleId", db_index=False)
    messaging_channel = models.ForeignKey("MessagingChannel", on_delete=models.SET_NULL, related_name="executed_actions", db_column="messagingChannelId", null=True, db_index=False)
    messaging_message_id = models.TextField(null=True, db_column="messagingMessageId")
    messaging_message_sent_at = UtcDateTimeField(null=True, db_column="messagingMessageSentAt")
    messaging_message_status = models.TextField(choices=MessagingMessageStatus.choices, null=True, db_column="messagingMessageStatus")
    label = models.TextField(null=True)
    label_id = models.TextField(null=True, db_column="labelId")
    subject = models.TextField(null=True)
    content = models.TextField(null=True)
    to = models.TextField(null=True)
    cc = models.TextField(null=True)
    bcc = models.TextField(null=True)
    url = models.TextField(null=True)
    folder_name = models.TextField(null=True, db_column="folderName")
    folder_id = models.TextField(null=True, db_column="folderId")
    static_attachments = models.JSONField(null=True, db_column="staticAttachments")
    selected_attachments = models.JSONField(null=True, db_column="selectedAttachments")
    integration_name = models.TextField(null=True, db_column="integrationName")
    integration_tool_name = models.TextField(null=True, db_column="integrationToolName")
    integration_args = models.JSONField(null=True, db_column="integrationArgs")
    draft_id = models.TextField(null=True, db_column="draftId")
    draft_status = models.TextField(choices=DraftEmailStatus.choices, null=True, db_column="draftStatus")
    was_draft_sent = models.BooleanField(null=True, db_column="wasDraftSent")
    draft_model_provider = models.TextField(null=True, db_column="draftModelProvider")
    draft_model_name = models.TextField(null=True, db_column="draftModelName")
    draft_pipeline_version = models.IntegerField(null=True, db_column="draftPipelineVersion")
    draft_context_metadata = models.JSONField(null=True, db_column="draftContextMetadata")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "ExecutedAction"
        indexes = [
            models.Index(fields=["executed_rule"]),
            models.Index(fields=["execution_status", "created_at"]),
            GinIndex(fields=["sent_message_ids"]),
            models.Index(fields=["messaging_channel", "created_at"]),
            models.Index(fields=["draft_status", "created_at"]),
        ]


class ScheduledAction(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    executed_rule = models.ForeignKey("ExecutedRule", on_delete=models.CASCADE, related_name="scheduled_actions", db_column="executedRuleId", db_index=False)
    action_type = models.TextField(choices=ActionType.choices, db_column="actionType")
    message_id = models.TextField(db_column="messageId")
    thread_id = models.TextField(db_column="threadId")
    scheduled_for = UtcDateTimeField(db_column="scheduledFor")
    email_account = models.ForeignKey("EmailAccount", on_delete=models.CASCADE, related_name="scheduled_actions", db_column="emailAccountId", db_index=False)
    status = models.TextField(choices=ScheduledActionStatus.choices, default=ScheduledActionStatus.PENDING)
    scheduling_status = models.TextField(choices=SchedulingStatus.choices, db_column="schedulingStatus", default=SchedulingStatus.PENDING)
    label = models.TextField(null=True)
    label_id = models.TextField(null=True, db_column="labelId")
    subject = models.TextField(null=True)
    content = models.TextField(null=True)
    to = models.TextField(null=True)
    cc = models.TextField(null=True)
    bcc = models.TextField(null=True)
    url = models.TextField(null=True)
    folder_name = models.TextField(null=True, db_column="folderName")
    folder_id = models.TextField(null=True, db_column="folderId")
    scheduled_id = models.TextField(null=True, db_column="scheduledId")
    static_attachments = models.JSONField(null=True, db_column="staticAttachments")
    selected_attachments = models.JSONField(null=True, db_column="selectedAttachments")
    executed_at = UtcDateTimeField(null=True, db_column="executedAt")
    executed_action = models.OneToOneField("ExecutedAction", on_delete=models.CASCADE, related_name="scheduled_action", db_column="executedActionId", null=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "ScheduledAction"
        indexes = [
            models.Index(fields=["executed_rule"]),
            models.Index(fields=["status", "scheduled_for"]),
            models.Index(fields=["email_account", "message_id"]),
        ]


class SnoozedThread(models.Model):
    id = models.TextField(primary_key=True, default=None)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    thread_id = models.TextField(db_column="threadId")
    scheduled_for = UtcDateTimeField(db_column="scheduledFor")
    status = models.TextField(choices=SnoozedThreadStatus.choices, default=SnoozedThreadStatus.PENDING)
    client_mutation_id = models.TextField(null=True, db_column="clientMutationId")
    email_account = models.ForeignKey("EmailAccount", on_delete=models.CASCADE, related_name="snoozed_threads", db_column="emailAccountId", db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "SnoozedThread"
        constraints = [
            models.UniqueConstraint(fields=["email_account", "client_mutation_id"], name="SnoozedThread_emailAccountId_clientMutationId_key"),
        ]
        indexes = [
            models.Index(fields=["status", "scheduled_for"]),
            models.Index(fields=["email_account", "thread_id", "status"]),
        ]


class ScheduledEmail(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    email_account = models.ForeignKey("EmailAccount", on_delete=models.CASCADE, related_name="scheduled_emails", db_column="emailAccountId", db_index=False)
    client_mutation_id = models.TextField(db_column="clientMutationId")
    payload_hash = models.TextField(db_column="payloadHash")
    payload = models.JSONField()
    thread_id = models.TextField(null=True, db_column="threadId")
    send_at = UtcDateTimeField(db_column="sendAt")
    status = models.TextField(choices=ScheduledEmailStatus.choices, default=ScheduledEmailStatus.PENDING)
    processing_started_at = UtcDateTimeField(null=True, db_column="processingStartedAt")
    execution_queued_at = UtcDateTimeField(null=True, db_column="executionQueuedAt")
    sent_at = UtcDateTimeField(null=True, db_column="sentAt")
    error = models.TextField(null=True)
    remind_at = UtcDateTimeField(null=True, db_column="remindAt")
    reminder_status = models.TextField(choices=EmailReminderStatus.choices, db_column="reminderStatus", default=EmailReminderStatus.NONE)
    reminder_started_at = UtcDateTimeField(null=True, db_column="reminderStartedAt")
    held_for_undo = models.BooleanField(db_column="heldForUndo", default=False)
    draft_message_ids = ArrayField(models.TextField(), db_column="draftMessageIds", default=list)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "ScheduledEmail"
        constraints = [
            models.UniqueConstraint(fields=["email_account", "client_mutation_id"], name="ScheduledEmail_emailAccountId_clientMutationId_key"),
        ]
        indexes = [
            models.Index(fields=["status", "send_at"]),
            models.Index(fields=["status", "processing_started_at"]),
            models.Index(fields=["reminder_status", "remind_at"]),
            models.Index(fields=["reminder_status", "reminder_started_at"]),
            models.Index(fields=["email_account", "thread_id"]),
        ]


class EmailSendOperation(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    client_mutation_id = models.TextField(db_column="clientMutationId")
    payload_hash = models.TextField(db_column="payloadHash")
    status = models.TextField(choices=EmailSendOperationStatus.choices, default=EmailSendOperationStatus.PROCESSING)
    processing_started_at = UtcDateTimeField(db_column="processingStartedAt", default=timezone.now)
    result = models.JSONField(null=True)
    email_account = models.ForeignKey("EmailAccount", on_delete=models.CASCADE, related_name="email_send_operations", db_column="emailAccountId", db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "EmailSendOperation"
        constraints = [
            models.UniqueConstraint(fields=["email_account", "client_mutation_id"], name="EmailSendOperation_emailAccountId_clientMutationId_key"),
        ]
        indexes = [
            models.Index(fields=["status", "updated_at"]),
        ]


class SentMessageOpen(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    token = models.TextField(unique=True)
    email_account = models.ForeignKey("EmailAccount", on_delete=models.CASCADE, related_name="sent_message_opens", db_column="emailAccountId", db_index=False)
    thread_id = models.TextField(null=True, db_column="threadId")
    message_id = models.TextField(null=True, db_column="messageId")
    first_opened_at = UtcDateTimeField(null=True, db_column="firstOpenedAt")
    last_opened_at = UtcDateTimeField(null=True, db_column="lastOpenedAt")
    open_count = models.IntegerField(db_column="openCount", default=0)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "SentMessageOpen"
        constraints = [
            models.UniqueConstraint(fields=["email_account", "message_id"], name="SentMessageOpen_emailAccountId_messageId_key"),
        ]
        indexes = [
            models.Index(fields=["email_account", "thread_id"]),
        ]


class Group(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    name = models.TextField()
    prompt = models.TextField(null=True)
    email_account = models.ForeignKey("EmailAccount", on_delete=models.CASCADE, related_name="groups", db_column="emailAccountId", db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Group"
        constraints = [
            models.UniqueConstraint(fields=["name", "email_account"], name="Group_name_emailAccountId_key"),
        ]
        indexes = [
            models.Index(fields=["email_account"]),
        ]


class GroupItem(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    group = models.ForeignKey("Group", on_delete=models.CASCADE, related_name="items", db_column="groupId", null=True, db_index=False)
    type = models.TextField(choices=GroupItemType.choices)
    value = models.TextField()
    exclude = models.BooleanField(default=False)
    reason = models.TextField(null=True)
    thread_id = models.TextField(null=True, db_column="threadId")
    message_id = models.TextField(null=True, db_column="messageId")
    source = models.TextField(choices=GroupItemSource.choices, null=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "GroupItem"
        constraints = [
            models.UniqueConstraint(fields=["group", "type", "value"], name="GroupItem_groupId_type_value_key"),
        ]


class ClassificationFeedback(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    sender = models.TextField()
    event_type = models.TextField(choices=ClassificationFeedbackEventType.choices, db_column="eventType")
    rule = models.ForeignKey("Rule", on_delete=models.CASCADE, related_name="classification_feedback", db_column="ruleId", db_index=False)
    email_account = models.ForeignKey("EmailAccount", on_delete=models.CASCADE, related_name="classification_feedback", db_column="emailAccountId", db_index=False)
    thread_id = models.TextField(db_column="threadId")
    message_id = models.TextField(db_column="messageId")

    class Meta:
        db_table = "ClassificationFeedback"
        constraints = [
            models.UniqueConstraint(fields=["email_account", "sender", "rule", "message_id", "event_type"], name="ClassificationFeedback_emailAccountId_sender_ruleId_messageId_eventType_key"),
        ]
        indexes = [
            models.Index(fields=["rule"]),
        ]


class Category(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    name = models.TextField()
    description = models.TextField(null=True)
    email_account = models.ForeignKey("EmailAccount", on_delete=models.CASCADE, related_name="categories", db_column="emailAccountId", db_index=False)
    rules: "models.ManyToManyField[Rule, CategoryToRule]" = models.ManyToManyField("Rule", through="CategoryToRule", related_name="category_filters")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Category"
        constraints = [
            models.UniqueConstraint(fields=["name", "email_account"], name="Category_name_emailAccountId_key"),
        ]
        indexes = [
            models.Index(fields=["email_account"]),
        ]


class Newsletter(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    email = models.TextField()
    name = models.TextField(null=True)
    status = models.TextField(choices=NewsletterStatus.choices, null=True)
    pattern_analyzed = models.BooleanField(db_column="patternAnalyzed", default=False)
    last_analyzed_at = UtcDateTimeField(null=True, db_column="lastAnalyzedAt")
    email_account = models.ForeignKey("EmailAccount", on_delete=models.CASCADE, related_name="newsletters", db_column="emailAccountId", db_index=False)
    category = models.ForeignKey("Category", on_delete=models.SET_NULL, related_name="email_senders", db_column="categoryId", null=True, db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Newsletter"
        constraints = [
            models.UniqueConstraint(fields=["email", "email_account"], name="Newsletter_email_emailAccountId_key"),
        ]
        indexes = [
            models.Index(fields=["email_account", "status"]),
            models.Index(fields=["category"]),
        ]


class ColdEmail(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    from_email = models.TextField(db_column="fromEmail")
    message_id = models.TextField(null=True, db_column="messageId")
    thread_id = models.TextField(null=True, db_column="threadId")
    status = models.TextField(choices=ColdEmailStatus.choices, null=True)
    reason = models.TextField(null=True)
    email_account = models.ForeignKey("EmailAccount", on_delete=models.CASCADE, related_name="cold_emails", db_column="emailAccountId", db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "ColdEmail"
        constraints = [
            models.UniqueConstraint(fields=["email_account", "from_email"], name="ColdEmail_emailAccountId_fromEmail_key"),
        ]
        indexes = [
            models.Index(fields=["email_account", "status"]),
            models.Index(fields=["email_account", "created_at"]),
        ]


class EmailMessage(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    thread_id = models.TextField(db_column="threadId")
    message_id = models.TextField(db_column="messageId")
    date = UtcDateTimeField()
    from_field = models.TextField(db_column="from")
    from_name = models.TextField(null=True, db_column="fromName")
    from_domain = models.TextField(db_column="fromDomain")
    to = models.TextField()
    unsubscribe_link = models.TextField(null=True, db_column="unsubscribeLink")
    read = models.BooleanField()
    sent = models.BooleanField()
    draft = models.BooleanField()
    inbox = models.BooleanField()
    email_account = models.ForeignKey("EmailAccount", on_delete=models.CASCADE, related_name="email_messages", db_column="emailAccountId", db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "EmailMessage"
        constraints = [
            models.UniqueConstraint(fields=["email_account", "thread_id", "message_id"], name="EmailMessage_emailAccountId_threadId_messageId_key"),
        ]
        indexes = [
            models.Index(fields=["email_account", "thread_id"]),
            models.Index(fields=["email_account", "date"]),
            models.Index(fields=["email_account", "from_field"]),
            models.Index(fields=["email_account", "from_name"]),
        ]


class ResponseTime(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    thread_id = models.TextField(db_column="threadId")
    sent_message_id = models.TextField(db_column="sentMessageId")
    received_message_id = models.TextField(db_column="receivedMessageId")
    response_time_mins = models.IntegerField(db_column="responseTimeMins")
    received_at = UtcDateTimeField(db_column="receivedAt")
    sent_at = UtcDateTimeField(db_column="sentAt")
    email_account = models.ForeignKey("EmailAccount", on_delete=models.CASCADE, related_name="response_times", db_column="emailAccountId", db_index=False)

    class Meta:
        db_table = "ResponseTime"
        constraints = [
            models.UniqueConstraint(fields=["email_account", "sent_message_id"], name="ResponseTime_emailAccountId_sentMessageId_key"),
        ]
        indexes = [
            models.Index(fields=["email_account", "sent_at"]),
        ]


class ThreadTracker(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    sent_at = UtcDateTimeField(db_column="sentAt")
    thread_id = models.TextField(db_column="threadId")
    message_id = models.TextField(db_column="messageId")
    resolved = models.BooleanField(default=False)
    type = models.TextField(choices=ThreadTrackerType.choices)
    follow_up_applied_at = UtcDateTimeField(null=True, db_column="followUpAppliedAt")
    follow_up_draft_id = models.TextField(null=True, db_column="followUpDraftId")
    follow_up_draft_content = models.TextField(null=True, db_column="followUpDraftContent")
    follow_up_draft_created_at = UtcDateTimeField(null=True, db_column="followUpDraftCreatedAt")
    follow_up_notifications = models.JSONField(null=True, db_column="followUpNotifications")
    email_account = models.ForeignKey("EmailAccount", on_delete=models.CASCADE, related_name="thread_trackers", db_column="emailAccountId", db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "ThreadTracker"
        constraints = [
            models.UniqueConstraint(fields=["email_account", "thread_id", "message_id"], name="ThreadTracker_emailAccountId_threadId_messageId_key"),
        ]
        indexes = [
            models.Index(fields=["email_account", "resolved"]),
            models.Index(fields=["email_account", "resolved", "sent_at", "type"]),
            models.Index(fields=["email_account", "type", "resolved", "sent_at"]),
            models.Index(fields=["email_account", "type", "resolved", "follow_up_applied_at", "sent_at"]),
            models.Index(fields=["email_account", "follow_up_draft_created_at"]),
        ]


class CleanupJob(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    action = models.TextField(choices=CleanAction.choices, default=CleanAction.ARCHIVE)
    days_old = models.IntegerField(db_column="daysOld", default=7)
    instructions = models.TextField(null=True)
    skip_reply = models.BooleanField(null=True, db_column="skipReply")
    skip_starred = models.BooleanField(null=True, db_column="skipStarred")
    skip_calendar = models.BooleanField(null=True, db_column="skipCalendar")
    skip_receipt = models.BooleanField(null=True, db_column="skipReceipt")
    skip_attachment = models.BooleanField(null=True, db_column="skipAttachment")
    skip_conversation = models.BooleanField(null=True, db_column="skipConversation")
    email_account = models.ForeignKey("EmailAccount", on_delete=models.CASCADE, related_name="cleanup_jobs", db_column="emailAccountId", db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "CleanupJob"
        indexes = [
            models.Index(fields=["email_account"]),
        ]


class CleanupThread(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    thread_id = models.TextField(db_column="threadId")
    archived = models.BooleanField()
    job = models.ForeignKey("CleanupJob", on_delete=models.CASCADE, related_name="threads", db_column="jobId", db_index=False)
    email_account = models.ForeignKey("EmailAccount", on_delete=models.CASCADE, related_name="cleanup_threads", db_column="emailAccountId", db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "CleanupThread"
        indexes = [
            models.Index(fields=["job"]),
            models.Index(fields=["email_account"]),
        ]


class Knowledge(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    title = models.TextField()
    content = models.TextField()
    email_account = models.ForeignKey("EmailAccount", on_delete=models.CASCADE, related_name="knowledge", db_column="emailAccountId", db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Knowledge"
        constraints = [
            models.UniqueConstraint(fields=["email_account", "title"], name="Knowledge_emailAccountId_title_key"),
        ]


class Snippet(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    shortcut = models.TextField()
    content = models.TextField()
    email_account = models.ForeignKey("EmailAccount", on_delete=models.CASCADE, related_name="snippets", db_column="emailAccountId", db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Snippet"
        constraints = [
            models.UniqueConstraint(fields=["email_account", "shortcut"], name="Snippet_emailAccountId_shortcut_key"),
        ]
        indexes = [
            models.Index(fields=["email_account"]),
        ]


class EmailSignature(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    name = models.TextField()
    html = models.TextField()
    email_account = models.ForeignKey("EmailAccount", on_delete=models.CASCADE, related_name="email_signatures", db_column="emailAccountId", db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "EmailSignature"
        constraints = [
            models.UniqueConstraint(fields=["email_account", "name"], name="EmailSignature_emailAccountId_name_key"),
        ]


class ReplyMemory(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    content = models.TextField()
    kind = models.TextField(choices=ReplyMemoryKind.choices)
    scope_type = models.TextField(choices=ReplyMemoryScopeType.choices, db_column="scopeType")
    scope_value = models.TextField(db_column="scopeValue")
    is_learned_style_evidence = models.BooleanField(db_column="isLearnedStyleEvidence", default=False)
    email_account = models.ForeignKey("EmailAccount", on_delete=models.CASCADE, related_name="reply_memories", db_column="emailAccountId", db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "ReplyMemory"
        constraints = [
            models.UniqueConstraint(fields=["email_account", "kind", "scope_type", "scope_value", "content"], name="ReplyMemory_emailAccountId_kind_scopeType_scopeValue_content_key"),
        ]
        indexes = [
            models.Index(fields=["email_account", "updated_at"]),
            models.Index(fields=["email_account", "scope_type", "scope_value"]),
        ]


class ReplyMemorySource(models.Model):
    pk = models.CompositePrimaryKey("reply_memory_id", "draft_send_log_id")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    learned_writing_style_analyzed_at = UtcDateTimeField(null=True, db_column="learnedWritingStyleAnalyzedAt")
    reply_memory = models.ForeignKey("ReplyMemory", on_delete=models.CASCADE, related_name="sources", db_column="replyMemoryId", db_index=False)
    draft_send_log = models.ForeignKey("DraftSendLog", on_delete=models.CASCADE, related_name="reply_memory_sources", db_column="draftSendLogId", db_index=False)

    class Meta:
        db_table = "ReplyMemorySource"
        indexes = [
            models.Index(fields=["draft_send_log"]),
            models.Index(fields=["reply_memory", "learned_writing_style_analyzed_at", "created_at"]),
        ]


class ApiKey(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    name = models.TextField(null=True)
    hashed_key = models.TextField(unique=True, db_column="hashedKey")
    is_active = models.BooleanField(db_column="isActive", default=True)
    expires_at = UtcDateTimeField(null=True, db_column="expiresAt")
    last_used_at = UtcDateTimeField(null=True, db_column="lastUsedAt")
    scopes = ArrayField(models.TextField(choices=ApiKeyScope.choices), default=list)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="api_keys", db_column="userId", db_index=False)
    email_account = models.ForeignKey("EmailAccount", on_delete=models.CASCADE, related_name="api_keys", db_column="emailAccountId", null=True, db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "ApiKey"
        indexes = [
            models.Index(fields=["user", "is_active"]),
            models.Index(fields=["email_account", "is_active"]),
        ]


class EmailToken(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    token = models.TextField(unique=True)
    expires_at = UtcDateTimeField(db_column="expiresAt")
    email_account = models.ForeignKey("EmailAccount", on_delete=models.CASCADE, related_name="email_tokens", db_column="emailAccountId", db_index=False)

    class Meta:
        db_table = "EmailToken"
        indexes = [
            models.Index(fields=["email_account"]),
        ]


class Payment(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt")
    updated_at = UtcDateTimeField(db_column="updatedAt")
    premium = models.ForeignKey("Premium", on_delete=models.SET_NULL, related_name="payments", db_column="premiumId", null=True, db_index=False)
    processor_type = models.TextField(choices=ProcessorType.choices, db_column="processorType", default=ProcessorType.LEMON_SQUEEZY)
    processor_id = models.TextField(null=True, unique=True, db_column="processorId")
    processor_subscription_id = models.TextField(null=True, db_column="processorSubscriptionId")
    processor_customer_id = models.TextField(null=True, db_column="processorCustomerId")
    amount = models.IntegerField()
    currency = models.TextField()
    status = models.TextField()
    tax = models.IntegerField()
    tax_inclusive = models.BooleanField(db_column="taxInclusive")
    refunded = models.BooleanField(default=False)
    refunded_at = UtcDateTimeField(null=True, db_column="refundedAt")
    refunded_amount = models.IntegerField(null=True, db_column="refundedAmount")
    billing_reason = models.TextField(null=True, db_column="billingReason")
    invoice_email_sent_at = UtcDateTimeField(null=True, db_column="invoiceEmailSentAt")

    class Meta:
        db_table = "Payment"
        indexes = [
            models.Index(fields=["premium", "created_at"]),
        ]


class DraftSendLog(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    executed_action = models.OneToOneField("ExecutedAction", on_delete=models.CASCADE, related_name="draft_send_log", db_column="executedActionId")
    sent_message_id = models.TextField(db_column="sentMessageId")
    similarity_score = models.FloatField(db_column="similarityScore")
    body_similarity_score = models.FloatField(null=True, db_column="bodySimilarityScore")
    body_similarity_status = models.TextField(null=True, db_column="bodySimilarityStatus")
    similarity_metadata = models.JSONField(null=True, db_column="similarityMetadata")
    sent_text = models.TextField(null=True, db_column="sentText")
    reply_memory_sent_text = models.TextField(null=True, db_column="replyMemorySentText")
    reply_memory_attempt_count = models.IntegerField(db_column="replyMemoryAttemptCount", default=0)
    reply_memory_processed_at = UtcDateTimeField(null=True, db_column="replyMemoryProcessedAt")

    class Meta:
        db_table = "DraftSendLog"
        indexes = [
            models.Index(fields=["executed_action"]),
            models.Index(fields=["reply_memory_processed_at", "reply_memory_attempt_count", "created_at"]),
        ]


class Chat(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    deleted_at = UtcDateTimeField(null=True, db_column="deletedAt")
    name = models.TextField(null=True)
    compaction_count = models.IntegerField(db_column="compactionCount", default=0)
    last_seen_rules_revision = models.IntegerField(null=True, db_column="lastSeenRulesRevision")
    active_stream_id = models.TextField(null=True, db_column="activeStreamId")
    active_stream_started_at = UtcDateTimeField(null=True, db_column="activeStreamStartedAt")
    email_account = models.ForeignKey("EmailAccount", on_delete=models.CASCADE, related_name="chats", db_column="emailAccountId", db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Chat"
        indexes = [
            models.Index(fields=["email_account"]),
            models.Index(fields=["deleted_at"]),
        ]


class ChatMessage(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    role = models.TextField()
    parts = models.JSONField()
    metadata = models.JSONField(null=True)
    chat = models.ForeignKey("Chat", on_delete=models.CASCADE, related_name="messages", db_column="chatId", db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "ChatMessage"
        indexes = [
            models.Index(fields=["chat"]),
        ]


class ChatCompaction(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    summary = models.TextField()
    message_count = models.IntegerField(db_column="messageCount")
    compacted_before_created_at = UtcDateTimeField(db_column="compactedBeforeCreatedAt")
    chat = models.ForeignKey("Chat", on_delete=models.CASCADE, related_name="compactions", db_column="chatId", db_index=False)

    class Meta:
        db_table = "ChatCompaction"
        indexes = [
            models.Index(fields=["chat"]),
        ]


class ChatMemory(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    content = models.TextField()
    chat = models.ForeignKey("Chat", on_delete=models.SET_NULL, related_name="memories", db_column="chatId", null=True, db_index=False)
    email_account = models.ForeignKey("EmailAccount", on_delete=models.CASCADE, related_name="chat_memories", db_column="emailAccountId", db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "ChatMemory"
        indexes = [
            models.Index(fields=["email_account"]),
        ]


class CalendarConnection(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    provider = models.TextField()
    email = models.TextField()
    access_token = models.TextField(null=True, db_column="accessToken")
    refresh_token = models.TextField(null=True, db_column="refreshToken")
    expires_at = UtcDateTimeField(null=True, db_column="expiresAt")
    is_connected = models.BooleanField(db_column="isConnected", default=True)
    email_account = models.ForeignKey("EmailAccount", on_delete=models.CASCADE, related_name="calendar_connections", db_column="emailAccountId", db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "CalendarConnection"
        constraints = [
            models.UniqueConstraint(fields=["email_account", "provider", "email"], name="CalendarConnection_emailAccountId_provider_email_key"),
        ]


class Calendar(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    calendar_id = models.TextField(db_column="calendarId")
    name = models.TextField()
    description = models.TextField(null=True)
    primary = models.BooleanField(default=False)
    is_enabled = models.BooleanField(db_column="isEnabled", default=True)
    timezone = models.TextField(null=True)
    connection = models.ForeignKey("CalendarConnection", on_delete=models.CASCADE, related_name="calendars", db_column="connectionId", db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Calendar"
        constraints = [
            models.UniqueConstraint(fields=["connection", "calendar_id"], name="Calendar_connectionId_calendarId_key"),
        ]


class BookingLink(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    slug = models.TextField(unique=True)
    title = models.TextField()
    description = models.TextField(null=True)
    is_active = models.BooleanField(db_column="isActive", default=True)
    duration_minutes = models.IntegerField(db_column="durationMinutes")
    slot_interval_minutes = models.IntegerField(db_column="slotIntervalMinutes")
    location_type = models.TextField(choices=BookingLinkLocationType.choices, db_column="locationType", default=BookingLinkLocationType.CUSTOM)
    location_value = models.TextField(null=True, db_column="locationValue")
    minimum_notice_minutes = models.IntegerField(db_column="minimumNoticeMinutes", default=120)
    max_days_ahead = models.IntegerField(db_column="maxDaysAhead", default=90)
    email_account = models.ForeignKey("EmailAccount", on_delete=models.CASCADE, related_name="booking_links", db_column="emailAccountId", db_index=False)
    destination_calendar = models.ForeignKey("Calendar", on_delete=models.SET_NULL, related_name="booking_links", db_column="destinationCalendarId", null=True, db_index=False)
    availability_schedule_id = models.TextField(db_column="availabilityScheduleId")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "BookingLink"
        indexes = [
            models.Index(fields=["destination_calendar"]),
            models.Index(fields=["availability_schedule_id"]),
        ]


class AvailabilitySchedule(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    name = models.TextField()
    is_default = models.BooleanField(db_column="isDefault", default=False)
    timezone = models.TextField(default="UTC")
    email_account = models.ForeignKey("EmailAccount", on_delete=models.CASCADE, related_name="availability_schedules", db_column="emailAccountId", db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "AvailabilitySchedule"
        constraints = [
            models.UniqueConstraint(fields=["id", "email_account"], name="AvailabilitySchedule_id_emailAccountId_key"),
        ]
        indexes = [
            models.Index(fields=["email_account"]),
            models.Index(fields=["email_account", "is_default"]),
        ]


class AvailabilityWindow(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    weekday = models.IntegerField()
    start_minutes = models.IntegerField(db_column="startMinutes")
    end_minutes = models.IntegerField(db_column="endMinutes")
    availability_schedule = models.ForeignKey("AvailabilitySchedule", on_delete=models.CASCADE, related_name="windows", db_column="availabilityScheduleId", db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "AvailabilityWindow"
        indexes = [
            models.Index(fields=["availability_schedule"]),
        ]


class Booking(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    guest_name = models.TextField(db_column="guestName")
    guest_email = models.TextField(db_column="guestEmail")
    guest_note = models.TextField(null=True, db_column="guestNote")
    start_time = UtcDateTimeField(db_column="startTime")
    end_time = UtcDateTimeField(db_column="endTime")
    status = models.TextField(choices=BookingStatus.choices, default=BookingStatus.PENDING_PROVIDER_EVENT)
    provider = models.TextField(null=True)
    provider_connection_id = models.TextField(null=True, db_column="providerConnectionId")
    provider_calendar_id = models.TextField(null=True, db_column="providerCalendarId")
    provider_event_id = models.TextField(null=True, db_column="providerEventId")
    video_conference_link = models.TextField(null=True, db_column="videoConferenceLink")
    cancel_token_hash = models.TextField(db_column="cancelTokenHash")
    cancellation_reason = models.TextField(null=True, db_column="cancellationReason")
    idempotency_token = models.TextField(null=True, db_column="idempotencyToken")
    booking_link = models.ForeignKey("BookingLink", on_delete=models.CASCADE, related_name="bookings", db_column="bookingLinkId", db_index=False)
    email_account = models.ForeignKey("EmailAccount", on_delete=models.CASCADE, related_name="bookings", db_column="emailAccountId", db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Booking"
        constraints = [
            models.UniqueConstraint(fields=["booking_link", "idempotency_token"], name="Booking_bookingLinkId_idempotencyToken_key"),
        ]
        indexes = [
            models.Index(fields=["booking_link", "start_time", "end_time"]),
            models.Index(fields=["email_account"]),
        ]


class MeetingBriefing(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    calendar_event_id = models.TextField(db_column="calendarEventId")
    event_title = models.TextField(db_column="eventTitle")
    event_start_time = UtcDateTimeField(db_column="eventStartTime")
    guest_count = models.IntegerField(db_column="guestCount")
    status = models.TextField(choices=MeetingBriefingStatus.choices)
    email_account = models.ForeignKey("EmailAccount", on_delete=models.CASCADE, related_name="meeting_briefings", db_column="emailAccountId", db_index=False)

    class Meta:
        db_table = "MeetingBriefing"
        constraints = [
            models.UniqueConstraint(fields=["email_account", "calendar_event_id"], name="MeetingBriefing_emailAccountId_calendarEventId_key"),
        ]
        indexes = [
            models.Index(fields=["email_account"]),
        ]


class ContactResearch(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    email = models.TextField()
    research_started_at = UtcDateTimeField(db_column="researchStartedAt")
    found = models.BooleanField()
    role = models.TextField(null=True)
    confidence = models.TextField(null=True)
    company = models.JSONField(null=True)
    sources = ArrayField(models.TextField(), default=list)

    class Meta:
        db_table = "ContactResearch"
        indexes = [
            models.Index(fields=["email", "-research_started_at", "-created_at", "-id"], name="ContactResearch_latest_idx"),
        ]


class MeetingRecording(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    bot_provider = models.TextField(db_column="botProvider", default="recall")
    external_bot_id = models.TextField(null=True, db_column="externalBotId")
    bot_name = models.TextField(null=True, db_column="botName")
    external_recording_id = models.TextField(null=True, db_column="externalRecordingId")
    transcript_requested_at = UtcDateTimeField(null=True, db_column="transcriptRequestedAt")
    external_transcript_id = models.TextField(null=True, db_column="externalTranscriptId")
    meeting_url = models.TextField(db_column="meetingUrl")
    normalized_meeting_url = models.TextField(db_column="normalizedMeetingUrl")
    active_key = models.TextField(null=True, db_column="activeKey")
    meeting_start_time = UtcDateTimeField(db_column="meetingStartTime")
    status = models.TextField(choices=MeetingRecordingStatus.choices, default=MeetingRecordingStatus.PENDING)
    failure_reason = models.TextField(null=True, db_column="failureReason")
    transcript = models.JSONField(null=True)
    transcript_fetched_at = UtcDateTimeField(null=True, db_column="transcriptFetchedAt")
    media_deleted_at = UtcDateTimeField(null=True, db_column="mediaDeletedAt")
    email_account = models.ForeignKey("EmailAccount", on_delete=models.CASCADE, related_name="meeting_recordings", db_column="emailAccountId", db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "MeetingRecording"
        constraints = [
            models.UniqueConstraint(fields=["bot_provider", "external_bot_id"], name="MeetingRecording_botProvider_externalBotId_key"),
            models.UniqueConstraint(fields=["active_key", "meeting_start_time"], name="MeetingRecording_activeKey_meetingStartTime_key"),
        ]
        indexes = [
            models.Index(fields=["normalized_meeting_url", "meeting_start_time"]),
            models.Index(fields=["status", "meeting_start_time"]),
            models.Index(fields=["status", "media_deleted_at"]),
            models.Index(fields=["email_account"]),
        ]


class Meeting(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    calendar_event_id = models.TextField(db_column="calendarEventId")
    event_title = models.TextField(db_column="eventTitle")
    start_time = UtcDateTimeField(db_column="startTime")
    end_time = UtcDateTimeField(db_column="endTime")
    attendees = models.JSONField()
    organizer_email = models.TextField(null=True, db_column="organizerEmail")
    join_override = models.BooleanField(null=True, db_column="joinOverride")
    processing_status = models.TextField(choices=MeetingProcessingStatus.choices, db_column="processingStatus", default=MeetingProcessingStatus.PENDING)
    processing_attempts = models.IntegerField(db_column="processingAttempts", default=0)
    summary = models.JSONField(null=True)
    follow_up_draft_started_at = UtcDateTimeField(null=True, db_column="followUpDraftStartedAt")
    follow_up_draft_id = models.TextField(null=True, db_column="followUpDraftId")
    recap_sent_at = UtcDateTimeField(null=True, db_column="recapSentAt")
    recording = models.ForeignKey("MeetingRecording", on_delete=models.SET_NULL, related_name="meetings", db_column="recordingId", null=True, db_index=False)
    email_account = models.ForeignKey("EmailAccount", on_delete=models.CASCADE, related_name="meetings", db_column="emailAccountId", db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Meeting"
        constraints = [
            models.UniqueConstraint(fields=["email_account", "calendar_event_id"], name="Meeting_emailAccountId_calendarEventId_key"),
            models.UniqueConstraint(fields=["email_account", "recording"], name="Meeting_emailAccountId_recordingId_key"),
        ]
        indexes = [
            models.Index(fields=["email_account", "start_time"]),
            models.Index(fields=["recording"]),
            models.Index(fields=["processing_status", "updated_at"]),
        ]


class MessagingChannel(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    provider = models.TextField(choices=MessagingProvider.choices)
    is_connected = models.BooleanField(db_column="isConnected", default=True)
    team_id = models.TextField(db_column="teamId")
    team_name = models.TextField(null=True, db_column="teamName")
    provider_user_id = models.TextField(null=True, db_column="providerUserId")
    bot_user_id = models.TextField(null=True, db_column="botUserId")
    access_token = models.TextField(null=True, db_column="accessToken")
    refresh_token = models.TextField(null=True, db_column="refreshToken")
    expires_at = UtcDateTimeField(null=True, db_column="expiresAt")
    email_account = models.ForeignKey("EmailAccount", on_delete=models.CASCADE, related_name="messaging_channels", db_column="emailAccountId", db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "MessagingChannel"
        constraints = [
            models.UniqueConstraint(fields=["email_account", "provider", "team_id"], name="MessagingChannel_emailAccountId_provider_teamId_key"),
            models.UniqueConstraint(fields=["id", "email_account"], name="MessagingChannel_id_emailAccountId_key"),
        ]
        indexes = [
            models.Index(fields=["email_account"]),
            models.Index(fields=["provider", "team_id"]),
        ]


class MessagingRoute(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    purpose = models.TextField(choices=MessagingRoutePurpose.choices)
    target_type = models.TextField(choices=MessagingRouteTargetType.choices, db_column="targetType")
    target_id = models.TextField(db_column="targetId")
    messaging_channel = models.ForeignKey("MessagingChannel", on_delete=models.CASCADE, related_name="routes", db_column="messagingChannelId", db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "MessagingRoute"
        constraints = [
            models.UniqueConstraint(fields=["messaging_channel", "purpose"], name="MessagingRoute_messagingChannelId_purpose_key"),
        ]


class AutomationJob(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    name = models.TextField()
    enabled = models.BooleanField(default=True)
    prompt = models.TextField(null=True)
    cron_expression = models.TextField(db_column="cronExpression")
    next_run_at = UtcDateTimeField(db_column="nextRunAt")
    messaging_channel_id = models.TextField(db_column="messagingChannelId")
    email_account_id = models.TextField(unique=True, db_column="emailAccountId")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "AutomationJob"
        constraints = [
            models.UniqueConstraint(fields=["email_account_id"], name="AutomationJob_emailAccountId_key"),
        ]
        indexes = [
            models.Index(fields=["email_account_id", "enabled", "next_run_at"]),
            models.Index(fields=["enabled", "next_run_at"]),
            models.Index(fields=["messaging_channel_id", "email_account_id"]),
        ]


class AutomationJobRun(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    status = models.TextField(choices=AutomationJobRunStatus.choices, default=AutomationJobRunStatus.PENDING)
    scheduled_for = UtcDateTimeField(db_column="scheduledFor")
    processed_at = UtcDateTimeField(null=True, db_column="processedAt")
    outbound_message = models.TextField(null=True, db_column="outboundMessage")
    provider_message_id = models.TextField(null=True, db_column="providerMessageId")
    error = models.TextField(null=True)
    automation_job = models.ForeignKey("AutomationJob", on_delete=models.CASCADE, related_name="runs", db_column="automationJobId", db_index=False)

    class Meta:
        db_table = "AutomationJobRun"
        constraints = [
            models.UniqueConstraint(fields=["automation_job", "scheduled_for"], name="AutomationJobRun_automationJobId_scheduledFor_key"),
        ]
        indexes = [
            models.Index(fields=["automation_job", "created_at"]),
            models.Index(fields=["status", "created_at"]),
        ]


class DriveConnection(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    provider = models.TextField()
    email = models.TextField()
    access_token = models.TextField(null=True, db_column="accessToken")
    refresh_token = models.TextField(null=True, db_column="refreshToken")
    expires_at = UtcDateTimeField(null=True, db_column="expiresAt")
    is_connected = models.BooleanField(db_column="isConnected", default=True)
    email_account = models.ForeignKey("EmailAccount", on_delete=models.CASCADE, related_name="drive_connections", db_column="emailAccountId", db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "DriveConnection"
        constraints = [
            models.UniqueConstraint(fields=["email_account", "provider"], name="DriveConnection_emailAccountId_provider_key"),
        ]
        indexes = [
            models.Index(fields=["email_account"]),
        ]


class FilingFolder(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    folder_id = models.TextField(db_column="folderId")
    folder_name = models.TextField(db_column="folderName")
    folder_path = models.TextField(db_column="folderPath")
    drive_connection = models.ForeignKey("DriveConnection", on_delete=models.CASCADE, related_name="filing_folders", db_column="driveConnectionId", db_index=False)
    email_account = models.ForeignKey("EmailAccount", on_delete=models.CASCADE, related_name="filing_folders", db_column="emailAccountId", db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "FilingFolder"
        constraints = [
            models.UniqueConstraint(fields=["email_account", "folder_id"], name="FilingFolder_emailAccountId_folderId_key"),
        ]
        indexes = [
            models.Index(fields=["drive_connection"]),
        ]


class DocumentFiling(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    message_id = models.TextField(db_column="messageId")
    attachment_id = models.TextField(db_column="attachmentId")
    filename = models.TextField()
    folder_id = models.TextField(null=True, db_column="folderId")
    folder_path = models.TextField(db_column="folderPath")
    file_id = models.TextField(null=True, db_column="fileId")
    reasoning = models.TextField(null=True)
    confidence = models.FloatField(null=True)
    status = models.TextField(choices=DocumentFilingStatus.choices, default=DocumentFilingStatus.FILED)
    was_asked = models.BooleanField(db_column="wasAsked", default=False)
    was_corrected = models.BooleanField(db_column="wasCorrected", default=False)
    original_path = models.TextField(null=True, db_column="originalPath")
    corrected_at = UtcDateTimeField(null=True, db_column="correctedAt")
    feedback_positive = models.BooleanField(null=True, db_column="feedbackPositive")
    feedback_at = UtcDateTimeField(null=True, db_column="feedbackAt")
    notification_message_id = models.TextField(null=True, unique=True, db_column="notificationMessageId")
    notification_sent_at = UtcDateTimeField(null=True, db_column="notificationSentAt")
    notification_batch_id = models.TextField(null=True, db_column="notificationBatchId")
    drive_connection = models.ForeignKey("DriveConnection", on_delete=models.CASCADE, related_name="document_filings", db_column="driveConnectionId", db_index=False)
    email_account = models.ForeignKey("EmailAccount", on_delete=models.CASCADE, related_name="document_filings", db_column="emailAccountId", db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "DocumentFiling"
        constraints = [
            models.UniqueConstraint(fields=["email_account", "message_id", "attachment_id"], name="DocumentFiling_emailAccountId_messageId_attachmentId_key"),
        ]
        indexes = [
            models.Index(fields=["email_account", "status"]),
            models.Index(fields=["email_account", "notification_batch_id"]),
            models.Index(fields=["drive_connection"]),
            models.Index(fields=["message_id"]),
        ]


class AttachmentSource(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    name = models.TextField()
    type = models.TextField(choices=AttachmentSourceType.choices)
    source_id = models.TextField(db_column="sourceId")
    source_path = models.TextField(null=True, db_column="sourcePath")
    rule = models.ForeignKey("Rule", on_delete=models.CASCADE, related_name="attachment_sources", db_column="ruleId", db_index=False)
    drive_connection = models.ForeignKey("DriveConnection", on_delete=models.CASCADE, related_name="attachment_sources", db_column="driveConnectionId", db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "AttachmentSource"
        constraints = [
            models.UniqueConstraint(fields=["rule", "drive_connection", "type", "source_id"], name="AttachmentSource_ruleId_driveConnectionId_type_sourceId_key"),
        ]
        indexes = [
            models.Index(fields=["rule"]),
            models.Index(fields=["drive_connection"]),
        ]


class AttachmentDocument(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    attachment_source = models.ForeignKey("AttachmentSource", on_delete=models.CASCADE, related_name="documents", db_column="attachmentSourceId", db_index=False)
    file_id = models.TextField(db_column="fileId")
    name = models.TextField()
    mime_type = models.TextField(db_column="mimeType")
    modified_at = UtcDateTimeField(null=True, db_column="modifiedAt")
    summary = models.TextField(null=True)
    content = models.TextField(null=True)
    metadata = models.JSONField(null=True)
    indexed_at = UtcDateTimeField(null=True, db_column="indexedAt")
    error = models.TextField(null=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "AttachmentDocument"
        constraints = [
            models.UniqueConstraint(fields=["attachment_source", "file_id"], name="AttachmentDocument_attachmentSourceId_fileId_key"),
        ]
        indexes = [
            models.Index(fields=["attachment_source", "modified_at"]),
        ]


class Referral(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    referrer_user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="referrals_made", db_column="referrerUserId", db_index=False)
    referred_user = models.OneToOneField("User", on_delete=models.CASCADE, related_name="referral_received", db_column="referredUserId")
    referral_code_used = models.TextField(db_column="referralCodeUsed")
    status = models.TextField(choices=ReferralStatus.choices, default=ReferralStatus.PENDING)
    reward_granted_at = UtcDateTimeField(null=True, db_column="rewardGrantedAt")
    stripe_balance_transaction_id = models.TextField(null=True, db_column="stripeBalanceTransactionId")
    reward_amount = models.IntegerField(null=True, db_column="rewardAmount")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Referral"
        indexes = [
            models.Index(fields=["referrer_user"]),
            models.Index(fields=["status"]),
        ]


class McpIntegration(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    name = models.TextField(unique=True)
    registered_server_url = models.TextField(null=True, db_column="registeredServerUrl")
    registered_authorization_url = models.TextField(null=True, db_column="registeredAuthorizationUrl")
    registered_token_url = models.TextField(null=True, db_column="registeredTokenUrl")
    oauth_client_id = models.TextField(null=True, db_column="oauthClientId")
    oauth_client_secret = models.TextField(null=True, db_column="oauthClientSecret")
    display_name = models.TextField(null=True, db_column="displayName")
    server_url = models.TextField(null=True, db_column="serverUrl")
    auth_type = models.TextField(choices=McpAuthType.choices, null=True, db_column="authType")
    email_account = models.ForeignKey("EmailAccount", on_delete=models.CASCADE, related_name="mcp_integrations", db_column="emailAccountId", null=True, db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "McpIntegration"
        indexes = [
            models.Index(fields=["email_account"]),
        ]


class McpConnection(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    name = models.TextField()
    is_active = models.BooleanField(db_column="isActive", default=True)
    access_token = models.TextField(null=True, db_column="accessToken")
    refresh_token = models.TextField(null=True, db_column="refreshToken")
    api_key = models.TextField(null=True, db_column="apiKey")
    expires_at = UtcDateTimeField(null=True, db_column="expiresAt")
    integration = models.ForeignKey("McpIntegration", on_delete=models.CASCADE, related_name="connections", db_column="integrationId", db_index=False)
    email_account = models.ForeignKey("EmailAccount", on_delete=models.CASCADE, related_name="mcp_connections", db_column="emailAccountId", null=True, db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "McpConnection"
        constraints = [
            models.UniqueConstraint(fields=["email_account", "integration"], name="McpConnection_emailAccountId_integrationId_key"),
        ]


class McpTool(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    name = models.TextField()
    description = models.TextField(null=True)
    schema = models.JSONField(null=True)
    is_enabled = models.BooleanField(db_column="isEnabled", default=True)
    is_write = models.BooleanField(db_column="isWrite", default=False)
    connection = models.ForeignKey("McpConnection", on_delete=models.CASCADE, related_name="tools", db_column="connectionId", db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "McpTool"
        constraints = [
            models.UniqueConstraint(fields=["connection", "name"], name="McpTool_connectionId_name_key"),
        ]


class MobilePushToken(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    token = models.TextField(unique=True)
    platform = models.TextField(choices=MobilePushPlatform.choices)
    token_type = models.TextField(choices=MobilePushTokenType.choices, db_column="tokenType", default=MobilePushTokenType.EXPO)
    app_version = models.TextField(null=True, db_column="appVersion")
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="mobile_push_tokens", db_column="userId", db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "MobilePushToken"
        indexes = [
            models.Index(fields=["user"]),
        ]


class MobilePushDelivery(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    deduplication_key = models.TextField(db_column="deduplicationKey")
    mobile_push_token = models.ForeignKey("MobilePushToken", on_delete=models.CASCADE, related_name="mobile_push_deliveries", db_column="mobilePushTokenId", db_index=False)

    class Meta:
        db_table = "MobilePushDelivery"
        constraints = [
            models.UniqueConstraint(fields=["deduplication_key", "mobile_push_token"], name="MobilePushDelivery_key_token_key"),
        ]
        indexes = [
            models.Index(fields=["created_at"]),
            models.Index(fields=["mobile_push_token"]),
        ]


class OauthClient(models.Model):
    id = models.TextField(primary_key=True, default=None)
    client_id = models.TextField(unique=True, db_column="clientId")
    client_secret = models.TextField(null=True, db_column="clientSecret")
    client_discovery_id = models.TextField(null=True, db_column="clientDiscoveryId")
    disabled = models.BooleanField(null=True, default=False)
    skip_consent = models.BooleanField(null=True, db_column="skipConsent")
    enable_end_session = models.BooleanField(null=True, db_column="enableEndSession")
    subject_type = models.TextField(null=True, db_column="subjectType")
    scopes = ArrayField(models.TextField())
    client_credentials_scopes = ArrayField(models.TextField(), db_column="clientCredentialsScopes", default=list)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="oauth_client_records", db_column="userId", null=True, db_index=False)
    created_at = UtcDateTimeField(null=True, db_column="createdAt")
    updated_at = UtcDateTimeField(null=True, db_column="updatedAt")
    name = models.TextField(null=True)
    uri = models.TextField(null=True)
    icon = models.TextField(null=True)
    contacts = ArrayField(models.TextField())
    tos = models.TextField(null=True)
    policy = models.TextField(null=True)
    software_id = models.TextField(null=True, db_column="softwareId")
    software_version = models.TextField(null=True, db_column="softwareVersion")
    software_statement = models.TextField(null=True, db_column="softwareStatement")
    redirect_uris = ArrayField(models.TextField(), db_column="redirectUris")
    post_logout_redirect_uris = ArrayField(models.TextField(), db_column="postLogoutRedirectUris")
    backchannel_logout_uri = models.TextField(null=True, db_column="backchannelLogoutUri")
    backchannel_logout_session_required = models.BooleanField(null=True, db_column="backchannelLogoutSessionRequired")
    token_endpoint_auth_method = models.TextField(null=True, db_column="tokenEndpointAuthMethod")
    application_type = models.TextField(null=True, db_column="applicationType")
    jwks = models.TextField(null=True)
    jwks_uri = models.TextField(null=True, db_column="jwksUri")
    grant_types = ArrayField(models.TextField(), db_column="grantTypes")
    response_types = ArrayField(models.TextField(), db_column="responseTypes")
    require_pkce = models.BooleanField(null=True, db_column="requirePKCE")
    dpop_bound_access_tokens = models.BooleanField(null=True, db_column="dpopBoundAccessTokens", default=False)
    reference_id = models.TextField(null=True, db_column="referenceId")
    metadata = models.JSONField(null=True)

    class Meta:
        db_table = "OauthClient"
        constraints = [
            models.UniqueConstraint(fields=["client_id"], name="OauthClient_clientId_key"),
        ]
        indexes = [
            models.Index(fields=["user"]),
        ]


class OauthRefreshToken(models.Model):
    id = models.TextField(primary_key=True, default=None)
    token = models.TextField(unique=True)
    client = models.ForeignKey("OauthClient", on_delete=models.CASCADE, related_name="oauthrefreshtokens", to_field="client_id", db_column="clientId", db_index=False)
    session = models.ForeignKey("Session", on_delete=models.SET_NULL, related_name="oauth_refresh_token_records", db_column="sessionId", null=True, db_index=False)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="oauth_refresh_token_records", db_column="userId", db_index=False)
    reference_id = models.TextField(null=True, db_column="referenceId")
    authorization_code_id = models.TextField(null=True, db_column="authorizationCodeId")
    resources = ArrayField(models.TextField())
    requested_user_info_claims = ArrayField(models.TextField(), db_column="requestedUserInfoClaims")
    expires_at = UtcDateTimeField(db_column="expiresAt")
    created_at = UtcDateTimeField(db_column="createdAt")
    revoked = UtcDateTimeField(null=True)
    rotated_at = UtcDateTimeField(null=True, db_column="rotatedAt")
    rotation_replay_response = models.TextField(null=True, db_column="rotationReplayResponse")
    rotation_replay_expires_at = UtcDateTimeField(null=True, db_column="rotationReplayExpiresAt")
    auth_time = UtcDateTimeField(null=True, db_column="authTime")
    confirmation = models.JSONField(null=True)
    scopes = ArrayField(models.TextField())

    class Meta:
        db_table = "OauthRefreshToken"
        constraints = [
            models.UniqueConstraint(fields=["token"], name="OauthRefreshToken_token_key"),
        ]
        indexes = [
            models.Index(fields=["client"]),
            models.Index(fields=["session"]),
            models.Index(fields=["user"]),
            models.Index(fields=["authorization_code_id"]),
        ]


class OauthAccessToken(models.Model):
    id = models.TextField(primary_key=True, default=None)
    token = models.TextField(unique=True)
    client = models.ForeignKey("OauthClient", on_delete=models.CASCADE, related_name="oauthaccesstokens", to_field="client_id", db_column="clientId", db_index=False)
    session = models.ForeignKey("Session", on_delete=models.SET_NULL, related_name="oauth_access_token_records", db_column="sessionId", null=True, db_index=False)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="oauth_access_token_records", db_column="userId", null=True, db_index=False)
    reference_id = models.TextField(null=True, db_column="referenceId")
    authorization_code_id = models.TextField(null=True, db_column="authorizationCodeId")
    resources = ArrayField(models.TextField())
    requested_user_info_claims = ArrayField(models.TextField(), db_column="requestedUserInfoClaims")
    refresh = models.ForeignKey("OauthRefreshToken", on_delete=models.CASCADE, related_name="oauthaccesstokens", db_column="refreshId", null=True, db_index=False)
    expires_at = UtcDateTimeField(db_column="expiresAt")
    created_at = UtcDateTimeField(db_column="createdAt")
    revoked = UtcDateTimeField(null=True)
    confirmation = models.JSONField(null=True)
    scopes = ArrayField(models.TextField())

    class Meta:
        db_table = "OauthAccessToken"
        constraints = [
            models.UniqueConstraint(fields=["token"], name="OauthAccessToken_token_key"),
        ]
        indexes = [
            models.Index(fields=["client"]),
            models.Index(fields=["session"]),
            models.Index(fields=["user"]),
            models.Index(fields=["authorization_code_id"]),
            models.Index(fields=["refresh"]),
        ]


class OauthConsent(models.Model):
    id = models.TextField(primary_key=True, default=None)
    client = models.ForeignKey("OauthClient", on_delete=models.CASCADE, related_name="oauthconsents", to_field="client_id", db_column="clientId", db_index=False)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="oauth_consent_records", db_column="userId", null=True, db_index=False)
    reference_id = models.TextField(null=True, db_column="referenceId")
    resources = ArrayField(models.TextField())
    requested_user_info_claims = ArrayField(models.TextField(), db_column="requestedUserInfoClaims")
    scopes = ArrayField(models.TextField())
    created_at = UtcDateTimeField(db_column="createdAt")
    updated_at = UtcDateTimeField(db_column="updatedAt")

    class Meta:
        db_table = "OauthConsent"
        constraints = [
            models.UniqueConstraint(fields=["client", "user"], name="OauthConsent_clientId_userId_key"),
        ]
        indexes = [
            models.Index(fields=["user"]),
        ]


class Jwks(models.Model):
    id = models.TextField(primary_key=True, default=None)
    public_key = models.TextField(db_column="publicKey")
    private_key = models.TextField(db_column="privateKey")
    created_at = UtcDateTimeField(db_column="createdAt")
    expires_at = UtcDateTimeField(null=True, db_column="expiresAt")
    alg = models.TextField(null=True)
    crv = models.TextField(null=True)

    class Meta:
        db_table = "Jwks"


class OauthResource(models.Model):
    id = models.TextField(primary_key=True, default=None)
    identifier = models.TextField(unique=True)
    name = models.TextField()
    access_token_ttl = models.IntegerField(null=True, db_column="accessTokenTtl")
    refresh_token_ttl = models.IntegerField(null=True, db_column="refreshTokenTtl")
    signing_algorithm = models.TextField(null=True, db_column="signingAlgorithm")
    signing_key_id = models.TextField(null=True, db_column="signingKeyId")
    allowed_scopes = ArrayField(models.TextField(), db_column="allowedScopes")
    custom_claims = models.JSONField(null=True, db_column="customClaims")
    dpop_bound_access_tokens_required = models.BooleanField(null=True, db_column="dpopBoundAccessTokensRequired", default=False)
    disabled = models.BooleanField(null=True, default=False)
    created_at = UtcDateTimeField(null=True, db_column="createdAt")
    updated_at = UtcDateTimeField(null=True, db_column="updatedAt")
    policy_version = models.IntegerField(null=True, db_column="policyVersion", default=1)
    metadata = models.JSONField(null=True)

    class Meta:
        db_table = "OauthResource"
        constraints = [
            models.UniqueConstraint(fields=["identifier"], name="OauthResource_identifier_key"),
        ]


class OauthClientResource(models.Model):
    id = models.TextField(primary_key=True, default=None)
    client = models.ForeignKey("OauthClient", on_delete=models.CASCADE, related_name="oauth_client_resources", to_field="client_id", db_column="clientId", db_index=False)
    oauth_resource = models.ForeignKey("OauthResource", on_delete=models.CASCADE, related_name="oauth_client_resources", to_field="identifier", db_column="resourceId", db_index=False)
    metadata = models.JSONField(null=True)
    created_at = UtcDateTimeField(null=True, db_column="createdAt")

    class Meta:
        db_table = "OauthClientResource"
        constraints = [
            models.UniqueConstraint(fields=["client", "oauth_resource"], name="oauthClientResource_clientId_resourceId_uidx"),
        ]
        indexes = [
            models.Index(fields=["client"]),
            models.Index(fields=["oauth_resource"]),
        ]


class OauthClientAssertion(models.Model):
    id = models.TextField(primary_key=True, default=None)
    expires_at = UtcDateTimeField(db_column="expiresAt")

    class Meta:
        db_table = "OauthClientAssertion"


class ScimManagedConnection(models.Model):
    id = models.TextField(primary_key=True, default=None)
    creation_request_id = models.TextField(unique=True, db_column="creationRequestId")
    connection_id = models.TextField(unique=True, db_column="connectionId")
    provisioning_domain_id = models.TextField(db_column="provisioningDomainId")
    status = models.TextField()
    revision = models.IntegerField()
    created_at = UtcDateTimeField(db_column="createdAt")
    created_by = models.TextField(db_column="createdBy")
    decommission_started_at = UtcDateTimeField(null=True, db_column="decommissionStartedAt")
    decommission_started_by = models.TextField(null=True, db_column="decommissionStartedBy")
    decommissioned_at = UtcDateTimeField(null=True, db_column="decommissionedAt")
    decommissioned_by = models.TextField(null=True, db_column="decommissionedBy")

    class Meta:
        db_table = "scimManagedConnection"
        constraints = [
            models.UniqueConstraint(fields=["creation_request_id"], name="scimManagedConnection_creationRequestId_key"),
            models.UniqueConstraint(fields=["connection_id"], name="scimManagedConnection_connectionId_key"),
        ]
        indexes = [
            models.Index(fields=["provisioning_domain_id"]),
        ]


class ScimManagedCredential(models.Model):
    id = models.TextField(primary_key=True, default=None)
    scimmanagedconnection = models.ForeignKey("ScimManagedConnection", on_delete=models.CASCADE, related_name="scimmanagedcredentials", db_column="connectionRecordId", db_index=False)
    credential_id = models.TextField(unique=True, db_column="credentialId")
    token_digest = models.TextField(db_column="tokenDigest")
    hash_version = models.TextField(db_column="hashVersion")
    active_slot_key = models.TextField(unique=True, db_column="activeSlotKey")
    status = models.TextField()
    serialized_scopes = models.TextField(db_column="serializedScopes")
    expires_at = UtcDateTimeField(db_column="expiresAt")
    created_at = UtcDateTimeField(db_column="createdAt")
    created_by = models.TextField(db_column="createdBy")
    last_used_at = UtcDateTimeField(null=True, db_column="lastUsedAt")
    revoked_at = UtcDateTimeField(null=True, db_column="revokedAt")
    revoked_by = models.TextField(null=True, db_column="revokedBy")
    decommissioned_at = UtcDateTimeField(null=True, db_column="decommissionedAt")

    class Meta:
        db_table = "scimManagedCredential"
        constraints = [
            models.UniqueConstraint(fields=["credential_id"], name="scimManagedCredential_credentialId_key"),
            models.UniqueConstraint(fields=["active_slot_key"], name="scimManagedCredential_activeSlotKey_key"),
        ]
        indexes = [
            models.Index(fields=["scimmanagedconnection"]),
        ]


class ScimManagedConnectionEvent(models.Model):
    id = models.TextField(primary_key=True, default=None)
    scimmanagedconnection = models.ForeignKey("ScimManagedConnection", on_delete=models.CASCADE, related_name="scimmanagedconnectionevents", db_column="connectionRecordId", db_index=False)
    event_key = models.TextField(unique=True, db_column="eventKey")
    sequence = models.IntegerField()
    type = models.TextField()
    actor_id = models.TextField(db_column="actorId")
    credential_id = models.TextField(null=True, db_column="credentialId")
    created_at = UtcDateTimeField(db_column="createdAt")

    class Meta:
        db_table = "scimManagedConnectionEvent"
        constraints = [
            models.UniqueConstraint(fields=["event_key"], name="scimManagedConnectionEvent_eventKey_key"),
        ]
        indexes = [
            models.Index(fields=["scimmanagedconnection"]),
        ]


class ScimConnectionBinding(models.Model):
    id = models.TextField(primary_key=True, default=None)
    connection_id = models.TextField(db_column="connectionId")
    connection_key = models.TextField(unique=True, db_column="connectionKey")
    provisioning_domain_id = models.TextField(db_column="provisioningDomainId")
    created_at = UtcDateTimeField(db_column="createdAt")
    decommissioned_at = UtcDateTimeField(null=True, db_column="decommissionedAt")
    decommission_status = models.TextField(db_column="decommissionStatus", default="active")
    decommission_cursor_user_id = models.TextField(null=True, db_column="decommissionCursorUserId")
    decommission_reconciled_user_count = models.IntegerField(db_column="decommissionReconciledUserCount", default=0)
    decommission_batch_count = models.IntegerField(db_column="decommissionBatchCount", default=0)
    decommission_revision = models.IntegerField(db_column="decommissionRevision", default=0)
    decommission_completed_at = UtcDateTimeField(null=True, db_column="decommissionCompletedAt")
    decommission_lease_id = models.TextField(null=True, db_column="decommissionLeaseId")
    decommission_lease_expires_at = UtcDateTimeField(null=True, db_column="decommissionLeaseExpiresAt")

    class Meta:
        db_table = "scimConnectionBinding"
        constraints = [
            models.UniqueConstraint(fields=["connection_key"], name="scimConnectionBinding_connectionKey_key"),
        ]
        indexes = [
            models.Index(fields=["connection_id"]),
        ]


class ScimIdentityTombstone(models.Model):
    id = models.TextField(primary_key=True, default=None)
    connection_id = models.TextField(db_column="connectionId")
    provisioning_domain_id = models.TextField(db_column="provisioningDomainId")
    external_id = models.TextField(db_column="externalId")
    external_id_key = models.TextField(unique=True, db_column="externalIdKey")
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="scim_identity_tombstones", db_column="userId", db_index=False)
    profile = models.TextField()
    deleted_at = UtcDateTimeField(db_column="deletedAt")

    class Meta:
        db_table = "scimIdentityTombstone"
        constraints = [
            models.UniqueConstraint(fields=["external_id_key"], name="scimIdentityTombstone_externalIdKey_key"),
        ]
        indexes = [
            models.Index(fields=["connection_id"]),
            models.Index(fields=["provisioning_domain_id"]),
            models.Index(fields=["user"]),
        ]


class ScimSubject(models.Model):
    id = models.TextField(primary_key=True, default=None)
    user = models.OneToOneField("User", on_delete=models.CASCADE, related_name="scim_subject", db_column="userId")
    profile_source_id = models.TextField(null=True, db_column="profileSourceId")
    revision = models.IntegerField()
    created_at = UtcDateTimeField(db_column="createdAt")
    updated_at = UtcDateTimeField(db_column="updatedAt")

    class Meta:
        db_table = "scimSubject"
        constraints = [
            models.UniqueConstraint(fields=["user"], name="scimSubject_userId_key"),
        ]
        indexes = [
            models.Index(fields=["profile_source_id"]),
        ]


class ScimUser(models.Model):
    id = models.TextField(primary_key=True, default=None)
    connection_id = models.TextField(db_column="connectionId")
    provisioning_domain_id = models.TextField(db_column="provisioningDomainId")
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="scim_users", db_column="userId", db_index=False)
    connection_user_key = models.TextField(unique=True, db_column="connectionUserKey")
    user_name = models.TextField(db_column="userName")
    user_name_key = models.TextField(unique=True, db_column="userNameKey")
    primary_email = models.TextField(db_column="primaryEmail")
    work_email_value_index = models.TextField(db_column="workEmailValueIndex")
    email_value_index = models.TextField(db_column="emailValueIndex")
    display_name = models.TextField(db_column="displayName")
    formatted_name = models.TextField(db_column="formattedName")
    given_name = models.TextField(null=True, db_column="givenName")
    family_name = models.TextField(null=True, db_column="familyName")
    serialized_emails = models.TextField(db_column="serializedEmails")
    serialized_attributes = models.TextField(null=True, db_column="serializedAttributes")
    external_id = models.TextField(null=True, db_column="externalId")
    external_id_key = models.TextField(null=True, unique=True, db_column="externalIdKey")
    active = models.BooleanField()
    order_key = models.TextField(unique=True, db_column="orderKey")
    created_at = UtcDateTimeField(db_column="createdAt")
    updated_at = UtcDateTimeField(db_column="updatedAt")

    class Meta:
        db_table = "scimUser"
        constraints = [
            models.UniqueConstraint(fields=["connection_user_key"], name="scimUser_connectionUserKey_key"),
            models.UniqueConstraint(fields=["user_name_key"], name="scimUser_userNameKey_key"),
            models.UniqueConstraint(fields=["external_id_key"], name="scimUser_externalIdKey_key"),
            models.UniqueConstraint(fields=["order_key"], name="scimUser_orderKey_key"),
        ]
        indexes = [
            models.Index(fields=["connection_id"]),
            models.Index(fields=["provisioning_domain_id"]),
            models.Index(fields=["user"]),
        ]


class ScimProjectionGrant(models.Model):
    id = models.TextField(primary_key=True, default=None)
    connection_id = models.TextField(db_column="connectionId")
    provisioning_domain_id = models.TextField(db_column="provisioningDomainId")
    scim_user = models.ForeignKey("ScimUser", on_delete=models.CASCADE, related_name="scim_projection_grants", db_column="scimUserId", db_index=False)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="scim_projection_grants", db_column="userId", db_index=False)
    source_kind = models.TextField(db_column="sourceKind")
    source_id = models.TextField(db_column="sourceId")
    source_value = models.TextField(null=True, db_column="sourceValue")
    role = models.TextField()
    grant_key = models.TextField(unique=True, db_column="grantKey")
    created_at = UtcDateTimeField(db_column="createdAt")
    updated_at = UtcDateTimeField(db_column="updatedAt")

    class Meta:
        db_table = "scimProjectionGrant"
        constraints = [
            models.UniqueConstraint(fields=["grant_key"], name="scimProjectionGrant_grantKey_key"),
        ]
        indexes = [
            models.Index(fields=["connection_id"]),
            models.Index(fields=["provisioning_domain_id"]),
            models.Index(fields=["scim_user"]),
            models.Index(fields=["user"]),
        ]


class ScimGroup(models.Model):
    id = models.TextField(primary_key=True, default=None)
    connection_id = models.TextField(db_column="connectionId")
    provisioning_domain_id = models.TextField(db_column="provisioningDomainId")
    revision = models.IntegerField(default=0)
    display_name = models.TextField(db_column="displayName")
    display_name_key = models.TextField(unique=True, db_column="displayNameKey")
    external_id = models.TextField(null=True, db_column="externalId")
    external_id_key = models.TextField(null=True, unique=True, db_column="externalIdKey")
    order_key = models.TextField(unique=True, db_column="orderKey")
    created_at = UtcDateTimeField(db_column="createdAt")
    updated_at = UtcDateTimeField(db_column="updatedAt")

    class Meta:
        db_table = "scimGroup"
        constraints = [
            models.UniqueConstraint(fields=["display_name_key"], name="scimGroup_displayNameKey_key"),
            models.UniqueConstraint(fields=["external_id_key"], name="scimGroup_externalIdKey_key"),
            models.UniqueConstraint(fields=["order_key"], name="scimGroup_orderKey_key"),
        ]
        indexes = [
            models.Index(fields=["connection_id"]),
            models.Index(fields=["provisioning_domain_id"]),
        ]


class ScimGroupMember(models.Model):
    id = models.TextField(primary_key=True, default=None)
    connection_id = models.TextField(db_column="connectionId")
    scim_group = models.ForeignKey("ScimGroup", on_delete=models.CASCADE, related_name="scim_group_members", db_column="groupId", db_index=False)
    scim_user = models.ForeignKey("ScimUser", on_delete=models.CASCADE, related_name="scim_group_members", db_column="scimUserId", db_index=False)
    membership_key = models.TextField(unique=True, db_column="membershipKey")
    created_at = UtcDateTimeField(db_column="createdAt")

    class Meta:
        db_table = "scimGroupMember"
        constraints = [
            models.UniqueConstraint(fields=["membership_key"], name="scimGroupMember_membershipKey_key"),
        ]
        indexes = [
            models.Index(fields=["connection_id"]),
            models.Index(fields=["scim_group"]),
            models.Index(fields=["scim_user"]),
        ]


class ScimIdentityLink(models.Model):
    id = models.TextField(primary_key=True, default=None)
    connection_id = models.TextField(db_column="connectionId")
    external_id = models.TextField(db_column="externalId")
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="scim_identity_links", db_column="userId", db_index=False)

    class Meta:
        db_table = "ScimIdentityLink"
        constraints = [
            models.UniqueConstraint(fields=["connection_id", "external_id"], name="ScimIdentityLink_connectionId_externalId_key"),
        ]
        indexes = [
            models.Index(fields=["user"]),
        ]


class LoopsEmailSource(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    email_message_id = models.TextField(null=True, unique=True, db_column="emailMessageId")
    loop_id = models.TextField(null=True, db_column="loopId")
    loop_name = models.TextField(null=True, db_column="loopName")
    campaign_id = models.TextField(null=True, db_column="campaignId")
    campaign_name = models.TextField(null=True, db_column="campaignName")
    source_type = models.TextField(null=True, db_column="sourceType")
    event_time = UtcDateTimeField(null=True, db_column="eventTime")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "LoopsEmailSource"
        indexes = [
            models.Index(fields=["loop_id"]),
            models.Index(fields=["campaign_id"]),
        ]


class CategoryToRule(models.Model):
    pk = models.CompositePrimaryKey("a_id", "b_id")
    a = models.ForeignKey("Category", on_delete=models.CASCADE, related_name="+", db_column="A", db_index=False)
    b = models.ForeignKey("Rule", on_delete=models.CASCADE, related_name="+", db_column="B", db_index=False)

    class Meta:
        db_table = "_CategoryToRule"
        indexes = [
            models.Index(fields=["b"]),
        ]
