from sqlalchemy import ARRAY, BigInteger, Date, Dialect, Enum, ForeignKey, ForeignKeyConstraint, Index, MetaData, Numeric, Text, TypeDecorator, text
from sqlalchemy.dialects.postgresql import JSONB, TIMESTAMP
from sqlalchemy.orm import DeclarativeBase, Mapped, mapped_column, relationship
from typing import Any, Optional
from decimal import Decimal as DecimalType
from datetime import datetime, date, timezone
import uuid as uuid_mod


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
    type_annotation_map = {datetime: UtcDateTime(precision=3), str: Text, DecimalType: Numeric(precision=65, scale=30)}


class Creative(Base):
    __tablename__ = "Creative"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)
    name: Mapped[str]
    type: Mapped[str] = mapped_column(Enum("feedbackButton", "banner", name="CreativeType"))
    status: Mapped[str] = mapped_column(Enum("draft", "inProgress", "paused", "completed", name="CreativeStatus"), default="draft", server_default=text("'draft'"))
    published_at: Mapped[Optional[datetime]] = mapped_column("publishedAt")
    archived_at: Mapped[Optional[datetime]] = mapped_column("archivedAt")
    workspace_id: Mapped[str] = mapped_column("workspaceId", ForeignKey("Workspace.id", ondelete="CASCADE", onupdate="CASCADE", name="Creative_workspaceId_fkey"))
    created_by: Mapped[Optional[str]] = mapped_column("createdBy", ForeignKey("User.id", ondelete="SET NULL", onupdate="CASCADE", name="Creative_createdBy_fkey"))
    content: Mapped[dict[str, Any]] = mapped_column(JSONB)
    styling: Mapped[dict[str, Any]] = mapped_column(JSONB)
    survey_id: Mapped[Optional[str]] = mapped_column("surveyId")
    link_url: Mapped[Optional[str]] = mapped_column("linkUrl")
    show_on: Mapped[str] = mapped_column("showOn", Enum("pageView", "action", name="CreativeShowOn"), default="pageView", server_default=text("'pageView'"))
    show_conditions: Mapped[Optional[dict[str, Any]]] = mapped_column("showConditions", JSONB(none_as_null=True))
    display_percentage: Mapped[Optional[float]] = mapped_column("displayPercentage")
    start_at: Mapped[Optional[datetime]] = mapped_column("startAt")
    end_at: Mapped[Optional[datetime]] = mapped_column("endAt")
    auto_select_language: Mapped[Optional[bool]] = mapped_column("autoSelectLanguage")

    __table_args__ = (
        Index("Creative_workspaceId_updated_at_idx", "workspaceId", "updated_at"),
        Index("Creative_workspaceId_created_at_idx", "workspaceId", "created_at"),
        Index("Creative_workspaceId_name_idx", "workspaceId", "name"),
        Index("Creative_surveyId_idx", "surveyId"),
        ForeignKeyConstraint(["surveyId", "workspaceId"], ["Survey.id", "Survey.workspaceId"], ondelete="NO ACTION", onupdate="NO ACTION", name="Creative_surveyId_workspaceId_fkey"),
    )

    workspace: Mapped["Workspace"] = relationship(overlaps="creatives,survey", back_populates="creatives")
    creator: Mapped[Optional["User"]] = relationship(back_populates="creatives")
    survey: Mapped[Optional["Survey"]] = relationship(overlaps="creatives,workspace", back_populates="creatives")
    triggers: Mapped[list["CreativeTrigger"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="creative")
    languages: Mapped[list["CreativeLanguage"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="creative")
    daily_stats: Mapped[list["CreativeDailyStat"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="creative")
    daily_page_stats: Mapped[list["CreativeDailyPageStat"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="creative")

class CreativeDailyStat(Base):
    __tablename__ = "CreativeDailyStat"

    creative_id: Mapped[str] = mapped_column("creativeId", ForeignKey("Creative.id", ondelete="CASCADE", onupdate="CASCADE", name="CreativeDailyStat_creativeId_fkey"), primary_key=True)
    date_: Mapped[date] = mapped_column("date", Date, primary_key=True)
    device_type: Mapped[str] = mapped_column("deviceType", Enum("phone", "tablet", "desktop", name="CreativeDeviceType"), primary_key=True)
    impressions: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    clicks: Mapped[int] = mapped_column(default=0, server_default=text("0"))

    creative: Mapped["Creative"] = relationship(back_populates="daily_stats")

class CreativeDailyPageStat(Base):
    __tablename__ = "CreativeDailyPageStat"

    creative_id: Mapped[str] = mapped_column("creativeId", ForeignKey("Creative.id", ondelete="CASCADE", onupdate="CASCADE", name="CreativeDailyPageStat_creativeId_fkey"), primary_key=True)
    date_: Mapped[date] = mapped_column("date", Date, primary_key=True)
    page: Mapped[str] = mapped_column(primary_key=True)
    impressions: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    clicks: Mapped[int] = mapped_column(default=0, server_default=text("0"))

    creative: Mapped["Creative"] = relationship(back_populates="daily_page_stats")

class CreativeTrigger(Base):
    __tablename__ = "CreativeTrigger"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)
    creative_id: Mapped[str] = mapped_column("creativeId", ForeignKey("Creative.id", ondelete="CASCADE", onupdate="CASCADE", name="CreativeTrigger_creativeId_fkey"))
    action_class_id: Mapped[str] = mapped_column("actionClassId", ForeignKey("ActionClass.id", ondelete="CASCADE", onupdate="CASCADE", name="CreativeTrigger_actionClassId_fkey"))

    __table_args__ = (
        Index("CreativeTrigger_actionClassId_idx", "actionClassId"),
        Index("CreativeTrigger_creativeId_actionClassId_key", "creativeId", "actionClassId", unique=True),
    )

    creative: Mapped["Creative"] = relationship(back_populates="triggers")
    action_class: Mapped["ActionClass"] = relationship(back_populates="creative_triggers")

class CreativeLanguage(Base):
    __tablename__ = "CreativeLanguage"

    language_id: Mapped[str] = mapped_column("languageId", ForeignKey("Language.id", ondelete="CASCADE", onupdate="CASCADE", name="CreativeLanguage_languageId_fkey"), primary_key=True)
    creative_id: Mapped[str] = mapped_column("creativeId", ForeignKey("Creative.id", ondelete="CASCADE", onupdate="CASCADE", name="CreativeLanguage_creativeId_fkey"), primary_key=True)
    default: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    enabled: Mapped[bool] = mapped_column(default=True, server_default=text("true"))

    __table_args__ = (
        Index("CreativeLanguage_creativeId_idx", "creativeId"),
    )

    language: Mapped["Language"] = relationship(back_populates="creative_languages")
    creative: Mapped["Creative"] = relationship(back_populates="languages")

class OutboundEmailAudit(Base):
    __tablename__ = "OutboundEmailAudit"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    expires_at: Mapped[datetime] = mapped_column("expiresAt")
    content_expires_at: Mapped[datetime] = mapped_column("contentExpiresAt")
    environment: Mapped[str]
    email_type: Mapped[str] = mapped_column("emailType")
    trigger: Mapped[str]
    actor_kind: Mapped[str] = mapped_column("actorKind")
    actor_user_id: Mapped[Optional[str]] = mapped_column("actorUserId")
    configured_by_user_id: Mapped[Optional[str]] = mapped_column("configuredByUserId")
    actor_user_hash: Mapped[Optional[str]] = mapped_column("actorUserHash")
    configured_by_user_hash: Mapped[Optional[str]] = mapped_column("configuredByUserHash")
    organization_id: Mapped[Optional[str]] = mapped_column("organizationId")
    survey_id: Mapped[Optional[str]] = mapped_column("surveyId")
    response_id: Mapped[Optional[str]] = mapped_column("responseId")
    follow_up_id: Mapped[Optional[str]] = mapped_column("followUpId")
    workflow_id: Mapped[Optional[str]] = mapped_column("workflowId")
    workflow_run_id: Mapped[Optional[str]] = mapped_column("workflowRunId")
    invite_id: Mapped[Optional[str]] = mapped_column("inviteId")
    recipient_mac: Mapped[Optional[str]] = mapped_column("recipientMac")
    ses_message_hash: Mapped[Optional[str]] = mapped_column("sesMessageHash")
    snapshot_key: Mapped[Optional[str]] = mapped_column("snapshotKey")
    snapshot_sha256: Mapped[Optional[str]] = mapped_column("snapshotSha256")
    status: Mapped[str] = mapped_column(default="prepared", server_default=text("'prepared'"))
    status_recorded_at: Mapped[Optional[datetime]] = mapped_column("statusRecordedAt")
    audit_gap: Mapped[bool] = mapped_column("auditGap", default=False, server_default=text("false"))
    erasure_requested_at: Mapped[Optional[datetime]] = mapped_column("erasureRequestedAt")
    erased_at: Mapped[Optional[datetime]] = mapped_column("erasedAt")
    content_delete_retry_at: Mapped[Optional[datetime]] = mapped_column("contentDeleteRetryAt")
    content_delete_failures: Mapped[int] = mapped_column("contentDeleteFailures", default=0, server_default=text("0"))

    __table_args__ = (
        Index("OutboundEmailAudit_environment_createdAt_id_idx", "environment", "createdAt", "id"),
        Index("OutboundEmailAudit_organizationId_createdAt_id_idx", "organizationId", "createdAt", "id"),
        Index("OutboundEmailAudit_surveyId_createdAt_id_idx", "surveyId", "createdAt", "id"),
        Index("OutboundEmailAudit_responseId_createdAt_id_idx", "responseId", "createdAt", "id"),
        Index("OutboundEmailAudit_actorUserId_createdAt_id_idx", "actorUserId", "createdAt", "id"),
        Index("OutboundEmailAudit_configuredByUserId_createdAt_id_idx", "configuredByUserId", "createdAt", "id"),
        Index("OutboundEmailAudit_actorUserHash_createdAt_id_idx", "actorUserHash", "createdAt", "id"),
        Index("OutboundEmailAudit_configuredByUserHash_createdAt_id_idx", "configuredByUserHash", "createdAt", "id"),
        Index("OutboundEmailAudit_recipientMac_createdAt_id_idx", "recipientMac", "createdAt", "id"),
        Index("OutboundEmailAudit_sesMessageHash_idx", "sesMessageHash"),
        Index("OutboundEmailAudit_status_createdAt_id_idx", "status", "createdAt", "id"),
        Index("OutboundEmailAudit_contentExpiresAt_id_idx", "contentExpiresAt", "id"),
        Index("OutboundEmailAudit_expiresAt_id_idx", "expiresAt", "id"),
        Index("OutboundEmailAudit_erasureRequestedAt_id_idx", "erasureRequestedAt", "id"),
        Index("OutboundEmailAudit_contentDeleteRetryAt_id_idx", "contentDeleteRetryAt", "id"),
    )

class OutboundEmailAuditErasureIntent(Base):
    __tablename__ = "OutboundEmailAuditErasureIntent"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    user_id: Mapped[Optional[str]] = mapped_column("userId")
    user_hash: Mapped[str] = mapped_column("userHash")
    recipient_mac: Mapped[str] = mapped_column("recipientMac")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    activated_at: Mapped[Optional[datetime]] = mapped_column("activatedAt")
    checked_at: Mapped[Optional[datetime]] = mapped_column("checkedAt")
    expires_at: Mapped[datetime] = mapped_column("expiresAt")

    __table_args__ = (
        Index("OutboundEmailAuditErasureIntent_userId_idx", "userId"),
        Index("OutboundEmailAuditErasureIntent_recipientMac_activatedAt_idx", "recipientMac", "activatedAt"),
        Index("OutboundEmailAuditErasureIntent_userHash_activatedAt_idx", "userHash", "activatedAt"),
        Index("OutboundEmailAuditErasureIntent_checkedAt_id_idx", "checkedAt", "id"),
        Index("OutboundEmailAuditErasureIntent_expiresAt_idx", "expiresAt"),
    )

class Webhook(Base):
    __tablename__ = "Webhook"

    id: Mapped[str] = mapped_column(primary_key=True)
    name: Mapped[Optional[str]]
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    url: Mapped[str]
    source: Mapped[str] = mapped_column(Enum("user", "zapier", "make", "n8n", "activepieces", name="WebhookSource"), default="user", server_default=text("'user'"))
    workspace_id: Mapped[str] = mapped_column("workspaceId", ForeignKey("Workspace.id", ondelete="CASCADE", onupdate="CASCADE", name="Webhook_workspaceId_fkey"))
    triggers: Mapped[list[str]] = mapped_column(ARRAY(Enum("responseCreated", "responseUpdated", "responseFinished", name="PipelineTriggers")), nullable=True)
    survey_ids: Mapped[list[str]] = mapped_column("surveyIds", ARRAY(Text), nullable=True)
    secret: Mapped[Optional[str]]

    __table_args__ = (
        Index("Webhook_workspaceId_idx", "workspaceId"),
    )

    workspace: Mapped["Workspace"] = relationship(back_populates="webhooks")

class ContactAttribute(Base):
    __tablename__ = "ContactAttribute"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)
    attribute_key_id: Mapped[str] = mapped_column("attributeKeyId", ForeignKey("ContactAttributeKey.id", ondelete="CASCADE", onupdate="CASCADE", name="ContactAttribute_attributeKeyId_fkey"))
    contact_id: Mapped[str] = mapped_column("contactId", ForeignKey("Contact.id", ondelete="CASCADE", onupdate="CASCADE", name="ContactAttribute_contactId_fkey"))
    value: Mapped[str]
    value_number: Mapped[Optional[float]] = mapped_column("valueNumber")
    value_date: Mapped[Optional[datetime]] = mapped_column("valueDate")

    __table_args__ = (
        Index("ContactAttribute_attributeKeyId_value_idx", "attributeKeyId", "value"),
        Index("ContactAttribute_attributeKeyId_valueNumber_idx", "attributeKeyId", "valueNumber"),
        Index("ContactAttribute_attributeKeyId_valueDate_idx", "attributeKeyId", "valueDate"),
        Index("ContactAttribute_contactId_attributeKeyId_key", "contactId", "attributeKeyId", unique=True),
    )

    attribute_key: Mapped["ContactAttributeKey"] = relationship(back_populates="attributes")
    contact: Mapped["Contact"] = relationship(back_populates="attributes")

class ContactAttributeKey(Base):
    __tablename__ = "ContactAttributeKey"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)
    is_unique: Mapped[bool] = mapped_column("isUnique", default=False, server_default=text("false"))
    key: Mapped[str]
    name: Mapped[Optional[str]]
    description: Mapped[Optional[str]]
    type: Mapped[str] = mapped_column(Enum("default", "custom", name="ContactAttributeType"), default="custom", server_default=text("'custom'"))
    data_type: Mapped[str] = mapped_column("dataType", Enum("string", "number", "date", name="ContactAttributeDataType"), default="string", server_default=text("'string'"))
    workspace_id: Mapped[str] = mapped_column("workspaceId", ForeignKey("Workspace.id", ondelete="CASCADE", onupdate="CASCADE", name="ContactAttributeKey_workspaceId_fkey"))

    __table_args__ = (
        Index("ContactAttributeKey_workspaceId_created_at_idx", "workspaceId", "created_at"),
        Index("ContactAttributeKey_key_workspaceId_key", "key", "workspaceId", unique=True),
    )

    workspace: Mapped["Workspace"] = relationship(back_populates="contact_attribute_keys")
    attributes: Mapped[list["ContactAttribute"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="attribute_key")
    attribute_filters: Mapped[list["SurveyAttributeFilter"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="attribute_key")

class Contact(Base):
    __tablename__ = "Contact"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)
    workspace_id: Mapped[str] = mapped_column("workspaceId", ForeignKey("Workspace.id", ondelete="CASCADE", onupdate="CASCADE", name="Contact_workspaceId_fkey"))

    __table_args__ = (
        Index("Contact_workspaceId_idx", "workspaceId"),
    )

    workspace: Mapped["Workspace"] = relationship(back_populates="contacts")
    responses: Mapped[list["Response"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="contact")
    attributes: Mapped[list["ContactAttribute"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="contact")
    displays: Mapped[list["Display"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="contact")

class Response(Base):
    __tablename__ = "Response"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    finished: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    survey_id: Mapped[str] = mapped_column("surveyId", ForeignKey("Survey.id", ondelete="CASCADE", onupdate="CASCADE", name="Response_surveyId_fkey"))
    contact_id: Mapped[Optional[str]] = mapped_column("contactId", ForeignKey("Contact.id", ondelete="CASCADE", onupdate="CASCADE", name="Response_contactId_fkey"))
    ending_id: Mapped[Optional[str]] = mapped_column("endingId")
    data: Mapped[dict[str, Any]] = mapped_column(JSONB, default=lambda: {}, server_default=text("'{}'"))
    variables: Mapped[dict[str, Any]] = mapped_column(JSONB, default=lambda: {}, server_default=text("'{}'"))
    ttc: Mapped[dict[str, Any]] = mapped_column(JSONB, default=lambda: {}, server_default=text("'{}'"))
    meta: Mapped[dict[str, Any]] = mapped_column(JSONB, default=lambda: {}, server_default=text("'{}'"))
    ingest_flags: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    outcome: Mapped[Optional[str]] = mapped_column(Enum("lowQuality", name="ResponseOutcome"))
    contact_attributes: Mapped[Optional[dict[str, Any]]] = mapped_column("contactAttributes", JSONB(none_as_null=True))
    single_use_id: Mapped[Optional[str]] = mapped_column("singleUseId")
    language: Mapped[Optional[str]]
    display_id: Mapped[Optional[str]] = mapped_column("displayId", ForeignKey("Display.id", ondelete="SET NULL", onupdate="CASCADE", name="Response_displayId_fkey"))

    __table_args__ = (
        Index("Response_displayId_key", "displayId", unique=True),
        Index("Response_created_at_idx", "created_at"),
        Index("Response_created_at_id_idx", "created_at", "id"),
        Index("Response_surveyId_created_at_idx", "surveyId", "created_at"),
        Index("Response_surveyId_created_at_id_idx", "surveyId", "created_at", "id"),
        Index("Response_contactId_created_at_idx", "contactId", "created_at"),
        Index("Response_contactId_created_at_id_idx", "contactId", "created_at", "id"),
        Index("Response_surveyId_finished_idx", "surveyId", "finished"),
        Index("Response_surveyId_singleUseId_key", "surveyId", "singleUseId", unique=True),
    )

    survey: Mapped["Survey"] = relationship(back_populates="responses")
    contact: Mapped[Optional["Contact"]] = relationship(back_populates="responses")
    display: Mapped[Optional["Display"]] = relationship(back_populates="response")
    tags: Mapped[list["TagsOnResponses"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="response")
    quota_links: Mapped[list["ResponseQuotaLink"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="response")
    workflow_runs: Mapped[list["WorkflowRun"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="response")

class Tag(Base):
    __tablename__ = "Tag"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)
    name: Mapped[str]
    workspace_id: Mapped[str] = mapped_column("workspaceId", ForeignKey("Workspace.id", ondelete="CASCADE", onupdate="CASCADE", name="Tag_workspaceId_fkey"))

    __table_args__ = (
        Index("Tag_workspaceId_idx", "workspaceId"),
        Index("Tag_workspaceId_name_key", "workspaceId", "name", unique=True),
    )

    workspace: Mapped["Workspace"] = relationship(back_populates="tags")
    responses: Mapped[list["TagsOnResponses"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="tag")

class TagsOnResponses(Base):
    __tablename__ = "TagsOnResponses"

    response_id: Mapped[str] = mapped_column("responseId", ForeignKey("Response.id", ondelete="CASCADE", onupdate="CASCADE", name="TagsOnResponses_responseId_fkey"), primary_key=True)
    tag_id: Mapped[str] = mapped_column("tagId", ForeignKey("Tag.id", ondelete="CASCADE", onupdate="CASCADE", name="TagsOnResponses_tagId_fkey"), primary_key=True)

    response: Mapped["Response"] = relationship(back_populates="tags")
    tag: Mapped["Tag"] = relationship(back_populates="responses")

class Display(Base):
    __tablename__ = "Display"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)
    survey_id: Mapped[str] = mapped_column("surveyId", ForeignKey("Survey.id", ondelete="CASCADE", onupdate="CASCADE", name="Display_surveyId_fkey"))
    contact_id: Mapped[Optional[str]] = mapped_column("contactId", ForeignKey("Contact.id", ondelete="CASCADE", onupdate="CASCADE", name="Display_contactId_fkey"))
    creative_id: Mapped[Optional[str]] = mapped_column("creativeId")

    __table_args__ = (
        Index("Display_surveyId_idx", "surveyId"),
        Index("Display_contactId_created_at_idx", "contactId", "created_at"),
    )

    survey: Mapped["Survey"] = relationship(back_populates="displays")
    contact: Mapped[Optional["Contact"]] = relationship(back_populates="displays")
    response: Mapped[Optional["Response"]] = relationship(back_populates="display")

class SurveyTrigger(Base):
    __tablename__ = "SurveyTrigger"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)
    survey_id: Mapped[str] = mapped_column("surveyId", ForeignKey("Survey.id", ondelete="CASCADE", onupdate="CASCADE", name="SurveyTrigger_surveyId_fkey"))
    action_class_id: Mapped[str] = mapped_column("actionClassId", ForeignKey("ActionClass.id", ondelete="CASCADE", onupdate="CASCADE", name="SurveyTrigger_actionClassId_fkey"))

    __table_args__ = (
        Index("SurveyTrigger_surveyId_actionClassId_key", "surveyId", "actionClassId", unique=True),
    )

    survey: Mapped["Survey"] = relationship(back_populates="triggers")
    action_class: Mapped["ActionClass"] = relationship(back_populates="survey_triggers")

class SurveyAttributeFilter(Base):
    __tablename__ = "SurveyAttributeFilter"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)
    attribute_key_id: Mapped[str] = mapped_column("attributeKeyId", ForeignKey("ContactAttributeKey.id", ondelete="CASCADE", onupdate="CASCADE", name="SurveyAttributeFilter_attributeKeyId_fkey"))
    survey_id: Mapped[str] = mapped_column("surveyId", ForeignKey("Survey.id", ondelete="CASCADE", onupdate="CASCADE", name="SurveyAttributeFilter_surveyId_fkey"))
    condition: Mapped[str] = mapped_column(Enum("equals", "notEquals", name="SurveyAttributeFilterCondition"))
    value: Mapped[str]

    __table_args__ = (
        Index("SurveyAttributeFilter_attributeKeyId_idx", "attributeKeyId"),
        Index("SurveyAttributeFilter_surveyId_attributeKeyId_key", "surveyId", "attributeKeyId", unique=True),
    )

    attribute_key: Mapped["ContactAttributeKey"] = relationship(back_populates="attribute_filters")
    survey: Mapped["Survey"] = relationship(back_populates="attribute_filters")

class Survey(Base):
    __tablename__ = "Survey"

    hidden_fields: Mapped[dict[str, Any]] = mapped_column("hiddenFields", JSONB, default=lambda: {"enabled": False}, server_default=text("'{\"enabled\": false}'"))
    variables: Mapped[dict[str, Any]] = mapped_column(JSONB, default=lambda: [], server_default=text("'[]'"))
    ownership_initialized: Mapped[bool] = mapped_column("ownershipInitialized", default=False, server_default=text("false"))
    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)
    name: Mapped[str]
    redirect_url: Mapped[Optional[str]] = mapped_column("redirectUrl")
    type: Mapped[str] = mapped_column(Enum("link", "app", name="SurveyType"), default="app", server_default=text("'app'"))
    workspace_id: Mapped[str] = mapped_column("workspaceId", ForeignKey("Workspace.id", ondelete="CASCADE", onupdate="CASCADE", name="Survey_workspaceId_fkey"))
    created_by: Mapped[Optional[str]] = mapped_column("createdBy", ForeignKey("User.id", ondelete="SET NULL", onupdate="CASCADE", name="Survey_createdBy_fkey"))
    visibility: Mapped[str] = mapped_column(Enum("restricted", "workspace", name="SurveyVisibility"), default="workspace", server_default=text("'workspace'"))
    owner_id: Mapped[Optional[str]] = mapped_column("ownerId", ForeignKey("User.id", ondelete="SET NULL", onupdate="CASCADE", name="Survey_ownerId_fkey"))
    visibility_version: Mapped[int] = mapped_column("visibilityVersion", default=0, server_default=text("0"))
    visibility_projected_version: Mapped[int] = mapped_column("visibilityProjectedVersion", default=0, server_default=text("0"))
    visibility_pending: Mapped[bool] = mapped_column("visibilityPending", default=False, server_default=text("false"))
    visibility_changed_at: Mapped[Optional[datetime]] = mapped_column("visibilityChangedAt")
    visibility_changed_by_id: Mapped[Optional[str]] = mapped_column("visibilityChangedById")
    status: Mapped[str] = mapped_column(Enum("draft", "inProgress", "paused", "completed", name="SurveyStatus"), default="draft", server_default=text("'draft'"))
    welcome_card: Mapped[dict[str, Any]] = mapped_column("welcomeCard", JSONB, default=lambda: {"enabled": False}, server_default=text("'{\"enabled\": false}'"))
    questions: Mapped[dict[str, Any]] = mapped_column(JSONB, default=lambda: [], server_default=text("'[]'"))
    blocks: Mapped[list[dict[str, Any]]] = mapped_column(ARRAY(JSONB), nullable=True, default=lambda: [], server_default=text("ARRAY[]::jsonb[]"))
    endings: Mapped[list[dict[str, Any]]] = mapped_column(ARRAY(JSONB), nullable=True, default=lambda: [], server_default=text("ARRAY[]::jsonb[]"))
    display_option: Mapped[str] = mapped_column("displayOption", Enum("displayOnce", "displayMultiple", "displaySome", "respondMultiple", name="displayOptions"), default="displayOnce", server_default=text("'displayOnce'"))
    recontact_days: Mapped[Optional[int]] = mapped_column("recontactDays")
    display_limit: Mapped[Optional[int]] = mapped_column("displayLimit")
    inline_triggers: Mapped[Optional[dict[str, Any]]] = mapped_column("inlineTriggers", JSONB(none_as_null=True))
    auto_close: Mapped[Optional[int]] = mapped_column("autoClose")
    keep_ending_card_open: Mapped[bool] = mapped_column("keepEndingCardOpen", default=False, server_default=text("false"))
    auto_complete: Mapped[Optional[int]] = mapped_column("autoComplete")
    delay: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    publish_on: Mapped[Optional[datetime]] = mapped_column("publishOn")
    close_on: Mapped[Optional[datetime]] = mapped_column("closeOn")
    archived_at: Mapped[Optional[datetime]] = mapped_column("archivedAt")
    survey_closed_message: Mapped[Optional[dict[str, Any]]] = mapped_column("surveyClosedMessage", JSONB(none_as_null=True))
    segment_id: Mapped[Optional[str]] = mapped_column("segmentId", ForeignKey("Segment.id", ondelete="SET NULL", onupdate="CASCADE", name="Survey_segmentId_fkey"))
    workspace_overwrites: Mapped[Optional[dict[str, Any]]] = mapped_column("workspaceOverwrites", JSONB(none_as_null=True))
    styling: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    single_use: Mapped[Optional[dict[str, Any]]] = mapped_column("singleUse", JSONB(none_as_null=True), default=lambda: {"enabled": False, "isEncrypted": True}, server_default=text("'{\"enabled\": false, \"isEncrypted\": true}'"))
    is_verify_email_enabled: Mapped[bool] = mapped_column("isVerifyEmailEnabled", default=False, server_default=text("false"))
    is_back_button_hidden: Mapped[bool] = mapped_column("isBackButtonHidden", default=False, server_default=text("false"))
    is_auto_progressing_enabled: Mapped[bool] = mapped_column("isAutoProgressingEnabled", default=False, server_default=text("false"))
    is_capture_ip_enabled: Mapped[bool] = mapped_column("isCaptureIpEnabled", default=False, server_default=text("false"))
    is_anonymize_responses_enabled: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    pin: Mapped[Optional[str]]
    display_percentage: Mapped[Optional[DecimalType]] = mapped_column("displayPercentage")
    show_language_switch: Mapped[Optional[bool]] = mapped_column("showLanguageSwitch")
    auto_select_language: Mapped[Optional[bool]] = mapped_column("autoSelectLanguage")
    recaptcha: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True), default=lambda: {"enabled": False, "threshold": 0.1}, server_default=text("'{\"enabled\": false, \"threshold\":0.1}'"))
    metadata_: Mapped[dict[str, Any]] = mapped_column("metadata", JSONB, default=lambda: {}, server_default=text("'{}'"))
    slug: Mapped[Optional[str]]
    custom_head_scripts: Mapped[Optional[str]] = mapped_column("customHeadScripts")
    custom_head_scripts_mode: Mapped[Optional[str]] = mapped_column("customHeadScriptsMode", Enum("add", "replace", name="SurveyScriptMode"), default="add", server_default=text("'add'"))
    custom_css: Mapped[Optional[dict[str, Any]]] = mapped_column("customCss", JSONB(none_as_null=True))
    show_conditions: Mapped[Optional[dict[str, Any]]] = mapped_column("showConditions", JSONB(none_as_null=True))

    __table_args__ = (
        Index("Survey_slug_key", "slug", unique=True),
        Index("Survey_workspaceId_updated_at_idx", "workspaceId", "updated_at"),
        Index("Survey_workspaceId_ownerId_idx", "workspaceId", "ownerId"),
        Index("Survey_segmentId_idx", "segmentId"),
        Index("Survey_status_publishOn_idx", "status", "publishOn"),
        Index("Survey_status_closeOn_idx", "status", "closeOn"),
        Index("Survey_archivedAt_idx", "archivedAt"),
        Index("Survey_id_workspaceId_key", "id", "workspaceId", unique=True),
    )

    workspace: Mapped["Workspace"] = relationship(back_populates="surveys")
    creator: Mapped[Optional["User"]] = relationship(foreign_keys=[created_by], back_populates="surveys")
    owner: Mapped[Optional["User"]] = relationship(foreign_keys=[owner_id], back_populates="owned_surveys")
    segment: Mapped[Optional["Segment"]] = relationship(back_populates="surveys")
    responses: Mapped[list["Response"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="survey")
    quotas: Mapped[list["SurveyQuota"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="survey")
    triggers: Mapped[list["SurveyTrigger"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="survey")
    attribute_filters: Mapped[list["SurveyAttributeFilter"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="survey")
    displays: Mapped[list["Display"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="survey")
    languages: Mapped[list["SurveyLanguage"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="survey")
    embedded_data: Mapped[list["EmbeddedData"]] = relationship(cascade="all, delete", passive_deletes=True, overlaps="embedded_data,workspace", back_populates="survey")
    embedded_data_links: Mapped[list["SurveyEmbeddedData"]] = relationship(cascade="all, delete", passive_deletes=True, overlaps="embedded_data,survey_links", back_populates="survey")
    follow_ups: Mapped[list["SurveyFollowUp"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="survey")
    feedback_source_mappings: Mapped[list["FeedbackSourceFormbricksMapping"]] = relationship(cascade="all, delete", passive_deletes=True, overlaps="feedback_source,formbricks_mappings", back_populates="survey")
    retention_exemptions: Mapped[list["RetentionExemption"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="survey")
    retention_notices: Mapped[list["RetentionNotice"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="survey")
    creatives: Mapped[list["Creative"]] = relationship(passive_deletes="all", overlaps="creatives,workspace", back_populates="survey")
    attachment_exports: Mapped[list["AttachmentExport"]] = relationship(cascade="all, delete", passive_deletes=True, overlaps="attachment_exports,workspace", back_populates="survey")
    retention_restore: Mapped[Optional["RetentionSurveyRestore"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="survey")

class SurveyQuota(Base):
    __tablename__ = "SurveyQuota"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)
    survey_id: Mapped[str] = mapped_column("surveyId", ForeignKey("Survey.id", ondelete="CASCADE", onupdate="CASCADE", name="SurveyQuota_surveyId_fkey"))
    name: Mapped[str]
    limit: Mapped[int]
    logic: Mapped[dict[str, Any]] = mapped_column(JSONB, default=lambda: {}, server_default=text("'{}'"))
    action: Mapped[str] = mapped_column(Enum("endSurvey", "continueSurvey", name="SurveyQuotaAction"))
    ending_card_id: Mapped[Optional[str]] = mapped_column("endingCardId")
    count_partial_submissions: Mapped[bool] = mapped_column("countPartialSubmissions", default=False, server_default=text("false"))

    __table_args__ = (
        Index("SurveyQuota_surveyId_name_key", "surveyId", "name", unique=True),
    )

    survey: Mapped["Survey"] = relationship(back_populates="quotas")
    quota_links: Mapped[list["ResponseQuotaLink"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="quota")

class ResponseQuotaLink(Base):
    __tablename__ = "ResponseQuotaLink"

    response_id: Mapped[str] = mapped_column("responseId", ForeignKey("Response.id", ondelete="CASCADE", onupdate="CASCADE", name="ResponseQuotaLink_responseId_fkey"), primary_key=True)
    quota_id: Mapped[str] = mapped_column("quotaId", ForeignKey("SurveyQuota.id", ondelete="CASCADE", onupdate="CASCADE", name="ResponseQuotaLink_quotaId_fkey"), primary_key=True)
    status: Mapped[str] = mapped_column(Enum("screenedIn", "screenedOut", name="ResponseQuotaLinkStatus"))

    __table_args__ = (
        Index("ResponseQuotaLink_quotaId_status_idx", "quotaId", "status"),
    )

    response: Mapped["Response"] = relationship(back_populates="quota_links")
    quota: Mapped["SurveyQuota"] = relationship(back_populates="quota_links")

class SurveyFollowUp(Base):
    __tablename__ = "SurveyFollowUp"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)
    survey_id: Mapped[str] = mapped_column("surveyId", ForeignKey("Survey.id", ondelete="CASCADE", onupdate="CASCADE", name="SurveyFollowUp_surveyId_fkey"))
    name: Mapped[str]
    trigger: Mapped[dict[str, Any]] = mapped_column(JSONB)
    action: Mapped[dict[str, Any]] = mapped_column(JSONB)

    survey: Mapped["Survey"] = relationship(back_populates="follow_ups")

class ActionClass(Base):
    __tablename__ = "ActionClass"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)
    name: Mapped[str]
    description: Mapped[Optional[str]]
    type: Mapped[str] = mapped_column(Enum("code", "noCode", name="ActionType"))
    key: Mapped[Optional[str]]
    no_code_config: Mapped[Optional[dict[str, Any]]] = mapped_column("noCodeConfig", JSONB(none_as_null=True))
    workspace_id: Mapped[str] = mapped_column("workspaceId", ForeignKey("Workspace.id", ondelete="CASCADE", onupdate="CASCADE", name="ActionClass_workspaceId_fkey"))

    __table_args__ = (
        Index("ActionClass_workspaceId_created_at_idx", "workspaceId", "created_at"),
        Index("ActionClass_key_workspaceId_key", "key", "workspaceId", unique=True),
        Index("ActionClass_name_workspaceId_key", "name", "workspaceId", unique=True),
    )

    workspace: Mapped["Workspace"] = relationship(back_populates="action_classes")
    survey_triggers: Mapped[list["SurveyTrigger"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="action_class")
    creative_triggers: Mapped[list["CreativeTrigger"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="action_class")

class Integration(Base):
    __tablename__ = "Integration"

    id: Mapped[str] = mapped_column(primary_key=True)
    type: Mapped[str] = mapped_column(Enum("googleSheets", "notion", "airtable", "slack", name="IntegrationType"))
    config: Mapped[dict[str, Any]] = mapped_column(JSONB)
    workspace_id: Mapped[str] = mapped_column("workspaceId", ForeignKey("Workspace.id", ondelete="CASCADE", onupdate="CASCADE", name="Integration_workspaceId_fkey"))

    __table_args__ = (
        Index("Integration_workspaceId_idx", "workspaceId"),
        Index("Integration_type_workspaceId_key", "type", "workspaceId", unique=True),
    )

    workspace: Mapped["Workspace"] = relationship(back_populates="integrations")

class DataMigration(Base):
    __tablename__ = "DataMigration"

    id: Mapped[str] = mapped_column(primary_key=True)
    started_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    finished_at: Mapped[Optional[datetime]]
    name: Mapped[str]
    status: Mapped[str] = mapped_column(Enum("pending", "applied", "failed", name="DataMigrationStatus"))

    __table_args__ = (
        Index("DataMigration_name_key", "name", unique=True),
    )

class Workspace(Base):
    __tablename__ = "Workspace"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)
    name: Mapped[str]
    legacy_environment_id: Mapped[Optional[str]] = mapped_column("legacyEnvironmentId")
    organization_id: Mapped[str] = mapped_column("organizationId", ForeignKey("Organization.id", ondelete="CASCADE", onupdate="CASCADE", name="Workspace_organizationId_fkey"))
    styling: Mapped[dict[str, Any]] = mapped_column(JSONB, default=lambda: {"allowStyleOverwrite": True}, server_default=text("'{\"allowStyleOverwrite\":true}'"))
    config: Mapped[dict[str, Any]] = mapped_column(JSONB, default=lambda: {}, server_default=text("'{}'"))
    recontact_days: Mapped[int] = mapped_column("recontactDays", default=7, server_default=text("7"))
    link_survey_branding: Mapped[bool] = mapped_column("linkSurveyBranding", default=True, server_default=text("true"))
    in_app_survey_branding: Mapped[bool] = mapped_column("inAppSurveyBranding", default=True, server_default=text("true"))
    placement: Mapped[str] = mapped_column(Enum("bottomLeft", "bottomRight", "topLeft", "topRight", "center", name="WidgetPlacement"), default="bottomRight", server_default=text("'bottomRight'"))
    click_outside_close: Mapped[bool] = mapped_column("clickOutsideClose", default=True, server_default=text("true"))
    overlay: Mapped[str] = mapped_column(Enum("none", "light", "dark", name="SurveyOverlay"), default="none", server_default=text("'none'"))
    overlay_color: Mapped[Optional[str]] = mapped_column("overlayColor")
    overlay_opacity: Mapped[Optional[int]] = mapped_column("overlayOpacity")
    logo: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    app_setup_completed: Mapped[bool] = mapped_column("appSetupCompleted", default=False, server_default=text("false"))
    custom_head_scripts: Mapped[Optional[str]] = mapped_column("customHeadScripts")
    custom_css: Mapped[Optional[dict[str, Any]]] = mapped_column("customCss", JSONB(none_as_null=True))
    custom_css_previous: Mapped[Optional[dict[str, Any]]] = mapped_column("customCssPrevious", JSONB(none_as_null=True))

    __table_args__ = (
        Index("Workspace_legacyEnvironmentId_key", "legacyEnvironmentId", unique=True),
        Index("Workspace_organizationId_name_key", "organizationId", "name", unique=True),
    )

    organization: Mapped["Organization"] = relationship(back_populates="workspaces")
    languages: Mapped[list["Language"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="workspace")
    embedded_data: Mapped[list["EmbeddedData"]] = relationship(cascade="all, delete", passive_deletes=True, overlaps="embedded_data,survey", back_populates="workspace")
    workspace_teams: Mapped[list["WorkspaceTeam"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="workspace")
    feedback_directory_workspaces: Mapped[list["FeedbackDirectoryWorkspace"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="workspace")
    charts: Mapped[list["Chart"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="workspace")
    dashboards: Mapped[list["Dashboard"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="workspace")
    creatives: Mapped[list["Creative"]] = relationship(cascade="all, delete", passive_deletes=True, overlaps="creatives,survey", back_populates="workspace")
    attachment_exports: Mapped[list["AttachmentExport"]] = relationship(cascade="all, delete", passive_deletes=True, overlaps="attachment_exports,survey", back_populates="workspace")
    surveys: Mapped[list["Survey"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="workspace")
    contacts: Mapped[list["Contact"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="workspace")
    action_classes: Mapped[list["ActionClass"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="workspace")
    contact_attribute_keys: Mapped[list["ContactAttributeKey"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="workspace")
    webhooks: Mapped[list["Webhook"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="workspace")
    tags: Mapped[list["Tag"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="workspace")
    segments: Mapped[list["Segment"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="workspace")
    integrations: Mapped[list["Integration"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="workspace")
    api_key_workspaces: Mapped[list["ApiKeyWorkspace"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="workspace")
    feedback_sources: Mapped[list["FeedbackSource"]] = relationship(cascade="all, delete", passive_deletes=True, overlaps="feedback_directory_workspace,feedback_sources", back_populates="workspace")
    workflows: Mapped[list["Workflow"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="workspace")
    workflow_runs: Mapped[list["WorkflowRun"]] = relationship(cascade="all, delete", passive_deletes=True, overlaps="runs,workflow", back_populates="workspace")

class Organization(Base):
    __tablename__ = "Organization"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)
    name: Mapped[str]
    whitelabel: Mapped[dict[str, Any]] = mapped_column(JSONB, default=lambda: {}, server_default=text("'{}'"))
    is_aismart_tools_enabled: Mapped[bool] = mapped_column("isAISmartToolsEnabled", default=False, server_default=text("false"))
    display_time_zone: Mapped[Optional[str]] = mapped_column("displayTimeZone")

    memberships: Mapped[list["Membership"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organization")
    workspaces: Mapped[list["Workspace"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organization")
    invites: Mapped[list["Invite"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organization")
    teams: Mapped[list["Team"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organization")
    api_keys: Mapped[list["ApiKey"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organization")
    feedback_directories: Mapped[list["FeedbackDirectory"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organization")
    retention_policies: Mapped[list["RetentionPolicy"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organization")
    retention_exemptions: Mapped[list["RetentionExemption"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organization")
    retention_runs: Mapped[list["RetentionRun"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organization")
    retention_notices: Mapped[list["RetentionNotice"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organization")
    billing: Mapped[Optional["OrganizationBilling"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organization")

class OrganizationBilling(Base):
    __tablename__ = "OrganizationBilling"

    organization_id: Mapped[str] = mapped_column(ForeignKey("Organization.id", ondelete="CASCADE", onupdate="CASCADE", name="OrganizationBilling_organization_id_fkey"), primary_key=True)
    stripe_customer_id: Mapped[Optional[str]]
    limits: Mapped[dict[str, Any]] = mapped_column(JSONB)
    usage_cycle_anchor: Mapped[Optional[datetime]]
    stripe: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("OrganizationBilling_stripe_customer_id_key", "stripe_customer_id", unique=True),
    )

    organization: Mapped["Organization"] = relationship(back_populates="billing")

class Membership(Base):
    __tablename__ = "Membership"

    organization_id: Mapped[str] = mapped_column("organizationId", ForeignKey("Organization.id", ondelete="CASCADE", onupdate="CASCADE", name="Membership_organizationId_fkey"), primary_key=True)
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="Membership_userId_fkey"), primary_key=True)
    accepted: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    role: Mapped[str] = mapped_column(Enum("owner", "manager", "member", "billing", name="OrganizationRole"), default="member", server_default=text("'member'"))

    __table_args__ = (
        Index("Membership_organizationId_idx", "organizationId"),
    )

    organization: Mapped["Organization"] = relationship(back_populates="memberships")
    user: Mapped["User"] = relationship(back_populates="memberships")

class Invite(Base):
    __tablename__ = "Invite"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    email: Mapped[str]
    name: Mapped[Optional[str]]
    organization_id: Mapped[str] = mapped_column("organizationId", ForeignKey("Organization.id", ondelete="CASCADE", onupdate="CASCADE", name="Invite_organizationId_fkey"))
    creator_id: Mapped[str] = mapped_column("creatorId", ForeignKey("User.id", ondelete="RESTRICT", onupdate="CASCADE", name="Invite_creatorId_fkey"))
    acceptor_id: Mapped[Optional[str]] = mapped_column("acceptorId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="Invite_acceptorId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    expires_at: Mapped[datetime] = mapped_column("expiresAt")
    role: Mapped[str] = mapped_column(Enum("owner", "manager", "member", "billing", name="OrganizationRole"), default="member", server_default=text("'member'"))
    team_ids: Mapped[list[str]] = mapped_column("teamIds", ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))

    __table_args__ = (
        Index("Invite_email_organizationId_idx", "email", "organizationId"),
        Index("Invite_organizationId_idx", "organizationId"),
    )

    organization: Mapped["Organization"] = relationship(back_populates="invites")
    creator: Mapped["User"] = relationship(foreign_keys=[creator_id], back_populates="invites_created")
    acceptor: Mapped[Optional["User"]] = relationship(foreign_keys=[acceptor_id], back_populates="invites_accepted")

class ApiKey(Base):
    __tablename__ = "ApiKey"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    created_by: Mapped[Optional[str]] = mapped_column("createdBy")
    last_used_at: Mapped[Optional[datetime]] = mapped_column("lastUsedAt")
    label: Mapped[str]
    hashed_key: Mapped[str] = mapped_column("hashedKey")
    lookup_hash: Mapped[Optional[str]] = mapped_column("lookupHash")
    organization_id: Mapped[str] = mapped_column("organizationId", ForeignKey("Organization.id", ondelete="CASCADE", onupdate="CASCADE", name="ApiKey_organizationId_fkey"))
    organization_access: Mapped[dict[str, Any]] = mapped_column("organizationAccess", JSONB, default=lambda: {}, server_default=text("'{}'"))

    __table_args__ = (
        Index("ApiKey_lookupHash_key", "lookupHash", unique=True),
        Index("ApiKey_organizationId_idx", "organizationId"),
    )

    organization: Mapped["Organization"] = relationship(back_populates="api_keys")
    api_key_workspaces: Mapped[list["ApiKeyWorkspace"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="api_key")

class ApiKeyWorkspace(Base):
    __tablename__ = "ApiKeyWorkspace"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    api_key_id: Mapped[str] = mapped_column("apiKeyId", ForeignKey("ApiKey.id", ondelete="CASCADE", onupdate="CASCADE", name="ApiKeyWorkspace_apiKeyId_fkey"))
    workspace_id: Mapped[str] = mapped_column("workspaceId", ForeignKey("Workspace.id", ondelete="CASCADE", onupdate="CASCADE", name="ApiKeyWorkspace_workspaceId_fkey"))
    permission: Mapped[str] = mapped_column(Enum("read", "write", "manage", name="ApiKeyPermission"))

    __table_args__ = (
        Index("ApiKeyWorkspace_workspaceId_idx", "workspaceId"),
        Index("ApiKeyWorkspace_apiKeyId_workspaceId_key", "apiKeyId", "workspaceId", unique=True),
    )

    api_key: Mapped["ApiKey"] = relationship(back_populates="api_key_workspaces")
    workspace: Mapped["Workspace"] = relationship(back_populates="api_key_workspaces")

class Account(Base):
    __tablename__ = "Account"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="Account_userId_fkey"))
    type: Mapped[Optional[str]]
    provider: Mapped[str]
    provider_account_id: Mapped[str] = mapped_column("providerAccountId")
    access_token: Mapped[Optional[str]]
    refresh_token: Mapped[Optional[str]]
    expires_at: Mapped[Optional[int]]
    ext_expires_in: Mapped[Optional[int]]
    token_type: Mapped[Optional[str]]
    scope: Mapped[Optional[str]]
    id_token: Mapped[Optional[str]]
    session_state: Mapped[Optional[str]]
    password: Mapped[Optional[str]]
    access_token_expires_at: Mapped[Optional[datetime]] = mapped_column("accessTokenExpiresAt")
    refresh_token_expires_at: Mapped[Optional[datetime]] = mapped_column("refreshTokenExpiresAt")
    issuer: Mapped[Optional[str]]

    __table_args__ = (
        Index("Account_userId_idx", "userId"),
        Index("Account_provider_providerAccountId_key", "provider", "providerAccountId", unique=True),
        Index("Account_issuer_providerAccountId_key", "issuer", "providerAccountId", unique=True),
    )

    user: Mapped[Optional["User"]] = relationship(back_populates="accounts")

class Session(Base):
    __tablename__ = "Session"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)
    session_token: Mapped[str] = mapped_column("sessionToken")
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="Session_userId_fkey"))
    expires: Mapped[datetime]
    ip_address: Mapped[Optional[str]] = mapped_column("ipAddress")
    user_agent: Mapped[Optional[str]] = mapped_column("userAgent")

    __table_args__ = (
        Index("Session_sessionToken_key", "sessionToken", unique=True),
        Index("Session_userId_idx", "userId"),
    )

    user: Mapped["User"] = relationship(back_populates="sessions")
    oauth_access_tokens: Mapped[list["OauthAccessToken"]] = relationship(back_populates="session")
    oauth_refresh_tokens: Mapped[list["OauthRefreshToken"]] = relationship(back_populates="session")

class VerificationToken(Base):
    __tablename__ = "VerificationToken"

    identifier: Mapped[str] = mapped_column()
    token: Mapped[str] = mapped_column()
    expires: Mapped[datetime]

    __table_args__ = (
        Index("VerificationToken_identifier_token_key", "identifier", "token", unique=True),
    )

    __mapper_args__ = {"primary_key": [identifier, token]}

class TwoFactor(Base):
    __tablename__ = "TwoFactor"

    id: Mapped[str] = mapped_column(primary_key=True)
    secret: Mapped[str]
    backup_codes: Mapped[str] = mapped_column("backupCodes")
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="TwoFactor_userId_fkey"))
    verified: Mapped[bool] = mapped_column(default=True, server_default=text("true"))
    failed_verification_count: Mapped[int] = mapped_column("failedVerificationCount", default=0, server_default=text("0"))
    locked_until: Mapped[Optional[datetime]] = mapped_column("lockedUntil")

    __table_args__ = (
        Index("TwoFactor_userId_key", "userId", unique=True),
    )

    user: Mapped["User"] = relationship(back_populates="two_factor")

class Jwks(Base):
    __tablename__ = "jwks"

    id: Mapped[str] = mapped_column(primary_key=True)
    public_key: Mapped[str] = mapped_column("publicKey")
    private_key: Mapped[str] = mapped_column("privateKey")
    created_at: Mapped[datetime] = mapped_column("createdAt")
    expires_at: Mapped[Optional[datetime]] = mapped_column("expiresAt")
    alg: Mapped[Optional[str]]
    crv: Mapped[Optional[str]]

class OauthClient(Base):
    __tablename__ = "oauthClient"

    id: Mapped[str] = mapped_column(primary_key=True)
    client_id: Mapped[str] = mapped_column("clientId")
    client_secret: Mapped[Optional[str]] = mapped_column("clientSecret")
    disabled: Mapped[Optional[bool]] = mapped_column(default=False, server_default=text("false"))
    skip_consent: Mapped[Optional[bool]] = mapped_column("skipConsent")
    enable_end_session: Mapped[Optional[bool]] = mapped_column("enableEndSession")
    subject_type: Mapped[Optional[str]] = mapped_column("subjectType")
    scopes: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    user_id: Mapped[Optional[str]] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="oauthClient_userId_fkey"))
    created_at: Mapped[Optional[datetime]] = mapped_column("createdAt")
    updated_at: Mapped[Optional[datetime]] = mapped_column("updatedAt")
    name: Mapped[Optional[str]]
    uri: Mapped[Optional[str]]
    icon: Mapped[Optional[str]]
    contacts: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    tos: Mapped[Optional[str]]
    policy: Mapped[Optional[str]]
    software_id: Mapped[Optional[str]] = mapped_column("softwareId")
    software_version: Mapped[Optional[str]] = mapped_column("softwareVersion")
    software_statement: Mapped[Optional[str]] = mapped_column("softwareStatement")
    redirect_uris: Mapped[list[str]] = mapped_column("redirectUris", ARRAY(Text), nullable=True)
    post_logout_redirect_uris: Mapped[list[str]] = mapped_column("postLogoutRedirectUris", ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    token_endpoint_auth_method: Mapped[Optional[str]] = mapped_column("tokenEndpointAuthMethod")
    grant_types: Mapped[list[str]] = mapped_column("grantTypes", ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    response_types: Mapped[list[str]] = mapped_column("responseTypes", ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    public: Mapped[Optional[bool]]
    type: Mapped[Optional[str]]
    require_pkce: Mapped[Optional[bool]] = mapped_column("requirePKCE")
    reference_id: Mapped[Optional[str]] = mapped_column("referenceId")
    metadata_: Mapped[Optional[dict[str, Any]]] = mapped_column("metadata", JSONB(none_as_null=True))
    client_discovery_id: Mapped[Optional[str]] = mapped_column("clientDiscoveryId")
    client_credentials_scopes: Mapped[list[str]] = mapped_column("clientCredentialsScopes", ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    backchannel_logout_uri: Mapped[Optional[str]] = mapped_column("backchannelLogoutUri")
    backchannel_logout_session_required: Mapped[Optional[bool]] = mapped_column("backchannelLogoutSessionRequired")
    application_type: Mapped[Optional[str]] = mapped_column("applicationType")
    jwks: Mapped[Optional[str]]
    jwks_uri: Mapped[Optional[str]] = mapped_column("jwksUri")
    dpop_bound_access_tokens: Mapped[Optional[bool]] = mapped_column("dpopBoundAccessTokens", default=False, server_default=text("false"))

    __table_args__ = (
        Index("oauthClient_clientId_key", "clientId", unique=True),
        Index("oauthClient_userId_idx", "userId"),
    )

    user: Mapped[Optional["User"]] = relationship(back_populates="oauth_clients")
    access_tokens: Mapped[list["OauthAccessToken"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="client")
    refresh_tokens: Mapped[list["OauthRefreshToken"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="client")
    consents: Mapped[list["OauthConsent"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="client")
    resources: Mapped[list["OauthClientResource"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="client")

class OauthAccessToken(Base):
    __tablename__ = "oauthAccessToken"

    id: Mapped[str] = mapped_column(primary_key=True)
    token: Mapped[str]
    client_id: Mapped[str] = mapped_column("clientId", ForeignKey("oauthClient.clientId", ondelete="CASCADE", onupdate="CASCADE", name="oauthAccessToken_clientId_fkey"))
    session_id: Mapped[Optional[str]] = mapped_column("sessionId", ForeignKey("Session.id", ondelete="SET NULL", onupdate="CASCADE", name="oauthAccessToken_sessionId_fkey"))
    user_id: Mapped[Optional[str]] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="oauthAccessToken_userId_fkey"))
    reference_id: Mapped[Optional[str]] = mapped_column("referenceId")
    refresh_id: Mapped[Optional[str]] = mapped_column("refreshId", ForeignKey("oauthRefreshToken.id", ondelete="CASCADE", onupdate="CASCADE", name="oauthAccessToken_refreshId_fkey"))
    expires_at: Mapped[datetime] = mapped_column("expiresAt")
    created_at: Mapped[datetime] = mapped_column("createdAt")
    scopes: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True)
    authorization_code_id: Mapped[Optional[str]] = mapped_column("authorizationCodeId")
    resources: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    requested_user_info_claims: Mapped[list[str]] = mapped_column("requestedUserInfoClaims", ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    revoked: Mapped[Optional[datetime]]
    confirmation: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))

    __table_args__ = (
        Index("oauthAccessToken_token_key", "token", unique=True),
        Index("oauthAccessToken_clientId_idx", "clientId"),
        Index("oauthAccessToken_sessionId_idx", "sessionId"),
        Index("oauthAccessToken_userId_idx", "userId"),
        Index("oauthAccessToken_refreshId_idx", "refreshId"),
        Index("oauthAccessToken_authorizationCodeId_idx", "authorizationCodeId"),
    )

    client: Mapped["OauthClient"] = relationship(back_populates="access_tokens")
    session: Mapped[Optional["Session"]] = relationship(back_populates="oauth_access_tokens")
    user: Mapped[Optional["User"]] = relationship(back_populates="oauth_access_tokens")
    refresh: Mapped[Optional["OauthRefreshToken"]] = relationship(back_populates="access_tokens")

class OauthRefreshToken(Base):
    __tablename__ = "oauthRefreshToken"

    id: Mapped[str] = mapped_column(primary_key=True)
    token: Mapped[str]
    client_id: Mapped[str] = mapped_column("clientId", ForeignKey("oauthClient.clientId", ondelete="CASCADE", onupdate="CASCADE", name="oauthRefreshToken_clientId_fkey"))
    session_id: Mapped[Optional[str]] = mapped_column("sessionId", ForeignKey("Session.id", ondelete="SET NULL", onupdate="CASCADE", name="oauthRefreshToken_sessionId_fkey"))
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="oauthRefreshToken_userId_fkey"))
    reference_id: Mapped[Optional[str]] = mapped_column("referenceId")
    expires_at: Mapped[datetime] = mapped_column("expiresAt")
    created_at: Mapped[datetime] = mapped_column("createdAt")
    revoked: Mapped[Optional[datetime]]
    auth_time: Mapped[Optional[datetime]] = mapped_column("authTime")
    scopes: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True)
    authorization_code_id: Mapped[Optional[str]] = mapped_column("authorizationCodeId")
    resources: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    requested_user_info_claims: Mapped[list[str]] = mapped_column("requestedUserInfoClaims", ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    rotated_at: Mapped[Optional[datetime]] = mapped_column("rotatedAt")
    rotation_replay_response: Mapped[Optional[str]] = mapped_column("rotationReplayResponse")
    rotation_replay_expires_at: Mapped[Optional[datetime]] = mapped_column("rotationReplayExpiresAt")
    confirmation: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))

    __table_args__ = (
        Index("oauthRefreshToken_token_key", "token", unique=True),
        Index("oauthRefreshToken_clientId_idx", "clientId"),
        Index("oauthRefreshToken_sessionId_idx", "sessionId"),
        Index("oauthRefreshToken_userId_idx", "userId"),
        Index("oauthRefreshToken_authorizationCodeId_idx", "authorizationCodeId"),
    )

    client: Mapped["OauthClient"] = relationship(back_populates="refresh_tokens")
    session: Mapped[Optional["Session"]] = relationship(back_populates="oauth_refresh_tokens")
    user: Mapped["User"] = relationship(back_populates="oauth_refresh_tokens")
    access_tokens: Mapped[list["OauthAccessToken"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="refresh")

class OauthConsent(Base):
    __tablename__ = "oauthConsent"

    id: Mapped[str] = mapped_column(primary_key=True)
    client_id: Mapped[str] = mapped_column("clientId", ForeignKey("oauthClient.clientId", ondelete="CASCADE", onupdate="CASCADE", name="oauthConsent_clientId_fkey"))
    user_id: Mapped[Optional[str]] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="oauthConsent_userId_fkey"))
    reference_id: Mapped[Optional[str]] = mapped_column("referenceId")
    scopes: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True)
    created_at: Mapped[datetime] = mapped_column("createdAt")
    updated_at: Mapped[datetime] = mapped_column("updatedAt")
    resources: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    requested_user_info_claims: Mapped[list[str]] = mapped_column("requestedUserInfoClaims", ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))

    __table_args__ = (
        Index("oauthConsent_clientId_idx", "clientId"),
        Index("oauthConsent_userId_idx", "userId"),
    )

    client: Mapped["OauthClient"] = relationship(back_populates="consents")
    user: Mapped[Optional["User"]] = relationship(back_populates="oauth_consents")

class OauthResource(Base):
    __tablename__ = "oauthResource"

    id: Mapped[str] = mapped_column(primary_key=True)
    identifier: Mapped[str]
    name: Mapped[str]
    access_token_ttl: Mapped[Optional[int]] = mapped_column("accessTokenTtl")
    refresh_token_ttl: Mapped[Optional[int]] = mapped_column("refreshTokenTtl")
    signing_algorithm: Mapped[Optional[str]] = mapped_column("signingAlgorithm")
    signing_key_id: Mapped[Optional[str]] = mapped_column("signingKeyId")
    allowed_scopes: Mapped[list[str]] = mapped_column("allowedScopes", ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    custom_claims: Mapped[Optional[dict[str, Any]]] = mapped_column("customClaims", JSONB(none_as_null=True))
    dpop_bound_access_tokens_required: Mapped[Optional[bool]] = mapped_column("dpopBoundAccessTokensRequired", default=False, server_default=text("false"))
    disabled: Mapped[Optional[bool]] = mapped_column(default=False, server_default=text("false"))
    created_at: Mapped[Optional[datetime]] = mapped_column("createdAt")
    updated_at: Mapped[Optional[datetime]] = mapped_column("updatedAt")
    policy_version: Mapped[Optional[int]] = mapped_column("policyVersion", default=1, server_default=text("1"))
    metadata_: Mapped[Optional[dict[str, Any]]] = mapped_column("metadata", JSONB(none_as_null=True))

    __table_args__ = (
        Index("oauthResource_identifier_key", "identifier", unique=True),
    )

    clients: Mapped[list["OauthClientResource"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="resource")

class OauthClientResource(Base):
    __tablename__ = "oauthClientResource"

    id: Mapped[str] = mapped_column(primary_key=True)
    client_id: Mapped[str] = mapped_column("clientId", ForeignKey("oauthClient.clientId", ondelete="CASCADE", onupdate="CASCADE", name="oauthClientResource_clientId_fkey"))
    resource_id: Mapped[str] = mapped_column("resourceId", ForeignKey("oauthResource.identifier", ondelete="CASCADE", onupdate="CASCADE", name="oauthClientResource_resourceId_fkey"))
    metadata_: Mapped[Optional[dict[str, Any]]] = mapped_column("metadata", JSONB(none_as_null=True))
    created_at: Mapped[Optional[datetime]] = mapped_column("createdAt")

    __table_args__ = (
        Index("oauthClientResource_resourceId_idx", "resourceId"),
        Index("oauthClientResource_clientId_resourceId_key", "clientId", "resourceId", unique=True),
    )

    client: Mapped["OauthClient"] = relationship(back_populates="resources")
    resource: Mapped["OauthResource"] = relationship(back_populates="clients")

class OauthClientAssertion(Base):
    __tablename__ = "oauthClientAssertion"

    id: Mapped[str] = mapped_column(primary_key=True)
    expires_at: Mapped[datetime] = mapped_column("expiresAt")

    __table_args__ = (
        Index("oauthClientAssertion_expiresAt_idx", "expiresAt"),
    )

class PasswordResetToken(Base):
    __tablename__ = "PasswordResetToken"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)
    token_hash: Mapped[str]
    expires_at: Mapped[datetime]
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="PasswordResetToken_userId_fkey"))

    __table_args__ = (
        Index("PasswordResetToken_token_hash_key", "token_hash", unique=True),
        Index("PasswordResetToken_userId_key", "userId", unique=True),
        Index("PasswordResetToken_expires_at_idx", "expires_at"),
    )

    user: Mapped["User"] = relationship(back_populates="password_reset_token")

class User(Base):
    __tablename__ = "User"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)
    name: Mapped[str]
    email: Mapped[str]
    email_verified: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    two_factor_secret: Mapped[Optional[str]] = mapped_column("twoFactorSecret")
    two_factor_enabled: Mapped[bool] = mapped_column("twoFactorEnabled", default=False, server_default=text("false"))
    backup_codes: Mapped[Optional[str]] = mapped_column("backupCodes")
    password: Mapped[Optional[str]]
    identity_provider: Mapped[str] = mapped_column("identityProvider", Enum("email", "github", "google", "azuread", "openid", "saml", name="IdentityProvider"), default="email", server_default=text("'email'"))
    identity_provider_account_id: Mapped[Optional[str]] = mapped_column("identityProviderAccountId")
    group_id: Mapped[Optional[str]] = mapped_column("groupId")
    notification_settings: Mapped[dict[str, Any]] = mapped_column("notificationSettings", JSONB, default=lambda: {}, server_default=text("'{}'"))
    locale: Mapped[str] = mapped_column(default="en-US", server_default=text("'en-US'"))
    last_login_at: Mapped[Optional[datetime]] = mapped_column("lastLoginAt")
    is_active: Mapped[bool] = mapped_column("isActive", default=True, server_default=text("true"))
    reactivated_at: Mapped[Optional[datetime]] = mapped_column("reactivatedAt")
    last_active_at: Mapped[Optional[datetime]] = mapped_column("lastActiveAt")
    is_bootstrap_admin: Mapped[Optional[str]] = mapped_column("isBootstrapAdmin", Enum("bootstrapAdmin", name="BootstrapAdminMarker"))

    __table_args__ = (
        Index("User_email_key", "email", unique=True),
        Index("User_isBootstrapAdmin_key", "isBootstrapAdmin", unique=True),
    )

    memberships: Mapped[list["Membership"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    accounts: Mapped[list["Account"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    sessions: Mapped[list["Session"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    oauth_clients: Mapped[list["OauthClient"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    oauth_access_tokens: Mapped[list["OauthAccessToken"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    oauth_refresh_tokens: Mapped[list["OauthRefreshToken"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    oauth_consents: Mapped[list["OauthConsent"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    invites_created: Mapped[list["Invite"]] = relationship(foreign_keys="Invite.creator_id", passive_deletes="all", back_populates="creator")
    invites_accepted: Mapped[list["Invite"]] = relationship(foreign_keys="Invite.acceptor_id", cascade="all, delete", passive_deletes=True, back_populates="acceptor")
    surveys: Mapped[list["Survey"]] = relationship(foreign_keys="Survey.created_by", back_populates="creator")
    owned_surveys: Mapped[list["Survey"]] = relationship(foreign_keys="Survey.owner_id", back_populates="owner")
    charts: Mapped[list["Chart"]] = relationship(back_populates="creator")
    dashboards: Mapped[list["Dashboard"]] = relationship(back_populates="creator")
    creatives: Mapped[list["Creative"]] = relationship(back_populates="creator")
    feedback_sources: Mapped[list["FeedbackSource"]] = relationship(back_populates="creator")
    team_users: Mapped[list["TeamUser"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    workflows: Mapped[list["Workflow"]] = relationship(back_populates="creator")
    workflow_versions_published: Mapped[list["WorkflowVersion"]] = relationship(back_populates="publisher")
    retention_policies_updated: Mapped[list["RetentionPolicy"]] = relationship(back_populates="updated_by")
    exemptions_created: Mapped[list["RetentionExemption"]] = relationship(foreign_keys="RetentionExemption.created_by_id", back_populates="created_by")
    exemptions_revoked: Mapped[list["RetentionExemption"]] = relationship(foreign_keys="RetentionExemption.revoked_by_id", back_populates="revoked_by")
    retention_notices: Mapped[list["RetentionNotice"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    attachment_exports: Mapped[list["AttachmentExport"]] = relationship(back_populates="requested_by")
    two_factor: Mapped[Optional["TwoFactor"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    password_reset_token: Mapped[Optional["PasswordResetToken"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")

class Segment(Base):
    __tablename__ = "Segment"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)
    title: Mapped[str]
    description: Mapped[Optional[str]]
    is_private: Mapped[bool] = mapped_column("isPrivate", default=True, server_default=text("true"))
    filters: Mapped[dict[str, Any]] = mapped_column(JSONB, default=lambda: [], server_default=text("'[]'"))
    workspace_id: Mapped[str] = mapped_column("workspaceId", ForeignKey("Workspace.id", ondelete="CASCADE", onupdate="CASCADE", name="Segment_workspaceId_fkey"))

    __table_args__ = (
        Index("Segment_workspaceId_idx", "workspaceId"),
        Index("Segment_workspaceId_title_key", "workspaceId", "title", unique=True),
    )

    workspace: Mapped["Workspace"] = relationship(back_populates="segments")
    surveys: Mapped[list["Survey"]] = relationship(back_populates="segment")

class Language(Base):
    __tablename__ = "Language"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)
    code: Mapped[str]
    alias: Mapped[Optional[str]]
    workspace_id: Mapped[str] = mapped_column("workspaceId", ForeignKey("Workspace.id", ondelete="CASCADE", onupdate="CASCADE", name="Language_workspaceId_fkey"))

    __table_args__ = (
        Index("Language_workspaceId_code_key", "workspaceId", "code", unique=True),
    )

    workspace: Mapped["Workspace"] = relationship(back_populates="languages")
    survey_languages: Mapped[list["SurveyLanguage"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="language")
    creative_languages: Mapped[list["CreativeLanguage"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="language")

class SurveyLanguage(Base):
    __tablename__ = "SurveyLanguage"

    language_id: Mapped[str] = mapped_column("languageId", ForeignKey("Language.id", ondelete="CASCADE", onupdate="CASCADE", name="SurveyLanguage_languageId_fkey"), primary_key=True)
    survey_id: Mapped[str] = mapped_column("surveyId", ForeignKey("Survey.id", ondelete="CASCADE", onupdate="CASCADE", name="SurveyLanguage_surveyId_fkey"), primary_key=True)
    default: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    enabled: Mapped[bool] = mapped_column(default=True, server_default=text("true"))

    __table_args__ = (
        Index("SurveyLanguage_surveyId_idx", "surveyId"),
    )

    language: Mapped["Language"] = relationship(back_populates="survey_languages")
    survey: Mapped["Survey"] = relationship(back_populates="languages")

class EmbeddedData(Base):
    __tablename__ = "EmbeddedData"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)
    key: Mapped[Optional[str]]
    name: Mapped[str]
    description: Mapped[Optional[str]]
    source: Mapped[str] = mapped_column(Enum("computed", "ingested", "reserved", name="EmbeddedDataSource"))
    data_type: Mapped[str] = mapped_column("dataType", Enum("string", "number", "boolean", "date", name="EmbeddedDataType"), default="string", server_default=text("'string'"))
    default_value: Mapped[Optional[dict[str, Any]]] = mapped_column("defaultValue", JSONB(none_as_null=True))
    locked: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    survey_id: Mapped[Optional[str]] = mapped_column("surveyId")
    workspace_id: Mapped[str] = mapped_column("workspaceId", ForeignKey("Workspace.id", ondelete="CASCADE", onupdate="CASCADE", name="EmbeddedData_workspaceId_fkey"))

    __table_args__ = (
        Index("EmbeddedData_surveyId_idx", "surveyId"),
        Index("EmbeddedData_workspaceId_key_key", "workspaceId", "key", unique=True),
        Index("EmbeddedData_id_workspaceId_key", "id", "workspaceId", unique=True),
        ForeignKeyConstraint(["surveyId", "workspaceId"], ["Survey.id", "Survey.workspaceId"], ondelete="CASCADE", onupdate="CASCADE", name="EmbeddedData_surveyId_workspaceId_fkey"),
    )

    survey: Mapped[Optional["Survey"]] = relationship(overlaps="embedded_data,workspace", back_populates="embedded_data")
    workspace: Mapped["Workspace"] = relationship(overlaps="embedded_data,survey", back_populates="embedded_data")
    survey_links: Mapped[list["SurveyEmbeddedData"]] = relationship(cascade="all, delete", passive_deletes=True, overlaps="embedded_data_links,survey", back_populates="embedded_data")

class SurveyEmbeddedData(Base):
    __tablename__ = "SurveyEmbeddedData"

    id: Mapped[str] = mapped_column(primary_key=True)
    workspace_id: Mapped[str] = mapped_column("workspaceId")
    survey_id: Mapped[str] = mapped_column("surveyId")
    embedded_data_id: Mapped[str] = mapped_column("embeddedDataId")
    storage_key: Mapped[str] = mapped_column("storageKey")
    order: Mapped[int]

    __table_args__ = (
        Index("SurveyEmbeddedData_embeddedDataId_idx", "embeddedDataId"),
        Index("SurveyEmbeddedData_surveyId_order_idx", "surveyId", "order"),
        Index("SurveyEmbeddedData_surveyId_embeddedDataId_key", "surveyId", "embeddedDataId", unique=True),
        Index("SurveyEmbeddedData_surveyId_storageKey_key", "surveyId", "storageKey", unique=True),
        ForeignKeyConstraint(["surveyId", "workspaceId"], ["Survey.id", "Survey.workspaceId"], ondelete="CASCADE", onupdate="CASCADE", name="SurveyEmbeddedData_surveyId_workspaceId_fkey"),
        ForeignKeyConstraint(["embeddedDataId", "workspaceId"], ["EmbeddedData.id", "EmbeddedData.workspaceId"], ondelete="CASCADE", onupdate="CASCADE", name="SurveyEmbeddedData_embeddedDataId_workspaceId_fkey"),
    )

    survey: Mapped["Survey"] = relationship(overlaps="embedded_data,survey_links", back_populates="embedded_data_links")
    embedded_data: Mapped["EmbeddedData"] = relationship(overlaps="embedded_data_links,survey", back_populates="survey_links")

class Team(Base):
    __tablename__ = "Team"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)
    name: Mapped[str]
    organization_id: Mapped[str] = mapped_column("organizationId", ForeignKey("Organization.id", ondelete="CASCADE", onupdate="CASCADE", name="Team_organizationId_fkey"))

    __table_args__ = (
        Index("Team_organizationId_name_key", "organizationId", "name", unique=True),
    )

    organization: Mapped["Organization"] = relationship(back_populates="teams")
    team_users: Mapped[list["TeamUser"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="team")
    workspace_teams: Mapped[list["WorkspaceTeam"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="team")

class TeamUser(Base):
    __tablename__ = "TeamUser"

    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)
    team_id: Mapped[str] = mapped_column("teamId", ForeignKey("Team.id", ondelete="CASCADE", onupdate="CASCADE", name="TeamUser_teamId_fkey"), primary_key=True)
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="TeamUser_userId_fkey"), primary_key=True)
    role: Mapped[str] = mapped_column(Enum("admin", "contributor", name="TeamUserRole"))

    __table_args__ = (
        Index("TeamUser_userId_idx", "userId"),
    )

    team: Mapped["Team"] = relationship(back_populates="team_users")
    user: Mapped["User"] = relationship(back_populates="team_users")

class WorkspaceTeam(Base):
    __tablename__ = "WorkspaceTeam"

    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)
    workspace_id: Mapped[str] = mapped_column("workspaceId", ForeignKey("Workspace.id", ondelete="CASCADE", onupdate="CASCADE", name="WorkspaceTeam_workspaceId_fkey"), primary_key=True)
    team_id: Mapped[str] = mapped_column("teamId", ForeignKey("Team.id", ondelete="CASCADE", onupdate="CASCADE", name="WorkspaceTeam_teamId_fkey"), primary_key=True)
    permission: Mapped[str] = mapped_column(Enum("read", "readWrite", "manage", name="WorkspaceTeamPermission"), default="read", server_default=text("'read'"))

    __table_args__ = (
        Index("WorkspaceTeam_teamId_idx", "teamId"),
    )

    workspace: Mapped["Workspace"] = relationship(back_populates="workspace_teams")
    team: Mapped["Team"] = relationship(back_populates="workspace_teams")

class Chart(Base):
    __tablename__ = "Chart"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)
    name: Mapped[str]
    type: Mapped[str] = mapped_column(Enum("area", "bar", "pie", "big_number", "matrix", name="ChartType"))
    workspace_id: Mapped[str] = mapped_column("workspaceId", ForeignKey("Workspace.id", ondelete="CASCADE", onupdate="CASCADE", name="Chart_workspaceId_fkey"))
    query: Mapped[dict[str, Any]] = mapped_column(JSONB, default=lambda: {}, server_default=text("'{}'"))
    config: Mapped[dict[str, Any]] = mapped_column(JSONB, default=lambda: {}, server_default=text("'{}'"))
    created_by: Mapped[Optional[str]] = mapped_column("createdBy", ForeignKey("User.id", ondelete="SET NULL", onupdate="CASCADE", name="Chart_createdBy_fkey"))
    feedback_directory_id: Mapped[str] = mapped_column("feedbackDirectoryId", ForeignKey("FeedbackDirectory.id", ondelete="RESTRICT", onupdate="CASCADE", name="Chart_feedbackDirectoryId_fkey"))

    __table_args__ = (
        Index("Chart_workspaceId_created_at_idx", "workspaceId", "created_at"),
        Index("Chart_feedbackDirectoryId_idx", "feedbackDirectoryId"),
        Index("Chart_workspaceId_name_key", "workspaceId", "name", unique=True),
    )

    workspace: Mapped["Workspace"] = relationship(back_populates="charts")
    creator: Mapped[Optional["User"]] = relationship(back_populates="charts")
    feedback_directory: Mapped["FeedbackDirectory"] = relationship(back_populates="charts")
    widgets: Mapped[list["DashboardWidget"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="chart")

class Dashboard(Base):
    __tablename__ = "Dashboard"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)
    name: Mapped[str]
    workspace_id: Mapped[str] = mapped_column("workspaceId", ForeignKey("Workspace.id", ondelete="CASCADE", onupdate="CASCADE", name="Dashboard_workspaceId_fkey"))
    created_by: Mapped[Optional[str]] = mapped_column("createdBy", ForeignKey("User.id", ondelete="SET NULL", onupdate="CASCADE", name="Dashboard_createdBy_fkey"))

    __table_args__ = (
        Index("Dashboard_workspaceId_created_at_idx", "workspaceId", "created_at"),
        Index("Dashboard_workspaceId_name_key", "workspaceId", "name", unique=True),
    )

    workspace: Mapped["Workspace"] = relationship(back_populates="dashboards")
    creator: Mapped[Optional["User"]] = relationship(back_populates="dashboards")
    widgets: Mapped[list["DashboardWidget"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="dashboard")

class DashboardWidget(Base):
    __tablename__ = "DashboardWidget"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)
    dashboard_id: Mapped[str] = mapped_column("dashboardId", ForeignKey("Dashboard.id", ondelete="CASCADE", onupdate="CASCADE", name="DashboardWidget_dashboardId_fkey"))
    chart_id: Mapped[str] = mapped_column("chartId", ForeignKey("Chart.id", ondelete="CASCADE", onupdate="CASCADE", name="DashboardWidget_chartId_fkey"))
    layout: Mapped[dict[str, Any]] = mapped_column(JSONB, default=lambda: {"x": 0, "y": 0, "w": 4, "h": 3}, server_default=text("'{\"x\":0,\"y\":0,\"w\":4,\"h\":3}'"))
    order: Mapped[int] = mapped_column(default=0, server_default=text("0"))

    __table_args__ = (
        Index("DashboardWidget_dashboardId_order_idx", "dashboardId", "order"),
        Index("DashboardWidget_dashboardId_chartId_key", "dashboardId", "chartId", unique=True),
    )

    dashboard: Mapped["Dashboard"] = relationship(back_populates="widgets")
    chart: Mapped["Chart"] = relationship(back_populates="widgets")

class DeletionCleanup(Base):
    __tablename__ = "DeletionCleanup"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)
    kind: Mapped[str] = mapped_column(Enum("hubSurvey", "hubResponses", "storageFiles", "storageSurveyFolder", "hubResponseOutcome", name="DeletionCleanupKind"))
    organization_id: Mapped[str] = mapped_column("organizationId")
    workspace_id: Mapped[str] = mapped_column("workspaceId")
    survey_id: Mapped[str] = mapped_column("surveyId")
    tenant_ids: Mapped[list[str]] = mapped_column("tenantIds", ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    response_ids: Mapped[list[str]] = mapped_column("responseIds", ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    file_keys: Mapped[list[str]] = mapped_column("fileKeys", ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    attempts: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    next_attempt_at: Mapped[datetime] = mapped_column("nextAttemptAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    last_error: Mapped[Optional[str]] = mapped_column("lastError")

    __table_args__ = (
        Index("DeletionCleanup_nextAttemptAt_idx", "nextAttemptAt"),
        Index("DeletionCleanup_organizationId_created_at_idx", "organizationId", "created_at"),
    )

class AttachmentExport(Base):
    __tablename__ = "AttachmentExport"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)
    survey_id: Mapped[str] = mapped_column("surveyId")
    workspace_id: Mapped[str] = mapped_column("workspaceId", ForeignKey("Workspace.id", ondelete="CASCADE", onupdate="CASCADE", name="AttachmentExport_workspaceId_fkey"))
    requested_by_id: Mapped[Optional[str]] = mapped_column("requestedById", ForeignKey("User.id", ondelete="SET NULL", onupdate="CASCADE", name="AttachmentExport_requestedById_fkey"))
    status: Mapped[str] = mapped_column(Enum("queued", "running", "completed", "failed", "expired", name="AttachmentExportStatus"), default="queued", server_default=text("'queued'"))
    filter_criteria: Mapped[Optional[dict[str, Any]]] = mapped_column("filterCriteria", JSONB(none_as_null=True))
    time_zone: Mapped[str] = mapped_column("timeZone")
    dispatch_count: Mapped[int] = mapped_column("dispatchCount", default=0, server_default=text("0"))
    file_count: Mapped[int] = mapped_column("fileCount")
    response_count: Mapped[int] = mapped_column("responseCount")
    included_file_count: Mapped[Optional[int]] = mapped_column("includedFileCount")
    total_bytes: Mapped[Optional[int]] = mapped_column("totalBytes", BigInteger)
    truncated: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    storage_key: Mapped[Optional[str]] = mapped_column("storageKey")
    error_code: Mapped[Optional[str]] = mapped_column("errorCode")
    started_at: Mapped[Optional[datetime]] = mapped_column("startedAt")
    finished_at: Mapped[Optional[datetime]] = mapped_column("finishedAt")
    expires_at: Mapped[Optional[datetime]] = mapped_column("expiresAt")

    __table_args__ = (
        Index("AttachmentExport_surveyId_requestedById_created_at_idx", "surveyId", "requestedById", "created_at"),
        Index("AttachmentExport_status_created_at_idx", "status", "created_at"),
        Index("AttachmentExport_expiresAt_idx", "expiresAt"),
        Index("AttachmentExport_workspaceId_idx", "workspaceId"),
        ForeignKeyConstraint(["surveyId", "workspaceId"], ["Survey.id", "Survey.workspaceId"], ondelete="CASCADE", onupdate="CASCADE", name="AttachmentExport_surveyId_workspaceId_fkey"),
    )

    survey: Mapped["Survey"] = relationship(overlaps="attachment_exports,workspace", back_populates="attachment_exports")
    workspace: Mapped["Workspace"] = relationship(overlaps="attachment_exports,survey", back_populates="attachment_exports")
    requested_by: Mapped[Optional["User"]] = relationship(back_populates="attachment_exports")

class FeedbackSource(Base):
    __tablename__ = "FeedbackSource"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)
    name: Mapped[str]
    type: Mapped[str] = mapped_column(Enum("formbricks_survey", "csv", name="FeedbackSourceType"))
    status: Mapped[str] = mapped_column(Enum("active", "paused", "error", name="FeedbackSourceStatus"), default="active", server_default=text("'active'"))
    import_mode: Mapped[str] = mapped_column(Enum("completedOnly", "all", name="FeedbackSourceImportMode"), default="completedOnly", server_default=text("'completedOnly'"))
    element_scope: Mapped[str] = mapped_column("elementScope", Enum("all", "specific", name="FeedbackSourceElementScope"), default="specific", server_default=text("'specific'"))
    workspace_id: Mapped[str] = mapped_column("workspaceId", ForeignKey("Workspace.id", ondelete="CASCADE", onupdate="CASCADE", name="FeedbackSource_workspaceId_fkey"))
    feedback_directory_id: Mapped[str] = mapped_column("feedbackDirectoryId", ForeignKey("FeedbackDirectory.id", ondelete="CASCADE", onupdate="CASCADE", name="FeedbackSource_feedbackDirectoryId_fkey"))
    last_sync_at: Mapped[Optional[datetime]]
    created_by: Mapped[Optional[str]] = mapped_column(ForeignKey("User.id", ondelete="SET NULL", onupdate="CASCADE", name="FeedbackSource_created_by_fkey"))

    __table_args__ = (
        Index("FeedbackSource_type_idx", "type"),
        Index("FeedbackSource_feedbackDirectoryId_workspaceId_idx", "feedbackDirectoryId", "workspaceId"),
        Index("FeedbackSource_id_workspaceId_key", "id", "workspaceId", unique=True),
        Index("FeedbackSource_workspaceId_name_key", "workspaceId", "name", unique=True),
        ForeignKeyConstraint(["feedbackDirectoryId", "workspaceId"], ["FeedbackDirectoryWorkspace.feedbackDirectoryId", "FeedbackDirectoryWorkspace.workspaceId"], ondelete="NO ACTION", onupdate="CASCADE", name="FeedbackSource_feedbackDirectoryId_workspaceId_fkey"),
    )

    workspace: Mapped["Workspace"] = relationship(overlaps="feedback_directory_workspace,feedback_sources", back_populates="feedback_sources")
    feedback_directory: Mapped["FeedbackDirectory"] = relationship(overlaps="feedback_directory_workspace,feedback_sources", back_populates="feedback_sources")
    creator: Mapped[Optional["User"]] = relationship(back_populates="feedback_sources")
    feedback_directory_workspace: Mapped["FeedbackDirectoryWorkspace"] = relationship(overlaps="feedback_directory,feedback_sources,workspace", back_populates="feedback_sources")
    formbricks_mappings: Mapped[list["FeedbackSourceFormbricksMapping"]] = relationship(cascade="all, delete", passive_deletes=True, overlaps="feedback_source_mappings,survey", back_populates="feedback_source")
    field_mappings: Mapped[list["FeedbackSourceFieldMapping"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="feedback_source")

class FeedbackSourceFormbricksMapping(Base):
    __tablename__ = "FeedbackSourceFormbricksMapping"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    feedback_source_id: Mapped[str]
    workspace_id: Mapped[str] = mapped_column("workspaceId")
    survey_id: Mapped[str] = mapped_column("surveyId")
    element_id: Mapped[str] = mapped_column("elementId")
    hub_field_type: Mapped[str] = mapped_column("hubFieldType", Enum("text", "categorical", "nps", "csat", "ces", "rating", "number", "boolean", "date", name="HubFieldType"))
    custom_field_label: Mapped[Optional[str]]

    __table_args__ = (
        Index("FeedbackSourceFormbricksMapping_workspaceId_surveyId_idx", "workspaceId", "surveyId"),
        Index("FeedbackSourceFormbricksMapping_surveyId_idx", "surveyId"),
        Index("FeedbackSourceFormbricksMapping_workspaceId_feedbackSourceId_su", "workspaceId", "feedback_source_id", "surveyId", "elementId", unique=True),
        ForeignKeyConstraint(["feedback_source_id", "workspaceId"], ["FeedbackSource.id", "FeedbackSource.workspaceId"], ondelete="CASCADE", onupdate="CASCADE", name="FeedbackSourceFormbricksMapping_feedback_source_id_workspa_fkey"),
        ForeignKeyConstraint(["surveyId", "workspaceId"], ["Survey.id", "Survey.workspaceId"], ondelete="CASCADE", onupdate="CASCADE", name="FeedbackSourceFormbricksMapping_surveyId_workspaceId_fkey"),
    )

    feedback_source: Mapped["FeedbackSource"] = relationship(overlaps="feedback_source_mappings,survey", back_populates="formbricks_mappings")
    survey: Mapped["Survey"] = relationship(overlaps="feedback_source,formbricks_mappings", back_populates="feedback_source_mappings")

class FeedbackSourceFieldMapping(Base):
    __tablename__ = "FeedbackSourceFieldMapping"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    feedback_source_id: Mapped[str]
    workspace_id: Mapped[str] = mapped_column("workspaceId")
    source_field_id: Mapped[str]
    target_field_id: Mapped[str]
    static_value: Mapped[Optional[str]]

    __table_args__ = (
        Index("FeedbackSourceFieldMapping_workspaceId_feedbackSourceId_sourceF", "workspaceId", "feedback_source_id", "source_field_id", "target_field_id", unique=True),
        ForeignKeyConstraint(["feedback_source_id", "workspaceId"], ["FeedbackSource.id", "FeedbackSource.workspaceId"], ondelete="CASCADE", onupdate="CASCADE", name="FeedbackSourceFieldMapping_feedback_source_id_workspaceId_fkey"),
    )

    feedback_source: Mapped["FeedbackSource"] = relationship(back_populates="field_mappings")

class FeedbackDirectory(Base):
    __tablename__ = "FeedbackDirectory"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)
    name: Mapped[str]
    is_archived: Mapped[bool] = mapped_column("isArchived", default=False, server_default=text("false"))
    organization_id: Mapped[str] = mapped_column("organizationId", ForeignKey("Organization.id", ondelete="CASCADE", onupdate="CASCADE", name="FeedbackDirectory_organizationId_fkey"))

    __table_args__ = (
        Index("FeedbackDirectory_organizationId_name_key", "organizationId", "name", unique=True),
    )

    organization: Mapped["Organization"] = relationship(back_populates="feedback_directories")
    workspaces: Mapped[list["FeedbackDirectoryWorkspace"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="feedback_directory")
    feedback_sources: Mapped[list["FeedbackSource"]] = relationship(cascade="all, delete", passive_deletes=True, overlaps="feedback_directory_workspace,feedback_sources", back_populates="feedback_directory")
    charts: Mapped[list["Chart"]] = relationship(passive_deletes="all", back_populates="feedback_directory")

class FeedbackDirectoryWorkspace(Base):
    __tablename__ = "FeedbackDirectoryWorkspace"

    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)
    feedback_directory_id: Mapped[str] = mapped_column("feedbackDirectoryId", ForeignKey("FeedbackDirectory.id", ondelete="CASCADE", onupdate="CASCADE", name="FeedbackDirectoryWorkspace_feedbackDirectoryId_fkey"), primary_key=True)
    workspace_id: Mapped[str] = mapped_column("workspaceId", ForeignKey("Workspace.id", ondelete="CASCADE", onupdate="CASCADE", name="FeedbackDirectoryWorkspace_workspaceId_fkey"), primary_key=True)

    __table_args__ = (
        Index("FeedbackDirectoryWorkspace_workspaceId_idx", "workspaceId"),
    )

    feedback_directory: Mapped["FeedbackDirectory"] = relationship(back_populates="workspaces")
    workspace: Mapped["Workspace"] = relationship(back_populates="feedback_directory_workspaces")
    feedback_sources: Mapped[list["FeedbackSource"]] = relationship(passive_deletes="all", overlaps="feedback_directory,feedback_sources,workspace", back_populates="feedback_directory_workspace")

class AuthzedProjectionScopeState(Base):
    __tablename__ = "AuthzedProjectionScopeState"

    scope: Mapped[str] = mapped_column(primary_key=True)
    ready_at: Mapped[Optional[datetime]] = mapped_column("readyAt")
    ready_by: Mapped[Optional[str]] = mapped_column("readyBy")
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

class AuthzedProjectionOutbox(Base):
    __tablename__ = "AuthzedProjectionOutbox"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    target_type: Mapped[str] = mapped_column("targetType")
    primary_id: Mapped[str] = mapped_column("primaryId")
    secondary_id: Mapped[Optional[str]] = mapped_column("secondaryId")
    is_revocation: Mapped[bool] = mapped_column("isRevocation", default=False, server_default=text("false"))
    attempts: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    permanent_failures: Mapped[int] = mapped_column("permanentFailures", default=0, server_default=text("0"))
    available_at: Mapped[datetime] = mapped_column("availableAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    leased_at: Mapped[Optional[datetime]] = mapped_column("leasedAt")
    lease_expires_at: Mapped[Optional[datetime]] = mapped_column("leaseExpiresAt")
    lease_owner: Mapped[Optional[str]] = mapped_column("leaseOwner")
    processed_at: Mapped[Optional[datetime]] = mapped_column("processedAt")
    dead_lettered_at: Mapped[Optional[datetime]] = mapped_column("deadLetteredAt")
    last_attempt_at: Mapped[Optional[datetime]] = mapped_column("lastAttemptAt")
    last_error_code: Mapped[Optional[str]] = mapped_column("lastErrorCode")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

class OnlineUpgradeState(Base):
    __tablename__ = "OnlineUpgradeState"

    release: Mapped[str] = mapped_column(primary_key=True)
    target_image: Mapped[str] = mapped_column("targetImage")
    phase: Mapped[str]
    completed_phases: Mapped[list[str]] = mapped_column("completedPhases", ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    survey_cursor: Mapped[Optional[str]] = mapped_column("surveyCursor")
    survey_count: Mapped[int] = mapped_column("surveyCount", BigInteger, default=0, server_default=text("0"))
    baseline_completed_at: Mapped[Optional[datetime]] = mapped_column("baselineCompletedAt")
    forward_only_at: Mapped[Optional[datetime]] = mapped_column("forwardOnlyAt")
    queue_paused_at: Mapped[Optional[datetime]] = mapped_column("queuePausedAt")
    queue_pause_deadline_at: Mapped[Optional[datetime]] = mapped_column("queuePauseDeadlineAt")
    queue_resumed_at: Mapped[Optional[datetime]] = mapped_column("queueResumedAt")
    activated_at: Mapped[Optional[datetime]] = mapped_column("activatedAt")
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

class RetentionPolicy(Base):
    __tablename__ = "RetentionPolicy"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)
    organization_id: Mapped[str] = mapped_column("organizationId", ForeignKey("Organization.id", ondelete="CASCADE", onupdate="CASCADE", name="RetentionPolicy_organizationId_fkey"))
    entity: Mapped[str] = mapped_column(Enum("responses", "surveys", "members", name="RetentionEntity"))
    enabled: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    enabled_at: Mapped[Optional[datetime]] = mapped_column("enabledAt")
    warn_days: Mapped[int] = mapped_column("warnDays", default=60, server_default=text("60"))
    period_days: Mapped[int] = mapped_column("periodDays")
    conditions: Mapped[list[str]] = mapped_column(ARRAY(Enum("noResponse", "noChange", "createdBefore", name="RetentionSurveyCondition")), nullable=True, default=lambda: [], server_default=text("ARRAY[]::\"RetentionSurveyCondition\"[]"))
    deferred_at: Mapped[Optional[datetime]] = mapped_column("deferredAt")
    updated_by_id: Mapped[Optional[str]] = mapped_column("updatedById", ForeignKey("User.id", ondelete="SET NULL", onupdate="CASCADE", name="RetentionPolicy_updatedById_fkey"))

    __table_args__ = (
        Index("RetentionPolicy_updatedById_idx", "updatedById"),
        Index("RetentionPolicy_organizationId_entity_key", "organizationId", "entity", unique=True),
    )

    organization: Mapped["Organization"] = relationship(back_populates="retention_policies")
    updated_by: Mapped[Optional["User"]] = relationship(back_populates="retention_policies_updated")

class RetentionExemption(Base):
    __tablename__ = "RetentionExemption"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    organization_id: Mapped[str] = mapped_column("organizationId", ForeignKey("Organization.id", ondelete="CASCADE", onupdate="CASCADE", name="RetentionExemption_organizationId_fkey"))
    entity: Mapped[str] = mapped_column(Enum("responses", "surveys", "members", name="RetentionEntity"))
    survey_id: Mapped[Optional[str]] = mapped_column("surveyId", ForeignKey("Survey.id", ondelete="CASCADE", onupdate="CASCADE", name="RetentionExemption_surveyId_fkey"))
    until: Mapped[datetime]
    source: Mapped[str] = mapped_column(Enum("manual", "restore", name="RetentionExemptionSource"), default="manual", server_default=text("'manual'"))
    reason: Mapped[Optional[str]]
    created_by_id: Mapped[Optional[str]] = mapped_column("createdById", ForeignKey("User.id", ondelete="SET NULL", onupdate="CASCADE", name="RetentionExemption_createdById_fkey"))
    revoked_at: Mapped[Optional[datetime]] = mapped_column("revokedAt")
    revoked_by_id: Mapped[Optional[str]] = mapped_column("revokedById", ForeignKey("User.id", ondelete="SET NULL", onupdate="CASCADE", name="RetentionExemption_revokedById_fkey"))

    __table_args__ = (
        Index("RetentionExemption_organizationId_created_at_id_idx", "organizationId", "created_at", "id"),
        Index("RetentionExemption_surveyId_idx", "surveyId"),
        Index("RetentionExemption_createdById_idx", "createdById"),
        Index("RetentionExemption_revokedById_idx", "revokedById"),
    )

    organization: Mapped["Organization"] = relationship(back_populates="retention_exemptions")
    survey: Mapped[Optional["Survey"]] = relationship(back_populates="retention_exemptions")
    created_by: Mapped[Optional["User"]] = relationship(foreign_keys=[created_by_id], back_populates="exemptions_created")
    revoked_by: Mapped[Optional["User"]] = relationship(foreign_keys=[revoked_by_id], back_populates="exemptions_revoked")

class RetentionRun(Base):
    __tablename__ = "RetentionRun"

    id: Mapped[str] = mapped_column(primary_key=True)
    organization_id: Mapped[str] = mapped_column("organizationId", ForeignKey("Organization.id", ondelete="CASCADE", onupdate="CASCADE", name="RetentionRun_organizationId_fkey"))
    entity: Mapped[str] = mapped_column(Enum("responses", "surveys", "members", name="RetentionEntity"))
    kind: Mapped[str] = mapped_column(Enum("sweep", "purge", name="RetentionRunKind"), default="sweep", server_default=text("'sweep'"))
    started_at: Mapped[datetime] = mapped_column("startedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    finished_at: Mapped[Optional[datetime]] = mapped_column("finishedAt")
    notified_count: Mapped[int] = mapped_column("notifiedCount", default=0, server_default=text("0"))
    archived_count: Mapped[int] = mapped_column("archivedCount", default=0, server_default=text("0"))
    deleted_count: Mapped[int] = mapped_column("deletedCount", default=0, server_default=text("0"))
    skipped_count: Mapped[int] = mapped_column("skippedCount", default=0, server_default=text("0"))
    has_changes: Mapped[bool] = mapped_column("hasChanges", default=False, server_default=text("false"))
    scan_cursor: Mapped[Optional[str]] = mapped_column("scanCursor")

    __table_args__ = (
        Index("RetentionRun_organizationId_startedAt_id_idx", "organizationId", "startedAt", "id"),
    )

    organization: Mapped["Organization"] = relationship(back_populates="retention_runs")
    items: Mapped[list["RetentionRunItem"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="run")

class RetentionRunItem(Base):
    __tablename__ = "RetentionRunItem"

    id: Mapped[str] = mapped_column(primary_key=True)
    run_id: Mapped[str] = mapped_column("runId", ForeignKey("RetentionRun.id", ondelete="CASCADE", onupdate="CASCADE", name="RetentionRunItem_runId_fkey"))
    target_type: Mapped[str] = mapped_column("targetType", Enum("survey", "user", name="RetentionTargetType"))
    target_id: Mapped[str] = mapped_column("targetId")
    target_name: Mapped[Optional[str]] = mapped_column("targetName")
    action: Mapped[str] = mapped_column(Enum("notified", "archived", "deactivated", "deleted", "skipped", name="RetentionRunItemAction"))
    count: Mapped[int] = mapped_column(default=1, server_default=text("1"))
    recipient: Mapped[Optional[str]]
    skip_reason: Mapped[Optional[str]] = mapped_column("skipReason", Enum("exempt", "lastOwner", "otherOrganization", "noRecipient", name="RetentionSkipReason"))

    __table_args__ = (
        Index("RetentionRunItem_runId_id_idx", "runId", "id"),
        Index("RetentionRunItem_targetId_idx", "targetId"),
    )

    run: Mapped["RetentionRun"] = relationship(back_populates="items")

class RetentionNotice(Base):
    __tablename__ = "RetentionNotice"

    id: Mapped[str] = mapped_column(primary_key=True)
    organization_id: Mapped[str] = mapped_column("organizationId", ForeignKey("Organization.id", ondelete="CASCADE", onupdate="CASCADE", name="RetentionNotice_organizationId_fkey"))
    entity: Mapped[str] = mapped_column(Enum("responses", "surveys", "members", name="RetentionEntity"))
    survey_id: Mapped[Optional[str]] = mapped_column("surveyId", ForeignKey("Survey.id", ondelete="CASCADE", onupdate="CASCADE", name="RetentionNotice_surveyId_fkey"))
    user_id: Mapped[Optional[str]] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="RetentionNotice_userId_fkey"))
    sent_at: Mapped[datetime] = mapped_column("sentAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    delivered_at: Mapped[Optional[datetime]] = mapped_column("deliveredAt")
    email_sent: Mapped[bool] = mapped_column("emailSent", default=False, server_default=text("false"))
    claim_token: Mapped[Optional[str]] = mapped_column("claimToken")
    clock_at: Mapped[Optional[datetime]] = mapped_column("clockAt")

    __table_args__ = (
        Index("RetentionNotice_organizationId_idx", "organizationId"),
        Index("RetentionNotice_surveyId_entity_key", "surveyId", "entity", unique=True),
        Index("RetentionNotice_userId_organizationId_entity_key", "userId", "organizationId", "entity", unique=True),
    )

    organization: Mapped["Organization"] = relationship(back_populates="retention_notices")
    survey: Mapped[Optional["Survey"]] = relationship(back_populates="retention_notices")
    user: Mapped[Optional["User"]] = relationship(back_populates="retention_notices")

class RetentionSurveyRestore(Base):
    __tablename__ = "RetentionSurveyRestore"

    survey_id: Mapped[str] = mapped_column("surveyId", ForeignKey("Survey.id", ondelete="CASCADE", onupdate="CASCADE", name="RetentionSurveyRestore_surveyId_fkey"), primary_key=True)
    restored_at: Mapped[datetime] = mapped_column("restoredAt")

    survey: Mapped["Survey"] = relationship(back_populates="retention_restore")

class Workflow(Base):
    __tablename__ = "Workflow"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)
    name: Mapped[str]
    description: Mapped[Optional[str]]
    status: Mapped[str] = mapped_column(Enum("draft", "enabled", "disabled", "archived", name="WorkflowStatus"), default="draft", server_default=text("'draft'"))
    workspace_id: Mapped[str] = mapped_column("workspaceId", ForeignKey("Workspace.id", ondelete="CASCADE", onupdate="CASCADE", name="Workflow_workspaceId_fkey"))
    created_by: Mapped[Optional[str]] = mapped_column("createdBy", ForeignKey("User.id", ondelete="SET NULL", onupdate="CASCADE", name="Workflow_createdBy_fkey"))
    definition: Mapped[dict[str, Any]] = mapped_column(JSONB)

    __table_args__ = (
        Index("Workflow_workspaceId_status_idx", "workspaceId", "status"),
        Index("Workflow_workspaceId_updated_at_idx", "workspaceId", "updated_at"),
        Index("Workflow_id_workspaceId_key", "id", "workspaceId", unique=True),
    )

    workspace: Mapped["Workspace"] = relationship(back_populates="workflows")
    creator: Mapped[Optional["User"]] = relationship(back_populates="workflows")
    versions: Mapped[list["WorkflowVersion"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="workflow")
    runs: Mapped[list["WorkflowRun"]] = relationship(cascade="all, delete", passive_deletes=True, overlaps="workflow_runs,workspace", back_populates="workflow")

class WorkflowVersion(Base):
    __tablename__ = "WorkflowVersion"

    id: Mapped[str] = mapped_column(primary_key=True)
    workflow_id: Mapped[str] = mapped_column("workflowId")
    workspace_id: Mapped[str] = mapped_column("workspaceId")
    version: Mapped[int]
    definition: Mapped[dict[str, Any]] = mapped_column(JSONB)
    published_at: Mapped[datetime] = mapped_column("publishedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    published_by: Mapped[Optional[str]] = mapped_column("publishedBy", ForeignKey("User.id", ondelete="SET NULL", onupdate="CASCADE", name="WorkflowVersion_publishedBy_fkey"))

    __table_args__ = (
        Index("WorkflowVersion_workflowId_workspaceId_idx", "workflowId", "workspaceId"),
        Index("WorkflowVersion_publishedBy_idx", "publishedBy"),
        Index("WorkflowVersion_workflowId_version_key", "workflowId", "version", unique=True),
        ForeignKeyConstraint(["workflowId", "workspaceId"], ["Workflow.id", "Workflow.workspaceId"], ondelete="CASCADE", onupdate="CASCADE", name="WorkflowVersion_workflowId_workspaceId_fkey"),
    )

    workflow: Mapped["Workflow"] = relationship(back_populates="versions")
    publisher: Mapped[Optional["User"]] = relationship(back_populates="workflow_versions_published")
    runs: Mapped[list["WorkflowRun"]] = relationship(back_populates="workflow_version")

class WorkflowRun(Base):
    __tablename__ = "WorkflowRun"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)
    workflow_id: Mapped[str] = mapped_column("workflowId")
    workspace_id: Mapped[str] = mapped_column("workspaceId", ForeignKey("Workspace.id", ondelete="CASCADE", onupdate="CASCADE", name="WorkflowRun_workspaceId_fkey"))
    workflow_version_id: Mapped[Optional[str]] = mapped_column("workflowVersionId", ForeignKey("WorkflowVersion.id", ondelete="SET NULL", onupdate="CASCADE", name="WorkflowRun_workflowVersionId_fkey"))
    response_id: Mapped[Optional[str]] = mapped_column("responseId", ForeignKey("Response.id", ondelete="CASCADE", onupdate="CASCADE", name="WorkflowRun_responseId_fkey"))
    status: Mapped[str] = mapped_column(Enum("queued", "running", "completed", "failed", "canceled", name="WorkflowRunStatus"), default="queued", server_default=text("'queued'"))
    trigger_type: Mapped[str] = mapped_column("triggerType")
    survey_id: Mapped[Optional[str]] = mapped_column("surveyId")
    is_dry_run: Mapped[bool] = mapped_column("isDryRun", default=False, server_default=text("false"))
    idempotency_key: Mapped[Optional[str]] = mapped_column("idempotencyKey")
    attempt: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    next_attempt_at: Mapped[Optional[datetime]] = mapped_column("nextAttemptAt")
    last_error_at: Mapped[Optional[datetime]] = mapped_column("lastErrorAt")
    trigger_payload: Mapped[dict[str, Any]] = mapped_column("triggerPayload", JSONB)
    data: Mapped[dict[str, Any]] = mapped_column(JSONB, default=lambda: {}, server_default=text("'{}'"))
    error: Mapped[Optional[str]]
    dispatched_at: Mapped[Optional[datetime]] = mapped_column("dispatchedAt")
    started_at: Mapped[Optional[datetime]] = mapped_column("startedAt")
    finished_at: Mapped[Optional[datetime]] = mapped_column("finishedAt")

    __table_args__ = (
        Index("WorkflowRun_workspaceId_created_at_idx", "workspaceId", "created_at"),
        Index("WorkflowRun_workflowId_workspaceId_created_at_idx", "workflowId", "workspaceId", "created_at"),
        Index("WorkflowRun_status_created_at_idx", "status", "created_at"),
        Index("WorkflowRun_responseId_idx", "responseId"),
        Index("WorkflowRun_isDryRun_idx", "isDryRun"),
        Index("WorkflowRun_nextAttemptAt_idx", "nextAttemptAt"),
        Index("WorkflowRun_workflowId_idempotencyKey_key", "workflowId", "idempotencyKey", unique=True),
        ForeignKeyConstraint(["workflowId", "workspaceId"], ["Workflow.id", "Workflow.workspaceId"], ondelete="CASCADE", onupdate="CASCADE", name="WorkflowRun_workflowId_workspaceId_fkey"),
    )

    workflow: Mapped["Workflow"] = relationship(overlaps="workflow_runs,workspace", back_populates="runs")
    workspace: Mapped["Workspace"] = relationship(overlaps="runs,workflow", back_populates="workflow_runs")
    workflow_version: Mapped[Optional["WorkflowVersion"]] = relationship(back_populates="runs")
    response: Mapped[Optional["Response"]] = relationship(back_populates="workflow_runs")
    logs: Mapped[list["WorkflowRunLog"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="run")

class WorkflowRunLog(Base):
    __tablename__ = "WorkflowRunLog"

    id: Mapped[str] = mapped_column(primary_key=True)
    run_id: Mapped[str] = mapped_column("runId", ForeignKey("WorkflowRun.id", ondelete="CASCADE", onupdate="CASCADE", name="WorkflowRunLog_runId_fkey"))
    sequence: Mapped[int]
    step_id: Mapped[str] = mapped_column("stepId")
    step_type: Mapped[str] = mapped_column("stepType")
    status: Mapped[str] = mapped_column(Enum("pending", "running", "succeeded", "failed", "skipped", name="WorkflowRunLogStatus"), default="pending", server_default=text("'pending'"))
    input: Mapped[dict[str, Any]] = mapped_column(JSONB, default=lambda: {}, server_default=text("'{}'"))
    output: Mapped[dict[str, Any]] = mapped_column(JSONB, default=lambda: {}, server_default=text("'{}'"))
    error: Mapped[Optional[str]]
    started_at: Mapped[Optional[datetime]] = mapped_column("startedAt")
    finished_at: Mapped[Optional[datetime]] = mapped_column("finishedAt")

    __table_args__ = (
        Index("WorkflowRunLog_runId_sequence_idx", "runId", "sequence"),
        Index("WorkflowRunLog_runId_stepId_key", "runId", "stepId", unique=True),
    )

    run: Mapped["WorkflowRun"] = relationship(back_populates="logs")
