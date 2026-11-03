defmodule Papermark.Chat do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Papermark.PrismaDateTime, autogenerate: {Papermark.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          title: String.t() | nil,
          vector_store_id: String.t() | nil,
          last_message_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          team: Papermark.Team.t() | nil,
          document: Papermark.Document.t() | nil,
          dataroom: Papermark.Dataroom.t() | nil,
          link: Papermark.Link.t() | nil,
          view: Papermark.View.t() | nil,
          user: Papermark.User.t() | nil,
          viewer: Papermark.Viewer.t() | nil,
          messages: [Papermark.ChatMessage.t()]
        }

  schema "Chat" do
    field(:id, :string, primary_key: true)
    field(:title, :string)
    field(:vector_store_id, :string, source: :vectorStoreId)
    field(:last_message_at, Papermark.PrismaDateTime, source: :lastMessageAt)
    field(:team_id, :string, source: :teamId)
    field(:document_id, :string, source: :documentId)
    field(:dataroom_id, :string, source: :dataroomId)
    field(:link_id, :string, source: :linkId)
    field(:view_id, :string, source: :viewId)
    field(:user_id, :string, source: :userId)
    field(:viewer_id, :string, source: :viewerId)
    belongs_to(:team, Papermark.Team, foreign_key: :team_id, define_field: false, type: :string)
    belongs_to(:document, Papermark.Document, foreign_key: :document_id, define_field: false, type: :string)
    belongs_to(:dataroom, Papermark.Dataroom, foreign_key: :dataroom_id, define_field: false, type: :string)
    belongs_to(:link, Papermark.Link, foreign_key: :link_id, define_field: false, type: :string)
    belongs_to(:view, Papermark.View, foreign_key: :view_id, define_field: false, type: :string)
    belongs_to(:user, Papermark.User, foreign_key: :user_id, define_field: false, type: :string)
    belongs_to(:viewer, Papermark.Viewer, foreign_key: :viewer_id, define_field: false, type: :string)
    has_many(:messages, Papermark.ChatMessage, foreign_key: :chat_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end