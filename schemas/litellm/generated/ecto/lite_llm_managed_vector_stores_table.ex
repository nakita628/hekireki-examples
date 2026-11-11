defmodule LiteLLM.LiteLLMManagedVectorStoresTable do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LiteLLM.PrismaDateTime, autogenerate: {LiteLLM.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          vector_store_id: String.t(),
          custom_llm_provider: String.t(),
          vector_store_name: String.t() | nil,
          vector_store_description: String.t() | nil,
          vector_store_metadata: map() | nil,
          litellm_credential_name: String.t() | nil,
          litellm_params: map() | nil,
          team_id: String.t() | nil,
          user_id: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "LiteLLM_ManagedVectorStoresTable" do
    field(:vector_store_id, :string, primary_key: true)
    field(:custom_llm_provider, :string)
    field(:vector_store_name, :string)
    field(:vector_store_description, :string)
    field(:vector_store_metadata, :map)
    field(:litellm_credential_name, :string)
    field(:litellm_params, :map)
    field(:team_id, :string)
    field(:user_id, :string)
    timestamps(inserted_at_source: :created_at)
  end
end