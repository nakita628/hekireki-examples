defmodule LiteLLM.LiteLLMLensWorker do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          token_hash: String.t(),
          data: map()
        }

  schema "LiteLLM_LensWorker" do
    field(:id, :string, primary_key: true)
    field(:token_hash, :string)
    field(:data, :map)
  end
end