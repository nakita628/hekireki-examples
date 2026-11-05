defmodule OpenPanel.Cohort do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: OpenPanel.PrismaDateTime, autogenerate: {OpenPanel.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          description: String.t() | nil,
          definition: map(),
          is_static: boolean(),
          profile_count: integer(),
          last_computed_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          project: OpenPanel.Project.t() | nil
        }

  schema "cohorts" do
    field(:id, :string, primary_key: true, read_after_writes: true)
    field(:name, :string)
    field(:description, :string)
    field(:definition, :map, default: %{})
    field(:is_static, :boolean, default: false, source: :isStatic)
    field(:profile_count, :integer, default: 0, source: :profileCount)
    field(:last_computed_at, OpenPanel.PrismaDateTime, source: :lastComputedAt)
    field(:project_id, :string, source: :projectId)
    belongs_to(:project, OpenPanel.Project, foreign_key: :project_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end