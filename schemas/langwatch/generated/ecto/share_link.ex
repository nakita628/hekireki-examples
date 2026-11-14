defmodule LangWatch.ShareLink do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          token: String.t(),
          resource_type: atom(),
          resource_id: String.t(),
          thread_id: String.t() | nil,
          visibility: atom(),
          expires_at: DateTime.t() | nil,
          max_views: integer() | nil,
          view_count: integer(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          project: LangWatch.Project.t() | nil,
          user: LangWatch.User.t() | nil
        }

  schema "ShareLink" do
    field(:id, :string, primary_key: true)
    field(:token, :string)
    field(:resource_type, Ecto.Enum, values: [:TRACE, :THREAD], source: :resourceType)
    field(:resource_id, :string, source: :resourceId)
    field(:thread_id, :string, source: :threadId)
    field(:visibility, Ecto.Enum, values: [:PUBLIC, :ORGANIZATION, :PROJECT], default: :PUBLIC)
    field(:expires_at, LangWatch.PrismaDateTime, source: :expiresAt)
    field(:max_views, :integer, source: :maxViews)
    field(:view_count, :integer, default: 0, source: :viewCount)
    field(:project_id, :string, source: :projectId)
    field(:user_id, :string, source: :userId)
    belongs_to(:project, LangWatch.Project, foreign_key: :project_id, define_field: false, type: :string)
    belongs_to(:user, LangWatch.User, foreign_key: :user_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end