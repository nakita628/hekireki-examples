defmodule LangWatch.Notification do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          metadata: map(),
          sent_at: DateTime.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          organization: LangWatch.Organization.t() | nil,
          project: LangWatch.Project.t() | nil
        }

  schema "Notification" do
    field(:id, :string, primary_key: true)
    field(:metadata, :map)
    field(:sent_at, LangWatch.PrismaDateTime, source: :sentAt)
    field(:organization_id, :string, source: :organizationId)
    field(:project_id, :string, source: :projectId)
    belongs_to(:organization, LangWatch.Organization, foreign_key: :organization_id, define_field: false, type: :string)
    belongs_to(:project, LangWatch.Project, foreign_key: :project_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end