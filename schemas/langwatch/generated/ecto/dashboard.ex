defmodule LangWatch.Dashboard do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          order: integer(),
          description: String.t() | nil,
          created_by_id: String.t() | nil,
          scope: atom(),
          organization_id: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          project: LangWatch.Project.t() | nil,
          graphs: [LangWatch.CustomGraph.t()]
        }

  schema "Dashboard" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:order, :integer, default: 0)
    field(:description, :string)
    field(:created_by_id, :string, source: :createdById)
    field(:scope, Ecto.Enum, values: [:PRIVATE, :PROJECT, :ORGANIZATION], default: :PROJECT)
    field(:organization_id, :string, source: :organizationId)
    field(:project_id, :string, source: :projectId)
    belongs_to(:project, LangWatch.Project, foreign_key: :project_id, define_field: false, type: :string)
    has_many(:graphs, LangWatch.CustomGraph, foreign_key: :dashboard_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end