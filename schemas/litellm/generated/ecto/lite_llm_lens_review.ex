defmodule LiteLLM.LiteLLMLensReview do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          lens_id: String.t(),
          criteria_key: String.t(),
          execution_id: String.t(),
          data: map(),
          lens: LiteLLM.LiteLLMLens.t() | nil
        }

  schema "LiteLLM_LensReview" do
    field(:criteria_key, :string, primary_key: true)
    field(:execution_id, :string, primary_key: true)
    field(:data, :map)
    field(:lens_id, :string, primary_key: true)
    belongs_to(:lens, LiteLLM.LiteLLMLens, foreign_key: :lens_id, define_field: false, type: :string)
  end
end