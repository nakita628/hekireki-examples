defmodule Langfuse.TopicConfig do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Langfuse.PrismaDateTime, autogenerate: {Langfuse.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          enabled: boolean(),
          blocked_at: DateTime.t() | nil,
          block_reason: atom() | nil,
          block_message: String.t() | nil,
          summary_model: String.t() | nil,
          embedding_model: String.t() | nil,
          embedding_dimensions: integer(),
          clustering_model: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          created_by_user: Langfuse.User.t() | nil,
          updated_by_user: Langfuse.User.t() | nil,
          project: Langfuse.Project.t() | nil,
          summary_llm_api_key: Langfuse.LlmApiKeys.t() | nil,
          embedding_llm_api_key: Langfuse.LlmApiKeys.t() | nil,
          clustering_llm_api_key: Langfuse.LlmApiKeys.t() | nil
        }

  schema "topic_configs" do
    field(:id, :string, primary_key: true)
    field(:enabled, :boolean, default: false)
    field(:blocked_at, Langfuse.PrismaDateTime)
    field(:block_reason, Ecto.Enum, values: [:LLM_CONNECTION_AUTH_INVALID, :LLM_CONNECTION_BILLING_EXHAUSTED, :LLM_CONNECTION_ENDPOINT_UNREACHABLE, :LLM_CONNECTION_MISSING, :DEFAULT_EVAL_MODEL_MISSING, :EVAL_MODEL_CONFIG_INVALID, :EVAL_MODEL_UNAVAILABLE, :PROVIDER_ACCOUNT_NOT_READY])
    field(:block_message, :string)
    field(:summary_model, :string)
    field(:embedding_model, :string)
    field(:embedding_dimensions, :integer, default: 1024)
    field(:clustering_model, :string)
    belongs_to(:created_by_user, Langfuse.User, foreign_key: :created_by, type: :string)
    belongs_to(:updated_by_user, Langfuse.User, foreign_key: :updated_by, type: :string)
    belongs_to(:project, Langfuse.Project, foreign_key: :project_id, type: :string)
    belongs_to(:summary_llm_api_key, Langfuse.LlmApiKeys, foreign_key: :summary_llm_api_key_id, type: :string)
    belongs_to(:embedding_llm_api_key, Langfuse.LlmApiKeys, foreign_key: :embedding_llm_api_key_id, type: :string)
    belongs_to(:clustering_llm_api_key, Langfuse.LlmApiKeys, foreign_key: :clustering_llm_api_key_id, type: :string)
    timestamps(inserted_at_source: :created_at)
  end
end