defmodule Papermark.Dataroom do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Papermark.PrismaDateTime, autogenerate: {Papermark.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          p_id: String.t(),
          name: String.t(),
          internal_name: String.t() | nil,
          description: String.t() | nil,
          conversations_enabled: boolean(),
          agents_enabled: boolean(),
          vector_store_id: String.t() | nil,
          request_list_enabled: boolean(),
          enable_change_notifications: boolean(),
          enable_visitor_upload_change_notifications: boolean(),
          default_permission_strategy: atom(),
          default_show_banner: boolean(),
          default_group_permission_strategy: atom(),
          default_root_item_access: atom(),
          default_group_root_item_access: atom(),
          allow_bulk_download: boolean(),
          show_last_updated: boolean(),
          introduction_enabled: boolean(),
          introduction_content: map() | nil,
          is_frozen: boolean(),
          frozen_at: DateTime.t() | nil,
          freeze_archive_url: String.t() | nil,
          freeze_archive_hash: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          team: Papermark.Team.t() | nil,
          base_brand: Papermark.Brand.t() | nil,
          frozen_by_user: Papermark.User.t() | nil,
          brand: Papermark.DataroomBrand.t() | nil,
          documents: [Papermark.DataroomDocument.t()],
          folders: [Papermark.DataroomFolder.t()],
          links: [Papermark.Link.t()],
          views: [Papermark.View.t()],
          viewers: [Papermark.Viewer.t()],
          viewer_groups: [Papermark.ViewerGroup.t()],
          user_datarooms: [Papermark.UserDataroom.t()],
          permission_groups: [Papermark.PermissionGroup.t()],
          conversations: [Papermark.Conversation.t()],
          questions: [Papermark.DataroomQuestion.t()],
          faq_items: [Papermark.DataroomFaqItem.t()],
          chats: [Papermark.Chat.t()],
          uploaded_documents: [Papermark.DocumentUpload.t()],
          tags: [Papermark.TagItem.t()],
          task_lists: [Papermark.TaskList.t()],
          tasks: [Papermark.Task.t()]
        }

  schema "Dataroom" do
    field(:id, :string, primary_key: true)
    field(:p_id, :string, source: :pId)
    field(:name, :string)
    field(:internal_name, :string, source: :internalName)
    field(:description, :string)
    field(:conversations_enabled, :boolean, default: false, source: :conversationsEnabled)
    field(:agents_enabled, :boolean, default: false, source: :agentsEnabled)
    field(:vector_store_id, :string, source: :vectorStoreId)
    field(:request_list_enabled, :boolean, default: false, source: :requestListEnabled)
    field(:enable_change_notifications, :boolean, default: false, source: :enableChangeNotifications)
    field(:enable_visitor_upload_change_notifications, :boolean, default: false, source: :enableVisitorUploadChangeNotifications)
    field(:default_permission_strategy, Ecto.Enum, values: [:INHERIT_FROM_PARENT, :ASK_EVERY_TIME, :HIDDEN_BY_DEFAULT], default: :INHERIT_FROM_PARENT, source: :defaultPermissionStrategy)
    field(:default_show_banner, :boolean, default: true, source: :defaultShowBanner)
    field(:default_group_permission_strategy, Ecto.Enum, values: [:INHERIT_FROM_PARENT, :ASK_EVERY_TIME, :HIDDEN_BY_DEFAULT], default: :INHERIT_FROM_PARENT, source: :defaultGroupPermissionStrategy)
    field(:default_root_item_access, Ecto.Enum, values: [:VIEW_ONLY, :VIEW_AND_DOWNLOAD, :HIDDEN], default: :VIEW_ONLY, source: :defaultRootItemAccess)
    field(:default_group_root_item_access, Ecto.Enum, values: [:VIEW_ONLY, :VIEW_AND_DOWNLOAD, :HIDDEN], default: :VIEW_ONLY, source: :defaultGroupRootItemAccess)
    field(:allow_bulk_download, :boolean, default: true, source: :allowBulkDownload)
    field(:show_last_updated, :boolean, default: true, source: :showLastUpdated)
    field(:introduction_enabled, :boolean, default: false, source: :introductionEnabled)
    field(:introduction_content, :map, source: :introductionContent)
    field(:is_frozen, :boolean, default: false, source: :isFrozen)
    field(:frozen_at, Papermark.PrismaDateTime, source: :frozenAt)
    field(:freeze_archive_url, :string, source: :freezeArchiveUrl)
    field(:freeze_archive_hash, :string, source: :freezeArchiveHash)
    field(:team_id, :string, source: :teamId)
    field(:brand_id, :string, source: :brandId)
    field(:frozen_by, :string, source: :frozenBy)
    belongs_to(:team, Papermark.Team, foreign_key: :team_id, define_field: false, type: :string)
    belongs_to(:base_brand, Papermark.Brand, foreign_key: :brand_id, define_field: false, type: :string)
    belongs_to(:frozen_by_user, Papermark.User, foreign_key: :frozen_by, define_field: false, type: :string)
    has_one(:brand, Papermark.DataroomBrand, foreign_key: :dataroom_id)
    has_many(:documents, Papermark.DataroomDocument, foreign_key: :dataroom_id)
    has_many(:folders, Papermark.DataroomFolder, foreign_key: :dataroom_id)
    has_many(:links, Papermark.Link, foreign_key: :dataroom_id)
    has_many(:views, Papermark.View, foreign_key: :dataroom_id)
    has_many(:viewers, Papermark.Viewer, foreign_key: :dataroom_id)
    has_many(:viewer_groups, Papermark.ViewerGroup, foreign_key: :dataroom_id)
    has_many(:user_datarooms, Papermark.UserDataroom, foreign_key: :dataroom_id)
    has_many(:permission_groups, Papermark.PermissionGroup, foreign_key: :dataroom_id)
    has_many(:conversations, Papermark.Conversation, foreign_key: :dataroom_id)
    has_many(:questions, Papermark.DataroomQuestion, foreign_key: :dataroom_id)
    has_many(:faq_items, Papermark.DataroomFaqItem, foreign_key: :dataroom_id)
    has_many(:chats, Papermark.Chat, foreign_key: :dataroom_id)
    has_many(:uploaded_documents, Papermark.DocumentUpload, foreign_key: :dataroom_id)
    has_many(:tags, Papermark.TagItem, foreign_key: :dataroom_id)
    has_many(:task_lists, Papermark.TaskList, foreign_key: :dataroom_id)
    has_many(:tasks, Papermark.Task, foreign_key: :dataroom_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end