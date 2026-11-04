defmodule Langfuse.Evaluator do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Langfuse.PrismaDateTime, autogenerate: {Langfuse.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          type: atom(),
          is_built_in: boolean(),
          description: String.t() | nil,
          blocked_at: DateTime.t() | nil,
          block_reason: atom() | nil,
          block_message: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          project: Langfuse.Project.t() | nil,
          created_by_user: Langfuse.User.t() | nil,
          versions: [Langfuse.EvaluatorVersion.t()],
          assignments: [Langfuse.EvaluationRuleEvaluatorAssignment.t()]
        }

  schema "evaluators" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:type, Ecto.Enum, values: [:LLM_AS_JUDGE, :CODE, :DECISION_MODEL, :FACET])
    field(:is_built_in, :boolean, default: false)
    field(:description, :string)
    field(:blocked_at, Langfuse.PrismaDateTime)
    field(:block_reason, Ecto.Enum, values: [:LLM_CONNECTION_AUTH_INVALID, :LLM_CONNECTION_BILLING_EXHAUSTED, :LLM_CONNECTION_ENDPOINT_UNREACHABLE, :LLM_CONNECTION_MISSING, :DEFAULT_EVAL_MODEL_MISSING, :EVAL_MODEL_CONFIG_INVALID, :EVAL_MODEL_UNAVAILABLE, :PROVIDER_ACCOUNT_NOT_READY])
    field(:block_message, :string)
    belongs_to(:project, Langfuse.Project, foreign_key: :project_id, type: :string)
    belongs_to(:created_by_user, Langfuse.User, foreign_key: :created_by_user_id, type: :string)
    has_many(:versions, Langfuse.EvaluatorVersion, foreign_key: :evaluator_id, references: :id)
    has_many(:assignments, Langfuse.EvaluationRuleEvaluatorAssignment, foreign_key: :evaluator_id, references: :id)
    timestamps(inserted_at_source: :created_at)
  end
end