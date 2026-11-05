defmodule OpenPanel.Client do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: OpenPanel.PrismaDateTime, autogenerate: {OpenPanel.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          secret: String.t() | nil,
          type: atom(),
          ignore_cors_and_secret: boolean(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          project: OpenPanel.Project.t() | nil,
          organization: OpenPanel.Organization.t() | nil
        }

  schema "clients" do
    field(:id, :string, primary_key: true, read_after_writes: true)
    field(:name, :string)
    field(:secret, :string)
    field(:type, Ecto.Enum, values: [:read, :write, :root], default: :write)
    field(:ignore_cors_and_secret, :boolean, default: false, source: :ignoreCorsAndSecret)
    field(:project_id, :string, source: :projectId)
    field(:organization_id, :string, source: :organizationId)
    belongs_to(:project, OpenPanel.Project, foreign_key: :project_id, define_field: false, type: :string)
    belongs_to(:organization, OpenPanel.Organization, foreign_key: :organization_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end