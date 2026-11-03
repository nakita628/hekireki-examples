defmodule Langfuse.LlmApiKeys do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Langfuse.PrismaDateTime, autogenerate: {Langfuse.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          provider: String.t(),
          adapter: String.t(),
          display_secret_key: String.t(),
          secret_key: String.t(),
          base_url: String.t() | nil,
          custom_models: [String.t()],
          with_default_models: boolean(),
          extra_headers: String.t() | nil,
          extra_header_keys: [String.t()],
          config: map() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          project: Langfuse.Project.t() | nil,
          organization: Langfuse.Organization.t() | nil,
          default_llm_model: [Langfuse.DefaultLlmModel.t()],
          topic_summary_configs: [Langfuse.TopicConfig.t()],
          topic_embedding_configs: [Langfuse.TopicConfig.t()],
          topic_clustering_configs: [Langfuse.TopicConfig.t()]
        }

  schema "llm_api_keys" do
    field(:id, :string, primary_key: true)
    field(:provider, :string)
    field(:adapter, :string)
    field(:display_secret_key, :string)
    field(:secret_key, :string)
    field(:base_url, :string)
    field(:custom_models, {:array, :string})
    field(:with_default_models, :boolean, default: true)
    field(:extra_headers, :string)
    field(:extra_header_keys, {:array, :string})
    field(:config, :map)
    belongs_to(:project, Langfuse.Project, foreign_key: :project_id, type: :string)
    belongs_to(:organization, Langfuse.Organization, foreign_key: :organization_id, type: :string)
    has_many(:default_llm_model, Langfuse.DefaultLlmModel, foreign_key: :llm_api_key_id)
    has_many(:topic_summary_configs, Langfuse.TopicConfig, foreign_key: :summary_llm_api_key_id)
    has_many(:topic_embedding_configs, Langfuse.TopicConfig, foreign_key: :embedding_llm_api_key_id)
    has_many(:topic_clustering_configs, Langfuse.TopicConfig, foreign_key: :clustering_llm_api_key_id)
    timestamps(inserted_at_source: :created_at)
  end
end