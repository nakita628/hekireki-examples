defmodule Langfuse.JobConfiguration do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Langfuse.PrismaDateTime, autogenerate: {Langfuse.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          job_type: atom(),
          status: atom(),
          blocked_at: DateTime.t() | nil,
          block_reason: atom() | nil,
          block_message: String.t() | nil,
          score_name: String.t(),
          filter: map(),
          target_object: String.t(),
          variable_mapping: map(),
          sampling: Decimal.t(),
          delay: integer(),
          time_scope: [String.t()],
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          project: Langfuse.Project.t() | nil,
          eval_template: Langfuse.EvalTemplate.t() | nil
        }

  schema "job_configurations" do
    field(:id, :string, primary_key: true)
    field(:job_type, Ecto.Enum, values: [:EVAL])
    field(:status, Ecto.Enum, values: [:ACTIVE, :INACTIVE], default: :ACTIVE)
    field(:blocked_at, Langfuse.PrismaDateTime)
    field(:block_reason, Ecto.Enum, values: [:LLM_CONNECTION_AUTH_INVALID, :LLM_CONNECTION_BILLING_EXHAUSTED, :LLM_CONNECTION_ENDPOINT_UNREACHABLE, :LLM_CONNECTION_MISSING, :DEFAULT_EVAL_MODEL_MISSING, :EVAL_MODEL_CONFIG_INVALID, :EVAL_MODEL_UNAVAILABLE, :PROVIDER_ACCOUNT_NOT_READY])
    field(:block_message, :string)
    field(:score_name, :string)
    field(:filter, :map)
    field(:target_object, :string)
    field(:variable_mapping, :map)
    field(:sampling, :decimal)
    field(:delay, :integer)
    field(:time_scope, {:array, :string})
    belongs_to(:project, Langfuse.Project, foreign_key: :project_id, type: :string)
    belongs_to(:eval_template, Langfuse.EvalTemplate, foreign_key: :eval_template_id, type: :string)
    timestamps(inserted_at_source: :created_at)
  end
end