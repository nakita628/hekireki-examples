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


class ConversationVisibility(models.TextChoices):
    PRIVATE = "PRIVATE"
    PUBLIC_LINK = "PUBLIC_LINK"
    PUBLIC_GROUP = "PUBLIC_GROUP"
    PUBLIC_DOCUMENT = "PUBLIC_DOCUMENT"
    PUBLIC_DATAROOM = "PUBLIC_DATAROOM"


class ParticipantRole(models.TextChoices):
    OWNER = "OWNER"
    PARTICIPANT = "PARTICIPANT"


class FaqVisibility(models.TextChoices):
    PUBLIC_DATAROOM = "PUBLIC_DATAROOM"
    PUBLIC_LINK = "PUBLIC_LINK"
    PUBLIC_DOCUMENT = "PUBLIC_DOCUMENT"


class FaqStatus(models.TextChoices):
    DRAFT = "DRAFT"
    PUBLISHED = "PUBLISHED"
    ARCHIVED = "ARCHIVED"


class ItemType(models.TextChoices):
    DATAROOM_DOCUMENT = "DATAROOM_DOCUMENT"
    DATAROOM_FOLDER = "DATAROOM_FOLDER"


class DefaultPermissionStrategy(models.TextChoices):
    INHERIT_FROM_PARENT = "INHERIT_FROM_PARENT"
    ASK_EVERY_TIME = "ASK_EVERY_TIME"
    HIDDEN_BY_DEFAULT = "HIDDEN_BY_DEFAULT"


class RootItemAccess(models.TextChoices):
    VIEW_ONLY = "VIEW_ONLY"
    VIEW_AND_DOWNLOAD = "VIEW_AND_DOWNLOAD"
    HIDDEN = "HIDDEN"


class DocumentStorageType(models.TextChoices):
    S3_PATH = "S3_PATH"
    VERCEL_BLOB = "VERCEL_BLOB"


class LinkType(models.TextChoices):
    DOCUMENT_LINK = "DOCUMENT_LINK"
    DATAROOM_LINK = "DATAROOM_LINK"
    WORKFLOW_LINK = "WORKFLOW_LINK"


class LinkAudienceType(models.TextChoices):
    GENERAL = "GENERAL"
    GROUP = "GROUP"
    TEAM = "TEAM"


class CustomFieldType(models.TextChoices):
    SHORT_TEXT = "SHORT_TEXT"
    LONG_TEXT = "LONG_TEXT"
    NUMBER = "NUMBER"
    PHONE_NUMBER = "PHONE_NUMBER"
    URL = "URL"
    CHECKBOX = "CHECKBOX"
    SELECT = "SELECT"
    MULTI_SELECT = "MULTI_SELECT"


class ReferralTrack(models.TextChoices):
    AFFILIATE = "AFFILIATE"
    PARTNER_DISCOUNT = "PARTNER_DISCOUNT"


class ViewType(models.TextChoices):
    DOCUMENT_VIEW = "DOCUMENT_VIEW"
    DATAROOM_VIEW = "DATAROOM_VIEW"


class DownloadType(models.TextChoices):
    SINGLE = "SINGLE"
    BULK = "BULK"
    FOLDER = "FOLDER"


class EmailType(models.TextChoices):
    FIRST_DAY_DOMAIN_REMINDER_EMAIL = "FIRST_DAY_DOMAIN_REMINDER_EMAIL"
    FIRST_DOMAIN_INVALID_EMAIL = "FIRST_DOMAIN_INVALID_EMAIL"
    SECOND_DOMAIN_INVALID_EMAIL = "SECOND_DOMAIN_INVALID_EMAIL"
    FIRST_TRIAL_END_REMINDER_EMAIL = "FIRST_TRIAL_END_REMINDER_EMAIL"
    FINAL_TRIAL_END_REMINDER_EMAIL = "FINAL_TRIAL_END_REMINDER_EMAIL"


class TagType(models.TextChoices):
    LINK_TAG = "LINK_TAG"
    DOCUMENT_TAG = "DOCUMENT_TAG"
    DATAROOM_TAG = "DATAROOM_TAG"


class InvitationStatus(models.TextChoices):
    SENT = "SENT"
    FAILED = "FAILED"
    BOUNCED = "BOUNCED"


class Role(models.TextChoices):
    ADMIN = "ADMIN"
    MANAGER = "MANAGER"
    MEMBER = "MEMBER"
    DATAROOM_MEMBER = "DATAROOM_MEMBER"


class WorkflowStepType(models.TextChoices):
    ROUTER = "ROUTER"


class ExecutionStatus(models.TextChoices):
    PENDING = "PENDING"
    IN_PROGRESS = "IN_PROGRESS"
    COMPLETED = "COMPLETED"
    FAILED = "FAILED"
    BLOCKED = "BLOCKED"


class DocumentAnnotation(models.Model):
    id = models.TextField(primary_key=True, default=None)
    title = models.TextField()
    content = models.JSONField()
    pages = ArrayField(models.IntegerField())
    document = models.ForeignKey("Document", on_delete=models.CASCADE, related_name="annotations", db_column="documentId", db_index=False)
    team = models.ForeignKey("Team", on_delete=models.CASCADE, related_name="annotations", db_column="teamId", db_index=False)
    created_by = models.ForeignKey("User", on_delete=models.CASCADE, related_name="created_annotations", db_column="createdById", db_index=False)
    is_visible = models.BooleanField(db_column="isVisible", default=True)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "DocumentAnnotation"
        indexes = [
            models.Index(fields=["document"]),
            models.Index(fields=["team"]),
            models.Index(fields=["created_by"]),
        ]


