defmodule LangWatch.ProjectSecret do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          encrypted_value: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          project: LangWatch.Project.t() | nil,
          created_by: LangWatch.User.t() | nil,
          updated_by: LangWatch.User.t() | nil
        }

  schema "ProjectSecret" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:encrypted_value, :string, source: :encryptedValue)
    field(:project_id, :string, source: :projectId)
    field(:created_by_id, :string, source: :createdById)
    field(:updated_by_id, :string, source: :updatedById)
    belongs_to(:project, LangWatch.Project, foreign_key: :project_id, define_field: false, type: :string)
    belongs_to(:created_by, LangWatch.User, foreign_key: :created_by_id, define_field: false, type: :string)
    belongs_to(:updated_by, LangWatch.User, foreign_key: :updated_by_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end