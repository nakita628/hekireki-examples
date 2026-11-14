from sqlalchemy import ARRAY, Column, Dialect, Enum, ForeignKey, ForeignKeyConstraint, Index, MetaData, PrimaryKeyConstraint, Table, Text, TypeDecorator, text
from sqlalchemy.dialects.postgresql import JSONB, TIMESTAMP
from sqlalchemy.orm import DeclarativeBase, Mapped, mapped_column, relationship
from typing import Any, Optional
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

category_to_rule = Table(
    "_CategoryToRule",
    Base.metadata,
    Column("A", Text, ForeignKey("Category.id", ondelete="CASCADE", onupdate="CASCADE", name="_CategoryToRule_A_fkey"), nullable=False),
    Column("B", Text, ForeignKey("Rule.id", ondelete="CASCADE", onupdate="CASCADE", name="_CategoryToRule_B_fkey"), nullable=False),
    PrimaryKeyConstraint("A", "B", name="_CategoryToRule_AB_pkey"),
    Index("_CategoryToRule_B_index", "B"),
)


class Account(Base):
    __tablename__ = "Account"

    password: Mapped[Optional[str]]
    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="Account_userId_fkey"))
    provider: Mapped[str]
    type: Mapped[str] = mapped_column(default="oidc", server_default=text("'oidc'"))
    provider_account_id: Mapped[str] = mapped_column("providerAccountId")
    refresh_token: Mapped[Optional[str]]
    refresh_token_expires_at: Mapped[Optional[datetime]] = mapped_column("refreshTokenExpiresAt")
    access_token: Mapped[Optional[str]]
    expires_at: Mapped[Optional[datetime]] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    disconnected_at: Mapped[Optional[datetime]] = mapped_column("disconnectedAt")
    token_type: Mapped[Optional[str]]
    scope: Mapped[Optional[str]]
    id_token: Mapped[Optional[str]]
    session_state: Mapped[Optional[str]]

    __table_args__ = (
        Index("Account_userId_idx", "userId"),
        Index("Account_provider_providerAccountId_key", "provider", "providerAccountId", unique=True),
    )

    user: Mapped["User"] = relationship(back_populates="accounts")
    email_account: Mapped[Optional["EmailAccount"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="account")

class Session(Base):
    __tablename__ = "Session"

    id: Mapped[str] = mapped_column(primary_key=True)
    session_token: Mapped[str] = mapped_column("sessionToken")
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="Session_userId_fkey"))
    expires: Mapped[datetime]
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    email_otp: Mapped[bool] = mapped_column("emailOtp", default=False, server_default=text("false"))
    email_otp_version: Mapped[int] = mapped_column("emailOtpVersion", default=0, server_default=text("0"))
    ip_address: Mapped[Optional[str]] = mapped_column("ipAddress")
    user_agent: Mapped[Optional[str]] = mapped_column("userAgent")
    active_organization_id: Mapped[Optional[str]] = mapped_column("activeOrganizationId", ForeignKey("Organization.id", ondelete="SET NULL", onupdate="CASCADE", name="Session_activeOrganizationId_fkey"))

    __table_args__ = (
        Index("Session_sessionToken_key", "sessionToken", unique=True),
        Index("Session_userId_idx", "userId"),
        Index("Session_activeOrganizationId_idx", "activeOrganizationId"),
    )

    user: Mapped["User"] = relationship(back_populates="sessions")
    active_organization: Mapped[Optional["Organization"]] = relationship(back_populates="sessions")
    oauth_refresh_token_records: Mapped[list["OauthRefreshToken"]] = relationship(back_populates="session")
    oauth_access_token_records: Mapped[list["OauthAccessToken"]] = relationship(back_populates="session")