class AnnotationImage(models.Model):
    id = models.TextField(primary_key=True, default=None)
    filename = models.TextField()
    url = models.TextField()
    size = models.IntegerField(null=True)
    mime_type = models.TextField(null=True, db_column="mimeType")
    annotation = models.ForeignKey("DocumentAnnotation", on_delete=models.CASCADE, related_name="images", db_column="annotationId", db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "AnnotationImage"
        indexes = [
            models.Index(fields=["annotation"]),
        ]


class Brand(models.Model):
    id = models.TextField(primary_key=True, default=None)
    logo = models.TextField(null=True)
    hide_logo = models.BooleanField(db_column="hideLogo", default=False)
    banner = models.TextField(null=True)
    brand_color = models.TextField(null=True, db_column="brandColor")
    accent_color = models.TextField(null=True, db_column="accentColor")
    accent_button_color = models.TextField(null=True, db_column="accentButtonColor")
    apply_accent_color_to_dataroom_view = models.BooleanField(db_column="applyAccentColorToDataroomView", default=False)
    welcome_message = models.TextField(null=True, db_column="welcomeMessage")
    cta_label = models.TextField(null=True, db_column="ctaLabel")
    cta_url = models.TextField(null=True, db_column="ctaUrl")
    privacy_policy_url = models.TextField(null=True, db_column="privacyPolicyUrl")
    card_layout = models.TextField(db_column="cardLayout", default="LIST")
    show_folder_tree = models.BooleanField(db_column="showFolderTree", default=True)
    viewer_layout_preset = models.TextField(db_column="viewerLayoutPreset", default="STANDARD")
    viewer_header_style = models.TextField(db_column="viewerHeaderStyle", default="DEFAULT")
    hide_folder_icons_in_main = models.BooleanField(db_column="hideFolderIconsInMain", default=False)
    custom_link_preview_enabled = models.BooleanField(db_column="customLinkPreviewEnabled", default=False)
    link_preview_title = models.TextField(null=True, db_column="linkPreviewTitle")
    link_preview_description = models.TextField(null=True, db_column="linkPreviewDescription")
    link_preview_image = models.TextField(null=True, db_column="linkPreviewImage")
    link_preview_favicon = models.TextField(null=True, db_column="linkPreviewFavicon")
    default_language = models.TextField(db_column="defaultLanguage", default="en")
    name = models.TextField(default="Default")
    team = models.ForeignKey("Team", on_delete=models.CASCADE, related_name="brands", db_column="teamId", db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Brand"
        constraints = [
            models.UniqueConstraint(fields=["team", "name"], name="Brand_teamId_name_key"),
        ]
        indexes = [
            models.Index(fields=["team"]),
        ]


class Conversation(models.Model):
    id = models.TextField(primary_key=True, default=None)
    title = models.TextField(null=True)
    is_enabled = models.BooleanField(db_column="isEnabled", default=True)
    visibility_mode = models.TextField(choices=ConversationVisibility.choices, db_column="visibilityMode", default=ConversationVisibility.PRIVATE)
    dataroom = models.ForeignKey("Dataroom", on_delete=models.CASCADE, related_name="conversations", db_column="dataroomId", db_index=False)
    dataroom_document = models.ForeignKey("DataroomDocument", on_delete=models.SET_NULL, related_name="conversations", db_column="dataroomDocumentId", null=True, db_index=False)
    document_version_number = models.IntegerField(null=True, db_column="documentVersionNumber")
    document_page_number = models.IntegerField(null=True, db_column="documentPageNumber")
    link = models.ForeignKey("Link", on_delete=models.SET_NULL, related_name="conversations", db_column="linkId", null=True, db_index=False)
    viewer_group = models.ForeignKey("ViewerGroup", on_delete=models.SET_NULL, related_name="conversations", db_column="viewerGroupId", null=True, db_index=False)
    question = models.ForeignKey("DataroomQuestion", on_delete=models.CASCADE, related_name="conversations", db_column="questionId", null=True, db_index=False)
    origin = models.TextField(default="visitor")
    category = models.TextField(null=True)
    priority = models.TextField(null=True)
    recipient_viewer = models.ForeignKey("Viewer", on_delete=models.SET_NULL, related_name="recipient_conversations", db_column="recipientViewerId", null=True, db_index=False)
    initial_view = models.ForeignKey("View", on_delete=models.SET_NULL, related_name="initial_conversations", db_column="initialViewId", null=True, db_index=False)
    team = models.ForeignKey("Team", on_delete=models.CASCADE, related_name="conversations", db_column="teamId", db_index=False)
    last_message_at = UtcDateTimeField(null=True, db_column="lastMessageAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Conversation"
        constraints = [
            models.UniqueConstraint(fields=["question", "recipient_viewer"], name="Conversation_questionId_recipientViewerId_key"),
        ]
        indexes = [
            models.Index(fields=["dataroom"]),
            models.Index(fields=["dataroom_document"]),
            models.Index(fields=["link"]),
            models.Index(fields=["team"]),
            models.Index(fields=["viewer_group"]),
            models.Index(fields=["initial_view"]),
            models.Index(fields=["question"]),
            models.Index(fields=["recipient_viewer"]),
        ]


class DataroomQuestion(models.Model):
    id = models.TextField(primary_key=True, default=None)
    content = models.TextField()
    category = models.TextField(null=True)
    priority = models.TextField(null=True)
    status = models.TextField(default="unanswered")
    order_index = models.IntegerField(null=True, db_column="orderIndex")
    import_batch_id = models.TextField(null=True, db_column="importBatchId")
    dataroom = models.ForeignKey("Dataroom", on_delete=models.CASCADE, related_name="questions", db_column="dataroomId", db_index=False)
    team = models.ForeignKey("Team", on_delete=models.CASCADE, related_name="dataroom_questions", db_column="teamId", db_index=False)
    created_by_user = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="created_dataroom_questions", db_column="createdByUserId", null=True, db_index=False)
    dataroom_document = models.ForeignKey("DataroomDocument", on_delete=models.SET_NULL, related_name="questions", db_column="dataroomDocumentId", null=True, db_index=False)
    document_page_number = models.IntegerField(null=True, db_column="documentPageNumber")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "DataroomQuestion"
        indexes = [
            models.Index(fields=["dataroom"]),
            models.Index(fields=["team"]),
            models.Index(fields=["status"]),
            models.Index(fields=["import_batch_id"]),
            models.Index(fields=["dataroom_document"]),
            models.Index(fields=["created_by_user"]),
        ]


class DataroomQuestionAssignment(models.Model):
    id = models.TextField(primary_key=True, default=None)
    question = models.ForeignKey("DataroomQuestion", on_delete=models.CASCADE, related_name="assignments", db_column="questionId", db_index=False)
    link = models.ForeignKey("Link", on_delete=models.CASCADE, related_name="question_assignments", db_column="linkId", null=True, db_index=False)
    group = models.ForeignKey("ViewerGroup", on_delete=models.CASCADE, related_name="question_assignments", db_column="groupId", null=True, db_index=False)
    viewer = models.ForeignKey("Viewer", on_delete=models.CASCADE, related_name="question_assignments", db_column="viewerId", null=True, db_index=False)
    email = models.TextField(null=True)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "DataroomQuestionAssignment"
        constraints = [
            models.UniqueConstraint(fields=["question", "link"], name="DataroomQuestionAssignment_questionId_linkId_key"),
            models.UniqueConstraint(fields=["question", "group"], name="DataroomQuestionAssignment_questionId_groupId_key"),
            models.UniqueConstraint(fields=["question", "viewer"], name="DataroomQuestionAssignment_questionId_viewerId_key"),
            models.UniqueConstraint(fields=["question", "email"], name="DataroomQuestionAssignment_questionId_email_key"),
        ]
        indexes = [
            models.Index(fields=["question"]),
            models.Index(fields=["link"]),
            models.Index(fields=["group"]),
            models.Index(fields=["viewer"]),
        ]


class ConversationParticipant(models.Model):
    id = models.TextField(primary_key=True, default=None)
    conversation = models.ForeignKey("Conversation", on_delete=models.CASCADE, related_name="participants", db_column="conversationId", db_index=False)
    role = models.TextField(choices=ParticipantRole.choices, default=ParticipantRole.PARTICIPANT)
    viewer = models.ForeignKey("Viewer", on_delete=models.SET_NULL, related_name="participated_conversations", db_column="viewerId", null=True, db_index=False)
    user = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="participated_conversations", db_column="userId", null=True, db_index=False)
    receive_notifications = models.BooleanField(db_column="receiveNotifications", default=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "ConversationParticipant"
        constraints = [
            models.UniqueConstraint(fields=["conversation", "viewer"], name="ConversationParticipant_conversationId_viewerId_key"),
            models.UniqueConstraint(fields=["conversation", "user"], name="ConversationParticipant_conversationId_userId_key"),
        ]
        indexes = [
            models.Index(fields=["conversation"]),
            models.Index(fields=["viewer"]),
            models.Index(fields=["user"]),
        ]


class Message(models.Model):
    id = models.TextField(primary_key=True, default=None)
    content = models.TextField()
    conversation = models.ForeignKey("Conversation", on_delete=models.CASCADE, related_name="messages", db_column="conversationId", db_index=False)
    user = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="messages", db_column="userId", null=True, db_index=False)
    viewer = models.ForeignKey("Viewer", on_delete=models.SET_NULL, related_name="messages", db_column="viewerId", null=True, db_index=False)
    view = models.ForeignKey("View", on_delete=models.SET_NULL, related_name="messages", db_column="viewId", null=True, db_index=False)
    is_read = models.BooleanField(db_column="isRead", default=False)
    is_internal_note = models.BooleanField(db_column="isInternalNote", default=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Message"
        indexes = [
            models.Index(fields=["conversation"]),
            models.Index(fields=["user"]),
            models.Index(fields=["viewer"]),
            models.Index(fields=["view"]),
        ]


class ConversationView(models.Model):
    id = models.TextField(primary_key=True, default=None)
    conversation = models.ForeignKey("Conversation", on_delete=models.CASCADE, related_name="views", db_column="conversationId", db_index=False)
    view = models.ForeignKey("View", on_delete=models.CASCADE, related_name="conversation_views", db_column="viewId", db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "ConversationView"
        constraints = [
            models.UniqueConstraint(fields=["conversation", "view"], name="ConversationView_conversationId_viewId_key"),
        ]
        indexes = [
            models.Index(fields=["conversation"]),
            models.Index(fields=["view"]),
        ]


class DataroomFaqItem(models.Model):
    id = models.TextField(primary_key=True, default=None)
    title = models.TextField(null=True)
    edited_question = models.TextField(db_column="editedQuestion")
    original_question = models.TextField(null=True, db_column="originalQuestion")
    answer = models.TextField()
    description = models.TextField(null=True)
    dataroom = models.ForeignKey("Dataroom", on_delete=models.CASCADE, related_name="faq_items", db_column="dataroomId", db_index=False)
    link = models.ForeignKey("Link", on_delete=models.SET_NULL, related_name="dataroom_faq_items", db_column="linkId", null=True, db_index=False)
    dataroom_document = models.ForeignKey("DataroomDocument", on_delete=models.SET_NULL, related_name="dataroom_faqs", db_column="dataroomDocumentId", null=True, db_index=False)
    source_conversation = models.ForeignKey("Conversation", on_delete=models.SET_NULL, related_name="faq_items", db_column="sourceConversationId", null=True, db_index=False)
    question_message = models.ForeignKey("Message", on_delete=models.SET_NULL, related_name="faq_as_question", db_column="questionMessageId", null=True, db_index=False)
    answer_message = models.ForeignKey("Message", on_delete=models.SET_NULL, related_name="faq_as_answer", db_column="answerMessageId", null=True, db_index=False)
    team = models.ForeignKey("Team", on_delete=models.CASCADE, related_name="dataroom_faq_items", db_column="teamId", db_index=False)
    published_by_user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="published_faq_items", db_column="publishedByUserId", db_index=False)
    visibility_mode = models.TextField(choices=FaqVisibility.choices, db_column="visibilityMode", default=FaqVisibility.PUBLIC_DATAROOM)
    status = models.TextField(choices=FaqStatus.choices, default=FaqStatus.PUBLISHED)
    is_anonymized = models.BooleanField(db_column="isAnonymized", default=True)
    view_count = models.IntegerField(db_column="viewCount", default=0)
    tags = ArrayField(models.TextField(), default=list)
    document_page_number = models.IntegerField(null=True, db_column="documentPageNumber")
    document_version_number = models.IntegerField(null=True, db_column="documentVersionNumber")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "DataroomFaqItem"
        indexes = [
            models.Index(fields=["dataroom"]),
            models.Index(fields=["link"]),
            models.Index(fields=["dataroom_document"]),
            models.Index(fields=["source_conversation"]),
            models.Index(fields=["team"]),
            models.Index(fields=["published_by_user"]),
            models.Index(fields=["status"]),
            models.Index(fields=["visibility_mode"]),
            models.Index(fields=["created_at"]),
        ]


class Dataroom(models.Model):
    id = models.TextField(primary_key=True, default=None)
    p_id = models.TextField(unique=True, db_column="pId")
    name = models.TextField()
    internal_name = models.TextField(null=True, db_column="internalName")
    description = models.TextField(null=True)
    team = models.ForeignKey("Team", on_delete=models.CASCADE, related_name="datarooms", db_column="teamId", db_index=False)
    base_brand = models.ForeignKey("Brand", on_delete=models.SET_NULL, related_name="datarooms", db_column="brandId", null=True, db_index=False)
    conversations_enabled = models.BooleanField(db_column="conversationsEnabled", default=False)
    agents_enabled = models.BooleanField(db_column="agentsEnabled", default=False)
    vector_store_id = models.TextField(null=True, db_column="vectorStoreId")
    request_list_enabled = models.BooleanField(db_column="requestListEnabled", default=False)
    enable_change_notifications = models.BooleanField(db_column="enableChangeNotifications", default=False)
    enable_visitor_upload_change_notifications = models.BooleanField(db_column="enableVisitorUploadChangeNotifications", default=False)
    default_permission_strategy = models.TextField(choices=DefaultPermissionStrategy.choices, db_column="defaultPermissionStrategy", default=DefaultPermissionStrategy.INHERIT_FROM_PARENT)
    default_show_banner = models.BooleanField(db_column="defaultShowBanner", default=True)
    default_group_permission_strategy = models.TextField(choices=DefaultPermissionStrategy.choices, db_column="defaultGroupPermissionStrategy", default=DefaultPermissionStrategy.INHERIT_FROM_PARENT)
    default_root_item_access = models.TextField(choices=RootItemAccess.choices, db_column="defaultRootItemAccess", default=RootItemAccess.VIEW_ONLY)
    default_group_root_item_access = models.TextField(choices=RootItemAccess.choices, db_column="defaultGroupRootItemAccess", default=RootItemAccess.VIEW_ONLY)
    allow_bulk_download = models.BooleanField(db_column="allowBulkDownload", default=True)
    show_last_updated = models.BooleanField(db_column="showLastUpdated", default=True)
    introduction_enabled = models.BooleanField(db_column="introductionEnabled", default=False)
    introduction_content = models.JSONField(null=True, db_column="introductionContent")
    is_frozen = models.BooleanField(db_column="isFrozen", default=False)
    frozen_at = UtcDateTimeField(null=True, db_column="frozenAt")
    frozen_by_user = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="frozen_datarooms", db_column="frozenBy", null=True, db_index=False)
    freeze_archive_url = models.TextField(null=True, db_column="freezeArchiveUrl")
    freeze_archive_hash = models.TextField(null=True, db_column="freezeArchiveHash")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Dataroom"
        indexes = [
            models.Index(fields=["team"]),
            models.Index(fields=["base_brand"]),
        ]


class TaskList(models.Model):
    id = models.TextField(primary_key=True, default=None)
    name = models.TextField()
    order_index = models.IntegerField(null=True, db_column="orderIndex")
    dataroom = models.ForeignKey("Dataroom", on_delete=models.CASCADE, related_name="task_lists", db_column="dataroomId", db_index=False)
    team = models.ForeignKey("Team", on_delete=models.CASCADE, related_name="task_lists", db_column="teamId", db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "TaskList"
        indexes = [
            models.Index(fields=["dataroom"]),
            models.Index(fields=["team"]),
            models.Index(fields=["dataroom", "order_index"]),
        ]


class Task(models.Model):
    id = models.TextField(primary_key=True, default=None)
    title = models.TextField()
    description = models.TextField(null=True)
    type = models.TextField(default="TODO")
    status = models.TextField(default="OPEN")
    category = models.TextField(null=True)
    due_date = UtcDateTimeField(null=True, db_column="dueDate")
    order_index = models.IntegerField(null=True, db_column="orderIndex")
    task_list = models.ForeignKey("TaskList", on_delete=models.CASCADE, related_name="tasks", db_column="taskListId", db_index=False)
    dataroom = models.ForeignKey("Dataroom", on_delete=models.CASCADE, related_name="tasks", db_column="dataroomId", db_index=False)
    team = models.ForeignKey("Team", on_delete=models.CASCADE, related_name="tasks", db_column="teamId", db_index=False)
    upload_folder = models.ForeignKey("DataroomFolder", on_delete=models.SET_NULL, related_name="upload_tasks", db_column="uploadFolderId", null=True, db_index=False)
    created_by_user = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="created_tasks", db_column="createdByUserId", null=True, db_index=False)
    last_activity_notified_at = UtcDateTimeField(null=True, db_column="lastActivityNotifiedAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Task"
        indexes = [
            models.Index(fields=["task_list"]),
            models.Index(fields=["dataroom"]),
            models.Index(fields=["team"]),
            models.Index(fields=["upload_folder"]),
            models.Index(fields=["task_list", "order_index"]),
        ]


class TaskAssignment(models.Model):
    id = models.TextField(primary_key=True, default=None)
    task = models.ForeignKey("Task", on_delete=models.CASCADE, related_name="assignments", db_column="taskId", db_index=False)
    viewer = models.ForeignKey("Viewer", on_delete=models.CASCADE, related_name="task_assignments", db_column="viewerId", null=True, db_index=False)
    group = models.ForeignKey("ViewerGroup", on_delete=models.CASCADE, related_name="task_assignments", db_column="groupId", null=True, db_index=False)
    link = models.ForeignKey("Link", on_delete=models.CASCADE, related_name="task_assignments", db_column="linkId", null=True, db_index=False)
    email = models.TextField(null=True)
    notified_at = UtcDateTimeField(null=True, db_column="notifiedAt")
    last_reminder_at = UtcDateTimeField(null=True, db_column="lastReminderAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "TaskAssignment"
        constraints = [
            models.UniqueConstraint(fields=["task", "viewer"], name="TaskAssignment_taskId_viewerId_key"),
            models.UniqueConstraint(fields=["task", "group"], name="TaskAssignment_taskId_groupId_key"),
            models.UniqueConstraint(fields=["task", "link"], name="TaskAssignment_taskId_linkId_key"),
            models.UniqueConstraint(fields=["task", "email"], name="TaskAssignment_taskId_email_key"),
        ]
        indexes = [
            models.Index(fields=["task"]),
            models.Index(fields=["viewer"]),
            models.Index(fields=["group"]),
            models.Index(fields=["link"]),
        ]


class TaskActivity(models.Model):
    id = models.TextField(primary_key=True, default=None)
    task = models.ForeignKey("Task", on_delete=models.CASCADE, related_name="activities", db_column="taskId", db_index=False)
    type = models.TextField()
    comment = models.TextField(null=True)
    from_status = models.TextField(null=True, db_column="fromStatus")
    to_status = models.TextField(null=True, db_column="toStatus")
    user = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="task_activities", db_column="userId", null=True, db_index=False)
    viewer = models.ForeignKey("Viewer", on_delete=models.SET_NULL, related_name="task_activities", db_column="viewerId", null=True, db_index=False)
    view = models.ForeignKey("View", on_delete=models.SET_NULL, related_name="task_activities", db_column="viewId", null=True, db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "TaskActivity"
        indexes = [
            models.Index(fields=["task"]),
            models.Index(fields=["user"]),
            models.Index(fields=["viewer"]),
            models.Index(fields=["view"]),
            models.Index(fields=["task", "created_at"]),
        ]


class DataroomDocument(models.Model):
    id = models.TextField(primary_key=True, default=None)
    dataroom = models.ForeignKey("Dataroom", on_delete=models.CASCADE, related_name="documents", db_column="dataroomId", db_index=False)
    document = models.ForeignKey("Document", on_delete=models.CASCADE, related_name="datarooms", db_column="documentId", db_index=False)
    folder = models.ForeignKey("DataroomFolder", on_delete=models.SET_NULL, related_name="documents", db_column="folderId", null=True, db_index=False)
    order_index = models.IntegerField(null=True, db_column="orderIndex")
    hierarchical_index = models.TextField(null=True, db_column="hierarchicalIndex")
    vector_store_file_id = models.TextField(null=True, db_column="vectorStoreFileId")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "DataroomDocument"
        constraints = [
            models.UniqueConstraint(fields=["dataroom", "document"], name="DataroomDocument_dataroomId_documentId_key"),
        ]
        indexes = [
            models.Index(fields=["folder"]),
            models.Index(fields=["dataroom", "folder", "order_index"]),
            models.Index(fields=["document"]),
        ]


class DataroomFolder(models.Model):
    id = models.TextField(primary_key=True, default=None)
    name = models.TextField()
    path = models.TextField()
    parent_folder = models.ForeignKey("self", on_delete=models.SET_NULL, related_name="child_folders", db_column="parentId", null=True, db_index=False)
    icon = models.TextField(null=True)
    color = models.TextField(null=True)
    dataroom = models.ForeignKey("Dataroom", on_delete=models.CASCADE, related_name="folders", db_column="dataroomId", db_index=False)
    order_index = models.IntegerField(null=True, db_column="orderIndex")
    hierarchical_index = models.TextField(null=True, db_column="hierarchicalIndex")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "DataroomFolder"
        constraints = [
            models.UniqueConstraint(fields=["dataroom", "path"], name="DataroomFolder_dataroomId_path_key"),
        ]
        indexes = [
            models.Index(fields=["parent_folder"]),
            models.Index(fields=["dataroom", "parent_folder", "order_index"]),
        ]


class DataroomBrand(models.Model):
    id = models.TextField(primary_key=True, default=None)
    logo = models.TextField(null=True)
    hide_logo = models.BooleanField(null=True, db_column="hideLogo")
    banner = models.TextField(null=True)
    brand_color = models.TextField(null=True, db_column="brandColor")
    accent_color = models.TextField(null=True, db_column="accentColor")
    accent_button_color = models.TextField(null=True, db_column="accentButtonColor")
    apply_accent_color_to_dataroom_view = models.BooleanField(db_column="applyAccentColorToDataroomView", default=False)
    welcome_message = models.TextField(null=True, db_column="welcomeMessage")
    card_layout = models.TextField(db_column="cardLayout", default="LIST")
    show_folder_tree = models.BooleanField(db_column="showFolderTree", default=True)
    viewer_layout_preset = models.TextField(db_column="viewerLayoutPreset", default="STANDARD")
    viewer_header_style = models.TextField(db_column="viewerHeaderStyle", default="DEFAULT")
    hide_folder_icons_in_main = models.BooleanField(db_column="hideFolderIconsInMain", default=False)
    cta_label = models.TextField(null=True, db_column="ctaLabel")
    cta_url = models.TextField(null=True, db_column="ctaUrl")
    custom_link_preview_enabled = models.BooleanField(db_column="customLinkPreviewEnabled", default=False)
    link_preview_title = models.TextField(null=True, db_column="linkPreviewTitle")
    link_preview_description = models.TextField(null=True, db_column="linkPreviewDescription")
    link_preview_image = models.TextField(null=True, db_column="linkPreviewImage")
    link_preview_favicon = models.TextField(null=True, db_column="linkPreviewFavicon")
    default_language = models.TextField(db_column="defaultLanguage", default="en")
    dataroom = models.OneToOneField("Dataroom", on_delete=models.CASCADE, related_name="brand", db_column="dataroomId")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "DataroomBrand"


class ViewerGroup(models.Model):
    id = models.TextField(primary_key=True, default=None)
    name = models.TextField()
    domains = ArrayField(models.TextField())
    allow_all = models.BooleanField(db_column="allowAll", default=False)
    dataroom = models.ForeignKey("Dataroom", on_delete=models.CASCADE, related_name="viewer_groups", db_column="dataroomId", db_index=False)
    team = models.ForeignKey("Team", on_delete=models.CASCADE, related_name="viewer_groups", db_column="teamId", db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "ViewerGroup"
        indexes = [
            models.Index(fields=["dataroom"]),
            models.Index(fields=["team"]),
            models.Index(fields=["dataroom", "created_at"]),
        ]


class ViewerGroupMembership(models.Model):
    id = models.TextField(primary_key=True, default=None)
    viewer = models.ForeignKey("Viewer", on_delete=models.CASCADE, related_name="groups", db_column="viewerId", db_index=False)
    group = models.ForeignKey("ViewerGroup", on_delete=models.CASCADE, related_name="members", db_column="groupId", db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "ViewerGroupMembership"
        constraints = [
            models.UniqueConstraint(fields=["viewer", "group"], name="ViewerGroupMembership_viewerId_groupId_key"),
        ]
        indexes = [
            models.Index(fields=["viewer"]),
            models.Index(fields=["group"]),
        ]


class ViewerGroupAccessControls(models.Model):
    id = models.TextField(primary_key=True, default=None)
    group = models.ForeignKey("ViewerGroup", on_delete=models.CASCADE, related_name="access_controls", db_column="groupId", db_index=False)
    item_id = models.TextField(db_column="itemId")
    item_type = models.TextField(choices=ItemType.choices, db_column="itemType")
    can_view = models.BooleanField(db_column="canView", default=True)
    can_download = models.BooleanField(db_column="canDownload", default=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "ViewerGroupAccessControls"
        constraints = [
            models.UniqueConstraint(fields=["group", "item_id"], name="ViewerGroupAccessControls_groupId_itemId_key"),
        ]
        indexes = [
            models.Index(fields=["group"]),
        ]


class PermissionGroup(models.Model):
    id = models.TextField(primary_key=True, default=None)
    name = models.TextField()
    description = models.TextField(null=True)
    dataroom = models.ForeignKey("Dataroom", on_delete=models.CASCADE, related_name="permission_groups", db_column="dataroomId", db_index=False)
    team = models.ForeignKey("Team", on_delete=models.CASCADE, related_name="permission_groups", db_column="teamId", db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "PermissionGroup"
        indexes = [
            models.Index(fields=["dataroom"]),
            models.Index(fields=["team"]),
        ]


class PermissionGroupAccessControls(models.Model):
    id = models.TextField(primary_key=True, default=None)
    group = models.ForeignKey("PermissionGroup", on_delete=models.CASCADE, related_name="access_controls", db_column="groupId", db_index=False)
    item_id = models.TextField(db_column="itemId")
    item_type = models.TextField(choices=ItemType.choices, db_column="itemType")
    can_view = models.BooleanField(db_column="canView", default=True)
    can_download = models.BooleanField(db_column="canDownload", default=False)
    can_download_original = models.BooleanField(db_column="canDownloadOriginal", default=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "PermissionGroupAccessControls"
        constraints = [
            models.UniqueConstraint(fields=["group", "item_id"], name="PermissionGroupAccessControls_groupId_itemId_key"),
        ]
        indexes = [
            models.Index(fields=["group"]),
        ]


class Document(models.Model):
    id = models.TextField(primary_key=True, default=None)
    name = models.TextField()
    description = models.TextField(null=True)
    file = models.TextField()
    original_file = models.TextField(null=True, db_column="originalFile")
    type = models.TextField(null=True)
    content_type = models.TextField(null=True, db_column="contentType")
    storage_type = models.TextField(choices=DocumentStorageType.choices, db_column="storageType", default=DocumentStorageType.VERCEL_BLOB)
    num_pages = models.IntegerField(null=True, db_column="numPages")
    team = models.ForeignKey("Team", on_delete=models.CASCADE, related_name="documents", db_column="teamId", db_index=False)
    owner = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="documents", db_column="ownerId", null=True, db_index=False)
    assistant_enabled = models.BooleanField(db_column="assistantEnabled", default=False)
    advanced_excel_enabled = models.BooleanField(db_column="advancedExcelEnabled", default=False)
    agents_enabled = models.BooleanField(db_column="agentsEnabled", default=False)
    download_only = models.BooleanField(db_column="downloadOnly", default=False)
    hidden_in_all_documents = models.BooleanField(db_column="hiddenInAllDocuments", default=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    folder = models.ForeignKey("Folder", on_delete=models.SET_NULL, related_name="documents", db_column="folderId", null=True, db_index=False)
    is_external_upload = models.BooleanField(db_column="isExternalUpload", default=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Document"
        indexes = [
            models.Index(fields=["owner"]),
            models.Index(fields=["team"]),
            models.Index(fields=["folder"]),
            models.Index(fields=["team", "folder"]),
            models.Index(fields=["team", "name"]),
            models.Index(fields=["team", "hidden_in_all_documents"]),
        ]


class DocumentVersion(models.Model):
    id = models.TextField(primary_key=True, default=None)
    version_number = models.IntegerField(db_column="versionNumber")
    document = models.ForeignKey("Document", on_delete=models.CASCADE, related_name="versions", db_column="documentId", db_index=False)
    file = models.TextField()
    original_file = models.TextField(null=True, db_column="originalFile")
    type = models.TextField(null=True)
    content_type = models.TextField(null=True, db_column="contentType")
    file_size = models.BigIntegerField(null=True, db_column="fileSize")
    storage_type = models.TextField(choices=DocumentStorageType.choices, db_column="storageType", default=DocumentStorageType.VERCEL_BLOB)
    num_pages = models.IntegerField(null=True, db_column="numPages")
    is_primary = models.BooleanField(db_column="isPrimary", default=False)
    is_vertical = models.BooleanField(db_column="isVertical", default=False)
    file_id = models.TextField(null=True, db_column="fileId")
    vector_store_file_id = models.TextField(null=True, db_column="vectorStoreFileId")
    has_pages = models.BooleanField(db_column="hasPages", default=False)
    length = models.IntegerField(null=True)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "DocumentVersion"
        constraints = [
            models.UniqueConstraint(fields=["version_number", "document"], name="DocumentVersion_versionNumber_documentId_key"),
        ]
        indexes = [
            models.Index(fields=["document"]),
            models.Index(fields=["document", "is_primary"]),
            models.Index(fields=["document", "-created_at"]),
            models.Index(fields=["document", "is_primary", "created_at"]),
        ]


class DocumentPage(models.Model):
    id = models.TextField(primary_key=True, default=None)
    version = models.ForeignKey("DocumentVersion", on_delete=models.CASCADE, related_name="pages", db_column="versionId", db_index=False)
    page_number = models.IntegerField(db_column="pageNumber")
    embedded_links = ArrayField(models.TextField(), db_column="embeddedLinks")
    page_links = models.JSONField(null=True, db_column="pageLinks")
    metadata = models.JSONField(null=True)
    file = models.TextField()
    storage_type = models.TextField(choices=DocumentStorageType.choices, db_column="storageType", default=DocumentStorageType.VERCEL_BLOB)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "DocumentPage"
        constraints = [
            models.UniqueConstraint(fields=["page_number", "version"], name="DocumentPage_pageNumber_versionId_key"),
        ]
        indexes = [
            models.Index(fields=["version"]),
        ]


class Folder(models.Model):
    id = models.TextField(primary_key=True, default=None)
    name = models.TextField()
    path = models.TextField()
    parent_folder = models.ForeignKey("self", on_delete=models.SET_NULL, related_name="child_folders", db_column="parentId", null=True, db_index=False)
    hidden_in_all_documents = models.BooleanField(db_column="hiddenInAllDocuments", default=False)
    icon = models.TextField(null=True)
    color = models.TextField(null=True)
    team = models.ForeignKey("Team", on_delete=models.CASCADE, related_name="folders", db_column="teamId", db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Folder"
        constraints = [
            models.UniqueConstraint(fields=["team", "path"], name="Folder_teamId_path_key"),
        ]
        indexes = [
            models.Index(fields=["parent_folder"]),
            models.Index(fields=["team", "hidden_in_all_documents"]),
        ]


class DocumentUpload(models.Model):
    id = models.TextField(primary_key=True, default=None)
    document = models.ForeignKey("Document", on_delete=models.CASCADE, related_name="uploaded_document", db_column="documentId", db_index=False)
    team = models.ForeignKey("Team", on_delete=models.CASCADE, related_name="uploaded_documents", db_column="teamId", db_index=False)
    viewer = models.ForeignKey("Viewer", on_delete=models.SET_NULL, related_name="uploaded_documents", db_column="viewerId", null=True, db_index=False)
    view = models.ForeignKey("View", on_delete=models.SET_NULL, related_name="uploaded_documents", db_column="viewId", null=True, db_index=False)
    link = models.ForeignKey("Link", on_delete=models.CASCADE, related_name="uploaded_documents", db_column="linkId", db_index=False)
    dataroom = models.ForeignKey("Dataroom", on_delete=models.SET_NULL, related_name="uploaded_documents", db_column="dataroomId", null=True, db_index=False)
    dataroom_document = models.ForeignKey("DataroomDocument", on_delete=models.SET_NULL, related_name="uploaded_documents", db_column="dataroomDocumentId", null=True, db_index=False)
    task = models.ForeignKey("Task", on_delete=models.SET_NULL, related_name="uploads", db_column="taskId", null=True, db_index=False)
    original_filename = models.TextField(null=True, db_column="originalFilename")
    file_size = models.BigIntegerField(null=True, db_column="fileSize")
    num_pages = models.IntegerField(null=True, db_column="numPages")
    mime_type = models.TextField(null=True, db_column="mimeType")
    uploaded_at = UtcDateTimeField(db_column="uploadedAt", default=timezone.now)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "DocumentUpload"
        indexes = [
            models.Index(fields=["document"]),
            models.Index(fields=["viewer"]),
            models.Index(fields=["view"]),
            models.Index(fields=["link"]),
            models.Index(fields=["team"]),
            models.Index(fields=["dataroom"]),
            models.Index(fields=["dataroom_document"]),
            models.Index(fields=["task"]),
        ]


class PendingDocumentUpload(models.Model):
    id = models.TextField(primary_key=True, default=None)
    upload_id = models.TextField(unique=True, db_column="uploadId")
    team_id = models.TextField(db_column="teamId")
    token_id = models.TextField(db_column="tokenId")
    key = models.TextField()
    content_type = models.TextField(db_column="contentType")
    storage_type = models.TextField(choices=DocumentStorageType.choices, db_column="storageType", default=DocumentStorageType.S3_PATH)
    expires_at = UtcDateTimeField(db_column="expiresAt")
    consumed_at = UtcDateTimeField(null=True, db_column="consumedAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "PendingDocumentUpload"
        indexes = [
            models.Index(fields=["team_id", "expires_at"]),
            models.Index(fields=["token_id", "expires_at"]),
        ]


class Integration(models.Model):
    id = models.TextField(primary_key=True, default=None)
    name = models.TextField()
    slug = models.TextField(unique=True)
    description = models.TextField(null=True)
    readme = models.TextField(null=True)
    developer = models.TextField()
    website = models.TextField()
    logo = models.TextField(null=True)
    screenshots = models.JSONField(null=True)
    verified = models.BooleanField(default=False)
    install_url = models.TextField(null=True, db_column="installUrl")
    category = models.TextField(null=True)
    coming_soon = models.BooleanField(db_column="comingSoon", default=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Integration"


class InstalledIntegration(models.Model):
    id = models.TextField(primary_key=True, default=None)
    credentials = models.JSONField(null=True)
    configuration = models.JSONField(null=True)
    enabled = models.BooleanField(default=True)
    integration = models.ForeignKey("Integration", on_delete=models.CASCADE, related_name="installed_integrations", db_column="integrationId", db_index=False)
    user = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="installed_integrations", db_column="userId", null=True, db_index=False)
    team = models.ForeignKey("Team", on_delete=models.CASCADE, related_name="installed_integrations", db_column="teamId", db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "InstalledIntegration"
        constraints = [
            models.UniqueConstraint(fields=["team", "integration"], name="InstalledIntegration_teamId_integrationId_key"),
        ]
        indexes = [
            models.Index(fields=["team"]),
            models.Index(fields=["integration"]),
        ]


class JacksonIndex(models.Model):
    id = models.AutoField(primary_key=True)
    key = models.TextField()
    store_key = models.TextField(db_column="storeKey")

    class Meta:
        db_table = "jackson_index"
        indexes = [
            models.Index(fields=["key", "store_key"]),
        ]


class JacksonStore(models.Model):
    key = models.TextField(primary_key=True, default=None)
    value = models.TextField()
    iv = models.TextField(null=True)
    tag = models.TextField(null=True)
    namespace = models.TextField(null=True)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    modified_at = UtcDateTimeField(null=True, db_column="modifiedAt")

    class Meta:
        db_table = "jackson_store"
        indexes = [
            models.Index(fields=["namespace"]),
        ]


class JacksonTtl(models.Model):
    key = models.TextField(primary_key=True, default=None)
    expires_at = models.BigIntegerField(db_column="expiresAt")

    class Meta:
        db_table = "jackson_ttl"
        indexes = [
            models.Index(fields=["expires_at"]),
        ]


class Link(models.Model):
    id = models.TextField(primary_key=True, default=None)
    document = models.ForeignKey("Document", on_delete=models.CASCADE, related_name="links", db_column="documentId", null=True, db_index=False)
    dataroom = models.ForeignKey("Dataroom", on_delete=models.CASCADE, related_name="links", db_column="dataroomId", null=True, db_index=False)
    link_type = models.TextField(choices=LinkType.choices, db_column="linkType", default=LinkType.DOCUMENT_LINK)
    url = models.TextField(null=True, unique=True)
    name = models.TextField(null=True)
    slug = models.TextField(null=True)
    expires_at = UtcDateTimeField(null=True, db_column="expiresAt")
    password = models.TextField(null=True)
    allow_list = ArrayField(models.TextField(), db_column="allowList")
    deny_list = ArrayField(models.TextField(), db_column="denyList")
    email_protected = models.BooleanField(db_column="emailProtected", default=True)
    email_authenticated = models.BooleanField(db_column="emailAuthenticated", default=False)
    allow_download = models.BooleanField(null=True, db_column="allowDownload", default=False)
    is_archived = models.BooleanField(db_column="isArchived", default=False)
    deleted_at = UtcDateTimeField(null=True, db_column="deletedAt")
    domain = models.ForeignKey("Domain", on_delete=models.SET_NULL, related_name="links", db_column="domainId", null=True, db_index=False)
    domain_slug = models.TextField(null=True, db_column="domainSlug")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    enable_notification = models.BooleanField(null=True, db_column="enableNotification", default=True)
    enable_feedback = models.BooleanField(null=True, db_column="enableFeedback", default=False)
    enable_question = models.BooleanField(null=True, db_column="enableQuestion", default=False)
    enable_screenshot_protection = models.BooleanField(null=True, db_column="enableScreenshotProtection", default=False)
    enable_confidential_view = models.BooleanField(null=True, db_column="enableConfidentialView", default=False)
    enable_agreement = models.BooleanField(null=True, db_column="enableAgreement", default=False)
    agreement = models.ForeignKey("Agreement", on_delete=models.SET_NULL, related_name="links", db_column="agreementId", null=True, db_index=False)
    show_banner = models.BooleanField(null=True, db_column="showBanner", default=False)
    enable_watermark = models.BooleanField(null=True, db_column="enableWatermark", default=False)
    watermark_config = models.JSONField(null=True, db_column="watermarkConfig")
    audience_type = models.TextField(choices=LinkAudienceType.choices, db_column="audienceType", default=LinkAudienceType.GENERAL)
    group = models.ForeignKey("ViewerGroup", on_delete=models.SET_NULL, related_name="links", db_column="groupId", null=True, db_index=False)
    permission_group = models.ForeignKey("PermissionGroup", on_delete=models.SET_NULL, related_name="links", db_column="permissionGroupId", null=True, db_index=False)
    meta_title = models.TextField(null=True, db_column="metaTitle")
    meta_description = models.TextField(null=True, db_column="metaDescription")
    meta_image = models.TextField(null=True, db_column="metaImage")
    meta_favicon = models.TextField(null=True, db_column="metaFavicon")
    enable_custom_metatag = models.BooleanField(null=True, db_column="enableCustomMetatag", default=False)
    welcome_message = models.TextField(null=True, db_column="welcomeMessage")
    enable_conversation = models.BooleanField(db_column="enableConversation", default=False)
    enable_aiagents = models.BooleanField(null=True, db_column="enableAIAgents", default=False)
    enable_upload = models.BooleanField(null=True, db_column="enableUpload", default=False)
    is_file_request_only = models.BooleanField(null=True, db_column="isFileRequestOnly", default=False)
    upload_folder_id = models.TextField(null=True, db_column="uploadFolderId")
    upload_folder_ids = ArrayField(models.TextField(), db_column="uploadFolderIds", default=list)
    enable_index_file = models.BooleanField(null=True, db_column="enableIndexFile", default=False)
    team = models.ForeignKey("Team", on_delete=models.CASCADE, related_name="links", db_column="teamId", null=True, db_index=False)
    brand = models.ForeignKey("Brand", on_delete=models.SET_NULL, related_name="links", db_column="brandId", null=True, db_index=False)
    owner = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="links", db_column="ownerId", null=True, db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Link"
        constraints = [
            models.UniqueConstraint(fields=["domain_slug", "slug"], name="Link_domainSlug_slug_key"),
        ]
        indexes = [
            models.Index(fields=["document"]),
            models.Index(fields=["team"]),
            models.Index(fields=["document", "is_archived"]),
            models.Index(fields=["permission_group"]),
            models.Index(fields=["deleted_at"]),
            models.Index(fields=["owner"]),
            models.Index(fields=["brand"]),
        ]


class LinkPreset(models.Model):
    id = models.TextField(primary_key=True, default=None)
    name = models.TextField()
    team = models.ForeignKey("Team", on_delete=models.CASCADE, related_name="link_presets", db_column="teamId", db_index=False)
    p_id = models.TextField(null=True, unique=True, db_column="pId")
    enable_custom_meta_tag = models.BooleanField(null=True, db_column="enableCustomMetaTag", default=False)
    meta_title = models.TextField(null=True, db_column="metaTitle")
    meta_description = models.TextField(null=True, db_column="metaDescription")
    meta_image = models.TextField(null=True, db_column="metaImage")
    meta_favicon = models.TextField(null=True, db_column="metaFavicon")
    enable_notification = models.BooleanField(null=True, db_column="enableNotification", default=False)
    email_protected = models.BooleanField(null=True, db_column="emailProtected", default=True)
    email_authenticated = models.BooleanField(null=True, db_column="emailAuthenticated", default=False)
    allow_download = models.BooleanField(null=True, db_column="allowDownload", default=False)
    enable_allow_list = models.BooleanField(null=True, db_column="enableAllowList", default=False)
    allow_list = ArrayField(models.TextField(), db_column="allowList")
    enable_deny_list = models.BooleanField(null=True, db_column="enableDenyList", default=False)
    deny_list = ArrayField(models.TextField(), db_column="denyList")
    expires_in = models.IntegerField(null=True, db_column="expiresIn")
    enable_screenshot_protection = models.BooleanField(null=True, db_column="enableScreenshotProtection", default=False)
    enable_confidential_view = models.BooleanField(null=True, db_column="enableConfidentialView", default=False)
    expires_at = UtcDateTimeField(null=True, db_column="expiresAt")
    enable_password = models.BooleanField(null=True, db_column="enablePassword", default=False)
    password = models.TextField(null=True)
    enable_watermark = models.BooleanField(null=True, db_column="enableWatermark", default=False)
    watermark_config = models.JSONField(null=True, db_column="watermarkConfig")
    enable_agreement = models.BooleanField(null=True, db_column="enableAgreement", default=False)
    agreement_id = models.TextField(null=True, db_column="agreementId")
    enable_custom_fields = models.BooleanField(null=True, db_column="enableCustomFields", default=False)
    custom_fields = models.JSONField(null=True, db_column="customFields")
    show_banner = models.BooleanField(null=True, db_column="showBanner", default=False)
    welcome_message = models.TextField(null=True, db_column="welcomeMessage")
    is_default = models.BooleanField(db_column="isDefault", default=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "LinkPreset"
        indexes = [
            models.Index(fields=["team"]),
        ]


class VisitorGroup(models.Model):
    id = models.TextField(primary_key=True, default=None)
    name = models.TextField()
    emails = ArrayField(models.TextField())
    team = models.ForeignKey("Team", on_delete=models.CASCADE, related_name="visitor_groups", db_column="teamId", db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "VisitorGroup"
        indexes = [
            models.Index(fields=["team"]),
        ]


class LinkVisitorGroup(models.Model):
    id = models.TextField(primary_key=True, default=None)
    link = models.ForeignKey("Link", on_delete=models.CASCADE, related_name="visitor_groups", db_column="linkId", db_index=False)
    visitor_group = models.ForeignKey("VisitorGroup", on_delete=models.CASCADE, related_name="links", db_column="visitorGroupId", db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "LinkVisitorGroup"
        constraints = [
            models.UniqueConstraint(fields=["link", "visitor_group"], name="LinkVisitorGroup_linkId_visitorGroupId_key"),
        ]
        indexes = [
            models.Index(fields=["link"]),
            models.Index(fields=["visitor_group"]),
        ]


class CustomField(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    type = models.TextField(choices=CustomFieldType.choices)
    identifier = models.TextField()
    label = models.TextField()
    placeholder = models.TextField(null=True)
    required = models.BooleanField(default=False)
    disabled = models.BooleanField(default=False)
    link = models.ForeignKey("Link", on_delete=models.CASCADE, related_name="custom_fields", db_column="linkId", db_index=False)
    order_index = models.IntegerField(db_column="orderIndex", default=0)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "CustomField"
        indexes = [
            models.Index(fields=["link"]),
        ]


class CustomFieldResponse(models.Model):
    id = models.TextField(primary_key=True, default=None)
    data = models.JSONField()
    view = models.OneToOneField("View", on_delete=models.CASCADE, related_name="custom_field_response", db_column="viewId")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "CustomFieldResponse"
        indexes = [
            models.Index(fields=["view"]),
        ]


class NotificationPreference(models.Model):
    id = models.TextField(primary_key=True, default=None)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="notification_preferences", db_column="userId", db_index=False)
    team = models.ForeignKey("Team", on_delete=models.CASCADE, related_name="notification_preferences", db_column="teamId", db_index=False)
    type = models.TextField()
    frequency = models.TextField(default="IMMEDIATE")
    scope = models.TextField(default="ALL")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "NotificationPreference"
        constraints = [
            models.UniqueConstraint(fields=["user", "team", "type"], name="NotificationPreference_userId_teamId_type_key"),
        ]
        indexes = [
            models.Index(fields=["user", "team"]),
        ]


class OAuthClient(models.Model):
    id = models.TextField(primary_key=True, default=None)
    client_id = models.TextField(unique=True, db_column="clientId")
    client_name = models.TextField(db_column="clientName")
    client_uri = models.TextField(null=True, db_column="clientUri")
    logo_uri = models.TextField(null=True, db_column="logoUri")
    redirect_uris = ArrayField(models.TextField(), db_column="redirectUris")
    grant_types = ArrayField(models.TextField(), db_column="grantTypes")
    scopes = models.TextField()
    token_auth_method = models.TextField(db_column="tokenAuthMethod")
    hashed_secret = models.TextField(null=True, db_column="hashedSecret")
    registration_type = models.TextField(db_column="registrationType")
    metadata_url = models.TextField(null=True, db_column="metadataUrl")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "OAuthClient"
        indexes = [
            models.Index(fields=["client_id"]),
        ]


class OAuthClientApproval(models.Model):
    id = models.TextField(primary_key=True, default=None)
    user_id = models.TextField(db_column="userId")
    client_id = models.TextField(db_column="clientId")
    approved_at = UtcDateTimeField(db_column="approvedAt", default=timezone.now)
    revoked_at = UtcDateTimeField(null=True, db_column="revokedAt")
    client_metadata_hash = models.TextField(null=True, db_column="clientMetadataHash")

    class Meta:
        db_table = "OAuthClientApproval"
        constraints = [
            models.UniqueConstraint(fields=["user_id", "client_id"], name="OAuthClientApproval_userId_clientId_key"),
        ]
        indexes = [
            models.Index(fields=["client_id"]),
        ]


class OAuthGrantTeam(models.Model):
    id = models.TextField(primary_key=True, default=None)
    grant_id = models.TextField(unique=True, db_column="grantId")
    team_id = models.TextField(db_column="teamId")
    user_id = models.TextField(db_column="userId")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "OAuthGrantTeam"
        indexes = [
            models.Index(fields=["grant_id"]),
        ]


class OAuthRecord(models.Model):
    id = models.TextField(primary_key=True, default=None)
    kind = models.TextField()
    payload = models.JSONField()
    grant_id = models.TextField(null=True, db_column="grantId")
    user_code = models.TextField(null=True, db_column="userCode")
    uid = models.TextField(null=True)
    expires_at = UtcDateTimeField(null=True, db_column="expiresAt")
    consumed_at = UtcDateTimeField(null=True, db_column="consumedAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "OAuthRecord"
        indexes = [
            models.Index(fields=["kind"]),
            models.Index(fields=["grant_id"]),
            models.Index(fields=["user_code"]),
            models.Index(fields=["uid"]),
            models.Index(fields=["expires_at"]),
        ]


class DocumentRedactionJob(models.Model):
    id = models.TextField(primary_key=True, default=None)
    document = models.ForeignKey("Document", on_delete=models.CASCADE, related_name="redaction_jobs", db_column="documentId", db_index=False)
    document_version = models.ForeignKey("DocumentVersion", on_delete=models.CASCADE, related_name="redaction_jobs", db_column="documentVersionId", db_index=False)
    team = models.ForeignKey("Team", on_delete=models.CASCADE, related_name="redaction_jobs", db_column="teamId", db_index=False)
    created_by = models.ForeignKey("User", on_delete=models.RESTRICT, related_name="created_redaction_jobs", db_column="createdById", db_index=False)
    status = models.TextField(default="PENDING")
    custom_terms = ArrayField(models.TextField(), db_column="customTerms")
    reasons = ArrayField(models.TextField())
    result_version_id = models.TextField(null=True, db_column="resultVersionId")
    error = models.TextField(null=True)
    detect_run_id = models.TextField(null=True, db_column="detectRunId")
    apply_run_id = models.TextField(null=True, db_column="applyRunId")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "DocumentRedactionJob"
        indexes = [
            models.Index(fields=["document"]),
            models.Index(fields=["document_version"]),
            models.Index(fields=["team"]),
            models.Index(fields=["team", "status"]),
        ]


class DocumentRedaction(models.Model):
    id = models.TextField(primary_key=True, default=None)
    job = models.ForeignKey("DocumentRedactionJob", on_delete=models.CASCADE, related_name="redactions", db_column="jobId", db_index=False)
    page_number = models.IntegerField(db_column="pageNumber")
    x = models.FloatField()
    y = models.FloatField()
    width = models.FloatField()
    height = models.FloatField()
    text_preview = models.TextField(null=True, db_column="textPreview")
    category = models.TextField(null=True)
    confidence = models.TextField(null=True)
    reason = models.TextField(null=True)
    source = models.TextField(default="AI")
    status = models.TextField(default="PENDING")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "DocumentRedaction"
        indexes = [
            models.Index(fields=["job"]),
            models.Index(fields=["job", "page_number"]),
            models.Index(fields=["job", "status"]),
        ]


class ReferralInvite(models.Model):
    id = models.TextField(primary_key=True, default=None)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="referral_invites", db_column="userId", db_index=False)
    team = models.ForeignKey("Team", on_delete=models.CASCADE, related_name="referral_invites", db_column="teamId", null=True, db_index=False)
    scope_id = models.TextField(db_column="scopeId")
    email = models.TextField()
    referral_link = models.TextField(null=True, db_column="referralLink")
    sent_at = UtcDateTimeField(db_column="sentAt", default=timezone.now)

    class Meta:
        db_table = "ReferralInvite"
        constraints = [
            models.UniqueConstraint(fields=["scope_id", "email"], name="ReferralInvite_scopeId_email_key"),
        ]
        indexes = [
            models.Index(fields=["user"]),
            models.Index(fields=["team"]),
        ]


class Account(models.Model):
    id = models.TextField(primary_key=True, default=None)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="accounts", db_column="userId", db_index=False)
    type = models.TextField()
    provider = models.TextField()
    provider_account_id = models.TextField(db_column="providerAccountId")
    refresh_token = models.TextField(null=True)
    access_token = models.TextField(null=True)
    expires_at = models.IntegerField(null=True)
    token_type = models.TextField(null=True)
    scope = models.TextField(null=True)
    id_token = models.TextField(null=True)
    session_state = models.TextField(null=True)

    class Meta:
        db_table = "Account"
        constraints = [
            models.UniqueConstraint(fields=["provider", "provider_account_id"], name="Account_provider_providerAccountId_key"),
        ]


class Session(models.Model):
    id = models.TextField(primary_key=True, default=None)
    session_token = models.TextField(unique=True, db_column="sessionToken")
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="sessions", db_column="userId", db_index=False)
    expires = UtcDateTimeField()

    class Meta:
        db_table = "Session"


class User(models.Model):
    id = models.TextField(primary_key=True, default=None)
    name = models.TextField(null=True)
    email = models.TextField(null=True, unique=True)
    email_verified = UtcDateTimeField(null=True, db_column="emailVerified")
    image = models.TextField(null=True)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    contact_id = models.TextField(null=True, db_column="contactId")
    plan = models.TextField(default="free")
    stripe_id = models.TextField(null=True, unique=True, db_column="stripeId")
    subscription_id = models.TextField(null=True, unique=True, db_column="subscriptionId")
    starts_at = UtcDateTimeField(null=True, db_column="startsAt")
    ends_at = UtcDateTimeField(null=True, db_column="endsAt")
    referral_track = models.TextField(choices=ReferralTrack.choices, null=True, db_column="referralTrack")
    referral_track_selected_at = UtcDateTimeField(null=True, db_column="referralTrackSelectedAt")
    referral_profile = models.JSONField(null=True, db_column="referralProfile")

    class Meta:
        db_table = "User"


class Domain(models.Model):
    id = models.TextField(primary_key=True, default=None)
    slug = models.TextField(unique=True)
    user = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="domains", db_column="userId", null=True, db_index=False)
    team = models.ForeignKey("Team", on_delete=models.CASCADE, related_name="domains", db_column="teamId", db_index=False)
    verified = models.BooleanField(default=False)
    is_default = models.BooleanField(db_column="isDefault", default=False)
    redirect_url = models.TextField(null=True, db_column="redirectUrl")
    last_checked = UtcDateTimeField(db_column="lastChecked", default=timezone.now)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Domain"
        indexes = [
            models.Index(fields=["user"]),
            models.Index(fields=["team"]),
        ]


class View(models.Model):
    id = models.TextField(primary_key=True, default=None)
    link = models.ForeignKey("Link", on_delete=models.CASCADE, related_name="views", db_column="linkId", db_index=False)
    document = models.ForeignKey("Document", on_delete=models.CASCADE, related_name="views", db_column="documentId", null=True, db_index=False)
    dataroom = models.ForeignKey("Dataroom", on_delete=models.CASCADE, related_name="views", db_column="dataroomId", null=True, db_index=False)
    dataroom_view_id = models.TextField(null=True, db_column="dataroomViewId")
    viewer_email = models.TextField(null=True, db_column="viewerEmail")
    viewer_name = models.TextField(null=True, db_column="viewerName")
    verified = models.BooleanField(default=False)
    viewed_at = UtcDateTimeField(db_column="viewedAt", default=timezone.now)
    downloaded_at = UtcDateTimeField(null=True, db_column="downloadedAt")
    download_type = models.TextField(choices=DownloadType.choices, null=True, db_column="downloadType")
    download_metadata = models.JSONField(null=True, db_column="downloadMetadata")
    view_type = models.TextField(choices=ViewType.choices, db_column="viewType", default=ViewType.DOCUMENT_VIEW)
    viewer = models.ForeignKey("Viewer", on_delete=models.CASCADE, related_name="views", db_column="viewerId", null=True, db_index=False)
    group = models.ForeignKey("ViewerGroup", on_delete=models.SET_NULL, related_name="views", db_column="groupId", null=True, db_index=False)
    is_archived = models.BooleanField(db_column="isArchived", default=False)
    team = models.ForeignKey("Team", on_delete=models.CASCADE, related_name="views", db_column="teamId", null=True, db_index=False)

    class Meta:
        db_table = "View"
        indexes = [
            models.Index(fields=["link"]),
            models.Index(fields=["document"]),
            models.Index(fields=["dataroom"]),
            models.Index(fields=["dataroom_view_id"]),
            models.Index(fields=["viewer"]),
            models.Index(fields=["group"]),
            models.Index(fields=["team"]),
            models.Index(fields=["-viewed_at"]),
            models.Index(fields=["viewer", "document"]),
            models.Index(fields=["viewer_email"]),
            models.Index(fields=["document", "is_archived"]),
            models.Index(fields=["document", "-viewed_at"]),
        ]


class Viewer(models.Model):
    id = models.TextField(primary_key=True, default=None)
    email = models.TextField()
    verified = models.BooleanField(default=False)
    invited_at = UtcDateTimeField(null=True, db_column="invitedAt")
    notification_preferences = models.JSONField(null=True, db_column="notificationPreferences")
    dataroom = models.ForeignKey("Dataroom", on_delete=models.SET_NULL, related_name="viewers", db_column="dataroomId", null=True, db_index=False)
    team = models.ForeignKey("Team", on_delete=models.CASCADE, related_name="viewers", db_column="teamId", db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Viewer"
        constraints = [
            models.UniqueConstraint(fields=["team", "email"], name="Viewer_teamId_email_key"),
        ]
        indexes = [
            models.Index(fields=["team"]),
            models.Index(fields=["dataroom"]),
        ]


class Reaction(models.Model):
    id = models.TextField(primary_key=True, default=None)
    view = models.ForeignKey("View", on_delete=models.CASCADE, related_name="reactions", db_column="viewId", db_index=False)
    page_number = models.IntegerField(db_column="pageNumber")
    type = models.TextField()
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "Reaction"
        indexes = [
            models.Index(fields=["view"]),
            models.Index(fields=["view", "type"]),
        ]


class SentEmail(models.Model):
    id = models.TextField(primary_key=True, default=None)
    type = models.TextField(choices=EmailType.choices)
    recipient = models.TextField()
    marketing = models.BooleanField(default=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    team = models.ForeignKey("Team", on_delete=models.CASCADE, related_name="sent_emails", db_column="teamId", db_index=False)
    domain_slug = models.TextField(null=True, db_column="domainSlug")

    class Meta:
        db_table = "SentEmail"
        indexes = [
            models.Index(fields=["team"]),
        ]


class Chat(models.Model):
    id = models.TextField(primary_key=True, default=None)
    title = models.TextField(null=True)
    team = models.ForeignKey("Team", on_delete=models.CASCADE, related_name="chats", db_column="teamId", db_index=False)
    document = models.ForeignKey("Document", on_delete=models.CASCADE, related_name="chats", db_column="documentId", null=True, db_index=False)
    dataroom = models.ForeignKey("Dataroom", on_delete=models.CASCADE, related_name="chats", db_column="dataroomId", null=True, db_index=False)
    link = models.ForeignKey("Link", on_delete=models.CASCADE, related_name="chats", db_column="linkId", null=True, db_index=False)
    view = models.ForeignKey("View", on_delete=models.CASCADE, related_name="chats", db_column="viewId", null=True, db_index=False)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="chats", db_column="userId", null=True, db_index=False)
    viewer = models.ForeignKey("Viewer", on_delete=models.CASCADE, related_name="chats", db_column="viewerId", null=True, db_index=False)
    vector_store_id = models.TextField(null=True, db_column="vectorStoreId")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    last_message_at = UtcDateTimeField(null=True, db_column="lastMessageAt")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Chat"
        indexes = [
            models.Index(fields=["team"]),
            models.Index(fields=["document"]),
            models.Index(fields=["dataroom"]),
            models.Index(fields=["link"]),
            models.Index(fields=["user"]),
            models.Index(fields=["viewer"]),
            models.Index(fields=["view"]),
            models.Index(fields=["-created_at"]),
        ]


class ChatMessage(models.Model):
    id = models.TextField(primary_key=True, default=None)
    chat = models.ForeignKey("Chat", on_delete=models.CASCADE, related_name="messages", db_column="chatId", db_index=False)
    role = models.TextField()
    content = models.TextField()
    metadata = models.JSONField(null=True)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "ChatMessage"
        indexes = [
            models.Index(fields=["chat"]),
            models.Index(fields=["chat", "created_at"]),
        ]


class Feedback(models.Model):
    id = models.TextField(primary_key=True, default=None)
    link = models.OneToOneField("Link", on_delete=models.CASCADE, related_name="feedback", db_column="linkId")
    data = models.JSONField()
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Feedback"
        indexes = [
            models.Index(fields=["link"]),
        ]


class FeedbackResponse(models.Model):
    id = models.TextField(primary_key=True, default=None)
    feedback = models.ForeignKey("Feedback", on_delete=models.CASCADE, related_name="responses", db_column="feedbackId", db_index=False)
    data = models.JSONField()
    view = models.OneToOneField("View", on_delete=models.CASCADE, related_name="feedback_response", db_column="viewId")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "FeedbackResponse"
        indexes = [
            models.Index(fields=["feedback"]),
            models.Index(fields=["view"]),
        ]


class Agreement(models.Model):
    id = models.TextField(primary_key=True, default=None)
    name = models.TextField()
    content = models.TextField()
    content_type = models.TextField(db_column="contentType", default="LINK")
    signing_provider = models.TextField(db_column="signingProvider", default="LEGACY")
    signing_external_id = models.TextField(null=True, unique=True, db_column="signingExternalId")
    signing_envelope_id = models.TextField(null=True, db_column="signingEnvelopeId")
    signing_template_id = models.TextField(null=True, db_column="signingTemplateId")
    require_name = models.BooleanField(db_column="requireName", default=True)
    team = models.ForeignKey("Team", on_delete=models.CASCADE, related_name="agreements", db_column="teamId", db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    deleted_at = UtcDateTimeField(null=True, db_column="deletedAt")
    deleted_by = models.TextField(null=True, db_column="deletedBy")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Agreement"
        indexes = [
            models.Index(fields=["team"]),
        ]


class AgreementResponse(models.Model):
    id = models.TextField(primary_key=True, default=None)
    agreement = models.ForeignKey("Agreement", on_delete=models.CASCADE, related_name="responses", db_column="agreementId", db_index=False)
    view = models.OneToOneField("View", on_delete=models.CASCADE, related_name="agreement_response", db_column="viewId", null=True)
    link_id = models.TextField(null=True, db_column="linkId")
    signer_email = models.TextField(null=True, db_column="signerEmail")
    signer_name = models.TextField(null=True, db_column="signerName")
    signing_status = models.TextField(db_column="signingStatus", default="PENDING")
    signing_external_id = models.TextField(null=True, unique=True, db_column="signingExternalId")
    signing_envelope_id = models.TextField(null=True, db_column="signingEnvelopeId")
    signing_document_id = models.IntegerField(null=True, db_column="signingDocumentId")
    signed_at = UtcDateTimeField(null=True, db_column="signedAt")
    completed_at = UtcDateTimeField(null=True, db_column="completedAt")
    signed_file_key = models.TextField(null=True, db_column="signedFileKey")
    signed_file_name = models.TextField(null=True, db_column="signedFileName")
    signed_file_storage_type = models.TextField(choices=DocumentStorageType.choices, null=True, db_column="signedFileStorageType")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "AgreementResponse"
        indexes = [
            models.Index(fields=["agreement"]),
            models.Index(fields=["view"]),
            models.Index(fields=["link_id"]),
            models.Index(fields=["signer_email"]),
        ]


class IncomingWebhook(models.Model):
    id = models.TextField(primary_key=True, default=None)
    external_id = models.TextField(unique=True, db_column="externalId")
    name = models.TextField()
    secret = models.TextField(null=True)
    source = models.TextField(null=True)
    actions = models.TextField(null=True)
    consecutive_failures = models.IntegerField(db_column="consecutiveFailures", default=0)
    last_failed_at = UtcDateTimeField(null=True, db_column="lastFailedAt")
    disabled_at = UtcDateTimeField(null=True, db_column="disabledAt")
    team = models.ForeignKey("Team", on_delete=models.CASCADE, related_name="incoming_webhooks", db_column="teamId", db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "IncomingWebhook"
        indexes = [
            models.Index(fields=["team"]),
        ]


class RestrictedToken(models.Model):
    id = models.TextField(primary_key=True, default=None)
    name = models.TextField()
    hashed_key = models.TextField(unique=True, db_column="hashedKey")
    partial_key = models.TextField(db_column="partialKey")
    scopes = models.TextField(null=True)
    mode = models.TextField(default="live")
    source = models.TextField(default="dashboard")
    subject_type = models.TextField(db_column="subjectType", default="user")
    oauth_client = models.ForeignKey("OAuthClient", on_delete=models.SET_NULL, related_name="tokens", db_column="clientId", null=True, db_index=False)
    expires = UtcDateTimeField(null=True)
    last_used = UtcDateTimeField(null=True, db_column="lastUsed")
    rate_limit = models.IntegerField(db_column="rateLimit", default=60)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="restricted_tokens", db_column="userId", db_index=False)
    team = models.ForeignKey("Team", on_delete=models.CASCADE, related_name="restricted_tokens", db_column="teamId", db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "RestrictedToken"
        indexes = [
            models.Index(fields=["user"]),
            models.Index(fields=["team"]),
            models.Index(fields=["hashed_key"]),
            models.Index(fields=["oauth_client"]),
        ]


class Webhook(models.Model):
    id = models.TextField(primary_key=True, default=None)
    p_id = models.TextField(unique=True, db_column="pId")
    name = models.TextField()
    url = models.TextField()
    secret = models.TextField()
    triggers = models.JSONField()
    team = models.ForeignKey("Team", on_delete=models.CASCADE, related_name="webhooks", db_column="teamId", db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Webhook"
        indexes = [
            models.Index(fields=["team"]),
        ]


class YearInReview(models.Model):
    id = models.TextField(primary_key=True, default=None)
    team_id = models.TextField(db_column="teamId")
    status = models.TextField(default="pending")
    attempts = models.IntegerField(default=0)
    last_attempted = UtcDateTimeField(null=True, db_column="lastAttempted")
    error = models.TextField(null=True)
    stats = models.JSONField()
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "YearInReview"
        indexes = [
            models.Index(fields=["status", "attempts"]),
            models.Index(fields=["team_id"]),
        ]


class Tag(models.Model):
    id = models.TextField(primary_key=True, default=None)
    name = models.TextField()
    color = models.TextField()
    description = models.TextField(null=True)
    team = models.ForeignKey("Team", on_delete=models.CASCADE, related_name="tag", db_column="teamId", db_index=False)
    created_by = models.TextField(null=True, db_column="createdBy")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Tag"
        constraints = [
            models.UniqueConstraint(fields=["team", "name"], name="Tag_teamId_name_key"),
        ]
        indexes = [
            models.Index(fields=["team"]),
            models.Index(fields=["name"]),
            models.Index(fields=["id"]),
        ]


class TagItem(models.Model):
    id = models.TextField(primary_key=True, default=None)
    tag = models.ForeignKey("Tag", on_delete=models.CASCADE, related_name="items", db_column="tagId", db_index=False)
    item_type = models.TextField(choices=TagType.choices, db_column="itemType")
    link = models.ForeignKey("Link", on_delete=models.CASCADE, related_name="tags", db_column="linkId", null=True, db_index=False)
    document = models.ForeignKey("Document", on_delete=models.CASCADE, related_name="tags", db_column="documentId", null=True, db_index=False)
    dataroom = models.ForeignKey("Dataroom", on_delete=models.CASCADE, related_name="tags", db_column="dataroomId", null=True, db_index=False)
    tagged_by = models.TextField(null=True, db_column="taggedBy")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "TagItem"
        indexes = [
            models.Index(fields=["tag", "link"]),
            models.Index(fields=["tag", "document"]),
            models.Index(fields=["tag", "dataroom"]),
        ]


class ViewerInvitation(models.Model):
    id = models.TextField(primary_key=True, default=None)
    viewer = models.ForeignKey("Viewer", on_delete=models.CASCADE, related_name="invitations", db_column="viewerId", db_index=False)
    link = models.ForeignKey("Link", on_delete=models.CASCADE, related_name="viewer_invitations", db_column="linkId", db_index=False)
    group = models.ForeignKey("ViewerGroup", on_delete=models.SET_NULL, related_name="invitations", db_column="groupId", null=True, db_index=False)
    invited_by = models.TextField(db_column="invitedBy")
    custom_message = models.TextField(null=True, db_column="customMessage")
    sent_at = UtcDateTimeField(db_column="sentAt", default=timezone.now)
    status = models.TextField(choices=InvitationStatus.choices, default=InvitationStatus.SENT)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "ViewerInvitation"
        indexes = [
            models.Index(fields=["viewer"]),
            models.Index(fields=["link"]),
            models.Index(fields=["group"]),
        ]


class Team(models.Model):
    id = models.TextField(primary_key=True, default=None)
    name = models.TextField()
    slug = models.TextField(null=True, unique=True)
    default_brand = models.ForeignKey("Brand", on_delete=models.SET_NULL, related_name="default_for_team", db_column="defaultBrandId", null=True, db_index=False)
    plan = models.TextField(default="free")
    stripe_id = models.TextField(null=True, unique=True, db_column="stripeId")
    subscription_id = models.TextField(null=True, unique=True, db_column="subscriptionId")
    starts_at = UtcDateTimeField(null=True, db_column="startsAt")
    ends_at = UtcDateTimeField(null=True, db_column="endsAt")
    paused_at = UtcDateTimeField(null=True, db_column="pausedAt")
    pause_starts_at = UtcDateTimeField(null=True, db_column="pauseStartsAt")
    pause_ends_at = UtcDateTimeField(null=True, db_column="pauseEndsAt")
    cancelled_at = UtcDateTimeField(null=True, db_column="cancelledAt")
    trial_started_at = UtcDateTimeField(null=True, db_column="trialStartedAt")
    trial_ends_at = UtcDateTimeField(null=True, db_column="trialEndsAt")
    trial_reminder_run_id = models.TextField(null=True, db_column="trialReminderRunId")
    trial_expired_run_id = models.TextField(null=True, db_column="trialExpiredRunId")
    limits = models.JSONField(null=True)
    enable_excel_advanced_mode = models.BooleanField(db_column="enableExcelAdvancedMode", default=False)
    replicate_dataroom_folders = models.BooleanField(db_column="replicateDataroomFolders", default=True)
    timezone = models.TextField(default="Etc/UTC")
    sso_enabled = models.BooleanField(db_column="ssoEnabled", default=False)
    sso_email_domain = models.TextField(null=True, unique=True, db_column="ssoEmailDomain")
    sso_enforced_at = UtcDateTimeField(null=True, db_column="ssoEnforcedAt")
    survey_data = models.JSONField(null=True, db_column="surveyData")
    agents_enabled = models.BooleanField(db_column="agentsEnabled", default=False)
    vector_store_id = models.TextField(null=True, db_column="vectorStoreId")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    ignored_domains = ArrayField(models.TextField(), db_column="ignoredDomains")
    global_block_list = ArrayField(models.TextField(), db_column="globalBlockList")
    referral_track = models.TextField(choices=ReferralTrack.choices, null=True, db_column="referralTrack")
    referral_track_selected_at = UtcDateTimeField(null=True, db_column="referralTrackSelectedAt")
    referral_profile = models.JSONField(null=True, db_column="referralProfile")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Team"
        indexes = [
            models.Index(fields=["default_brand"]),
        ]


class UserDataroom(models.Model):
    pk = models.CompositePrimaryKey("user_id", "dataroom_id")
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="user_datarooms", db_column="userId", db_index=False)
    team = models.ForeignKey("Team", on_delete=models.CASCADE, related_name="user_datarooms", db_column="teamId", db_index=False)
    dataroom = models.ForeignKey("Dataroom", on_delete=models.CASCADE, related_name="user_datarooms", db_column="dataroomId", db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "UserDataroom"
        indexes = [
            models.Index(fields=["user"]),
            models.Index(fields=["team"]),
            models.Index(fields=["dataroom"]),
        ]


class UserTeam(models.Model):
    pk = models.CompositePrimaryKey("user_id", "team_id")
    role = models.TextField(choices=Role.choices, default=Role.MEMBER)
    status = models.TextField(default="ACTIVE")
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="teams", db_column="userId", db_index=False)
    team = models.ForeignKey("Team", on_delete=models.CASCADE, related_name="users", db_column="teamId", db_index=False)
    blocked_at = UtcDateTimeField(null=True, db_column="blockedAt")
    notification_preferences = models.JSONField(null=True, db_column="notificationPreferences")

    class Meta:
        db_table = "UserTeam"
        indexes = [
            models.Index(fields=["user"]),
            models.Index(fields=["team"]),
        ]


class Workflow(models.Model):
    id = models.TextField(primary_key=True, default=None)
    name = models.TextField()
    description = models.TextField(null=True)
    entry_link = models.OneToOneField("Link", on_delete=models.CASCADE, related_name="workflow", db_column="entryLinkId")
    team = models.ForeignKey("Team", on_delete=models.CASCADE, related_name="workflows", db_column="teamId", db_index=False)
    is_active = models.BooleanField(db_column="isActive", default=True)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Workflow"
        indexes = [
            models.Index(fields=["entry_link"]),
            models.Index(fields=["team"]),
            models.Index(fields=["is_active"]),
        ]


class WorkflowStep(models.Model):
    id = models.TextField(primary_key=True, default=None)
    workflow = models.ForeignKey("Workflow", on_delete=models.CASCADE, related_name="steps", db_column="workflowId", db_index=False)
    name = models.TextField()
    step_order = models.IntegerField(db_column="stepOrder")
    step_type = models.TextField(choices=WorkflowStepType.choices, db_column="stepType", default=WorkflowStepType.ROUTER)
    conditions = models.JSONField()
    actions = models.JSONField()
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "WorkflowStep"
        constraints = [
            models.UniqueConstraint(fields=["workflow", "step_order"], name="WorkflowStep_workflowId_stepOrder_key"),
        ]
        indexes = [
            models.Index(fields=["workflow"]),
        ]


class WorkflowExecution(models.Model):
    id = models.TextField(primary_key=True, default=None)
    workflow = models.ForeignKey("Workflow", on_delete=models.CASCADE, related_name="executions", db_column="workflowId", db_index=False)
    visitor_email = models.TextField(null=True, db_column="visitorEmail")
    visitor_ip = models.TextField(null=True, db_column="visitorIp")
    status = models.TextField(choices=ExecutionStatus.choices)
    started_at = UtcDateTimeField(db_column="startedAt", default=timezone.now)
    completed_at = UtcDateTimeField(null=True, db_column="completedAt")
    result = models.JSONField(null=True)
    metadata = models.JSONField(null=True)

    class Meta:
        db_table = "WorkflowExecution"
        indexes = [
            models.Index(fields=["workflow", "started_at"]),
            models.Index(fields=["visitor_email"]),
            models.Index(fields=["status"]),
        ]


class WorkflowStepLog(models.Model):
    id = models.TextField(primary_key=True, default=None)
    execution = models.ForeignKey("WorkflowExecution", on_delete=models.CASCADE, related_name="step_logs", db_column="executionId", db_index=False)
    step = models.ForeignKey("WorkflowStep", on_delete=models.CASCADE, related_name="execution_logs", db_column="workflowStepId", db_index=False)
    conditions_matched = models.BooleanField(db_column="conditionsMatched")
    condition_results = models.JSONField(null=True, db_column="conditionResults")
    actions_executed = models.JSONField(null=True, db_column="actionsExecuted")
    executed_at = UtcDateTimeField(db_column="executedAt", default=timezone.now)
    duration = models.IntegerField(null=True)
    error = models.TextField(null=True)

    class Meta:
        db_table = "WorkflowStepLog"
        indexes = [
            models.Index(fields=["execution"]),
            models.Index(fields=["step"]),
        ]
