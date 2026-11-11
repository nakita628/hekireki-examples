defmodule LiteLLM.LiteLLMLensIngestionKey do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          data: map()
        }

  schema "LiteLLM_LensIngestionKey" do
    field(:id, :string, primary_key: true)
    field(:data, :map)
  end
end