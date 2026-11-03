defmodule Langfuse.EvaluationRule do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Langfuse.PrismaDateTime, autogenerate: {Langfuse.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          status: atom(),
          target_object: String.t(),
          filter: map(),
          sampling: Decimal.t(),
          delay: integer(),
          idle_time: integer() | nil,
          time_scope: [String.t()],
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          project: Langfuse.Project.t() | nil,
          created_by_user: Langfuse.User.t() | nil,
          assignments: [Langfuse.EvaluationRuleEvaluatorAssignment.t()]
        }

  schema "evaluation_rules" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:status, Ecto.Enum, values: [:ACTIVE, :INACTIVE], default: :ACTIVE)
    field(:target_object, :string)
    field(:filter, :map)
    field(:sampling, :decimal)
    field(:delay, :integer)
    field(:idle_time, :integer)
    field(:time_scope, {:array, :string})
    belongs_to(:project, Langfuse.Project, foreign_key: :project_id, type: :string)
    belongs_to(:created_by_user, Langfuse.User, foreign_key: :created_by_user_id, type: :string)
    has_many(:assignments, Langfuse.EvaluationRuleEvaluatorAssignment, foreign_key: :evaluation_rule_id)
    timestamps(inserted_at_source: :created_at)
  end
end