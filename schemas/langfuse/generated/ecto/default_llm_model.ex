defmodule Langfuse.DefaultLlmModel do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Langfuse.PrismaDateTime, autogenerate: {Langfuse.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          provider: String.t(),
          adapter: String.t(),
          model: String.t(),
          model_params: map() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          project: Langfuse.Project.t() | nil,
          llm_api_key: Langfuse.LlmApiKeys.t() | nil
        }

  schema "default_llm_models" do
    field(:id, :string, primary_key: true)
    field(:provider, :string)
    field(:adapter, :string)
    field(:model, :string)
    field(:model_params, :map)
    belongs_to(:project, Langfuse.Project, foreign_key: :project_id, type: :string)
    belongs_to(:llm_api_key, Langfuse.LlmApiKeys, foreign_key: :llm_api_key_id, type: :string)
    timestamps(inserted_at_source: :created_at)
  end
end