class User(Base):
    __tablename__ = "User"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    name: Mapped[Optional[str]]
    email: Mapped[str]
    email_verified: Mapped[Optional[bool]] = mapped_column("emailVerified", default=False, server_default=text("false"))
    image: Mapped[Optional[str]]
    completed_onboarding_at: Mapped[Optional[datetime]] = mapped_column("completedOnboardingAt")
    signup_intent: Mapped[Optional[str]] = mapped_column("signupIntent", Enum("AI_ASSISTANT", "MAIL_CLIENT", "BOTH", name="SignupIntent"))
    onboarding_answers: Mapped[Optional[dict[str, Any]]] = mapped_column("onboardingAnswers", JSONB(none_as_null=True))
    last_login: Mapped[Optional[datetime]] = mapped_column("lastLogin")
    utms: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    error_messages: Mapped[Optional[dict[str, Any]]] = mapped_column("errorMessages", JSONB(none_as_null=True))
    announcement_dismissed_at: Mapped[Optional[datetime]] = mapped_column("announcementDismissedAt")
    dismissed_hints: Mapped[list[str]] = mapped_column("dismissedHints", ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    survey_features: Mapped[list[str]] = mapped_column("surveyFeatures", ARRAY(Text), nullable=True)
    survey_role: Mapped[Optional[str]] = mapped_column("surveyRole")
    survey_goal: Mapped[Optional[str]] = mapped_column("surveyGoal")
    survey_company_size: Mapped[Optional[int]] = mapped_column("surveyCompanySize")
    survey_source: Mapped[Optional[str]] = mapped_column("surveySource")
    survey_improvements: Mapped[Optional[str]] = mapped_column("surveyImprovements")
    mcp_server_enabled: Mapped[bool] = mapped_column("mcpServerEnabled", default=False, server_default=text("false"))
    mcp_token_version: Mapped[int] = mapped_column("mcpTokenVersion", default=0, server_default=text("0"))
    email_otp_enabled: Mapped[bool] = mapped_column("emailOtpEnabled", default=False, server_default=text("false"))
    email_otp_version: Mapped[int] = mapped_column("emailOtpVersion", default=0, server_default=text("0"))
    ai_provider: Mapped[Optional[str]] = mapped_column("aiProvider")
    ai_model: Mapped[Optional[str]] = mapped_column("aiModel")
    ai_api_key: Mapped[Optional[str]] = mapped_column("aiApiKey")
    decision_model_enabled: Mapped[Optional[bool]] = mapped_column("decisionModelEnabled")
    webhook_secret: Mapped[Optional[str]] = mapped_column("webhookSecret")
    referral_code: Mapped[Optional[str]] = mapped_column("referralCode")
    premium_id: Mapped[Optional[str]] = mapped_column("premiumId", ForeignKey("Premium.id", ondelete="SET NULL", onupdate="CASCADE", name="User_premiumId_fkey"))
    premium_admin_id: Mapped[Optional[str]] = mapped_column("premiumAdminId", ForeignKey("Premium.id", ondelete="SET NULL", onupdate="CASCADE", name="User_premiumAdminId_fkey"))
    scim_access_disabled: Mapped[bool] = mapped_column("scimAccessDisabled", default=False, server_default=text("false"))

    __table_args__ = (
        Index("User_email_key", "email", unique=True),
        Index("User_referralCode_key", "referralCode", unique=True),
    )

    premium: Mapped[Optional["Premium"]] = relationship(foreign_keys=[premium_id], back_populates="users")
    premium_admin: Mapped[Optional["Premium"]] = relationship(foreign_keys=[premium_admin_id], back_populates="admins")
    accounts: Mapped[list["Account"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    sessions: Mapped[list["Session"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    api_keys: Mapped[list["ApiKey"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    email_accounts: Mapped[list["EmailAccount"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    referrals_made: Mapped[list["Referral"]] = relationship(foreign_keys="Referral.referrer_user_id", cascade="all, delete", passive_deletes=True, back_populates="referrer_user")
    mobile_push_tokens: Mapped[list["MobilePushToken"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    oauth_client_records: Mapped[list["OauthClient"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    oauth_refresh_token_records: Mapped[list["OauthRefreshToken"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    oauth_access_token_records: Mapped[list["OauthAccessToken"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    oauth_consent_records: Mapped[list["OauthConsent"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    sso_providers: Mapped[list["SsoProvider"]] = relationship(back_populates="user")
    scim_identity_tombstones: Mapped[list["ScimIdentityTombstone"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    scim_users: Mapped[list["ScimUser"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    scim_projection_grants: Mapped[list["ScimProjectionGrant"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    scim_identity_links: Mapped[list["ScimIdentityLink"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    referral_received: Mapped[Optional["Referral"]] = relationship(foreign_keys="Referral.referred_user_id", cascade="all, delete", passive_deletes=True, back_populates="referred_user")
    scim_subject: Mapped[Optional["ScimSubject"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")

class EmailAccount(Base):
    __tablename__ = "EmailAccount"

    id: Mapped[str] = mapped_column(primary_key=True)
    email: Mapped[str]
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    name: Mapped[Optional[str]]
    image: Mapped[Optional[str]]
    sort_order: Mapped[Optional[int]] = mapped_column("sortOrder")
    about: Mapped[Optional[str]]
    writing_style: Mapped[Optional[str]] = mapped_column("writingStyle")
    signature: Mapped[Optional[str]]
    sent_signature_scan: Mapped[Optional[dict[str, Any]]] = mapped_column("sentSignatureScan", JSONB(none_as_null=True))
    sent_signature_scanned_at: Mapped[Optional[datetime]] = mapped_column("sentSignatureScannedAt")
    include_referral_signature: Mapped[bool] = mapped_column("includeReferralSignature", default=False, server_default=text("false"))
    include_sent_with_signature: Mapped[bool] = mapped_column("includeSentWithSignature", default=True, server_default=text("true"))
    sent_message_open_tracking_enabled: Mapped[bool] = mapped_column("sentMessageOpenTrackingEnabled", default=True, server_default=text("true"))
    watch_emails_expiration_date: Mapped[Optional[datetime]] = mapped_column("watchEmailsExpirationDate")
    watch_emails_subscription_id: Mapped[Optional[str]] = mapped_column("watchEmailsSubscriptionId")
    watch_emails_subscription_history: Mapped[Optional[dict[str, Any]]] = mapped_column("watchEmailsSubscriptionHistory", JSONB(none_as_null=True))
    last_synced_history_id: Mapped[Optional[str]] = mapped_column("lastSyncedHistoryId")
    behavior_profile: Mapped[Optional[dict[str, Any]]] = mapped_column("behaviorProfile", JSONB(none_as_null=True))
    persona_analysis: Mapped[Optional[dict[str, Any]]] = mapped_column("personaAnalysis", JSONB(none_as_null=True))
    role: Mapped[Optional[str]]
    timezone_: Mapped[Optional[str]] = mapped_column("timezone")
    calendar_booking_link: Mapped[Optional[str]] = mapped_column("calendarBookingLink")
    draft_docs_urls: Mapped[list[str]] = mapped_column("draftDocsUrls", ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    stats_email_frequency: Mapped[str] = mapped_column("statsEmailFrequency", Enum("NEVER", "DAILY", "WEEKLY", name="Frequency"), default="WEEKLY", server_default=text("'WEEKLY'"))
    summary_email_frequency: Mapped[str] = mapped_column("summaryEmailFrequency", Enum("NEVER", "DAILY", "WEEKLY", name="Frequency"), default="WEEKLY", server_default=text("'WEEKLY'"))
    last_summary_email_at: Mapped[Optional[datetime]] = mapped_column("lastSummaryEmailAt")
    last_inbox_health_email_at: Mapped[Optional[datetime]] = mapped_column("lastInboxHealthEmailAt")
    cold_email_blocker: Mapped[Optional[str]] = mapped_column("coldEmailBlocker", Enum("DISABLED", "LIST", "LABEL", "ARCHIVE_AND_LABEL", "ARCHIVE_AND_READ_AND_LABEL", name="ColdEmailSetting"))
    cold_email_digest: Mapped[bool] = mapped_column("coldEmailDigest", default=False, server_default=text("false"))
    cold_email_prompt: Mapped[Optional[str]] = mapped_column("coldEmailPrompt")
    rules_prompt: Mapped[Optional[str]] = mapped_column("rulesPrompt")
    auto_categorize_senders: Mapped[bool] = mapped_column("autoCategorizeSenders", default=False, server_default=text("false"))
    multi_rule_selection_enabled: Mapped[bool] = mapped_column("multiRuleSelectionEnabled", default=False, server_default=text("false"))
    sensitive_data_policy: Mapped[Optional[str]] = mapped_column("sensitiveDataPolicy")
    draft_reply_confidence: Mapped[str] = mapped_column("draftReplyConfidence", Enum("ALL_EMAILS", "STANDARD", "HIGH_CONFIDENCE", name="DraftReplyConfidence"), default="ALL_EMAILS", server_default=text("'ALL_EMAILS'"))
    draft_cleanup_days: Mapped[Optional[int]] = mapped_column("draftCleanupDays", default=14, server_default=text("14"))
    allow_hidden_ai_draft_links: Mapped[bool] = mapped_column("allowHiddenAiDraftLinks", default=False, server_default=text("false"))
    rules_revision: Mapped[int] = mapped_column("rulesRevision", default=0, server_default=text("0"))
    mail_layout: Mapped[str] = mapped_column("mailLayout", Enum("LIST", "SPLIT", name="MailLayout"), default="LIST", server_default=text("'LIST'"))
    mail_expanded_preview: Mapped[bool] = mapped_column("mailExpandedPreview", default=False, server_default=text("false"))
    mail_bundle_inbox: Mapped[Optional[bool]] = mapped_column("mailBundleInbox")
    mail_default_split_id: Mapped[Optional[str]] = mapped_column("mailDefaultSplitId")
    mail_setup: Mapped[str] = mapped_column("mailSetup", Enum("PENDING", "SUGGESTED", "DONE", name="MailSetupState"), default="PENDING", server_default=text("'PENDING'"))
    include_in_all_accounts: Mapped[bool] = mapped_column("includeInAllAccounts", default=True, server_default=text("true"))
    meeting_briefings_enabled: Mapped[bool] = mapped_column("meetingBriefingsEnabled", default=False, server_default=text("false"))
    meeting_briefings_minutes_before: Mapped[int] = mapped_column("meetingBriefingsMinutesBefore", default=240, server_default=text("240"))
    meeting_briefs_send_email: Mapped[bool] = mapped_column("meetingBriefsSendEmail", default=True, server_default=text("true"))
    meeting_recorder_enabled: Mapped[bool] = mapped_column("meetingRecorderEnabled", default=False, server_default=text("false"))
    meeting_recorder_join_rule: Mapped[str] = mapped_column("meetingRecorderJoinRule", Enum("ALL", "EXTERNAL_ONLY", "HOST_ONLY", "OFF", name="MeetingJoinRule"), default="ALL", server_default=text("'ALL'"))
    meeting_recorder_recap_email_enabled: Mapped[bool] = mapped_column("meetingRecorderRecapEmailEnabled", default=True, server_default=text("true"))
    meeting_recorder_follow_up_draft_enabled: Mapped[bool] = mapped_column("meetingRecorderFollowUpDraftEnabled", default=True, server_default=text("true"))
    filing_enabled: Mapped[bool] = mapped_column("filingEnabled", default=False, server_default=text("false"))
    filing_prompt: Mapped[Optional[str]] = mapped_column("filingPrompt")
    filing_confirmation_send_email: Mapped[bool] = mapped_column("filingConfirmationSendEmail", default=True, server_default=text("true"))
    follow_up_awaiting_reply_days: Mapped[Optional[float]] = mapped_column("followUpAwaitingReplyDays")
    follow_up_needs_reply_days: Mapped[Optional[float]] = mapped_column("followUpNeedsReplyDays")
    follow_up_auto_draft_enabled: Mapped[bool] = mapped_column("followUpAutoDraftEnabled", default=False, server_default=text("false"))
    digest_send_email: Mapped[bool] = mapped_column("digestSendEmail", default=True, server_default=text("true"))
    learned_writing_style: Mapped[Optional[str]] = mapped_column("learnedWritingStyle")
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="EmailAccount_userId_fkey"))
    account_id: Mapped[str] = mapped_column("accountId", ForeignKey("Account.id", ondelete="CASCADE", onupdate="CASCADE", name="EmailAccount_accountId_fkey"))

    __table_args__ = (
        Index("EmailAccount_email_key", "email", unique=True),
        Index("EmailAccount_accountId_key", "accountId", unique=True),
        Index("EmailAccount_userId_idx", "userId"),
        Index("EmailAccount_lastSummaryEmailAt_idx", "lastSummaryEmailAt"),
        Index("EmailAccount_lastInboxHealthEmailAt_idx", "lastInboxHealthEmailAt"),
        Index("EmailAccount_watchEmailsSubscriptionId_idx", "watchEmailsSubscriptionId"),
        Index("EmailAccount_watchEmailsSubscriptionHistory_idx", "watchEmailsSubscriptionHistory"),
    )

    user: Mapped["User"] = relationship(back_populates="email_accounts")
    account: Mapped["Account"] = relationship(back_populates="email_account")
    labels: Mapped[list["Label"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="email_account")
    rules: Mapped[list["Rule"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="email_account")
    executed_rules: Mapped[list["ExecutedRule"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="email_account")
    newsletters: Mapped[list["Newsletter"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="email_account")
    cold_emails: Mapped[list["ColdEmail"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="email_account")
    groups: Mapped[list["Group"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="email_account")
    categories: Mapped[list["Category"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="email_account")
    thread_trackers: Mapped[list["ThreadTracker"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="email_account")
    cleanup_jobs: Mapped[list["CleanupJob"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="email_account")
    bulk_inbox_action_jobs: Mapped[list["BulkInboxActionJob"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="email_account")
    cleanup_threads: Mapped[list["CleanupThread"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="email_account")
    email_messages: Mapped[list["EmailMessage"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="email_account")
    email_tokens: Mapped[list["EmailToken"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="email_account")
    knowledge: Mapped[list["Knowledge"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="email_account")
    snippets: Mapped[list["Snippet"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="email_account")
    email_signatures: Mapped[list["EmailSignature"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="email_account")
    reply_memories: Mapped[list["ReplyMemory"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="email_account")
    chats: Mapped[list["Chat"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="email_account")
    chat_memories: Mapped[list["ChatMemory"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="email_account")
    digests: Mapped[list["Digest"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="email_account")
    scheduled_actions: Mapped[list["ScheduledAction"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="email_account")
    snoozed_threads: Mapped[list["SnoozedThread"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="email_account")
    email_send_operations: Mapped[list["EmailSendOperation"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="email_account")
    sent_message_opens: Mapped[list["SentMessageOpen"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="email_account")
    scheduled_emails: Mapped[list["ScheduledEmail"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="email_account")
    response_times: Mapped[list["ResponseTime"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="email_account")
    actions: Mapped[list["Action"]] = relationship(cascade="all, delete", passive_deletes=True, overlaps="actions,rule", back_populates="email_account")
    members: Mapped[list["Member"]] = relationship(passive_deletes="all", back_populates="email_account")
    invitations: Mapped[list["Invitation"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="inviter")
    ssoproviders: Mapped[list["SsoProvider"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="email_account")
    calendar_connections: Mapped[list["CalendarConnection"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="email_account")
    availability_schedules: Mapped[list["AvailabilitySchedule"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="email_account")
    booking_links: Mapped[list["BookingLink"]] = relationship(cascade="all, delete", passive_deletes=True, overlaps="availability_schedule,booking_links", back_populates="email_account")
    bookings: Mapped[list["Booking"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="email_account")
    mcp_connections: Mapped[list["McpConnection"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="email_account")
    mcp_integrations: Mapped[list["McpIntegration"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="email_account")
    meeting_briefings: Mapped[list["MeetingBriefing"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="email_account")
    meetings: Mapped[list["Meeting"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="email_account")
    meeting_recordings: Mapped[list["MeetingRecording"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="email_account")
    drive_connections: Mapped[list["DriveConnection"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="email_account")
    messaging_channels: Mapped[list["MessagingChannel"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="email_account")
    document_filings: Mapped[list["DocumentFiling"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="email_account")
    filing_folders: Mapped[list["FilingFolder"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="email_account")
    api_keys: Mapped[list["ApiKey"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="email_account")
    classification_feedback: Mapped[list["ClassificationFeedback"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="email_account")
    mail_splits: Mapped[list["MailSplit"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="email_account")
    digest_schedule: Mapped[Optional["Schedule"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="email_account")

class MailSplit(Base):
    __tablename__ = "MailSplit"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    name: Mapped[str]
    order: Mapped[int]
    match_all: Mapped[bool] = mapped_column("matchAll", default=True, server_default=text("true"))
    bundle_in_inbox: Mapped[bool] = mapped_column("bundleInInbox", default=False, server_default=text("false"))
    hide_from_all: Mapped[bool] = mapped_column("hideFromAll", default=False, server_default=text("false"))
    email_account_id: Mapped[str] = mapped_column("emailAccountId", ForeignKey("EmailAccount.id", ondelete="CASCADE", onupdate="CASCADE", name="MailSplit_emailAccountId_fkey"))

    __table_args__ = (
        Index("MailSplit_emailAccountId_order_idx", "emailAccountId", "order"),
        Index("MailSplit_emailAccountId_name_key", "emailAccountId", "name", unique=True),
    )

    email_account: Mapped["EmailAccount"] = relationship(back_populates="mail_splits")
    filters: Mapped[list["MailSplitFilter"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="mail_split")

class MailSplitFilter(Base):
    __tablename__ = "MailSplitFilter"

    id: Mapped[str] = mapped_column(primary_key=True)
    kind: Mapped[str] = mapped_column(Enum("UNREAD", "STARRED", "LABEL", "CATEGORY", "FROM", "OLDER_THAN", "TO", name="MailSplitFilterKind"))
    value: Mapped[Optional[str]]
    order: Mapped[int]
    mail_split_id: Mapped[str] = mapped_column("mailSplitId", ForeignKey("MailSplit.id", ondelete="CASCADE", onupdate="CASCADE", name="MailSplitFilter_mailSplitId_fkey"))

    __table_args__ = (
        Index("MailSplitFilter_mailSplitId_idx", "mailSplitId"),
        Index("MailSplitFilter_kind_value_idx", "kind", "value"),
        Index("MailSplitFilter_mailSplitId_order_key", "mailSplitId", "order", unique=True),
    )

    mail_split: Mapped["MailSplit"] = relationship(back_populates="filters")

class Organization(Base):
    __tablename__ = "Organization"

    id: Mapped[str] = mapped_column(primary_key=True)
    name: Mapped[str]
    slug: Mapped[str]
    logo: Mapped[Optional[str]]
    metadata_: Mapped[Optional[dict[str, Any]]] = mapped_column("metadata", JSONB(none_as_null=True))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("Organization_slug_key", "slug", unique=True),
    )

    members: Mapped[list["Member"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organization")
    sessions: Mapped[list["Session"]] = relationship(back_populates="active_organization")
    invitations: Mapped[list["Invitation"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organization")
    rules: Mapped[list["OrganizationRule"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organization")
    conversations: Mapped[list["Conversation"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organization")
    sso_provider: Mapped[Optional["SsoProvider"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organization")

class OrganizationRule(Base):
    __tablename__ = "OrganizationRule"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    organization_id: Mapped[str] = mapped_column("organizationId", ForeignKey("Organization.id", ondelete="CASCADE", onupdate="CASCADE", name="OrganizationRule_organizationId_fkey"))
    enabled: Mapped[bool] = mapped_column(default=True, server_default=text("true"))
    name: Mapped[str]
    instructions: Mapped[Optional[str]]
    run_on_threads: Mapped[bool] = mapped_column("runOnThreads", default=False, server_default=text("false"))
    conditional_operator: Mapped[str] = mapped_column("conditionalOperator", Enum("AND", "OR", name="LogicalOperator"), default="AND", server_default=text("'AND'"))
    from_: Mapped[Optional[str]] = mapped_column("from")
    to: Mapped[Optional[str]]
    subject: Mapped[Optional[str]]
    body: Mapped[Optional[str]]

    __table_args__ = (
        Index("OrganizationRule_organizationId_enabled_idx", "organizationId", "enabled"),
        Index("OrganizationRule_organizationId_name_key", "organizationId", "name", unique=True),
    )

    organization: Mapped["Organization"] = relationship(back_populates="rules")
    actions: Mapped[list["OrganizationRuleAction"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organization_rule")
    member_rules: Mapped[list["Rule"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organization_rule")

class OrganizationRuleAction(Base):
    __tablename__ = "OrganizationRuleAction"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    organization_rule_id: Mapped[str] = mapped_column("organizationRuleId", ForeignKey("OrganizationRule.id", ondelete="CASCADE", onupdate="CASCADE", name="OrganizationRuleAction_organizationRuleId_fkey"))
    type: Mapped[str] = mapped_column(Enum("ARCHIVE", "LABEL", "REPLY", "SEND_EMAIL", "FORWARD", "DRAFT_EMAIL", "DRAFT_MESSAGING_CHANNEL", "NOTIFY_MESSAGING_CHANNEL", "MARK_SPAM", "CALL_WEBHOOK", "MARK_READ", "STAR", "DELETE", "DIGEST", "MOVE_FOLDER", "NOTIFY_SENDER", "INTEGRATION", name="ActionType"))
    label: Mapped[Optional[str]]
    subject: Mapped[Optional[str]]
    content: Mapped[Optional[str]]
    to: Mapped[Optional[str]]
    cc: Mapped[Optional[str]]
    bcc: Mapped[Optional[str]]
    url: Mapped[Optional[str]]
    folder_name: Mapped[Optional[str]] = mapped_column("folderName")
    delay_in_minutes: Mapped[Optional[int]] = mapped_column("delayInMinutes")
    static_attachments: Mapped[Optional[dict[str, Any]]] = mapped_column("staticAttachments", JSONB(none_as_null=True))

    __table_args__ = (
        Index("OrganizationRuleAction_organizationRuleId_idx", "organizationRuleId"),
    )

    organization_rule: Mapped["OrganizationRule"] = relationship(back_populates="actions")

class Member(Base):
    __tablename__ = "Member"

    id: Mapped[str] = mapped_column(primary_key=True)
    organization_id: Mapped[str] = mapped_column("organizationId", ForeignKey("Organization.id", ondelete="CASCADE", onupdate="CASCADE", name="Member_organizationId_fkey"))
    email_account_id: Mapped[str] = mapped_column("emailAccountId", ForeignKey("EmailAccount.id", ondelete="RESTRICT", onupdate="CASCADE", name="Member_emailAccountId_fkey"))
    role: Mapped[str] = mapped_column(default="member", server_default=text("'member'"))
    allow_org_admin_analytics: Mapped[bool] = mapped_column("allowOrgAdminAnalytics", default=False, server_default=text("false"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("Member_organizationId_emailAccountId_key", "organizationId", "emailAccountId", unique=True),
        Index("Member_emailAccountId_key", "emailAccountId", unique=True),
    )

    organization: Mapped["Organization"] = relationship(back_populates="members")
    email_account: Mapped["EmailAccount"] = relationship(back_populates="members")
    conversations: Mapped[list["Conversation"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="publisher")
    conversation_participants: Mapped[list["ConversationParticipant"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="member")
    conversation_comments: Mapped[list["ConversationComment"]] = relationship(back_populates="author")

class Conversation(Base):
    __tablename__ = "Conversation"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    organization_id: Mapped[str] = mapped_column("organizationId", ForeignKey("Organization.id", ondelete="CASCADE", onupdate="CASCADE", name="Conversation_organizationId_fkey"))
    publisher_id: Mapped[str] = mapped_column("publisherId", ForeignKey("Member.id", ondelete="CASCADE", onupdate="CASCADE", name="Conversation_publisherId_fkey"))
    thread_id: Mapped[str] = mapped_column("threadId")
    status: Mapped[str] = mapped_column(Enum("ACTIVE", "STOPPED", name="ConversationStatus"), default="ACTIVE", server_default=text("'ACTIVE'"))
    revision: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    generation: Mapped[int] = mapped_column(default=1, server_default=text("1"))

    __table_args__ = (
        Index("Conversation_organizationId_idx", "organizationId"),
        Index("Conversation_publisherId_threadId_key", "publisherId", "threadId", unique=True),
    )

    organization: Mapped["Organization"] = relationship(back_populates="conversations")
    publisher: Mapped["Member"] = relationship(back_populates="conversations")
    participants: Mapped[list["ConversationParticipant"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="conversation")
    comments: Mapped[list["ConversationComment"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="conversation")

class ConversationParticipant(Base):
    __tablename__ = "ConversationParticipant"

    id: Mapped[str] = mapped_column(primary_key=True)
    conversation_id: Mapped[str] = mapped_column("conversationId", ForeignKey("Conversation.id", ondelete="CASCADE", onupdate="CASCADE", name="ConversationParticipant_conversationId_fkey"))
    member_id: Mapped[str] = mapped_column("memberId", ForeignKey("Member.id", ondelete="CASCADE", onupdate="CASCADE", name="ConversationParticipant_memberId_fkey"))
    generation: Mapped[int]
    active: Mapped[bool] = mapped_column(default=True, server_default=text("true"))
    read_revision: Mapped[int] = mapped_column("readRevision", default=0, server_default=text("0"))
    muted: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("ConversationParticipant_memberId_idx", "memberId"),
        Index("ConversationParticipant_conversationId_memberId_generation_key", "conversationId", "memberId", "generation", unique=True),
    )

    conversation: Mapped["Conversation"] = relationship(back_populates="participants")
    member: Mapped["Member"] = relationship(back_populates="conversation_participants")
    activity: Mapped[list["ConversationActivity"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="participant")

class ConversationComment(Base):
    __tablename__ = "ConversationComment"

    id: Mapped[str] = mapped_column(primary_key=True)
    conversation_id: Mapped[str] = mapped_column("conversationId", ForeignKey("Conversation.id", ondelete="CASCADE", onupdate="CASCADE", name="ConversationComment_conversationId_fkey"))
    author_member_id: Mapped[Optional[str]] = mapped_column("authorMemberId", ForeignKey("Member.id", ondelete="SET NULL", onupdate="CASCADE", name="ConversationComment_authorMemberId_fkey"))
    client_mutation_id: Mapped[str] = mapped_column("clientMutationId")
    body: Mapped[str]
    revision: Mapped[int]
    deleted_at: Mapped[Optional[datetime]] = mapped_column("deletedAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("ConversationComment_conversationId_revision_key", "conversationId", "revision", unique=True),
        Index("ConversationComment_conversationId_authorMemberId_clientMut_key", "conversationId", "authorMemberId", "clientMutationId", unique=True),
    )

    conversation: Mapped["Conversation"] = relationship(back_populates="comments")
    author: Mapped[Optional["Member"]] = relationship(back_populates="conversation_comments")
    activity: Mapped[list["ConversationActivity"]] = relationship(back_populates="comment")

class ConversationActivity(Base):
    __tablename__ = "ConversationActivity"

    id: Mapped[str] = mapped_column(primary_key=True)
    participant_id: Mapped[str] = mapped_column("participantId", ForeignKey("ConversationParticipant.id", ondelete="CASCADE", onupdate="CASCADE", name="ConversationActivity_participantId_fkey"))
    comment_id: Mapped[Optional[str]] = mapped_column("commentId", ForeignKey("ConversationComment.id", ondelete="SET NULL", onupdate="CASCADE", name="ConversationActivity_commentId_fkey"))
    kind: Mapped[str] = mapped_column(Enum("INVITED", "COMMENT", "MENTION", name="ConversationActivityKind"))
    revision: Mapped[int]
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("ConversationActivity_participantId_createdAt_id_idx", "participantId", "createdAt", "id"),
        Index("ConversationActivity_participantId_kind_revision_key", "participantId", "kind", "revision", unique=True),
    )

    participant: Mapped["ConversationParticipant"] = relationship(back_populates="activity")
    comment: Mapped[Optional["ConversationComment"]] = relationship(back_populates="activity")

class Invitation(Base):
    __tablename__ = "invitation"

    id: Mapped[str] = mapped_column(primary_key=True)
    organization_id: Mapped[str] = mapped_column("organizationId", ForeignKey("Organization.id", ondelete="CASCADE", onupdate="CASCADE", name="invitation_organizationId_fkey"))
    email: Mapped[str]
    role: Mapped[Optional[str]]
    status: Mapped[str]
    expires_at: Mapped[datetime] = mapped_column("expiresAt")
    inviter_id: Mapped[str] = mapped_column("inviterId", ForeignKey("EmailAccount.id", ondelete="CASCADE", onupdate="CASCADE", name="invitation_inviterId_fkey"))

    __table_args__ = (
        Index("invitation_organizationId_idx", "organizationId"),
        Index("invitation_inviterId_idx", "inviterId"),
    )

    organization: Mapped["Organization"] = relationship(back_populates="invitations")
    inviter: Mapped["EmailAccount"] = relationship(back_populates="invitations")

class Verification(Base):
    __tablename__ = "verification"

    id: Mapped[str] = mapped_column(primary_key=True)
    identifier: Mapped[str]
    value: Mapped[str]
    expires_at: Mapped[datetime] = mapped_column("expiresAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

class SsoProvider(Base):
    __tablename__ = "ssoProvider"

    user_id: Mapped[Optional[str]] = mapped_column("userId", ForeignKey("User.id", ondelete="SET NULL", onupdate="CASCADE", name="ssoProvider_userId_fkey"))
    id: Mapped[str] = mapped_column(primary_key=True)
    issuer: Mapped[str]
    oidc_config: Mapped[Optional[str]] = mapped_column("oidcConfig")
    saml_config: Mapped[Optional[str]] = mapped_column("samlConfig")
    email_account_id: Mapped[Optional[str]] = mapped_column("emailAccountId", ForeignKey("EmailAccount.id", ondelete="CASCADE", onupdate="CASCADE", name="ssoProvider_emailAccountId_fkey"))
    provider_id: Mapped[str] = mapped_column("providerId")
    organization_id: Mapped[Optional[str]] = mapped_column("organizationId", ForeignKey("Organization.id", ondelete="CASCADE", onupdate="CASCADE", name="ssoProvider_organizationId_fkey"))
    domain: Mapped[str]

    __table_args__ = (
        Index("ssoProvider_providerId_key", "providerId", unique=True),
        Index("ssoProvider_organizationId_key", "organizationId", unique=True),
        Index("ssoProvider_emailAccountId_idx", "emailAccountId"),
    )

    user: Mapped[Optional["User"]] = relationship(back_populates="sso_providers")
    email_account: Mapped[Optional["EmailAccount"]] = relationship(back_populates="ssoproviders")
    organization: Mapped[Optional["Organization"]] = relationship(back_populates="sso_provider")

class Digest(Base):
    __tablename__ = "Digest"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    email_account_id: Mapped[str] = mapped_column("emailAccountId", ForeignKey("EmailAccount.id", ondelete="CASCADE", onupdate="CASCADE", name="Digest_emailAccountId_fkey"))
    sent_at: Mapped[Optional[datetime]] = mapped_column("sentAt")
    status: Mapped[str] = mapped_column(Enum("PENDING", "PROCESSING", "SENT", "FAILED", name="DigestStatus"), default="PENDING", server_default=text("'PENDING'"))

    __table_args__ = (
        Index("Digest_emailAccountId_idx", "emailAccountId"),
    )

    email_account: Mapped["EmailAccount"] = relationship(back_populates="digests")
    items: Mapped[list["DigestItem"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="digest")

class DigestItem(Base):
    __tablename__ = "DigestItem"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    message_id: Mapped[str] = mapped_column("messageId")
    thread_id: Mapped[str] = mapped_column("threadId")
    content: Mapped[str]
    digest_id: Mapped[str] = mapped_column("digestId", ForeignKey("Digest.id", ondelete="CASCADE", onupdate="CASCADE", name="DigestItem_digestId_fkey"))
    action_id: Mapped[Optional[str]] = mapped_column("actionId", ForeignKey("ExecutedAction.id", ondelete="CASCADE", onupdate="CASCADE", name="DigestItem_actionId_fkey"))
    cold_email_id: Mapped[Optional[str]] = mapped_column("coldEmailId", ForeignKey("ColdEmail.id", ondelete="SET NULL", onupdate="CASCADE", name="DigestItem_coldEmailId_fkey"))

    __table_args__ = (
        Index("DigestItem_actionId_idx", "actionId"),
        Index("DigestItem_coldEmailId_idx", "coldEmailId"),
        Index("DigestItem_digestId_threadId_messageId_key", "digestId", "threadId", "messageId", unique=True),
    )

    digest: Mapped["Digest"] = relationship(back_populates="items")
    action: Mapped[Optional["ExecutedAction"]] = relationship(back_populates="digest_items")
    cold_email: Mapped[Optional["ColdEmail"]] = relationship(back_populates="digest_items")

class Schedule(Base):
    __tablename__ = "Schedule"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    interval_days: Mapped[Optional[int]] = mapped_column("intervalDays")
    occurrences: Mapped[Optional[int]]
    days_of_week: Mapped[Optional[int]] = mapped_column("daysOfWeek")
    time_of_day: Mapped[Optional[datetime]] = mapped_column("timeOfDay")
    email_account_id: Mapped[str] = mapped_column("emailAccountId", ForeignKey("EmailAccount.id", ondelete="CASCADE", onupdate="CASCADE", name="Schedule_emailAccountId_fkey"))
    last_occurrence_at: Mapped[Optional[datetime]] = mapped_column("lastOccurrenceAt")
    next_occurrence_at: Mapped[Optional[datetime]] = mapped_column("nextOccurrenceAt")

    __table_args__ = (
        Index("Schedule_emailAccountId_key", "emailAccountId", unique=True),
    )

    email_account: Mapped["EmailAccount"] = relationship(back_populates="digest_schedule")

class Premium(Base):
    __tablename__ = "Premium"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    pending_invites: Mapped[list[str]] = mapped_column("pendingInvites", ARRAY(Text), nullable=True)
    lemon_squeezy_renews_at: Mapped[Optional[datetime]] = mapped_column("lemonSqueezyRenewsAt")
    lemon_squeezy_customer_id: Mapped[Optional[int]] = mapped_column("lemonSqueezyCustomerId")
    lemon_squeezy_subscription_id: Mapped[Optional[int]] = mapped_column("lemonSqueezySubscriptionId")
    lemon_squeezy_subscription_item_id: Mapped[Optional[int]] = mapped_column("lemonSqueezySubscriptionItemId")
    lemon_squeezy_order_id: Mapped[Optional[int]] = mapped_column("lemonSqueezyOrderId")
    lemon_squeezy_product_id: Mapped[Optional[int]] = mapped_column("lemonSqueezyProductId")
    lemon_squeezy_variant_id: Mapped[Optional[int]] = mapped_column("lemonSqueezyVariantId")
    lemon_license_key: Mapped[Optional[str]] = mapped_column("lemonLicenseKey")
    lemon_license_instance_id: Mapped[Optional[str]] = mapped_column("lemonLicenseInstanceId")
    lemon_subscription_status: Mapped[Optional[str]] = mapped_column("lemonSubscriptionStatus")
    stripe_customer_id: Mapped[Optional[str]] = mapped_column("stripeCustomerId")
    stripe_subscription_id: Mapped[Optional[str]] = mapped_column("stripeSubscriptionId")
    stripe_subscription_item_id: Mapped[Optional[str]] = mapped_column("stripeSubscriptionItemId")
    stripe_price_id: Mapped[Optional[str]] = mapped_column("stripePriceId")
    stripe_product_id: Mapped[Optional[str]] = mapped_column("stripeProductId")
    stripe_subscription_status: Mapped[Optional[str]] = mapped_column("stripeSubscriptionStatus")
    stripe_cancel_at: Mapped[Optional[datetime]] = mapped_column("stripeCancelAt")
    stripe_cancel_at_period_end: Mapped[Optional[bool]] = mapped_column("stripeCancelAtPeriodEnd")
    stripe_renews_at: Mapped[Optional[datetime]] = mapped_column("stripeRenewsAt")
    stripe_trial_end: Mapped[Optional[datetime]] = mapped_column("stripeTrialEnd")
    stripe_trial_conversion_invoice_id: Mapped[Optional[str]] = mapped_column("stripeTrialConversionInvoiceId")
    stripe_trial_converted_at: Mapped[Optional[datetime]] = mapped_column("stripeTrialConvertedAt")
    stripe_cancellation_initiated_at: Mapped[Optional[datetime]] = mapped_column("stripeCancellationInitiatedAt")
    stripe_canceled_at: Mapped[Optional[datetime]] = mapped_column("stripeCanceledAt")
    stripe_ended_at: Mapped[Optional[datetime]] = mapped_column("stripeEndedAt")
    stripe_cancellation_feedback: Mapped[Optional[str]] = mapped_column("stripeCancellationFeedback")
    stripe_cancellation_comment: Mapped[Optional[str]] = mapped_column("stripeCancellationComment")
    stripe_ai_overage_last_invoice_id: Mapped[Optional[str]] = mapped_column("stripeAiOverageLastInvoiceId")
    stripe_ai_overage_last_period_end: Mapped[Optional[datetime]] = mapped_column("stripeAiOverageLastPeriodEnd")
    stripe_ai_overage_last_units: Mapped[Optional[int]] = mapped_column("stripeAiOverageLastUnits")
    stripe_invoice_emails_enabled: Mapped[bool] = mapped_column("stripeInvoiceEmailsEnabled", default=False, server_default=text("false"))
    apple_original_transaction_id: Mapped[Optional[str]] = mapped_column("appleOriginalTransactionId")
    apple_latest_transaction_id: Mapped[Optional[str]] = mapped_column("appleLatestTransactionId")
    apple_product_id: Mapped[Optional[str]] = mapped_column("appleProductId")
    apple_subscription_group_identifier: Mapped[Optional[str]] = mapped_column("appleSubscriptionGroupIdentifier")
    apple_app_account_token: Mapped[Optional[str]] = mapped_column("appleAppAccountToken")
    apple_subscription_status: Mapped[Optional[str]] = mapped_column("appleSubscriptionStatus")
    apple_environment: Mapped[Optional[str]] = mapped_column("appleEnvironment")
    apple_purchase_date: Mapped[Optional[datetime]] = mapped_column("applePurchaseDate")
    apple_expires_at: Mapped[Optional[datetime]] = mapped_column("appleExpiresAt")
    apple_revoked_at: Mapped[Optional[datetime]] = mapped_column("appleRevokedAt")
    apple_offer_discount_type: Mapped[Optional[str]] = mapped_column("appleOfferDiscountType")
    admin_grant_expires_at: Mapped[Optional[datetime]] = mapped_column("adminGrantExpiresAt")
    admin_grant_tier: Mapped[Optional[str]] = mapped_column("adminGrantTier", Enum("BASIC_MONTHLY", "BASIC_ANNUALLY", "PRO_MONTHLY", "PRO_ANNUALLY", "BUSINESS_MONTHLY", "BUSINESS_ANNUALLY", "PLUS_MONTHLY", "PLUS_ANNUALLY", "BUSINESS_PLUS_MONTHLY", "BUSINESS_PLUS_ANNUALLY", "COPILOT_MONTHLY", "LIFETIME", name="PremiumTier"))
    tier: Mapped[Optional[str]] = mapped_column(Enum("BASIC_MONTHLY", "BASIC_ANNUALLY", "PRO_MONTHLY", "PRO_ANNUALLY", "BUSINESS_MONTHLY", "BUSINESS_ANNUALLY", "PLUS_MONTHLY", "PLUS_ANNUALLY", "BUSINESS_PLUS_MONTHLY", "BUSINESS_PLUS_ANNUALLY", "COPILOT_MONTHLY", "LIFETIME", name="PremiumTier"))
    email_accounts_access: Mapped[Optional[int]] = mapped_column("emailAccountsAccess")
    unsubscribe_month: Mapped[Optional[int]] = mapped_column("unsubscribeMonth")
    unsubscribe_credits: Mapped[Optional[int]] = mapped_column("unsubscribeCredits")
    ai_month: Mapped[Optional[int]] = mapped_column("aiMonth")
    ai_credits: Mapped[Optional[int]] = mapped_column("aiCredits")

    __table_args__ = (
        Index("Premium_stripeCustomerId_key", "stripeCustomerId", unique=True),
        Index("Premium_stripeSubscriptionId_key", "stripeSubscriptionId", unique=True),
        Index("Premium_stripeSubscriptionItemId_key", "stripeSubscriptionItemId", unique=True),
        Index("Premium_appleOriginalTransactionId_key", "appleOriginalTransactionId", unique=True),
        Index("Premium_appleLatestTransactionId_key", "appleLatestTransactionId", unique=True),
        Index("Premium_pendingInvites_idx", "pendingInvites"),
    )

    users: Mapped[list["User"]] = relationship(foreign_keys="User.premium_id", back_populates="premium")
    admins: Mapped[list["User"]] = relationship(foreign_keys="User.premium_admin_id", back_populates="premium_admin")
    payments: Mapped[list["Payment"]] = relationship(back_populates="premium")

class PendingPremiumGrant(Base):
    __tablename__ = "PendingPremiumGrant"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    email: Mapped[str]
    tier: Mapped[str] = mapped_column(Enum("BASIC_MONTHLY", "BASIC_ANNUALLY", "PRO_MONTHLY", "PRO_ANNUALLY", "BUSINESS_MONTHLY", "BUSINESS_ANNUALLY", "PLUS_MONTHLY", "PLUS_ANNUALLY", "BUSINESS_PLUS_MONTHLY", "BUSINESS_PLUS_ANNUALLY", "COPILOT_MONTHLY", "LIFETIME", name="PremiumTier"))
    count: Mapped[int] = mapped_column(default=1, server_default=text("1"))
    email_accounts_access: Mapped[Optional[int]] = mapped_column("emailAccountsAccess")

    __table_args__ = (
        Index("PendingPremiumGrant_email_key", "email", unique=True),
    )

class VerificationToken(Base):
    __tablename__ = "VerificationToken"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    identifier: Mapped[str]
    token: Mapped[str]
    expires: Mapped[datetime]

    __table_args__ = (
        Index("VerificationToken_token_key", "token", unique=True),
        Index("VerificationToken_identifier_token_key", "identifier", "token", unique=True),
    )

class Label(Base):
    __tablename__ = "Label"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    gmail_label_id: Mapped[str] = mapped_column("gmailLabelId")
    name: Mapped[str]
    description: Mapped[Optional[str]]
    enabled: Mapped[bool] = mapped_column(default=True, server_default=text("true"))
    email_account_id: Mapped[str] = mapped_column("emailAccountId", ForeignKey("EmailAccount.id", ondelete="CASCADE", onupdate="CASCADE", name="Label_emailAccountId_fkey"))

    __table_args__ = (
        Index("Label_emailAccountId_idx", "emailAccountId"),
        Index("Label_gmailLabelId_emailAccountId_key", "gmailLabelId", "emailAccountId", unique=True),
        Index("Label_name_emailAccountId_key", "name", "emailAccountId", unique=True),
    )

    email_account: Mapped["EmailAccount"] = relationship(back_populates="labels")

class Rule(Base):
    __tablename__ = "Rule"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    name: Mapped[str]
    enabled: Mapped[bool] = mapped_column(default=True, server_default=text("true"))
    automate: Mapped[bool] = mapped_column(default=True, server_default=text("true"))
    run_on_threads: Mapped[bool] = mapped_column("runOnThreads", default=False, server_default=text("false"))
    email_account_id: Mapped[str] = mapped_column("emailAccountId", ForeignKey("EmailAccount.id", ondelete="CASCADE", onupdate="CASCADE", name="Rule_emailAccountId_fkey"))
    organization_rule_id: Mapped[Optional[str]] = mapped_column("organizationRuleId", ForeignKey("OrganizationRule.id", ondelete="CASCADE", onupdate="CASCADE", name="Rule_organizationRuleId_fkey"))
    organization_rule_member_enabled: Mapped[Optional[bool]] = mapped_column("organizationRuleMemberEnabled")
    conditional_operator: Mapped[str] = mapped_column("conditionalOperator", Enum("AND", "OR", name="LogicalOperator"), default="AND", server_default=text("'AND'"))
    instructions: Mapped[Optional[str]]
    group_id: Mapped[Optional[str]] = mapped_column("groupId", ForeignKey("Group.id", ondelete="CASCADE", onupdate="CASCADE", name="Rule_groupId_fkey"))
    from_: Mapped[Optional[str]] = mapped_column("from")
    to: Mapped[Optional[str]]
    subject: Mapped[Optional[str]]
    body: Mapped[Optional[str]]
    category_filter_type: Mapped[Optional[str]] = mapped_column("categoryFilterType", Enum("INCLUDE", "EXCLUDE", name="CategoryFilterType"))
    system_type: Mapped[Optional[str]] = mapped_column("systemType", Enum("TO_REPLY", "FYI", "AWAITING_REPLY", "ACTIONED", "COLD_EMAIL", "NEWSLETTER", "MARKETING", "CALENDAR", "RECEIPT", "NOTIFICATION", "OTP", name="SystemType"))
    prompt_text: Mapped[Optional[str]] = mapped_column("promptText")

    __table_args__ = (
        Index("Rule_groupId_key", "groupId", unique=True),
        Index("Rule_emailAccountId_enabled_idx", "emailAccountId", "enabled"),
        Index("Rule_organizationRuleId_idx", "organizationRuleId"),
        Index("Rule_id_emailAccountId_key", "id", "emailAccountId", unique=True),
        Index("Rule_name_emailAccountId_key", "name", "emailAccountId", unique=True),
        Index("Rule_emailAccountId_systemType_key", "emailAccountId", "systemType", unique=True),
        Index("Rule_emailAccountId_organizationRuleId_key", "emailAccountId", "organizationRuleId", unique=True),
    )

    email_account: Mapped["EmailAccount"] = relationship(back_populates="rules")
    organization_rule: Mapped[Optional["OrganizationRule"]] = relationship(back_populates="member_rules")
    group: Mapped[Optional["Group"]] = relationship(back_populates="rule")
    actions: Mapped[list["Action"]] = relationship(cascade="all, delete", passive_deletes=True, overlaps="actions,email_account", back_populates="rule")
    attachment_sources: Mapped[list["AttachmentSource"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="rule")
    executed_rules: Mapped[list["ExecutedRule"]] = relationship(back_populates="rule")
    classification_feedback: Mapped[list["ClassificationFeedback"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="rule")
    history: Mapped[list["RuleHistory"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="rule")
    category_filters: Mapped[list["Category"]] = relationship(secondary=category_to_rule, back_populates="rules")

class Action(Base):
    __tablename__ = "Action"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    type: Mapped[str] = mapped_column(Enum("ARCHIVE", "LABEL", "REPLY", "SEND_EMAIL", "FORWARD", "DRAFT_EMAIL", "DRAFT_MESSAGING_CHANNEL", "NOTIFY_MESSAGING_CHANNEL", "MARK_SPAM", "CALL_WEBHOOK", "MARK_READ", "STAR", "DELETE", "DIGEST", "MOVE_FOLDER", "NOTIFY_SENDER", "INTEGRATION", name="ActionType"))
    rule_id: Mapped[str] = mapped_column("ruleId")
    email_account_id: Mapped[str] = mapped_column("emailAccountId", ForeignKey("EmailAccount.id", ondelete="CASCADE", onupdate="CASCADE", name="Action_emailAccountId_fkey"))
    messaging_channel_id: Mapped[Optional[str]] = mapped_column("messagingChannelId")
    messaging_channel_email_account_id: Mapped[Optional[str]] = mapped_column("messagingChannelEmailAccountId")
    label: Mapped[Optional[str]]
    label_id: Mapped[Optional[str]] = mapped_column("labelId")
    subject: Mapped[Optional[str]]
    content: Mapped[Optional[str]]
    to: Mapped[Optional[str]]
    cc: Mapped[Optional[str]]
    bcc: Mapped[Optional[str]]
    url: Mapped[Optional[str]]
    folder_name: Mapped[Optional[str]] = mapped_column("folderName")
    folder_id: Mapped[Optional[str]] = mapped_column("folderId")
    delay_in_minutes: Mapped[Optional[int]] = mapped_column("delayInMinutes")
    static_attachments: Mapped[Optional[dict[str, Any]]] = mapped_column("staticAttachments", JSONB(none_as_null=True))
    integration_name: Mapped[Optional[str]] = mapped_column("integrationName")
    integration_tool_name: Mapped[Optional[str]] = mapped_column("integrationToolName")
    integration_args: Mapped[Optional[dict[str, Any]]] = mapped_column("integrationArgs", JSONB(none_as_null=True))

    __table_args__ = (
        Index("Action_ruleId_idx", "ruleId"),
        Index("Action_emailAccountId_idx", "emailAccountId"),
        Index("Action_messagingChannelId_messagingChannelEmailAccountId_idx", "messagingChannelId", "messagingChannelEmailAccountId"),
        ForeignKeyConstraint(["ruleId", "emailAccountId"], ["Rule.id", "Rule.emailAccountId"], ondelete="CASCADE", onupdate="CASCADE", name="Action_ruleId_emailAccountId_fkey"),
        ForeignKeyConstraint(["messagingChannelId", "messagingChannelEmailAccountId"], ["MessagingChannel.id", "MessagingChannel.emailAccountId"], ondelete="SET NULL", onupdate="CASCADE", name="Action_messagingChannelId_messagingChannelEmailAccountId_fkey"),
    )

    rule: Mapped["Rule"] = relationship(overlaps="actions,email_account", back_populates="actions")
    email_account: Mapped["EmailAccount"] = relationship(overlaps="actions,rule", back_populates="actions")
    messaging_channel: Mapped[Optional["MessagingChannel"]] = relationship(back_populates="actions")

class RuleHistory(Base):
    __tablename__ = "RuleHistory"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    rule_id: Mapped[str] = mapped_column("ruleId", ForeignKey("Rule.id", ondelete="CASCADE", onupdate="CASCADE", name="RuleHistory_ruleId_fkey"))
    version: Mapped[int]
    trigger_type: Mapped[str] = mapped_column("triggerType")
    prompt_text: Mapped[Optional[str]] = mapped_column("promptText")
    name: Mapped[str]
    instructions: Mapped[Optional[str]]
    enabled: Mapped[bool]
    automate: Mapped[bool]
    run_on_threads: Mapped[bool] = mapped_column("runOnThreads")
    conditional_operator: Mapped[str] = mapped_column("conditionalOperator")
    from_: Mapped[Optional[str]] = mapped_column("from")
    to: Mapped[Optional[str]]
    subject: Mapped[Optional[str]]
    body: Mapped[Optional[str]]
    category_filter_type: Mapped[Optional[str]] = mapped_column("categoryFilterType")
    system_type: Mapped[Optional[str]] = mapped_column("systemType")
    actions: Mapped[dict[str, Any]] = mapped_column(JSONB)
    category_filters: Mapped[Optional[dict[str, Any]]] = mapped_column("categoryFilters", JSONB(none_as_null=True))

    __table_args__ = (
        Index("RuleHistory_ruleId_createdAt_idx", "ruleId", "createdAt"),
        Index("RuleHistory_ruleId_version_key", "ruleId", "version", unique=True),
    )

    rule: Mapped["Rule"] = relationship(back_populates="history")

class ExecutedRule(Base):
    __tablename__ = "ExecutedRule"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    thread_id: Mapped[str] = mapped_column("threadId")
    message_id: Mapped[str] = mapped_column("messageId")
    status: Mapped[str] = mapped_column(Enum("APPLIED", "APPLYING", "REJECTED", "PENDING", "SKIPPED", "ERROR", name="ExecutedRuleStatus"))
    automated: Mapped[bool]
    reason: Mapped[Optional[str]]
    match_metadata: Mapped[Optional[dict[str, Any]]] = mapped_column("matchMetadata", JSONB(none_as_null=True))
    rule_id: Mapped[Optional[str]] = mapped_column("ruleId", ForeignKey("Rule.id", ondelete="SET NULL", onupdate="CASCADE", name="ExecutedRule_ruleId_fkey"))
    email_account_id: Mapped[str] = mapped_column("emailAccountId", ForeignKey("EmailAccount.id", ondelete="CASCADE", onupdate="CASCADE", name="ExecutedRule_emailAccountId_fkey"))

    __table_args__ = (
        Index("ExecutedRule_emailAccountId_threadId_messageId_ruleId_idx", "emailAccountId", "threadId", "messageId", "ruleId"),
        Index("ExecutedRule_emailAccountId_messageId_idx", "emailAccountId", "messageId"),
        Index("ExecutedRule_emailAccountId_status_createdAt_idx", "emailAccountId", "status", "createdAt"),
        Index("ExecutedRule_emailAccountId_createdAt_idx", "emailAccountId", "createdAt"),
        Index("ExecutedRule_emailAccountId_id_idx", "emailAccountId", "id"),
        Index("ExecutedRule_emailAccountId_updatedAt_id_idx", "emailAccountId", "updatedAt", "id"),
        Index("ExecutedRule_ruleId_idx", "ruleId"),
    )

    rule: Mapped[Optional["Rule"]] = relationship(back_populates="executed_rules")
    email_account: Mapped["EmailAccount"] = relationship(back_populates="executed_rules")
    action_items: Mapped[list["ExecutedAction"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="executed_rule")
    scheduled_actions: Mapped[list["ScheduledAction"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="executed_rule")

class ExecutedAction(Base):
    __tablename__ = "ExecutedAction"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    type: Mapped[str] = mapped_column(Enum("ARCHIVE", "LABEL", "REPLY", "SEND_EMAIL", "FORWARD", "DRAFT_EMAIL", "DRAFT_MESSAGING_CHANNEL", "NOTIFY_MESSAGING_CHANNEL", "MARK_SPAM", "CALL_WEBHOOK", "MARK_READ", "STAR", "DELETE", "DIGEST", "MOVE_FOLDER", "NOTIFY_SENDER", "INTEGRATION", name="ActionType"))
    execution_status: Mapped[Optional[str]] = mapped_column("executionStatus", Enum("SUCCEEDED", "FAILED", "SKIPPED", name="ExecutedActionStatus"))
    execution_started_at: Mapped[Optional[datetime]] = mapped_column("executionStartedAt")
    executed_at: Mapped[Optional[datetime]] = mapped_column("executedAt")
    execution_error: Mapped[Optional[dict[str, Any]]] = mapped_column("executionError", JSONB(none_as_null=True))
    sent_message_ids: Mapped[list[str]] = mapped_column("sentMessageIds", ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    executed_rule_id: Mapped[str] = mapped_column("executedRuleId", ForeignKey("ExecutedRule.id", ondelete="CASCADE", onupdate="CASCADE", name="ExecutedAction_executedRuleId_fkey"))
    messaging_channel_id: Mapped[Optional[str]] = mapped_column("messagingChannelId", ForeignKey("MessagingChannel.id", ondelete="SET NULL", onupdate="CASCADE", name="ExecutedAction_messagingChannelId_fkey"))
    messaging_message_id: Mapped[Optional[str]] = mapped_column("messagingMessageId")
    messaging_message_sent_at: Mapped[Optional[datetime]] = mapped_column("messagingMessageSentAt")
    messaging_message_status: Mapped[Optional[str]] = mapped_column("messagingMessageStatus", Enum("SENT", "DRAFT_SENT", "DRAFT_EDITED", "DISMISSED", "EXPIRED", "FAILED", name="MessagingMessageStatus"))
    label: Mapped[Optional[str]]
    label_id: Mapped[Optional[str]] = mapped_column("labelId")
    subject: Mapped[Optional[str]]
    content: Mapped[Optional[str]]
    to: Mapped[Optional[str]]
    cc: Mapped[Optional[str]]
    bcc: Mapped[Optional[str]]
    url: Mapped[Optional[str]]
    folder_name: Mapped[Optional[str]] = mapped_column("folderName")
    folder_id: Mapped[Optional[str]] = mapped_column("folderId")
    static_attachments: Mapped[Optional[dict[str, Any]]] = mapped_column("staticAttachments", JSONB(none_as_null=True))
    selected_attachments: Mapped[Optional[dict[str, Any]]] = mapped_column("selectedAttachments", JSONB(none_as_null=True))
    integration_name: Mapped[Optional[str]] = mapped_column("integrationName")
    integration_tool_name: Mapped[Optional[str]] = mapped_column("integrationToolName")
    integration_args: Mapped[Optional[dict[str, Any]]] = mapped_column("integrationArgs", JSONB(none_as_null=True))
    draft_id: Mapped[Optional[str]] = mapped_column("draftId")
    draft_status: Mapped[Optional[str]] = mapped_column("draftStatus", Enum("PENDING", "LIKELY_SENT", "REPLIED_WITHOUT_DRAFT", "CLEANED_UP_UNUSED", "MISSING_FROM_PROVIDER", "USER_EDITED", "CLEANUP_SKIPPED", name="DraftEmailStatus"))
    was_draft_sent: Mapped[Optional[bool]] = mapped_column("wasDraftSent")
    draft_model_provider: Mapped[Optional[str]] = mapped_column("draftModelProvider")
    draft_model_name: Mapped[Optional[str]] = mapped_column("draftModelName")
    draft_pipeline_version: Mapped[Optional[int]] = mapped_column("draftPipelineVersion")
    draft_context_metadata: Mapped[Optional[dict[str, Any]]] = mapped_column("draftContextMetadata", JSONB(none_as_null=True))

    __table_args__ = (
        Index("ExecutedAction_executedRuleId_idx", "executedRuleId"),
        Index("ExecutedAction_executionStatus_createdAt_idx", "executionStatus", "createdAt"),
        Index("ExecutedAction_sentMessageIds_idx", "sentMessageIds"),
        Index("ExecutedAction_messagingChannelId_createdAt_idx", "messagingChannelId", "createdAt"),
        Index("ExecutedAction_draftStatus_createdAt_draft_idx", "draftStatus", "createdAt"),
        Index("ExecutedAction_executedRuleId_draft_idx", "executedRuleId"),
    )

    executed_rule: Mapped["ExecutedRule"] = relationship(back_populates="action_items")
    messaging_channel: Mapped[Optional["MessagingChannel"]] = relationship(back_populates="executed_actions")
    digest_items: Mapped[list["DigestItem"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="action")
    draft_send_log: Mapped[Optional["DraftSendLog"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="executed_action")
    scheduled_action: Mapped[Optional["ScheduledAction"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="executed_action")

class ScheduledAction(Base):
    __tablename__ = "ScheduledAction"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    executed_rule_id: Mapped[str] = mapped_column("executedRuleId", ForeignKey("ExecutedRule.id", ondelete="CASCADE", onupdate="CASCADE", name="ScheduledAction_executedRuleId_fkey"))
    action_type: Mapped[str] = mapped_column("actionType", Enum("ARCHIVE", "LABEL", "REPLY", "SEND_EMAIL", "FORWARD", "DRAFT_EMAIL", "DRAFT_MESSAGING_CHANNEL", "NOTIFY_MESSAGING_CHANNEL", "MARK_SPAM", "CALL_WEBHOOK", "MARK_READ", "STAR", "DELETE", "DIGEST", "MOVE_FOLDER", "NOTIFY_SENDER", "INTEGRATION", name="ActionType"))
    message_id: Mapped[str] = mapped_column("messageId")
    thread_id: Mapped[str] = mapped_column("threadId")
    scheduled_for: Mapped[datetime] = mapped_column("scheduledFor")
    email_account_id: Mapped[str] = mapped_column("emailAccountId", ForeignKey("EmailAccount.id", ondelete="CASCADE", onupdate="CASCADE", name="ScheduledAction_emailAccountId_fkey"))
    status: Mapped[str] = mapped_column(Enum("PENDING", "EXECUTING", "COMPLETED", "FAILED", "CANCELLED", name="ScheduledActionStatus"), default="PENDING", server_default=text("'PENDING'"))
    scheduling_status: Mapped[str] = mapped_column("schedulingStatus", Enum("PENDING", "SCHEDULED", "FAILED", name="SchedulingStatus"), default="PENDING", server_default=text("'PENDING'"))
    label: Mapped[Optional[str]]
    label_id: Mapped[Optional[str]] = mapped_column("labelId")
    subject: Mapped[Optional[str]]
    content: Mapped[Optional[str]]
    to: Mapped[Optional[str]]
    cc: Mapped[Optional[str]]
    bcc: Mapped[Optional[str]]
    url: Mapped[Optional[str]]
    folder_name: Mapped[Optional[str]] = mapped_column("folderName")
    folder_id: Mapped[Optional[str]] = mapped_column("folderId")
    scheduled_id: Mapped[Optional[str]] = mapped_column("scheduledId")
    static_attachments: Mapped[Optional[dict[str, Any]]] = mapped_column("staticAttachments", JSONB(none_as_null=True))
    selected_attachments: Mapped[Optional[dict[str, Any]]] = mapped_column("selectedAttachments", JSONB(none_as_null=True))
    executed_at: Mapped[Optional[datetime]] = mapped_column("executedAt")
    executed_action_id: Mapped[Optional[str]] = mapped_column("executedActionId", ForeignKey("ExecutedAction.id", ondelete="CASCADE", onupdate="CASCADE", name="ScheduledAction_executedActionId_fkey"))

    __table_args__ = (
        Index("ScheduledAction_executedActionId_key", "executedActionId", unique=True),
        Index("ScheduledAction_executedRuleId_idx", "executedRuleId"),
        Index("ScheduledAction_status_scheduledFor_idx", "status", "scheduledFor"),
        Index("ScheduledAction_emailAccountId_messageId_idx", "emailAccountId", "messageId"),
    )

    executed_rule: Mapped["ExecutedRule"] = relationship(back_populates="scheduled_actions")
    executed_action: Mapped[Optional["ExecutedAction"]] = relationship(back_populates="scheduled_action")
    email_account: Mapped["EmailAccount"] = relationship(back_populates="scheduled_actions")

class SnoozedThread(Base):
    __tablename__ = "SnoozedThread"

    id: Mapped[str] = mapped_column(primary_key=True)
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    thread_id: Mapped[str] = mapped_column("threadId")
    scheduled_for: Mapped[datetime] = mapped_column("scheduledFor")
    status: Mapped[str] = mapped_column(Enum("PREPARING", "PENDING", "EXECUTING", "COMPLETED", "CANCELLED", name="SnoozedThreadStatus"), default="PENDING", server_default=text("'PENDING'"))
    client_mutation_id: Mapped[Optional[str]] = mapped_column("clientMutationId")
    email_account_id: Mapped[str] = mapped_column("emailAccountId", ForeignKey("EmailAccount.id", ondelete="CASCADE", onupdate="CASCADE", name="SnoozedThread_emailAccountId_fkey"))

    __table_args__ = (
        Index("SnoozedThread_status_scheduledFor_idx", "status", "scheduledFor"),
        Index("SnoozedThread_emailAccountId_threadId_status_idx", "emailAccountId", "threadId", "status"),
        Index("SnoozedThread_emailAccountId_clientMutationId_key", "emailAccountId", "clientMutationId", unique=True),
    )

    email_account: Mapped["EmailAccount"] = relationship(back_populates="snoozed_threads")

class ScheduledEmail(Base):
    __tablename__ = "ScheduledEmail"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    email_account_id: Mapped[str] = mapped_column("emailAccountId", ForeignKey("EmailAccount.id", ondelete="CASCADE", onupdate="CASCADE", name="ScheduledEmail_emailAccountId_fkey"))
    client_mutation_id: Mapped[str] = mapped_column("clientMutationId")
    payload_hash: Mapped[str] = mapped_column("payloadHash")
    payload: Mapped[dict[str, Any]] = mapped_column(JSONB)
    thread_id: Mapped[Optional[str]] = mapped_column("threadId")
    send_at: Mapped[datetime] = mapped_column("sendAt")
    status: Mapped[str] = mapped_column(Enum("PENDING", "PROCESSING", "SENT", "BLOCKED_AUTH", "UNCERTAIN", "FAILED", "CANCELLED", name="ScheduledEmailStatus"), default="PENDING", server_default=text("'PENDING'"))
    processing_started_at: Mapped[Optional[datetime]] = mapped_column("processingStartedAt")
    execution_queued_at: Mapped[Optional[datetime]] = mapped_column("executionQueuedAt")
    sent_at: Mapped[Optional[datetime]] = mapped_column("sentAt")
    error: Mapped[Optional[str]]
    remind_at: Mapped[Optional[datetime]] = mapped_column("remindAt")
    reminder_status: Mapped[str] = mapped_column("reminderStatus", Enum("NONE", "PENDING", "PROCESSING", "COMPLETED", "CANCELLED", name="EmailReminderStatus"), default="NONE", server_default=text("'NONE'"))
    reminder_started_at: Mapped[Optional[datetime]] = mapped_column("reminderStartedAt")
    held_for_undo: Mapped[bool] = mapped_column("heldForUndo", default=False, server_default=text("false"))
    draft_message_ids: Mapped[list[str]] = mapped_column("draftMessageIds", ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))

    __table_args__ = (
        Index("ScheduledEmail_status_sendAt_idx", "status", "sendAt"),
        Index("ScheduledEmail_status_processingStartedAt_idx", "status", "processingStartedAt"),
        Index("ScheduledEmail_reminderStatus_remindAt_idx", "reminderStatus", "remindAt"),
        Index("ScheduledEmail_reminderStatus_reminderStartedAt_idx", "reminderStatus", "reminderStartedAt"),
        Index("ScheduledEmail_emailAccountId_threadId_idx", "emailAccountId", "threadId"),
        Index("ScheduledEmail_emailAccountId_clientMutationId_key", "emailAccountId", "clientMutationId", unique=True),
    )

    email_account: Mapped["EmailAccount"] = relationship(back_populates="scheduled_emails")

class EmailSendOperation(Base):
    __tablename__ = "EmailSendOperation"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    client_mutation_id: Mapped[str] = mapped_column("clientMutationId")
    payload_hash: Mapped[str] = mapped_column("payloadHash")
    status: Mapped[str] = mapped_column(Enum("PROCESSING", "SENT", "UNCERTAIN", name="EmailSendOperationStatus"), default="PROCESSING", server_default=text("'PROCESSING'"))
    processing_started_at: Mapped[datetime] = mapped_column("processingStartedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    result: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    email_account_id: Mapped[str] = mapped_column("emailAccountId", ForeignKey("EmailAccount.id", ondelete="CASCADE", onupdate="CASCADE", name="EmailSendOperation_emailAccountId_fkey"))

    __table_args__ = (
        Index("EmailSendOperation_status_updatedAt_idx", "status", "updatedAt"),
        Index("EmailSendOperation_emailAccountId_clientMutationId_key", "emailAccountId", "clientMutationId", unique=True),
    )

    email_account: Mapped["EmailAccount"] = relationship(back_populates="email_send_operations")

class SentMessageOpen(Base):
    __tablename__ = "SentMessageOpen"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    token: Mapped[str]
    email_account_id: Mapped[str] = mapped_column("emailAccountId", ForeignKey("EmailAccount.id", ondelete="CASCADE", onupdate="CASCADE", name="SentMessageOpen_emailAccountId_fkey"))
    thread_id: Mapped[Optional[str]] = mapped_column("threadId")
    message_id: Mapped[Optional[str]] = mapped_column("messageId")
    first_opened_at: Mapped[Optional[datetime]] = mapped_column("firstOpenedAt")
    last_opened_at: Mapped[Optional[datetime]] = mapped_column("lastOpenedAt")
    open_count: Mapped[int] = mapped_column("openCount", default=0, server_default=text("0"))

    __table_args__ = (
        Index("SentMessageOpen_token_key", "token", unique=True),
        Index("SentMessageOpen_emailAccountId_threadId_idx", "emailAccountId", "threadId"),
        Index("SentMessageOpen_emailAccountId_messageId_key", "emailAccountId", "messageId", unique=True),
    )

    email_account: Mapped["EmailAccount"] = relationship(back_populates="sent_message_opens")

class Group(Base):
    __tablename__ = "Group"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    name: Mapped[str]
    prompt: Mapped[Optional[str]]
    email_account_id: Mapped[str] = mapped_column("emailAccountId", ForeignKey("EmailAccount.id", ondelete="CASCADE", onupdate="CASCADE", name="Group_emailAccountId_fkey"))

    __table_args__ = (
        Index("Group_emailAccountId_idx", "emailAccountId"),
        Index("Group_name_emailAccountId_key", "name", "emailAccountId", unique=True),
    )

    email_account: Mapped["EmailAccount"] = relationship(back_populates="groups")
    items: Mapped[list["GroupItem"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="group")
    rule: Mapped[Optional["Rule"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="group")

class GroupItem(Base):
    __tablename__ = "GroupItem"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    group_id: Mapped[Optional[str]] = mapped_column("groupId", ForeignKey("Group.id", ondelete="CASCADE", onupdate="CASCADE", name="GroupItem_groupId_fkey"))
    type: Mapped[str] = mapped_column(Enum("FROM", "SUBJECT", "BODY", name="GroupItemType"))
    value: Mapped[str]
    exclude: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    reason: Mapped[Optional[str]]
    thread_id: Mapped[Optional[str]] = mapped_column("threadId")
    message_id: Mapped[Optional[str]] = mapped_column("messageId")
    source: Mapped[Optional[str]] = mapped_column(Enum("AI", "USER", "LABEL_REMOVED", "LABEL_ADDED", name="GroupItemSource"))

    __table_args__ = (
        Index("GroupItem_groupId_type_value_key", "groupId", "type", "value", unique=True),
    )

    group: Mapped[Optional["Group"]] = relationship(back_populates="items")

class ClassificationFeedback(Base):
    __tablename__ = "ClassificationFeedback"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    sender: Mapped[str]
    event_type: Mapped[str] = mapped_column("eventType", Enum("LABEL_ADDED", "LABEL_REMOVED", name="ClassificationFeedbackEventType"))
    rule_id: Mapped[str] = mapped_column("ruleId", ForeignKey("Rule.id", ondelete="CASCADE", onupdate="CASCADE", name="ClassificationFeedback_ruleId_fkey"))
    email_account_id: Mapped[str] = mapped_column("emailAccountId", ForeignKey("EmailAccount.id", ondelete="CASCADE", onupdate="CASCADE", name="ClassificationFeedback_emailAccountId_fkey"))
    thread_id: Mapped[str] = mapped_column("threadId")
    message_id: Mapped[str] = mapped_column("messageId")

    __table_args__ = (
        Index("ClassificationFeedback_ruleId_idx", "ruleId"),
        Index("ClassificationFeedback_emailAccountId_sender_ruleId_message_key", "emailAccountId", "sender", "ruleId", "messageId", "eventType", unique=True),
    )

    rule: Mapped["Rule"] = relationship(back_populates="classification_feedback")
    email_account: Mapped["EmailAccount"] = relationship(back_populates="classification_feedback")

class Category(Base):
    __tablename__ = "Category"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    name: Mapped[str]
    description: Mapped[Optional[str]]
    email_account_id: Mapped[str] = mapped_column("emailAccountId", ForeignKey("EmailAccount.id", ondelete="CASCADE", onupdate="CASCADE", name="Category_emailAccountId_fkey"))

    __table_args__ = (
        Index("Category_emailAccountId_idx", "emailAccountId"),
        Index("Category_name_emailAccountId_key", "name", "emailAccountId", unique=True),
    )

    email_account: Mapped["EmailAccount"] = relationship(back_populates="categories")
    email_senders: Mapped[list["Newsletter"]] = relationship(back_populates="category")
    rules: Mapped[list["Rule"]] = relationship(secondary=category_to_rule, back_populates="category_filters")

class Newsletter(Base):
    __tablename__ = "Newsletter"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    email: Mapped[str]
    name: Mapped[Optional[str]]
    status: Mapped[Optional[str]] = mapped_column(Enum("APPROVED", "UNSUBSCRIBED", "AUTO_ARCHIVED", name="NewsletterStatus"))
    pattern_analyzed: Mapped[bool] = mapped_column("patternAnalyzed", default=False, server_default=text("false"))
    last_analyzed_at: Mapped[Optional[datetime]] = mapped_column("lastAnalyzedAt")
    email_account_id: Mapped[str] = mapped_column("emailAccountId", ForeignKey("EmailAccount.id", ondelete="CASCADE", onupdate="CASCADE", name="Newsletter_emailAccountId_fkey"))
    category_id: Mapped[Optional[str]] = mapped_column("categoryId", ForeignKey("Category.id", ondelete="SET NULL", onupdate="CASCADE", name="Newsletter_categoryId_fkey"))

    __table_args__ = (
        Index("Newsletter_emailAccountId_status_idx", "emailAccountId", "status"),
        Index("Newsletter_categoryId_idx", "categoryId"),
        Index("Newsletter_email_emailAccountId_key", "email", "emailAccountId", unique=True),
    )

    email_account: Mapped["EmailAccount"] = relationship(back_populates="newsletters")
    category: Mapped[Optional["Category"]] = relationship(back_populates="email_senders")

class ColdEmail(Base):
    __tablename__ = "ColdEmail"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    from_email: Mapped[str] = mapped_column("fromEmail")
    message_id: Mapped[Optional[str]] = mapped_column("messageId")
    thread_id: Mapped[Optional[str]] = mapped_column("threadId")
    status: Mapped[Optional[str]] = mapped_column(Enum("AI_LABELED_COLD", "USER_REJECTED_COLD", name="ColdEmailStatus"))
    reason: Mapped[Optional[str]]
    email_account_id: Mapped[str] = mapped_column("emailAccountId", ForeignKey("EmailAccount.id", ondelete="CASCADE", onupdate="CASCADE", name="ColdEmail_emailAccountId_fkey"))

    __table_args__ = (
        Index("ColdEmail_emailAccountId_status_idx", "emailAccountId", "status"),
        Index("ColdEmail_emailAccountId_createdAt_idx", "emailAccountId", "createdAt"),
        Index("ColdEmail_emailAccountId_fromEmail_key", "emailAccountId", "fromEmail", unique=True),
    )

    email_account: Mapped["EmailAccount"] = relationship(back_populates="cold_emails")
    digest_items: Mapped[list["DigestItem"]] = relationship(back_populates="cold_email")

class EmailMessage(Base):
    __tablename__ = "EmailMessage"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    thread_id: Mapped[str] = mapped_column("threadId")
    message_id: Mapped[str] = mapped_column("messageId")
    date_: Mapped[datetime] = mapped_column("date")
    from_: Mapped[str] = mapped_column("from")
    from_name: Mapped[Optional[str]] = mapped_column("fromName")
    from_domain: Mapped[str] = mapped_column("fromDomain")
    to: Mapped[str]
    unsubscribe_link: Mapped[Optional[str]] = mapped_column("unsubscribeLink")
    read: Mapped[bool]
    sent: Mapped[bool]
    draft: Mapped[bool]
    inbox: Mapped[bool]
    email_account_id: Mapped[str] = mapped_column("emailAccountId", ForeignKey("EmailAccount.id", ondelete="CASCADE", onupdate="CASCADE", name="EmailMessage_emailAccountId_fkey"))

    __table_args__ = (
        Index("EmailMessage_emailAccountId_threadId_idx", "emailAccountId", "threadId"),
        Index("EmailMessage_emailAccountId_date_idx", "emailAccountId", "date"),
        Index("EmailMessage_emailAccountId_from_idx", "emailAccountId", "from"),
        Index("EmailMessage_emailAccountId_fromName_idx", "emailAccountId", "fromName"),
        Index("EmailMessage_emailAccountId_threadId_messageId_key", "emailAccountId", "threadId", "messageId", unique=True),
    )

    email_account: Mapped["EmailAccount"] = relationship(back_populates="email_messages")

class ResponseTime(Base):
    __tablename__ = "ResponseTime"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    thread_id: Mapped[str] = mapped_column("threadId")
    sent_message_id: Mapped[str] = mapped_column("sentMessageId")
    received_message_id: Mapped[str] = mapped_column("receivedMessageId")
    response_time_mins: Mapped[int] = mapped_column("responseTimeMins")
    received_at: Mapped[datetime] = mapped_column("receivedAt")
    sent_at: Mapped[datetime] = mapped_column("sentAt")
    email_account_id: Mapped[str] = mapped_column("emailAccountId", ForeignKey("EmailAccount.id", ondelete="CASCADE", onupdate="CASCADE", name="ResponseTime_emailAccountId_fkey"))

    __table_args__ = (
        Index("ResponseTime_emailAccountId_sentAt_idx", "emailAccountId", "sentAt"),
        Index("ResponseTime_emailAccountId_sentMessageId_key", "emailAccountId", "sentMessageId", unique=True),
    )

    email_account: Mapped["EmailAccount"] = relationship(back_populates="response_times")

class ThreadTracker(Base):
    __tablename__ = "ThreadTracker"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    sent_at: Mapped[datetime] = mapped_column("sentAt")
    thread_id: Mapped[str] = mapped_column("threadId")
    message_id: Mapped[str] = mapped_column("messageId")
    resolved: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    type: Mapped[str] = mapped_column(Enum("AWAITING", "NEEDS_REPLY", "NEEDS_ACTION", name="ThreadTrackerType"))
    follow_up_applied_at: Mapped[Optional[datetime]] = mapped_column("followUpAppliedAt")
    follow_up_draft_id: Mapped[Optional[str]] = mapped_column("followUpDraftId")
    follow_up_draft_content: Mapped[Optional[str]] = mapped_column("followUpDraftContent")
    follow_up_draft_created_at: Mapped[Optional[datetime]] = mapped_column("followUpDraftCreatedAt")
    follow_up_notifications: Mapped[Optional[dict[str, Any]]] = mapped_column("followUpNotifications", JSONB(none_as_null=True))
    email_account_id: Mapped[str] = mapped_column("emailAccountId", ForeignKey("EmailAccount.id", ondelete="CASCADE", onupdate="CASCADE", name="ThreadTracker_emailAccountId_fkey"))

    __table_args__ = (
        Index("ThreadTracker_emailAccountId_resolved_idx", "emailAccountId", "resolved"),
        Index("ThreadTracker_emailAccountId_resolved_sentAt_type_idx", "emailAccountId", "resolved", "sentAt", "type"),
        Index("ThreadTracker_emailAccountId_type_resolved_sentAt_idx", "emailAccountId", "type", "resolved", "sentAt"),
        Index("ThreadTracker_emailAccountId_type_resolved_followUpAppliedA_idx", "emailAccountId", "type", "resolved", "followUpAppliedAt", "sentAt"),
        Index("ThreadTracker_followUpDraft_cleanup_idx", "emailAccountId", "followUpDraftCreatedAt"),
        Index("ThreadTracker_emailAccountId_threadId_messageId_key", "emailAccountId", "threadId", "messageId", unique=True),
    )

    email_account: Mapped["EmailAccount"] = relationship(back_populates="thread_trackers")

class CleanupJob(Base):
    __tablename__ = "CleanupJob"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    action: Mapped[str] = mapped_column(Enum("ARCHIVE", "MARK_READ", name="CleanAction"), default="ARCHIVE", server_default=text("'ARCHIVE'"))
    days_old: Mapped[int] = mapped_column("daysOld", default=7, server_default=text("7"))
    instructions: Mapped[Optional[str]]
    skip_reply: Mapped[Optional[bool]] = mapped_column("skipReply")
    skip_starred: Mapped[Optional[bool]] = mapped_column("skipStarred")
    skip_calendar: Mapped[Optional[bool]] = mapped_column("skipCalendar")
    skip_receipt: Mapped[Optional[bool]] = mapped_column("skipReceipt")
    skip_attachment: Mapped[Optional[bool]] = mapped_column("skipAttachment")
    skip_conversation: Mapped[Optional[bool]] = mapped_column("skipConversation")
    email_account_id: Mapped[str] = mapped_column("emailAccountId", ForeignKey("EmailAccount.id", ondelete="CASCADE", onupdate="CASCADE", name="CleanupJob_emailAccountId_fkey"))

    __table_args__ = (
        Index("CleanupJob_emailAccountId_idx", "emailAccountId"),
    )

    email_account: Mapped["EmailAccount"] = relationship(back_populates="cleanup_jobs")
    threads: Mapped[list["CleanupThread"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="job")

class CleanupThread(Base):
    __tablename__ = "CleanupThread"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    thread_id: Mapped[str] = mapped_column("threadId")
    archived: Mapped[bool]
    job_id: Mapped[str] = mapped_column("jobId", ForeignKey("CleanupJob.id", ondelete="CASCADE", onupdate="CASCADE", name="CleanupThread_jobId_fkey"))
    email_account_id: Mapped[str] = mapped_column("emailAccountId", ForeignKey("EmailAccount.id", ondelete="CASCADE", onupdate="CASCADE", name="CleanupThread_emailAccountId_fkey"))

    __table_args__ = (
        Index("CleanupThread_jobId_idx", "jobId"),
        Index("CleanupThread_emailAccountId_idx", "emailAccountId"),
    )

    job: Mapped["CleanupJob"] = relationship(back_populates="threads")
    email_account: Mapped["EmailAccount"] = relationship(back_populates="cleanup_threads")

class BulkInboxActionJob(Base):
    __tablename__ = "BulkInboxActionJob"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    status: Mapped[str] = mapped_column(Enum("RUNNING", "COMPLETED", "FAILED", "CANCELLED", name="BulkInboxActionJobStatus"), default="RUNNING", server_default=text("'RUNNING'"))
    search: Mapped[dict[str, Any]] = mapped_column(JSONB)
    action: Mapped[str]
    label_id: Mapped[Optional[str]] = mapped_column("labelId")
    label_name: Mapped[Optional[str]] = mapped_column("labelName")
    instruction: Mapped[Optional[str]]
    page_token: Mapped[Optional[str]] = mapped_column("pageToken")
    rate_limited_until: Mapped[Optional[datetime]] = mapped_column("rateLimitedUntil")
    lease_expires_at: Mapped[Optional[datetime]] = mapped_column("leaseExpiresAt")
    chunk_token: Mapped[Optional[str]] = mapped_column("chunkToken")
    recovery_count: Mapped[int] = mapped_column("recoveryCount", default=0, server_default=text("0"))
    processed_count: Mapped[int] = mapped_column("processedCount", default=0, server_default=text("0"))
    matched_count: Mapped[int] = mapped_column("matchedCount", default=0, server_default=text("0"))
    acted_count: Mapped[int] = mapped_column("actedCount", default=0, server_default=text("0"))
    skipped_count: Mapped[int] = mapped_column("skippedCount", default=0, server_default=text("0"))
    failed_count: Mapped[int] = mapped_column("failedCount", default=0, server_default=text("0"))
    limit_reached: Mapped[bool] = mapped_column("limitReached", default=False, server_default=text("false"))
    error: Mapped[Optional[str]]
    completed_at: Mapped[Optional[datetime]] = mapped_column("completedAt")
    confirmation_key: Mapped[str] = mapped_column("confirmationKey")
    chat_id: Mapped[Optional[str]] = mapped_column("chatId", ForeignKey("Chat.id", ondelete="SET NULL", onupdate="CASCADE", name="BulkInboxActionJob_chatId_fkey"))
    email_account_id: Mapped[str] = mapped_column("emailAccountId", ForeignKey("EmailAccount.id", ondelete="CASCADE", onupdate="CASCADE", name="BulkInboxActionJob_emailAccountId_fkey"))

    __table_args__ = (
        Index("BulkInboxActionJob_chatId_updatedAt_idx", "chatId", "updatedAt"),
        Index("BulkInboxActionJob_emailAccountId_confirmationKey_key", "emailAccountId", "confirmationKey", unique=True),
    )

    chat: Mapped[Optional["Chat"]] = relationship(back_populates="bulk_inbox_action_jobs")
    email_account: Mapped["EmailAccount"] = relationship(back_populates="bulk_inbox_action_jobs")
    handled_threads: Mapped[list["BulkInboxActionThread"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="job")

class BulkInboxActionThread(Base):
    __tablename__ = "BulkInboxActionThread"

    job_id: Mapped[str] = mapped_column("jobId", ForeignKey("BulkInboxActionJob.id", ondelete="CASCADE", onupdate="CASCADE", name="BulkInboxActionThread_jobId_fkey"), primary_key=True)
    thread_id: Mapped[str] = mapped_column("threadId", primary_key=True)

    job: Mapped["BulkInboxActionJob"] = relationship(back_populates="handled_threads")

class Knowledge(Base):
    __tablename__ = "Knowledge"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    title: Mapped[str]
    content: Mapped[str]
    email_account_id: Mapped[str] = mapped_column("emailAccountId", ForeignKey("EmailAccount.id", ondelete="CASCADE", onupdate="CASCADE", name="Knowledge_emailAccountId_fkey"))

    __table_args__ = (
        Index("Knowledge_emailAccountId_title_key", "emailAccountId", "title", unique=True),
    )

    email_account: Mapped["EmailAccount"] = relationship(back_populates="knowledge")

class Snippet(Base):
    __tablename__ = "Snippet"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    shortcut: Mapped[str]
    content: Mapped[str]
    email_account_id: Mapped[str] = mapped_column("emailAccountId", ForeignKey("EmailAccount.id", ondelete="CASCADE", onupdate="CASCADE", name="Snippet_emailAccountId_fkey"))

    __table_args__ = (
        Index("Snippet_emailAccountId_idx", "emailAccountId"),
        Index("Snippet_emailAccountId_shortcut_key", "emailAccountId", "shortcut", unique=True),
    )

    email_account: Mapped["EmailAccount"] = relationship(back_populates="snippets")

class EmailSignature(Base):
    __tablename__ = "EmailSignature"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    name: Mapped[str]
    html: Mapped[str]
    email_account_id: Mapped[str] = mapped_column("emailAccountId", ForeignKey("EmailAccount.id", ondelete="CASCADE", onupdate="CASCADE", name="EmailSignature_emailAccountId_fkey"))

    __table_args__ = (
        Index("EmailSignature_emailAccountId_name_key", "emailAccountId", "name", unique=True),
    )

    email_account: Mapped["EmailAccount"] = relationship(back_populates="email_signatures")

class ReplyMemory(Base):
    __tablename__ = "ReplyMemory"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    content: Mapped[str]
    kind: Mapped[str] = mapped_column(Enum("FACT", "PREFERENCE", "PROCEDURE", name="ReplyMemoryKind"))
    scope_type: Mapped[str] = mapped_column("scopeType", Enum("GLOBAL", "SENDER", "DOMAIN", "TOPIC", name="ReplyMemoryScopeType"))
    scope_value: Mapped[str] = mapped_column("scopeValue")
    is_learned_style_evidence: Mapped[bool] = mapped_column("isLearnedStyleEvidence", default=False, server_default=text("false"))
    email_account_id: Mapped[str] = mapped_column("emailAccountId", ForeignKey("EmailAccount.id", ondelete="CASCADE", onupdate="CASCADE", name="ReplyMemory_emailAccountId_fkey"))

    __table_args__ = (
        Index("ReplyMemory_emailAccountId_updatedAt_idx", "emailAccountId", "updatedAt"),
        Index("ReplyMemory_emailAccountId_scopeType_scopeValue_idx", "emailAccountId", "scopeType", "scopeValue"),
        Index("ReplyMemory_emailAccountId_kind_scopeType_scopeValue_conten_key", "emailAccountId", "kind", "scopeType", "scopeValue", "content", unique=True),
    )

    email_account: Mapped["EmailAccount"] = relationship(back_populates="reply_memories")
    sources: Mapped[list["ReplyMemorySource"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="reply_memory")

class ReplyMemorySource(Base):
    __tablename__ = "ReplyMemorySource"

    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    learned_writing_style_analyzed_at: Mapped[Optional[datetime]] = mapped_column("learnedWritingStyleAnalyzedAt")
    reply_memory_id: Mapped[str] = mapped_column("replyMemoryId", ForeignKey("ReplyMemory.id", ondelete="CASCADE", onupdate="CASCADE", name="ReplyMemorySource_replyMemoryId_fkey"), primary_key=True)
    draft_send_log_id: Mapped[str] = mapped_column("draftSendLogId", ForeignKey("DraftSendLog.id", ondelete="CASCADE", onupdate="CASCADE", name="ReplyMemorySource_draftSendLogId_fkey"), primary_key=True)

    __table_args__ = (
        Index("ReplyMemorySource_draftSendLogId_idx", "draftSendLogId"),
        Index("ReplyMemorySource_replyMemoryId_learnedWritingStyleAnalyzed_idx", "replyMemoryId", "learnedWritingStyleAnalyzedAt", "createdAt"),
    )

    reply_memory: Mapped["ReplyMemory"] = relationship(back_populates="sources")
    draft_send_log: Mapped["DraftSendLog"] = relationship(back_populates="reply_memory_sources")

class ApiKey(Base):
    __tablename__ = "ApiKey"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    name: Mapped[Optional[str]]
    hashed_key: Mapped[str] = mapped_column("hashedKey")
    is_active: Mapped[bool] = mapped_column("isActive", default=True, server_default=text("true"))
    expires_at: Mapped[Optional[datetime]] = mapped_column("expiresAt")
    last_used_at: Mapped[Optional[datetime]] = mapped_column("lastUsedAt")
    scopes: Mapped[list[str]] = mapped_column(ARRAY(Enum("STATS_READ", "RULES_READ", "RULES_WRITE", "SETTINGS_READ", "SETTINGS_WRITE", "ASSISTANT_CHAT", "SENDERS_UNSUBSCRIBE", name="ApiKeyScope")), nullable=True, default=lambda: [], server_default=text("ARRAY[]::\"ApiKeyScope\"[]"))
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="ApiKey_userId_fkey"))
    email_account_id: Mapped[Optional[str]] = mapped_column("emailAccountId", ForeignKey("EmailAccount.id", ondelete="CASCADE", onupdate="CASCADE", name="ApiKey_emailAccountId_fkey"))

    __table_args__ = (
        Index("ApiKey_hashedKey_key", "hashedKey", unique=True),
        Index("ApiKey_userId_isActive_idx", "userId", "isActive"),
        Index("ApiKey_emailAccountId_isActive_idx", "emailAccountId", "isActive"),
    )

    user: Mapped["User"] = relationship(back_populates="api_keys")
    email_account: Mapped[Optional["EmailAccount"]] = relationship(back_populates="api_keys")

class EmailToken(Base):
    __tablename__ = "EmailToken"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    token: Mapped[str]
    expires_at: Mapped[datetime] = mapped_column("expiresAt")
    email_account_id: Mapped[str] = mapped_column("emailAccountId", ForeignKey("EmailAccount.id", ondelete="CASCADE", onupdate="CASCADE", name="EmailToken_emailAccountId_fkey"))

    __table_args__ = (
        Index("EmailToken_token_key", "token", unique=True),
        Index("EmailToken_emailAccountId_idx", "emailAccountId"),
    )

    email_account: Mapped["EmailAccount"] = relationship(back_populates="email_tokens")

class Payment(Base):
    __tablename__ = "Payment"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt")
    updated_at: Mapped[datetime] = mapped_column("updatedAt")
    premium_id: Mapped[Optional[str]] = mapped_column("premiumId", ForeignKey("Premium.id", ondelete="SET NULL", onupdate="CASCADE", name="Payment_premiumId_fkey"))
    processor_type: Mapped[str] = mapped_column("processorType", Enum("LEMON_SQUEEZY", "STRIPE", name="ProcessorType"), default="LEMON_SQUEEZY", server_default=text("'LEMON_SQUEEZY'"))
    processor_id: Mapped[Optional[str]] = mapped_column("processorId")
    processor_subscription_id: Mapped[Optional[str]] = mapped_column("processorSubscriptionId")
    processor_customer_id: Mapped[Optional[str]] = mapped_column("processorCustomerId")
    amount: Mapped[int]
    currency: Mapped[str]
    status: Mapped[str]
    tax: Mapped[int]
    tax_inclusive: Mapped[bool] = mapped_column("taxInclusive")
    refunded: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    refunded_at: Mapped[Optional[datetime]] = mapped_column("refundedAt")
    refunded_amount: Mapped[Optional[int]] = mapped_column("refundedAmount")
    billing_reason: Mapped[Optional[str]] = mapped_column("billingReason")
    invoice_email_sent_at: Mapped[Optional[datetime]] = mapped_column("invoiceEmailSentAt")

    __table_args__ = (
        Index("Payment_processorId_key", "processorId", unique=True),
        Index("Payment_premiumId_createdAt_idx", "premiumId", "createdAt"),
    )

    premium: Mapped[Optional["Premium"]] = relationship(back_populates="payments")

class DraftSendLog(Base):
    __tablename__ = "DraftSendLog"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    executed_action_id: Mapped[str] = mapped_column("executedActionId", ForeignKey("ExecutedAction.id", ondelete="CASCADE", onupdate="CASCADE", name="DraftSendLog_executedActionId_fkey"))
    sent_message_id: Mapped[str] = mapped_column("sentMessageId")
    similarity_score: Mapped[float] = mapped_column("similarityScore")
    body_similarity_score: Mapped[Optional[float]] = mapped_column("bodySimilarityScore")
    body_similarity_status: Mapped[Optional[str]] = mapped_column("bodySimilarityStatus")
    similarity_metadata: Mapped[Optional[dict[str, Any]]] = mapped_column("similarityMetadata", JSONB(none_as_null=True))
    sent_text: Mapped[Optional[str]] = mapped_column("sentText")
    reply_memory_sent_text: Mapped[Optional[str]] = mapped_column("replyMemorySentText")
    reply_memory_attempt_count: Mapped[int] = mapped_column("replyMemoryAttemptCount", default=0, server_default=text("0"))
    reply_memory_processed_at: Mapped[Optional[datetime]] = mapped_column("replyMemoryProcessedAt")

    __table_args__ = (
        Index("DraftSendLog_executedActionId_key", "executedActionId", unique=True),
        Index("DraftSendLog_executedActionId_idx", "executedActionId"),
        Index("DraftSendLog_replyMemoryProcessedAt_replyMemoryAttemptCount_idx", "replyMemoryProcessedAt", "replyMemoryAttemptCount", "createdAt"),
    )

    executed_action: Mapped["ExecutedAction"] = relationship(back_populates="draft_send_log")
    reply_memory_sources: Mapped[list["ReplyMemorySource"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="draft_send_log")

class Chat(Base):
    __tablename__ = "Chat"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    deleted_at: Mapped[Optional[datetime]] = mapped_column("deletedAt")
    name: Mapped[Optional[str]]
    compaction_count: Mapped[int] = mapped_column("compactionCount", default=0, server_default=text("0"))
    last_seen_rules_revision: Mapped[Optional[int]] = mapped_column("lastSeenRulesRevision")
    active_stream_id: Mapped[Optional[str]] = mapped_column("activeStreamId")
    active_stream_started_at: Mapped[Optional[datetime]] = mapped_column("activeStreamStartedAt")
    email_account_id: Mapped[str] = mapped_column("emailAccountId", ForeignKey("EmailAccount.id", ondelete="CASCADE", onupdate="CASCADE", name="Chat_emailAccountId_fkey"))

    __table_args__ = (
        Index("Chat_emailAccountId_idx", "emailAccountId"),
        Index("Chat_deletedAt_idx", "deletedAt"),
    )

    email_account: Mapped["EmailAccount"] = relationship(back_populates="chats")
    messages: Mapped[list["ChatMessage"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="chat")
    compactions: Mapped[list["ChatCompaction"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="chat")
    memories: Mapped[list["ChatMemory"]] = relationship(back_populates="chat")
    action_totals: Mapped[list["ChatActionTotal"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="chat")
    bulk_inbox_action_jobs: Mapped[list["BulkInboxActionJob"]] = relationship(back_populates="chat")

class ChatMessage(Base):
    __tablename__ = "ChatMessage"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    role: Mapped[str]
    parts: Mapped[dict[str, Any]] = mapped_column(JSONB)
    metadata_: Mapped[Optional[dict[str, Any]]] = mapped_column("metadata", JSONB(none_as_null=True))
    chat_id: Mapped[str] = mapped_column("chatId", ForeignKey("Chat.id", ondelete="CASCADE", onupdate="CASCADE", name="ChatMessage_chatId_fkey"))

    __table_args__ = (
        Index("ChatMessage_chatId_idx", "chatId"),
    )

    chat: Mapped["Chat"] = relationship(back_populates="messages")

class ChatCompaction(Base):
    __tablename__ = "ChatCompaction"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    summary: Mapped[str]
    message_count: Mapped[int] = mapped_column("messageCount")
    compacted_before_created_at: Mapped[datetime] = mapped_column("compactedBeforeCreatedAt")
    chat_id: Mapped[str] = mapped_column("chatId", ForeignKey("Chat.id", ondelete="CASCADE", onupdate="CASCADE", name="ChatCompaction_chatId_fkey"))

    __table_args__ = (
        Index("ChatCompaction_chatId_idx", "chatId"),
    )

    chat: Mapped["Chat"] = relationship(back_populates="compactions")

class ChatActionTotal(Base):
    __tablename__ = "ChatActionTotal"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    action: Mapped[str]
    count: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    chat_id: Mapped[str] = mapped_column("chatId", ForeignKey("Chat.id", ondelete="CASCADE", onupdate="CASCADE", name="ChatActionTotal_chatId_fkey"))

    __table_args__ = (
        Index("ChatActionTotal_chatId_action_key", "chatId", "action", unique=True),
    )

    chat: Mapped["Chat"] = relationship(back_populates="action_totals")

class ChatMemory(Base):
    __tablename__ = "ChatMemory"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    content: Mapped[str]
    chat_id: Mapped[Optional[str]] = mapped_column("chatId", ForeignKey("Chat.id", ondelete="SET NULL", onupdate="CASCADE", name="ChatMemory_chatId_fkey"))
    email_account_id: Mapped[str] = mapped_column("emailAccountId", ForeignKey("EmailAccount.id", ondelete="CASCADE", onupdate="CASCADE", name="ChatMemory_emailAccountId_fkey"))

    __table_args__ = (
        Index("ChatMemory_emailAccountId_idx", "emailAccountId"),
    )

    chat: Mapped[Optional["Chat"]] = relationship(back_populates="memories")
    email_account: Mapped["EmailAccount"] = relationship(back_populates="chat_memories")

class CalendarConnection(Base):
    __tablename__ = "CalendarConnection"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    provider: Mapped[str]
    email: Mapped[str]
    access_token: Mapped[Optional[str]] = mapped_column("accessToken")
    refresh_token: Mapped[Optional[str]] = mapped_column("refreshToken")
    expires_at: Mapped[Optional[datetime]] = mapped_column("expiresAt")
    is_connected: Mapped[bool] = mapped_column("isConnected", default=True, server_default=text("true"))
    email_account_id: Mapped[str] = mapped_column("emailAccountId", ForeignKey("EmailAccount.id", ondelete="CASCADE", onupdate="CASCADE", name="CalendarConnection_emailAccountId_fkey"))

    __table_args__ = (
        Index("CalendarConnection_emailAccountId_provider_email_key", "emailAccountId", "provider", "email", unique=True),
    )

    email_account: Mapped["EmailAccount"] = relationship(back_populates="calendar_connections")
    calendars: Mapped[list["Calendar"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="connection")

class Calendar(Base):
    __tablename__ = "Calendar"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    calendar_id: Mapped[str] = mapped_column("calendarId")
    name: Mapped[str]
    description: Mapped[Optional[str]]
    primary: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    is_enabled: Mapped[bool] = mapped_column("isEnabled", default=True, server_default=text("true"))
    timezone_: Mapped[Optional[str]] = mapped_column("timezone")
    connection_id: Mapped[str] = mapped_column("connectionId", ForeignKey("CalendarConnection.id", ondelete="CASCADE", onupdate="CASCADE", name="Calendar_connectionId_fkey"))

    __table_args__ = (
        Index("Calendar_connectionId_calendarId_key", "connectionId", "calendarId", unique=True),
    )

    connection: Mapped["CalendarConnection"] = relationship(back_populates="calendars")
    booking_links: Mapped[list["BookingLink"]] = relationship(back_populates="destination_calendar")

class BookingLink(Base):
    __tablename__ = "BookingLink"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    slug: Mapped[str]
    title: Mapped[str]
    description: Mapped[Optional[str]]
    is_active: Mapped[bool] = mapped_column("isActive", default=True, server_default=text("true"))
    duration_minutes: Mapped[int] = mapped_column("durationMinutes")
    slot_interval_minutes: Mapped[int] = mapped_column("slotIntervalMinutes")
    location_type: Mapped[str] = mapped_column("locationType", Enum("IN_PERSON", "GOOGLE_MEET", "MICROSOFT_TEAMS", "PHONE", "CUSTOM", name="BookingLinkLocationType"), default="CUSTOM", server_default=text("'CUSTOM'"))
    location_value: Mapped[Optional[str]] = mapped_column("locationValue")
    minimum_notice_minutes: Mapped[int] = mapped_column("minimumNoticeMinutes", default=120, server_default=text("120"))
    max_days_ahead: Mapped[int] = mapped_column("maxDaysAhead", default=90, server_default=text("90"))
    email_account_id: Mapped[str] = mapped_column("emailAccountId", ForeignKey("EmailAccount.id", ondelete="CASCADE", onupdate="CASCADE", name="BookingLink_emailAccountId_fkey"))
    destination_calendar_id: Mapped[Optional[str]] = mapped_column("destinationCalendarId", ForeignKey("Calendar.id", ondelete="SET NULL", onupdate="CASCADE", name="BookingLink_destinationCalendarId_fkey"))
    availability_schedule_id: Mapped[str] = mapped_column("availabilityScheduleId")

    __table_args__ = (
        Index("BookingLink_slug_key", "slug", unique=True),
        Index("BookingLink_emailAccountId_key", "emailAccountId", unique=True),
        Index("BookingLink_destinationCalendarId_idx", "destinationCalendarId"),
        Index("BookingLink_availabilityScheduleId_idx", "availabilityScheduleId"),
        ForeignKeyConstraint(["availabilityScheduleId", "emailAccountId"], ["AvailabilitySchedule.id", "AvailabilitySchedule.emailAccountId"], ondelete="NO ACTION", onupdate="CASCADE", name="BookingLink_availabilityScheduleId_emailAccountId_fkey"),
    )

    email_account: Mapped["EmailAccount"] = relationship(overlaps="availability_schedule,booking_links", back_populates="booking_links")
    destination_calendar: Mapped[Optional["Calendar"]] = relationship(back_populates="booking_links")
    availability_schedule: Mapped["AvailabilitySchedule"] = relationship(overlaps="booking_links,email_account", back_populates="booking_links")
    bookings: Mapped[list["Booking"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="booking_link")

class AvailabilitySchedule(Base):
    __tablename__ = "AvailabilitySchedule"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    name: Mapped[str]
    is_default: Mapped[bool] = mapped_column("isDefault", default=False, server_default=text("false"))
    timezone_: Mapped[str] = mapped_column("timezone", default="UTC", server_default=text("'UTC'"))
    email_account_id: Mapped[str] = mapped_column("emailAccountId", ForeignKey("EmailAccount.id", ondelete="CASCADE", onupdate="CASCADE", name="AvailabilitySchedule_emailAccountId_fkey"))

    __table_args__ = (
        Index("AvailabilitySchedule_emailAccountId_idx", "emailAccountId"),
        Index("AvailabilitySchedule_emailAccountId_isDefault_idx", "emailAccountId", "isDefault"),
        Index("AvailabilitySchedule_id_emailAccountId_key", "id", "emailAccountId", unique=True),
    )

    email_account: Mapped["EmailAccount"] = relationship(back_populates="availability_schedules")
    windows: Mapped[list["AvailabilityWindow"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="availability_schedule")
    booking_links: Mapped[list["BookingLink"]] = relationship(passive_deletes="all", overlaps="booking_links,email_account", back_populates="availability_schedule")

class AvailabilityWindow(Base):
    __tablename__ = "AvailabilityWindow"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    weekday: Mapped[int]
    start_minutes: Mapped[int] = mapped_column("startMinutes")
    end_minutes: Mapped[int] = mapped_column("endMinutes")
    availability_schedule_id: Mapped[str] = mapped_column("availabilityScheduleId", ForeignKey("AvailabilitySchedule.id", ondelete="CASCADE", onupdate="CASCADE", name="AvailabilityWindow_availabilityScheduleId_fkey"))

    __table_args__ = (
        Index("AvailabilityWindow_availabilityScheduleId_idx", "availabilityScheduleId"),
    )

    availability_schedule: Mapped["AvailabilitySchedule"] = relationship(back_populates="windows")

class Booking(Base):
    __tablename__ = "Booking"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    guest_name: Mapped[str] = mapped_column("guestName")
    guest_email: Mapped[str] = mapped_column("guestEmail")
    guest_note: Mapped[Optional[str]] = mapped_column("guestNote")
    start_time: Mapped[datetime] = mapped_column("startTime")
    end_time: Mapped[datetime] = mapped_column("endTime")
    status: Mapped[str] = mapped_column(Enum("PENDING_PROVIDER_EVENT", "CONFIRMED", "CANCELED", "FAILED", name="BookingStatus"), default="PENDING_PROVIDER_EVENT", server_default=text("'PENDING_PROVIDER_EVENT'"))
    provider: Mapped[Optional[str]]
    provider_connection_id: Mapped[Optional[str]] = mapped_column("providerConnectionId")
    provider_calendar_id: Mapped[Optional[str]] = mapped_column("providerCalendarId")
    provider_event_id: Mapped[Optional[str]] = mapped_column("providerEventId")
    video_conference_link: Mapped[Optional[str]] = mapped_column("videoConferenceLink")
    cancel_token_hash: Mapped[str] = mapped_column("cancelTokenHash")
    cancellation_reason: Mapped[Optional[str]] = mapped_column("cancellationReason")
    idempotency_token: Mapped[Optional[str]] = mapped_column("idempotencyToken")
    booking_link_id: Mapped[str] = mapped_column("bookingLinkId", ForeignKey("BookingLink.id", ondelete="CASCADE", onupdate="CASCADE", name="Booking_bookingLinkId_fkey"))
    email_account_id: Mapped[str] = mapped_column("emailAccountId", ForeignKey("EmailAccount.id", ondelete="CASCADE", onupdate="CASCADE", name="Booking_emailAccountId_fkey"))

    __table_args__ = (
        Index("Booking_bookingLinkId_startTime_endTime_idx", "bookingLinkId", "startTime", "endTime"),
        Index("Booking_emailAccountId_idx", "emailAccountId"),
        Index("Booking_bookingLinkId_idempotencyToken_key", "bookingLinkId", "idempotencyToken", unique=True),
    )

    booking_link: Mapped["BookingLink"] = relationship(back_populates="bookings")
    email_account: Mapped["EmailAccount"] = relationship(back_populates="bookings")

class MeetingBriefing(Base):
    __tablename__ = "MeetingBriefing"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    calendar_event_id: Mapped[str] = mapped_column("calendarEventId")
    event_title: Mapped[str] = mapped_column("eventTitle")
    event_start_time: Mapped[datetime] = mapped_column("eventStartTime")
    guest_count: Mapped[int] = mapped_column("guestCount")
    status: Mapped[str] = mapped_column(Enum("PENDING", "SENT", "FAILED", "SKIPPED", name="MeetingBriefingStatus"))
    email_account_id: Mapped[str] = mapped_column("emailAccountId", ForeignKey("EmailAccount.id", ondelete="CASCADE", onupdate="CASCADE", name="MeetingBriefing_emailAccountId_fkey"))

    __table_args__ = (
        Index("MeetingBriefing_emailAccountId_idx", "emailAccountId"),
        Index("MeetingBriefing_emailAccountId_calendarEventId_key", "emailAccountId", "calendarEventId", unique=True),
    )

    email_account: Mapped["EmailAccount"] = relationship(back_populates="meeting_briefings")

class ContactResearch(Base):
    __tablename__ = "ContactResearch"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    email: Mapped[str]
    research_started_at: Mapped[datetime] = mapped_column("researchStartedAt")
    found: Mapped[bool]
    role: Mapped[Optional[str]]
    confidence: Mapped[Optional[str]]
    company: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    sources: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))

    __table_args__ = (
        Index("ContactResearch_latest_idx", "email", "researchStartedAt", "createdAt", "id"),
    )

class MeetingRecording(Base):
    __tablename__ = "MeetingRecording"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    bot_provider: Mapped[str] = mapped_column("botProvider", default="recall", server_default=text("'recall'"))
    external_bot_id: Mapped[Optional[str]] = mapped_column("externalBotId")
    bot_name: Mapped[Optional[str]] = mapped_column("botName")
    external_recording_id: Mapped[Optional[str]] = mapped_column("externalRecordingId")
    transcript_requested_at: Mapped[Optional[datetime]] = mapped_column("transcriptRequestedAt")
    external_transcript_id: Mapped[Optional[str]] = mapped_column("externalTranscriptId")
    meeting_url: Mapped[str] = mapped_column("meetingUrl")
    normalized_meeting_url: Mapped[str] = mapped_column("normalizedMeetingUrl")
    active_key: Mapped[Optional[str]] = mapped_column("activeKey")
    meeting_start_time: Mapped[datetime] = mapped_column("meetingStartTime")
    status: Mapped[str] = mapped_column(Enum("PENDING", "SCHEDULED", "CANCELLING", "JOINING", "IN_WAITING_ROOM", "IN_CALL", "RECORDING", "CALL_ENDED", "DONE", "FAILED", "CANCELLED", name="MeetingRecordingStatus"), default="PENDING", server_default=text("'PENDING'"))
    failure_reason: Mapped[Optional[str]] = mapped_column("failureReason")
    transcript: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    transcript_fetched_at: Mapped[Optional[datetime]] = mapped_column("transcriptFetchedAt")
    media_deleted_at: Mapped[Optional[datetime]] = mapped_column("mediaDeletedAt")
    email_account_id: Mapped[str] = mapped_column("emailAccountId", ForeignKey("EmailAccount.id", ondelete="CASCADE", onupdate="CASCADE", name="MeetingRecording_emailAccountId_fkey"))

    __table_args__ = (
        Index("MeetingRecording_normalizedMeetingUrl_meetingStartTime_idx", "normalizedMeetingUrl", "meetingStartTime"),
        Index("MeetingRecording_status_meetingStartTime_idx", "status", "meetingStartTime"),
        Index("MeetingRecording_status_mediaDeletedAt_idx", "status", "mediaDeletedAt"),
        Index("MeetingRecording_emailAccountId_idx", "emailAccountId"),
        Index("MeetingRecording_botProvider_externalBotId_key", "botProvider", "externalBotId", unique=True),
        Index("MeetingRecording_activeKey_meetingStartTime_key", "activeKey", "meetingStartTime", unique=True),
    )

    email_account: Mapped["EmailAccount"] = relationship(back_populates="meeting_recordings")
    meetings: Mapped[list["Meeting"]] = relationship(back_populates="recording")

class Meeting(Base):
    __tablename__ = "Meeting"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    calendar_event_id: Mapped[str] = mapped_column("calendarEventId")
    event_title: Mapped[str] = mapped_column("eventTitle")
    start_time: Mapped[datetime] = mapped_column("startTime")
    end_time: Mapped[datetime] = mapped_column("endTime")
    attendees: Mapped[dict[str, Any]] = mapped_column(JSONB)
    organizer_email: Mapped[Optional[str]] = mapped_column("organizerEmail")
    join_override: Mapped[Optional[bool]] = mapped_column("joinOverride")
    processing_status: Mapped[str] = mapped_column("processingStatus", Enum("PENDING", "PROCESSING", "COMPLETED", "FAILED", name="MeetingProcessingStatus"), default="PENDING", server_default=text("'PENDING'"))
    processing_attempts: Mapped[int] = mapped_column("processingAttempts", default=0, server_default=text("0"))
    summary: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    follow_up_draft_started_at: Mapped[Optional[datetime]] = mapped_column("followUpDraftStartedAt")
    follow_up_draft_id: Mapped[Optional[str]] = mapped_column("followUpDraftId")
    recap_sent_at: Mapped[Optional[datetime]] = mapped_column("recapSentAt")
    recording_id: Mapped[Optional[str]] = mapped_column("recordingId", ForeignKey("MeetingRecording.id", ondelete="SET NULL", onupdate="CASCADE", name="Meeting_recordingId_fkey"))
    email_account_id: Mapped[str] = mapped_column("emailAccountId", ForeignKey("EmailAccount.id", ondelete="CASCADE", onupdate="CASCADE", name="Meeting_emailAccountId_fkey"))

    __table_args__ = (
        Index("Meeting_emailAccountId_startTime_idx", "emailAccountId", "startTime"),
        Index("Meeting_recordingId_idx", "recordingId"),
        Index("Meeting_processingStatus_updatedAt_idx", "processingStatus", "updatedAt"),
        Index("Meeting_emailAccountId_calendarEventId_key", "emailAccountId", "calendarEventId", unique=True),
        Index("Meeting_emailAccountId_recordingId_key", "emailAccountId", "recordingId", unique=True),
    )

    recording: Mapped[Optional["MeetingRecording"]] = relationship(back_populates="meetings")
    email_account: Mapped["EmailAccount"] = relationship(back_populates="meetings")

class MessagingChannel(Base):
    __tablename__ = "MessagingChannel"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    provider: Mapped[str] = mapped_column(Enum("SLACK", "TEAMS", "TELEGRAM", name="MessagingProvider"))
    is_connected: Mapped[bool] = mapped_column("isConnected", default=True, server_default=text("true"))
    team_id: Mapped[str] = mapped_column("teamId")
    team_name: Mapped[Optional[str]] = mapped_column("teamName")
    provider_user_id: Mapped[Optional[str]] = mapped_column("providerUserId")
    bot_user_id: Mapped[Optional[str]] = mapped_column("botUserId")
    access_token: Mapped[Optional[str]] = mapped_column("accessToken")
    refresh_token: Mapped[Optional[str]] = mapped_column("refreshToken")
    expires_at: Mapped[Optional[datetime]] = mapped_column("expiresAt")
    email_account_id: Mapped[str] = mapped_column("emailAccountId", ForeignKey("EmailAccount.id", ondelete="CASCADE", onupdate="CASCADE", name="MessagingChannel_emailAccountId_fkey"))

    __table_args__ = (
        Index("MessagingChannel_emailAccountId_idx", "emailAccountId"),
        Index("MessagingChannel_provider_teamId_idx", "provider", "teamId"),
        Index("MessagingChannel_emailAccountId_provider_teamId_key", "emailAccountId", "provider", "teamId", unique=True),
        Index("MessagingChannel_id_emailAccountId_key", "id", "emailAccountId", unique=True),
    )

    email_account: Mapped["EmailAccount"] = relationship(back_populates="messaging_channels")
    routes: Mapped[list["MessagingRoute"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="messaging_channel")
    automation_jobs: Mapped[list["AutomationJob"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="messaging_channel")
    actions: Mapped[list["Action"]] = relationship(back_populates="messaging_channel")
    executed_actions: Mapped[list["ExecutedAction"]] = relationship(back_populates="messaging_channel")

class MessagingRoute(Base):
    __tablename__ = "MessagingRoute"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    purpose: Mapped[str] = mapped_column(Enum("RULE_NOTIFICATIONS", "SCHEDULED_CHECK_INS", "MEETING_BRIEFS", "DOCUMENT_FILINGS", "DIGESTS", "FOLLOW_UPS", name="MessagingRoutePurpose"))
    target_type: Mapped[str] = mapped_column("targetType", Enum("CHANNEL", "DIRECT_MESSAGE", name="MessagingRouteTargetType"))
    target_id: Mapped[str] = mapped_column("targetId")
    messaging_channel_id: Mapped[str] = mapped_column("messagingChannelId", ForeignKey("MessagingChannel.id", ondelete="CASCADE", onupdate="CASCADE", name="MessagingRoute_messagingChannelId_fkey"))

    __table_args__ = (
        Index("MessagingRoute_messagingChannelId_purpose_key", "messagingChannelId", "purpose", unique=True),
    )

    messaging_channel: Mapped["MessagingChannel"] = relationship(back_populates="routes")

class AutomationJob(Base):
    __tablename__ = "AutomationJob"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    name: Mapped[str]
    enabled: Mapped[bool] = mapped_column(default=True, server_default=text("true"))
    prompt: Mapped[Optional[str]]
    cron_expression: Mapped[str] = mapped_column("cronExpression")
    next_run_at: Mapped[datetime] = mapped_column("nextRunAt")
    messaging_channel_id: Mapped[str] = mapped_column("messagingChannelId")
    email_account_id: Mapped[str] = mapped_column("emailAccountId")

    __table_args__ = (
        Index("AutomationJob_emailAccountId_enabled_nextRunAt_idx", "emailAccountId", "enabled", "nextRunAt"),
        Index("AutomationJob_enabled_nextRunAt_idx", "enabled", "nextRunAt"),
        Index("AutomationJob_messagingChannelId_emailAccountId_idx", "messagingChannelId", "emailAccountId"),
        Index("AutomationJob_emailAccountId_key", "emailAccountId", unique=True),
        ForeignKeyConstraint(["messagingChannelId", "emailAccountId"], ["MessagingChannel.id", "MessagingChannel.emailAccountId"], ondelete="CASCADE", onupdate="CASCADE", name="AutomationJob_messagingChannelId_emailAccountId_fkey"),
    )

    messaging_channel: Mapped["MessagingChannel"] = relationship(back_populates="automation_jobs")
    runs: Mapped[list["AutomationJobRun"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="automation_job")

class AutomationJobRun(Base):
    __tablename__ = "AutomationJobRun"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    status: Mapped[str] = mapped_column(Enum("PENDING", "RUNNING", "SENT", "SKIPPED", "FAILED", name="AutomationJobRunStatus"), default="PENDING", server_default=text("'PENDING'"))
    scheduled_for: Mapped[datetime] = mapped_column("scheduledFor")
    processed_at: Mapped[Optional[datetime]] = mapped_column("processedAt")
    outbound_message: Mapped[Optional[str]] = mapped_column("outboundMessage")
    provider_message_id: Mapped[Optional[str]] = mapped_column("providerMessageId")
    error: Mapped[Optional[str]]
    automation_job_id: Mapped[str] = mapped_column("automationJobId", ForeignKey("AutomationJob.id", ondelete="CASCADE", onupdate="CASCADE", name="AutomationJobRun_automationJobId_fkey"))

    __table_args__ = (
        Index("AutomationJobRun_automationJobId_createdAt_idx", "automationJobId", "createdAt"),
        Index("AutomationJobRun_status_createdAt_idx", "status", "createdAt"),
        Index("AutomationJobRun_automationJobId_scheduledFor_key", "automationJobId", "scheduledFor", unique=True),
    )

    automation_job: Mapped["AutomationJob"] = relationship(back_populates="runs")

class DriveConnection(Base):
    __tablename__ = "DriveConnection"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    provider: Mapped[str]
    email: Mapped[str]
    access_token: Mapped[Optional[str]] = mapped_column("accessToken")
    refresh_token: Mapped[Optional[str]] = mapped_column("refreshToken")
    expires_at: Mapped[Optional[datetime]] = mapped_column("expiresAt")
    is_connected: Mapped[bool] = mapped_column("isConnected", default=True, server_default=text("true"))
    email_account_id: Mapped[str] = mapped_column("emailAccountId", ForeignKey("EmailAccount.id", ondelete="CASCADE", onupdate="CASCADE", name="DriveConnection_emailAccountId_fkey"))

    __table_args__ = (
        Index("DriveConnection_emailAccountId_idx", "emailAccountId"),
        Index("DriveConnection_emailAccountId_provider_key", "emailAccountId", "provider", unique=True),
    )

    email_account: Mapped["EmailAccount"] = relationship(back_populates="drive_connections")
    document_filings: Mapped[list["DocumentFiling"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="drive_connection")
    filing_folders: Mapped[list["FilingFolder"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="drive_connection")
    attachment_sources: Mapped[list["AttachmentSource"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="drive_connection")

class FilingFolder(Base):
    __tablename__ = "FilingFolder"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    folder_id: Mapped[str] = mapped_column("folderId")
    folder_name: Mapped[str] = mapped_column("folderName")
    folder_path: Mapped[str] = mapped_column("folderPath")
    drive_connection_id: Mapped[str] = mapped_column("driveConnectionId", ForeignKey("DriveConnection.id", ondelete="CASCADE", onupdate="CASCADE", name="FilingFolder_driveConnectionId_fkey"))
    email_account_id: Mapped[str] = mapped_column("emailAccountId", ForeignKey("EmailAccount.id", ondelete="CASCADE", onupdate="CASCADE", name="FilingFolder_emailAccountId_fkey"))

    __table_args__ = (
        Index("FilingFolder_driveConnectionId_idx", "driveConnectionId"),
        Index("FilingFolder_emailAccountId_folderId_key", "emailAccountId", "folderId", unique=True),
    )

    drive_connection: Mapped["DriveConnection"] = relationship(back_populates="filing_folders")
    email_account: Mapped["EmailAccount"] = relationship(back_populates="filing_folders")

class DocumentFiling(Base):
    __tablename__ = "DocumentFiling"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    message_id: Mapped[str] = mapped_column("messageId")
    attachment_id: Mapped[str] = mapped_column("attachmentId")
    filename: Mapped[str]
    folder_id: Mapped[Optional[str]] = mapped_column("folderId")
    folder_path: Mapped[str] = mapped_column("folderPath")
    file_id: Mapped[Optional[str]] = mapped_column("fileId")
    reasoning: Mapped[Optional[str]]
    confidence: Mapped[Optional[float]]
    status: Mapped[str] = mapped_column(Enum("PROCESSING", "PENDING", "FILED", "REJECTED", "ERROR", "PREVIEW", name="DocumentFilingStatus"), default="FILED", server_default=text("'FILED'"))
    was_asked: Mapped[bool] = mapped_column("wasAsked", default=False, server_default=text("false"))
    was_corrected: Mapped[bool] = mapped_column("wasCorrected", default=False, server_default=text("false"))
    original_path: Mapped[Optional[str]] = mapped_column("originalPath")
    corrected_at: Mapped[Optional[datetime]] = mapped_column("correctedAt")
    feedback_positive: Mapped[Optional[bool]] = mapped_column("feedbackPositive")
    feedback_at: Mapped[Optional[datetime]] = mapped_column("feedbackAt")
    notification_message_id: Mapped[Optional[str]] = mapped_column("notificationMessageId")
    notification_sent_at: Mapped[Optional[datetime]] = mapped_column("notificationSentAt")
    notification_batch_id: Mapped[Optional[str]] = mapped_column("notificationBatchId")
    drive_connection_id: Mapped[str] = mapped_column("driveConnectionId", ForeignKey("DriveConnection.id", ondelete="CASCADE", onupdate="CASCADE", name="DocumentFiling_driveConnectionId_fkey"))
    email_account_id: Mapped[str] = mapped_column("emailAccountId", ForeignKey("EmailAccount.id", ondelete="CASCADE", onupdate="CASCADE", name="DocumentFiling_emailAccountId_fkey"))

    __table_args__ = (
        Index("DocumentFiling_notificationMessageId_key", "notificationMessageId", unique=True),
        Index("DocumentFiling_emailAccountId_status_idx", "emailAccountId", "status"),
        Index("DocumentFiling_emailAccountId_notificationBatchId_idx", "emailAccountId", "notificationBatchId"),
        Index("DocumentFiling_driveConnectionId_idx", "driveConnectionId"),
        Index("DocumentFiling_messageId_idx", "messageId"),
        Index("DocumentFiling_emailAccountId_messageId_attachmentId_key", "emailAccountId", "messageId", "attachmentId", unique=True),
    )

    drive_connection: Mapped["DriveConnection"] = relationship(back_populates="document_filings")
    email_account: Mapped["EmailAccount"] = relationship(back_populates="document_filings")

class AttachmentSource(Base):
    __tablename__ = "AttachmentSource"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    name: Mapped[str]
    type: Mapped[str] = mapped_column(Enum("FILE", "FOLDER", name="AttachmentSourceType"))
    source_id: Mapped[str] = mapped_column("sourceId")
    source_path: Mapped[Optional[str]] = mapped_column("sourcePath")
    rule_id: Mapped[str] = mapped_column("ruleId", ForeignKey("Rule.id", ondelete="CASCADE", onupdate="CASCADE", name="AttachmentSource_ruleId_fkey"))
    drive_connection_id: Mapped[str] = mapped_column("driveConnectionId", ForeignKey("DriveConnection.id", ondelete="CASCADE", onupdate="CASCADE", name="AttachmentSource_driveConnectionId_fkey"))

    __table_args__ = (
        Index("AttachmentSource_ruleId_idx", "ruleId"),
        Index("AttachmentSource_driveConnectionId_idx", "driveConnectionId"),
        Index("AttachmentSource_ruleId_driveConnectionId_type_sourceId_key", "ruleId", "driveConnectionId", "type", "sourceId", unique=True),
    )

    rule: Mapped["Rule"] = relationship(back_populates="attachment_sources")
    drive_connection: Mapped["DriveConnection"] = relationship(back_populates="attachment_sources")
    documents: Mapped[list["AttachmentDocument"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="attachment_source")

class AttachmentDocument(Base):
    __tablename__ = "AttachmentDocument"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    attachment_source_id: Mapped[str] = mapped_column("attachmentSourceId", ForeignKey("AttachmentSource.id", ondelete="CASCADE", onupdate="CASCADE", name="AttachmentDocument_attachmentSourceId_fkey"))
    file_id: Mapped[str] = mapped_column("fileId")
    name: Mapped[str]
    mime_type: Mapped[str] = mapped_column("mimeType")
    modified_at: Mapped[Optional[datetime]] = mapped_column("modifiedAt")
    summary: Mapped[Optional[str]]
    content: Mapped[Optional[str]]
    metadata_: Mapped[Optional[dict[str, Any]]] = mapped_column("metadata", JSONB(none_as_null=True))
    indexed_at: Mapped[Optional[datetime]] = mapped_column("indexedAt")
    error: Mapped[Optional[str]]

    __table_args__ = (
        Index("AttachmentDocument_attachmentSourceId_modifiedAt_idx", "attachmentSourceId", "modifiedAt"),
        Index("AttachmentDocument_attachmentSourceId_fileId_key", "attachmentSourceId", "fileId", unique=True),
    )

    attachment_source: Mapped["AttachmentSource"] = relationship(back_populates="documents")

class Referral(Base):
    __tablename__ = "Referral"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    referrer_user_id: Mapped[str] = mapped_column("referrerUserId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="Referral_referrerUserId_fkey"))
    referred_user_id: Mapped[str] = mapped_column("referredUserId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="Referral_referredUserId_fkey"))
    referral_code_used: Mapped[str] = mapped_column("referralCodeUsed")
    status: Mapped[str] = mapped_column(Enum("PENDING", "COMPLETED", name="ReferralStatus"), default="PENDING", server_default=text("'PENDING'"))
    reward_granted_at: Mapped[Optional[datetime]] = mapped_column("rewardGrantedAt")
    stripe_balance_transaction_id: Mapped[Optional[str]] = mapped_column("stripeBalanceTransactionId")
    reward_amount: Mapped[Optional[int]] = mapped_column("rewardAmount")

    __table_args__ = (
        Index("Referral_referredUserId_key", "referredUserId", unique=True),
        Index("Referral_referrerUserId_idx", "referrerUserId"),
        Index("Referral_status_idx", "status"),
    )

    referrer_user: Mapped["User"] = relationship(foreign_keys=[referrer_user_id], back_populates="referrals_made")
    referred_user: Mapped["User"] = relationship(foreign_keys=[referred_user_id], back_populates="referral_received")

class McpIntegration(Base):
    __tablename__ = "McpIntegration"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    name: Mapped[str]
    registered_server_url: Mapped[Optional[str]] = mapped_column("registeredServerUrl")
    registered_authorization_url: Mapped[Optional[str]] = mapped_column("registeredAuthorizationUrl")
    registered_token_url: Mapped[Optional[str]] = mapped_column("registeredTokenUrl")
    oauth_client_id: Mapped[Optional[str]] = mapped_column("oauthClientId")
    oauth_client_secret: Mapped[Optional[str]] = mapped_column("oauthClientSecret")
    display_name: Mapped[Optional[str]] = mapped_column("displayName")
    server_url: Mapped[Optional[str]] = mapped_column("serverUrl")
    auth_type: Mapped[Optional[str]] = mapped_column("authType", Enum("OAUTH", "API_TOKEN", "NONE", name="McpAuthType"))
    email_account_id: Mapped[Optional[str]] = mapped_column("emailAccountId", ForeignKey("EmailAccount.id", ondelete="CASCADE", onupdate="CASCADE", name="McpIntegration_emailAccountId_fkey"))

    __table_args__ = (
        Index("McpIntegration_name_key", "name", unique=True),
        Index("McpIntegration_emailAccountId_idx", "emailAccountId"),
    )

    email_account: Mapped[Optional["EmailAccount"]] = relationship(back_populates="mcp_integrations")
    connections: Mapped[list["McpConnection"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="integration")

class McpConnection(Base):
    __tablename__ = "McpConnection"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    name: Mapped[str]
    is_active: Mapped[bool] = mapped_column("isActive", default=True, server_default=text("true"))
    access_token: Mapped[Optional[str]] = mapped_column("accessToken")
    refresh_token: Mapped[Optional[str]] = mapped_column("refreshToken")
    api_key: Mapped[Optional[str]] = mapped_column("apiKey")
    expires_at: Mapped[Optional[datetime]] = mapped_column("expiresAt")
    integration_id: Mapped[str] = mapped_column("integrationId", ForeignKey("McpIntegration.id", ondelete="CASCADE", onupdate="CASCADE", name="McpConnection_integrationId_fkey"))
    email_account_id: Mapped[Optional[str]] = mapped_column("emailAccountId", ForeignKey("EmailAccount.id", ondelete="CASCADE", onupdate="CASCADE", name="McpConnection_emailAccountId_fkey"))

    __table_args__ = (
        Index("McpConnection_emailAccountId_integrationId_key", "emailAccountId", "integrationId", unique=True),
    )

    integration: Mapped["McpIntegration"] = relationship(back_populates="connections")
    email_account: Mapped[Optional["EmailAccount"]] = relationship(back_populates="mcp_connections")
    tools: Mapped[list["McpTool"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="connection")

class McpTool(Base):
    __tablename__ = "McpTool"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    name: Mapped[str]
    description: Mapped[Optional[str]]
    schema: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    is_enabled: Mapped[bool] = mapped_column("isEnabled", default=True, server_default=text("true"))
    is_write: Mapped[bool] = mapped_column("isWrite", default=False, server_default=text("false"))
    connection_id: Mapped[str] = mapped_column("connectionId", ForeignKey("McpConnection.id", ondelete="CASCADE", onupdate="CASCADE", name="McpTool_connectionId_fkey"))

    __table_args__ = (
        Index("McpTool_connectionId_name_key", "connectionId", "name", unique=True),
    )

    connection: Mapped["McpConnection"] = relationship(back_populates="tools")

class MobilePushToken(Base):
    __tablename__ = "MobilePushToken"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    token: Mapped[str]
    platform: Mapped[str] = mapped_column(Enum("android", "ios", name="MobilePushPlatform"))
    token_type: Mapped[str] = mapped_column("tokenType", Enum("expo", "apns", "fcm", name="MobilePushTokenType"), default="expo", server_default=text("'expo'"))
    app_version: Mapped[Optional[str]] = mapped_column("appVersion")
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="MobilePushToken_userId_fkey"))

    __table_args__ = (
        Index("MobilePushToken_token_key", "token", unique=True),
        Index("MobilePushToken_userId_idx", "userId"),
    )

    user: Mapped["User"] = relationship(back_populates="mobile_push_tokens")
    mobile_push_deliveries: Mapped[list["MobilePushDelivery"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="mobile_push_token")

class MobilePushDelivery(Base):
    __tablename__ = "MobilePushDelivery"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    deduplication_key: Mapped[str] = mapped_column("deduplicationKey")
    mobile_push_token_id: Mapped[str] = mapped_column("mobilePushTokenId", ForeignKey("MobilePushToken.id", ondelete="CASCADE", onupdate="CASCADE", name="MobilePushDelivery_mobilePushTokenId_fkey"))

    __table_args__ = (
        Index("MobilePushDelivery_createdAt_idx", "createdAt"),
        Index("MobilePushDelivery_mobilePushTokenId_idx", "mobilePushTokenId"),
        Index("MobilePushDelivery_key_token_key", "deduplicationKey", "mobilePushTokenId", unique=True),
    )

    mobile_push_token: Mapped["MobilePushToken"] = relationship(back_populates="mobile_push_deliveries")

class OauthClient(Base):
    __tablename__ = "OauthClient"

    id: Mapped[str] = mapped_column(primary_key=True)
    client_id: Mapped[str] = mapped_column("clientId")
    client_secret: Mapped[Optional[str]] = mapped_column("clientSecret")
    client_discovery_id: Mapped[Optional[str]] = mapped_column("clientDiscoveryId")
    disabled: Mapped[Optional[bool]] = mapped_column(default=False, server_default=text("false"))
    skip_consent: Mapped[Optional[bool]] = mapped_column("skipConsent")
    enable_end_session: Mapped[Optional[bool]] = mapped_column("enableEndSession")
    subject_type: Mapped[Optional[str]] = mapped_column("subjectType")
    scopes: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True)
    client_credentials_scopes: Mapped[list[str]] = mapped_column("clientCredentialsScopes", ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    user_id: Mapped[Optional[str]] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="OauthClient_userId_fkey"))
    created_at: Mapped[Optional[datetime]] = mapped_column("createdAt")
    updated_at: Mapped[Optional[datetime]] = mapped_column("updatedAt")
    name: Mapped[Optional[str]]
    uri: Mapped[Optional[str]]
    icon: Mapped[Optional[str]]
    contacts: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True)
    tos: Mapped[Optional[str]]
    policy: Mapped[Optional[str]]
    software_id: Mapped[Optional[str]] = mapped_column("softwareId")
    software_version: Mapped[Optional[str]] = mapped_column("softwareVersion")
    software_statement: Mapped[Optional[str]] = mapped_column("softwareStatement")
    redirect_uris: Mapped[list[str]] = mapped_column("redirectUris", ARRAY(Text), nullable=True)
    post_logout_redirect_uris: Mapped[list[str]] = mapped_column("postLogoutRedirectUris", ARRAY(Text), nullable=True)
    backchannel_logout_uri: Mapped[Optional[str]] = mapped_column("backchannelLogoutUri")
    backchannel_logout_session_required: Mapped[Optional[bool]] = mapped_column("backchannelLogoutSessionRequired")
    token_endpoint_auth_method: Mapped[Optional[str]] = mapped_column("tokenEndpointAuthMethod")
    application_type: Mapped[Optional[str]] = mapped_column("applicationType")
    jwks: Mapped[Optional[str]]
    jwks_uri: Mapped[Optional[str]] = mapped_column("jwksUri")
    grant_types: Mapped[list[str]] = mapped_column("grantTypes", ARRAY(Text), nullable=True)
    response_types: Mapped[list[str]] = mapped_column("responseTypes", ARRAY(Text), nullable=True)
    require_pkce: Mapped[Optional[bool]] = mapped_column("requirePKCE")
    dpop_bound_access_tokens: Mapped[Optional[bool]] = mapped_column("dpopBoundAccessTokens", default=False, server_default=text("false"))
    reference_id: Mapped[Optional[str]] = mapped_column("referenceId")
    metadata_: Mapped[Optional[dict[str, Any]]] = mapped_column("metadata", JSONB(none_as_null=True))

    __table_args__ = (
        Index("OauthClient_userId_idx", "userId"),
        Index("OauthClient_clientId_key", "clientId", unique=True),
    )

    user: Mapped[Optional["User"]] = relationship(back_populates="oauth_client_records")
    oauth_client_resources: Mapped[list["OauthClientResource"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="client")
    oauthrefreshtokens: Mapped[list["OauthRefreshToken"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="client")
    oauthaccesstokens: Mapped[list["OauthAccessToken"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="client")
    oauthconsents: Mapped[list["OauthConsent"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="client")

class OauthRefreshToken(Base):
    __tablename__ = "OauthRefreshToken"

    id: Mapped[str] = mapped_column(primary_key=True)
    token: Mapped[str]
    client_id: Mapped[str] = mapped_column("clientId", ForeignKey("OauthClient.clientId", ondelete="CASCADE", onupdate="CASCADE", name="OauthRefreshToken_clientId_fkey"))
    session_id: Mapped[Optional[str]] = mapped_column("sessionId", ForeignKey("Session.id", ondelete="SET NULL", onupdate="CASCADE", name="OauthRefreshToken_sessionId_fkey"))
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="OauthRefreshToken_userId_fkey"))
    reference_id: Mapped[Optional[str]] = mapped_column("referenceId")
    authorization_code_id: Mapped[Optional[str]] = mapped_column("authorizationCodeId")
    resources: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True)
    requested_user_info_claims: Mapped[list[str]] = mapped_column("requestedUserInfoClaims", ARRAY(Text), nullable=True)
    expires_at: Mapped[datetime] = mapped_column("expiresAt")
    created_at: Mapped[datetime] = mapped_column("createdAt")
    revoked: Mapped[Optional[datetime]]
    rotated_at: Mapped[Optional[datetime]] = mapped_column("rotatedAt")
    rotation_replay_response: Mapped[Optional[str]] = mapped_column("rotationReplayResponse")
    rotation_replay_expires_at: Mapped[Optional[datetime]] = mapped_column("rotationReplayExpiresAt")
    auth_time: Mapped[Optional[datetime]] = mapped_column("authTime")
    confirmation: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    scopes: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True)

    __table_args__ = (
        Index("OauthRefreshToken_clientId_idx", "clientId"),
        Index("OauthRefreshToken_sessionId_idx", "sessionId"),
        Index("OauthRefreshToken_userId_idx", "userId"),
        Index("OauthRefreshToken_authorizationCodeId_idx", "authorizationCodeId"),
        Index("OauthRefreshToken_token_key", "token", unique=True),
    )

    client: Mapped["OauthClient"] = relationship(back_populates="oauthrefreshtokens")
    session: Mapped[Optional["Session"]] = relationship(back_populates="oauth_refresh_token_records")
    user: Mapped["User"] = relationship(back_populates="oauth_refresh_token_records")
    oauthaccesstokens: Mapped[list["OauthAccessToken"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="refresh")

class OauthAccessToken(Base):
    __tablename__ = "OauthAccessToken"

    id: Mapped[str] = mapped_column(primary_key=True)
    token: Mapped[str]
    client_id: Mapped[str] = mapped_column("clientId", ForeignKey("OauthClient.clientId", ondelete="CASCADE", onupdate="CASCADE", name="OauthAccessToken_clientId_fkey"))
    session_id: Mapped[Optional[str]] = mapped_column("sessionId", ForeignKey("Session.id", ondelete="SET NULL", onupdate="CASCADE", name="OauthAccessToken_sessionId_fkey"))
    user_id: Mapped[Optional[str]] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="OauthAccessToken_userId_fkey"))
    reference_id: Mapped[Optional[str]] = mapped_column("referenceId")
    authorization_code_id: Mapped[Optional[str]] = mapped_column("authorizationCodeId")
    resources: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True)
    requested_user_info_claims: Mapped[list[str]] = mapped_column("requestedUserInfoClaims", ARRAY(Text), nullable=True)
    refresh_id: Mapped[Optional[str]] = mapped_column("refreshId", ForeignKey("OauthRefreshToken.id", ondelete="CASCADE", onupdate="CASCADE", name="OauthAccessToken_refreshId_fkey"))
    expires_at: Mapped[datetime] = mapped_column("expiresAt")
    created_at: Mapped[datetime] = mapped_column("createdAt")
    revoked: Mapped[Optional[datetime]]
    confirmation: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    scopes: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True)

    __table_args__ = (
        Index("OauthAccessToken_clientId_idx", "clientId"),
        Index("OauthAccessToken_sessionId_idx", "sessionId"),
        Index("OauthAccessToken_userId_idx", "userId"),
        Index("OauthAccessToken_authorizationCodeId_idx", "authorizationCodeId"),
        Index("OauthAccessToken_refreshId_idx", "refreshId"),
        Index("OauthAccessToken_token_key", "token", unique=True),
    )

    client: Mapped["OauthClient"] = relationship(back_populates="oauthaccesstokens")
    session: Mapped[Optional["Session"]] = relationship(back_populates="oauth_access_token_records")
    user: Mapped[Optional["User"]] = relationship(back_populates="oauth_access_token_records")
    refresh: Mapped[Optional["OauthRefreshToken"]] = relationship(back_populates="oauthaccesstokens")

class OauthConsent(Base):
    __tablename__ = "OauthConsent"

    id: Mapped[str] = mapped_column(primary_key=True)
    client_id: Mapped[str] = mapped_column("clientId", ForeignKey("OauthClient.clientId", ondelete="CASCADE", onupdate="CASCADE", name="OauthConsent_clientId_fkey"))
    user_id: Mapped[Optional[str]] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="OauthConsent_userId_fkey"))
    reference_id: Mapped[Optional[str]] = mapped_column("referenceId")
    resources: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True)
    requested_user_info_claims: Mapped[list[str]] = mapped_column("requestedUserInfoClaims", ARRAY(Text), nullable=True)
    scopes: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True)
    created_at: Mapped[datetime] = mapped_column("createdAt")
    updated_at: Mapped[datetime] = mapped_column("updatedAt")

    __table_args__ = (
        Index("OauthConsent_userId_idx", "userId"),
        Index("OauthConsent_clientId_userId_key", "clientId", "userId", unique=True),
    )

    client: Mapped["OauthClient"] = relationship(back_populates="oauthconsents")
    user: Mapped[Optional["User"]] = relationship(back_populates="oauth_consent_records")

class Jwks(Base):
    __tablename__ = "Jwks"

    id: Mapped[str] = mapped_column(primary_key=True)
    public_key: Mapped[str] = mapped_column("publicKey")
    private_key: Mapped[str] = mapped_column("privateKey")
    created_at: Mapped[datetime] = mapped_column("createdAt")
    expires_at: Mapped[Optional[datetime]] = mapped_column("expiresAt")
    alg: Mapped[Optional[str]]
    crv: Mapped[Optional[str]]

class OauthResource(Base):
    __tablename__ = "OauthResource"

    id: Mapped[str] = mapped_column(primary_key=True)
    identifier: Mapped[str]
    name: Mapped[str]
    access_token_ttl: Mapped[Optional[int]] = mapped_column("accessTokenTtl")
    refresh_token_ttl: Mapped[Optional[int]] = mapped_column("refreshTokenTtl")
    signing_algorithm: Mapped[Optional[str]] = mapped_column("signingAlgorithm")
    signing_key_id: Mapped[Optional[str]] = mapped_column("signingKeyId")
    allowed_scopes: Mapped[list[str]] = mapped_column("allowedScopes", ARRAY(Text), nullable=True)
    custom_claims: Mapped[Optional[dict[str, Any]]] = mapped_column("customClaims", JSONB(none_as_null=True))
    dpop_bound_access_tokens_required: Mapped[Optional[bool]] = mapped_column("dpopBoundAccessTokensRequired", default=False, server_default=text("false"))
    disabled: Mapped[Optional[bool]] = mapped_column(default=False, server_default=text("false"))
    created_at: Mapped[Optional[datetime]] = mapped_column("createdAt")
    updated_at: Mapped[Optional[datetime]] = mapped_column("updatedAt")
    policy_version: Mapped[Optional[int]] = mapped_column("policyVersion", default=1, server_default=text("1"))
    metadata_: Mapped[Optional[dict[str, Any]]] = mapped_column("metadata", JSONB(none_as_null=True))

    __table_args__ = (
        Index("OauthResource_identifier_key", "identifier", unique=True),
    )

    oauth_client_resources: Mapped[list["OauthClientResource"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="oauth_resource")

class OauthClientResource(Base):
    __tablename__ = "OauthClientResource"

    id: Mapped[str] = mapped_column(primary_key=True)
    client_id: Mapped[str] = mapped_column("clientId", ForeignKey("OauthClient.clientId", ondelete="CASCADE", onupdate="CASCADE", name="OauthClientResource_clientId_fkey"))
    resource_id: Mapped[str] = mapped_column("resourceId", ForeignKey("OauthResource.identifier", ondelete="CASCADE", onupdate="CASCADE", name="OauthClientResource_resourceId_fkey"))
    metadata_: Mapped[Optional[dict[str, Any]]] = mapped_column("metadata", JSONB(none_as_null=True))
    created_at: Mapped[Optional[datetime]] = mapped_column("createdAt")

    __table_args__ = (
        Index("OauthClientResource_clientId_idx", "clientId"),
        Index("OauthClientResource_resourceId_idx", "resourceId"),
        Index("oauthClientResource_clientId_resourceId_uidx", "clientId", "resourceId", unique=True),
    )

    client: Mapped["OauthClient"] = relationship(back_populates="oauth_client_resources")
    oauth_resource: Mapped["OauthResource"] = relationship(back_populates="oauth_client_resources")

class OauthClientAssertion(Base):
    __tablename__ = "OauthClientAssertion"

    id: Mapped[str] = mapped_column(primary_key=True)
    expires_at: Mapped[datetime] = mapped_column("expiresAt")

class ScimManagedConnection(Base):
    __tablename__ = "scimManagedConnection"

    id: Mapped[str] = mapped_column(primary_key=True)
    creation_request_id: Mapped[str] = mapped_column("creationRequestId")
    connection_id: Mapped[str] = mapped_column("connectionId")
    provisioning_domain_id: Mapped[str] = mapped_column("provisioningDomainId")
    status: Mapped[str]
    revision: Mapped[int]
    created_at: Mapped[datetime] = mapped_column("createdAt")
    created_by: Mapped[str] = mapped_column("createdBy")
    decommission_started_at: Mapped[Optional[datetime]] = mapped_column("decommissionStartedAt")
    decommission_started_by: Mapped[Optional[str]] = mapped_column("decommissionStartedBy")
    decommissioned_at: Mapped[Optional[datetime]] = mapped_column("decommissionedAt")
    decommissioned_by: Mapped[Optional[str]] = mapped_column("decommissionedBy")

    __table_args__ = (
        Index("scimManagedConnection_provisioningDomainId_idx", "provisioningDomainId"),
        Index("scimManagedConnection_creationRequestId_key", "creationRequestId", unique=True),
        Index("scimManagedConnection_connectionId_key", "connectionId", unique=True),
    )

    scimmanagedcredentials: Mapped[list["ScimManagedCredential"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="scimmanagedconnection")
    scimmanagedconnectionevents: Mapped[list["ScimManagedConnectionEvent"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="scimmanagedconnection")

class ScimManagedCredential(Base):
    __tablename__ = "scimManagedCredential"

    id: Mapped[str] = mapped_column(primary_key=True)
    connection_record_id: Mapped[str] = mapped_column("connectionRecordId", ForeignKey("scimManagedConnection.id", ondelete="CASCADE", onupdate="CASCADE", name="scimManagedCredential_connectionRecordId_fkey"))
    credential_id: Mapped[str] = mapped_column("credentialId")
    token_digest: Mapped[str] = mapped_column("tokenDigest")
    hash_version: Mapped[str] = mapped_column("hashVersion")
    active_slot_key: Mapped[str] = mapped_column("activeSlotKey")
    status: Mapped[str]
    serialized_scopes: Mapped[str] = mapped_column("serializedScopes")
    expires_at: Mapped[datetime] = mapped_column("expiresAt")
    created_at: Mapped[datetime] = mapped_column("createdAt")
    created_by: Mapped[str] = mapped_column("createdBy")
    last_used_at: Mapped[Optional[datetime]] = mapped_column("lastUsedAt")
    revoked_at: Mapped[Optional[datetime]] = mapped_column("revokedAt")
    revoked_by: Mapped[Optional[str]] = mapped_column("revokedBy")
    decommissioned_at: Mapped[Optional[datetime]] = mapped_column("decommissionedAt")

    __table_args__ = (
        Index("scimManagedCredential_connectionRecordId_idx", "connectionRecordId"),
        Index("scimManagedCredential_credentialId_key", "credentialId", unique=True),
        Index("scimManagedCredential_activeSlotKey_key", "activeSlotKey", unique=True),
    )

    scimmanagedconnection: Mapped["ScimManagedConnection"] = relationship(back_populates="scimmanagedcredentials")

class ScimManagedConnectionEvent(Base):
    __tablename__ = "scimManagedConnectionEvent"

    id: Mapped[str] = mapped_column(primary_key=True)
    connection_record_id: Mapped[str] = mapped_column("connectionRecordId", ForeignKey("scimManagedConnection.id", ondelete="CASCADE", onupdate="CASCADE", name="scimManagedConnectionEvent_connectionRecordId_fkey"))
    event_key: Mapped[str] = mapped_column("eventKey")
    sequence: Mapped[int]
    type: Mapped[str]
    actor_id: Mapped[str] = mapped_column("actorId")
    credential_id: Mapped[Optional[str]] = mapped_column("credentialId")
    created_at: Mapped[datetime] = mapped_column("createdAt")

    __table_args__ = (
        Index("scimManagedConnectionEvent_connectionRecordId_idx", "connectionRecordId"),
        Index("scimManagedConnectionEvent_eventKey_key", "eventKey", unique=True),
    )

    scimmanagedconnection: Mapped["ScimManagedConnection"] = relationship(back_populates="scimmanagedconnectionevents")

class ScimConnectionBinding(Base):
    __tablename__ = "scimConnectionBinding"

    id: Mapped[str] = mapped_column(primary_key=True)
    connection_id: Mapped[str] = mapped_column("connectionId")
    connection_key: Mapped[str] = mapped_column("connectionKey")
    provisioning_domain_id: Mapped[str] = mapped_column("provisioningDomainId")
    created_at: Mapped[datetime] = mapped_column("createdAt")
    decommissioned_at: Mapped[Optional[datetime]] = mapped_column("decommissionedAt")
    decommission_status: Mapped[str] = mapped_column("decommissionStatus", default="active", server_default=text("'active'"))
    decommission_cursor_user_id: Mapped[Optional[str]] = mapped_column("decommissionCursorUserId")
    decommission_reconciled_user_count: Mapped[int] = mapped_column("decommissionReconciledUserCount", default=0, server_default=text("0"))
    decommission_batch_count: Mapped[int] = mapped_column("decommissionBatchCount", default=0, server_default=text("0"))
    decommission_revision: Mapped[int] = mapped_column("decommissionRevision", default=0, server_default=text("0"))
    decommission_completed_at: Mapped[Optional[datetime]] = mapped_column("decommissionCompletedAt")
    decommission_lease_id: Mapped[Optional[str]] = mapped_column("decommissionLeaseId")
    decommission_lease_expires_at: Mapped[Optional[datetime]] = mapped_column("decommissionLeaseExpiresAt")

    __table_args__ = (
        Index("scimConnectionBinding_connectionId_idx", "connectionId"),
        Index("scimConnectionBinding_connectionKey_key", "connectionKey", unique=True),
    )

class ScimIdentityTombstone(Base):
    __tablename__ = "scimIdentityTombstone"

    id: Mapped[str] = mapped_column(primary_key=True)
    connection_id: Mapped[str] = mapped_column("connectionId")
    provisioning_domain_id: Mapped[str] = mapped_column("provisioningDomainId")
    external_id: Mapped[str] = mapped_column("externalId")
    external_id_key: Mapped[str] = mapped_column("externalIdKey")
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="scimIdentityTombstone_userId_fkey"))
    profile: Mapped[str]
    deleted_at: Mapped[datetime] = mapped_column("deletedAt")

    __table_args__ = (
        Index("scimIdentityTombstone_connectionId_idx", "connectionId"),
        Index("scimIdentityTombstone_provisioningDomainId_idx", "provisioningDomainId"),
        Index("scimIdentityTombstone_userId_idx", "userId"),
        Index("scimIdentityTombstone_externalIdKey_key", "externalIdKey", unique=True),
    )

    user: Mapped["User"] = relationship(back_populates="scim_identity_tombstones")

class ScimSubject(Base):
    __tablename__ = "scimSubject"

    id: Mapped[str] = mapped_column(primary_key=True)
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="scimSubject_userId_fkey"))
    profile_source_id: Mapped[Optional[str]] = mapped_column("profileSourceId")
    revision: Mapped[int]
    created_at: Mapped[datetime] = mapped_column("createdAt")
    updated_at: Mapped[datetime] = mapped_column("updatedAt")

    __table_args__ = (
        Index("scimSubject_profileSourceId_idx", "profileSourceId"),
        Index("scimSubject_userId_key", "userId", unique=True),
    )

    user: Mapped["User"] = relationship(back_populates="scim_subject")

class ScimUser(Base):
    __tablename__ = "scimUser"

    id: Mapped[str] = mapped_column(primary_key=True)
    connection_id: Mapped[str] = mapped_column("connectionId")
    provisioning_domain_id: Mapped[str] = mapped_column("provisioningDomainId")
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="scimUser_userId_fkey"))
    connection_user_key: Mapped[str] = mapped_column("connectionUserKey")
    user_name: Mapped[str] = mapped_column("userName")
    user_name_key: Mapped[str] = mapped_column("userNameKey")
    primary_email: Mapped[str] = mapped_column("primaryEmail")
    work_email_value_index: Mapped[str] = mapped_column("workEmailValueIndex")
    email_value_index: Mapped[str] = mapped_column("emailValueIndex")
    display_name: Mapped[str] = mapped_column("displayName")
    formatted_name: Mapped[str] = mapped_column("formattedName")
    given_name: Mapped[Optional[str]] = mapped_column("givenName")
    family_name: Mapped[Optional[str]] = mapped_column("familyName")
    serialized_emails: Mapped[str] = mapped_column("serializedEmails")
    serialized_attributes: Mapped[Optional[str]] = mapped_column("serializedAttributes")
    external_id: Mapped[Optional[str]] = mapped_column("externalId")
    external_id_key: Mapped[Optional[str]] = mapped_column("externalIdKey")
    active: Mapped[bool]
    order_key: Mapped[str] = mapped_column("orderKey")
    created_at: Mapped[datetime] = mapped_column("createdAt")
    updated_at: Mapped[datetime] = mapped_column("updatedAt")

    __table_args__ = (
        Index("scimUser_connectionId_idx", "connectionId"),
        Index("scimUser_provisioningDomainId_idx", "provisioningDomainId"),
        Index("scimUser_userId_idx", "userId"),
        Index("scimUser_connectionUserKey_key", "connectionUserKey", unique=True),
        Index("scimUser_userNameKey_key", "userNameKey", unique=True),
        Index("scimUser_externalIdKey_key", "externalIdKey", unique=True),
        Index("scimUser_orderKey_key", "orderKey", unique=True),
    )

    user: Mapped["User"] = relationship(back_populates="scim_users")
    scim_projection_grants: Mapped[list["ScimProjectionGrant"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="scim_user")
    scim_group_members: Mapped[list["ScimGroupMember"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="scim_user")

class ScimProjectionGrant(Base):
    __tablename__ = "scimProjectionGrant"

    id: Mapped[str] = mapped_column(primary_key=True)
    connection_id: Mapped[str] = mapped_column("connectionId")
    provisioning_domain_id: Mapped[str] = mapped_column("provisioningDomainId")
    scim_user_id: Mapped[str] = mapped_column("scimUserId", ForeignKey("scimUser.id", ondelete="CASCADE", onupdate="CASCADE", name="scimProjectionGrant_scimUserId_fkey"))
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="scimProjectionGrant_userId_fkey"))
    source_kind: Mapped[str] = mapped_column("sourceKind")
    source_id: Mapped[str] = mapped_column("sourceId")
    source_value: Mapped[Optional[str]] = mapped_column("sourceValue")
    role: Mapped[str]
    grant_key: Mapped[str] = mapped_column("grantKey")
    created_at: Mapped[datetime] = mapped_column("createdAt")
    updated_at: Mapped[datetime] = mapped_column("updatedAt")

    __table_args__ = (
        Index("scimProjectionGrant_connectionId_idx", "connectionId"),
        Index("scimProjectionGrant_provisioningDomainId_idx", "provisioningDomainId"),
        Index("scimProjectionGrant_scimUserId_idx", "scimUserId"),
        Index("scimProjectionGrant_userId_idx", "userId"),
        Index("scimProjectionGrant_grantKey_key", "grantKey", unique=True),
    )

    scim_user: Mapped["ScimUser"] = relationship(back_populates="scim_projection_grants")
    user: Mapped["User"] = relationship(back_populates="scim_projection_grants")

class ScimGroup(Base):
    __tablename__ = "scimGroup"

    id: Mapped[str] = mapped_column(primary_key=True)
    connection_id: Mapped[str] = mapped_column("connectionId")
    provisioning_domain_id: Mapped[str] = mapped_column("provisioningDomainId")
    revision: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    display_name: Mapped[str] = mapped_column("displayName")
    display_name_key: Mapped[str] = mapped_column("displayNameKey")
    external_id: Mapped[Optional[str]] = mapped_column("externalId")
    external_id_key: Mapped[Optional[str]] = mapped_column("externalIdKey")
    order_key: Mapped[str] = mapped_column("orderKey")
    created_at: Mapped[datetime] = mapped_column("createdAt")
    updated_at: Mapped[datetime] = mapped_column("updatedAt")

    __table_args__ = (
        Index("scimGroup_connectionId_idx", "connectionId"),
        Index("scimGroup_provisioningDomainId_idx", "provisioningDomainId"),
        Index("scimGroup_displayNameKey_key", "displayNameKey", unique=True),
        Index("scimGroup_externalIdKey_key", "externalIdKey", unique=True),
        Index("scimGroup_orderKey_key", "orderKey", unique=True),
    )

    scim_group_members: Mapped[list["ScimGroupMember"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="scim_group")

class ScimGroupMember(Base):
    __tablename__ = "scimGroupMember"

    id: Mapped[str] = mapped_column(primary_key=True)
    connection_id: Mapped[str] = mapped_column("connectionId")
    group_id: Mapped[str] = mapped_column("groupId", ForeignKey("scimGroup.id", ondelete="CASCADE", onupdate="CASCADE", name="scimGroupMember_groupId_fkey"))
    scim_user_id: Mapped[str] = mapped_column("scimUserId", ForeignKey("scimUser.id", ondelete="CASCADE", onupdate="CASCADE", name="scimGroupMember_scimUserId_fkey"))
    membership_key: Mapped[str] = mapped_column("membershipKey")
    created_at: Mapped[datetime] = mapped_column("createdAt")

    __table_args__ = (
        Index("scimGroupMember_connectionId_idx", "connectionId"),
        Index("scimGroupMember_groupId_idx", "groupId"),
        Index("scimGroupMember_scimUserId_idx", "scimUserId"),
        Index("scimGroupMember_membershipKey_key", "membershipKey", unique=True),
    )

    scim_group: Mapped["ScimGroup"] = relationship(back_populates="scim_group_members")
    scim_user: Mapped["ScimUser"] = relationship(back_populates="scim_group_members")

class ScimIdentityLink(Base):
    __tablename__ = "ScimIdentityLink"

    id: Mapped[str] = mapped_column(primary_key=True)
    connection_id: Mapped[str] = mapped_column("connectionId")
    external_id: Mapped[str] = mapped_column("externalId")
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="ScimIdentityLink_userId_fkey"))

    __table_args__ = (
        Index("ScimIdentityLink_userId_idx", "userId"),
        Index("ScimIdentityLink_connectionId_externalId_key", "connectionId", "externalId", unique=True),
    )

    user: Mapped["User"] = relationship(back_populates="scim_identity_links")

class LoopsEmailSource(Base):
    __tablename__ = "LoopsEmailSource"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    email_message_id: Mapped[Optional[str]] = mapped_column("emailMessageId")
    loop_id: Mapped[Optional[str]] = mapped_column("loopId")
    loop_name: Mapped[Optional[str]] = mapped_column("loopName")
    campaign_id: Mapped[Optional[str]] = mapped_column("campaignId")
    campaign_name: Mapped[Optional[str]] = mapped_column("campaignName")
    source_type: Mapped[Optional[str]] = mapped_column("sourceType")
    event_time: Mapped[Optional[datetime]] = mapped_column("eventTime")

    __table_args__ = (
        Index("LoopsEmailSource_emailMessageId_key", "emailMessageId", unique=True),
        Index("LoopsEmailSource_loopId_idx", "loopId"),
        Index("LoopsEmailSource_campaignId_idx", "campaignId"),
        Index("LoopsEmailSource_loopId_unversioned_key", "loopId", unique=True),
        Index("LoopsEmailSource_campaignId_unversioned_key", "campaignId", unique=True),
    )
