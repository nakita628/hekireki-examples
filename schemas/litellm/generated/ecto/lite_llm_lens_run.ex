defmodule LiteLLM.LiteLLMLensRun do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          lens_id: String.t(),
          created_at: DateTime.t(),
          data: map()
        }

  schema "LiteLLM_LensRun" do
    field(:id, :string, primary_key: true)
    field(:lens_id, :string)
    field(:created_at, LiteLLM.PrismaDateTime)
    field(:data, :map)
  end
end