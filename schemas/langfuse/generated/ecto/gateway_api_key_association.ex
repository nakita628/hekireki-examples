defmodule Langfuse.GatewayApiKeyAssociation do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          metadata: map(),
          api_key: Langfuse.ApiKey.t() | nil
        }

  schema "gateway_api_key_associations" do
    field(:metadata, :map, default: %{})
    belongs_to(:api_key, Langfuse.ApiKey, foreign_key: :api_key_id, type: :string)
  end
end