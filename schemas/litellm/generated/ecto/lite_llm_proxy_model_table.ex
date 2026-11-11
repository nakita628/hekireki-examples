defmodule LiteLLM.LiteLLMProxyModelTable do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true, source: :model_id}
  @foreign_key_type :binary_id
  @timestamps_opts [type: LiteLLM.PrismaDateTime, autogenerate: {LiteLLM.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          model_name: String.t(),
          litellm_params: map(),
          model_info: map() | nil,
          blocked: boolean(),
          created_by: String.t(),
          updated_by: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "LiteLLM_ProxyModelTable" do
    field(:model_name, :string)
    field(:litellm_params, :map)
    field(:model_info, :map)
    field(:blocked, :boolean, default: false)
    field(:created_by, :string)
    field(:updated_by, :string)
    timestamps(inserted_at_source: :created_at)
  end
end