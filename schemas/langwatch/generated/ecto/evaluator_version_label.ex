defmodule LangWatch.EvaluatorVersionLabel do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          project_id: String.t(),
          evaluator_id: String.t(),
          label: String.t(),
          version_id: String.t(),
          created_by_id: String.t() | nil,
          updated_by_id: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "EvaluatorVersionLabel" do
    field(:id, :string, primary_key: true)
    field(:project_id, :string, source: :projectId)
    field(:evaluator_id, :string, source: :evaluatorId)
    field(:label, :string)
    field(:version_id, :string, source: :versionId)
    field(:created_by_id, :string, source: :createdById)
    field(:updated_by_id, :string, source: :updatedById)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end