defmodule LangWatch.Cost do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          cost_type: atom(),
          cost_name: String.t() | nil,
          reference_type: atom(),
          reference_id: String.t(),
          amount: float(),
          currency: String.t(),
          extra_info: map() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          project: LangWatch.Project.t() | nil
        }

  schema "Cost" do
    field(:id, :string, primary_key: true)
    field(:cost_type, Ecto.Enum, values: [:TRACE_CHECK, :GUARDRAIL, :CLUSTERING, :BATCH_EVALUATION], source: :costType)
    field(:cost_name, :string, source: :costName)
    field(:reference_type, Ecto.Enum, values: [:CHECK, :TRACE, :PROJECT, :BATCH], source: :referenceType)
    field(:reference_id, :string, source: :referenceId)
    field(:amount, :float)
    field(:currency, :string)
    field(:extra_info, :map, source: :extraInfo)
    field(:project_id, :string, source: :projectId)
    belongs_to(:project, LangWatch.Project, foreign_key: :project_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end