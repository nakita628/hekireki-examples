defmodule Papermark.Link do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Papermark.PrismaDateTime, autogenerate: {Papermark.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          link_type: atom(),
          url: String.t() | nil,
          name: String.t() | nil,
          slug: String.t() | nil,
          expires_at: DateTime.t() | nil,
          password: String.t() | nil,
          allow_list: [String.t()],
          deny_list: [String.t()],
          email_protected: boolean(),
          email_authenticated: boolean(),
          allow_download: boolean() | nil,
          is_archived: boolean(),
          deleted_at: DateTime.t() | nil,
          domain_slug: String.t() | nil,
          enable_notification: boolean() | nil,
          enable_feedback: boolean() | nil,
          enable_question: boolean() | nil,
          enable_screenshot_protection: boolean() | nil,
          enable_confidential_view: boolean() | nil,
          enable_agreement: boolean() | nil,
          show_banner: boolean() | nil,
          enable_watermark: boolean() | nil,
          watermark_config: map() | nil,
          audience_type: atom(),
          meta_title: String.t() | nil,
          meta_description: String.t() | nil,
          meta_image: String.t() | nil,
          meta_favicon: String.t() | nil,
          enable_custom_metatag: boolean() | nil,
          welcome_message: String.t() | nil,
          enable_conversation: boolean(),
          enable_aiagents: boolean() | nil,
          enable_upload: boolean() | nil,
          is_file_request_only: boolean() | nil,
          upload_folder_id: String.t() | nil,
          upload_folder_ids: [String.t()],
          enable_index_file: boolean() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          document: Papermark.Document.t() | nil,
          dataroom: Papermark.Dataroom.t() | nil,
          domain: Papermark.Domain.t() | nil,
          agreement: Papermark.Agreement.t() | nil,
          group: Papermark.ViewerGroup.t() | nil,
          permission_group: Papermark.PermissionGroup.t() | nil,
          team: Papermark.Team.t() | nil,
          brand: Papermark.Brand.t() | nil,
          owner: Papermark.User.t() | nil,
          feedback: Papermark.Feedback.t() | nil,
          workflow: Papermark.Workflow.t() | nil,
          views: [Papermark.View.t()],
          conversations: [Papermark.Conversation.t()],
          question_assignments: [Papermark.DataroomQuestionAssignment.t()],
          dataroom_faq_items: [Papermark.DataroomFaqItem.t()],
          chats: [Papermark.Chat.t()],
          uploaded_documents: [Papermark.DocumentUpload.t()],
          custom_fields: [Papermark.CustomField.t()],
          tags: [Papermark.TagItem.t()],
          viewer_invitations: [Papermark.ViewerInvitation.t()],
          visitor_groups: [Papermark.LinkVisitorGroup.t()],
          task_assignments: [Papermark.TaskAssignment.t()]
        }

  schema "Link" do
    field(:id, :string, primary_key: true)
    field(:link_type, Ecto.Enum, values: [:DOCUMENT_LINK, :DATAROOM_LINK, :WORKFLOW_LINK], default: :DOCUMENT_LINK, source: :linkType)
    field(:url, :string)
    field(:name, :string)
    field(:slug, :string)
    field(:expires_at, Papermark.PrismaDateTime, source: :expiresAt)
    field(:password, :string)
    field(:allow_list, {:array, :string}, source: :allowList)
    field(:deny_list, {:array, :string}, source: :denyList)
    field(:email_protected, :boolean, default: true, source: :emailProtected)
    field(:email_authenticated, :boolean, default: false, source: :emailAuthenticated)
    field(:allow_download, :boolean, default: false, source: :allowDownload)
    field(:is_archived, :boolean, default: false, source: :isArchived)
    field(:deleted_at, Papermark.PrismaDateTime, source: :deletedAt)
    field(:domain_slug, :string, source: :domainSlug)
    field(:enable_notification, :boolean, default: true, source: :enableNotification)
    field(:enable_feedback, :boolean, default: false, source: :enableFeedback)
    field(:enable_question, :boolean, default: false, source: :enableQuestion)
    field(:enable_screenshot_protection, :boolean, default: false, source: :enableScreenshotProtection)
    field(:enable_confidential_view, :boolean, default: false, source: :enableConfidentialView)
    field(:enable_agreement, :boolean, default: false, source: :enableAgreement)
    field(:show_banner, :boolean, default: false, source: :showBanner)
    field(:enable_watermark, :boolean, default: false, source: :enableWatermark)
    field(:watermark_config, :map, source: :watermarkConfig)
    field(:audience_type, Ecto.Enum, values: [:GENERAL, :GROUP, :TEAM], default: :GENERAL, source: :audienceType)
    field(:meta_title, :string, source: :metaTitle)
    field(:meta_description, :string, source: :metaDescription)
    field(:meta_image, :string, source: :metaImage)
    field(:meta_favicon, :string, source: :metaFavicon)
    field(:enable_custom_metatag, :boolean, default: false, source: :enableCustomMetatag)
    field(:welcome_message, :string, source: :welcomeMessage)
    field(:enable_conversation, :boolean, default: false, source: :enableConversation)
    field(:enable_aiagents, :boolean, default: false, source: :enableAIAgents)
    field(:enable_upload, :boolean, default: false, source: :enableUpload)
    field(:is_file_request_only, :boolean, default: false, source: :isFileRequestOnly)
    field(:upload_folder_id, :string, source: :uploadFolderId)
    field(:upload_folder_ids, {:array, :string}, source: :uploadFolderIds)
    field(:enable_index_file, :boolean, default: false, source: :enableIndexFile)
    field(:document_id, :string, source: :documentId)
    field(:dataroom_id, :string, source: :dataroomId)
    field(:domain_id, :string, source: :domainId)
    field(:agreement_id, :string, source: :agreementId)
    field(:group_id, :string, source: :groupId)
    field(:permission_group_id, :string, source: :permissionGroupId)
    field(:team_id, :string, source: :teamId)
    field(:brand_id, :string, source: :brandId)
    field(:owner_id, :string, source: :ownerId)
    belongs_to(:document, Papermark.Document, foreign_key: :document_id, define_field: false, type: :string)
    belongs_to(:dataroom, Papermark.Dataroom, foreign_key: :dataroom_id, define_field: false, type: :string)
    belongs_to(:domain, Papermark.Domain, foreign_key: :domain_id, define_field: false, type: :string)
    belongs_to(:agreement, Papermark.Agreement, foreign_key: :agreement_id, define_field: false, type: :string)
    belongs_to(:group, Papermark.ViewerGroup, foreign_key: :group_id, define_field: false, type: :string)
    belongs_to(:permission_group, Papermark.PermissionGroup, foreign_key: :permission_group_id, define_field: false, type: :string)
    belongs_to(:team, Papermark.Team, foreign_key: :team_id, define_field: false, type: :string)
    belongs_to(:brand, Papermark.Brand, foreign_key: :brand_id, define_field: false, type: :string)
    belongs_to(:owner, Papermark.User, foreign_key: :owner_id, define_field: false, type: :string)
    has_one(:feedback, Papermark.Feedback, foreign_key: :link_id, references: :id)
    has_one(:workflow, Papermark.Workflow, foreign_key: :entry_link_id, references: :id)
    has_many(:views, Papermark.View, foreign_key: :link_id, references: :id)
    has_many(:conversations, Papermark.Conversation, foreign_key: :link_id, references: :id)
    has_many(:question_assignments, Papermark.DataroomQuestionAssignment, foreign_key: :link_id, references: :id)
    has_many(:dataroom_faq_items, Papermark.DataroomFaqItem, foreign_key: :link_id, references: :id)
    has_many(:chats, Papermark.Chat, foreign_key: :link_id, references: :id)
    has_many(:uploaded_documents, Papermark.DocumentUpload, foreign_key: :link_id, references: :id)
    has_many(:custom_fields, Papermark.CustomField, foreign_key: :link_id, references: :id)
    has_many(:tags, Papermark.TagItem, foreign_key: :link_id, references: :id)
    has_many(:viewer_invitations, Papermark.ViewerInvitation, foreign_key: :link_id, references: :id)
    has_many(:visitor_groups, Papermark.LinkVisitorGroup, foreign_key: :link_id, references: :id)
    has_many(:task_assignments, Papermark.TaskAssignment, foreign_key: :link_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end