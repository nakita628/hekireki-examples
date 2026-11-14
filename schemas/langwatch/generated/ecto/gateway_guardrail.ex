defmodule LangWatch.GatewayGuardrail do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          description: String.t() | nil,
          direction: atom(),
          failure_mode: atom(),
          archived_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          project: LangWatch.Project.t() | nil,
          evaluator: LangWatch.Evaluator.t() | nil,
          created_by: LangWatch.User.t() | nil,
          updated_by: LangWatch.User.t() | nil
        }

  schema "GatewayGuardrail" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:description, :string)
    field(:direction, Ecto.Enum, values: [:PRE, :POST, :STREAM_CHUNK])
    field(:failure_mode, Ecto.Enum, values: [:FAIL_OPEN, :FAIL_CLOSED], default: :FAIL_CLOSED, source: :failureMode)
    field(:archived_at, LangWatch.PrismaDateTime, source: :archivedAt)
    field(:project_id, :string, source: :projectId)
    field(:evaluator_id, :string, source: :evaluatorId)
    field(:created_by_id, :string, source: :createdById)
    field(:updated_by_id, :string, source: :updatedById)
    belongs_to(:project, LangWatch.Project, foreign_key: :project_id, define_field: false, type: :string)
    belongs_to(:evaluator, LangWatch.Evaluator, foreign_key: :evaluator_id, define_field: false, type: :string)
    belongs_to(:created_by, LangWatch.User, foreign_key: :created_by_id, define_field: false, type: :string)
    belongs_to(:updated_by, LangWatch.User, foreign_key: :updated_by_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end