from sqlalchemy import ARRAY, BigInteger, Dialect, Enum, ForeignKey, Index, Integer, MetaData, Text, TypeDecorator, text
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


class DocumentAnnotation(Base):
    __tablename__ = "DocumentAnnotation"

    id: Mapped[str] = mapped_column(primary_key=True)
    title: Mapped[str]
    content: Mapped[dict[str, Any]] = mapped_column(JSONB)
    pages: Mapped[list[int]] = mapped_column(ARRAY(Integer), nullable=True)
    document_id: Mapped[str] = mapped_column("documentId", ForeignKey("Document.id", ondelete="CASCADE", onupdate="CASCADE", name="DocumentAnnotation_documentId_fkey"))
    team_id: Mapped[str] = mapped_column("teamId", ForeignKey("Team.id", ondelete="CASCADE", onupdate="CASCADE", name="DocumentAnnotation_teamId_fkey"))
    created_by_id: Mapped[str] = mapped_column("createdById", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="DocumentAnnotation_createdById_fkey"))
    is_visible: Mapped[bool] = mapped_column("isVisible", default=True, server_default=text("true"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("DocumentAnnotation_documentId_idx", "documentId"),
        Index("DocumentAnnotation_teamId_idx", "teamId"),
        Index("DocumentAnnotation_createdById_idx", "createdById"),
    )

    document: Mapped["Document"] = relationship(back_populates="annotations")
    team: Mapped["Team"] = relationship(back_populates="annotations")
    created_by: Mapped["User"] = relationship(back_populates="created_annotations")
    images: Mapped[list["AnnotationImage"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="annotation")

class AnnotationImage(Base):
    __tablename__ = "AnnotationImage"

    id: Mapped[str] = mapped_column(primary_key=True)
    filename: Mapped[str]
    url: Mapped[str]
    size: Mapped[Optional[int]]
    mime_type: Mapped[Optional[str]] = mapped_column("mimeType")
    annotation_id: Mapped[str] = mapped_column("annotationId", ForeignKey("DocumentAnnotation.id", ondelete="CASCADE", onupdate="CASCADE", name="AnnotationImage_annotationId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("AnnotationImage_annotationId_idx", "annotationId"),
    )

    annotation: Mapped["DocumentAnnotation"] = relationship(back_populates="images")

class Brand(Base):
    __tablename__ = "Brand"

    id: Mapped[str] = mapped_column(primary_key=True)
    logo: Mapped[Optional[str]]
    hide_logo: Mapped[bool] = mapped_column("hideLogo", default=False, server_default=text("false"))
    banner: Mapped[Optional[str]]
    brand_color: Mapped[Optional[str]] = mapped_column("brandColor")
    accent_color: Mapped[Optional[str]] = mapped_column("accentColor")
    accent_button_color: Mapped[Optional[str]] = mapped_column("accentButtonColor")
    apply_accent_color_to_dataroom_view: Mapped[bool] = mapped_column("applyAccentColorToDataroomView", default=False, server_default=text("false"))
    welcome_message: Mapped[Optional[str]] = mapped_column("welcomeMessage")
    cta_label: Mapped[Optional[str]] = mapped_column("ctaLabel")
    cta_url: Mapped[Optional[str]] = mapped_column("ctaUrl")
    privacy_policy_url: Mapped[Optional[str]] = mapped_column("privacyPolicyUrl")
    card_layout: Mapped[str] = mapped_column("cardLayout", default="LIST", server_default=text("'LIST'"))
    show_folder_tree: Mapped[bool] = mapped_column("showFolderTree", default=True, server_default=text("true"))
    viewer_layout_preset: Mapped[str] = mapped_column("viewerLayoutPreset", default="STANDARD", server_default=text("'STANDARD'"))
    viewer_header_style: Mapped[str] = mapped_column("viewerHeaderStyle", default="DEFAULT", server_default=text("'DEFAULT'"))
    hide_folder_icons_in_main: Mapped[bool] = mapped_column("hideFolderIconsInMain", default=False, server_default=text("false"))
    custom_link_preview_enabled: Mapped[bool] = mapped_column("customLinkPreviewEnabled", default=False, server_default=text("false"))
    link_preview_title: Mapped[Optional[str]] = mapped_column("linkPreviewTitle")
    link_preview_description: Mapped[Optional[str]] = mapped_column("linkPreviewDescription")
    link_preview_image: Mapped[Optional[str]] = mapped_column("linkPreviewImage")
    link_preview_favicon: Mapped[Optional[str]] = mapped_column("linkPreviewFavicon")
    default_language: Mapped[str] = mapped_column("defaultLanguage", default="en", server_default=text("'en'"))
    name: Mapped[str] = mapped_column(default="Default", server_default=text("'Default'"))
    team_id: Mapped[str] = mapped_column("teamId", ForeignKey("Team.id", ondelete="CASCADE", onupdate="CASCADE", name="Brand_teamId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("Brand_teamId_idx", "teamId"),
        Index("Brand_teamId_name_key", "teamId", "name", unique=True),
    )

    team: Mapped["Team"] = relationship(back_populates="brands")
    default_for_team: Mapped[list["Team"]] = relationship(back_populates="default_brand")
    links: Mapped[list["Link"]] = relationship(back_populates="brand")
    datarooms: Mapped[list["Dataroom"]] = relationship(back_populates="base_brand")

class Conversation(Base):
    __tablename__ = "Conversation"

    id: Mapped[str] = mapped_column(primary_key=True)
    title: Mapped[Optional[str]]
    is_enabled: Mapped[bool] = mapped_column("isEnabled", default=True, server_default=text("true"))
    visibility_mode: Mapped[str] = mapped_column("visibilityMode", Enum("PRIVATE", "PUBLIC_LINK", "PUBLIC_GROUP", "PUBLIC_DOCUMENT", "PUBLIC_DATAROOM", name="ConversationVisibility"), default="PRIVATE", server_default=text("'PRIVATE'"))
    dataroom_id: Mapped[str] = mapped_column("dataroomId", ForeignKey("Dataroom.id", ondelete="CASCADE", onupdate="CASCADE", name="Conversation_dataroomId_fkey"))
    dataroom_document_id: Mapped[Optional[str]] = mapped_column("dataroomDocumentId", ForeignKey("DataroomDocument.id", ondelete="SET NULL", onupdate="CASCADE", name="Conversation_dataroomDocumentId_fkey"))
    document_version_number: Mapped[Optional[int]] = mapped_column("documentVersionNumber")
    document_page_number: Mapped[Optional[int]] = mapped_column("documentPageNumber")
    link_id: Mapped[Optional[str]] = mapped_column("linkId", ForeignKey("Link.id", ondelete="SET NULL", onupdate="CASCADE", name="Conversation_linkId_fkey"))
    viewer_group_id: Mapped[Optional[str]] = mapped_column("viewerGroupId", ForeignKey("ViewerGroup.id", ondelete="SET NULL", onupdate="CASCADE", name="Conversation_viewerGroupId_fkey"))
    question_id: Mapped[Optional[str]] = mapped_column("questionId", ForeignKey("DataroomQuestion.id", ondelete="CASCADE", onupdate="CASCADE", name="Conversation_questionId_fkey"))
    origin: Mapped[str] = mapped_column(default="visitor", server_default=text("'visitor'"))
    category: Mapped[Optional[str]]
    priority: Mapped[Optional[str]]
    recipient_viewer_id: Mapped[Optional[str]] = mapped_column("recipientViewerId", ForeignKey("Viewer.id", ondelete="SET NULL", onupdate="CASCADE", name="Conversation_recipientViewerId_fkey"))
    initial_view_id: Mapped[Optional[str]] = mapped_column("initialViewId", ForeignKey("View.id", ondelete="SET NULL", onupdate="CASCADE", name="Conversation_initialViewId_fkey"))
    team_id: Mapped[str] = mapped_column("teamId", ForeignKey("Team.id", ondelete="CASCADE", onupdate="CASCADE", name="Conversation_teamId_fkey"))
    last_message_at: Mapped[Optional[datetime]] = mapped_column("lastMessageAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("Conversation_dataroomId_idx", "dataroomId"),
        Index("Conversation_dataroomDocumentId_idx", "dataroomDocumentId"),
        Index("Conversation_linkId_idx", "linkId"),
        Index("Conversation_teamId_idx", "teamId"),
        Index("Conversation_viewerGroupId_idx", "viewerGroupId"),
        Index("Conversation_initialViewId_idx", "initialViewId"),
        Index("Conversation_questionId_idx", "questionId"),
        Index("Conversation_recipientViewerId_idx", "recipientViewerId"),
        Index("Conversation_questionId_recipientViewerId_key", "questionId", "recipientViewerId", unique=True),
    )

    dataroom: Mapped["Dataroom"] = relationship(back_populates="conversations")
    dataroom_document: Mapped[Optional["DataroomDocument"]] = relationship(back_populates="conversations")
    link: Mapped[Optional["Link"]] = relationship(back_populates="conversations")
    viewer_group: Mapped[Optional["ViewerGroup"]] = relationship(back_populates="conversations")
    question: Mapped[Optional["DataroomQuestion"]] = relationship(back_populates="conversations")
    recipient_viewer: Mapped[Optional["Viewer"]] = relationship(back_populates="recipient_conversations")
    initial_view: Mapped[Optional["View"]] = relationship(back_populates="initial_conversations")
    team: Mapped["Team"] = relationship(back_populates="conversations")
    views: Mapped[list["ConversationView"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="conversation")
    participants: Mapped[list["ConversationParticipant"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="conversation")
    messages: Mapped[list["Message"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="conversation")
    faq_items: Mapped[list["DataroomFaqItem"]] = relationship(back_populates="source_conversation")

class DataroomQuestion(Base):
    __tablename__ = "DataroomQuestion"

    id: Mapped[str] = mapped_column(primary_key=True)
    content: Mapped[str]
    category: Mapped[Optional[str]]
    priority: Mapped[Optional[str]]
    status: Mapped[str] = mapped_column(default="unanswered", server_default=text("'unanswered'"))
    order_index: Mapped[Optional[int]] = mapped_column("orderIndex")
    import_batch_id: Mapped[Optional[str]] = mapped_column("importBatchId")
    dataroom_id: Mapped[str] = mapped_column("dataroomId", ForeignKey("Dataroom.id", ondelete="CASCADE", onupdate="CASCADE", name="DataroomQuestion_dataroomId_fkey"))
    team_id: Mapped[str] = mapped_column("teamId", ForeignKey("Team.id", ondelete="CASCADE", onupdate="CASCADE", name="DataroomQuestion_teamId_fkey"))
    created_by_user_id: Mapped[Optional[str]] = mapped_column("createdByUserId", ForeignKey("User.id", ondelete="SET NULL", onupdate="CASCADE", name="DataroomQuestion_createdByUserId_fkey"))
    dataroom_document_id: Mapped[Optional[str]] = mapped_column("dataroomDocumentId", ForeignKey("DataroomDocument.id", ondelete="SET NULL", onupdate="CASCADE", name="DataroomQuestion_dataroomDocumentId_fkey"))
    document_page_number: Mapped[Optional[int]] = mapped_column("documentPageNumber")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("DataroomQuestion_dataroomId_idx", "dataroomId"),
        Index("DataroomQuestion_teamId_idx", "teamId"),
        Index("DataroomQuestion_status_idx", "status"),
        Index("DataroomQuestion_importBatchId_idx", "importBatchId"),
        Index("DataroomQuestion_dataroomDocumentId_idx", "dataroomDocumentId"),
        Index("DataroomQuestion_createdByUserId_idx", "createdByUserId"),
    )

    dataroom: Mapped["Dataroom"] = relationship(back_populates="questions")
    team: Mapped["Team"] = relationship(back_populates="dataroom_questions")
    created_by_user: Mapped[Optional["User"]] = relationship(back_populates="created_dataroom_questions")
    dataroom_document: Mapped[Optional["DataroomDocument"]] = relationship(back_populates="questions")
    assignments: Mapped[list["DataroomQuestionAssignment"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="question")
    conversations: Mapped[list["Conversation"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="question")

class DataroomQuestionAssignment(Base):
    __tablename__ = "DataroomQuestionAssignment"

    id: Mapped[str] = mapped_column(primary_key=True)
    question_id: Mapped[str] = mapped_column("questionId", ForeignKey("DataroomQuestion.id", ondelete="CASCADE", onupdate="CASCADE", name="DataroomQuestionAssignment_questionId_fkey"))
    link_id: Mapped[Optional[str]] = mapped_column("linkId", ForeignKey("Link.id", ondelete="CASCADE", onupdate="CASCADE", name="DataroomQuestionAssignment_linkId_fkey"))
    group_id: Mapped[Optional[str]] = mapped_column("groupId", ForeignKey("ViewerGroup.id", ondelete="CASCADE", onupdate="CASCADE", name="DataroomQuestionAssignment_groupId_fkey"))
    viewer_id: Mapped[Optional[str]] = mapped_column("viewerId", ForeignKey("Viewer.id", ondelete="CASCADE", onupdate="CASCADE", name="DataroomQuestionAssignment_viewerId_fkey"))
    email: Mapped[Optional[str]]
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("DataroomQuestionAssignment_questionId_idx", "questionId"),
        Index("DataroomQuestionAssignment_linkId_idx", "linkId"),
        Index("DataroomQuestionAssignment_groupId_idx", "groupId"),
        Index("DataroomQuestionAssignment_viewerId_idx", "viewerId"),
        Index("DataroomQuestionAssignment_questionId_linkId_key", "questionId", "linkId", unique=True),
        Index("DataroomQuestionAssignment_questionId_groupId_key", "questionId", "groupId", unique=True),
        Index("DataroomQuestionAssignment_questionId_viewerId_key", "questionId", "viewerId", unique=True),
        Index("DataroomQuestionAssignment_questionId_email_key", "questionId", "email", unique=True),
    )

    question: Mapped["DataroomQuestion"] = relationship(back_populates="assignments")
    link: Mapped[Optional["Link"]] = relationship(back_populates="question_assignments")
    group: Mapped[Optional["ViewerGroup"]] = relationship(back_populates="question_assignments")
    viewer: Mapped[Optional["Viewer"]] = relationship(back_populates="question_assignments")

class ConversationParticipant(Base):
    __tablename__ = "ConversationParticipant"

    id: Mapped[str] = mapped_column(primary_key=True)
    conversation_id: Mapped[str] = mapped_column("conversationId", ForeignKey("Conversation.id", ondelete="CASCADE", onupdate="CASCADE", name="ConversationParticipant_conversationId_fkey"))
    role: Mapped[str] = mapped_column(Enum("OWNER", "PARTICIPANT", name="ParticipantRole"), default="PARTICIPANT", server_default=text("'PARTICIPANT'"))
    viewer_id: Mapped[Optional[str]] = mapped_column("viewerId", ForeignKey("Viewer.id", ondelete="SET NULL", onupdate="CASCADE", name="ConversationParticipant_viewerId_fkey"))
    user_id: Mapped[Optional[str]] = mapped_column("userId", ForeignKey("User.id", ondelete="SET NULL", onupdate="CASCADE", name="ConversationParticipant_userId_fkey"))
    receive_notifications: Mapped[bool] = mapped_column("receiveNotifications", default=False, server_default=text("false"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("ConversationParticipant_conversationId_idx", "conversationId"),
        Index("ConversationParticipant_viewerId_idx", "viewerId"),
        Index("ConversationParticipant_userId_idx", "userId"),
        Index("ConversationParticipant_conversationId_viewerId_key", "conversationId", "viewerId", unique=True),
        Index("ConversationParticipant_conversationId_userId_key", "conversationId", "userId", unique=True),
    )

    conversation: Mapped["Conversation"] = relationship(back_populates="participants")
    viewer: Mapped[Optional["Viewer"]] = relationship(back_populates="participated_conversations")
    user: Mapped[Optional["User"]] = relationship(back_populates="participated_conversations")

class Message(Base):
    __tablename__ = "Message"

    id: Mapped[str] = mapped_column(primary_key=True)
    content: Mapped[str]
    conversation_id: Mapped[str] = mapped_column("conversationId", ForeignKey("Conversation.id", ondelete="CASCADE", onupdate="CASCADE", name="Message_conversationId_fkey"))
    user_id: Mapped[Optional[str]] = mapped_column("userId", ForeignKey("User.id", ondelete="SET NULL", onupdate="CASCADE", name="Message_userId_fkey"))
    viewer_id: Mapped[Optional[str]] = mapped_column("viewerId", ForeignKey("Viewer.id", ondelete="SET NULL", onupdate="CASCADE", name="Message_viewerId_fkey"))
    view_id: Mapped[Optional[str]] = mapped_column("viewId", ForeignKey("View.id", ondelete="SET NULL", onupdate="CASCADE", name="Message_viewId_fkey"))
    is_read: Mapped[bool] = mapped_column("isRead", default=False, server_default=text("false"))
    is_internal_note: Mapped[bool] = mapped_column("isInternalNote", default=False, server_default=text("false"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("Message_conversationId_idx", "conversationId"),
        Index("Message_userId_idx", "userId"),
        Index("Message_viewerId_idx", "viewerId"),
        Index("Message_viewId_idx", "viewId"),
    )

    conversation: Mapped["Conversation"] = relationship(back_populates="messages")
    user: Mapped[Optional["User"]] = relationship(back_populates="messages")
    viewer: Mapped[Optional["Viewer"]] = relationship(back_populates="messages")
    view: Mapped[Optional["View"]] = relationship(back_populates="messages")
    faq_as_question: Mapped[list["DataroomFaqItem"]] = relationship(foreign_keys="DataroomFaqItem.question_message_id", back_populates="question_message")
    faq_as_answer: Mapped[list["DataroomFaqItem"]] = relationship(foreign_keys="DataroomFaqItem.answer_message_id", back_populates="answer_message")

class ConversationView(Base):
    __tablename__ = "ConversationView"

    id: Mapped[str] = mapped_column(primary_key=True)
    conversation_id: Mapped[str] = mapped_column("conversationId", ForeignKey("Conversation.id", ondelete="CASCADE", onupdate="CASCADE", name="ConversationView_conversationId_fkey"))
    view_id: Mapped[str] = mapped_column("viewId", ForeignKey("View.id", ondelete="CASCADE", onupdate="CASCADE", name="ConversationView_viewId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("ConversationView_conversationId_idx", "conversationId"),
        Index("ConversationView_viewId_idx", "viewId"),
        Index("ConversationView_conversationId_viewId_key", "conversationId", "viewId", unique=True),
    )

    conversation: Mapped["Conversation"] = relationship(back_populates="views")
    view: Mapped["View"] = relationship(back_populates="conversation_views")

class DataroomFaqItem(Base):
    __tablename__ = "DataroomFaqItem"

    id: Mapped[str] = mapped_column(primary_key=True)
    title: Mapped[Optional[str]]
    edited_question: Mapped[str] = mapped_column("editedQuestion")
    original_question: Mapped[Optional[str]] = mapped_column("originalQuestion")
    answer: Mapped[str]
    description: Mapped[Optional[str]]
    dataroom_id: Mapped[str] = mapped_column("dataroomId", ForeignKey("Dataroom.id", ondelete="CASCADE", onupdate="CASCADE", name="DataroomFaqItem_dataroomId_fkey"))
    link_id: Mapped[Optional[str]] = mapped_column("linkId", ForeignKey("Link.id", ondelete="SET NULL", onupdate="CASCADE", name="DataroomFaqItem_linkId_fkey"))
    dataroom_document_id: Mapped[Optional[str]] = mapped_column("dataroomDocumentId", ForeignKey("DataroomDocument.id", ondelete="SET NULL", onupdate="CASCADE", name="DataroomFaqItem_dataroomDocumentId_fkey"))
    source_conversation_id: Mapped[Optional[str]] = mapped_column("sourceConversationId", ForeignKey("Conversation.id", ondelete="SET NULL", onupdate="CASCADE", name="DataroomFaqItem_sourceConversationId_fkey"))
    question_message_id: Mapped[Optional[str]] = mapped_column("questionMessageId", ForeignKey("Message.id", ondelete="SET NULL", onupdate="CASCADE", name="DataroomFaqItem_questionMessageId_fkey"))
    answer_message_id: Mapped[Optional[str]] = mapped_column("answerMessageId", ForeignKey("Message.id", ondelete="SET NULL", onupdate="CASCADE", name="DataroomFaqItem_answerMessageId_fkey"))
    team_id: Mapped[str] = mapped_column("teamId", ForeignKey("Team.id", ondelete="CASCADE", onupdate="CASCADE", name="DataroomFaqItem_teamId_fkey"))
    published_by_user_id: Mapped[str] = mapped_column("publishedByUserId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="DataroomFaqItem_publishedByUserId_fkey"))
    visibility_mode: Mapped[str] = mapped_column("visibilityMode", Enum("PUBLIC_DATAROOM", "PUBLIC_LINK", "PUBLIC_DOCUMENT", name="FaqVisibility"), default="PUBLIC_DATAROOM", server_default=text("'PUBLIC_DATAROOM'"))
    status: Mapped[str] = mapped_column(Enum("DRAFT", "PUBLISHED", "ARCHIVED", name="FaqStatus"), default="PUBLISHED", server_default=text("'PUBLISHED'"))
    is_anonymized: Mapped[bool] = mapped_column("isAnonymized", default=True, server_default=text("true"))
    view_count: Mapped[int] = mapped_column("viewCount", default=0, server_default=text("0"))
    tags: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    document_page_number: Mapped[Optional[int]] = mapped_column("documentPageNumber")
    document_version_number: Mapped[Optional[int]] = mapped_column("documentVersionNumber")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("DataroomFaqItem_dataroomId_idx", "dataroomId"),
        Index("DataroomFaqItem_linkId_idx", "linkId"),
        Index("DataroomFaqItem_dataroomDocumentId_idx", "dataroomDocumentId"),
        Index("DataroomFaqItem_sourceConversationId_idx", "sourceConversationId"),
        Index("DataroomFaqItem_teamId_idx", "teamId"),
        Index("DataroomFaqItem_publishedByUserId_idx", "publishedByUserId"),
        Index("DataroomFaqItem_status_idx", "status"),
        Index("DataroomFaqItem_visibilityMode_idx", "visibilityMode"),
        Index("DataroomFaqItem_createdAt_idx", "createdAt"),
    )

    dataroom: Mapped["Dataroom"] = relationship(back_populates="faq_items")
    link: Mapped[Optional["Link"]] = relationship(back_populates="dataroom_faq_items")
    dataroom_document: Mapped[Optional["DataroomDocument"]] = relationship(back_populates="dataroom_faqs")
    source_conversation: Mapped[Optional["Conversation"]] = relationship(back_populates="faq_items")
    question_message: Mapped[Optional["Message"]] = relationship(foreign_keys=[question_message_id], back_populates="faq_as_question")
    answer_message: Mapped[Optional["Message"]] = relationship(foreign_keys=[answer_message_id], back_populates="faq_as_answer")
    team: Mapped["Team"] = relationship(back_populates="dataroom_faq_items")
    published_by_user: Mapped["User"] = relationship(back_populates="published_faq_items")

class Dataroom(Base):
    __tablename__ = "Dataroom"

    id: Mapped[str] = mapped_column(primary_key=True)
    p_id: Mapped[str] = mapped_column("pId")
    name: Mapped[str]
    internal_name: Mapped[Optional[str]] = mapped_column("internalName")
    description: Mapped[Optional[str]]
    team_id: Mapped[str] = mapped_column("teamId", ForeignKey("Team.id", ondelete="CASCADE", onupdate="CASCADE", name="Dataroom_teamId_fkey"))
    brand_id: Mapped[Optional[str]] = mapped_column("brandId", ForeignKey("Brand.id", ondelete="SET NULL", onupdate="CASCADE", name="Dataroom_brandId_fkey"))
    conversations_enabled: Mapped[bool] = mapped_column("conversationsEnabled", default=False, server_default=text("false"))
    agents_enabled: Mapped[bool] = mapped_column("agentsEnabled", default=False, server_default=text("false"))
    vector_store_id: Mapped[Optional[str]] = mapped_column("vectorStoreId")
    request_list_enabled: Mapped[bool] = mapped_column("requestListEnabled", default=False, server_default=text("false"))
    enable_change_notifications: Mapped[bool] = mapped_column("enableChangeNotifications", default=False, server_default=text("false"))
    enable_visitor_upload_change_notifications: Mapped[bool] = mapped_column("enableVisitorUploadChangeNotifications", default=False, server_default=text("false"))
    default_permission_strategy: Mapped[str] = mapped_column("defaultPermissionStrategy", Enum("INHERIT_FROM_PARENT", "ASK_EVERY_TIME", "HIDDEN_BY_DEFAULT", name="DefaultPermissionStrategy"), default="INHERIT_FROM_PARENT", server_default=text("'INHERIT_FROM_PARENT'"))
    default_show_banner: Mapped[bool] = mapped_column("defaultShowBanner", default=True, server_default=text("true"))
    default_group_permission_strategy: Mapped[str] = mapped_column("defaultGroupPermissionStrategy", Enum("INHERIT_FROM_PARENT", "ASK_EVERY_TIME", "HIDDEN_BY_DEFAULT", name="DefaultPermissionStrategy"), default="INHERIT_FROM_PARENT", server_default=text("'INHERIT_FROM_PARENT'"))
    default_root_item_access: Mapped[str] = mapped_column("defaultRootItemAccess", Enum("VIEW_ONLY", "VIEW_AND_DOWNLOAD", "HIDDEN", name="RootItemAccess"), default="VIEW_ONLY", server_default=text("'VIEW_ONLY'"))
    default_group_root_item_access: Mapped[str] = mapped_column("defaultGroupRootItemAccess", Enum("VIEW_ONLY", "VIEW_AND_DOWNLOAD", "HIDDEN", name="RootItemAccess"), default="VIEW_ONLY", server_default=text("'VIEW_ONLY'"))
    allow_bulk_download: Mapped[bool] = mapped_column("allowBulkDownload", default=True, server_default=text("true"))
    show_last_updated: Mapped[bool] = mapped_column("showLastUpdated", default=True, server_default=text("true"))
    introduction_enabled: Mapped[bool] = mapped_column("introductionEnabled", default=False, server_default=text("false"))
    introduction_content: Mapped[Optional[dict[str, Any]]] = mapped_column("introductionContent", JSONB(none_as_null=True))
    is_frozen: Mapped[bool] = mapped_column("isFrozen", default=False, server_default=text("false"))
    frozen_at: Mapped[Optional[datetime]] = mapped_column("frozenAt")
    frozen_by: Mapped[Optional[str]] = mapped_column("frozenBy", ForeignKey("User.id", ondelete="SET NULL", onupdate="CASCADE", name="Dataroom_frozenBy_fkey"))
    freeze_archive_url: Mapped[Optional[str]] = mapped_column("freezeArchiveUrl")
    freeze_archive_hash: Mapped[Optional[str]] = mapped_column("freezeArchiveHash")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("Dataroom_pId_key", "pId", unique=True),
        Index("Dataroom_teamId_idx", "teamId"),
        Index("Dataroom_brandId_idx", "brandId"),
    )

    team: Mapped["Team"] = relationship(back_populates="datarooms")
    base_brand: Mapped[Optional["Brand"]] = relationship(back_populates="datarooms")
    frozen_by_user: Mapped[Optional["User"]] = relationship(back_populates="frozen_datarooms")
    documents: Mapped[list["DataroomDocument"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="dataroom")
    folders: Mapped[list["DataroomFolder"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="dataroom")
    links: Mapped[list["Link"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="dataroom")
    views: Mapped[list["View"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="dataroom")
    viewers: Mapped[list["Viewer"]] = relationship(back_populates="dataroom")
    viewer_groups: Mapped[list["ViewerGroup"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="dataroom")
    user_datarooms: Mapped[list["UserDataroom"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="dataroom")
    permission_groups: Mapped[list["PermissionGroup"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="dataroom")
    conversations: Mapped[list["Conversation"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="dataroom")
    questions: Mapped[list["DataroomQuestion"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="dataroom")
    faq_items: Mapped[list["DataroomFaqItem"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="dataroom")
    chats: Mapped[list["Chat"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="dataroom")
    uploaded_documents: Mapped[list["DocumentUpload"]] = relationship(back_populates="dataroom")
    tags: Mapped[list["TagItem"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="dataroom")
    task_lists: Mapped[list["TaskList"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="dataroom")
    tasks: Mapped[list["Task"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="dataroom")
    brand: Mapped[Optional["DataroomBrand"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="dataroom")

class TaskList(Base):
    __tablename__ = "TaskList"

    id: Mapped[str] = mapped_column(primary_key=True)
    name: Mapped[str]
    order_index: Mapped[Optional[int]] = mapped_column("orderIndex")
    dataroom_id: Mapped[str] = mapped_column("dataroomId", ForeignKey("Dataroom.id", ondelete="CASCADE", onupdate="CASCADE", name="TaskList_dataroomId_fkey"))
    team_id: Mapped[str] = mapped_column("teamId", ForeignKey("Team.id", ondelete="CASCADE", onupdate="CASCADE", name="TaskList_teamId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("TaskList_dataroomId_idx", "dataroomId"),
        Index("TaskList_teamId_idx", "teamId"),
        Index("TaskList_dataroomId_orderIndex_idx", "dataroomId", "orderIndex"),
    )

    dataroom: Mapped["Dataroom"] = relationship(back_populates="task_lists")
    team: Mapped["Team"] = relationship(back_populates="task_lists")
    tasks: Mapped[list["Task"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="task_list")

class Task(Base):
    __tablename__ = "Task"

    id: Mapped[str] = mapped_column(primary_key=True)
    title: Mapped[str]
    description: Mapped[Optional[str]]
    type: Mapped[str] = mapped_column(default="TODO", server_default=text("'TODO'"))
    status: Mapped[str] = mapped_column(default="OPEN", server_default=text("'OPEN'"))
    category: Mapped[Optional[str]]
    due_date: Mapped[Optional[datetime]] = mapped_column("dueDate")
    order_index: Mapped[Optional[int]] = mapped_column("orderIndex")
    task_list_id: Mapped[str] = mapped_column("taskListId", ForeignKey("TaskList.id", ondelete="CASCADE", onupdate="CASCADE", name="Task_taskListId_fkey"))
    dataroom_id: Mapped[str] = mapped_column("dataroomId", ForeignKey("Dataroom.id", ondelete="CASCADE", onupdate="CASCADE", name="Task_dataroomId_fkey"))
    team_id: Mapped[str] = mapped_column("teamId", ForeignKey("Team.id", ondelete="CASCADE", onupdate="CASCADE", name="Task_teamId_fkey"))
    upload_folder_id: Mapped[Optional[str]] = mapped_column("uploadFolderId", ForeignKey("DataroomFolder.id", ondelete="SET NULL", onupdate="CASCADE", name="Task_uploadFolderId_fkey"))
    created_by_user_id: Mapped[Optional[str]] = mapped_column("createdByUserId", ForeignKey("User.id", ondelete="SET NULL", onupdate="CASCADE", name="Task_createdByUserId_fkey"))
    last_activity_notified_at: Mapped[Optional[datetime]] = mapped_column("lastActivityNotifiedAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("Task_taskListId_idx", "taskListId"),
        Index("Task_dataroomId_idx", "dataroomId"),
        Index("Task_teamId_idx", "teamId"),
        Index("Task_uploadFolderId_idx", "uploadFolderId"),
        Index("Task_taskListId_orderIndex_idx", "taskListId", "orderIndex"),
    )

    task_list: Mapped["TaskList"] = relationship(back_populates="tasks")
    dataroom: Mapped["Dataroom"] = relationship(back_populates="tasks")
    team: Mapped["Team"] = relationship(back_populates="tasks")
    upload_folder: Mapped[Optional["DataroomFolder"]] = relationship(back_populates="upload_tasks")
    created_by_user: Mapped[Optional["User"]] = relationship(back_populates="created_tasks")
    assignments: Mapped[list["TaskAssignment"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="task")
    activities: Mapped[list["TaskActivity"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="task")
    uploads: Mapped[list["DocumentUpload"]] = relationship(back_populates="task")

class TaskAssignment(Base):
    __tablename__ = "TaskAssignment"

    id: Mapped[str] = mapped_column(primary_key=True)
    task_id: Mapped[str] = mapped_column("taskId", ForeignKey("Task.id", ondelete="CASCADE", onupdate="CASCADE", name="TaskAssignment_taskId_fkey"))
    viewer_id: Mapped[Optional[str]] = mapped_column("viewerId", ForeignKey("Viewer.id", ondelete="CASCADE", onupdate="CASCADE", name="TaskAssignment_viewerId_fkey"))
    group_id: Mapped[Optional[str]] = mapped_column("groupId", ForeignKey("ViewerGroup.id", ondelete="CASCADE", onupdate="CASCADE", name="TaskAssignment_groupId_fkey"))
    link_id: Mapped[Optional[str]] = mapped_column("linkId", ForeignKey("Link.id", ondelete="CASCADE", onupdate="CASCADE", name="TaskAssignment_linkId_fkey"))
    email: Mapped[Optional[str]]
    notified_at: Mapped[Optional[datetime]] = mapped_column("notifiedAt")
    last_reminder_at: Mapped[Optional[datetime]] = mapped_column("lastReminderAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("TaskAssignment_taskId_idx", "taskId"),
        Index("TaskAssignment_viewerId_idx", "viewerId"),
        Index("TaskAssignment_groupId_idx", "groupId"),
        Index("TaskAssignment_linkId_idx", "linkId"),
        Index("TaskAssignment_taskId_viewerId_key", "taskId", "viewerId", unique=True),
        Index("TaskAssignment_taskId_groupId_key", "taskId", "groupId", unique=True),
        Index("TaskAssignment_taskId_linkId_key", "taskId", "linkId", unique=True),
        Index("TaskAssignment_taskId_email_key", "taskId", "email", unique=True),
    )

    task: Mapped["Task"] = relationship(back_populates="assignments")
    viewer: Mapped[Optional["Viewer"]] = relationship(back_populates="task_assignments")
    group: Mapped[Optional["ViewerGroup"]] = relationship(back_populates="task_assignments")
    link: Mapped[Optional["Link"]] = relationship(back_populates="task_assignments")

class TaskActivity(Base):
    __tablename__ = "TaskActivity"

    id: Mapped[str] = mapped_column(primary_key=True)
    task_id: Mapped[str] = mapped_column("taskId", ForeignKey("Task.id", ondelete="CASCADE", onupdate="CASCADE", name="TaskActivity_taskId_fkey"))
    type: Mapped[str]
    comment: Mapped[Optional[str]]
    from_status: Mapped[Optional[str]] = mapped_column("fromStatus")
    to_status: Mapped[Optional[str]] = mapped_column("toStatus")
    user_id: Mapped[Optional[str]] = mapped_column("userId", ForeignKey("User.id", ondelete="SET NULL", onupdate="CASCADE", name="TaskActivity_userId_fkey"))
    viewer_id: Mapped[Optional[str]] = mapped_column("viewerId", ForeignKey("Viewer.id", ondelete="SET NULL", onupdate="CASCADE", name="TaskActivity_viewerId_fkey"))
    view_id: Mapped[Optional[str]] = mapped_column("viewId", ForeignKey("View.id", ondelete="SET NULL", onupdate="CASCADE", name="TaskActivity_viewId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("TaskActivity_taskId_idx", "taskId"),
        Index("TaskActivity_userId_idx", "userId"),
        Index("TaskActivity_viewerId_idx", "viewerId"),
        Index("TaskActivity_viewId_idx", "viewId"),
        Index("TaskActivity_taskId_createdAt_idx", "taskId", "createdAt"),
    )

    task: Mapped["Task"] = relationship(back_populates="activities")
    user: Mapped[Optional["User"]] = relationship(back_populates="task_activities")
    viewer: Mapped[Optional["Viewer"]] = relationship(back_populates="task_activities")
    view: Mapped[Optional["View"]] = relationship(back_populates="task_activities")

class DataroomDocument(Base):
    __tablename__ = "DataroomDocument"

    id: Mapped[str] = mapped_column(primary_key=True)
    dataroom_id: Mapped[str] = mapped_column("dataroomId", ForeignKey("Dataroom.id", ondelete="CASCADE", onupdate="CASCADE", name="DataroomDocument_dataroomId_fkey"))
    document_id: Mapped[str] = mapped_column("documentId", ForeignKey("Document.id", ondelete="CASCADE", onupdate="CASCADE", name="DataroomDocument_documentId_fkey"))
    folder_id: Mapped[Optional[str]] = mapped_column("folderId", ForeignKey("DataroomFolder.id", ondelete="SET NULL", onupdate="CASCADE", name="DataroomDocument_folderId_fkey"))
    order_index: Mapped[Optional[int]] = mapped_column("orderIndex")
    hierarchical_index: Mapped[Optional[str]] = mapped_column("hierarchicalIndex")
    vector_store_file_id: Mapped[Optional[str]] = mapped_column("vectorStoreFileId")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("DataroomDocument_folderId_idx", "folderId"),
        Index("DataroomDocument_dataroomId_folderId_orderIndex_idx", "dataroomId", "folderId", "orderIndex"),
        Index("DataroomDocument_documentId_idx", "documentId"),
        Index("DataroomDocument_dataroomId_documentId_key", "dataroomId", "documentId", unique=True),
    )

    dataroom: Mapped["Dataroom"] = relationship(back_populates="documents")
    document: Mapped["Document"] = relationship(back_populates="datarooms")
    folder: Mapped[Optional["DataroomFolder"]] = relationship(back_populates="documents")
    conversations: Mapped[list["Conversation"]] = relationship(back_populates="dataroom_document")
    questions: Mapped[list["DataroomQuestion"]] = relationship(back_populates="dataroom_document")
    dataroom_faqs: Mapped[list["DataroomFaqItem"]] = relationship(back_populates="dataroom_document")
    uploaded_documents: Mapped[list["DocumentUpload"]] = relationship(back_populates="dataroom_document")

class DataroomFolder(Base):
    __tablename__ = "DataroomFolder"

    id: Mapped[str] = mapped_column(primary_key=True)
    name: Mapped[str]
    path: Mapped[str]
    parent_id: Mapped[Optional[str]] = mapped_column("parentId", ForeignKey("DataroomFolder.id", ondelete="SET NULL", onupdate="CASCADE", name="DataroomFolder_parentId_fkey"))
    icon: Mapped[Optional[str]]
    color: Mapped[Optional[str]]
    dataroom_id: Mapped[str] = mapped_column("dataroomId", ForeignKey("Dataroom.id", ondelete="CASCADE", onupdate="CASCADE", name="DataroomFolder_dataroomId_fkey"))
    order_index: Mapped[Optional[int]] = mapped_column("orderIndex")
    hierarchical_index: Mapped[Optional[str]] = mapped_column("hierarchicalIndex")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("DataroomFolder_parentId_idx", "parentId"),
        Index("DataroomFolder_dataroomId_parentId_orderIndex_idx", "dataroomId", "parentId", "orderIndex"),
        Index("DataroomFolder_dataroomId_path_key", "dataroomId", "path", unique=True),
    )

    parent_folder: Mapped[Optional["DataroomFolder"]] = relationship(remote_side=[id], back_populates="child_folders")
    dataroom: Mapped["Dataroom"] = relationship(back_populates="folders")
    documents: Mapped[list["DataroomDocument"]] = relationship(back_populates="folder")
    child_folders: Mapped[list["DataroomFolder"]] = relationship(back_populates="parent_folder")
    upload_tasks: Mapped[list["Task"]] = relationship(back_populates="upload_folder")

class DataroomBrand(Base):
    __tablename__ = "DataroomBrand"

    id: Mapped[str] = mapped_column(primary_key=True)
    logo: Mapped[Optional[str]]
    hide_logo: Mapped[Optional[bool]] = mapped_column("hideLogo")
    banner: Mapped[Optional[str]]
    brand_color: Mapped[Optional[str]] = mapped_column("brandColor")
    accent_color: Mapped[Optional[str]] = mapped_column("accentColor")
    accent_button_color: Mapped[Optional[str]] = mapped_column("accentButtonColor")
    apply_accent_color_to_dataroom_view: Mapped[bool] = mapped_column("applyAccentColorToDataroomView", default=False, server_default=text("false"))
    welcome_message: Mapped[Optional[str]] = mapped_column("welcomeMessage")
    card_layout: Mapped[str] = mapped_column("cardLayout", default="LIST", server_default=text("'LIST'"))
    show_folder_tree: Mapped[bool] = mapped_column("showFolderTree", default=True, server_default=text("true"))
    viewer_layout_preset: Mapped[str] = mapped_column("viewerLayoutPreset", default="STANDARD", server_default=text("'STANDARD'"))
    viewer_header_style: Mapped[str] = mapped_column("viewerHeaderStyle", default="DEFAULT", server_default=text("'DEFAULT'"))
    hide_folder_icons_in_main: Mapped[bool] = mapped_column("hideFolderIconsInMain", default=False, server_default=text("false"))
    cta_label: Mapped[Optional[str]] = mapped_column("ctaLabel")
    cta_url: Mapped[Optional[str]] = mapped_column("ctaUrl")
    custom_link_preview_enabled: Mapped[bool] = mapped_column("customLinkPreviewEnabled", default=False, server_default=text("false"))
    link_preview_title: Mapped[Optional[str]] = mapped_column("linkPreviewTitle")
    link_preview_description: Mapped[Optional[str]] = mapped_column("linkPreviewDescription")
    link_preview_image: Mapped[Optional[str]] = mapped_column("linkPreviewImage")
    link_preview_favicon: Mapped[Optional[str]] = mapped_column("linkPreviewFavicon")
    default_language: Mapped[str] = mapped_column("defaultLanguage", default="en", server_default=text("'en'"))
    dataroom_id: Mapped[str] = mapped_column("dataroomId", ForeignKey("Dataroom.id", ondelete="CASCADE", onupdate="CASCADE", name="DataroomBrand_dataroomId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("DataroomBrand_dataroomId_key", "dataroomId", unique=True),
    )

    dataroom: Mapped["Dataroom"] = relationship(back_populates="brand")

class ViewerGroup(Base):
    __tablename__ = "ViewerGroup"

    id: Mapped[str] = mapped_column(primary_key=True)
    name: Mapped[str]
    domains: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True)
    allow_all: Mapped[bool] = mapped_column("allowAll", default=False, server_default=text("false"))
    dataroom_id: Mapped[str] = mapped_column("dataroomId", ForeignKey("Dataroom.id", ondelete="CASCADE", onupdate="CASCADE", name="ViewerGroup_dataroomId_fkey"))
    team_id: Mapped[str] = mapped_column("teamId", ForeignKey("Team.id", ondelete="CASCADE", onupdate="CASCADE", name="ViewerGroup_teamId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("ViewerGroup_dataroomId_idx", "dataroomId"),
        Index("ViewerGroup_teamId_idx", "teamId"),
        Index("ViewerGroup_dataroomId_createdAt_idx", "dataroomId", "createdAt"),
    )

    dataroom: Mapped["Dataroom"] = relationship(back_populates="viewer_groups")
    team: Mapped["Team"] = relationship(back_populates="viewer_groups")
    members: Mapped[list["ViewerGroupMembership"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="group")
    links: Mapped[list["Link"]] = relationship(back_populates="group")
    access_controls: Mapped[list["ViewerGroupAccessControls"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="group")
    views: Mapped[list["View"]] = relationship(back_populates="group")
    conversations: Mapped[list["Conversation"]] = relationship(back_populates="viewer_group")
    invitations: Mapped[list["ViewerInvitation"]] = relationship(back_populates="group")
    question_assignments: Mapped[list["DataroomQuestionAssignment"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="group")
    task_assignments: Mapped[list["TaskAssignment"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="group")

class ViewerGroupMembership(Base):
    __tablename__ = "ViewerGroupMembership"

    id: Mapped[str] = mapped_column(primary_key=True)
    viewer_id: Mapped[str] = mapped_column("viewerId", ForeignKey("Viewer.id", ondelete="CASCADE", onupdate="CASCADE", name="ViewerGroupMembership_viewerId_fkey"))
    group_id: Mapped[str] = mapped_column("groupId", ForeignKey("ViewerGroup.id", ondelete="CASCADE", onupdate="CASCADE", name="ViewerGroupMembership_groupId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("ViewerGroupMembership_viewerId_idx", "viewerId"),
        Index("ViewerGroupMembership_groupId_idx", "groupId"),
        Index("ViewerGroupMembership_viewerId_groupId_key", "viewerId", "groupId", unique=True),
    )

    viewer: Mapped["Viewer"] = relationship(back_populates="groups")
    group: Mapped["ViewerGroup"] = relationship(back_populates="members")

class ViewerGroupAccessControls(Base):
    __tablename__ = "ViewerGroupAccessControls"

    id: Mapped[str] = mapped_column(primary_key=True)
    group_id: Mapped[str] = mapped_column("groupId", ForeignKey("ViewerGroup.id", ondelete="CASCADE", onupdate="CASCADE", name="ViewerGroupAccessControls_groupId_fkey"))
    item_id: Mapped[str] = mapped_column("itemId")
    item_type: Mapped[str] = mapped_column("itemType", Enum("DATAROOM_DOCUMENT", "DATAROOM_FOLDER", name="ItemType"))
    can_view: Mapped[bool] = mapped_column("canView", default=True, server_default=text("true"))
    can_download: Mapped[bool] = mapped_column("canDownload", default=False, server_default=text("false"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("ViewerGroupAccessControls_groupId_idx", "groupId"),
        Index("ViewerGroupAccessControls_groupId_itemId_key", "groupId", "itemId", unique=True),
    )

    group: Mapped["ViewerGroup"] = relationship(back_populates="access_controls")

class PermissionGroup(Base):
    __tablename__ = "PermissionGroup"

    id: Mapped[str] = mapped_column(primary_key=True)
    name: Mapped[str]
    description: Mapped[Optional[str]]
    dataroom_id: Mapped[str] = mapped_column("dataroomId", ForeignKey("Dataroom.id", ondelete="CASCADE", onupdate="CASCADE", name="PermissionGroup_dataroomId_fkey"))
    team_id: Mapped[str] = mapped_column("teamId", ForeignKey("Team.id", ondelete="CASCADE", onupdate="CASCADE", name="PermissionGroup_teamId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("PermissionGroup_dataroomId_idx", "dataroomId"),
        Index("PermissionGroup_teamId_idx", "teamId"),
    )

    dataroom: Mapped["Dataroom"] = relationship(back_populates="permission_groups")
    team: Mapped["Team"] = relationship(back_populates="permission_groups")
    links: Mapped[list["Link"]] = relationship(back_populates="permission_group")
    access_controls: Mapped[list["PermissionGroupAccessControls"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="group")

class PermissionGroupAccessControls(Base):
    __tablename__ = "PermissionGroupAccessControls"

    id: Mapped[str] = mapped_column(primary_key=True)
    group_id: Mapped[str] = mapped_column("groupId", ForeignKey("PermissionGroup.id", ondelete="CASCADE", onupdate="CASCADE", name="PermissionGroupAccessControls_groupId_fkey"))
    item_id: Mapped[str] = mapped_column("itemId")
    item_type: Mapped[str] = mapped_column("itemType", Enum("DATAROOM_DOCUMENT", "DATAROOM_FOLDER", name="ItemType"))
    can_view: Mapped[bool] = mapped_column("canView", default=True, server_default=text("true"))
    can_download: Mapped[bool] = mapped_column("canDownload", default=False, server_default=text("false"))
    can_download_original: Mapped[bool] = mapped_column("canDownloadOriginal", default=False, server_default=text("false"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("PermissionGroupAccessControls_groupId_idx", "groupId"),
        Index("PermissionGroupAccessControls_groupId_itemId_key", "groupId", "itemId", unique=True),
    )

    group: Mapped["PermissionGroup"] = relationship(back_populates="access_controls")

class Document(Base):
    __tablename__ = "Document"

    id: Mapped[str] = mapped_column(primary_key=True)
    name: Mapped[str]
    description: Mapped[Optional[str]]
    file: Mapped[str]
    original_file: Mapped[Optional[str]] = mapped_column("originalFile")
    type: Mapped[Optional[str]]
    content_type: Mapped[Optional[str]] = mapped_column("contentType")
    storage_type: Mapped[str] = mapped_column("storageType", Enum("S3_PATH", "VERCEL_BLOB", name="DocumentStorageType"), default="VERCEL_BLOB", server_default=text("'VERCEL_BLOB'"))
    num_pages: Mapped[Optional[int]] = mapped_column("numPages")
    team_id: Mapped[str] = mapped_column("teamId", ForeignKey("Team.id", ondelete="CASCADE", onupdate="CASCADE", name="Document_teamId_fkey"))
    owner_id: Mapped[Optional[str]] = mapped_column("ownerId", ForeignKey("User.id", ondelete="SET NULL", onupdate="CASCADE", name="Document_ownerId_fkey"))
    assistant_enabled: Mapped[bool] = mapped_column("assistantEnabled", default=False, server_default=text("false"))
    advanced_excel_enabled: Mapped[bool] = mapped_column("advancedExcelEnabled", default=False, server_default=text("false"))
    agents_enabled: Mapped[bool] = mapped_column("agentsEnabled", default=False, server_default=text("false"))
    download_only: Mapped[bool] = mapped_column("downloadOnly", default=False, server_default=text("false"))
    hidden_in_all_documents: Mapped[bool] = mapped_column("hiddenInAllDocuments", default=False, server_default=text("false"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    folder_id: Mapped[Optional[str]] = mapped_column("folderId", ForeignKey("Folder.id", ondelete="SET NULL", onupdate="CASCADE", name="Document_folderId_fkey"))
    is_external_upload: Mapped[bool] = mapped_column("isExternalUpload", default=False, server_default=text("false"))

    __table_args__ = (
        Index("Document_ownerId_idx", "ownerId"),
        Index("Document_teamId_idx", "teamId"),
        Index("Document_folderId_idx", "folderId"),
        Index("Document_teamId_folderId_idx", "teamId", "folderId"),
        Index("Document_teamId_name_idx", "teamId", "name"),
        Index("Document_teamId_hiddenInAllDocuments_idx", "teamId", "hiddenInAllDocuments"),
    )

    owner: Mapped[Optional["User"]] = relationship(back_populates="documents")
    team: Mapped["Team"] = relationship(back_populates="documents")
    folder: Mapped[Optional["Folder"]] = relationship(back_populates="documents")
    links: Mapped[list["Link"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="document")
    views: Mapped[list["View"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="document")
    versions: Mapped[list["DocumentVersion"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="document")
    chats: Mapped[list["Chat"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="document")
    datarooms: Mapped[list["DataroomDocument"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="document")
    tags: Mapped[list["TagItem"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="document")
    annotations: Mapped[list["DocumentAnnotation"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="document")
    redaction_jobs: Mapped[list["DocumentRedactionJob"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="document")
    uploaded_document: Mapped[list["DocumentUpload"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="document")

class DocumentVersion(Base):
    __tablename__ = "DocumentVersion"

    id: Mapped[str] = mapped_column(primary_key=True)
    version_number: Mapped[int] = mapped_column("versionNumber")
    document_id: Mapped[str] = mapped_column("documentId", ForeignKey("Document.id", ondelete="CASCADE", onupdate="CASCADE", name="DocumentVersion_documentId_fkey"))
    file: Mapped[str]
    original_file: Mapped[Optional[str]] = mapped_column("originalFile")
    type: Mapped[Optional[str]]
    content_type: Mapped[Optional[str]] = mapped_column("contentType")
    file_size: Mapped[Optional[int]] = mapped_column("fileSize", BigInteger)
    storage_type: Mapped[str] = mapped_column("storageType", Enum("S3_PATH", "VERCEL_BLOB", name="DocumentStorageType"), default="VERCEL_BLOB", server_default=text("'VERCEL_BLOB'"))
    num_pages: Mapped[Optional[int]] = mapped_column("numPages")
    is_primary: Mapped[bool] = mapped_column("isPrimary", default=False, server_default=text("false"))
    is_vertical: Mapped[bool] = mapped_column("isVertical", default=False, server_default=text("false"))
    file_id: Mapped[Optional[str]] = mapped_column("fileId")
    vector_store_file_id: Mapped[Optional[str]] = mapped_column("vectorStoreFileId")
    has_pages: Mapped[bool] = mapped_column("hasPages", default=False, server_default=text("false"))
    length: Mapped[Optional[int]]
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("DocumentVersion_documentId_idx", "documentId"),
        Index("DocumentVersion_documentId_isPrimary_idx", "documentId", "isPrimary"),
        Index("DocumentVersion_documentId_createdAt_idx", "documentId", "createdAt"),
        Index("DocumentVersion_documentId_isPrimary_createdAt_idx", "documentId", "isPrimary", "createdAt"),
        Index("DocumentVersion_versionNumber_documentId_key", "versionNumber", "documentId", unique=True),
    )

    document: Mapped["Document"] = relationship(back_populates="versions")
    pages: Mapped[list["DocumentPage"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="version")
    redaction_jobs: Mapped[list["DocumentRedactionJob"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="document_version")

class DocumentPage(Base):
    __tablename__ = "DocumentPage"

    id: Mapped[str] = mapped_column(primary_key=True)
    version_id: Mapped[str] = mapped_column("versionId", ForeignKey("DocumentVersion.id", ondelete="CASCADE", onupdate="CASCADE", name="DocumentPage_versionId_fkey"))
    page_number: Mapped[int] = mapped_column("pageNumber")
    embedded_links: Mapped[list[str]] = mapped_column("embeddedLinks", ARRAY(Text), nullable=True)
    page_links: Mapped[Optional[dict[str, Any]]] = mapped_column("pageLinks", JSONB(none_as_null=True))
    metadata_: Mapped[Optional[dict[str, Any]]] = mapped_column("metadata", JSONB(none_as_null=True))
    file: Mapped[str]
    storage_type: Mapped[str] = mapped_column("storageType", Enum("S3_PATH", "VERCEL_BLOB", name="DocumentStorageType"), default="VERCEL_BLOB", server_default=text("'VERCEL_BLOB'"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("DocumentPage_versionId_idx", "versionId"),
        Index("DocumentPage_pageNumber_versionId_key", "pageNumber", "versionId", unique=True),
    )

    version: Mapped["DocumentVersion"] = relationship(back_populates="pages")

class Folder(Base):
    __tablename__ = "Folder"

    id: Mapped[str] = mapped_column(primary_key=True)
    name: Mapped[str]
    path: Mapped[str]
    parent_id: Mapped[Optional[str]] = mapped_column("parentId", ForeignKey("Folder.id", ondelete="SET NULL", onupdate="CASCADE", name="Folder_parentId_fkey"))
    hidden_in_all_documents: Mapped[bool] = mapped_column("hiddenInAllDocuments", default=False, server_default=text("false"))
    icon: Mapped[Optional[str]]
    color: Mapped[Optional[str]]
    team_id: Mapped[str] = mapped_column("teamId", ForeignKey("Team.id", ondelete="CASCADE", onupdate="CASCADE", name="Folder_teamId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("Folder_parentId_idx", "parentId"),
        Index("Folder_teamId_hiddenInAllDocuments_idx", "teamId", "hiddenInAllDocuments"),
        Index("Folder_teamId_path_key", "teamId", "path", unique=True),
    )

    parent_folder: Mapped[Optional["Folder"]] = relationship(remote_side=[id], back_populates="child_folders")
    team: Mapped["Team"] = relationship(back_populates="folders")
    documents: Mapped[list["Document"]] = relationship(back_populates="folder")
    child_folders: Mapped[list["Folder"]] = relationship(back_populates="parent_folder")

class DocumentUpload(Base):
    __tablename__ = "DocumentUpload"

    id: Mapped[str] = mapped_column(primary_key=True)
    document_id: Mapped[str] = mapped_column("documentId", ForeignKey("Document.id", ondelete="CASCADE", onupdate="CASCADE", name="DocumentUpload_documentId_fkey"))
    team_id: Mapped[str] = mapped_column("teamId", ForeignKey("Team.id", ondelete="CASCADE", onupdate="CASCADE", name="DocumentUpload_teamId_fkey"))
    viewer_id: Mapped[Optional[str]] = mapped_column("viewerId", ForeignKey("Viewer.id", ondelete="SET NULL", onupdate="CASCADE", name="DocumentUpload_viewerId_fkey"))
    view_id: Mapped[Optional[str]] = mapped_column("viewId", ForeignKey("View.id", ondelete="SET NULL", onupdate="CASCADE", name="DocumentUpload_viewId_fkey"))
    link_id: Mapped[str] = mapped_column("linkId", ForeignKey("Link.id", ondelete="CASCADE", onupdate="CASCADE", name="DocumentUpload_linkId_fkey"))
    dataroom_id: Mapped[Optional[str]] = mapped_column("dataroomId", ForeignKey("Dataroom.id", ondelete="SET NULL", onupdate="CASCADE", name="DocumentUpload_dataroomId_fkey"))
    dataroom_document_id: Mapped[Optional[str]] = mapped_column("dataroomDocumentId", ForeignKey("DataroomDocument.id", ondelete="SET NULL", onupdate="CASCADE", name="DocumentUpload_dataroomDocumentId_fkey"))
    task_id: Mapped[Optional[str]] = mapped_column("taskId", ForeignKey("Task.id", ondelete="SET NULL", onupdate="CASCADE", name="DocumentUpload_taskId_fkey"))
    original_filename: Mapped[Optional[str]] = mapped_column("originalFilename")
    file_size: Mapped[Optional[int]] = mapped_column("fileSize", BigInteger)
    num_pages: Mapped[Optional[int]] = mapped_column("numPages")
    mime_type: Mapped[Optional[str]] = mapped_column("mimeType")
    uploaded_at: Mapped[datetime] = mapped_column("uploadedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("DocumentUpload_documentId_idx", "documentId"),
        Index("DocumentUpload_viewerId_idx", "viewerId"),
        Index("DocumentUpload_viewId_idx", "viewId"),
        Index("DocumentUpload_linkId_idx", "linkId"),
        Index("DocumentUpload_teamId_idx", "teamId"),
        Index("DocumentUpload_dataroomId_idx", "dataroomId"),
        Index("DocumentUpload_dataroomDocumentId_idx", "dataroomDocumentId"),
        Index("DocumentUpload_taskId_idx", "taskId"),
    )

    document: Mapped["Document"] = relationship(back_populates="uploaded_document")
    team: Mapped["Team"] = relationship(back_populates="uploaded_documents")
    viewer: Mapped[Optional["Viewer"]] = relationship(back_populates="uploaded_documents")
    view: Mapped[Optional["View"]] = relationship(back_populates="uploaded_documents")
    link: Mapped["Link"] = relationship(back_populates="uploaded_documents")
    dataroom: Mapped[Optional["Dataroom"]] = relationship(back_populates="uploaded_documents")
    dataroom_document: Mapped[Optional["DataroomDocument"]] = relationship(back_populates="uploaded_documents")
    task: Mapped[Optional["Task"]] = relationship(back_populates="uploads")

class PendingDocumentUpload(Base):
    __tablename__ = "PendingDocumentUpload"

    id: Mapped[str] = mapped_column(primary_key=True)
    upload_id: Mapped[str] = mapped_column("uploadId")
    team_id: Mapped[str] = mapped_column("teamId")
    token_id: Mapped[str] = mapped_column("tokenId")
    key: Mapped[str]
    content_type: Mapped[str] = mapped_column("contentType")
    storage_type: Mapped[str] = mapped_column("storageType", Enum("S3_PATH", "VERCEL_BLOB", name="DocumentStorageType"), default="S3_PATH", server_default=text("'S3_PATH'"))
    expires_at: Mapped[datetime] = mapped_column("expiresAt")
    consumed_at: Mapped[Optional[datetime]] = mapped_column("consumedAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("PendingDocumentUpload_uploadId_key", "uploadId", unique=True),
        Index("PendingDocumentUpload_teamId_expiresAt_idx", "teamId", "expiresAt"),
        Index("PendingDocumentUpload_tokenId_expiresAt_idx", "tokenId", "expiresAt"),
    )

class Integration(Base):
    __tablename__ = "Integration"

    id: Mapped[str] = mapped_column(primary_key=True)
    name: Mapped[str]
    slug: Mapped[str]
    description: Mapped[Optional[str]]
    readme: Mapped[Optional[str]]
    developer: Mapped[str]
    website: Mapped[str]
    logo: Mapped[Optional[str]]
    screenshots: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    verified: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    install_url: Mapped[Optional[str]] = mapped_column("installUrl")
    category: Mapped[Optional[str]]
    coming_soon: Mapped[bool] = mapped_column("comingSoon", default=False, server_default=text("false"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("Integration_slug_key", "slug", unique=True),
    )

    installed_integrations: Mapped[list["InstalledIntegration"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="integration")

class InstalledIntegration(Base):
    __tablename__ = "InstalledIntegration"

    id: Mapped[str] = mapped_column(primary_key=True)
    credentials: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    configuration: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    enabled: Mapped[bool] = mapped_column(default=True, server_default=text("true"))
    integration_id: Mapped[str] = mapped_column("integrationId", ForeignKey("Integration.id", ondelete="CASCADE", onupdate="CASCADE", name="InstalledIntegration_integrationId_fkey"))
    user_id: Mapped[Optional[str]] = mapped_column("userId", ForeignKey("User.id", ondelete="SET NULL", onupdate="CASCADE", name="InstalledIntegration_userId_fkey"))
    team_id: Mapped[str] = mapped_column("teamId", ForeignKey("Team.id", ondelete="CASCADE", onupdate="CASCADE", name="InstalledIntegration_teamId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("InstalledIntegration_teamId_idx", "teamId"),
        Index("InstalledIntegration_integrationId_idx", "integrationId"),
        Index("InstalledIntegration_teamId_integrationId_key", "teamId", "integrationId", unique=True),
    )

    integration: Mapped["Integration"] = relationship(back_populates="installed_integrations")
    user: Mapped[Optional["User"]] = relationship(back_populates="installed_integrations")
    team: Mapped["Team"] = relationship(back_populates="installed_integrations")

class JacksonIndex(Base):
    __tablename__ = "jackson_index"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    key: Mapped[str]
    store_key: Mapped[str] = mapped_column("storeKey")

    __table_args__ = (
        Index("_jackson_index_key_store", "key", "storeKey"),
    )

class JacksonStore(Base):
    __tablename__ = "jackson_store"

    key: Mapped[str] = mapped_column(primary_key=True)
    value: Mapped[str]
    iv: Mapped[Optional[str]]
    tag: Mapped[Optional[str]]
    namespace: Mapped[Optional[str]]
    created_at: Mapped[datetime] = mapped_column("createdAt", UtcDateTime(precision=0), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    modified_at: Mapped[Optional[datetime]] = mapped_column("modifiedAt", UtcDateTime(precision=0))

    __table_args__ = (
        Index("_jackson_store_namespace", "namespace"),
    )

class JacksonTtl(Base):
    __tablename__ = "jackson_ttl"

    key: Mapped[str] = mapped_column(primary_key=True)
    expires_at: Mapped[int] = mapped_column("expiresAt", BigInteger)

    __table_args__ = (
        Index("_jackson_ttl_expires_at", "expiresAt"),
    )

class Link(Base):
    __tablename__ = "Link"

    id: Mapped[str] = mapped_column(primary_key=True)
    document_id: Mapped[Optional[str]] = mapped_column("documentId", ForeignKey("Document.id", ondelete="CASCADE", onupdate="CASCADE", name="Link_documentId_fkey"))
    dataroom_id: Mapped[Optional[str]] = mapped_column("dataroomId", ForeignKey("Dataroom.id", ondelete="CASCADE", onupdate="CASCADE", name="Link_dataroomId_fkey"))
    link_type: Mapped[str] = mapped_column("linkType", Enum("DOCUMENT_LINK", "DATAROOM_LINK", "WORKFLOW_LINK", name="LinkType"), default="DOCUMENT_LINK", server_default=text("'DOCUMENT_LINK'"))
    url: Mapped[Optional[str]]
    name: Mapped[Optional[str]]
    slug: Mapped[Optional[str]]
    expires_at: Mapped[Optional[datetime]] = mapped_column("expiresAt")
    password: Mapped[Optional[str]]
    allow_list: Mapped[list[str]] = mapped_column("allowList", ARRAY(Text), nullable=True)
    deny_list: Mapped[list[str]] = mapped_column("denyList", ARRAY(Text), nullable=True)
    email_protected: Mapped[bool] = mapped_column("emailProtected", default=True, server_default=text("true"))
    email_authenticated: Mapped[bool] = mapped_column("emailAuthenticated", default=False, server_default=text("false"))
    allow_download: Mapped[Optional[bool]] = mapped_column("allowDownload", default=False, server_default=text("false"))
    is_archived: Mapped[bool] = mapped_column("isArchived", default=False, server_default=text("false"))
    deleted_at: Mapped[Optional[datetime]] = mapped_column("deletedAt")
    domain_id: Mapped[Optional[str]] = mapped_column("domainId", ForeignKey("Domain.id", ondelete="SET NULL", onupdate="CASCADE", name="Link_domainId_fkey"))
    domain_slug: Mapped[Optional[str]] = mapped_column("domainSlug")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    enable_notification: Mapped[Optional[bool]] = mapped_column("enableNotification", default=True, server_default=text("true"))
    enable_feedback: Mapped[Optional[bool]] = mapped_column("enableFeedback", default=False, server_default=text("false"))
    enable_question: Mapped[Optional[bool]] = mapped_column("enableQuestion", default=False, server_default=text("false"))
    enable_screenshot_protection: Mapped[Optional[bool]] = mapped_column("enableScreenshotProtection", default=False, server_default=text("false"))
    enable_confidential_view: Mapped[Optional[bool]] = mapped_column("enableConfidentialView", default=False, server_default=text("false"))
    enable_agreement: Mapped[Optional[bool]] = mapped_column("enableAgreement", default=False, server_default=text("false"))
    agreement_id: Mapped[Optional[str]] = mapped_column("agreementId", ForeignKey("Agreement.id", ondelete="SET NULL", onupdate="CASCADE", name="Link_agreementId_fkey"))
    show_banner: Mapped[Optional[bool]] = mapped_column("showBanner", default=False, server_default=text("false"))
    enable_watermark: Mapped[Optional[bool]] = mapped_column("enableWatermark", default=False, server_default=text("false"))
    watermark_config: Mapped[Optional[dict[str, Any]]] = mapped_column("watermarkConfig", JSONB(none_as_null=True))
    audience_type: Mapped[str] = mapped_column("audienceType", Enum("GENERAL", "GROUP", "TEAM", name="LinkAudienceType"), default="GENERAL", server_default=text("'GENERAL'"))
    group_id: Mapped[Optional[str]] = mapped_column("groupId", ForeignKey("ViewerGroup.id", ondelete="SET NULL", onupdate="CASCADE", name="Link_groupId_fkey"))
    permission_group_id: Mapped[Optional[str]] = mapped_column("permissionGroupId", ForeignKey("PermissionGroup.id", ondelete="SET NULL", onupdate="CASCADE", name="Link_permissionGroupId_fkey"))
    meta_title: Mapped[Optional[str]] = mapped_column("metaTitle")
    meta_description: Mapped[Optional[str]] = mapped_column("metaDescription")
    meta_image: Mapped[Optional[str]] = mapped_column("metaImage")
    meta_favicon: Mapped[Optional[str]] = mapped_column("metaFavicon")
    enable_custom_metatag: Mapped[Optional[bool]] = mapped_column("enableCustomMetatag", default=False, server_default=text("false"))
    welcome_message: Mapped[Optional[str]] = mapped_column("welcomeMessage")
    enable_conversation: Mapped[bool] = mapped_column("enableConversation", default=False, server_default=text("false"))
    enable_aiagents: Mapped[Optional[bool]] = mapped_column("enableAIAgents", default=False, server_default=text("false"))
    enable_upload: Mapped[Optional[bool]] = mapped_column("enableUpload", default=False, server_default=text("false"))
    is_file_request_only: Mapped[Optional[bool]] = mapped_column("isFileRequestOnly", default=False, server_default=text("false"))
    upload_folder_id: Mapped[Optional[str]] = mapped_column("uploadFolderId")
    upload_folder_ids: Mapped[list[str]] = mapped_column("uploadFolderIds", ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    enable_index_file: Mapped[Optional[bool]] = mapped_column("enableIndexFile", default=False, server_default=text("false"))
    team_id: Mapped[Optional[str]] = mapped_column("teamId", ForeignKey("Team.id", ondelete="CASCADE", onupdate="CASCADE", name="Link_teamId_fkey"))
    brand_id: Mapped[Optional[str]] = mapped_column("brandId", ForeignKey("Brand.id", ondelete="SET NULL", onupdate="CASCADE", name="Link_brandId_fkey"))
    owner_id: Mapped[Optional[str]] = mapped_column("ownerId", ForeignKey("User.id", ondelete="SET NULL", onupdate="CASCADE", name="Link_ownerId_fkey"))

    __table_args__ = (
        Index("Link_url_key", "url", unique=True),
        Index("Link_documentId_idx", "documentId"),
        Index("Link_teamId_idx", "teamId"),
        Index("Link_documentId_isArchived_idx", "documentId", "isArchived"),
        Index("Link_permissionGroupId_idx", "permissionGroupId"),
        Index("Link_deletedAt_idx", "deletedAt"),
        Index("Link_ownerId_idx", "ownerId"),
        Index("Link_brandId_idx", "brandId"),
        Index("Link_domainSlug_slug_key", "domainSlug", "slug", unique=True),
    )

    document: Mapped[Optional["Document"]] = relationship(back_populates="links")
    dataroom: Mapped[Optional["Dataroom"]] = relationship(back_populates="links")
    domain: Mapped[Optional["Domain"]] = relationship(back_populates="links")
    agreement: Mapped[Optional["Agreement"]] = relationship(back_populates="links")
    group: Mapped[Optional["ViewerGroup"]] = relationship(back_populates="links")
    permission_group: Mapped[Optional["PermissionGroup"]] = relationship(back_populates="links")
    team: Mapped[Optional["Team"]] = relationship(back_populates="links")
    brand: Mapped[Optional["Brand"]] = relationship(back_populates="links")
    owner: Mapped[Optional["User"]] = relationship(back_populates="links")
    views: Mapped[list["View"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="link")
    conversations: Mapped[list["Conversation"]] = relationship(back_populates="link")
    question_assignments: Mapped[list["DataroomQuestionAssignment"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="link")
    dataroom_faq_items: Mapped[list["DataroomFaqItem"]] = relationship(back_populates="link")
    chats: Mapped[list["Chat"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="link")
    uploaded_documents: Mapped[list["DocumentUpload"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="link")
    custom_fields: Mapped[list["CustomField"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="link")
    tags: Mapped[list["TagItem"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="link")
    viewer_invitations: Mapped[list["ViewerInvitation"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="link")
    visitor_groups: Mapped[list["LinkVisitorGroup"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="link")
    task_assignments: Mapped[list["TaskAssignment"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="link")
    feedback: Mapped[Optional["Feedback"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="link")
    workflow: Mapped[Optional["Workflow"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="entry_link")

class LinkPreset(Base):
    __tablename__ = "LinkPreset"

    id: Mapped[str] = mapped_column(primary_key=True)
    name: Mapped[str]
    team_id: Mapped[str] = mapped_column("teamId", ForeignKey("Team.id", ondelete="CASCADE", onupdate="CASCADE", name="LinkPreset_teamId_fkey"))
    p_id: Mapped[Optional[str]] = mapped_column("pId")
    enable_custom_meta_tag: Mapped[Optional[bool]] = mapped_column("enableCustomMetaTag", default=False, server_default=text("false"))
    meta_title: Mapped[Optional[str]] = mapped_column("metaTitle")
    meta_description: Mapped[Optional[str]] = mapped_column("metaDescription")
    meta_image: Mapped[Optional[str]] = mapped_column("metaImage")
    meta_favicon: Mapped[Optional[str]] = mapped_column("metaFavicon")
    enable_notification: Mapped[Optional[bool]] = mapped_column("enableNotification", default=False, server_default=text("false"))
    email_protected: Mapped[Optional[bool]] = mapped_column("emailProtected", default=True, server_default=text("true"))
    email_authenticated: Mapped[Optional[bool]] = mapped_column("emailAuthenticated", default=False, server_default=text("false"))
    allow_download: Mapped[Optional[bool]] = mapped_column("allowDownload", default=False, server_default=text("false"))
    enable_allow_list: Mapped[Optional[bool]] = mapped_column("enableAllowList", default=False, server_default=text("false"))
    allow_list: Mapped[list[str]] = mapped_column("allowList", ARRAY(Text), nullable=True)
    enable_deny_list: Mapped[Optional[bool]] = mapped_column("enableDenyList", default=False, server_default=text("false"))
    deny_list: Mapped[list[str]] = mapped_column("denyList", ARRAY(Text), nullable=True)
    expires_in: Mapped[Optional[int]] = mapped_column("expiresIn")
    enable_screenshot_protection: Mapped[Optional[bool]] = mapped_column("enableScreenshotProtection", default=False, server_default=text("false"))
    enable_confidential_view: Mapped[Optional[bool]] = mapped_column("enableConfidentialView", default=False, server_default=text("false"))
    expires_at: Mapped[Optional[datetime]] = mapped_column("expiresAt")
    enable_password: Mapped[Optional[bool]] = mapped_column("enablePassword", default=False, server_default=text("false"))
    password: Mapped[Optional[str]]
    enable_watermark: Mapped[Optional[bool]] = mapped_column("enableWatermark", default=False, server_default=text("false"))
    watermark_config: Mapped[Optional[dict[str, Any]]] = mapped_column("watermarkConfig", JSONB(none_as_null=True))
    enable_agreement: Mapped[Optional[bool]] = mapped_column("enableAgreement", default=False, server_default=text("false"))
    agreement_id: Mapped[Optional[str]] = mapped_column("agreementId")
    enable_custom_fields: Mapped[Optional[bool]] = mapped_column("enableCustomFields", default=False, server_default=text("false"))
    custom_fields: Mapped[Optional[dict[str, Any]]] = mapped_column("customFields", JSONB(none_as_null=True))
    show_banner: Mapped[Optional[bool]] = mapped_column("showBanner", default=False, server_default=text("false"))
    welcome_message: Mapped[Optional[str]] = mapped_column("welcomeMessage")
    is_default: Mapped[bool] = mapped_column("isDefault", default=False, server_default=text("false"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("LinkPreset_pId_key", "pId", unique=True),
        Index("LinkPreset_teamId_idx", "teamId"),
    )

    team: Mapped["Team"] = relationship(back_populates="link_presets")

class VisitorGroup(Base):
    __tablename__ = "VisitorGroup"

    id: Mapped[str] = mapped_column(primary_key=True)
    name: Mapped[str]
    emails: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True)
    team_id: Mapped[str] = mapped_column("teamId", ForeignKey("Team.id", ondelete="CASCADE", onupdate="CASCADE", name="VisitorGroup_teamId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("VisitorGroup_teamId_idx", "teamId"),
    )

    team: Mapped["Team"] = relationship(back_populates="visitor_groups")
    links: Mapped[list["LinkVisitorGroup"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="visitor_group")

class LinkVisitorGroup(Base):
    __tablename__ = "LinkVisitorGroup"

    id: Mapped[str] = mapped_column(primary_key=True)
    link_id: Mapped[str] = mapped_column("linkId", ForeignKey("Link.id", ondelete="CASCADE", onupdate="CASCADE", name="LinkVisitorGroup_linkId_fkey"))
    visitor_group_id: Mapped[str] = mapped_column("visitorGroupId", ForeignKey("VisitorGroup.id", ondelete="CASCADE", onupdate="CASCADE", name="LinkVisitorGroup_visitorGroupId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("LinkVisitorGroup_linkId_idx", "linkId"),
        Index("LinkVisitorGroup_visitorGroupId_idx", "visitorGroupId"),
        Index("LinkVisitorGroup_linkId_visitorGroupId_key", "linkId", "visitorGroupId", unique=True),
    )

    link: Mapped["Link"] = relationship(back_populates="visitor_groups")
    visitor_group: Mapped["VisitorGroup"] = relationship(back_populates="links")

class CustomField(Base):
    __tablename__ = "CustomField"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    type: Mapped[str] = mapped_column(Enum("SHORT_TEXT", "LONG_TEXT", "NUMBER", "PHONE_NUMBER", "URL", "CHECKBOX", "SELECT", "MULTI_SELECT", name="CustomFieldType"))
    identifier: Mapped[str]
    label: Mapped[str]
    placeholder: Mapped[Optional[str]]
    required: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    disabled: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    link_id: Mapped[str] = mapped_column("linkId", ForeignKey("Link.id", ondelete="CASCADE", onupdate="CASCADE", name="CustomField_linkId_fkey"))
    order_index: Mapped[int] = mapped_column("orderIndex", default=0, server_default=text("0"))

    __table_args__ = (
        Index("CustomField_linkId_idx", "linkId"),
    )

    link: Mapped["Link"] = relationship(back_populates="custom_fields")

class CustomFieldResponse(Base):
    __tablename__ = "CustomFieldResponse"

    id: Mapped[str] = mapped_column(primary_key=True)
    data: Mapped[dict[str, Any]] = mapped_column(JSONB)
    view_id: Mapped[str] = mapped_column("viewId", ForeignKey("View.id", ondelete="CASCADE", onupdate="CASCADE", name="CustomFieldResponse_viewId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("CustomFieldResponse_viewId_key", "viewId", unique=True),
        Index("CustomFieldResponse_viewId_idx", "viewId"),
    )

    view: Mapped["View"] = relationship(back_populates="custom_field_response")

class NotificationPreference(Base):
    __tablename__ = "NotificationPreference"

    id: Mapped[str] = mapped_column(primary_key=True)
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="NotificationPreference_userId_fkey"))
    team_id: Mapped[str] = mapped_column("teamId", ForeignKey("Team.id", ondelete="CASCADE", onupdate="CASCADE", name="NotificationPreference_teamId_fkey"))
    type: Mapped[str]
    frequency: Mapped[str] = mapped_column(default="IMMEDIATE", server_default=text("'IMMEDIATE'"))
    scope: Mapped[str] = mapped_column(default="ALL", server_default=text("'ALL'"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("NotificationPreference_userId_teamId_idx", "userId", "teamId"),
        Index("NotificationPreference_userId_teamId_type_key", "userId", "teamId", "type", unique=True),
    )

    user: Mapped["User"] = relationship(back_populates="notification_preferences")
    team: Mapped["Team"] = relationship(back_populates="notification_preferences")

class OAuthClient(Base):
    __tablename__ = "OAuthClient"

    id: Mapped[str] = mapped_column(primary_key=True)
    client_id: Mapped[str] = mapped_column("clientId")
    client_name: Mapped[str] = mapped_column("clientName")
    client_uri: Mapped[Optional[str]] = mapped_column("clientUri")
    logo_uri: Mapped[Optional[str]] = mapped_column("logoUri")
    redirect_uris: Mapped[list[str]] = mapped_column("redirectUris", ARRAY(Text), nullable=True)
    grant_types: Mapped[list[str]] = mapped_column("grantTypes", ARRAY(Text), nullable=True)
    scopes: Mapped[str]
    token_auth_method: Mapped[str] = mapped_column("tokenAuthMethod")
    hashed_secret: Mapped[Optional[str]] = mapped_column("hashedSecret")
    registration_type: Mapped[str] = mapped_column("registrationType")
    metadata_url: Mapped[Optional[str]] = mapped_column("metadataUrl")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("OAuthClient_clientId_key", "clientId", unique=True),
        Index("OAuthClient_clientId_idx", "clientId"),
    )

    tokens: Mapped[list["RestrictedToken"]] = relationship(back_populates="oauth_client")

class OAuthClientApproval(Base):
    __tablename__ = "OAuthClientApproval"

    id: Mapped[str] = mapped_column(primary_key=True)
    user_id: Mapped[str] = mapped_column("userId")
    client_id: Mapped[str] = mapped_column("clientId")
    approved_at: Mapped[datetime] = mapped_column("approvedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    revoked_at: Mapped[Optional[datetime]] = mapped_column("revokedAt")
    client_metadata_hash: Mapped[Optional[str]] = mapped_column("clientMetadataHash")

    __table_args__ = (
        Index("OAuthClientApproval_clientId_idx", "clientId"),
        Index("OAuthClientApproval_userId_clientId_key", "userId", "clientId", unique=True),
    )

class OAuthGrantTeam(Base):
    __tablename__ = "OAuthGrantTeam"

    id: Mapped[str] = mapped_column(primary_key=True)
    grant_id: Mapped[str] = mapped_column("grantId")
    team_id: Mapped[str] = mapped_column("teamId")
    user_id: Mapped[str] = mapped_column("userId")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("OAuthGrantTeam_grantId_key", "grantId", unique=True),
        Index("OAuthGrantTeam_grantId_idx", "grantId"),
    )

class OAuthRecord(Base):
    __tablename__ = "OAuthRecord"

    id: Mapped[str] = mapped_column(primary_key=True)
    kind: Mapped[str]
    payload: Mapped[dict[str, Any]] = mapped_column(JSONB)
    grant_id: Mapped[Optional[str]] = mapped_column("grantId")
    user_code: Mapped[Optional[str]] = mapped_column("userCode")
    uid: Mapped[Optional[str]]
    expires_at: Mapped[Optional[datetime]] = mapped_column("expiresAt")
    consumed_at: Mapped[Optional[datetime]] = mapped_column("consumedAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("OAuthRecord_kind_idx", "kind"),
        Index("OAuthRecord_grantId_idx", "grantId"),
        Index("OAuthRecord_userCode_idx", "userCode"),
        Index("OAuthRecord_uid_idx", "uid"),
        Index("OAuthRecord_expiresAt_idx", "expiresAt"),
    )

class DocumentRedactionJob(Base):
    __tablename__ = "DocumentRedactionJob"

    id: Mapped[str] = mapped_column(primary_key=True)
    document_id: Mapped[str] = mapped_column("documentId", ForeignKey("Document.id", ondelete="CASCADE", onupdate="CASCADE", name="DocumentRedactionJob_documentId_fkey"))
    document_version_id: Mapped[str] = mapped_column("documentVersionId", ForeignKey("DocumentVersion.id", ondelete="CASCADE", onupdate="CASCADE", name="DocumentRedactionJob_documentVersionId_fkey"))
    team_id: Mapped[str] = mapped_column("teamId", ForeignKey("Team.id", ondelete="CASCADE", onupdate="CASCADE", name="DocumentRedactionJob_teamId_fkey"))
    created_by_id: Mapped[str] = mapped_column("createdById", ForeignKey("User.id", ondelete="RESTRICT", onupdate="CASCADE", name="DocumentRedactionJob_createdById_fkey"))
    status: Mapped[str] = mapped_column(default="PENDING", server_default=text("'PENDING'"))
    custom_terms: Mapped[list[str]] = mapped_column("customTerms", ARRAY(Text), nullable=True)
    reasons: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True)
    result_version_id: Mapped[Optional[str]] = mapped_column("resultVersionId")
    error: Mapped[Optional[str]]
    detect_run_id: Mapped[Optional[str]] = mapped_column("detectRunId")
    apply_run_id: Mapped[Optional[str]] = mapped_column("applyRunId")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("DocumentRedactionJob_documentId_idx", "documentId"),
        Index("DocumentRedactionJob_documentVersionId_idx", "documentVersionId"),
        Index("DocumentRedactionJob_teamId_idx", "teamId"),
        Index("DocumentRedactionJob_teamId_status_idx", "teamId", "status"),
    )

    document: Mapped["Document"] = relationship(back_populates="redaction_jobs")
    document_version: Mapped["DocumentVersion"] = relationship(back_populates="redaction_jobs")
    team: Mapped["Team"] = relationship(back_populates="redaction_jobs")
    created_by: Mapped["User"] = relationship(back_populates="created_redaction_jobs")
    redactions: Mapped[list["DocumentRedaction"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="job")

class DocumentRedaction(Base):
    __tablename__ = "DocumentRedaction"

    id: Mapped[str] = mapped_column(primary_key=True)
    job_id: Mapped[str] = mapped_column("jobId", ForeignKey("DocumentRedactionJob.id", ondelete="CASCADE", onupdate="CASCADE", name="DocumentRedaction_jobId_fkey"))
    page_number: Mapped[int] = mapped_column("pageNumber")
    x: Mapped[float]
    y: Mapped[float]
    width: Mapped[float]
    height: Mapped[float]
    text_preview: Mapped[Optional[str]] = mapped_column("textPreview")
    category: Mapped[Optional[str]]
    confidence: Mapped[Optional[str]]
    reason: Mapped[Optional[str]]
    source: Mapped[str] = mapped_column(default="AI", server_default=text("'AI'"))
    status: Mapped[str] = mapped_column(default="PENDING", server_default=text("'PENDING'"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("DocumentRedaction_jobId_idx", "jobId"),
        Index("DocumentRedaction_jobId_pageNumber_idx", "jobId", "pageNumber"),
        Index("DocumentRedaction_jobId_status_idx", "jobId", "status"),
    )

    job: Mapped["DocumentRedactionJob"] = relationship(back_populates="redactions")

class ReferralInvite(Base):
    __tablename__ = "ReferralInvite"

    id: Mapped[str] = mapped_column(primary_key=True)
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="ReferralInvite_userId_fkey"))
    team_id: Mapped[Optional[str]] = mapped_column("teamId", ForeignKey("Team.id", ondelete="CASCADE", onupdate="CASCADE", name="ReferralInvite_teamId_fkey"))
    scope_id: Mapped[str] = mapped_column("scopeId")
    email: Mapped[str]
    referral_link: Mapped[Optional[str]] = mapped_column("referralLink")
    sent_at: Mapped[datetime] = mapped_column("sentAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("ReferralInvite_userId_idx", "userId"),
        Index("ReferralInvite_teamId_idx", "teamId"),
        Index("ReferralInvite_scopeId_email_key", "scopeId", "email", unique=True),
    )

    user: Mapped["User"] = relationship(back_populates="referral_invites")
    team: Mapped[Optional["Team"]] = relationship(back_populates="referral_invites")

class Account(Base):
    __tablename__ = "Account"

    id: Mapped[str] = mapped_column(primary_key=True)
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="Account_userId_fkey"))
    type: Mapped[str]
    provider: Mapped[str]
    provider_account_id: Mapped[str] = mapped_column("providerAccountId")
    refresh_token: Mapped[Optional[str]]
    access_token: Mapped[Optional[str]]
    expires_at: Mapped[Optional[int]]
    token_type: Mapped[Optional[str]]
    scope: Mapped[Optional[str]]
    id_token: Mapped[Optional[str]]
    session_state: Mapped[Optional[str]]

    __table_args__ = (
        Index("Account_provider_providerAccountId_key", "provider", "providerAccountId", unique=True),
    )

    user: Mapped["User"] = relationship(back_populates="accounts")

class Session(Base):
    __tablename__ = "Session"

    id: Mapped[str] = mapped_column(primary_key=True)
    session_token: Mapped[str] = mapped_column("sessionToken")
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="Session_userId_fkey"))
    expires: Mapped[datetime]

    __table_args__ = (
        Index("Session_sessionToken_key", "sessionToken", unique=True),
    )

    user: Mapped["User"] = relationship(back_populates="sessions")

class User(Base):
    __tablename__ = "User"

    id: Mapped[str] = mapped_column(primary_key=True)
    name: Mapped[Optional[str]]
    email: Mapped[Optional[str]]
    email_verified: Mapped[Optional[datetime]] = mapped_column("emailVerified")
    image: Mapped[Optional[str]]
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    contact_id: Mapped[Optional[str]] = mapped_column("contactId")
    plan: Mapped[str] = mapped_column(default="free", server_default=text("'free'"))
    stripe_id: Mapped[Optional[str]] = mapped_column("stripeId")
    subscription_id: Mapped[Optional[str]] = mapped_column("subscriptionId")
    starts_at: Mapped[Optional[datetime]] = mapped_column("startsAt")
    ends_at: Mapped[Optional[datetime]] = mapped_column("endsAt")
    referral_track: Mapped[Optional[str]] = mapped_column("referralTrack", Enum("AFFILIATE", "PARTNER_DISCOUNT", name="ReferralTrack"))
    referral_track_selected_at: Mapped[Optional[datetime]] = mapped_column("referralTrackSelectedAt")
    referral_profile: Mapped[Optional[dict[str, Any]]] = mapped_column("referralProfile", JSONB(none_as_null=True))

    __table_args__ = (
        Index("User_email_key", "email", unique=True),
        Index("User_stripeId_key", "stripeId", unique=True),
        Index("User_subscriptionId_key", "subscriptionId", unique=True),
    )

    accounts: Mapped[list["Account"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    sessions: Mapped[list["Session"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    documents: Mapped[list["Document"]] = relationship(back_populates="owner")
    teams: Mapped[list["UserTeam"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    user_datarooms: Mapped[list["UserDataroom"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    domains: Mapped[list["Domain"]] = relationship(back_populates="user")
    chats: Mapped[list["Chat"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    restricted_tokens: Mapped[list["RestrictedToken"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    participated_conversations: Mapped[list["ConversationParticipant"]] = relationship(back_populates="user")
    messages: Mapped[list["Message"]] = relationship(back_populates="user")
    created_dataroom_questions: Mapped[list["DataroomQuestion"]] = relationship(back_populates="created_by_user")
    published_faq_items: Mapped[list["DataroomFaqItem"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="published_by_user")
    created_annotations: Mapped[list["DocumentAnnotation"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="created_by")
    created_redaction_jobs: Mapped[list["DocumentRedactionJob"]] = relationship(passive_deletes="all", back_populates="created_by")
    installed_integrations: Mapped[list["InstalledIntegration"]] = relationship(back_populates="user")
    links: Mapped[list["Link"]] = relationship(back_populates="owner")
    frozen_datarooms: Mapped[list["Dataroom"]] = relationship(back_populates="frozen_by_user")
    created_tasks: Mapped[list["Task"]] = relationship(back_populates="created_by_user")
    task_activities: Mapped[list["TaskActivity"]] = relationship(back_populates="user")
    notification_preferences: Mapped[list["NotificationPreference"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    referral_invites: Mapped[list["ReferralInvite"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")

class Domain(Base):
    __tablename__ = "Domain"

    id: Mapped[str] = mapped_column(primary_key=True)
    slug: Mapped[str]
    user_id: Mapped[Optional[str]] = mapped_column("userId", ForeignKey("User.id", ondelete="SET NULL", onupdate="CASCADE", name="Domain_userId_fkey"))
    team_id: Mapped[str] = mapped_column("teamId", ForeignKey("Team.id", ondelete="CASCADE", onupdate="CASCADE", name="Domain_teamId_fkey"))
    verified: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    is_default: Mapped[bool] = mapped_column("isDefault", default=False, server_default=text("false"))
    redirect_url: Mapped[Optional[str]] = mapped_column("redirectUrl")
    last_checked: Mapped[datetime] = mapped_column("lastChecked", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("Domain_slug_key", "slug", unique=True),
        Index("Domain_userId_idx", "userId"),
        Index("Domain_teamId_idx", "teamId"),
    )

    user: Mapped[Optional["User"]] = relationship(back_populates="domains")
    team: Mapped["Team"] = relationship(back_populates="domains")
    links: Mapped[list["Link"]] = relationship(back_populates="domain")

class View(Base):
    __tablename__ = "View"

    id: Mapped[str] = mapped_column(primary_key=True)
    link_id: Mapped[str] = mapped_column("linkId", ForeignKey("Link.id", ondelete="CASCADE", onupdate="CASCADE", name="View_linkId_fkey"))
    document_id: Mapped[Optional[str]] = mapped_column("documentId", ForeignKey("Document.id", ondelete="CASCADE", onupdate="CASCADE", name="View_documentId_fkey"))
    dataroom_id: Mapped[Optional[str]] = mapped_column("dataroomId", ForeignKey("Dataroom.id", ondelete="CASCADE", onupdate="CASCADE", name="View_dataroomId_fkey"))
    dataroom_view_id: Mapped[Optional[str]] = mapped_column("dataroomViewId")
    viewer_email: Mapped[Optional[str]] = mapped_column("viewerEmail")
    viewer_name: Mapped[Optional[str]] = mapped_column("viewerName")
    verified: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    viewed_at: Mapped[datetime] = mapped_column("viewedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    downloaded_at: Mapped[Optional[datetime]] = mapped_column("downloadedAt")
    download_type: Mapped[Optional[str]] = mapped_column("downloadType", Enum("SINGLE", "BULK", "FOLDER", name="DownloadType"))
    download_metadata: Mapped[Optional[dict[str, Any]]] = mapped_column("downloadMetadata", JSONB(none_as_null=True))
    view_type: Mapped[str] = mapped_column("viewType", Enum("DOCUMENT_VIEW", "DATAROOM_VIEW", name="ViewType"), default="DOCUMENT_VIEW", server_default=text("'DOCUMENT_VIEW'"))
    viewer_id: Mapped[Optional[str]] = mapped_column("viewerId", ForeignKey("Viewer.id", ondelete="CASCADE", onupdate="CASCADE", name="View_viewerId_fkey"))
    group_id: Mapped[Optional[str]] = mapped_column("groupId", ForeignKey("ViewerGroup.id", ondelete="SET NULL", onupdate="CASCADE", name="View_groupId_fkey"))
    is_archived: Mapped[bool] = mapped_column("isArchived", default=False, server_default=text("false"))
    team_id: Mapped[Optional[str]] = mapped_column("teamId", ForeignKey("Team.id", ondelete="CASCADE", onupdate="CASCADE", name="View_teamId_fkey"))

    __table_args__ = (
        Index("View_linkId_idx", "linkId"),
        Index("View_documentId_idx", "documentId"),
        Index("View_dataroomId_idx", "dataroomId"),
        Index("View_dataroomViewId_idx", "dataroomViewId"),
        Index("View_viewerId_idx", "viewerId"),
        Index("View_groupId_idx", "groupId"),
        Index("View_teamId_idx", "teamId"),
        Index("View_viewedAt_idx", "viewedAt"),
        Index("View_viewerId_documentId_idx", "viewerId", "documentId"),
        Index("View_viewerEmail_idx", "viewerEmail"),
        Index("View_documentId_isArchived_idx", "documentId", "isArchived"),
        Index("View_documentId_viewedAt_idx", "documentId", "viewedAt"),
    )

    link: Mapped["Link"] = relationship(back_populates="views")
    document: Mapped[Optional["Document"]] = relationship(back_populates="views")
    dataroom: Mapped[Optional["Dataroom"]] = relationship(back_populates="views")
    viewer: Mapped[Optional["Viewer"]] = relationship(back_populates="views")
    group: Mapped[Optional["ViewerGroup"]] = relationship(back_populates="views")
    team: Mapped[Optional["Team"]] = relationship(back_populates="views")
    reactions: Mapped[list["Reaction"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="view")
    conversation_views: Mapped[list["ConversationView"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="view")
    messages: Mapped[list["Message"]] = relationship(back_populates="view")
    initial_conversations: Mapped[list["Conversation"]] = relationship(back_populates="initial_view")
    uploaded_documents: Mapped[list["DocumentUpload"]] = relationship(back_populates="view")
    task_activities: Mapped[list["TaskActivity"]] = relationship(back_populates="view")
    chats: Mapped[list["Chat"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="view")
    feedback_response: Mapped[Optional["FeedbackResponse"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="view")
    agreement_response: Mapped[Optional["AgreementResponse"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="view")
    custom_field_response: Mapped[Optional["CustomFieldResponse"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="view")

class Viewer(Base):
    __tablename__ = "Viewer"

    id: Mapped[str] = mapped_column(primary_key=True)
    email: Mapped[str]
    verified: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    invited_at: Mapped[Optional[datetime]] = mapped_column("invitedAt")
    notification_preferences: Mapped[Optional[dict[str, Any]]] = mapped_column("notificationPreferences", JSONB(none_as_null=True))
    dataroom_id: Mapped[Optional[str]] = mapped_column("dataroomId", ForeignKey("Dataroom.id", ondelete="SET NULL", onupdate="CASCADE", name="Viewer_dataroomId_fkey"))
    team_id: Mapped[str] = mapped_column("teamId", ForeignKey("Team.id", ondelete="CASCADE", onupdate="CASCADE", name="Viewer_teamId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("Viewer_teamId_idx", "teamId"),
        Index("Viewer_dataroomId_idx", "dataroomId"),
        Index("Viewer_teamId_email_key", "teamId", "email", unique=True),
    )

    dataroom: Mapped[Optional["Dataroom"]] = relationship(back_populates="viewers")
    team: Mapped["Team"] = relationship(back_populates="viewers")
    views: Mapped[list["View"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="viewer")
    groups: Mapped[list["ViewerGroupMembership"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="viewer")
    invitations: Mapped[list["ViewerInvitation"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="viewer")
    participated_conversations: Mapped[list["ConversationParticipant"]] = relationship(back_populates="viewer")
    messages: Mapped[list["Message"]] = relationship(back_populates="viewer")
    recipient_conversations: Mapped[list["Conversation"]] = relationship(back_populates="recipient_viewer")
    question_assignments: Mapped[list["DataroomQuestionAssignment"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="viewer")
    uploaded_documents: Mapped[list["DocumentUpload"]] = relationship(back_populates="viewer")
    task_assignments: Mapped[list["TaskAssignment"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="viewer")
    task_activities: Mapped[list["TaskActivity"]] = relationship(back_populates="viewer")
    chats: Mapped[list["Chat"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="viewer")

class Reaction(Base):
    __tablename__ = "Reaction"

    id: Mapped[str] = mapped_column(primary_key=True)
    view_id: Mapped[str] = mapped_column("viewId", ForeignKey("View.id", ondelete="CASCADE", onupdate="CASCADE", name="Reaction_viewId_fkey"))
    page_number: Mapped[int] = mapped_column("pageNumber")
    type: Mapped[str]
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("Reaction_viewId_idx", "viewId"),
        Index("Reaction_viewId_type_idx", "viewId", "type"),
    )

    view: Mapped["View"] = relationship(back_populates="reactions")

class SentEmail(Base):
    __tablename__ = "SentEmail"

    id: Mapped[str] = mapped_column(primary_key=True)
    type: Mapped[str] = mapped_column(Enum("FIRST_DAY_DOMAIN_REMINDER_EMAIL", "FIRST_DOMAIN_INVALID_EMAIL", "SECOND_DOMAIN_INVALID_EMAIL", "FIRST_TRIAL_END_REMINDER_EMAIL", "FINAL_TRIAL_END_REMINDER_EMAIL", name="EmailType"))
    recipient: Mapped[str]
    marketing: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    team_id: Mapped[str] = mapped_column("teamId", ForeignKey("Team.id", ondelete="CASCADE", onupdate="CASCADE", name="SentEmail_teamId_fkey"))
    domain_slug: Mapped[Optional[str]] = mapped_column("domainSlug")

    __table_args__ = (
        Index("SentEmail_teamId_idx", "teamId"),
    )

    team: Mapped["Team"] = relationship(back_populates="sent_emails")

class Chat(Base):
    __tablename__ = "Chat"

    id: Mapped[str] = mapped_column(primary_key=True)
    title: Mapped[Optional[str]]
    team_id: Mapped[str] = mapped_column("teamId", ForeignKey("Team.id", ondelete="CASCADE", onupdate="CASCADE", name="Chat_teamId_fkey"))
    document_id: Mapped[Optional[str]] = mapped_column("documentId", ForeignKey("Document.id", ondelete="CASCADE", onupdate="CASCADE", name="Chat_documentId_fkey"))
    dataroom_id: Mapped[Optional[str]] = mapped_column("dataroomId", ForeignKey("Dataroom.id", ondelete="CASCADE", onupdate="CASCADE", name="Chat_dataroomId_fkey"))
    link_id: Mapped[Optional[str]] = mapped_column("linkId", ForeignKey("Link.id", ondelete="CASCADE", onupdate="CASCADE", name="Chat_linkId_fkey"))
    view_id: Mapped[Optional[str]] = mapped_column("viewId", ForeignKey("View.id", ondelete="CASCADE", onupdate="CASCADE", name="Chat_viewId_fkey"))
    user_id: Mapped[Optional[str]] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="Chat_userId_fkey"))
    viewer_id: Mapped[Optional[str]] = mapped_column("viewerId", ForeignKey("Viewer.id", ondelete="CASCADE", onupdate="CASCADE", name="Chat_viewerId_fkey"))
    vector_store_id: Mapped[Optional[str]] = mapped_column("vectorStoreId")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    last_message_at: Mapped[Optional[datetime]] = mapped_column("lastMessageAt")

    __table_args__ = (
        Index("Chat_teamId_idx", "teamId"),
        Index("Chat_documentId_idx", "documentId"),
        Index("Chat_dataroomId_idx", "dataroomId"),
        Index("Chat_linkId_idx", "linkId"),
        Index("Chat_userId_idx", "userId"),
        Index("Chat_viewerId_idx", "viewerId"),
        Index("Chat_viewId_idx", "viewId"),
        Index("Chat_createdAt_idx", "createdAt"),
    )

    team: Mapped["Team"] = relationship(back_populates="chats")
    document: Mapped[Optional["Document"]] = relationship(back_populates="chats")
    dataroom: Mapped[Optional["Dataroom"]] = relationship(back_populates="chats")
    link: Mapped[Optional["Link"]] = relationship(back_populates="chats")
    view: Mapped[Optional["View"]] = relationship(back_populates="chats")
    user: Mapped[Optional["User"]] = relationship(back_populates="chats")
    viewer: Mapped[Optional["Viewer"]] = relationship(back_populates="chats")
    messages: Mapped[list["ChatMessage"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="chat")

class ChatMessage(Base):
    __tablename__ = "ChatMessage"

    id: Mapped[str] = mapped_column(primary_key=True)
    chat_id: Mapped[str] = mapped_column("chatId", ForeignKey("Chat.id", ondelete="CASCADE", onupdate="CASCADE", name="ChatMessage_chatId_fkey"))
    role: Mapped[str]
    content: Mapped[str]
    metadata_: Mapped[Optional[dict[str, Any]]] = mapped_column("metadata", JSONB(none_as_null=True))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("ChatMessage_chatId_idx", "chatId"),
        Index("ChatMessage_chatId_createdAt_idx", "chatId", "createdAt"),
    )

    chat: Mapped["Chat"] = relationship(back_populates="messages")

class Feedback(Base):
    __tablename__ = "Feedback"

    id: Mapped[str] = mapped_column(primary_key=True)
    link_id: Mapped[str] = mapped_column("linkId", ForeignKey("Link.id", ondelete="CASCADE", onupdate="CASCADE", name="Feedback_linkId_fkey"))
    data: Mapped[dict[str, Any]] = mapped_column(JSONB)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("Feedback_linkId_key", "linkId", unique=True),
        Index("Feedback_linkId_idx", "linkId"),
    )

    link: Mapped["Link"] = relationship(back_populates="feedback")
    responses: Mapped[list["FeedbackResponse"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="feedback")

class FeedbackResponse(Base):
    __tablename__ = "FeedbackResponse"

    id: Mapped[str] = mapped_column(primary_key=True)
    feedback_id: Mapped[str] = mapped_column("feedbackId", ForeignKey("Feedback.id", ondelete="CASCADE", onupdate="CASCADE", name="FeedbackResponse_feedbackId_fkey"))
    data: Mapped[dict[str, Any]] = mapped_column(JSONB)
    view_id: Mapped[str] = mapped_column("viewId", ForeignKey("View.id", ondelete="CASCADE", onupdate="CASCADE", name="FeedbackResponse_viewId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("FeedbackResponse_viewId_key", "viewId", unique=True),
        Index("FeedbackResponse_feedbackId_idx", "feedbackId"),
        Index("FeedbackResponse_viewId_idx", "viewId"),
    )

    feedback: Mapped["Feedback"] = relationship(back_populates="responses")
    view: Mapped["View"] = relationship(back_populates="feedback_response")

class Agreement(Base):
    __tablename__ = "Agreement"

    id: Mapped[str] = mapped_column(primary_key=True)
    name: Mapped[str]
    content: Mapped[str]
    content_type: Mapped[str] = mapped_column("contentType", default="LINK", server_default=text("'LINK'"))
    signing_provider: Mapped[str] = mapped_column("signingProvider", default="LEGACY", server_default=text("'LEGACY'"))
    signing_external_id: Mapped[Optional[str]] = mapped_column("signingExternalId")
    signing_envelope_id: Mapped[Optional[str]] = mapped_column("signingEnvelopeId")
    signing_template_id: Mapped[Optional[str]] = mapped_column("signingTemplateId")
    require_name: Mapped[bool] = mapped_column("requireName", default=True, server_default=text("true"))
    team_id: Mapped[str] = mapped_column("teamId", ForeignKey("Team.id", ondelete="CASCADE", onupdate="CASCADE", name="Agreement_teamId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    deleted_at: Mapped[Optional[datetime]] = mapped_column("deletedAt")
    deleted_by: Mapped[Optional[str]] = mapped_column("deletedBy")

    __table_args__ = (
        Index("Agreement_signingExternalId_key", "signingExternalId", unique=True),
        Index("Agreement_teamId_idx", "teamId"),
    )

    team: Mapped["Team"] = relationship(back_populates="agreements")
    links: Mapped[list["Link"]] = relationship(back_populates="agreement")
    responses: Mapped[list["AgreementResponse"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="agreement")

class AgreementResponse(Base):
    __tablename__ = "AgreementResponse"

    id: Mapped[str] = mapped_column(primary_key=True)
    agreement_id: Mapped[str] = mapped_column("agreementId", ForeignKey("Agreement.id", ondelete="CASCADE", onupdate="CASCADE", name="AgreementResponse_agreementId_fkey"))
    view_id: Mapped[Optional[str]] = mapped_column("viewId", ForeignKey("View.id", ondelete="CASCADE", onupdate="CASCADE", name="AgreementResponse_viewId_fkey"))
    link_id: Mapped[Optional[str]] = mapped_column("linkId")
    signer_email: Mapped[Optional[str]] = mapped_column("signerEmail")
    signer_name: Mapped[Optional[str]] = mapped_column("signerName")
    signing_status: Mapped[str] = mapped_column("signingStatus", default="PENDING", server_default=text("'PENDING'"))
    signing_external_id: Mapped[Optional[str]] = mapped_column("signingExternalId")
    signing_envelope_id: Mapped[Optional[str]] = mapped_column("signingEnvelopeId")
    signing_document_id: Mapped[Optional[int]] = mapped_column("signingDocumentId")
    signed_at: Mapped[Optional[datetime]] = mapped_column("signedAt")
    completed_at: Mapped[Optional[datetime]] = mapped_column("completedAt")
    signed_file_key: Mapped[Optional[str]] = mapped_column("signedFileKey")
    signed_file_name: Mapped[Optional[str]] = mapped_column("signedFileName")
    signed_file_storage_type: Mapped[Optional[str]] = mapped_column("signedFileStorageType", Enum("S3_PATH", "VERCEL_BLOB", name="DocumentStorageType"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("AgreementResponse_viewId_key", "viewId", unique=True),
        Index("AgreementResponse_signingExternalId_key", "signingExternalId", unique=True),
        Index("AgreementResponse_agreementId_idx", "agreementId"),
        Index("AgreementResponse_viewId_idx", "viewId"),
        Index("AgreementResponse_linkId_idx", "linkId"),
        Index("AgreementResponse_signerEmail_idx", "signerEmail"),
    )

    agreement: Mapped["Agreement"] = relationship(back_populates="responses")
    view: Mapped[Optional["View"]] = relationship(back_populates="agreement_response")

class IncomingWebhook(Base):
    __tablename__ = "IncomingWebhook"

    id: Mapped[str] = mapped_column(primary_key=True)
    external_id: Mapped[str] = mapped_column("externalId")
    name: Mapped[str]
    secret: Mapped[Optional[str]]
    source: Mapped[Optional[str]]
    actions: Mapped[Optional[str]]
    consecutive_failures: Mapped[int] = mapped_column("consecutiveFailures", default=0, server_default=text("0"))
    last_failed_at: Mapped[Optional[datetime]] = mapped_column("lastFailedAt")
    disabled_at: Mapped[Optional[datetime]] = mapped_column("disabledAt")
    team_id: Mapped[str] = mapped_column("teamId", ForeignKey("Team.id", ondelete="CASCADE", onupdate="CASCADE", name="IncomingWebhook_teamId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("IncomingWebhook_externalId_key", "externalId", unique=True),
        Index("IncomingWebhook_teamId_idx", "teamId"),
    )

    team: Mapped["Team"] = relationship(back_populates="incoming_webhooks")

class RestrictedToken(Base):
    __tablename__ = "RestrictedToken"

    id: Mapped[str] = mapped_column(primary_key=True)
    name: Mapped[str]
    hashed_key: Mapped[str] = mapped_column("hashedKey")
    partial_key: Mapped[str] = mapped_column("partialKey")
    scopes: Mapped[Optional[str]]
    mode: Mapped[str] = mapped_column(default="live", server_default=text("'live'"))
    source: Mapped[str] = mapped_column(default="dashboard", server_default=text("'dashboard'"))
    subject_type: Mapped[str] = mapped_column("subjectType", default="user", server_default=text("'user'"))
    client_id: Mapped[Optional[str]] = mapped_column("clientId", ForeignKey("OAuthClient.id", ondelete="SET NULL", onupdate="CASCADE", name="RestrictedToken_clientId_fkey"))
    expires: Mapped[Optional[datetime]]
    last_used: Mapped[Optional[datetime]] = mapped_column("lastUsed")
    rate_limit: Mapped[int] = mapped_column("rateLimit", default=60, server_default=text("60"))
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="RestrictedToken_userId_fkey"))
    team_id: Mapped[str] = mapped_column("teamId", ForeignKey("Team.id", ondelete="CASCADE", onupdate="CASCADE", name="RestrictedToken_teamId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("RestrictedToken_hashedKey_key", "hashedKey", unique=True),
        Index("RestrictedToken_userId_idx", "userId"),
        Index("RestrictedToken_teamId_idx", "teamId"),
        Index("RestrictedToken_hashedKey_idx", "hashedKey"),
        Index("RestrictedToken_clientId_idx", "clientId"),
    )

    user: Mapped["User"] = relationship(back_populates="restricted_tokens")
    team: Mapped["Team"] = relationship(back_populates="restricted_tokens")
    oauth_client: Mapped[Optional["OAuthClient"]] = relationship(back_populates="tokens")

class Webhook(Base):
    __tablename__ = "Webhook"

    id: Mapped[str] = mapped_column(primary_key=True)
    p_id: Mapped[str] = mapped_column("pId")
    name: Mapped[str]
    url: Mapped[str]
    secret: Mapped[str]
    triggers: Mapped[dict[str, Any]] = mapped_column(JSONB)
    team_id: Mapped[str] = mapped_column("teamId", ForeignKey("Team.id", ondelete="CASCADE", onupdate="CASCADE", name="Webhook_teamId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("Webhook_pId_key", "pId", unique=True),
        Index("Webhook_teamId_idx", "teamId"),
    )

    team: Mapped["Team"] = relationship(back_populates="webhooks")

class YearInReview(Base):
    __tablename__ = "YearInReview"

    id: Mapped[str] = mapped_column(primary_key=True)
    team_id: Mapped[str] = mapped_column("teamId")
    status: Mapped[str] = mapped_column(default="pending", server_default=text("'pending'"))
    attempts: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    last_attempted: Mapped[Optional[datetime]] = mapped_column("lastAttempted")
    error: Mapped[Optional[str]]
    stats: Mapped[dict[str, Any]] = mapped_column(JSONB)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("YearInReview_status_attempts_idx", "status", "attempts"),
        Index("YearInReview_teamId_idx", "teamId"),
    )

class Tag(Base):
    __tablename__ = "Tag"

    id: Mapped[str] = mapped_column(primary_key=True)
    name: Mapped[str]
    color: Mapped[str]
    description: Mapped[Optional[str]]
    team_id: Mapped[str] = mapped_column("teamId", ForeignKey("Team.id", ondelete="CASCADE", onupdate="CASCADE", name="Tag_teamId_fkey"))
    created_by: Mapped[Optional[str]] = mapped_column("createdBy")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("Tag_teamId_idx", "teamId"),
        Index("Tag_name_idx", "name"),
        Index("Tag_id_idx", "id"),
        Index("Tag_teamId_name_key", "teamId", "name", unique=True),
    )

    team: Mapped["Team"] = relationship(back_populates="tag")
    items: Mapped[list["TagItem"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="tag")

class TagItem(Base):
    __tablename__ = "TagItem"

    id: Mapped[str] = mapped_column(primary_key=True)
    tag_id: Mapped[str] = mapped_column("tagId", ForeignKey("Tag.id", ondelete="CASCADE", onupdate="CASCADE", name="TagItem_tagId_fkey"))
    item_type: Mapped[str] = mapped_column("itemType", Enum("LINK_TAG", "DOCUMENT_TAG", "DATAROOM_TAG", name="TagType"))
    link_id: Mapped[Optional[str]] = mapped_column("linkId", ForeignKey("Link.id", ondelete="CASCADE", onupdate="CASCADE", name="TagItem_linkId_fkey"))
    document_id: Mapped[Optional[str]] = mapped_column("documentId", ForeignKey("Document.id", ondelete="CASCADE", onupdate="CASCADE", name="TagItem_documentId_fkey"))
    dataroom_id: Mapped[Optional[str]] = mapped_column("dataroomId", ForeignKey("Dataroom.id", ondelete="CASCADE", onupdate="CASCADE", name="TagItem_dataroomId_fkey"))
    tagged_by: Mapped[Optional[str]] = mapped_column("taggedBy")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("TagItem_tagId_linkId_idx", "tagId", "linkId"),
        Index("TagItem_tagId_documentId_idx", "tagId", "documentId"),
        Index("TagItem_tagId_dataroomId_idx", "tagId", "dataroomId"),
    )

    tag: Mapped["Tag"] = relationship(back_populates="items")
    link: Mapped[Optional["Link"]] = relationship(back_populates="tags")
    document: Mapped[Optional["Document"]] = relationship(back_populates="tags")
    dataroom: Mapped[Optional["Dataroom"]] = relationship(back_populates="tags")

class ViewerInvitation(Base):
    __tablename__ = "ViewerInvitation"

    id: Mapped[str] = mapped_column(primary_key=True)
    viewer_id: Mapped[str] = mapped_column("viewerId", ForeignKey("Viewer.id", ondelete="CASCADE", onupdate="CASCADE", name="ViewerInvitation_viewerId_fkey"))
    link_id: Mapped[str] = mapped_column("linkId", ForeignKey("Link.id", ondelete="CASCADE", onupdate="CASCADE", name="ViewerInvitation_linkId_fkey"))
    group_id: Mapped[Optional[str]] = mapped_column("groupId", ForeignKey("ViewerGroup.id", ondelete="SET NULL", onupdate="CASCADE", name="ViewerInvitation_groupId_fkey"))
    invited_by: Mapped[str] = mapped_column("invitedBy")
    custom_message: Mapped[Optional[str]] = mapped_column("customMessage")
    sent_at: Mapped[datetime] = mapped_column("sentAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    status: Mapped[str] = mapped_column(Enum("SENT", "FAILED", "BOUNCED", name="InvitationStatus"), default="SENT", server_default=text("'SENT'"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("ViewerInvitation_viewerId_idx", "viewerId"),
        Index("ViewerInvitation_linkId_idx", "linkId"),
        Index("ViewerInvitation_groupId_idx", "groupId"),
    )

    viewer: Mapped["Viewer"] = relationship(back_populates="invitations")
    link: Mapped["Link"] = relationship(back_populates="viewer_invitations")
    group: Mapped[Optional["ViewerGroup"]] = relationship(back_populates="invitations")

class Team(Base):
    __tablename__ = "Team"

    id: Mapped[str] = mapped_column(primary_key=True)
    name: Mapped[str]
    slug: Mapped[Optional[str]]
    default_brand_id: Mapped[Optional[str]] = mapped_column("defaultBrandId", ForeignKey("Brand.id", ondelete="SET NULL", onupdate="CASCADE", name="Team_defaultBrandId_fkey"))
    plan: Mapped[str] = mapped_column(default="free", server_default=text("'free'"))
    stripe_id: Mapped[Optional[str]] = mapped_column("stripeId")
    subscription_id: Mapped[Optional[str]] = mapped_column("subscriptionId")
    starts_at: Mapped[Optional[datetime]] = mapped_column("startsAt")
    ends_at: Mapped[Optional[datetime]] = mapped_column("endsAt")
    paused_at: Mapped[Optional[datetime]] = mapped_column("pausedAt")
    pause_starts_at: Mapped[Optional[datetime]] = mapped_column("pauseStartsAt")
    pause_ends_at: Mapped[Optional[datetime]] = mapped_column("pauseEndsAt")
    cancelled_at: Mapped[Optional[datetime]] = mapped_column("cancelledAt")
    trial_started_at: Mapped[Optional[datetime]] = mapped_column("trialStartedAt")
    trial_ends_at: Mapped[Optional[datetime]] = mapped_column("trialEndsAt")
    trial_reminder_run_id: Mapped[Optional[str]] = mapped_column("trialReminderRunId")
    trial_expired_run_id: Mapped[Optional[str]] = mapped_column("trialExpiredRunId")
    limits: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    enable_excel_advanced_mode: Mapped[bool] = mapped_column("enableExcelAdvancedMode", default=False, server_default=text("false"))
    replicate_dataroom_folders: Mapped[bool] = mapped_column("replicateDataroomFolders", default=True, server_default=text("true"))
    timezone: Mapped[str] = mapped_column(default="Etc/UTC", server_default=text("'Etc/UTC'"))
    sso_enabled: Mapped[bool] = mapped_column("ssoEnabled", default=False, server_default=text("false"))
    sso_email_domain: Mapped[Optional[str]] = mapped_column("ssoEmailDomain")
    sso_enforced_at: Mapped[Optional[datetime]] = mapped_column("ssoEnforcedAt")
    survey_data: Mapped[Optional[dict[str, Any]]] = mapped_column("surveyData", JSONB(none_as_null=True))
    agents_enabled: Mapped[bool] = mapped_column("agentsEnabled", default=False, server_default=text("false"))
    vector_store_id: Mapped[Optional[str]] = mapped_column("vectorStoreId")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    ignored_domains: Mapped[list[str]] = mapped_column("ignoredDomains", ARRAY(Text), nullable=True)
    global_block_list: Mapped[list[str]] = mapped_column("globalBlockList", ARRAY(Text), nullable=True)
    referral_track: Mapped[Optional[str]] = mapped_column("referralTrack", Enum("AFFILIATE", "PARTNER_DISCOUNT", name="ReferralTrack"))
    referral_track_selected_at: Mapped[Optional[datetime]] = mapped_column("referralTrackSelectedAt")
    referral_profile: Mapped[Optional[dict[str, Any]]] = mapped_column("referralProfile", JSONB(none_as_null=True))

    __table_args__ = (
        Index("Team_slug_key", "slug", unique=True),
        Index("Team_stripeId_key", "stripeId", unique=True),
        Index("Team_subscriptionId_key", "subscriptionId", unique=True),
        Index("Team_ssoEmailDomain_key", "ssoEmailDomain", unique=True),
        Index("Team_defaultBrandId_idx", "defaultBrandId"),
    )

    default_brand: Mapped[Optional["Brand"]] = relationship(back_populates="default_for_team")
    users: Mapped[list["UserTeam"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="team")
    user_datarooms: Mapped[list["UserDataroom"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="team")
    documents: Mapped[list["Document"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="team")
    folders: Mapped[list["Folder"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="team")
    domains: Mapped[list["Domain"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="team")
    invitations: Mapped[list["Invitation"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="team")
    sent_emails: Mapped[list["SentEmail"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="team")
    brands: Mapped[list["Brand"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="team")
    datarooms: Mapped[list["Dataroom"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="team")
    agreements: Mapped[list["Agreement"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="team")
    viewer_groups: Mapped[list["ViewerGroup"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="team")
    visitor_groups: Mapped[list["VisitorGroup"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="team")
    viewers: Mapped[list["Viewer"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="team")
    permission_groups: Mapped[list["PermissionGroup"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="team")
    link_presets: Mapped[list["LinkPreset"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="team")
    incoming_webhooks: Mapped[list["IncomingWebhook"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="team")
    restricted_tokens: Mapped[list["RestrictedToken"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="team")
    webhooks: Mapped[list["Webhook"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="team")
    conversations: Mapped[list["Conversation"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="team")
    dataroom_questions: Mapped[list["DataroomQuestion"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="team")
    dataroom_faq_items: Mapped[list["DataroomFaqItem"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="team")
    uploaded_documents: Mapped[list["DocumentUpload"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="team")
    tag: Mapped[list["Tag"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="team")
    annotations: Mapped[list["DocumentAnnotation"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="team")
    redaction_jobs: Mapped[list["DocumentRedactionJob"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="team")
    installed_integrations: Mapped[list["InstalledIntegration"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="team")
    links: Mapped[list["Link"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="team")
    views: Mapped[list["View"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="team")
    workflows: Mapped[list["Workflow"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="team")
    task_lists: Mapped[list["TaskList"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="team")
    tasks: Mapped[list["Task"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="team")
    chats: Mapped[list["Chat"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="team")
    notification_preferences: Mapped[list["NotificationPreference"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="team")
    referral_invites: Mapped[list["ReferralInvite"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="team")

class UserDataroom(Base):
    __tablename__ = "UserDataroom"

    user_id: Mapped[str] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="UserDataroom_userId_fkey"), primary_key=True)
    team_id: Mapped[str] = mapped_column("teamId", ForeignKey("Team.id", ondelete="CASCADE", onupdate="CASCADE", name="UserDataroom_teamId_fkey"))
    dataroom_id: Mapped[str] = mapped_column("dataroomId", ForeignKey("Dataroom.id", ondelete="CASCADE", onupdate="CASCADE", name="UserDataroom_dataroomId_fkey"), primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("UserDataroom_userId_idx", "userId"),
        Index("UserDataroom_teamId_idx", "teamId"),
        Index("UserDataroom_dataroomId_idx", "dataroomId"),
    )

    user: Mapped["User"] = relationship(back_populates="user_datarooms")
    team: Mapped["Team"] = relationship(back_populates="user_datarooms")
    dataroom: Mapped["Dataroom"] = relationship(back_populates="user_datarooms")

class UserTeam(Base):
    __tablename__ = "UserTeam"

    role: Mapped[str] = mapped_column(Enum("ADMIN", "MANAGER", "MEMBER", "DATAROOM_MEMBER", name="Role"), default="MEMBER", server_default=text("'MEMBER'"))
    status: Mapped[str] = mapped_column(default="ACTIVE", server_default=text("'ACTIVE'"))
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="UserTeam_userId_fkey"), primary_key=True)
    team_id: Mapped[str] = mapped_column("teamId", ForeignKey("Team.id", ondelete="CASCADE", onupdate="CASCADE", name="UserTeam_teamId_fkey"), primary_key=True)
    blocked_at: Mapped[Optional[datetime]] = mapped_column("blockedAt")
    notification_preferences: Mapped[Optional[dict[str, Any]]] = mapped_column("notificationPreferences", JSONB(none_as_null=True))

    __table_args__ = (
        Index("UserTeam_userId_idx", "userId"),
        Index("UserTeam_teamId_idx", "teamId"),
    )

    user: Mapped["User"] = relationship(back_populates="teams")
    team: Mapped["Team"] = relationship(back_populates="users")

class Workflow(Base):
    __tablename__ = "Workflow"

    id: Mapped[str] = mapped_column(primary_key=True)
    name: Mapped[str]
    description: Mapped[Optional[str]]
    entry_link_id: Mapped[str] = mapped_column("entryLinkId", ForeignKey("Link.id", ondelete="CASCADE", onupdate="CASCADE", name="Workflow_entryLinkId_fkey"))
    team_id: Mapped[str] = mapped_column("teamId", ForeignKey("Team.id", ondelete="CASCADE", onupdate="CASCADE", name="Workflow_teamId_fkey"))
    is_active: Mapped[bool] = mapped_column("isActive", default=True, server_default=text("true"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("Workflow_entryLinkId_key", "entryLinkId", unique=True),
        Index("Workflow_entryLinkId_idx", "entryLinkId"),
        Index("Workflow_teamId_idx", "teamId"),
        Index("Workflow_isActive_idx", "isActive"),
    )

    entry_link: Mapped["Link"] = relationship(back_populates="workflow")
    team: Mapped["Team"] = relationship(back_populates="workflows")
    steps: Mapped[list["WorkflowStep"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="workflow")
    executions: Mapped[list["WorkflowExecution"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="workflow")

class WorkflowStep(Base):
    __tablename__ = "WorkflowStep"

    id: Mapped[str] = mapped_column(primary_key=True)
    workflow_id: Mapped[str] = mapped_column("workflowId", ForeignKey("Workflow.id", ondelete="CASCADE", onupdate="CASCADE", name="WorkflowStep_workflowId_fkey"))
    name: Mapped[str]
    step_order: Mapped[int] = mapped_column("stepOrder")
    step_type: Mapped[str] = mapped_column("stepType", Enum("ROUTER", name="WorkflowStepType"), default="ROUTER", server_default=text("'ROUTER'"))
    conditions: Mapped[dict[str, Any]] = mapped_column(JSONB)
    actions: Mapped[dict[str, Any]] = mapped_column(JSONB)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("WorkflowStep_workflowId_idx", "workflowId"),
        Index("WorkflowStep_workflowId_stepOrder_key", "workflowId", "stepOrder", unique=True),
    )

    workflow: Mapped["Workflow"] = relationship(back_populates="steps")
    execution_logs: Mapped[list["WorkflowStepLog"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="step")

class WorkflowExecution(Base):
    __tablename__ = "WorkflowExecution"

    id: Mapped[str] = mapped_column(primary_key=True)
    workflow_id: Mapped[str] = mapped_column("workflowId", ForeignKey("Workflow.id", ondelete="CASCADE", onupdate="CASCADE", name="WorkflowExecution_workflowId_fkey"))
    visitor_email: Mapped[Optional[str]] = mapped_column("visitorEmail")
    visitor_ip: Mapped[Optional[str]] = mapped_column("visitorIp")
    status: Mapped[str] = mapped_column(Enum("PENDING", "IN_PROGRESS", "COMPLETED", "FAILED", "BLOCKED", name="ExecutionStatus"))
    started_at: Mapped[datetime] = mapped_column("startedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    completed_at: Mapped[Optional[datetime]] = mapped_column("completedAt")
    result: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    metadata_: Mapped[Optional[dict[str, Any]]] = mapped_column("metadata", JSONB(none_as_null=True))

    __table_args__ = (
        Index("WorkflowExecution_workflowId_startedAt_idx", "workflowId", "startedAt"),
        Index("WorkflowExecution_visitorEmail_idx", "visitorEmail"),
        Index("WorkflowExecution_status_idx", "status"),
    )

    workflow: Mapped["Workflow"] = relationship(back_populates="executions")
    step_logs: Mapped[list["WorkflowStepLog"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="execution")

class WorkflowStepLog(Base):
    __tablename__ = "WorkflowStepLog"

    id: Mapped[str] = mapped_column(primary_key=True)
    execution_id: Mapped[str] = mapped_column("executionId", ForeignKey("WorkflowExecution.id", ondelete="CASCADE", onupdate="CASCADE", name="WorkflowStepLog_executionId_fkey"))
    workflow_step_id: Mapped[str] = mapped_column("workflowStepId", ForeignKey("WorkflowStep.id", ondelete="CASCADE", onupdate="CASCADE", name="WorkflowStepLog_workflowStepId_fkey"))
    conditions_matched: Mapped[bool] = mapped_column("conditionsMatched")
    condition_results: Mapped[Optional[dict[str, Any]]] = mapped_column("conditionResults", JSONB(none_as_null=True))
    actions_executed: Mapped[Optional[dict[str, Any]]] = mapped_column("actionsExecuted", JSONB(none_as_null=True))
    executed_at: Mapped[datetime] = mapped_column("executedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    duration: Mapped[Optional[int]]
    error: Mapped[Optional[str]]

    __table_args__ = (
        Index("WorkflowStepLog_executionId_idx", "executionId"),
        Index("WorkflowStepLog_workflowStepId_idx", "workflowStepId"),
    )

    execution: Mapped["WorkflowExecution"] = relationship(back_populates="step_logs")
    step: Mapped["WorkflowStep"] = relationship(back_populates="execution_logs")
