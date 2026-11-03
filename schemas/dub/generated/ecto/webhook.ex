defmodule Dub.Webhook do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Dub.PrismaDateTime, autogenerate: {Dub.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          receiver: atom(),
          name: String.t(),
          url: String.t(),
          secret: String.t(),
          triggers: map(),
          link_scope: atom() | nil,
          consecutive_failures: integer(),
          last_failed_at: DateTime.t() | nil,
          disabled_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          project: Dub.Project.t() | nil,
          installed_integration: Dub.InstalledIntegration.t() | nil,
          links: [Dub.LinkWebhook.t()],
          folders: [Dub.FolderWebhook.t()]
        }

  schema "Webhook" do
    field(:id, :string, primary_key: true)
    field(:receiver, Ecto.Enum, values: [:user, :zapier, :make, :slack, :segment], default: :user)
    field(:name, :string)
    field(:url, :string)
    field(:secret, :string)
    field(:triggers, :map)
    field(:link_scope, Ecto.Enum, values: [:workspace, :folders, :links], source: :linkScope)
    field(:consecutive_failures, :integer, default: 0, source: :consecutiveFailures)
    field(:last_failed_at, Dub.PrismaDateTime, source: :lastFailedAt)
    field(:disabled_at, Dub.PrismaDateTime, source: :disabledAt)
    field(:project_id, :string, source: :projectId)
    field(:installation_id, :string, source: :installationId)
    belongs_to(:project, Dub.Project, foreign_key: :project_id, define_field: false, type: :string)
    belongs_to(:installed_integration, Dub.InstalledIntegration, foreign_key: :installation_id, define_field: false, type: :string)
    has_many(:links, Dub.LinkWebhook, foreign_key: :webhook_id)
    has_many(:folders, Dub.FolderWebhook, foreign_key: :webhook_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end