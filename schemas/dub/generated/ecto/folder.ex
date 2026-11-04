defmodule Dub.Folder do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Dub.PrismaDateTime, autogenerate: {Dub.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          description: String.t() | nil,
          type: atom(),
          access_level: atom() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          project: Dub.Project.t() | nil,
          dashboard: Dub.Dashboard.t() | nil,
          links: [Dub.Link.t()],
          users: [Dub.FolderUser.t()],
          access_requests: [Dub.FolderAccessRequest.t()],
          webhooks: [Dub.FolderWebhook.t()]
        }

  schema "Folder" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:description, :string)
    field(:type, Ecto.Enum, values: [:default, :mega], default: :default)
    field(:access_level, Ecto.Enum, values: [:read, :write], source: :accessLevel)
    field(:project_id, :string, source: :projectId)
    belongs_to(:project, Dub.Project, foreign_key: :project_id, define_field: false, type: :string)
    has_one(:dashboard, Dub.Dashboard, foreign_key: :folder_id, references: :id)
    has_many(:links, Dub.Link, foreign_key: :folder_id, references: :id)
    has_many(:users, Dub.FolderUser, foreign_key: :folder_id, references: :id)
    has_many(:access_requests, Dub.FolderAccessRequest, foreign_key: :folder_id, references: :id)
    has_many(:webhooks, Dub.FolderWebhook, foreign_key: :folder_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end